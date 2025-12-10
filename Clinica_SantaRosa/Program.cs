using System.Data.Common;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();


builder.Services.AddSingleton<IConfiguration>(builder.Configuration);


var app = builder.Build();


app.UseHttpsRedirection();//para poder ir a cualquier ruta de mis controladores ruta o vista
app.UseStaticFiles();//para poder usar la carpeta root 
//donde estan los archivos html, css, js que sirven para funcionar+

app.UseRouting(); //para que maneje todas las rutas del proyecto 

//app.UseAuthentication();
//app.UseAuthorization();


//app.MapGet("/", () => "Hello World!");
app.MapControllerRoute(name: "default", pattern: "{controller=Seguridad}/{action=Home}/{id?}");






app.Run();
