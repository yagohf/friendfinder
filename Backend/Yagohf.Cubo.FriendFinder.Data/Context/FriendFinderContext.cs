using Microsoft.EntityFrameworkCore;
using Yagohf.Cubo.FriendFinder.Data.Context.Configuration;

namespace Yagohf.Cubo.FriendFinder.Data.Context
{
    public class FriendFinderContext : DbContext
    {
        public FriendFinderContext(DbContextOptions<FriendFinderContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
            modelBuilder.ApplyConfiguration(new AmigoConfiguration());
            modelBuilder.ApplyConfiguration(new CalculoHistoricoLogConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
