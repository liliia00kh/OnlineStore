using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Context
{
    public class OnlineStoreDbContext : DbContext
    {
        public OnlineStoreDbContext(DbContextOptions<OnlineStoreDbContext> options) : base(options) { }
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Category
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Name)
                      .IsRequired()
                      .HasMaxLength(100);
            });

            // Product
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Name)
                      .IsRequired()
                      .HasMaxLength(200);
                entity.Property(p => p.Description)
                      .HasMaxLength(1000);
                entity.Property(p => p.Price)
                      .HasColumnType("decimal(7,2)");
                entity.Property(p => p.ImageUrl)
                      .HasMaxLength(500);

                entity.HasOne(p => p.Category)
                      .WithMany(c => c.Products)
                      .HasForeignKey(p => p.CategoryId);
            });

            modelBuilder.Entity<Product>().ToTable(tb =>
            {
                tb.HasCheckConstraint("CK_Product_StockQuantity_NonNegative", "[StockQuantity] >= 0");
            });

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Bags" },
                new Category { Id = 2, Name = "Wallets" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Bag 1", Description = "Bag 1 description", Price = 599.99m, StockQuantity = 50, CategoryId = 1, ImageUrl = "images\\bags\\bag-1.jpg" },
                new Product { Id = 2, Name = "Bag 2", Description = "Bag 2 description", Price = 999.99m, StockQuantity = 30, CategoryId = 1, ImageUrl = "images\\bags\\bag-2.jpg" },
                new Product { Id = 3, Name = "Bag 3", Description = "Bag 3 description", Price = 19.99m, StockQuantity = 100, CategoryId = 1, ImageUrl = "images\\bags\\bag-3.jpg" },
                new Product { Id = 4, Name = "Bag 4", Description = "Bag 4 description", Price = 12.99m, StockQuantity = 70, CategoryId = 1, ImageUrl =  "images\\bags\\bag-4.jpg" }
            );

        }
    }
}
