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

            // EF Core
            // Select the connection string based on the environment
            string defaultConnection;

            if (builder.Environment.IsDevelopment() && Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") != "true")
            {
                // Local run (Visual Studio / dotnet run)
                defaultConnection = builder.Configuration.GetConnectionString("DefaultConnection");
            }
            else
            {
                // Running in Docker or in Production
                // Prefer the connection string from the environment variable if available,
                // otherwise fall back to the one in appsettings.json
                defaultConnection = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")
                                    ?? builder.Configuration.GetConnectionString("DefaultConnection");
            }

            // Configure Entity Framework to use SQL Server with the selected connection string
            builder.Services.AddDbContext<OnlineStoreDbContext>(options =>
                options.UseSqlServer(defaultConnection));

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
