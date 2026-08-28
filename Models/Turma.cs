using System.ComponentModel.DataAnnotations;

namespace ZeloApp.Models
{
    public class Turma
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; } = string.Empty; // Ex: Maternal I

        public string Turno { get; set; } = "Integral"; // Matutino, Vespertino ou Integral
        public string ProfessorResponsavel { get; set; } = string.Empty;

        public int EscolaId { get; set; }
        public Escola? Escola { get; set; }

        public List<Aluno> Alunos { get; set; } = new();
    }
}