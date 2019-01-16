using Microsoft.EntityFrameworkCore;
using Yagohf.Cubo.FriendFinder.Data.Context;

namespace Yagohf.Cubo.FriendFinder.Tests.Data.Repository
{
    public class SpecificRepositoryBaseTests
    {
        protected FriendFinderContext CriarContexto(string nomeBanco = null)
        {
            //Arrange
            var options = new DbContextOptionsBuilder<FriendFinderContext>()
               .UseInMemoryDatabase(databaseName: nomeBanco ?? "TESTAR_CRIACAO_DB")
               .Options;

            //Act.
            FriendFinderContext context = new FriendFinderContext(options);
            return context;
        }
    }
}
