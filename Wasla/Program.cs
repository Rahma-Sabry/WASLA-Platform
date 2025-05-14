using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Wasla.Models;

var builder = WebApplication.CreateBuilder(args);

// Connection string for the database
var connectionString = builder.Configuration.GetConnectionString("constr");

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<WaslaContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDbContext<WaslaSecurityContext>(options =>  options.UseSqlServer(connectionString));

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<WaslaSecurityContext>();
builder.Services.AddDistributedMemoryCache(); // For storing session in memory
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddDataProtection();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseSession();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();