using DataAccess.Context;
using DataAccess.Repositories;
using DataAccess.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using OnlineStore.ExceptionHandling;
using Services.Services;

namespace OnlineStore
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccess(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngular",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:4200")
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });

            builder.Services.AddDbContext<OnlineStoreDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IUnitOfWork<OnlineStoreDbContext>, UnitOfWork>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddTransient<GlobalExceptionMiddleware>();

            builder.Services.AddSingleton<MongoContext>();
            builder.Services.AddScoped<IProductChangeLogRepository, ProductChangeLogRepository>();
            builder.Services.AddScoped<IProductChangeLogService, ProductChangeLogService>();

            return builder.Services;
        }

    }
}
