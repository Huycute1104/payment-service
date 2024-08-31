using Application.IService;
using Application.Service;
using Application.Utils.GenerateCode;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection InfrastructureService(this IServiceCollection services, IConfiguration configuration)
        {
            #region Repo
            services.AddDbContext<MyDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Default"),
                b => b.MigrationsAssembly(typeof(DependencyInjection).Assembly.FullName)),
                ServiceLifetime.Scoped);
            #endregion
            // service
            services.AddScoped<IPaymentService, PaymentService>();

            // GenerateCode
            services.AddScoped<IGenerateCode, GenerateCode>();

            return services;
        }
    }
}
