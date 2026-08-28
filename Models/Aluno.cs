using System.ComponentModel.DataAnnotations;

namespace ZeloApp.Models
{
    public class Aluno
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; } = string.Empty;

        public string NomeResponsavel { get; set; } = string.Empty;
        public string TelefoneResponsavel { get; set; } = string.Empty;

        // Vínculo / Convênio
        public bool ConvenioPrefeitura { get; set; } = false; // True = Prefeitura, False = Particular
        public string TurnoAluno { get; set; } = "Integral"; // Matutino, Vespertino ou Integral

        // Credenciais do Portal
        public string LoginPortal { get; set; } = string.Empty;
        public string SenhaPortal { get; set; } = string.Empty;

        // Financeiro
        public decimal ValorMensalidade { get; set; } = 350.00m;
        public bool MensalidadeMesPaga { get; set; } = true;

        public int TurmaId { get; set; }
        public Turma? Turma { get; set; }
    }
}