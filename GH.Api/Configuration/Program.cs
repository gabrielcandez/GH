using GH.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureOptions();
builder.ConfigureDatabase();

var app = builder.Build();
app.MapGet("/", () => "Hello World!");

app.Run();