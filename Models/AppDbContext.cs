using Microsoft.EntityFrameworkCore;
using ZeloApp.Models;

namespace ZeloApp.Services
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Escola> Escolas { get; set; }
        public DbSet<Turma> Turmas { get; set; }
        public DbSet<Aluno> Alunos { get; set; }
        
        // Adicionado para suportar a consulta do Login.razor
        public DbSet<Responsavel> Responsaveis { get; set; }
    }
}