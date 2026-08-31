using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ZeloApp.Models
{
    public class Aluno
    {
        public int Id { get; set; }
        public int TurmaId { get; set; }
        public Turma? Turma { get; set; }

        public string NomeCompleto { get; set; } = string.Empty;
        public string Nome => NomeCompleto;

        public DateTime DataNascimento { get; set; }
        public DateTime DataMatricula { get; set; } = DateTime.Now;
        public string Vinculo { get; set; } = "Particular";
        public string Turno { get; set; } = "Integral";
        
        public decimal Mensalidade { get; set; }
        public decimal ValorMensalidade => Mensalidade;
        public bool MensalidadeMesPaga { get; set; } = false;
        
        public string? NomeResponsavel { get; set; }
        public string? TelefoneResponsavel { get; set; }

        public string? Endereco { get; set; }
        public string? FotoUrl { get; set; }
        public string? LoginPortal { get; set; }
        public string? SenhaPortal { get; set; }

        public ICollection<Responsavel> Responsaveis { get; set; } = new List<Responsavel>();

        public Responsavel? Responsavel 
        { 
            get => Responsaveis.FirstOrDefault(r => r.Principal) ?? Responsaveis.FirstOrDefault();
            set 
            {
                if (value != null && !Responsaveis.Contains(value))
                    Responsaveis.Add(value);
            }
        }

        // Propriedade de compatibilidade ignorada pelo EF Core para não gerar conflito de banco
        private int? _responsavelId;
        [NotMapped]
        public int? ResponsavelId 
        { 
            get => _responsavelId ?? Responsaveis.FirstOrDefault(r => r.Principal)?.Id ?? Responsaveis.FirstOrDefault()?.Id;
            set => _responsavelId = value; 
        }
    }
}