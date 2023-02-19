namespace DotnetWebhookGH.Api.Data;

using DotnetWebhookGH.Api.Data.Model.Issues;
using DotnetWebhookGH.Api.Data.Model.Labels;
using DotnetWebhookGH.Api.Data.Model.Reactions;
using DotnetWebhookGH.Api.Data.Model.Repositories;
using DotnetWebhookGH.Api.Data.Model.Users;
using Microsoft.EntityFrameworkCore;

public class ApiDbContext : DbContext
{
    public ApiDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Issue> Issues { get; set; } = null!;
    public DbSet<Label> Labels { get; set; } = null!;
    public DbSet<Reaction> Reactions { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Repository> Repositories { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("public");

        modelBuilder.Entity<Issue>().Configure();
        modelBuilder.Entity<Label>().Configure();
        modelBuilder.Entity<Reaction>().Configure();
        modelBuilder.Entity<User>().Configure();
        modelBuilder.Entity<Repository>().Configure();
    }
}
