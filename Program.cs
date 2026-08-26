using Microsoft.EntityFrameworkCore;
using ZeloApp.Models;

// 1. Desativa explicitamente o uso de FileSystemWatcher/inotify do .NET no nível do processo
Environment.SetEnvironmentVariable("DOTNET_USE_POLLING_FILE_WATCHER", "false");

var builder = WebApplication.CreateEmptyBuilder(new WebApplicationOptions
{
    Args = args,
    ContentRootPath = Directory.GetCurrentDirectory()
});

// 2. Adiciona os serviços essenciais do WebHost manualmente
builder.WebHost.UseKestrel(options =>
{
    var portEnv = Environment.GetEnvironmentVariable("PORT") ?? "8080";
    if (int.TryParse(portEnv, out var port))
    {
        options.ListenAnyIP(port);
    }
});

// 3. Adiciona configurações sem o watcher de arquivos (reloadOnChange = false)
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
    .AddEnvironmentVariables();

// 4. Registrar Serviços
builder.Services.AddRouting();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

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

// Carrega o Seed de Dados
DbSeeder.CarregarDadosIniciais(app.Services);

app.Run();