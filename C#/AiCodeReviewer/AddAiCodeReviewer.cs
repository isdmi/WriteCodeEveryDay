using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.AI;

namespace AiCodeReviewer
{
    public static class ServiceCollectionExtensions
    {
        // 既に IChatClient インスタンスを作って渡すパターン
        public static IServiceCollection AddAiCodeReviewer(this IServiceCollection services, IChatClient client)
        {
            services.AddChatClient(client);
            services.AddScoped<ICodeReviewer, SimpleCodeReviewer>();
            return services;
        }

        // オプション式で ChatClient を作るパターン（オーバーロード）
        public static IServiceCollection AddAiCodeReviewer(this IServiceCollection services, Action<IServiceCollection>? configureChatClient = null)
        {
            configureChatClient?.Invoke(services);
            services.AddScoped<ICodeReviewer, SimpleCodeReviewer>();
            return services;
        }
    }
}
