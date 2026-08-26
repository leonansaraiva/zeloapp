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
            // =========================================================================
            // ESCOLA 1: Creche e Escola Pequeno Anjo
            // =========================================================================
            var escola1 = new Escola
            {
                Nome = "Creche e Escola Pequeno Anjo",
                Cnpj = "12.345.678/0001-95",
                NomeGestor = "Ana Maria Silva",
                EmailGestor = "anamaria@pequenoanjo.com.br",
                Telefone = "(41) 98888-1122",
                Endereco = "Rua das Flores, 120 - Centro",
                Ativo = true,
                DataCadastro = DateTime.UtcNow.AddDays(-15)
            };

            // ESCOLA 2: Colégio Saber & Futuro
            var escola2 = new Escola
            {
                Nome = "Colégio Saber & Futuro",
                Cnpj = "99.888.777/0001-22",
                NomeGestor = "Carlos Eduardo Santos",
                EmailGestor = "carlos@saberefuturo.com.br",
                Telefone = "(41) 99977-3344",
                Endereco = "Av. Brasil, 450 - Batel",
                Ativo = true,
                DataCadastro = DateTime.UtcNow.AddDays(-10)
            };

            // ESCOLA 3: Centro Educacional Infantia
            var escola3 = new Escola
            {
                Nome = "Centro Educacional Infantia",
                Cnpj = "11.222.333/0001-81",
                NomeGestor = "Mariana Oliveira",
                EmailGestor = "mariana@infantia.com.br",
                Telefone = "(41) 98765-4321",
                Endereco = "Rua XV de Novembro, 890 - Agua Verde",
                Ativo = true,
                DataCadastro = DateTime.UtcNow.AddDays(-5)
            };

            db.Escolas.AddRange(escola1, escola2, escola3);
            db.SaveChanges();

            // =========================================================================
            // EQUIPES DE GESTÃO E PROFESSORES
            // =========================================================================
            db.Gestores.AddRange(
                // Equipe Escola 1 (Diretora + Coordenadora + Professora)
                new GestorEscola { EscolaId = escola1.Id, Nome = "Ana Maria Silva", Email = "anamaria@pequenoanjo.com.br", Cargo = "Diretora Geral", Telefone = "(41) 98888-1122", FotoUrl = "https://images.unsplash.com/photo-1573496359142-b8d87734a5a2?w=150&auto=format&fit=crop&q=80" },
                new GestorEscola { EscolaId = escola1.Id, Nome = "Luciana Martins", Email = "luciana@pequenoanjo.com.br", Cargo = "Coordenadora Pedagógica", Telefone = "(41) 97777-3344", FotoUrl = "https://images.unsplash.com/photo-1580489944761-15a19d654956?w=150&auto=format&fit=crop&q=80" },
                new GestorEscola { EscolaId = escola1.Id, Nome = "Profª Claudia Ramos", Email = "claudia@pequenoanjo.com.br", Cargo = "Professora / Educadora", Telefone = "(41) 96666-5544", FotoUrl = "https://images.unsplash.com/photo-1544005313-94ddf0286df2?w=150&auto=format&fit=crop&q=80" },

                // Equipe Escola 2 (Diretor + 2 Professoras)
                new GestorEscola { EscolaId = escola2.Id, Nome = "Carlos Eduardo Santos", Email = "carlos@saberefuturo.com.br", Cargo = "Diretor", Telefone = "(41) 99977-3344", FotoUrl = "https://images.unsplash.com/photo-1507003211169-0a1dd7228f2d?w=150&auto=format&fit=crop&q=80" },
                new GestorEscola { EscolaId = escola2.Id, Nome = "Profª Beatriz Lima", Email = "beatriz@saberefuturo.com.br", Cargo = "Professora / Educadora", Telefone = "(41) 98811-2233", FotoUrl = "https://images.unsplash.com/photo-1534528741775-53994a69daeb?w=150&auto=format&fit=crop&q=80" },
                new GestorEscola { EscolaId = escola2.Id, Nome = "Profª Fernanda Dias", Email = "fernanda@saberefuturo.com.br", Cargo = "Professora / Educadora", Telefone = "(41) 98822-4455", FotoUrl = "https://images.unsplash.com/photo-1573497019940-1c28c88b4f3e?w=150&auto=format&fit=crop&q=80" },

                // Equipe Escola 3 (Diretora + Coordenadora)
                new GestorEscola { EscolaId = escola3.Id, Nome = "Mariana Oliveira", Email = "mariana@infantia.com.br", Cargo = "Diretora Geral", Telefone = "(41) 98765-4321", FotoUrl = "https://images.unsplash.com/photo-1573496359142-b8d87734a5a2?w=150&auto=format&fit=crop&q=80" },
                new GestorEscola { EscolaId = escola3.Id, Nome = "Profª Amanda Souza", Email = "amanda@infantia.com.br", Cargo = "Coordenadora Pedagógica", Telefone = "(41) 99111-4455", FotoUrl = "https://images.unsplash.com/photo-1580489944761-15a19d654956?w=150&auto=format&fit=crop&q=80" }
            );

            // =========================================================================
            // TURMAS
            // =========================================================================
            var e1_bercario = new Turma { EscolaId = escola1.Id, Nome = "Berçário I", Turno = "Integral", EducadoraResponsavel = "Profª Claudia Ramos" };
            var e1_infantil1 = new Turma { EscolaId = escola1.Id, Nome = "Infantil I", Turno = "Manhã", EducadoraResponsavel = "Profª Luciana Martins" };

            var e2_infantil1 = new Turma { EscolaId = escola2.Id, Nome = "Infantil I", Turno = "Tarde", EducadoraResponsavel = "Profª Beatriz Lima" };
            var e2_infantil2 = new Turma { EscolaId = escola2.Id, Nome = "Infantil II", Turno = "Integral", EducadoraResponsavel = "Profª Fernanda Dias" };

            var e3_bercario = new Turma { EscolaId = escola3.Id, Nome = "Berçário II", Turno = "Integral", EducadoraResponsavel = "Profª Amanda Souza" };
            var e3_infantil2 = new Turma { EscolaId = escola3.Id, Nome = "Infantil II", Turno = "Tarde", EducadoraResponsavel = "Profª Amanda Souza" };

            db.Turmas.AddRange(e1_bercario, e1_infantil1, e2_infantil1, e2_infantil2, e3_bercario, e3_infantil2);
            db.SaveChanges();

            // =========================================================================
            // CRIANÇAS
            // =========================================================================
            // Escola 1 - Berçário I
            var lucas = new Crianca { TurmaId = e1_bercario.Id, Nome = "Lucas Gabriel Silva", DataNascimento = new DateTime(2024, 03, 15), FotoUrl = "https://images.unsplash.com/photo-1519689680058-324335c77eba?w=150&auto=format&fit=crop&q=80", EmailResponsavelConvidado = "fernanda@gmail.com" };
            var alice = new Crianca { TurmaId = e1_bercario.Id, Nome = "Alice Souza", DataNascimento = new DateTime(2024, 05, 10), FotoUrl = "https://images.unsplash.com/photo-1543332164-6e82f355badc?w=150&auto=format&fit=crop&q=80", EmailResponsavelConvidado = "roberto@gmail.com" };
            var enzo = new Crianca { TurmaId = e1_bercario.Id, Nome = "Enzo Gabriel Lima", DataNascimento = new DateTime(2024, 01, 20), FotoUrl = "https://images.unsplash.com/photo-1519689680058-324335c77eba?w=150&auto=format&fit=crop&q=80", EmailResponsavelConvidado = "mariana@gmail.com" };

            // Escola 1 - Infantil I
            var sofia = new Crianca { TurmaId = e1_infantil1.Id, Nome = "Sofia Martins", DataNascimento = new DateTime(2022, 08, 12), FotoUrl = "https://images.unsplash.com/photo-1543332164-6e82f355badc?w=150&auto=format&fit=crop&q=80", EmailResponsavelConvidado = "patricia@gmail.com" };
            var theo = new Crianca { TurmaId = e1_infantil1.Id, Nome = "Theo Rocha", DataNascimento = new DateTime(2022, 11, 05), FotoUrl = "https://images.unsplash.com/photo-1519689680058-324335c77eba?w=150&auto=format&fit=crop&q=80", EmailResponsavelConvidado = "andre@gmail.com" };

            // Escola 2 - Infantil I e II
            var miguel = new Crianca { TurmaId = e2_infantil1.Id, Nome = "Miguel Alves", DataNascimento = new DateTime(2022, 04, 18), FotoUrl = "https://images.unsplash.com/photo-1519689680058-324335c77eba?w=150&auto=format&fit=crop&q=80", EmailResponsavelConvidado = "juliana@gmail.com" };
            var helena = new Crianca { TurmaId = e2_infantil2.Id, Nome = "Helena Castro", DataNascimento = new DateTime(2021, 09, 30), FotoUrl = "https://images.unsplash.com/photo-1543332164-6e82f355badc?w=150&auto=format&fit=crop&q=80", EmailResponsavelConvidado = "camila@gmail.com" };

            db.Criancas.AddRange(lucas, alice, enzo, sofia, theo, miguel, helena);
            db.SaveChanges();

            // =========================================================================
            // RESPONSÁVEIS (Múltiplos perfis e combinações)
            // =========================================================================
            db.Responsaveis.AddRange(
                // --- Lucas Gabriel (Mãe, Pai e Avó Temporária) ---
                new Responsavel { CriancaId = lucas.Id, Nome = "Fernanda Silva", Cpf = "111.222.333-00", Parentesco = "Mãe", Telefone = "(41) 99111-2233", Principal = true, PodeRetirar = true, EhTemporario = false, FotoUrl = "https://images.unsplash.com/photo-1534528741775-53994a69daeb?w=150&auto=format&fit=crop&q=80" },
                new Responsavel { CriancaId = lucas.Id, Nome = "Ricardo Silva", Cpf = "222.333.444-11", Parentesco = "Pai", Telefone = "(41) 99111-8899", Principal = false, PodeRetirar = true, EhTemporario = false, FotoUrl = "https://images.unsplash.com/photo-1507003211169-0a1dd7228f2d?w=150&auto=format&fit=crop&q=80" },
                new Responsavel { CriancaId = lucas.Id, Nome = "Dona Helena Silva", Cpf = "333.444.555-22", Parentesco = "Avó/Avô", Telefone = "(41) 98822-3344", Principal = false, PodeRetirar = true, EhTemporario = true, DataInicioRetirada = DateTime.Today, DataFimRetirada = DateTime.Today.AddDays(7), FotoUrl = "https://images.unsplash.com/photo-1544005313-94ddf0286df2?w=150&auto=format&fit=crop&q=80" },

                // --- Alice (Apenas Pai) ---
                new Responsavel { CriancaId = alice.Id, Nome = "Roberto Souza", Cpf = "444.555.666-33", Parentesco = "Pai", Telefone = "(41) 99222-4455", Principal = true, PodeRetirar = true, EhTemporario = false, FotoUrl = "https://images.unsplash.com/photo-1500648767791-00dcc994a43e?w=150&auto=format&fit=crop&q=80" },

                // --- Enzo (Mãe Solo) ---
                new Responsavel { CriancaId = enzo.Id, Nome = "Mariana Lima", Cpf = "555.666.777-44", Parentesco = "Mãe", Telefone = "(41) 99333-6677", Principal = true, PodeRetirar = true, EhTemporario = false, FotoUrl = "https://images.unsplash.com/photo-1573496359142-b8d87734a5a2?w=150&auto=format&fit=crop&q=80" },

                // --- Sofia (Mãe, Pai, Tio Temporário) ---
                new Responsavel { CriancaId = sofia.Id, Nome = "Patricia Martins", Cpf = "666.777.888-55", Parentesco = "Mãe", Telefone = "(41) 99444-1122", Principal = true, PodeRetirar = true, EhTemporario = false, FotoUrl = "https://images.unsplash.com/photo-1580489944761-15a19d654956?w=150&auto=format&fit=crop&q=80" },
                new Responsavel { CriancaId = sofia.Id, Nome = "Lucas Martins", Cpf = "777.888.999-66", Parentesco = "Pai", Telefone = "(41) 99444-3344", Principal = false, PodeRetirar = true, EhTemporario = false, FotoUrl = "https://images.unsplash.com/photo-1472099645785-5658abf4ff4e?w=150&auto=format&fit=crop&q=80" },
                new Responsavel { CriancaId = sofia.Id, Nome = "Gabriel Martins (Tio)", Cpf = "888.999.000-77", Parentesco = "Tio/Tia", Telefone = "(41) 99444-5566", Principal = false, PodeRetirar = true, EhTemporario = true, DataInicioRetirada = DateTime.Today, DataFimRetirada = DateTime.Today.AddDays(3), FotoUrl = "https://images.unsplash.com/photo-1519085360753-af0119f7cbe7?w=150&auto=format&fit=crop&q=80" },

                // --- Theo (Mãe e Pai) ---
                new Responsavel { CriancaId = theo.Id, Nome = "André Rocha", Cpf = "999.000.111-88", Parentesco = "Pai", Telefone = "(41) 99555-8899", Principal = true, PodeRetirar = true, EhTemporario = false, FotoUrl = "https://images.unsplash.com/photo-1507003211169-0a1dd7228f2d?w=150&auto=format&fit=crop&q=80" },
                new Responsavel { CriancaId = theo.Id, Nome = "Vanessa Rocha", Cpf = "000.111.222-99", Parentesco = "Mãe", Telefone = "(41) 99555-7766", Principal = false, PodeRetirar = true, EhTemporario = false, FotoUrl = "https://images.unsplash.com/photo-1534528741775-53994a69daeb?w=150&auto=format&fit=crop&q=80" },

                // --- Miguel (Mãe e Padrinho) ---
                new Responsavel { CriancaId = miguel.Id, Nome = "Juliana Alves", Cpf = "123.234.345-01", Parentesco = "Mãe", Telefone = "(41) 99666-1234", Principal = true, PodeRetirar = true, EhTemporario = false, FotoUrl = "https://images.unsplash.com/photo-1573497019940-1c28c88b4f3e?w=150&auto=format&fit=crop&q=80" },
                new Responsavel { CriancaId = miguel.Id, Nome = "Marcelo Santos (Padrinho)", Cpf = "234.345.456-12", Parentesco = "Padrinho/Madrinha", Telefone = "(41) 99666-5678", Principal = false, PodeRetirar = true, EhTemporario = false, FotoUrl = "https://images.unsplash.com/photo-1500648767791-00dcc994a43e?w=150&auto=format&fit=crop&q=80" },

                // --- Helena (Mãe e Pai) ---
                new Responsavel { CriancaId = helena.Id, Nome = "Camila Castro", Cpf = "345.456.567-23", Parentesco = "Mãe", Telefone = "(41) 99777-4321", Principal = true, PodeRetirar = true, EhTemporario = false, FotoUrl = "https://images.unsplash.com/photo-1580489944761-15a19d654956?w=150&auto=format&fit=crop&q=80" },
                new Responsavel { CriancaId = helena.Id, Nome = "Bruno Castro", Cpf = "456.567.678-34", Parentesco = "Pai", Telefone = "(41) 99777-8765", Principal = false, PodeRetirar = true, EhTemporario = false, FotoUrl = "https://images.unsplash.com/photo-1472099645785-5658abf4ff4e?w=150&auto=format&fit=crop&q=80" }
            );

            db.SaveChanges();
        }
    }
}