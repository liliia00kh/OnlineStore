using DataAccess.Context;
using DataAccess.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using OnlineStore.ExceptionHandling;
using Services.Services;

namespace OnlineStore
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAngular",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:4200")
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });

            // EF Core
            services.AddDbContext<OnlineStoreDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUnitOfWork<OnlineStoreDbContext>, UnitOfWork>();
            services.AddScoped<IProductService, ProductService>();
            services.AddTransient<GlobalExceptionMiddleware>();

            return services;
        }

    }
}
