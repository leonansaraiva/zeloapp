using Microsoft.EntityFrameworkCore;
using ZeloApp;
using ZeloApp.Models;
using ZeloApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Adiciona os serviços do Razor Components / Blazor
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Configuração do Banco de Dados (SQLite)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=zelo.db"));

// Seu serviço de autenticação
builder.Services.AddScoped<AuthStateService>();

var app = builder.Build();

// Executa o Seeder do banco ao iniciar
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await DbSeeder.SeedAsync(db);
}

// Configuração de ambiente para produção / Render
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // NÃO use app.UseHsts() ou UseHttpsRedirection() de forma agressiva no Render 
    // se ele já faz o SSL Termination no proxy externo, ou ajuste assim:
}

app.UseStaticFiles();
app.UseAntiforgery();

// 6. Mapeamento dos componentes Blazor (Essencial para as páginas funcionarem e evitar 404)
app.MapRazorComponents<ZeloApp.Components.App>()
    .AddInteractiveServerRenderMode();

app.Run();