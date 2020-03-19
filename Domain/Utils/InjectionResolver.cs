using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Entities;
using Npgsql.EntityFrameworkCore;
using Infrastructure.Interfaces;
using Infrastructure.UnitOfWork;
using Domain.Interfaces;
using Domain.Services;
using Domain.Mappers;

namespace Domain.Utils
{
    public static class InjectionResolver
    {
        public static void Inject(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));
            services.AddIdentity<User, IdentityRole>(o =>
            {
                o.User.RequireUniqueEmail = true;
                o.Password.RequiredLength = 6;
                o.Password.RequireLowercase = true;
                o.Password.RequireUppercase = false;
                o.Password.RequireDigit = true;
                o.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperConfig>();
            });
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);
            services.AddScoped<IJwtFactory, JwtFactory>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IOrderService, OrderService>();
        }
    }
}