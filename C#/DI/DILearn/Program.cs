using DILearn;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddScoped<IMessageWriter, MessageWriter>();
services.AddScoped<FooService>();

var provider = services.BuildServiceProvider();
var fooService = provider.GetService<FooService>();

fooService.Execute();