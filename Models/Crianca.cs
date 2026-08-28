using System;
using System.ComponentModel.DataAnnotations;

namespace ZeloApp.Models;

public class Crianca
{
    public int Id { get; set; }

    [Required]
    public string Nome { get; set; } = string.Empty;

    public DateTime DataNascimento { get; set; }

    public string FotoUrl { get; set; } = string.Empty;

    // Chave estrangeira para a Turma
    public int TurmaId { get; set; }
    public Turma? Turma { get; set; }

    // Dados financeiros e tipo de matrícula
    public string TipoMatricula { get; set; } = "Particular"; // "Particular" ou "Convênio / Prefeitura"
    
    [Range(0, 99999.99)]
    public decimal ValorMensalidade { get; set; } = 650.00m;

    // Relação com os responsáveis
    public List<Responsavel>? Responsaveis { get; set; }
}