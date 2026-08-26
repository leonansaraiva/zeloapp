using Microsoft.EntityFrameworkCore;
using ZeloApp.Models;

// Desativa o reloadOnChange para evitar o estouro de inotify no Render
var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args
});

builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: false);

// Adiciona Blazor Server
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Registra Banco em Memória
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

// Carga do Mock de Dados
DbSeeder.CarregarDadosIniciais(app.Services);

app.Run();