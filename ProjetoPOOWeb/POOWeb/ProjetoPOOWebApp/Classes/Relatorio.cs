using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjetoPOOWebApp.Classes
{
    public class Relatorio
    {
        public decimal CalcularTotalReceitas(DateTime inicio, DateTime fim, List<Transacao> transacoes)
        {
            decimal total = 0m;
            foreach (Transacao t in transacoes)
            {
                if (t.Tipo == TipoTransacao.Receita && t.Data >= inicio && t.Data <= fim)
                {
                    total += t.Valor;
                }
            }
            return total;
        }

        public decimal CalcularTotalDespesas(DateTime inicio, DateTime fim, List<Transacao> transacoes)
        {
            decimal total = 0m;
            foreach (Transacao t in transacoes)
            {
                if (t.Tipo == TipoTransacao.Despesa && t.Data >= inicio && t.Data <= fim)
                {
                    total += t.Valor;
                }
            }
            return total;
        }

        public Dictionary<string, decimal> GerarResumoPorCategoria(DateTime inicio, DateTime fim, List<Transacao> transacoes, List<Categoria> categorias)
        {
            Dictionary<string, decimal> resumo = new Dictionary<string, decimal>();
            foreach (Categoria cat in categorias)
            {
                decimal totalCategoria = 0m;
                foreach (Transacao t in transacoes)
                {
                    if (t.CategoriaId == cat.Id && t.Data >= inicio && t.Data <= fim)
                    {
                        totalCategoria += t.Valor;
                    }
                }
                resumo[cat.Nome] = totalCategoria;
            }
            return resumo;
        }
    }
}
