using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hydra.Infrastructure.Security
{
    public static class CorsStartup
    {
        public const string FRONTEND_CORS = "NextCors";
        public static void AddCorsPolicy(this IServiceCollection services,
            IConfiguration configuration)
        {
            var coreOriginUrl = configuration["CoreOrigin:Url"];
            var authorityUrl = configuration["Authentication:Schemes:Bearer:Authority"];

            services.AddCors(options =>
            {
                options.AddPolicy(FRONTEND_CORS,
                        builder =>
                        {
                            builder.WithOrigins(authorityUrl, coreOriginUrl).AllowAnyHeader().AllowAnyMethod();
                        });
            });
        }

        public static void UseCorsFrontend(this IApplicationBuilder services)
        {
            services.UseCors(FRONTEND_CORS);
        }
    }
}
