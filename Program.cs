using Microsoft.EntityFrameworkCore;
using ZeloApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Configuração Blazor Server
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Banco em memória
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("ZeloDb"));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<ZeloApp.Components.App>()
    .AddInteractiveServerRenderMode();

// GARANTE QUE O BANCO DE DADOS EM MEMÓRIA É POPULADO COM O MOCK
DbSeeder.CarregarDadosIniciais(app.Services);

app.Run();