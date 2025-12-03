using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ChristmasBackend.Domain.Entities;

namespace ChristmasBackend.Infrastructure.EntityTypeConfigurations
{
    public class AnimationConfig : IEntityTypeConfiguration<Animation>
    {
        public void Configure(EntityTypeBuilder<Animation> builder)
        {
            builder.ToTable("Animations");

            // Primary key
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id)
                   .ValueGeneratedOnAdd();

            // Properties
            builder.Property(a => a.FileName)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(a => a.FilePath)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.Property(a => a.UserId)
                   .IsRequired();

            // Index
            builder.HasIndex(a => a.UserId);

            // Relationship
            builder.HasOne(a => a.User)
                   .WithMany(u => u.Animations)
                   .HasForeignKey(a => a.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
