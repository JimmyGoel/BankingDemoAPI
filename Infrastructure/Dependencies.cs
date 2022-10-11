using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("BankingApp"),
                    b => b.MigrationsAssembly(typeof(DataContext).Assembly.FullName)));

            //services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
        }
    }
}
