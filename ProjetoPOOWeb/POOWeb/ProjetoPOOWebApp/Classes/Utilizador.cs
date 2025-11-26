namespace ProjetoPOOWebApp.Classes
{
    public enum TipoUtilizador
    {
        Administrador,
        Utilizador_Normal
    }
    public class Utilizador
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public TipoUtilizador Tipo { get; set; }
    }
}