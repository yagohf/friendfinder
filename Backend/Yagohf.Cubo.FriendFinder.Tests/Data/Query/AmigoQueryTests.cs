using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yagohf.Cubo.FriendFinder.Data.Query;

namespace Yagohf.Cubo.FriendFinder.Tests.Data.Query
{
    [TestClass]
    public class AmigoQueryTests
    {
        private readonly AmigoQuery _amigoQuery;

        public AmigoQueryTests()
        {
            this._amigoQuery = new AmigoQuery();
        }

        [TestMethod]
        public void Testar_PorId()
        {
            //Arrange


            //Act
            var query = this._amigoQuery.PorId(1);

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
            var query = this._amigoQuery.PorUsuario("yagohf");

            //Assert
            Assert.IsNotNull(query);
            Assert.IsNotNull(query.Includes);
            Assert.IsNotNull(query.Criteria);
            Assert.IsNotNull(query.OrderBy);
            Assert.AreEqual(1, query.Includes.Count);
            Assert.AreEqual(1, query.Criteria.Count);
            Assert.AreEqual(1, query.OrderBy.Count);
        }
    }
}
