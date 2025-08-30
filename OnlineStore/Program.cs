using OnlineStore;
using OnlineStore.ExceptionHandling;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDataAccess(builder.Configuration);

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.UseMiddleware<GlobalExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseCors("AllowAngular");
//app.UseHttpsRedirection();

// Disable CORS since angular will be running on port 4200 and the service on port 5258.
app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.Run();
