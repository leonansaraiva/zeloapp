using Microsoft.EntityFrameworkCore;

namespace ZeloApp.Models;

public static class DbSeeder
{
    public static void CarregarDadosIniciais(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (!db.Escolas.Any())
        {
            var escola1 = new Escola
            {
                Nome = "Creche e Escola Pequeno Anjo",
                Cnpj = "12.345.678/0001-95",
                NomeGestor = "Ana Maria Silva",
                EmailGestor = "anamaria@pequenoanjo.com.br",
                Telefone = "(41) 98888-1122",
                Endereco = "Rua das Flores, 120",
                Ativo = true,
                DataCadastro = DateTime.UtcNow.AddDays(-10)
            };

            db.Escolas.Add(escola1);
            db.SaveChanges();

            // Equipe de Gestão
            db.Gestores.AddRange(
                new GestorEscola { 
                    EscolaId = escola1.Id, 
                    Nome = "Ana Maria Silva", 
                    Email = "anamaria@pequenoanjo.com.br", 
                    Cargo = "Diretora Geral", 
                    Telefone = "(41) 98888-1122",
                    FotoUrl = "https://images.unsplash.com/photo-1573496359142-b8d87734a5a2?w=150&auto=format&fit=crop&q=80"
                },
                new GestorEscola { 
                    EscolaId = escola1.Id, 
                    Nome = "Profª Claudia Ramos", 
                    Email = "claudia@pequenoanjo.com.br", 
                    Cargo = "Educadora / Berçário", 
                    Telefone = "(41) 97777-3344",
                    FotoUrl = "https://images.unsplash.com/photo-1580489944761-15a19d654956?w=150&auto=format&fit=crop&q=80"
                }
            );

            // Turmas
            var turmaBercario = new Turma { EscolaId = escola1.Id, Nome = "Berçário I", Turno = "Integral", EducadoraResponsavel = "Profª Claudia Ramos" };
            db.Turmas.Add(turmaBercario);
            db.SaveChanges();

            // Criança com foto do Unsplash corrigida
            var lucas = new Crianca { 
                TurmaId = turmaBercario.Id, 
                Nome = "Lucas Gabriel Silva", 
                DataNascimento = new DateTime(2024, 03, 15),
                FotoUrl = "https://images.unsplash.com/photo-1519689680058-324335c77eba?w=150&auto=format&fit=crop&q=80",
                EmailResponsavelConvidado = "fernanda@gmail.com"
            };

            db.Criancas.Add(lucas);
            db.SaveChanges();

            // Responsáveis
            db.Responsaveis.AddRange(
                new Responsavel { 
                    CriancaId = lucas.Id, 
                    Nome = "Fernanda Silva", 
                    Cpf = "111.222.333-00", 
                    Parentesco = "Mãe", 
                    Telefone = "(41) 99111-2233", 
                    Principal = true, 
                    PodeRetirar = true,
                    EhTemporario = false,
                    FotoUrl = "https://images.unsplash.com/photo-1534528741775-53994a69daeb?w=150&auto=format&fit=crop&q=80"
                },
                new Responsavel { 
                    CriancaId = lucas.Id, 
                    Nome = "Ricardo Silva", 
                    Cpf = "222.333.444-11", 
                    Parentesco = "Pai", 
                    Telefone = "(41) 99111-8899", 
                    Principal = false, 
                    PodeRetirar = true,
                    EhTemporario = false,
                    FotoUrl = "https://images.unsplash.com/photo-1507003211169-0a1dd7228f2d?w=150&auto=format&fit=crop&q=80"
                },
                new Responsavel { 
                    CriancaId = lucas.Id, 
                    Nome = "Dona Helena Silva", 
                    Cpf = "333.444.555-22", 
                    Parentesco = "Avó", 
                    Telefone = "(41) 98822-3344", 
                    Principal = false, 
                    PodeRetirar = true,
                    EhTemporario = true,
                    DataInicioRetirada = DateTime.Today,
                    DataFimRetirada = DateTime.Today.AddDays(7),
                    FotoUrl = "https://images.unsplash.com/photo-1544005313-94ddf0286df2?w=150&auto=format&fit=crop&q=80"
                }
            );

            db.SaveChanges();
        }
    }
}