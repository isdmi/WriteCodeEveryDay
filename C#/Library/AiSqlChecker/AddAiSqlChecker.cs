using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;

namespace AiSqlChecker
{
    public static class SqlCheckerServiceCollectionExtensions
    {
        // client を既にインスタンス化して渡すパターン
        public static IServiceCollection AddAiSqlChecker(this IServiceCollection services, IChatClient client)
        {
            services.AddChatClient(client);
            services.AddScoped<ISqlChecker, SimpleSqlChecker>();
            return services;
        }

        // ChatClient の登録アクションを受け取るオーバーロード
        public static IServiceCollection AddAiSqlChecker(this IServiceCollection services, Action<IServiceCollection> configureChatClient)
        {
            configureChatClient(services);
            services.AddScoped<ISqlChecker, SimpleSqlChecker>();
            return services;
        }
    }
}
