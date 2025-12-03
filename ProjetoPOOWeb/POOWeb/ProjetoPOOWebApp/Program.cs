using ProjetoPOOWebApp.Classes;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Serve arquivos estáticos (HTML, JS, CSS)
app.UseStaticFiles();

// LISTAS EM MEMÓRIA
List<Categoria> categorias = new()
{
    new Categoria { Id = 1, Nome = "Alimentação", Descricao = "Compras e refeições" },
    new Categoria { Id = 2, Nome = "Transporte", Descricao = "Combustível, autocarro" },
    new Categoria { Id = 3, Nome = "Lazer", Descricao = "Filmes, jogos, viagens" }
};

List<Transacao> transacoes = new()
{
    new Transacao { Id = 1, Descricao = "Supermercado", Valor = 50.75m, Data = DateTime.UtcNow.AddDays(-3), CategoriaId = 1, Tipo = TipoTransacao.Despesa },
    new Transacao { Id = 2, Descricao = "Salário", Valor = 1200m, Data = DateTime.UtcNow.AddDays(-10), CategoriaId = 0, Tipo = TipoTransacao.Receita }
};

// REDIRECIONA RAIZ → index.html
app.MapGet("/", context =>
{
    context.Response.Redirect("index.html");
    return Task.CompletedTask;
});

// ROTAS
app.MapGet("/categorias", () => Results.Json(categorias));

app.MapGet("/transacoes", () => Results.Json(transacoes));

app.MapPost("/transacoes", (Transacao novaTransacao) =>
{
    if (novaTransacao.Valor <= 0)
        return Results.BadRequest(new { error = "Valor deve ser maior que 0" });

    int novoId = transacoes.Any() ? transacoes.Max(t => t.Id) + 1 : 1;
    novaTransacao.Id = novoId;
    novaTransacao.Data = DateTime.UtcNow;

    transacoes.Add(novaTransacao);
    return Results.Json(novaTransacao);
});

app.Run();
