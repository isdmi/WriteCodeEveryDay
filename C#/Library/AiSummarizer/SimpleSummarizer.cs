using Microsoft.Extensions.AI;

namespace AiSummarizer
{
    public interface ISummarizer
    {
        Task<string> SummarizeAsync(string text);
    }

    public class SimpleSummarizer : ISummarizer
    {
        private readonly IChatClient _chatClient;


        public SimpleSummarizer(IChatClient chatClient)
        {
            _chatClient = chatClient;
        }

        public async Task<string> SummarizeAsync(string text)
        {
            var message = new ChatMessage(ChatRole.User, $"次の文章を短く要約してください:\n{text}");
            var response = await _chatClient.GetResponseAsync(message);

            return response.Text;
        }
    }
}