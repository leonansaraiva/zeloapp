using Microsoft.EntityFrameworkCore;

namespace ZeloApp.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Escola> Escolas { get; set; }
    public DbSet<Turma> Turmas { get; set; }
    public DbSet<Crianca> Criancas { get; set; }
    public DbSet<Responsavel> Responsaveis { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configurações de relacionamento em cascata para exclusão limpa
        modelBuilder.Entity<Turma>()
            .HasOne<Escola>()
            .WithMany()
            .HasForeignKey(t => t.EscolaId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Crianca>()
            .HasOne<Turma>()
            .WithMany()
            .HasForeignKey(c => c.TurmaId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Responsavel>()
            .HasOne<Crianca>()
            .WithMany()
            .HasForeignKey(r => r.CriancaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}