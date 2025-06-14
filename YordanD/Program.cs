using InertiaCore;
using InertiaCore.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using YordanD.Data;
using YordanD.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => {
        options.SignIn.RequireConfirmedAccount = false;
        
        options.User.RequireUniqueEmail = false;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>();


builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.LoginPath = "/login";
});

builder.Services.AddControllersWithViews();

builder.Services.AddInertia(options => {
    options.RootView = "~/Views/Template.cshtml";
    // options.SsrEnabled = true;
});

builder.Services.AddViteHelper(options => {
    options.PublicDirectory = "wwwroot";
    options.BuildDirectory = "build";
    options.HotFile = "hot";
    options.ManifestFilename = "manifest.json";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseMigrationsEndPoint();
}
else {
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseInertia();
app.Use(async (ctx, next) => {
    var userManager = ctx.RequestServices.GetRequiredService<UserManager<ApplicationUser>>();
    
    var username = ctx.User.Identity?.Name;
    if (username != null) {
         var user = await userManager.FindByNameAsync(username);
         
         if (user != null) {
             Inertia.Share("auth", new {
                 User = new {
                     Id = user.Id,
                     Username = user.UserName,
                     DisplayName = user.DisplayName,
                 },
             });
         }
    }

    await next();
});

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
    .WithStaticAssets();


app.Run();