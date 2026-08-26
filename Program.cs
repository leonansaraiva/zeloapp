using Microsoft.EntityFrameworkCore;
using ZeloApp.Models;

// 1. Desativa o comportamento padrão de monitoramento de arquivos do Host
var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    ContentRootPath = Directory.GetCurrentDirectory()
});

// 2. Limpa os provedores de configuração padrão (que causam o estouro de inotify)
builder.Configuration.Sources.Clear();

// 3. Adiciona as configurações explicitamente sem monitorar mudanças de arquivo em tempo real
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: false)
    .AddEnvironmentVariables();

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

// Carrega os dados Iniciais (Mock)
DbSeeder.CarregarDadosIniciais(app.Services);

app.Run();