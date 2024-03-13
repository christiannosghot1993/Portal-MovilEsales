using Microsoft.AspNetCore.Authentication.Cookies;
using Portal_MovilEsales.Middlewares;
using Portal_MovilEsales.Services;
using Portal_MovilEsales.Services.AprobadorServices;
using Portal_MovilEsales.Services.AsesorServices;
using Portal_MovilEsales.Services.ClienteServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
#region Configuración de las variables de session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Establece el tiempo de espera de la sesión
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
#endregion
builder.Services.AddScoped(typeof(IService), typeof(Service));
builder.Services.AddScoped(typeof(IAsesorService), typeof(AsesorService));
builder.Services.AddScoped(typeof(IAprobadorService), typeof(AprobadorService));
builder.Services.AddScoped(typeof(IClienteService), typeof(ClienteService));
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            /*Sirve para evitar ataques Cross-Site Scripting - XSS, ya que al no estar disponible 
             * para cceder por https no se pueden ejecutar scripts maliciosos en la web*/
            options.Cookie.HttpOnly = true;
            /*Tiempo de expiración de la cookie (Se renueva cada nueva solicitud, si no se realiza 
             * ninguna solicitud en el tiempo especificado de la solicitud, la coockie expira)*/
            options.ExpireTimeSpan = TimeSpan.FromMinutes(15); 
            options.LoginPath = "/Login/Index"; // Ruta a la página de inicio de sesión
            options.AccessDeniedPath = "/Account/AccessDenied"; // Ruta a la página de acceso denegado
        });

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
app.UseSession();//instrucción para habilitzar la configuración de variables de session
app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseMiddleware<RedirectIfAuthenticatedMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}");
app.Run();
