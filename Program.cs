using Microsoft.EntityFrameworkCore;
using ZeloApp;
using ZeloApp.Models;
using ZeloApp.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Configurando o DbContext para usar o banco em memória (perfeito para testes locais)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("ZeloDb"));

// 2. Adicionando serviços do Blazor Server
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// 3. Registrando serviços customizados da aplicação (ex: AuthStateService)
builder.Services.AddScoped<AuthStateService>();

var app = builder.Build();

// 4. Executando o Seeder para popular dados iniciais na primeira execução
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await DbSeeder.SeedAsync(db);
}

// 5. Configuração do pipeline HTTP e Middlewares
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

// 6. Mapeamento dos componentes Blazor (Essencial para as páginas funcionarem e evitar 404)
app.MapRazorComponents<ZeloApp.Components.App>()
    .AddInteractiveServerRenderMode();

app.Run();