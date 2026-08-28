using Microsoft.EntityFrameworkCore;
using ZeloApp.Models;
using ZeloApp.Services;

namespace ZeloApp.Models
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(AppDbContext db)
        {
            // Garante que o banco está criado
            await db.Database.EnsureCreatedAsync();

            // Se já houver escolas, não duplica o seed
            if (await db.Escolas.AnyAsync()) return;

            // ==========================================
            // ESCOLA 1: Creche Sementinha do Saber
            // ==========================================
            var escola1 = new Escola
            {
                Nome = "Creche Sementinha do Saber",
                Cnpj = "12.345.678/0001-11",
                NomeGestor = "Maria da Silva",
                TelefoneGestor = "(41) 99999-1111",
                EmailGestor = "contato@sementinha.com",
                LoginAdmin = "admin1",
                SenhaAdmin = "123456",
                LoginPortal = "portal_sementinha",
                SenhaPortal = "123456",
                ValorMensalidadePlataforma = 450.00m,
                MensalidadeAtualPaga = true
            };

            // ==========================================
            // ESCOLA 2: Centro Educacional Pequeno Passo
            // ==========================================
            var escola2 = new Escola
            {
                Nome = "Centro Educacional Pequeno Passo",
                Cnpj = "98.765.432/0001-99",
                NomeGestor = "Ana Paula Souza",
                TelefoneGestor = "(41) 98888-2222",
                EmailGestor = "direcao@pequenopasso.com",
                LoginAdmin = "admin2",
                SenhaAdmin = "123456",
                LoginPortal = "portal_passo",
                SenhaPortal = "123456",
                ValorMensalidadePlataforma = 600.00m,
                MensalidadeAtualPaga = false // Exemplo de escola inadimplente com o SaaS
            };

            db.Escolas.AddRange(escola1, escola2);
            await db.SaveChangesAsync();

            // Nomes de exemplo para gerar alunos variados
            string[] nomesAlunos = { "Lucas", "Julia", "Enzo", "Valentina", "Matheus", "Sophia", "Gabriel", "Helena", "Davi", "Alice", "Miguel", "Laura", "Arthur", "Manuela", "Bernardo", "Isadora", "Heitor", "Lívia", "Theo", "Antonella" };
            string[] sobrenomes = { "Santos", "Oliveira", "Souza", "Rodrigues", "Ferreira", "Alves", "Pereira", "Lima", "Gomes", "Costa", "Martins", "Araújo", "Barbosa", "Cardoso", "Dias" };

            Random rand = new Random();

            // Criando Turmas e Alunos para a ESCOLA 1
            string[] nomesTurmas = { "Berçário I", "Maternal I", "Infantil I", "Infantil II" };
            
            foreach (var nomeTurma in nomesTurmas)
            {
                var turma = new Turma
                {
                    Nome = nomeTurma,
                    EscolaId = escola1.Id
                };
                db.Turmas.Add(turma);
                await db.SaveChangesAsync();

                // Adiciona cerca de 20 a 25 alunos por turma na Escola 1
                int qtdAlunos = rand.Next(20, 26);
                for (int i = 1; i <= qtdAlunos; i++)
                {
                    string nomeCompleto = $"{nomesAlunos[rand.Next(nomesAlunos.Length)]} {sobrenomes[rand.Next(sobrenomes.Length)]}";
                    string nomeResp = $"Responsável de {nomeCompleto.Split(' ')[0]}";
                    
                    var aluno = new Aluno
                    {
                        Nome = nomeCompleto,
                        NomeResponsavel = nomeResp,
                        TelefoneResponsavel = $"(41) 9{rand.Next(1000, 9999)}-{rand.Next(1000, 9999)}",
                        ConvenioPrefeitura = rand.Next(0, 10) > 7, // 30% chance de ser da prefeitura
                        LoginPortal = $"pai_{rand.Next(10000, 99999)}",
                        SenhaPortal = "123456",
                        ValorMensalidade = rand.Next(300, 600), // Mensalidade entre 300 e 600 reais
                        MensalidadeMesPaga = rand.Next(0, 10) > 2, // 80% adimplentes, 20% inadimplentes
                        TurmaId = turma.Id
                    };
                    db.Alunos.Add(aluno);
                }
                await db.SaveChangesAsync();
            }

            // Criando Turmas e Alunos para a ESCOLA 2 (Pequeno Passo)
            string[] nomesTurmas2 = { "Berçário II", "Maternal II", "Pré-Escola" };
            foreach (var nomeTurma in nomesTurmas2)
            {
                var turma = new Turma
                {
                    Nome = nomeTurma,
                    EscolaId = escola2.Id
                };
                db.Turmas.Add(turma);
                await db.SaveChangesAsync();

                int qtdAlunos = rand.Next(15, 25);
                for (int i = 1; i <= qtdAlunos; i++)
                {
                    string nomeCompleto = $"{nomesAlunos[rand.Next(nomesAlunos.Length)]} {sobrenomes[rand.Next(sobrenomes.Length)]}";
                    string nomeResp = $"Responsável de {nomeCompleto.Split(' ')[0]}";

                    var aluno = new Aluno
                    {
                        Nome = nomeCompleto,
                        NomeResponsavel = nomeResp,
                        TelefoneResponsavel = $"(41) 9{rand.Next(1000, 9999)}-{rand.Next(1000, 9999)}",
                        ConvenioPrefeitura = false,
                        LoginPortal = $"pai_{rand.Next(10000, 99999)}",
                        SenhaPortal = "123456",
                        ValorMensalidade = rand.Next(400, 750),
                        MensalidadeMesPaga = rand.Next(0, 10) > 3,
                        TurmaId = turma.Id
                    };
                    db.Alunos.Add(aluno);
                }
                await db.SaveChangesAsync();
            }
        }
    }
}