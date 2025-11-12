var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.UseStaticFiles(); // permite servir HTML, CSS, JS, imagens
app.MapGet("/", async context =>
{
    context.Response.Redirect("index.html");
});
app.Run();