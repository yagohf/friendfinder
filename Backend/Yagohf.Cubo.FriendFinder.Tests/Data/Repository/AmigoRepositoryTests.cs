using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yagohf.Cubo.FriendFinder.Data.Repository;

namespace Yagohf.Cubo.FriendFinder.Tests.Data.Repository
{
    [TestClass]
    public class AmigoRepositoryTests : SpecificRepositoryBaseTests
    {
        [TestMethod]
        public void Testar_Instancia()
        {
            //Arrange.

            //Act.
            AmigoRepository repository = new AmigoRepository(this.CriarContexto());

            //Assert.
            Assert.IsNotNull(repository);
        }
    }
}
