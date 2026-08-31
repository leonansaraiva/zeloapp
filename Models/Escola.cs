using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ZeloApp.Models
{
    public class Escola
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da escola é obrigatório.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O CNPJ é obrigatório.")]
        public string Cnpj { get; set; } = string.Empty;

        public string? Endereco { get; set; }

        // Dados do Gestor
        [Required(ErrorMessage = "O nome do gestor é obrigatório.")]
        public string NomeGestor { get; set; } = string.Empty;
        public string? CpfGestor { get; set; }
        public string TelefoneGestor { get; set; } = string.Empty;
        public string EmailGestor { get; set; } = string.Empty;

        // Contrato
        public int MesesContrato { get; set; } = 12;
        public DateTime DataInicioContrato { get; set; } = DateTime.Today;

        // Credenciais de Acesso Admin
        public string LoginAdmin { get; set; } = string.Empty;
        public string SenhaAdmin { get; set; } = string.Empty;

        public string LoginPortal { get; set; } = string.Empty;
        public string SenhaPortal { get; set; } = string.Empty;

        public decimal ValorMensalidadePlataforma { get; set; } = 450.00m;
        public bool MensalidadeAtualPaga { get; set; } = true;

        public List<Turma> Turmas { get; set; } = new();
        public List<Aluno> Alunos { get; set; } = new();
    }
}