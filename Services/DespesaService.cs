using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZeloApp.Models;

namespace ZeloApp.Services
{
    public class DespesaService
    {
        private readonly AppDbContext _context;

        public DespesaService(AppDbContext context)
        {
            _context = context;
        }

        // Retorna despesas filtradas por Escola (ou SuperAdmin se escolaId for null) e mês/ano
        public async Task<List<Despesa>> ObterDespesasPorMesAsync(int? escolaId, int mes, int ano)
        {
            return await _context.Despesas
                .Where(d => d.EscolaId == escolaId && 
                            d.DataCompetencia.Month == mes && 
                            d.DataCompetencia.Year == ano)
                .OrderByDescending(d => d.DataCompetencia)
                .ToListAsync();
        }

        // Salva despesa (única ou gerando parcelas automáticas mês a mês)
        public async Task SalvarDespesaAsync(Despesa novaDespesa, decimal valorTotalInformado, bool parcelado, int totalParcelas)
        {
            if (parcelado && totalParcelas > 1)
            {
                var grupoId = Guid.NewGuid();
                decimal valorParcela = Math.Round(valorTotalInformado / totalParcelas, 2);

                for (int i = 0; i < totalParcelas; i++)
                {
                    var despesaParcela = new Despesa
                    {
                        EscolaId = novaDespesa.EscolaId,
                        Descricao = $"{novaDespesa.Descricao} ({i + 1}/{totalParcelas})",
                        Valor = valorParcela,
                        Categoria = novaDespesa.Categoria,
                        DataCompetencia = novaDespesa.DataCompetencia.AddMonths(i),
                        EhParcelado = true,
                        NumeroParcela = i + 1,
                        TotalParcelas = totalParcelas,
                        GrupoParcelamentoId = grupoId
                    };
                    _context.Despesas.Add(despesaParcela);
                }
            }
            else
            {
                novaDespesa.Valor = valorTotalInformado;
                novaDespesa.EhParcelado = false;
                novaDespesa.NumeroParcela = 1;
                novaDespesa.TotalParcelas = 1;
                _context.Despesas.Add(novaDespesa);
            }

            await _context.SaveChangesAsync();
        }

        public async Task ExcluirAsync(int id)
        {
            var despesa = await _context.Despesas.FindAsync(id);
            if (despesa != null)
            {
                _context.Despesas.Remove(despesa);
                await _context.SaveChangesAsync();
            }
        }
    }
}