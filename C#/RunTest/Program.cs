using AiCodeReviewer;
using AiProofreader;
using AiSummarizer;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenAI;
using static AiCodeReviewer.AiCodeReviewer;

var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
string model = "gpt-4o-mini";

var client = new OpenAIClient(config["OPENAI_KEY"]).GetChatClient(model).AsIChatClient();

var services = new ServiceCollection();
services.AddAiCodeReviewer(client);
services.AddScoped<CodeReviewFileProcessor>();
var processor = services.BuildServiceProvider().GetRequiredService<CodeReviewFileProcessor>();

await processor.ProcessFileAsync("AtCoder1102.cs", "AtCoder1102.cs.review.json");
