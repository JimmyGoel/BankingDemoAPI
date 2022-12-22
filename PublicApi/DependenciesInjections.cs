using ApplicationCore.Interfaces;
using AutoMapper;
using Infrastructure.Logging;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using PublicApi.Mapping;
using PublicApi.MiddleWare;
using PublicApi.Services;

namespace PublicApi
{
    public static class DependenciesInjections
    {
        public static void ConfigurationServices(this IServiceCollection serviceProvider)
        {
            serviceProvider.AddTransient<IUser, clsUserServices>();
            serviceProvider.AddTransient<IuserExtend, clsUserServices>();
            serviceProvider.AddTransient<LogUserActivity>();
            serviceProvider.AddTransient<IAccountRegistor, clsAccountService>();
            serviceProvider.AddTransient<ILoginUser, clsAccountService>();
            serviceProvider.AddTransient<IPhotoServices, PhotoServices>();
            serviceProvider.AddTransient(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            serviceProvider.AddAutoMapper(typeof(Startup));
            IMapper mapper = MapperProfile.RegisterMaps().CreateMapper();
            serviceProvider.AddSingleton(mapper);
            serviceProvider.AddTransient<ITokenServices, TokenServices>();
        }
    }
}
