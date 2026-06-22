using Cafe_Mazaj.Data;
using Cafe_Mazaj.Services.Category;
using Cafe_Mazaj.Services.ContactMessage;
using Cafe_Mazaj.Services.Email;
using Cafe_Mazaj.Services.Gallery;
using Cafe_Mazaj.Services.Images;
using Cafe_Mazaj.Services.Product;
using Cafe_Mazaj.Services.Reservation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ── Database ──────────────────────────────────────────────
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ── Session ───────────────────────────────────────────────
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(8);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddHttpContextAccessor();

// ── Email Settings ────────────────────────────────────────
builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection("EmailSettings"));

// ── App Services ──────────────────────────────────────────
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IGalleryService, GalleryService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IEmailService, EmailService>();

// ── MVC ───────────────────────────────────────────────────
builder.Services.AddControllersWithViews();

var app = builder.Build();

// ── Middleware Pipeline ───────────────────────────────────
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

// ── Route: Admin Area ─────────────────────────────────────
app.MapControllerRoute(
    name: "admin",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

// ── Route: Auth (Login/Logout) ────────────────────────────
app.MapControllerRoute(
    name: "auth",
    pattern: "admin/{action=Login}/{id?}",
    defaults: new { controller = "Auth" });

// ── Route: Default ────────────────────────────────────────
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// ── Auto-Migrate on Startup (Dev only) ───────────────────
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.Run();
