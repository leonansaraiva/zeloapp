using Microsoft.EntityFrameworkCore;
using ZeloApp.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("ZeloDb"));

var app = builder.Build();

// Bloco global para capturar e mostrar qualquer erro de inicialização na tela e no console
app.UseDeveloperExceptionPage();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<ZeloApp.Components.App>()
    .AddInteractiveServerRenderMode();

// Tenta carregar o banco de dados de forma segura com Try-Catch visível
try
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
    
    // Se você tiver o Seeder, chama aqui dentro protegido
    DbSeeder.CarregarDadosIniciais(scope.ServiceProvider);
    Console.WriteLine("✅ Banco de dados e Seeder carregados com sucesso!");
}
catch (Exception ex)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"\n❌ ERRO CRÍTICO NA INICIALIZAÇÃO DO BANCO: {ex.Message}\n{ex.StackTrace}\n");
    Console.ResetColor();
}

app.Run();