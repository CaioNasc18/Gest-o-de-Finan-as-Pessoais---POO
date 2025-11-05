using System;
using System.Collections.Generic;

public class categoria
{
    int id;
    string nome;
    string descricao;

    public void Criar()
    {
        Console.WriteLine("Qual será a categoria?");
        nome = Console.ReadLine();

        Console.WriteLine("Quer adicionar alguma descrição?");
        descricao = Console.ReadLine();

        if (descricao.trim == "")
        {
            descricao == "SEM DESCRIÇÃO";
        }

        random rnd = new random;
        id = rnd.Next(1, 100000)

        //TODO: CASO ID JA EXISTA, CRIAR OUTRO ID
    }

    public void Editar(nome, descricao)
    {

    }

    public void Eliminar()
    {

    }
}

public class utilizador
{
    int id;
    string nome;
    string email;
    string senha;
    Perfil perfil;

    public void Registar (string nome, string email, string senha)
    {
        console.writeline("Preencha todos os campos para registar um novo utilizador.");

        
        while (string.trim(nome) == "" || string.trim(email) == "" || string.trim(senha) == "")
        {
    
            console.writeline("Nome: ");
            nome = console.readline();

            console.writeline("Email: ");
            email = console.readline();

            console.writeline("Senha: ");
            senha = console.readline();

            if (string.trim(nome) == "" || string.trim(email) == "" || string.trim(senha) == "")
            {
                console.writeline("Todos os campos são obrigatórios. Por favor, tente novamente.");
            }
            else if (!email.contains("@") || !email.contains("."))
            {
                console.writeline("Email inválido. Por favor, insira um email válido.");
                email = "";
            }
            else
            {
                console.writeline("Registo bem-sucedido!");
            }

        }


    }

    public bool (string email, string password)
}

public class transacao
{
    int id;
    string descricao;
    float valor;
    DateTime data;
    int categoriaId;
    TipoTransacao tipo;
}
    
public enum Perfil
{
    Administrador,
    Utilizador_Normal
}

public enum TipoTransacao
{
    Receita,
    Despesa
}
