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
        this.nome = nome;
        this.descricao = descricao;

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

    public bool Login(string email, string password)
    {
        
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
