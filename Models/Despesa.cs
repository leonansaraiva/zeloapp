using System;

namespace ZeloApp.Models
{
    public class Despesa
    {
        public int Id { get; set; }
        
        // Se for null ou 0, é despesa do SuperAdmin (SaaS). Se tiver ID, pertence à escola específica.
        public int? EscolaId { get; set; }
        public Escola? Escola { get; set; }

        public string Descricao { get; set; } = string.Empty;
        
        // Valor da parcela individual ou valor total (se à vista)
        public decimal Valor { get; set; }
        
        public string Categoria { get; set; } = "Geral"; // Ex: Infraestrutura, Manutenção, Pessoal, etc.

        // Mês/Ano de competência inicial
        public DateTime DataCompetencia { get; set; } = DateTime.Today;

        // Controle de Parcelamento
        public bool EhParcelado { get; set; } = false;
        public int NumeroParcela { get; set; } = 1;
        public int TotalParcelas { get; set; } = 1;
        
        // Agrupa parcelas geradas pela mesma compra
        public Guid? GrupoParcelamentoId { get; set; }
    }
}