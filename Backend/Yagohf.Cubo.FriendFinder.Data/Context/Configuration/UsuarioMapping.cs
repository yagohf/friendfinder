using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yagohf.Cubo.FriendFinder.Model.Entidades;

namespace Yagohf.Cubo.FriendFinder.Data.Context.Configuration
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("Id")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Login)
             .HasColumnName("Login")
             .IsRequired();

            builder.Property(x => x.Senha)
              .HasColumnName("Senha")
              .IsRequired();

            builder.Property(x => x.Nome)
              .HasColumnName("Nome")
              .IsRequired();

            //Relacionamentos.
            builder.HasMany(x => x.Amigos)
                .WithOne(x => x.Usuario)
                .HasForeignKey(x => x.IdUsuario);

            builder.HasMany(x => x.CalculoHistoricoLogs)
                .WithOne(x => x.Usuario)
                .HasForeignKey(x => x.IdUsuario);
        }
    }
}
