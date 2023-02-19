namespace DotnetWebhookGH.Api.Data.Model.Users;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class UserDBMap
{
    public static void Configure(this EntityTypeBuilder<User> user)
    {
        user.ToTable("user");

        user.HasKey(p => p.Id);
        user.HasIndex(p => p.Login);

        user.Property(p => p.Id)
            .HasColumnName("id")
            .ValueGeneratedNever();

        user.Property(p => p.Login)
            .HasColumnName("login")
            .HasMaxLength(50)
            .IsRequired();

        user.HasMany(p => p.Repositories)
            .WithOne(p => p.Owner)
            .HasForeignKey(p => p.OwnerId);

        user.HasMany(p => p.SentIssues)
            .WithOne(p => p.Sender)
            .HasForeignKey(p => p.SenderId);
    }
}
