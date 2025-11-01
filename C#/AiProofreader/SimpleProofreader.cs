using Microsoft.Extensions.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiProofreader
{
    public class SimpleProofreader : IProofreader
    {
        private readonly IChatClient _chatClient;

        public SimpleProofreader(IChatClient chatClient)
        {
            _chatClient = chatClient;
        }

        public async Task<string> ProofreadAsync(string text)
        {
            var message = new ChatMessage(ChatRole.User, $"次の文章を校正してください。明確で自然で読みやすく修正してください:\n{text}");
            var response = await _chatClient.GetResponseAsync(message);

            return response.Text;
        }

    }
}
