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

    public transacao criarTransacao(string descricao, float valor, categoria categoria, TipoTransacao tipo)
    {
        transacao novaTransacao = new transacao();
        novaTransacao.descricao = descricao;
        novaTransacao.valor = valor;
        novaTransacao.categoriaId = categoria.id;
        novaTransacao.tipo = tipo;
        novaTransacao.data = DateTime.Now;

        Console.WriteLine("De uma descrição para a transação:"); //opcional
        novaTransacao.descricao = Console.ReadLine();

        if (novaTransacao.descricao.Trim == "")
        {
            novaTransacao.descricao = "SEM DESCRIÇÃO";
        }

        Console.WriteLine("Insira o valor da transação:"); //obrigatorio
        novaTransacao.valor = float.Parse(Console.ReadLine());

        while (!novaTransacao.ValidarValor())
        {
            Console.WriteLine("Insira um valor válido para a transação:");
            novaTransacao.valor = float.Parse(Console.ReadLine());
        }

        do //testestesteste
        {
            Console.WriteLine("Qual é o tipo da transação? (1- Receita, 2- Despesa)"); //obrigatorio
            int tipoInput = int.Parse(Console.ReadLine());

            if (tipoInput == 1)
            {
                novaTransacao.tipo = TipoTransacao.Receita;
            }
            else if (tipoInput == 2)
            {
                novaTransacao.tipo = TipoTransacao.Despesa;
            }
            else
            {
                Console.WriteLine("Tipo inválido. Por favor, insira 1 para Receita ou 2 para Despesa.");
            }
        } while (tipoInput != 1 && tipoInput != 2);

        Random rnd = new Random();//obrigatorio (automatico)
        novaTransacao.id = rnd.Next(1, 100000);

        while (novaTransacao.id == this.id)
        {
            novaTransacao.id = rnd.Next(1, 100000);
        }

        Console.WriteLine("existe uma categoria associada a esta transação? (s/n)"); //opcional
        string resposta = Console.ReadLine().ToLower();

        if (resposta == "s")
        {
            novaTransacao.categoriaId = categoria.id;
        }
        else if (resposta == "n")
        {
            novaTransacao.categoriaId = 0; // Sem categoria
        }
        else
        {
            Console.WriteLine("Opção inválida. Nenhuma categoria foi associada.");
            novaTransacao.categoriaId = 0; // Sem categoria
        }

        novaTransacao.data = DateTime.Now;//obrigatorio (automatico)

        Console.WriteLine("Transação criada com sucesso com o ID " + novaTransacao.id);
        return novaTransacao;
    }

    public relatorio gerarRelatorio(DateTime inicio, DateTime fim)
    {
        Console.WriteLine("Qual o período do relatório? Insira a data de início (dd/mm/aaaa):");
        inicio = DateTime.Parse(Console.ReadLine());
        Console.WriteLine("Insira a data de fim (dd/mm/aaaa):");
        fim = DateTime.Parse(Console.ReadLine());
        relatorio novoRelatorio = new relatorio();
        Console.WriteLine("Relatório gerado com sucesso.");
         
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

    public bool ValidarValor()
    {
        if (valor <= 0)
        {
            Console.WriteLine("O valor da transação deve ser maior que zero.");
            return false;
        }
        return true;
    }

    public void Editar(string descricao, float valor)
    {
        this.descricao = descricao;
        this.valor = valor;

        Console.WriteLine("O que deseja editar? (d/v)");
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
        else if (resposta == "v")
        {
            Console.WriteLine("Insira o novo valor:");
            valor = float.Parse(Console.ReadLine());
            this.valor = valor;

            while (!ValidarValor())
            {
                Console.WriteLine("Insira um valor válido:");
                valor = float.Parse(Console.ReadLine());
                this.valor = valor;
            }

            Console.WriteLine("Valor atualizado com sucesso.");
        }
        else
        {
            Console.WriteLine("Opção inválida. Nenhuma alteração foi feita.");
        }
    }

    public void Eliminar()
    {
        Console.WriteLine("Tem certeza que deseja eliminar esta transação? (s/n)");
        string resposta = Console.ReadLine().ToLower();

        if (resposta == "s")
        {
            this.id = 0;
            this.descricao = null;
            this.valor = 0;
            this.data = default(DateTime);
            this.categoriaId = 0;
            this.tipo = 0;

            Console.WriteLine("Transação eliminada com sucesso.");
        }
        else
        {
            Console.WriteLine("Operação cancelada. A transação não foi eliminada.");
        }
    }
}

public class relatorio
{
    public float calcularTotalReceitas(DateTime inicio, DateTime fim, List<transacao> transacoes)
    {
        float total = 0;
        foreach (transacao t in transacoes)
        {
            if (t.tipo == TipoTransacao.Receita && t.data >= inicio && t.data <= fim)
            {
                total += t.valor;
            }
        }

        Console.WriteLine($"Total de receitas entre {inicio.ToShortDateString()} e {fim.ToShortDateString()}: {total}");
        return total;
    }

    public float calcularTotalDespesas(DateTime inicio, DateTime fim, List<transacao> transacoes)
    {
        float total = 0;
        foreach (transacao t in transacoes)
        {
            if (t.tipo == TipoTransacao.Despesa && t.data >= inicio && t.data <= fim)
            {
                total += t.valor;
            }
        }
        Console.WriteLine($"Total de despesas entre {inicio.ToShortDateString()} e {fim.ToShortDateString()}: {total}");
        return total;
    }

    public Dictionary<> GerarResumoPorCategoria(DateTime inicio, DateTime fim, List<transacao> transacoes, List<categoria> categorias)
    {
        Dictionary<string, float> resumo = new Dictionary<string, float>();

        foreach (categoria cat in categorias)
        {
            float totalCategoria = 0;
            foreach (transacao t in transacoes)
            {
                if (t.categoriaId == cat.id && t.data >= inicio && t.data <= fim)
                {
                    totalCategoria += t.valor;
                }

            }
            resumo[cat.nome] = totalCategoria;
            Console.WriteLine($"Categoria: {cat.nome}, Total: {totalCategoria}");

        }
        return resumo;
    }
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
