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
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public TipoUtilizador Tipo { get; set; }
    }
}