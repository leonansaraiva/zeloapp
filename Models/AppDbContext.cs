using Microsoft.EntityFrameworkCore;
using ZeloApp.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Escola> Escolas { get; set; }
    public DbSet<Turma> Turmas { get; set; }
    public DbSet<Aluno> Alunos { get; set; }
    public DbSet<Responsavel> Responsaveis { get; set; }
    public DbSet<Despesa> Despesas { get; set; } // <--- ADICIONE ESTA LINHA

public DbSet<Professor> Professores { get; set; }
public DbSet<ProfessorTurma> ProfessorTurmas { get; set; }
public DbSet<HistoricoMovimentacao> HistoricoMovimentacoes { get; set; }
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<Responsavel>()
        .HasOne(r => r.Aluno)
        .WithMany(a => a.Responsaveis)
        .HasForeignKey(r => r.AlunoId)
        .OnDelete(DeleteBehavior.Cascade);
}
}