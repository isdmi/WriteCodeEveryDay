using Microsoft.Extensions.AI;
using Microsoft.Extensions.Primitives;
using System.Text;
using System.Text.Json;

namespace AiCodeReviewer
{
    public class SimpleCodeReviewer : ICodeReviewer
    {
        private readonly IChatClient _chatClient;

        public SimpleCodeReviewer(IChatClient chatClient)
        {
            _chatClient = chatClient;
        }

        public async Task<CodeReviewResult> ReviewAsync(string code, CodeReviewOptions? options = null)
        {
            options ??= new CodeReviewOptions();

            var prompt = BuildPrompt(code, options);

            var message = new ChatMessage(ChatRole.User, prompt);
            var response = await _chatClient.GetResponseAsync(message);

            var text = response.Text ?? string.Empty;

            try
            {
                // AI に JSON 形式で出力させるようにプロンプトするのでパースを試みる
                var doc = JsonDocument.Parse(text);
                var result = new CodeReviewResult
                {
                    Summary = doc.RootElement.GetProperty("summary").GetString() ?? string.Empty
                };

                if (doc.RootElement.TryGetProperty("issues", out var issuesEl) && issuesEl.ValueKind == JsonValueKind.Array)
                {
                    foreach (var it in issuesEl.EnumerateArray())
                    {
                        var title = it.GetProperty("title").GetString() ?? "";
                        var desc = it.GetProperty("description").GetString() ?? "";
                        var sev = it.GetProperty("severity").GetString() ?? "Low";
                        result.Issues.Add(new CodeIssue(title, desc, sev));
                    }
                }

                if (doc.RootElement.TryGetProperty("suggestions", out var suggEl) && suggEl.ValueKind == JsonValueKind.Array)
                {
                    foreach (var it in suggEl.EnumerateArray())
                    {
                        var title = it.GetProperty("title").GetString() ?? "";
                        var desc = it.GetProperty("description").GetString() ?? "";
                        var patch = it.TryGetProperty("patch", out var p) ? p.GetString() : null;
                        result.Suggestions.Add(new CodeSuggestion(title, desc, patch));
                    }
                }

                return result;
            }
            catch
            {
                // JSON パース失敗時は自由テキストを summary に入れて返す。
                return new CodeReviewResult { Summary = text };
            }
        }

        private string BuildPrompt(string code, CodeReviewOptions options)
        {
            // シンプルな JSON 出力指示を含めたプロンプトテンプレート
            StringBuilder jsonSpecString = new StringBuilder();
            jsonSpecString.AppendLine("返答は必ず JSON 形式で出力してください。形式:");
            jsonSpecString.AppendLine("{");
            jsonSpecString.AppendLine(@"  ""summary"": ""短い要約"",");
            jsonSpecString.AppendLine(@"  ""issues"": [{ ""title"": ""問題名"", ""description"": ""詳細"", ""severity"": ""Low|Med|High"" }],");
            jsonSpecString.AppendLine(@"  ""suggestions"": [{ ""title"": ""改善案名"", ""description"": ""説明"", ""patch"": ""（オプション。差分や修正版コード）"" }]");
            jsonSpecString.AppendLine("}");
            
            var shortHint = options.ShortMode ? "短く回答してください。" : "詳細に回答してください。";
            var patchHint = options.ProvidePatch ? "可能な場合は修正パッチ（差分や修正版コード）をpatchフィールドに入れてください。" : "patchは不要です。";

            StringBuilder promptString = new StringBuilder();
            promptString.AppendLine("あなたは熟練したソフトウェアエンジニアです。以下のコードをレビューし、");
            promptString.AppendLine("・重大なバグや論理的誤り");
            promptString.AppendLine("・性能上の懸念");
            promptString.AppendLine("・セキュリティ問題");
            promptString.AppendLine("・可読性・命名・設計の改善点");
            promptString.AppendLine("を見つけ、上記の JSON 形式に従って返してください。");
            promptString.AppendLine($"言語:{options.Language} ");
            promptString.AppendLine(shortHint);
            promptString.AppendLine(patchHint);
            promptString.AppendLine(jsonSpecString.ToString());
            promptString.AppendLine("=== コード開始 ===");
            promptString.AppendLine(code);
            promptString.AppendLine("=== コード終わり ===");

            return promptString.ToString();
        }
    }
}
