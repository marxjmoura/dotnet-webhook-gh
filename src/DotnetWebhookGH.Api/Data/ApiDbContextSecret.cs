namespace DotnetWebhookGH.Api.Data;

using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class ApiDbContextSecret
{
    public string? Host { get; set; }

    public string? Port { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string ToConnectionString()
    {
        var host = $"Server={Host}";
        var port = $"Port={Port}";
        var username = $"User Id={Username}";
        var password = $"Password={Password}";
        var database = "Database=webhook";
        var error = "Include Error Detail=True;";

        return $"{host};{port};{database};{username};{password};{error};";
    }
}
