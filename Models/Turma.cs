using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ZeloApp.Models
{
    public class Turma
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da turma é obrigatório.")]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        public string Turno { get; set; } = "Integral"; // Matutino, Vespertino, Integral

        public string? ProfessorResponsavel { get; set; } // Legado / Texto rápido

        // Novos Campos Solicitados
        public DateTime? DataInicioVigencia { get; set; } = new DateTime(DateTime.Now.Year, 2, 1);
        public DateTime? DataFimVigencia { get; set; } = new DateTime(DateTime.Now.Year, 12, 20);
        
        [StringLength(2000)]
        public string? ResumoPlanejamento { get; set; }

        // Relações
        public int EscolaId { get; set; }
        public Escola? Escola { get; set; }

        public List<Aluno> Alunos { get; set; } = new();
        public List<ProfessorTurma> ProfessorTurmas { get; set; } = new();
        public List<HistoricoMovimentacao> Historicos { get; set; } = new();
    }

    // Tabela associativa para suportar Múltiplas Professoras por Turma
    public class ProfessorTurma
    {
        public int Id { get; set; }
        public int TurmaId { get; set; }
        public Turma? Turma { get; set; }
        
        public int ProfessorId { get; set; }
        public Professor? Professor { get; set; }
    }

    // Modelo de Auditoria / Histórico de Movimentações
    public class HistoricoMovimentacao
    {
        [Key]
        public int Id { get; set; }
        public int TurmaId { get; set; }
        public DateTime DataHora { get; set; } = DateTime.Now;
        public string Descricao { get; set; } = string.Empty;
        public string Tipo { get; set; } = "Informativo"; // Matrícula, Alteração, Frequência, etc.
    }
}