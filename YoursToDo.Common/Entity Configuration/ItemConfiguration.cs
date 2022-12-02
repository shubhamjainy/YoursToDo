using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YoursToDo.Common.Models;

namespace YoursToDo.Common.Entity_Configuration
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.Property(x => x.Id)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(x => x.Content)
                .HasColumnType("nvarchar(200)")
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.CreatedAt)
                .HasColumnType("datetime")
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(x => x.UserId)
                .HasColumnType("int")
                .IsRequired();

            builder
                .HasOne(p => p.User)
                .WithMany(b => b.Items)
                .HasForeignKey(p => p.UserId);
        }
    }
}