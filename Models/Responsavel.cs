using System.ComponentModel.DataAnnotations;

namespace ZeloApp.Models
{
    public class Responsavel
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; } = string.Empty;

        public string Cpf { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
        public string Parentesco { get; set; } = "Pai / Mãe";
        public string FotoUrl { get; set; } = string.Empty;
        
        public bool Principal { get; set; } = false;
        public bool PodeRetirar { get; set; } = true;

        // Acesso Temporário com Período Definido
        public bool Temporario { get; set; } = false;
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }

        public int AlunoId { get; set; }
        public Aluno? Aluno { get; set; }
    }
}