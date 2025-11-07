using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.AI;
using System.Text.Json;
using System.Text.Encodings.Web;

namespace AiSqlChecker
{
    public class SimpleSqlChecker : ISqlChecker
    {
        private readonly IChatClient _chatClient;
        public SimpleSqlChecker(IChatClient chatClient)
        {
            _chatClient = chatClient;
        }

        public async Task<SqlCheckResult> CheckAsync(string sql, SqlCheckOptions? options = null)
        {
            options ??= new SqlCheckOptions();
            var prompt = BuildPrompt(sql, options);

            var message = new ChatMessage(ChatRole.User, prompt);
            var response = await _chatClient.GetResponseAsync(message);
            var text = response.Text ?? string.Empty;

            // Try parse JSON — fall back to raw summary if parse fails
            try
            {
                var doc = JsonDocument.Parse(text);
                var root = doc.RootElement;
                var result = new SqlCheckResult
                {
                    Summary = root.GetProperty("summary").GetString() ?? ""
                };

                if (root.TryGetProperty("issues", out var issuesEl) && issuesEl.ValueKind == JsonValueKind.Array)
                {
                    foreach (var it in issuesEl.EnumerateArray())
                    {
                        var title = it.GetProperty("title").GetString() ?? "";
                        var desc = it.GetProperty("description").GetString() ?? "";
                        var severity = it.GetProperty("severity").GetString() ?? "Low";
                        result.Issues.Add(new SqlIssue(title, desc, severity));
                    }
                }

                if (root.TryGetProperty("suggestions", out var suggEl) && suggEl.ValueKind == JsonValueKind.Array)
                {
                    foreach (var it in suggEl.EnumerateArray())
                    {
                        var title = it.GetProperty("title").GetString() ?? "";
                        var desc = it.GetProperty("description").GetString() ?? "";
                        var patch = it.TryGetProperty("patch", out var p) ? p.GetString() : null;
                        result.Suggestions.Add(new SqlSuggestion(title, desc, patch));
                    }
                }

                return result;
            }
            catch
            {
                // パース失敗時は生テキストを summary に入れて返す
                return new SqlCheckResult { Summary = text };
            }
        }

        private string BuildPrompt(string sql, SqlCheckOptions options)
        {
            var explainHint = options.ExplainPlan ? "可能なら EXPLAIN/EXPLAIN ANALYZE を元にした指摘も含めてください。" : "";
            var patchHint = options.ProvidePatch ? "可能なら最適化後の SQL を patch に入れてください。" : "patch は不要です。";

            var schema = @"
                返答は必ず純粋な JSON オブジェクトで返してください。Markdown コードブロック、文字列化、あるいはエスケープ（\uXXXX）で返さないでください。                
                形式:
                {
                  ""summary"": ""string"",
                  ""issues"": [
                    { ""title"": ""string"", ""description"": ""string"", ""severity"": ""High|Med|Low"" }
                  ],
                  ""suggestions"": [
                    { ""title"": ""string"", ""description"": ""string"", ""patch"": ""string (SQL)"" }
                  ]
                }
                ";

            return $@"
                あなたは熟練したデータベースエンジニアです。以下の SQL をレビューしてください。
                目的: 性能、正確性、セキュリティ（SQLインジェクション等）、可読性、DDL/スキーマの問題を検出してください。
                方針: 問題点は severity を付けて列挙し、改善案と（可能なら）最適化後の SQL を patch に入れてください。
                方言: {options.Dialect}
                {explainHint}
                {patchHint}

                {schema}

                === SQL START ===
                {sql}
                === SQL END ===
                ";
        }
    }
}
