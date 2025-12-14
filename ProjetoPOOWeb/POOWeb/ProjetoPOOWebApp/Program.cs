using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using ProjetoPOOWebApp.Classes;

// builder e app explícitos (sem var)
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Configurar serialização para camelCase (id, descricao, valor, etc.)
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.SerializerOptions.WriteIndented = true;
});

WebApplication app = builder.Build();

app.UseStaticFiles();

//
// Dados em memória (simulam BD)
//
List<Categoria> categorias = DataStorage.Load<Categoria>("categorias.json");

List<Utilizador> utilizadores = DataStorage.Load<Utilizador>("utilizadores.json");

List<Transacao> transacoes = DataStorage.Load<Transacao>("transacoes.json");

Relatorio relatorioHelper = new Relatorio();

//
// Rotas
//

// raíz -> index.html
app.MapGet("/", context =>
{
    context.Response.Redirect("index.html");
    return Task.CompletedTask;
});

//
// Categorias
//
app.MapGet("/categorias", () => Results.Json(categorias));

app.MapGet("/categorias/{id:int}", (int id) =>
{
    Categoria? cat = categorias.FirstOrDefault(c => c.Id == id);
    if (cat == null) return Results.NotFound(new { error = "Categoria não encontrada" });
    return Results.Json(cat);
});

app.MapPost("/categorias", (Categoria novaCategoria) =>
{
    int novoId = categorias.Any() ? categorias.Max(c => c.Id) + 1 : 1;
    novaCategoria.Id = novoId;
    if (string.IsNullOrWhiteSpace(novaCategoria.Nome)) novaCategoria.Nome = "SEM NOME";
    if (string.IsNullOrWhiteSpace(novaCategoria.Descricao)) novaCategoria.Descricao = string.Empty;
    categorias.Add(novaCategoria);
    DataStorage.Save("categorias.json", categorias);

    return Results.Json(novaCategoria);
});

app.MapPut("/categorias/{id:int}", (int id, Categoria categoriaAtualizada) =>
{
    Categoria? existente = categorias.FirstOrDefault(c => c.Id == id);
    if (existente == null) return Results.NotFound(new { error = "Categoria não encontrada" });
    existente.Nome = string.IsNullOrWhiteSpace(categoriaAtualizada.Nome) ? existente.Nome : categoriaAtualizada.Nome;
    existente.Descricao = categoriaAtualizada.Descricao ?? existente.Descricao;
    DataStorage.Save("categorias.json", categorias);

    return Results.Json(existente);
});

app.MapDelete("/categorias/{id:int}", (int id) =>
{
    Categoria? existente = categorias.FirstOrDefault(c => c.Id == id);
    if (existente == null) return Results.NotFound(new { error = "Categoria não encontrada" });
    categorias.Remove(existente);
    DataStorage.Save("categorias.json", categorias);

    return Results.Ok(new { message = "Categoria removida" });
});

//
// Utilizadores
//
app.MapGet("/utilizadores", () => Results.Json(utilizadores));

app.MapGet("/utilizadores/{id:int}", (int id) =>
{
    Utilizador? u = utilizadores.FirstOrDefault(x => x.Id == id);
    if (u == null) return Results.NotFound(new { error = "Utilizador não encontrado" });
    return Results.Json(u);
});

app.MapPost("/utilizadores", (Utilizador novo) =>
{
    int novoId = utilizadores.Any() ? utilizadores.Max(u => u.Id) + 1 : 1;
    novo.Id = novoId;
    if (!novo.ValidarEmail()) return Results.BadRequest(new { error = "Email inválido" });
    if (!novo.ValidarSenha()) return Results.BadRequest(new { error = "Senha inválida (mín 4 caracteres)" });
    utilizadores.Add(novo);
    DataStorage.Save("utilizadores.json", utilizadores);

    return Results.Json(novo);
});

app.MapPut("/utilizadores/{id:int}", (int id, Utilizador atualizado) =>
{
    Utilizador? existente = utilizadores.FirstOrDefault(u => u.Id == id);
    if (existente == null) return Results.NotFound(new { error = "Utilizador não encontrado" });
    existente.Nome = string.IsNullOrWhiteSpace(atualizado.Nome) ? existente.Nome : atualizado.Nome;
    existente.Email = string.IsNullOrWhiteSpace(atualizado.Email) ? existente.Email : atualizado.Email;
    existente.Senha = string.IsNullOrWhiteSpace(atualizado.Senha) ? existente.Senha : atualizado.Senha;
    existente.Tipo = atualizado.Tipo;
    DataStorage.Save("utilizadores.json", utilizadores);
    return Results.Json(existente);
});

