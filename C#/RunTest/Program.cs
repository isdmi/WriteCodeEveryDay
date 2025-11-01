using AiProofreader;
using AiSummarizer;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenAI;

var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
string model = "gpt-4o-mini";

var client = new OpenAIClient(config["OPENAI_KEY"]).GetChatClient(model).AsIChatClient();

var services = new ServiceCollection();
services.AddAiProofreader(client);

var provider = services.BuildServiceProvider();
var processor = provider.GetRequiredService<ProofreadFileProcessor>();

await processor.ProcessAsync("ishida.txt", "aaaa.txt");
