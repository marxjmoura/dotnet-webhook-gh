var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "The challenge has begun!");

app.Run();
