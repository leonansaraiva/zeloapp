using System.ComponentModel.DataAnnotations;

namespace ZeloApp.Models
{
    public class Escola
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; } = string.Empty;

        public string Cnpj { get; set; } = string.Empty;
        public string NomeGestor { get; set; } = string.Empty;
        public string TelefoneGestor { get; set; } = string.Empty;
        public string EmailGestor { get; set; } = string.Empty;

        // Credenciais de Acesso Admin da Escola
        public string LoginAdmin { get; set; } = string.Empty;
        public string SenhaAdmin { get; set; } = string.Empty;

        // Credenciais do Portal de Pais da Escola
        public string LoginPortal { get; set; } = string.Empty;
        public string SenhaPortal { get; set; } = string.Empty;

        // Financeiro SaaS (Quanto a escola paga para você)
        public decimal ValorMensalidadePlataforma { get; set; } = 450.00m;
        public bool MensalidadeAtualPaga { get; set; } = true;

        // Relacionamento com Turmas
        public List<Turma> Turmas { get; set; } = new();
    }
}