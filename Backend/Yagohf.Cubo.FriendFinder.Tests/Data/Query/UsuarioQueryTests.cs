using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yagohf.Cubo.FriendFinder.Data.Query;

namespace Yagohf.Cubo.FriendFinder.Tests.Data.Query
{
    [TestClass]
    public class UsuarioQueryTests
    {
        private readonly UsuarioQuery _usuarioQuery;

        public UsuarioQueryTests()
        {
            this._usuarioQuery = new UsuarioQuery();
        }

        [TestMethod]
        public void Testar_PorId()
        {
            //Arrange


            //Act
            var query = this._usuarioQuery.PorId(1);

            //Assert
            Assert.IsNotNull(query);
            Assert.IsNotNull(query.Includes);
            Assert.IsNotNull(query.Criteria);
            Assert.AreEqual(0, query.Includes.Count);
            Assert.AreEqual(1, query.Criteria.Count);
        }

        [TestMethod]
        public void Testar_PorUsuario()
        {
            //Arrange


            //Act
            var query = this._usuarioQuery.PorUsuario("yagohf");

            //Assert
            Assert.IsNotNull(query);
            Assert.IsNotNull(query.Includes);
            Assert.IsNotNull(query.Criteria);
            Assert.AreEqual(0, query.Includes.Count);
            Assert.AreEqual(1, query.Criteria.Count);
        }

        [TestMethod]
        public void Testar_PorUsuarioSenha()
        {
            //Arrange


            //Act
            var query = this._usuarioQuery.PorUsuarioSenha("yagohf", "123mudar");

            //Assert
            Assert.IsNotNull(query);
            Assert.IsNotNull(query.Includes);
            Assert.IsNotNull(query.Criteria);
            Assert.AreEqual(0, query.Includes.Count);
            Assert.AreEqual(1, query.Criteria.Count);
        }
    }
}
