using Microsoft.EntityFrameworkCore;
using MotoGP.Data;

var builder = WebApplication.CreateBuilder(args);

// Đăng ký ApplicationDbContext với SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Thêm các service cho controller và view
builder.Services.AddControllersWithViews();

// Thêm Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian session
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Thêm xác thực (Authentication) và phân quyền (Authorization)
builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", config =>
    {
        config.LoginPath = "/Account/Login"; // Trang đăng nhập mặc định
    });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
});

var app = builder.Build();

// Cấu hình pipeline cho môi trường production
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Cấu hình các middleware
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession(); // Thêm dòng này để kích hoạt session

app.UseAuthentication(); // Sử dụng Authentication middleware
app.UseAuthorization(); // Sử dụng Authorization middleware

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");





app.Run();
