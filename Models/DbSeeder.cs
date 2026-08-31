using System;
using System.Linq;
using ZeloApp.Models;

namespace ZeloApp.Services
{
    public static class DbSeeder
    {
        public static void Seed(AppDbContext db)
        {
            // Garante que o banco foi criado
            db.Database.EnsureCreated();

            // Se já houver escolas, não precisa popular novamente
            if (db.Escolas.Any()) return;

            // 1. Criar Escola Exemplo
            var escola1 = new Escola
            {
                Nome = "Creche Recanto Infantil",
                Cnpj = "12.345.678/0001-99",
                Endereco = "Rua das Flores, 100 - Curitiba/PR",
                NomeGestor = "Mariana Souza",
                CpfGestor = "123.456.789-00",
                TelefoneGestor = "(41) 99999-1111",
                EmailGestor = "direcao@recantoinfantil.com",
                MesesContrato = 12,
                LoginAdmin = "admin_recanto",
                SenhaAdmin = "123456",
                ValorMensalidadePlataforma = 450.00m,
                MensalidadeAtualPaga = true
            };

            db.Escolas.Add(escola1);
            db.SaveChanges();

            // 2. Criar Turmas Exemplo
            var turmaMaternal = new Turma
            {
                EscolaId = escola1.Id,
                Nome = "Maternal I",
                Turno = "Integral"
            };

            var turmaJardim = new Turma
            {
                EscolaId = escola1.Id,
                Nome = "Jardim II",
                Turno = "Matutino"
            };

            db.Turmas.AddRange(turmaMaternal, turmaJardim);
            db.SaveChanges();

            // 3. Criar Alunos e Responsáveis Exemplo
            string[] nomesAlunos = { "Lucas Gabriel", "Sophia Vitória", "Enzo Gabriel", "Valentina Rosa", "Heitor Miguel", "Helena Beatriz" };
            string[] cpfsAlunos = { "111.222.333-44", "222.333.444-55", "333.444.555-66", "444.555.666-77", "555.666.777-88", "666.777.888-99" };

            for (int i = 0; i < nomesAlunos.Length; i++)
            {
                var turmaDestino = i % 2 == 0 ? turmaMaternal : turmaJardim;
                string cpfLimpo = cpfsAlunos[i].Replace(".", "").Replace("-", "");

                var aluno = new Aluno
                {
                    TurmaId = turmaDestino.Id,
                    NomeCompleto = nomesAlunos[i],
                    DataNascimento = DateTime.Today.AddYears(-3).AddMonths(-i),
                    DataMatricula = DateTime.Now.AddDays(-10 * i),
                    Vinculo = i == 0 ? "Prefeitura" : "Particular",
                    Turno = turmaDestino.Turno,
                    Mensalidade = 550.00m,
                    LoginPortal = cpfLimpo,
                    SenhaPortal = "15052020", // Data padrão limpa (ddMMyyyy)
                    Endereco = "Rua Central, 500 - Curitiba/PR"
                };

                db.Alunos.Add(aluno);
                db.SaveChanges();

                // Responsável Principal (Sem a propriedade Endereco que causava erro)
                var respPrincipal = new Responsavel
                {
                    EscolaId = escola1.Id,
                    NomeCompleto = $"Responsável de {nomesAlunos[i]}",
                    Telefone = "(41) 98888-2222",
                    Cpf = cpfsAlunos[i],
                    Parentesco = "Mãe / Pai",
                    Principal = true,
                    PodeRetirar = true,
                    AlunoId = aluno.Id
                };
                db.Responsaveis.Add(respPrincipal);

                // Autorizado Extra
                var autorizadoExtra = new Responsavel
                {
                    EscolaId = escola1.Id,
                    NomeCompleto = $"Tio(a) de {nomesAlunos[i].Split(' ')[0]}",
                    Telefone = "(41) 98765-4321",
                    Cpf = "999.888.777-66",
                    Parentesco = "Tio / Tia / Avô",
                    Principal = false,
                    PodeRetirar = true,
                    Temporario = i % 2 == 1,
                    DataInicio = DateTime.Today,
                    DataFim = DateTime.Today.AddDays(15),
                    AlunoId = aluno.Id
                };
                db.Responsaveis.Add(autorizadoExtra);
            }

            db.SaveChanges();
        }
    }
}