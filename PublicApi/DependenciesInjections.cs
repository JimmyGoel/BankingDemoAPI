using ApplicationCore.Interfaces;
using AutoMapper;
using Infrastructure.Logging;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using PublicApi.Mapping;
using PublicApi.Services;
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
            serviceProvider.AddScoped<IAccountRegistor, clsAccountService>();
            serviceProvider.AddScoped<ILoginUser, clsAccountService>();
            serviceProvider.AddScoped<IPhotoServices, PhotoServices>();
            serviceProvider.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            serviceProvider.AddAutoMapper(typeof(Startup));
            IMapper mapper = MapperProfile.RegisterMaps().CreateMapper();
            serviceProvider.AddSingleton(mapper);
            serviceProvider.AddScoped<ITokenServices, TokenServices>();
        }
    }
}
