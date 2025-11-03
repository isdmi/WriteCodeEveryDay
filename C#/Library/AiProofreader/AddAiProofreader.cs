using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiProofreader
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAiProofreader(this IServiceCollection services, IChatClient client)
        {
            services.AddChatClient(client);

            services.AddScoped<IProofreader, SimpleProofreader>();
            services.AddScoped<ProofreadFileProcessor>();
            return services;
        }
    }
}
