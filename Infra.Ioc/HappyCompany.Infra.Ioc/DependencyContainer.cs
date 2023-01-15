using HappyCompany.Application.Helpers;
using HappyCompany.Application.Repos;
using HappyCompany.Application.Services;
using HappyCompany.Domain.Interface.Common;
using HappyCompany.Domain.Interface.Repos;
using HappyCompany.Domain.Interface.Services;
using HappyCompany.Domain.Models;
using HappyCompany.Domain.Models.Helpers;
using HappyCompany.Infra.Utils;
using HappyCompany.Infra.Utils.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace HappyCompany.Infra.Ioc
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {

            services.AddTransient<IJsonUtils, JsonUtils>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IRepository<User>, UserRepository>();
            services.AddScoped<IRepository<Warehouse>, WarehouseRepository>();
            services.AddScoped<IRepository<Item>, ItemRepository>();
            services.AddScoped<IUserServiceHandler, UserServiceHandler>();
            services.AddScoped<IWarehouseServiceHandler, WarehouseServiceHandler>();
            services.AddScoped<IItemServiceHandler, ItemServiceHandler>();
            services.AddScoped<IJWTUtils, JWTUtils>();
            services.AddScoped<IJwtSettings, JwtSettings>();
            
        }



    }
}