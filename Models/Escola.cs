using System.ComponentModel.DataAnnotations;

namespace ZeloApp.Models;
public class Escola
{
    [Key]
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Cnpj { get; set; } = string.Empty;
    public string Endereco { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string NomeGestor { get; set; } = string.Empty;
    public string EmailGestor { get; set; } = string.Empty;
    
    // Credenciais da Diretora da Escola
    public string LoginAdmin { get; set; } = string.Empty;
    public string SenhaAdmin { get; set; } = string.Empty;
    
    public bool Ativo { get; set; } = true;
}