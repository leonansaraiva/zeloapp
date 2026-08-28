using System.ComponentModel.DataAnnotations;

namespace ZeloApp.Models;

public class Responsavel
{
    public int Id { get; set; }

    [Required]
    public string Nome { get; set; } = string.Empty;

    [Required]
    public string Cpf { get; set; } = string.Empty;

    public string Telefone { get; set; } = string.Empty;

    public string Parentesco { get; set; } = "Pai / Mãe";

    public bool Principal { get; set; } = false; // Indica se é o titular da conta no portal

    public bool PodeRetirar { get; set; } = true; // Status de liberação na portaria

    public string FotoUrl { get; set; } = string.Empty;

    // Chave estrangeira para a Criança
    public int CriancaId { get; set; }
    public Crianca? Crianca { get; set; }
}