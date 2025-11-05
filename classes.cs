using System;
using System.Collections.Generic;

public class categoria
{
    int id;
    string nome;
    string descricao;

    public void Criar()
    {
        categoria novaCategoria = new categoria();

        Console.WriteLine("Qual será a categoria?");
        novaCategoria.nome = Console.ReadLine();

        if (nome.Trim == "")
        {
            Console.WriteLine("O nome da categoria não pode estar vazio. Por favor, insira um nome válido.");
            novaCategoria.nome = Console.ReadLine();
        }

        Console.WriteLine("Quer adicionar alguma descrição?");
        novaCategoria.descricao = Console.ReadLine();

        if (descricao.Trim == "")
        {
            novaCategoria.descricao == "SEM DESCRIÇÃO";
        }

        random rnd = new random();
        novaCategoria.id = rnd.Next(1, 100000);

        while(novaCategoria.id == this.id)
        {
            novaCategoria.id = rnd.Next(1, 100000);
        }

        Console.WriteLine($"Categoria '{novaCategoria.nome}' criada com sucesso com o ID {novaCategoria.id}.");
    }

    public void Editar(string nome, string descricao)
    {
        Console.WriteLine("Deseja alterar a descrição ou o nome? (d/n)");
        string resposta = Console.ReadLine().ToLower();

        if (resposta == "d")
        {
            Console.WriteLine("Insira a nova descrição:");
            descricao = Console.ReadLine();
            this.descricao = descricao;

            if (descricao.Trim == "")
            {
                descricao = "SEM DESCRIÇÃO";
                this.descricao = descricao;
            }

            Console.WriteLine("Descrição atualizada com sucesso.");
        }
        else if (resposta == "n")
        {
            Console.WriteLine("Insira o novo nome:");
            nome = Console.ReadLine();
            this.nome = nome;

            while (string.Trim(nome) == "")
            {
                Console.WriteLine("O nome não pode estar vazio. Insira o novo nome:");
                nome = Console.ReadLine();
                this.nome = nome;
            }

            Console.WriteLine("Nome atualizado com sucesso.");
        }
        else
        {
            Console.WriteLine("Opção inválida. Nenhuma alteração foi feita.");
        }

    }

    public void Eliminar()
    {
        Console.WriteLine("Qual categoria deseja eliminar?");
        string nomeCategoria = Console.ReadLine();

        if (nomeCategoria == this.nome)
        {
            this.id = 0;
            this.nome = null;
            this.descricao = null;

            Console.WriteLine($"Categoria '{nomeCategoria}' eliminada com sucesso.");
        }
        else
        {
            Console.WriteLine("Categoria não encontrada.");
        }
    }
}

public class utilizador
{
    int id;
    string nome;
    string email;
    string password;
    Perfil perfil;

    public void Registar ()
    {

        utilizador novoUtilizador = new utilizador();
        console.writeline("Preencha todos os campos para registar um novo utilizador.");

        
        while (string.trim(nome) == "" || string.trim(email) == "" || string.trim(password) == "")
        {
    
            console.writeline("Nome: ");
            novoUtilizador.nome = console.readline();

            console.writeline("Email: ");
            novoUtilizador.email = console.readline();

            console.writeline("Password: ");
            senha = console.readline();

            if (string.trim(nome) == "" || string.trim(email) == "" || string.trim(password) == "")
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

    public bool Login(string email, string password)
    {
        Console.WriteLine("Insira seu email:");
        email = Console.ReadLine();
        Console.WriteLine("Insira sua senha:");
        password = Console.ReadLine();
        if (email == this.email && password == this.password)
        {
            Console.WriteLine("Login bem-sucedido!");
            return true;
        }
        else
        {
            Console.WriteLine("Email ou senha incorretos. Tente novamente.");
            return false;
        }
    }
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
