using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yagohf.Cubo.FriendFinder.Data.Context;
using Yagohf.Cubo.FriendFinder.Model.Entidades;

namespace Yagohf.Cubo.FriendFinder.Tests.Data.Context
{
    [TestClass]
    public class FriendFinderContextTests
    {
        [TestMethod]
        public void Testar_Criacao()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<FriendFinderContext>()
               .UseInMemoryDatabase(databaseName: "TESTAR_CRIACAO_DB")
               .Options;

            //Act.
            FriendFinderContext context = new FriendFinderContext(options);
            var setAmigos = context.Set<Amigo>(); //Forçar a inicialização (OnModelCreating)

            //Assert.
            Assert.IsNotNull(context);
            Assert.IsNotNull(setAmigos);
        }
    }
}
