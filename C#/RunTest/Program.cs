using AiSummarizer;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using OpenAI;

var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
string model = "gpt-4o-mini";

var summarizer = new SimpleSummarizer(new OpenAIClient(config["OPENAI_KEY"]).GetChatClient(model).AsIChatClient());

string text = @"今日はとても天気が良く、気温も高いため多くの観光客が公園に訪れています。";

string summary = await summarizer.SummarizeAsync(text);

Console.WriteLine("要約結果:");
Console.WriteLine(summary);