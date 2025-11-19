using HelloWorldApp;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseStaticFiles();

// Página inicial -> index.html
app.MapGet("/", context =>
{
    context.Response.Redirect("index.html");
    return Task.CompletedTask;
});

// Endpoint que devolve um objeto Product em JSON
app.MapGet("/product", () =>
{
    var product = new Product
    {
        Id = 1,
        Name = "Laptop",
        Price = 1200.50m
    };
    return Results.Json(product);
});

app.Run();