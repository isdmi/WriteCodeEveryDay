using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace AiSqlChecker
{
    public class SqlFileProcessor
    {
        private readonly ISqlChecker _checker;
        public SqlFileProcessor(ISqlChecker checker) => _checker = checker;

        public async Task ProcessFileAsync(string inputPath, SqlCheckOptions? options = null)
        {
            var sql = await File.ReadAllTextAsync(inputPath);
            var result = await _checker.CheckAsync(sql, options);
            var json = JsonSerializer.Serialize(result, new JsonSerializerOptions { WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping });
            string outputPath = inputPath + ".review.json";
            await File.WriteAllTextAsync(outputPath, json, Encoding.UTF8);
        }

        public async Task ProcessDirectoryAsync(string inputDir, string outputDir, string pattern = "*.sql", SqlCheckOptions? options = null)
        {
            Directory.CreateDirectory(outputDir);
            var files = Directory.EnumerateFiles(inputDir, pattern, SearchOption.AllDirectories);
            foreach (var f in files)
            {
                var rel = Path.GetRelativePath(inputDir, f);
                var outPath = Path.Combine(outputDir, rel + ".review.json");
                Directory.CreateDirectory(Path.GetDirectoryName(outPath)!);
                await ProcessFileAsync(f, options);
            }
        }
    }
}
