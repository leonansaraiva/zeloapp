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
            // ESCOLAS
            var escola1 = new Escola { Nome = "Creche e Escola Pequeno Anjo", Cnpj = "12.345.678/0001-95", NomeGestor = "Ana Maria Silva", EmailGestor = "anamaria@pequenoanjo.com.br", Telefone = "(41) 98888-1122", Endereco = "Rua das Flores, 120", Ativo = true, DataCadastro = DateTime.UtcNow.AddDays(-15) };
            var escola2 = new Escola { Nome = "Colégio Saber & Futuro", Cnpj = "99.888.777/0001-22", NomeGestor = "Carlos Eduardo Santos", EmailGestor = "carlos@saberefuturo.com.br", Telefone = "(41) 99977-3344", Endereco = "Av. Brasil, 450", Ativo = true, DataCadastro = DateTime.UtcNow.AddDays(-10) };
            var escola3 = new Escola { Nome = "Centro Educacional Infantia", Cnpj = "11.222.333/0001-81", NomeGestor = "Mariana Oliveira", EmailGestor = "mariana@infantia.com.br", Telefone = "(41) 98765-4321", Endereco = "Rua XV de Novembro, 890", Ativo = true, DataCadastro = DateTime.UtcNow.AddDays(-5) };

            db.Escolas.AddRange(escola1, escola2, escola3);
            db.SaveChanges();

            // EQUIPE DE GESTÃO
            db.Gestores.AddRange(
                new GestorEscola { EscolaId = escola1.Id, Nome = "Ana Maria Silva", Email = "anamaria@pequenoanjo.com.br", Cargo = "Diretora Geral", Telefone = "(41) 98888-1122", FotoUrl = "https://images.unsplash.com/photo-1573496359142-b8d87734a5a2?w=150&auto=format&fit=crop&q=80" },
                new GestorEscola { EscolaId = escola2.Id, Nome = "Carlos Eduardo Santos", Email = "carlos@saberefuturo.com.br", Cargo = "Diretor", Telefone = "(41) 99977-3344", FotoUrl = "https://images.unsplash.com/photo-1507003211169-0a1dd7228f2d?w=150&auto=format&fit=crop&q=80" },
                new GestorEscola { EscolaId = escola3.Id, Nome = "Mariana Oliveira", Email = "mariana@infantia.com.br", Cargo = "Diretora Geral", Telefone = "(41) 98765-4321", FotoUrl = "https://images.unsplash.com/photo-1580489944761-15a19d654956?w=150&auto=format&fit=crop&q=80" }
            );

            // TURMAS
            var e1_bercario = new Turma { EscolaId = escola1.Id, Nome = "Berçário I", Turno = "Integral", EducadoraResponsavel = "Profª Claudia Ramos" };
            var e2_infantil = new Turma { EscolaId = escola2.Id, Nome = "Infantil I", Turno = "Tarde", EducadoraResponsavel = "Profª Beatriz Lima" };
            var e3_bercario = new Turma { EscolaId = escola3.Id, Nome = "Berçário II", Turno = "Integral", EducadoraResponsavel = "Profª Amanda Souza" };

            db.Turmas.AddRange(e1_bercario, e2_infantil, e3_bercario);
            db.SaveChanges();

            // CRIANÇAS
            var lucas = new Crianca { TurmaId = e1_bercario.Id, Nome = "Lucas Gabriel Silva", DataNascimento = new DateTime(2024, 03, 15), FotoUrl = "https://images.unsplash.com/photo-1519689680058-324335c77eba?w=150&auto=format&fit=crop&q=80" };
            var miguel = new Crianca { TurmaId = e2_infantil.Id, Nome = "Miguel Alves", DataNascimento = new DateTime(2022, 04, 18), FotoUrl = "https://images.unsplash.com/photo-1519689680058-324335c77eba?w=150&auto=format&fit=crop&q=80" };
            var maite = new Crianca { TurmaId = e3_bercario.Id, Nome = "Maitê Oliveira", DataNascimento = new DateTime(2024, 02, 10), FotoUrl = "https://images.unsplash.com/photo-1543332164-6e82f355badc?w=150&auto=format&fit=crop&q=80" };

            db.Criancas.AddRange(lucas, miguel, maite);
            db.SaveChanges();

            // RESPONSÁVEIS (Permanentes e Temporários)
            db.Responsaveis.AddRange(
                // Lucas
                new Responsavel { CriancaId = lucas.Id, Nome = "Fernanda Silva", Cpf = "111.222.333-00", Parentesco = "Mãe", Telefone = "(41) 99111-2233", Principal = true, PodeRetirar = true, EhTemporario = false, FotoUrl = "https://images.unsplash.com/photo-1534528741775-53994a69daeb?w=150&auto=format&fit=crop&q=80" },
                new Responsavel { CriancaId = lucas.Id, Nome = "Dona Helena Silva", Cpf = "333.444.555-22", Parentesco = "Avó", Telefone = "(41) 98822-3344", Principal = false, PodeRetirar = true, EhTemporario = true, DataInicioRetirada = DateTime.Today, DataFimRetirada = DateTime.Today.AddDays(7), FotoUrl = "https://images.unsplash.com/photo-1544005313-94ddf0286df2?w=150&auto=format&fit=crop&q=80" },

                // Miguel
                new Responsavel { CriancaId = miguel.Id, Nome = "Juliana Alves", Cpf = "234.345.456-12", Parentesco = "Mãe", Telefone = "(41) 99666-1234", Principal = true, PodeRetirar = true, EhTemporario = false, FotoUrl = "https://images.unsplash.com/photo-1573497019940-1c28c88b4f3e?w=150&auto=format&fit=crop&q=80" },

                // Maitê (Infantia)
                new Responsavel { CriancaId = maite.Id, Nome = "Camila Oliveira", Cpf = "345.456.567-23", Parentesco = "Mãe", Telefone = "(41) 99777-4321", Principal = true, PodeRetirar = true, EhTemporario = false, FotoUrl = "https://images.unsplash.com/photo-1580489944761-15a19d654956?w=150&auto=format&fit=crop&q=80" },
                new Responsavel { CriancaId = maite.Id, Nome = "Gabriel Tio", Cpf = "999.888.777-11", Parentesco = "Tio", Telefone = "(41) 99888-1122", Principal = false, PodeRetirar = true, EhTemporario = true, DataInicioRetirada = DateTime.Today, DataFimRetirada = DateTime.Today.AddDays(3), FotoUrl = "https://images.unsplash.com/photo-1500648767791-00dcc994a43e?w=150&auto=format&fit=crop&q=80" }
            );

            db.SaveChanges();
        }
    }
}