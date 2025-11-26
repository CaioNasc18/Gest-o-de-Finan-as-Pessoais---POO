using ProjetoPOOWebApp.Classes;
using System.Collections.Generic;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
WebApplication app = builder.Build();

app.UseStaticFiles();

List<Categoria> categories = new List<Categoria>
{
    new Categoria { Id = 0, Nome = "Salário", Descricao = "Recebimento mensal" },
    new Categoria { Id = 1, Nome = "Alimentação", Descricao = "Despesas com comida" },
};

List<Utilizador> users = new List<Utilizador>
{
    new Utilizador { Id = 0, Nome = "Alice", Email = "123@gmail.com", Senha = "senha123" },
};

List<Transacao> transactions = new List<Transacao>
{
    new Transacao { Id = 0, Valor = 5000, Data = DateTime.UtcNow, Descricao = "Salário de Junho", CategoriaId = 0 }
};

// Página inicial -> index.html
app.MapGet("/", context =>
{
    context.Response.Redirect("index.html");
    return Task.CompletedTask;
});

// Categories endpoints
app.MapGet("/categories", () => Results.Json(categories));
app.MapPost("/categories", (Categoria newCategory) =>
{
    int nextId = categories.Any() ? categories.Max(c => c.Id) + 1 : 1;
    newCategory.Id = nextId;
    if (string.IsNullOrWhiteSpace(newCategory.Nome))
    {
        newCategory.Nome = "SEM NOME";
    }
    categories.Add(newCategory);
    return Results.Json(newCategory);
});

// Users endpoints (básico)
app.MapGet("/users", () => Results.Json(users));
app.MapPost("/users", (Utilizador newUser) =>
{
    int nextId = users.Any() ? users.Max(u => u.Id) + 1 : 1;
    newUser.Id = nextId;
    users.Add(newUser);
    return Results.Json(newUser);
});

// Transactions endpoints
app.MapGet("/transactions", () => Results.Json(transactions));
app.MapPost("/transactions", (Transacao newTransaction) =>
{
    if (!newTransaction.Validar())
    {
        return Results.BadRequest(new { error = "Value must be > 0" });
    }

    int nextId = transactions.Any() ? transactions.Max(t => t.Id) + 1 : 1;
    newTransaction.Id = nextId;
    newTransaction.Data = DateTime.UtcNow;
    if (string.IsNullOrWhiteSpace(newTransaction.Descricao))
    {
        newTransaction.Descricao = "SEM DESCRIÇÃO";
    }

    transactions.Add(newTransaction);
    return Results.Json(newTransaction);
});

app.Run();