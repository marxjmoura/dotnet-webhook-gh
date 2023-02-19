namespace DotnetWebhookGH.Api.Data.Model.Repositories;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class RepositoryDBMap
{
    public static void Configure(this EntityTypeBuilder<Repository> repository)
    {
        repository.ToTable("repository");

        repository.HasKey(p => p.Id);
        repository.HasIndex(p => p.Name);

        repository.Property(p => p.Id)
            .HasColumnName("id")
            .ValueGeneratedNever();

        repository.Property(p => p.OwnerId)
            .HasColumnName("owner_id");

        repository.Property(p => p.Name)
            .HasColumnName("name")
            .HasMaxLength(100)
            .IsRequired();

        repository.Property(p => p.Description)
            .HasColumnName("description");

        repository.Property(p => p.Language)
            .HasColumnName("language");

        repository.Property(p => p.DefaultBranch)
            .HasColumnName("default_branch");

        repository.Property(p => p.Topics)
            .HasColumnName("topics");

        repository.Property(p => p.IsPrivate)
            .HasColumnName("is_private");

        repository.Property(p => p.IsFork)
            .HasColumnName("is_fork");

        repository.HasMany(p => p.Issues)
            .WithOne(p => p.Repository)
            .HasForeignKey(p => p.RepositoryId);
    }
}
