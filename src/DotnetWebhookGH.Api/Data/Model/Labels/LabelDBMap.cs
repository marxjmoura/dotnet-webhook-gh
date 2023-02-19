namespace DotnetWebhookGH.Api.Data.Model.Labels;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class LabelDBMap
{
    public static void Configure(this EntityTypeBuilder<Label> label)
    {
        label.ToTable("label");

        label.HasKey(p => p.Id);

        label.Property(p => p.Id)
            .HasColumnName("id")
            .ValueGeneratedNever();

        label.Property(p => p.Name)
            .HasColumnName("name")
            .HasMaxLength(50)
            .IsRequired();

        label.Property(p => p.Color)
            .HasColumnName("color")
            .HasMaxLength(6);

        label.Property(p => p.Description)
            .HasColumnName("description")
            .HasMaxLength(100);

        label.Property(p => p.IsDefault)
            .HasColumnName("is_default");
    }
}
