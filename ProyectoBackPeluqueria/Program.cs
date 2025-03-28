using ProyectoBackPeluqueria.Data;
using ProyectoBackPeluqueria.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using ProyectoBackPeluqueria.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddAuthentication(
    options =>
    {
        options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    }
).AddCookie(options =>
{
    // Puedes agregar algunas configuraciones de cookies si es necesario
    options.LoginPath = "/Auth/Login";  // Ruta donde se redirige a Login
    options.AccessDeniedPath = "/Auth/AccessDenied";  // Ruta para acceso denegado
});

builder.Services.AddControllersWithViews();  // Configuraci�n de controladores y vistas

builder.Services.AddTransient<RepositoryPeluqueria>();
builder.Services.AddTransient<ServicePeluqueria>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlPeluqueria")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();  // Aseg�rate de que la autenticaci�n est� configurada antes de la autorizaci�n
app.UseAuthorization();   // Aseg�rate de que la autorizaci�n est� configurada despu�s de la autenticaci�n

app.UseStaticFiles();  // Importante: habilita el uso de archivos est�ticos (CSS, JS, im�genes)

app.UseSession();  // Aseg�rate de que las sesiones est�n habilitadas

// Configuraci�n de las rutas de los controladores
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Inicia la aplicaci�n
app.Run();
