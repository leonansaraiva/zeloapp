using System.ComponentModel.DataAnnotations;

namespace ZeloApp.Models;

public class Escola
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome da escola é obrigatório")]
    [StringLength(100)]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O CNPJ é obrigatório")]
    [StringLength(18)]
    public string Cnpj { get; set; } = string.Empty;

    public string? Endereco { get; set; }
    public string? Telefone { get; set; }

    [Required(ErrorMessage = "O nome do gestor é obrigatório")]
    public string NomeGestor { get; set; } = string.Empty;

    [Required(ErrorMessage = "O e-mail do gestor é obrigatório")]
    [EmailAddress]
    public string EmailGestor { get; set; } = string.Empty;

    public bool Ativo { get; set; } = true;
    public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
}