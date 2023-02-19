namespace DotnetWebhookGH.Api.Data.Model.Reactions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class ReactionDBMap
{
    public static void Configure(this EntityTypeBuilder<Reaction> reaction)
    {
        reaction.ToTable("reaction");

        reaction.HasKey(p => p.Delivery);

        reaction.Property(p => p.Delivery)
            .HasColumnName("delivery")
            .ValueGeneratedNever();

        reaction.Property(p => p.TotalCount)
            .HasColumnName("total_count")
            .IsRequired();

        reaction.Property(p => p.OnePlus)
            .HasColumnName("one_plus")
            .IsRequired();

        reaction.Property(p => p.OneMinus)
            .HasColumnName("one_minus")
            .IsRequired();

        reaction.Property(p => p.Laugh)
            .HasColumnName("laugh")
            .IsRequired();

        reaction.Property(p => p.Hooray)
            .HasColumnName("hooray")
            .IsRequired();

        reaction.Property(p => p.Confused)
            .HasColumnName("confused")
            .IsRequired();

        reaction.Property(p => p.Heart)
            .HasColumnName("heart")
            .IsRequired();

        reaction.Property(p => p.Rocket)
            .HasColumnName("rocket")
            .IsRequired();

        reaction.Property(p => p.Eyes)
            .HasColumnName("eyes")
            .IsRequired();
    }
}
