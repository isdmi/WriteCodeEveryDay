using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiCodeReviewer
{
    public record CodeIssue(string Title, string Description, string Severity); // Severity: Low/Med/High
    public record CodeSuggestion(string Title, string Description, string? Patch = null);

    public class CodeReviewResult
    {
        public string Summary { get; init; } = string.Empty;
        public List<CodeIssue> Issues { get; } = new();
        public List<CodeSuggestion> Suggestions { get; } = new();
    }

    public class CodeReviewOptions
    {
        public string Language { get; init; } = "csharp";
        public bool ProvidePatch { get; init; } = true;
        public bool ShortMode { get; init; } = false;
    }
}
