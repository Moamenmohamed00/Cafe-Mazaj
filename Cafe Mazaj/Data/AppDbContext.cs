using Cafe_Mazaj.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cafe_Mazaj.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }
        public DbSet<GalleryImage> GalleryImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, NameEn = "Coffee",    NameAr = "قهوة",     Slug = "coffee",    SortOrder = 1 },
                new Category { Id = 2, NameEn = "Cold Drinks", NameAr = "مشروبات باردة", Slug = "cold-drinks", SortOrder = 2 },
                new Category { Id = 3, NameEn = "Hot Drinks", NameAr = "مشروبات ساخنة", Slug = "hot-drinks",  SortOrder = 3 },
                new Category { Id = 4, NameEn = "Food",      NameAr = "أكل",      Slug = "food",      SortOrder = 4 },
                new Category { Id = 5, NameEn = "Desserts",  NameAr = "حلويات",   Slug = "desserts",  SortOrder = 5 }
            );

            // Seed Sample Products
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1, CategoryId = 1,
                    NameEn = "Espresso",    NameAr = "إسبريسو",
                    DescriptionEn = "Rich, concentrated coffee shot.",
                    DescriptionAr = "جرعة قهوة مركزة وغنية.",
                    Price = 25, IsFeatured = true, IsAvailable = true,
                    ImagePath = "/uploads/products/espresso.jpg",
                    CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Product
                {
                    Id = 2, CategoryId = 1,
                    NameEn = "Cappuccino",  NameAr = "كابتشينو",
                    DescriptionEn = "Espresso with steamed milk foam.",
                    DescriptionAr = "إسبريسو مع رغوة الحليب.",
                    Price = 35, IsFeatured = true, IsAvailable = true,
                    ImagePath = "/uploads/products/cappuccino.jpg",
                    CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Product
                {
                    Id = 3, CategoryId = 1,
                    NameEn = "Latte",       NameAr = "لاتيه",
                    DescriptionEn = "Smooth espresso with steamed milk.",
                    DescriptionAr = "إسبريسو ناعم مع الحليب.",
                    Price = 35, IsFeatured = true, IsAvailable = true,
                    ImagePath = "/uploads/products/latte.jpg",
                    CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Product
                {
                    Id = 4, CategoryId = 2,
                    NameEn = "Iced Latte",  NameAr = "لاتيه بارد",
                    DescriptionEn = "Cold latte over ice.",
                    DescriptionAr = "لاتيه بارد على الثلج.",
                    Price = 40, IsFeatured = true, IsAvailable = true,
                    ImagePath = "/uploads/products/iced-latte.jpg",
                    CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Product
                {
                    Id = 5, CategoryId = 4,
                    NameEn = "Croissant",   NameAr = "كرواسون",
                    DescriptionEn = "Buttery, flaky French pastry.",
                    DescriptionAr = "معجنة فرنسية هشة بالزبدة.",
                    Price = 30, IsFeatured = true, IsAvailable = true,
                    ImagePath = "/uploads/products/croissant.jpg",
                    CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Product
                {
                    Id = 6, CategoryId = 5,
                    NameEn = "Chocolate Cake", NameAr = "كيك شوكولاتة",
                    DescriptionEn = "Rich dark chocolate layer cake.",
                    DescriptionAr = "كيك شوكولاتة داكنة غنية.",
                    Price = 45, IsFeatured = true, IsAvailable = true,
                    ImagePath = "/uploads/products/chocolate-cake.jpg",
                    CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                }
            );
        }
    }
}
