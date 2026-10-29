
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShopSphere.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Enable MVC controllers and views
builder.Services.AddControllersWithViews();

// 2. Connect to the existing MySQL database
var connectionString = builder.Configuration
    .GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        connectionString,
        new MySqlServerVersion(new Version(10, 4, 0))
    ));

// 3. Enable registration and login using ASP.NET Core Identity
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;

    options.Password.RequiredLength = 8;
    options.Password.RequireDigit = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
})
.AddEntityFrameworkStores<AppDbContext>();

// 4. Enable Identity pages
builder.Services.AddRazorPages();

var app = builder.Build();

// 5. Configure the application
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// 6. Enable authentication BEFORE authorization
app.UseAuthentication();
app.UseAuthorization();

// 7. Configure routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Store}/{action=Index}/{id?}"
);

// 8. Enable Register and Login pages
app.MapRazorPages();

// 9. Seed starter products, if you already created DbSeeder.cs
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider
        .GetRequiredService<AppDbContext>();

    await DbSeeder.SeedAsync(db);
}

app.Run();
