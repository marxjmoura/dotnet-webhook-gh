namespace DotnetWebhookGH.Api.Data.Model.Issues;

using DotnetWebhookGH.Api.Data.Model.Labels;
using DotnetWebhookGH.Api.Data.Model.Reactions;
using DotnetWebhookGH.Api.Data.Model.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class IssueDBMap
{
    public static void Configure(this EntityTypeBuilder<Issue> issue)
    {
        issue.ToTable("issue");

        issue.HasKey(p => p.Delivery);
        issue.HasIndex(p => p.Number);
        issue.HasIndex(p => p.UpdatedAt);

        issue.Property(p => p.Delivery)
            .HasColumnName("delivery")
            .IsRequired();

        issue.Property(p => p.Event)
            .HasColumnName("event")
            .HasConversion<string>()
            .IsRequired();

        issue.Property(p => p.Number)
            .HasColumnName("number")
            .IsRequired();

        issue.Property(p => p.RepositoryId)
            .HasColumnName("repository_id");

        issue.Property(p => p.SenderId)
            .HasColumnName("sender_id");

        issue.Property(p => p.Title)
            .HasColumnName("title")
            .HasMaxLength(1024)
            .IsRequired();

        issue.Property(p => p.Body)
            .HasColumnName("body");

        issue.Property(p => p.State)
            .HasColumnName("state")
            .HasConversion<string>()
            .IsRequired();

        issue.Property(p => p.Locked)
            .HasColumnName("locked")
            .IsRequired();

        issue.Property(p => p.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        issue.Property(p => p.UpdatedAt)
            .HasColumnName("updated_at")
            .IsRequired();

        issue.Property(p => p.ClosedAt)
            .HasColumnName("closed_at");

        issue.HasOne(p => p.Reactions)
            .WithOne(p => p.Issue)
            .HasForeignKey<Reaction>(p => p.Delivery);

        issue.HasMany(p => p.Assignees)
            .WithMany(p => p.AssignedIssues)
            .UsingEntity<IssueAssignee>(
                p => p.HasOne<User>().WithMany().HasForeignKey(p => p.UserId),
                p => p.HasOne<Issue>().WithMany().HasForeignKey(p => p.Delivery),
                p =>
                {
                    p.ToTable("issue_assignee");
                    p.HasKey(p => new { p.Delivery, p.UserId });
                    p.Property(p => p.UserId).HasColumnName("user_id");
                    p.Property(p => p.Delivery).HasColumnName("issue_delivery");
                });

        issue.HasMany(p => p.Labels)
            .WithMany(p => p.Issues)
            .UsingEntity<IssueLabel>(
                p => p.HasOne<Label>().WithMany().HasForeignKey(p => p.LabelId),
                p => p.HasOne<Issue>().WithMany().HasForeignKey(p => p.Delivery),
                p =>
                {
                    p.ToTable("issue_label");
                    p.HasKey(p => new { p.Delivery, p.LabelId });
                    p.Property(p => p.LabelId).HasColumnName("label_id");
                    p.Property(p => p.Delivery).HasColumnName("issue_delivery");
                });
    }
}
