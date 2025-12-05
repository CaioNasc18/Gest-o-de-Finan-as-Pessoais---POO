using System;

namespace ProjetoPOOWebApp.Classes
{
    public enum TipoTransacao
    {
        Receita,
        Despesa
    }

    public class Transacao
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public DateTime Data { get; set; } = DateTime.UtcNow;
        public int CategoriaId { get; set; } = 0;
        public TipoTransacao Tipo { get; set; } = TipoTransacao.Despesa;

        public bool ValidarValor()
        {
            return this.Valor > 0;
        }
    }
}
