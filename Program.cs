using Microsoft.EntityFrameworkCore;
using ZeloApp.Models;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    ContentRootPath = Directory.GetCurrentDirectory()
});

// Desativa o monitoramento constante de arquivos no Linux (evita estouro de inotify/crash 139)
builder.Configuration.Sources.Clear();
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: false)
    .AddEnvironmentVariables();

// CONFIGURAÇÃO DA PORTA PARA O RENDER.COM
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

// Configurações do Blazor Server
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Banco em Memória
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("ZeloDb"));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<ZeloApp.Components.App>()
    .AddInteractiveServerRenderMode();

// Carrega o Mock de Dados Inicial
DbSeeder.CarregarDadosIniciais(app.Services);

app.Run();