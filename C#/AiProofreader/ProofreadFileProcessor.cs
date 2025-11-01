using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiProofreader
{
    public interface IProofreader
    {
        Task<string> ProofreadAsync(string text);
    }

    public class ProofreadFileProcessor
    {
        private readonly IProofreader _proofreader;

        public ProofreadFileProcessor(IProofreader proofreader)
        {
            _proofreader = proofreader;
        }

        public async Task ProcessAsync(string inputPath, string outputPath)
        {
            string text = await File.ReadAllTextAsync(inputPath);

            string corrected = await _proofreader.ProofreadAsync(text);

            await File.WriteAllTextAsync(outputPath, corrected);
        }
    }
}
