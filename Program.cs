using Microsoft.EntityFrameworkCore;
using ZeloApp;
using ZeloApp.Models;
using ZeloApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Adiciona os serviços do Razor Components / Blazor
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Configuração do Banco de Dados (SQLite) com Caminho Absoluto na Raiz
var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "zelo.db");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));

// Serviços do sistema
builder.Services.AddScoped<AuthStateService>();
builder.Services.AddScoped<DespesaService>();

var app = builder.Build();

// Executa o Seeder do banco ao iniciar
// Executa as Migrations pendentes automaticamente ao iniciar
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated(); // Cria o banco local sem exigir migrações travadas
    DbSeeder.Seed(db);
}

// Configuração de ambiente para produção / Render
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

// Mapeamento dos componentes Blazor
app.MapRazorComponents<ZeloApp.Components.App>()
    .AddInteractiveServerRenderMode();

app.Run();