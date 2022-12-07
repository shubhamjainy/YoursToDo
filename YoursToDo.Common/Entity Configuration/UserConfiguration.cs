using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YoursToDo.Common.Entity;

namespace YoursToDo.Common.Entity_Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Id)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(x=> x.Name)
                .HasColumnType("nvarchar(20)")
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.Email)
                .HasColumnType("nvarchar(30)")
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(x => x.Password)
                .HasColumnType("nvarchar(20)")
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}