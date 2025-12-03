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
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public int CategoriaId { get; set; }
        public TipoTransacao Tipo { get; set; }
    }
}
