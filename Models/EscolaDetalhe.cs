using System.ComponentModel.DataAnnotations;

namespace ZeloApp.Models;

public class GestorEscola
{
    public int Id { get; set; }
    public int EscolaId { get; set; }

    [Required(ErrorMessage = "Nome é obrigatório")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "E-mail é obrigatório")]
    [EmailAddress(ErrorMessage = "E-mail inválido")]
    public string Email { get; set; } = string.Empty;

    public string Cargo { get; set; } = "Professora / Educadora";
    public string Telefone { get; set; } = string.Empty;
    public string FotoUrl { get; set; } = "https://images.unsplash.com/photo-1573496359142-b8d87734a5a2?w=150&auto=format&fit=crop&q=80";
}

public class Turma
{
    public int Id { get; set; }
    public int EscolaId { get; set; }

    [Required(ErrorMessage = "Nome da turma é obrigatório")]
    public string Nome { get; set; } = string.Empty; // Ex: Berçário I, Maternal II

    public string Turno { get; set; } = "Integral"; // Manhã, Tarde, Integral
    public string EducadoraResponsavel { get; set; } = string.Empty;
    public List<Crianca> Criancas { get; set; } = new();
}

public class Responsavel
{
    public int Id { get; set; }
    public int CriancaId { get; set; }

    [Required(ErrorMessage = "Nome do responsável é obrigatório")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "CPF é obrigatório")]
    public string Cpf { get; set; } = string.Empty;

    public string Parentesco { get; set; } = "Mãe";
    public string Telefone { get; set; } = string.Empty;
    public bool PodeRetirar { get; set; } = true;
    public bool Principal { get; set; } = false;

    // Regras de Retirada Temporária / Permanente
    public bool EhTemporario { get; set; } = false;
    public DateTime? DataInicioRetirada { get; set; }
    public DateTime? DataFimRetirada { get; set; }

    public string FotoUrl { get; set; } = "https://images.unsplash.com/photo-1534528741775-53994a69daeb?w=150&auto=format&fit=crop&q=80";
}

public class Crianca
{
    public int Id { get; set; }
    public int TurmaId { get; set; }

    [Required(ErrorMessage = "Nome da criança é obrigatório")]
    public string Nome { get; set; } = string.Empty;

    public DateTime DataNascimento { get; set; } = DateTime.Today.AddYears(-2);
    public string FotoUrl { get; set; } = "https://images.unsplash.com/photo-1519689680058-324335c77eba?w=150&auto=format&fit=crop&q=80";
    
    public string EmailResponsavelConvidado { get; set; } = string.Empty;
    public List<Responsavel> Responsaveis { get; set; } = new();
}