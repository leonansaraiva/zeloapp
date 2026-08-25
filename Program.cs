using Microsoft.EntityFrameworkCore;
using ZeloApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Adiciona suporte aos componentes Blazor Server
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Registra o banco em memória
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

app.Run();