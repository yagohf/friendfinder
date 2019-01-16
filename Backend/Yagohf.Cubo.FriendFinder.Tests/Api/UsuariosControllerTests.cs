using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using Yagohf.Cubo.FriendFinder.Api.Controllers;
using Yagohf.Cubo.FriendFinder.Business.Interface.Domain;
using Yagohf.Cubo.FriendFinder.Model.DTO;

namespace Yagohf.Cubo.FriendFinder.Tests.Api
{
    [TestClass]
    public class UsuariosControllerTests
    {
        private readonly Mock<IUsuarioBusiness> _usuarioBusinessMock;
        private UsuariosController _usuariosController;

        public UsuariosControllerTests()
        {
            this._usuarioBusinessMock = new Mock<IUsuarioBusiness>();
        }

        [TestInitialize]
        public void Inicializar()
        {
            this._usuariosController = new UsuariosController(this._usuarioBusinessMock.Object);
        }

        [TestMethod]
        public async Task Testar_GetPorId()
        {
            //Arrange.
            int idUsuario = 1;
            UsuarioDTO mockUsuario = new UsuarioDTO()
            {
                Id = 1,
                Nome = "Usuario",
                Login = "yagohf"
            };

            this._usuarioBusinessMock
                .Setup(bsn => bsn.SelecionarPorIdAsync(idUsuario))
                .Returns(Task.FromResult(mockUsuario));

            //Act.
            var result = await this._usuariosController.GetPorId(idUsuario);
            var okResult = result as OkObjectResult;

            //Assert.
            Assert.IsNotNull(okResult);
            Assert.IsInstanceOfType(okResult.Value, typeof(UsuarioDTO));
            Assert.AreEqual(mockUsuario, okResult.Value as UsuarioDTO);
        }

        [TestMethod]
        public async Task Testar_Post()
        {
            //Arrange.
            UsuarioDTO mockRegistro = new UsuarioDTO()
            {
                Login = "yagohf",
                Nome = "Yago",
                Id = 1
            };

            RegistroDTO dadosRegistro = new RegistroDTO()
            {
                Login = "yagohf",
                Nome = "Yago",
                Senha = "123mudar"
            };

            this._usuarioBusinessMock
                .Setup(bsn => bsn.RegistrarAsync(dadosRegistro))
                .Returns(Task.FromResult(mockRegistro));

            //Act.
            var result = await this._usuariosController.Post(dadosRegistro);
            var createdAtResult = result as CreatedAtActionResult;

            //Assert.
            Assert.IsNotNull(createdAtResult);
            Assert.IsInstanceOfType(createdAtResult.Value, typeof(UsuarioDTO));
            Assert.AreEqual(mockRegistro, createdAtResult.Value as UsuarioDTO);

            //Testar URL de redirecionamento.
            Assert.AreEqual(createdAtResult.ActionName, nameof(this._usuariosController.GetPorId));
            Assert.AreEqual(createdAtResult.RouteValues["id"], mockRegistro.Id);
        }

        [TestMethod]
        public async Task Testar_PostAutenticacao()
        {
            //Arrange.
            TokenDTO mockToken = new TokenDTO()
            {
                Login = "yagohf",
                Nome = "Yago",
                Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6InlhZ29oZiIsIm5iZiI6MTU0NzYwNjQ5NywiZXhwIjoxNTQ3NjkyODk3LCJpYXQiOjE1NDc2MDY0OTd9.qzmJSEvtoHphSpOkFJ81mN2FqeiyXk47zo3euVFxACk"
            };

            AutenticacaoDTO dadosAutenticacao = new AutenticacaoDTO()
            {
                Login = "yagohf",
                Senha = "123mudar"
            };

            this._usuarioBusinessMock
                .Setup(bsn => bsn.GerarTokenAsync(dadosAutenticacao))
                .Returns(Task.FromResult(mockToken));

            //Act.
            var result = await this._usuariosController.PostAutenticacao(dadosAutenticacao);
            var okResult = result as OkObjectResult;

            //Assert.
            Assert.IsNotNull(okResult);
            Assert.IsInstanceOfType(okResult.Value, typeof(TokenDTO));
            Assert.AreEqual(mockToken, okResult.Value as TokenDTO);
        }
    }
}
