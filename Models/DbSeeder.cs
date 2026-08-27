using Microsoft.Extensions.DependencyInjection;

namespace ZeloApp.Models;

public static class DbSeeder
{
    public static void CarregarDadosIniciais(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        context.Database.EnsureCreated();

        if (context.Escolas.Any()) return;

        // Escola 1
        var escola1 = new Escola
        {
            Nome = "CEI Pequeno Príncipe",
            Cnpj = "12.345.678/0001-99",
            Endereco = "Rua das Flores, 100 - Centro",
            Telefone = "(41) 3333-4444",
            NomeGestor = "Diretora Ana Maria",
            EmailGestor = "ana@pequenoprincipe.com",
            LoginAdmin = "cei_admin_1",
            SenhaAdmin = "123456",
            Ativo = true
        };

        // Escola 2
        var escola2 = new Escola
        {
            Nome = "Centro Educacional Zelo Kids",
            Cnpj = "98.765.432/0001-11",
            Endereco = "Av. Brasil, 500 - Sul",
            Telefone = "(41) 3333-5555",
            NomeGestor = "Diretor Carlos Eduardo",
            EmailGestor = "carlos@zelokids.com",
            LoginAdmin = "cei_admin_2",
            SenhaAdmin = "123456",
            Ativo = true
        };

        context.Escolas.AddRange(escola1, escola2);
        context.SaveChanges();

        // Turmas da Escola 1
        var t1 = new Turma { EscolaId = escola1.Id, Nome = "Berçário I", Turno = "Integral", EducadoraResponsavel = "Prof.ª Beatriz" };
        var t2 = new Turma { EscolaId = escola1.Id, Nome = "Maternal II", Turno = "Matutino", EducadoraResponsavel = "Prof.ª Juliana" };
        var t3 = new Turma { EscolaId = escola1.Id, Nome = "Pré-Escola I", Turno = "Vespertino", EducadoraResponsavel = "Prof.ª Mariana" };

        // Turmas da Escola 2
        var t4 = new Turma { EscolaId = escola2.Id, Nome = "Maternal I", Turno = "Integral", EducadoraResponsavel = "Prof.ª Carla" };
        var t5 = new Turma { EscolaId = escola2.Id, Nome = "Pré-Escola II", Turno = "Matutino", EducadoraResponsavel = "Prof.ª Fernanda" };

        context.Turmas.AddRange(t1, t2, t3, t4, t5);
        context.SaveChanges();

        // Gerando crianças com responsáveis titulares, permanentes e temporários
        var turmasList = new[] { t1, t2, t3, t4, t5 };
        var random = new Random();
        string[] nomesCriancas = { "Lucas", "Sophia", "Gabriel", "Alice", "Bernardo", "Valentina", "Heitor", "Helena", "Davi", "Laura" };
        string[] sobrenomes = { "Silva", "Santos", "Oliveira", "Souza", "Rodrigues", "Ferreira", "Alves", "Pereira", "Lima", "Gomes" };

        int contadorCpfBase = 111;

        foreach (var turma in turmasList)
        {
            for (int i = 1; i <= 5; i++)
            {
                string nomeCompleto = $"{nomesCriancas[random.Next(nomesCriancas.Length)]} {sobrenomes[random.Next(sobrenomes.Length)]} {i}";
                var dataNasc = new DateTime(2023, random.Next(1, 13), random.Next(1, 28));

                var crianca = new Crianca
                {
                    TurmaId = turma.Id,
                    Nome = nomeCompleto,
                    DataNascimento = dataNasc,
                    FotoUrl = "https://images.unsplash.com/photo-1543332164-6e82f355badc?w=150&auto=format&fit=crop&q=80"
                };

                context.Criancas.Add(crianca);
                context.SaveChanges();

                // 1. Responsável Principal (Titular)
                string cpfGerado = $"{contadorCpfBase:D3}.222.333-44";
                contadorCpfBase++;

                context.Responsaveis.Add(new Responsavel
                {
                    CriancaId = crianca.Id,
                    Nome = $"Responsável de {nomeCompleto}",
                    Cpf = cpfGerado,
                    Parentesco = "Pai / Mãe",
                    Telefone = "(41) 98888-7777",
                    Principal = true,
                    PodeRetirar = true,
                    FotoUrl = "https://images.unsplash.com/photo-1507003211169-0a1dd7228f2d?w=150&auto=format&fit=crop&q=80"
                });

                // 2. Autorizado Permanente (Ex: Tio/Tia)
                context.Responsaveis.Add(new Responsavel
                {
                    CriancaId = crianca.Id,
                    Nome = $"Tio(a) de {nomeCompleto}",
                    Cpf = $"{contadorCpfBase:D3}.333.444-55",
                    Parentesco = "Tio / Tia",
                    Telefone = "(41) 99999-1111",
                    Principal = false,
                    PodeRetirar = true,
                    FotoUrl = "https://images.unsplash.com/photo-1544005313-94ddf0286df2?w=150&auto=format&fit=crop&q=80"
                });

                // 3. Autorizado Temporário (Ex: Babá ou Cuidador com período de vigência)
                context.Responsaveis.Add(new Responsavel
                {
                    CriancaId = crianca.Id,
                    Nome = $"Babá/Cuidador(a) de {nomeCompleto}",
                    Cpf = $"{contadorCpfBase:D3}.444.555-66",
                    Parentesco = "Babá / Cuidador(a) (Prov.: 01/09 a 15/09)",
                    Telefone = "(41) 97777-2222",
                    Principal = false,
                    PodeRetirar = true,
                    FotoUrl = "https://images.unsplash.com/photo-1534528741775-53994a69daeb?w=150&auto=format&fit=crop&q=80"
                });
            }
        }
        context.SaveChanges();
    }
}