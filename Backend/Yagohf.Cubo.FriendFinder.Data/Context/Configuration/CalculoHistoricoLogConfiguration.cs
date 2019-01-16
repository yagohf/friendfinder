using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yagohf.Cubo.FriendFinder.Model.Entidades;

namespace Yagohf.Cubo.FriendFinder.Data.Context.Configuration
{
    public class CalculoHistoricoLogConfiguration : IEntityTypeConfiguration<CalculoHistoricoLog>
    {
        public void Configure(EntityTypeBuilder<CalculoHistoricoLog> builder)
        {
            builder.ToTable("CalculoHistoricoLog", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("Id")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(x => x.IdUsuario)
            .HasColumnName("IdUsuario")
            .IsRequired();

            builder.Property(x => x.DataOcorrencia)
           .HasColumnName("DataOcorrencia")
           .IsRequired();
        }
    }
}
