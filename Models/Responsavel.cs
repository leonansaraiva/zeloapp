using System;

namespace ZeloApp.Models
{
    public class Responsavel
    {
        public int Id { get; set; }
        public int EscolaId { get; set; }
        
        public int? AlunoId { get; set; }
        public Aluno? Aluno { get; set; }

        public string NomeCompleto { get; set; } = string.Empty;
        public string? Cpf { get; set; }
        public string? Telefone { get; set; }
        public string? Parentesco { get; set; }
        public bool Principal { get; set; }
        public bool PodeRetirar { get; set; }
        public bool Temporario { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
    }
}