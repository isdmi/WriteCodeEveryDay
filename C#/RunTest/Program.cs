using AiCodeReviewer;
using AiProofreader;
using AiSqlChecker;
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
services.AddAiSqlChecker(client);
services.AddScoped<SqlFileProcessor>();
var processor = services.BuildServiceProvider().GetRequiredService<SqlFileProcessor>();

await processor.ProcessFileAsync("ABC.sql");
