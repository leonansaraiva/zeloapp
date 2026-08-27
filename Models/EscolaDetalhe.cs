using System.ComponentModel.DataAnnotations;

namespace ZeloApp.Models;

public class GestorEscola
{
    [Key]
    public int Id { get; set; }
    public int EscolaId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Cargo { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string FotoUrl { get; set; } = string.Empty;
}

public class Turma
{
    [Key]
    public int Id { get; set; }
    public int EscolaId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Turno { get; set; } = string.Empty;
    public string EducadoraResponsavel { get; set; } = string.Empty;
}

public class Crianca
{
    [Key]
    public int Id { get; set; }
    public int TurmaId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public DateTime DataNascimento { get; set; }
    public string FotoUrl { get; set; } = string.Empty;
}
public class Responsavel
{
    [Key]
    public int Id { get; set; }
    public int CriancaId { get; set; }
    
    // Propriedade de navegação exigida pelo .Include()
    public Crianca? Crianca { get; set; }

    public string Nome { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public string Parentesco { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public bool Principal { get; set; } = false;
    public bool PodeRetirar { get; set; } = true;
    public bool EhTemporario { get; set; } = false;
    public DateTime? DataInicioRetirada { get; set; }
    public DateTime? DataFimRetirada { get; set; }
    public string FotoUrl { get; set; } = string.Empty;
}