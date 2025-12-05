using System;

namespace ProjetoPOOWebApp.Classes
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;

        public bool ValidarNome()
        {
            return !string.IsNullOrWhiteSpace(this.Nome);
        }
    }
}
