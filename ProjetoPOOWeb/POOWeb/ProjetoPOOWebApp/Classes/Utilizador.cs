using System;

namespace ProjetoPOOWebApp.Classes
{
    public enum Perfil
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
        public Perfil Tipo { get; set; } = Perfil.Utilizador_Normal;

        public bool ValidarEmail()
        {
            return !string.IsNullOrWhiteSpace(this.Email) && this.Email.Contains("@") && this.Email.Contains(".");
        }

        public bool ValidarSenha()
        {
            return !string.IsNullOrWhiteSpace(this.Senha) && this.Senha.Length >= 4;
        }
    }
}
