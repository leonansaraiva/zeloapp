using Microsoft.EntityFrameworkCore;

namespace ZeloApp.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Escola> Escolas => Set<Escola>();
    public DbSet<GestorEscola> Gestores => Set<GestorEscola>();
    public DbSet<Turma> Turmas => Set<Turma>();
    public DbSet<Crianca> Criancas => Set<Crianca>();
    public DbSet<Responsavel> Responsaveis => Set<Responsavel>();
}