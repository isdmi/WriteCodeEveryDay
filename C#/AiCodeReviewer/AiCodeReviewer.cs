using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                var json = System.Text.Json.JsonSerializer.Serialize(result, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
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