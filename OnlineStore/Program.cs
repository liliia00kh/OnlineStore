using Azure.Core;
using OnlineStore;
using OnlineStore.DTOs;
using Services.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDataAccess(builder.Configuration);

var app = builder.Build();

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

app.MapGet("/api/products", async (HttpContext http, IProductService productService) =>
{
    var products = await productService.GetAllProductsAsync();
    var productDTOs = new List<ProductDTO>();

    foreach (var product in products)
    {
        productDTOs.Add(new ProductDTO
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            ImageUrl = $"{http.Request.Scheme}://{http.Request.Host}{Path.AltDirectorySeparatorChar}{product.ImageUrl.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)}",
            CategoryId = product.CategoryId,
            StockQuantity = product.StockQuantity
        });
    }
    return Results.Ok(productDTOs);
}
);

app.Run();
