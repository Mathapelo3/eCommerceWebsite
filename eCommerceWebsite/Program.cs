using eCommerceWebsite.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using eCommerceWebsite.Repositories;
using Microsoft.AspNetCore.Identity.UI.Services;
using eCommerceWebsite.IRepositories;
using UserEmailSende;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUnit, Unit>();
//builder.Services.AddScoped<IDbInitializer, DbInitialize>();
//injecting the connectionString class to the applicationdbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SoftwareChasersCnn"));
});

//builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddDefaultTokenProviders();
//    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddSingleton<IEmailSender, EmailSender>();
builder.Services.ConfigureApplicationCookie(options =>
{
options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
options.LoginPath = $"/Identity/Account/Login";
options.LogoutPath = $"/Identity/Account/Logout";

});
builder.Services.AddRazorPages();
//builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(100);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


/*options => options.SignIn.RequireConfirmedAccount = true*/
builder.Services.AddDefaultIdentity<IdentityUser>().AddDefaultTokenProviders()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<IUnit, Unit>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
//StripeConfiguration.ApiKey =
//    builder.Configuration.GetSection("PaymentSettings:SecretKey").Get<string>();
//dataSedding();


app.UseAuthentication();;

app.UseAuthorization();

app.UseSession();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
