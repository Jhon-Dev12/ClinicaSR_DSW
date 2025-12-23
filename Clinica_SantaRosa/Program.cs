using ClinicaSR.PL.WebApp.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddControllersWithViews();

builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpContextAccessor();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseMiddleware<SecurityConfigMiddleware>();
app.MapControllerRoute(name: "default", pattern: "{controller=Seguridad}/{action=Login}/{id?}");
app.Run();
