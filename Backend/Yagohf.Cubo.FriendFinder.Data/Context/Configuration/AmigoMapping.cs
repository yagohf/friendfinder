using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yagohf.Cubo.FriendFinder.Model.Entidades;

namespace Yagohf.Cubo.FriendFinder.Data.Context.Configuration
{
    public class AmigoConfiguration : IEntityTypeConfiguration<Amigo>
    {
        public void Configure(EntityTypeBuilder<Amigo> builder)
        {
            builder.ToTable("Amigo", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("Id")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(x => x.IdUsuario)
             .HasColumnName("IdUsuario")
             .IsRequired();

            builder.Property(x => x.Nome)
              .HasColumnName("Nome")
              .IsRequired();

            builder.Property(x => x.Latitude)
               .HasColumnName("Latitude")
               .IsRequired()
               .HasColumnType("decimal(8,6)");

            builder.Property(x => x.Longitude)
                .HasColumnName("Longitude")
                .IsRequired()
                .HasColumnType("decimal(9,6)");
        }
    }
}
