using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiSqlChecker
{
    public record SqlIssue(string Title, string Description, string Severity);
    public record SqlSuggestion(string Title, string Description, string? Patch = null);

    public class SqlCheckResult
    {
        public string Summary { get; set; } = "";
        public List<SqlIssue> Issues { get; } = new();
        public List<SqlSuggestion> Suggestions { get; } = new();
    }

    public class SqlCheckOptions
    {
        public string Dialect { get; init; } = "sql"; // e.g., "sql", "postgres", "mysql", "sqlserver"
        public bool ProvidePatch { get; init; } = true;
        public bool ExplainPlan { get; init; } = false; // ask for EXPLAIN-based insight
    }
}
