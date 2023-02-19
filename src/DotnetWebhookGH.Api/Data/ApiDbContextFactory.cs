namespace DotnetWebhookGH.Api.Data;

using Amazon.SecretsManager;
using Amazon.SecretsManager.Extensions.Caching;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

[ExcludeFromCodeCoverage]
public class ApiDbContextFactory : IDbContextFactory<ApiDbContext>
{
    private readonly ISecretsManagerCache _secrets;

    public ApiDbContextFactory(IAmazonSecretsManager secrets)
    {
        _secrets = new SecretsManagerCache(secrets);
    }

    public ApiDbContext CreateDbContext()
    {
        return CreateDbContextAsync().GetAwaiter().GetResult();
    }

    public async Task<ApiDbContext> CreateDbContextAsync(CancellationToken cancellationToken = default)
    {
        var secret = await _secrets.GetSecretString("dotnet-webhook-gh/PostgreSQL");
        var dbContextSecret = JsonSerializer.Deserialize<ApiDbContextSecret>(secret);
        var connectionString = dbContextSecret!.ToConnectionString();

        var builder = new DbContextOptionsBuilder<ApiDbContext>()
            .UseNpgsql(connectionString, options => options.MigrationsHistoryTable(ApiDbContextMigrations.Table))
            .ReplaceService<IHistoryRepository, ApiDbContextMigrations>();

        return new ApiDbContext(builder.Options);
    }
}
