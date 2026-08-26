using Microsoft.EntityFrameworkCore;
using ZeloApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Adiciona Blazor Server
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Registra Banco de Dados em Memória
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

// Executa o DbSeeder para carregar o Mock de dados
DbSeeder.CarregarDadosIniciais(app.Services);

app.Run();