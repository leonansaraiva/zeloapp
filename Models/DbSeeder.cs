using Microsoft.EntityFrameworkCore;
using ZeloApp.Models;
using ZeloApp.Services;

namespace ZeloApp.Models
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(AppDbContext db)
        {
            await db.Database.EnsureCreatedAsync();
            if (await db.Escolas.AnyAsync()) return;

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
                MensalidadeAtualPaga = false
            };

            db.Escolas.AddRange(escola1, escola2);
            await db.SaveChangesAsync();

            string[] nomesAlunos = { "Lucas", "Julia", "Enzo", "Valentina", "Matheus", "Sophia", "Gabriel", "Helena", "Davi", "Alice" };
            string[] sobrenomes = { "Santos", "Oliveira", "Souza", "Rodrigues", "Ferreira", "Alves" };
            string[] fotosAlunos = {
                "https://images.unsplash.com/photo-1519238263530-99bdd11df2ea?w=150&auto=format&fit=crop&q=80",
                "https://images.unsplash.com/photo-1502086223501-7ea6ecd79368?w=150&auto=format&fit=crop&q=80",
                "https://images.unsplash.com/photo-1540479859555-170e78326214?w=150&auto=format&fit=crop&q=80"
            };

            Random rand = new Random();

            var turma = new Turma { Nome = "Maternal I", Turno = "Integral", ProfessorResponsavel = "Profª Juliana", EscolaId = escola1.Id };
            db.Turmas.Add(turma);
            await db.SaveChangesAsync();

            for (int i = 1; i <= 10; i++)
            {
                string sobrenome = sobrenomes[rand.Next(sobrenomes.Length)];
                string nomeAluno = $"{nomesAlunos[rand.Next(nomesAlunos.Length)]} {sobrenome}";
                string nomeResp = $"Responsável de {nomeAluno.Split(' ')[0]}";

                var aluno = new Aluno
                {
                    Nome = nomeAluno,
                    FotoUrl = fotosAlunos[rand.Next(fotosAlunos.Length)],
                    Endereco = $"Rua das Flores, {rand.Next(10, 500)} - Curitiba/PR",
                    NomeResponsavel = nomeResp,
                    TelefoneResponsavel = $"(41) 9{rand.Next(1000, 9999)}-{rand.Next(1000, 9999)}",
                    ConvenioPrefeitura = i % 3 == 0,
                    TurnoAluno = "Integral",
                    LoginPortal = $"pai_{rand.Next(10000, 99999)}",
                    SenhaPortal = "123456",
                    ValorMensalidade = 450.00m,
                    MensalidadeMesPaga = i % 2 == 0,
                    TurmaId = turma.Id
                };
                db.Alunos.Add(aluno);
                await db.SaveChangesAsync();

                // Responsável Principal
                var respPrincipal = new Responsavel
                {
                    Nome = nomeResp,
                    Telefone = aluno.TelefoneResponsavel,
                    Endereco = aluno.Endereco,
                    Parentesco = "Pai / Mãe",
                    Principal = true,
                    PodeRetirar = true,
                    AlunoId = aluno.Id,
                    FotoUrl = "https://images.unsplash.com/photo-1544005313-94ddf0286df2?w=150&auto=format&fit=crop&q=80"
                };
                db.Responsaveis.Add(respPrincipal);

                // Autorizado Extra (Ex: Tio/Avô com opção temporária)
                var autorizadoExtra = new Responsavel
                {
                    Nome = $"Tio Roberto ({nomeAluno.Split(' ')[0]})",
                    Telefone = "(41) 98765-4321",
                    Endereco = "Rua do Sol, 123",
                    Parentesco = "Tio / Tia",
                    Principal = false,
                    PodeRetirar = true,
                    Temporario = i % 2 == 1,
                    DataInicio = DateTime.Today,
                    DataFim = DateTime.Today.AddDays(15),
                    AlunoId = aluno.Id,
                    FotoUrl = "https://images.unsplash.com/photo-1507003211169-0a1dd7228f2d?w=150&auto=format&fit=crop&q=80"
                };
                db.Responsaveis.Add(autorizadoExtra);
                await db.SaveChangesAsync();
            }
        }
    }
}