app.MapDelete("/utilizadores/{id:int}", (int id) =>
{
    Utilizador? existente = utilizadores.FirstOrDefault(u => u.Id == id);
    if (existente == null) return Results.NotFound(new { error = "Utilizador não encontrado" });
    utilizadores.Remove(existente);
    DataStorage.Save("utilizadores.json", utilizadores);
    return Results.Ok(new { message = "Utilizador removido" });
});

//
// Login (simples, em memória)
//
app.MapPost("/login", (LoginRequest req) =>
{
    Utilizador? u = utilizadores.FirstOrDefault(x => x.Email == req.Email && x.Senha == req.Senha);
    if (u == null) return Results.Unauthorized();
    // Em produção aqui devolveríamos um token JWT. Para já devolvemos o utilizador (sem senha).
    Utilizador responseUser = new Utilizador
    {
        Id = u.Id,
        Nome = u.Nome,
        Email = u.Email,
        Senha = string.Empty,
        Tipo = u.Tipo
    };
    return Results.Json(responseUser);
});

//
// Transacoes
//
app.MapGet("/transacoes", () => Results.Json(transacoes));

app.MapGet("/transacoes/{id:int}", (int id) =>
{
    Transacao? t = transacoes.FirstOrDefault(x => x.Id == id);
    if (t == null) return Results.NotFound(new { error = "Transação não encontrada" });
    return Results.Json(t);
});

app.MapPost("/transacoes", (Transacao nova) =>
{
    if (!nova.ValidarValor()) return Results.BadRequest(new { error = "Valor inválido" });
    int novoId = transacoes.Any() ? transacoes.Max(x => x.Id) + 1 : 1;
    nova.Id = novoId;
    if (nova.Data == default(DateTime)) nova.Data = DateTime.UtcNow;
    transacoes.Add(nova);
    DataStorage.Save("transacoes.json", transacoes);
    return Results.Json(nova);
});

app.MapPut("/transacoes/{id:int}", (int id, Transacao atualizado) =>
{
    Transacao? existente = transacoes.FirstOrDefault(x => x.Id == id);
    if (existente == null) return Results.NotFound(new { error = "Transação não encontrada" });
    existente.Descricao = string.IsNullOrWhiteSpace(atualizado.Descricao) ? existente.Descricao : atualizado.Descricao;
    existente.Valor = atualizado.Valor <= 0 ? existente.Valor : atualizado.Valor;
    existente.CategoriaId = atualizado.CategoriaId;
    existente.Tipo = atualizado.Tipo;
    existente.Data = atualizado.Data == default(DateTime) ? existente.Data : atualizado.Data;
    DataStorage.Save("transacoes.json", transacoes);

    return Results.Json(existente);
});

app.MapDelete("/transacoes/{id:int}", (int id) =>
{
    Transacao? existente = transacoes.FirstOrDefault(x => x.Id == id);
    if (existente == null) return Results.NotFound(new { error = "Transação não encontrada" });
    transacoes.Remove(existente);
    DataStorage.Save("transacoes.json", transacoes);
    return Results.Ok(new { message = "Transação removida" });
});

//
// Relatórios
//
// GET /relatorios?inicio=2024-01-01&fim=2024-12-31
app.MapGet("/relatorios", (HttpRequest request) =>
{
    string? inicioStr = request.Query["inicio"];
    string? fimStr = request.Query["fim"];

    DateTime inicio;
    DateTime fim;

    if (!DateTime.TryParse(inicioStr, out inicio) || !DateTime.TryParse(fimStr, out fim))
    {
        return Results.BadRequest(new { error = "Datas inválidas. Use início e fim no formato yyyy-MM-dd." });
    }

    decimal totalReceitas = relatorioHelper.CalcularTotalReceitas(inicio, fim, transacoes);
    decimal totalDespesas = relatorioHelper.CalcularTotalDespesas(inicio, fim, transacoes);
    Dictionary<string, decimal> resumoCategorias = relatorioHelper.GerarResumoPorCategoria(inicio, fim, transacoes, categorias);

    return Results.Json(new
    {
        inicio = inicio.ToString("yyyy-MM-dd"),
        fim = fim.ToString("yyyy-MM-dd"),
        totalReceitas = totalReceitas,
        totalDespesas = totalDespesas,
        resumoPorCategoria = resumoCategorias
    });
});

app.Run();

//
// Helper para login (DTO)
public record LoginRequest(string Email, string Senha);