using Microsoft.Extensions.AI;
using System.Text.Json;

namespace AiCustomControl
{
    public class AiChatClient
    {
        private readonly IChatClient _chatClient;
        public AiChatClient(IChatClient chatClient)
        {
            _chatClient = chatClient;
        }
    }
}