using AiCodeReviewer;
using Microsoft.Extensions.AI;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AiUnitTest
{
    public class MockChatClient : IChatClient
    {
        private readonly Func<ChatMessage, CodeReviewResult> _factory;

        public MockChatClient(Func<ChatMessage, CodeReviewResult> factory)
        {
            _factory = factory;
        }

        public async Task<ChatResponse> GetResponseAsync(
            IEnumerable<ChatMessage> chatMessages,
            ChatOptions? options = null,
            CancellationToken cancellationToken = default)
        {
            // Simulate some operation.
            await Task.Delay(300, cancellationToken);

            var review = _factory(chatMessages.SingleOrDefault());

            string json = JsonSerializer.Serialize(review, new JsonSerializerOptions
            {
                WriteIndented = false,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });

            var response = new ChatResponse(
                messages: new[] { new ChatMessage(ChatRole.Assistant, json) }
            );

            return new(new ChatMessage(ChatRole.Assistant,response.Text));
        }

        public async IAsyncEnumerable<ChatResponseUpdate> GetStreamingResponseAsync(
            IEnumerable<ChatMessage> chatMessages,
            ChatOptions? options = null,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            // Simulate streaming by yielding messages one by one.
            string[] words = ["This ", "is ", "the ", "response ", "for ", "the ", "request."];
            foreach (string word in words)
            {
                // Simulate some operation.
                await Task.Delay(100, cancellationToken);

                // Yield the next message in the response.
                yield return new ChatResponseUpdate(ChatRole.Assistant, word);
            }
        }

        public object? GetService(Type serviceType, object? serviceKey) => this;

        public TService? GetService<TService>(object? key = null)
            where TService : class => this as TService;

        void IDisposable.Dispose() { }
    }
}
