using Microsoft.Extensions.AI;
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
            var sb = new StringBuilder();
            var shortHint = options.ShortMode ? "短く回答してください。" : "詳細に回答してください。";
            var patchHint = options.ProvidePatch ? "可能な場合は修正パッチ（差分や修正版コード）をpatchフィールドに入れてください。" : "patchは不要です。";

            sb.AppendLine("あなたはプロフェッショナルなコードレビューツールとして動作します。");

            sb.AppendLine("## 🔒 出力フォーマット規則（必ず守ること）");
            sb.AppendLine("- 回答は **純粋な JSON オブジェクトのみ** を返すこと。");
            sb.AppendLine("- JSON を Markdown 形式（```json ... ```）で囲んではいけません。");
            sb.AppendLine("- JSON を文字列としてエスケープして返してはいけません。");
            sb.AppendLine("- JSON のキー名は以下のスキーマに完全に一致させること。");
            sb.AppendLine("- コメント、説明文、前置き、後置きは一切返してはいけません。JSON 以外の文字を含めてはいけません。");

            sb.AppendLine("## ✅ 出力 JSON スキーマ");
            sb.AppendLine("{");
            sb.AppendLine(@"  ""summary"": ""短い要約"",");
            sb.AppendLine(@"  ""issues"": [{ ""title"": ""問題名"", ""description"": ""詳細"", ""severity"": ""Low|Med|High"" }],");
            sb.AppendLine(@"  ""suggestions"": [{ ""title"": ""改善案名"", ""description"": ""説明"", ""patch"": ""（オプション。差分や修正版コード）"" }]");
            sb.AppendLine("}");

            sb.AppendLine("## 📘 レビュー基準");
            sb.AppendLine("- バグの可能性");
            sb.AppendLine("- 例外処理の不足");
            sb.AppendLine("- Nullチェック不足");
            sb.AppendLine("- パフォーマンス問題");
            sb.AppendLine("- 可読性・メンテナンス性");
            sb.AppendLine("- セキュリティ問題");
            sb.AppendLine("- 最適化余地");
            sb.AppendLine("- C# ベストプラクティス準拠");

            sb.AppendLine("## 📘 出力要件");
            sb.AppendLine("- summary は全体の問題を 1～3 文で要約");
            sb.AppendLine("- issues は具体的な問題点と severity（重要度）を含める");
            sb.AppendLine("- suggestions は改善提案と patch（修正例コード）を提供");
            sb.AppendLine("- patch は実際に使える C# コード片を含める");

            sb.AppendLine("## 📘 出力オプション");
            sb.AppendLine($"言語:{options.Language} ");
            sb.AppendLine(shortHint);
            sb.AppendLine(patchHint);

            sb.AppendLine("## ✅ 実際のレビュー対象コード");
            sb.AppendLine("=== コード開始 ===");
            sb.AppendLine(code);
            sb.AppendLine("=== コード終わり ===");

            return sb.ToString();
        }
    }
}
