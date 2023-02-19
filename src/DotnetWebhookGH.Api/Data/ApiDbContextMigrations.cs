namespace DotnetWebhookGH.Api.Data;

using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Migrations.Internal;

[ExcludeFromCodeCoverage]
public class ApiDbContextMigrations : NpgsqlHistoryRepository
{
    public const string Table = "__ef_migration_history";

    public ApiDbContextMigrations(HistoryRepositoryDependencies dependencies) : base(dependencies) { }

    protected override void ConfigureTable(EntityTypeBuilder<HistoryRow> history)
    {
        base.ConfigureTable(history);

        history.Property(p => p.MigrationId).HasColumnName("migration_id");
        history.Property(p => p.ProductVersion).HasColumnName("product_version");
    }
}
