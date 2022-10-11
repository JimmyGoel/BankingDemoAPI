using ApplicationCore.Interfaces;
using AutoMapper;
using Infrastructure.Logging;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicApi
{
    public static class DependenciesInjections
    {
        public static void ConfigurationServices(this IServiceCollection serviceProvider)
        {
            serviceProvider.AddScoped<IUser, clsUserServices>();
            serviceProvider.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            serviceProvider.AddAutoMapper(typeof(Startup));
        }
    }
}
