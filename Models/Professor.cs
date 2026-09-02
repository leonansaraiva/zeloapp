using System.ComponentModel.DataAnnotations;

namespace ZeloApp.Models
{
    public class Professor
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do professor(a) é obrigatório.")]
        [StringLength(150)]
        public string NomeCompleto { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Especialidade { get; set; } = "Professor(a) Regente";

        [StringLength(20)]
        public string? Telefone { get; set; }

        [StringLength(100)]
        public string? Email { get; set; }

        [StringLength(14)]
        public string? Cpf { get; set; }

        [StringLength(250)]
        public string? Endereco { get; set; }

        [StringLength(500)]
        public string? FotoUrl { get; set; } // Link da foto de perfil

        public DateTime DataInclusao { get; set; } = DateTime.Now; // Data de cadastro

        public int EscolaId { get; set; }
        public Escola? Escola { get; set; }
    }
}