﻿using Microsoft.Extensions.DependencyInjection;
using Hydra.Infrastructure.Security.Service;
using Hydra.Infrastructure.Setting.Service;
using Hydra.Infrastructure.Data.Interface;
using Hydra.Infrastructure.Security.Interface;
using Hydra.Infrastructure.Scheduler.Service;
using Hydra.Infrastructure.Data.Repository;

namespace Hydra.Infrastructure.ServiceRegistrar
{
    public static class ServiceRegistrarStartup
    {
        public static void AddServices(this IServiceCollection services)
        {
            // services
            services.AddTransient<ICommandRepository, CommandRepository>();
            services.AddTransient<IQueryRepository, QueryRepository>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IPermissionChecker, PermissionChecker>();
            services.AddScoped<ISettingService, SettingService>();
            services.AddScoped<IScheduleService, ScheduleService>();

        }
    }
}
