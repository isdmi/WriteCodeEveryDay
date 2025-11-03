using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace AiCodeReviewer
{
    public class AiCodeReviewer
    {
        public class CodeReviewFileProcessor
        {
            private readonly ICodeReviewer _reviewer;

            public CodeReviewFileProcessor(ICodeReviewer reviewer)
            {
                _reviewer = reviewer;
            }

            public async Task ProcessFileAsync(string inputPath, string outputPath, CodeReviewOptions? options = null)
            {
                var code = await File.ReadAllTextAsync(inputPath);
                var result = await _reviewer.ReviewAsync(code, options);
                var serializerOptions = new JsonSerializerOptions
                {
                    WriteIndented = true, 
                    // + とかが文字化けするのでシリアライズを緩くする。
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                };

                var json = JsonSerializer.Serialize(result, serializerOptions);
                await File.WriteAllTextAsync(outputPath, json);
            }

            public async Task ProcessDirectoryAsync(string inputDir, string outputDir, string searchPattern = "*.cs", CodeReviewOptions? options = null)
            {
                Directory.CreateDirectory(outputDir);
                var files = Directory.EnumerateFiles(inputDir, searchPattern, SearchOption.AllDirectories);
                foreach (var f in files)
                {
                    var rel = Path.GetRelativePath(inputDir, f);
                    var outPath = Path.Combine(outputDir, rel + ".review.json");
                    Directory.CreateDirectory(Path.GetDirectoryName(outPath)!);
                    await ProcessFileAsync(f, outPath, options);
                }
            }
        }
    }
}