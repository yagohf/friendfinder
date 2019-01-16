using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yagohf.Cubo.FriendFinder.Data.Repository;

namespace Yagohf.Cubo.FriendFinder.Tests.Data.Repository
{
    [TestClass]
    public class UsuarioRepositoryTests : SpecificRepositoryBaseTests
    {
        [TestMethod]
        public void Testar_Instancia()
        {
            //Arrange.

            //Act.
            UsuarioRepository repository = new UsuarioRepository(this.CriarContexto());

            //Assert.
            Assert.IsNotNull(repository);
        }
    }
}
