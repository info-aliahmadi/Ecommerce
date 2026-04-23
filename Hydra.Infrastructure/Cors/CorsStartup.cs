using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Hydra.Infrastructure.Security
{
    public static class CorsStartup
    {
        public const string FRONTEND_CORS = "NextCors";
        public static void AddCorsPolicy(this IServiceCollection services,
            IConfiguration configuration)
        {
            var coreOriginUrl = configuration["CoreOrigin:Url"];

            services.AddCors(options =>
            {
                options.AddPolicy(FRONTEND_CORS,
                        builder =>
                        {
                            builder.WithOrigins(configuration["Authentication:Schemes:Bearer:Authority"]).AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                            builder.WithOrigins(coreOriginUrl).AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                        });
            });
        }
    }
}
