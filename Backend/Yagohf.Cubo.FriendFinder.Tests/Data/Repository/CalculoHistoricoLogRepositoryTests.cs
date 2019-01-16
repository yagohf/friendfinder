using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yagohf.Cubo.FriendFinder.Data.Repository;

namespace Yagohf.Cubo.FriendFinder.Tests.Data.Repository
{
    [TestClass]
    public class CalculoHistoricoLogRepositoryTests : SpecificRepositoryBaseTests
    {
        [TestMethod]
        public void Testar_Instancia()
        {
            //Arrange.

            //Act.
            CalculoHistoricoLogRepository repository = new CalculoHistoricoLogRepository(this.CriarContexto());

            //Assert.
            Assert.IsNotNull(repository);
        }
    }
}
