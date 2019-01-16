using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Yagohf.Cubo.FriendFinder.Api.Controllers;
using Yagohf.Cubo.FriendFinder.Business.Interface.Domain;
using Yagohf.Cubo.FriendFinder.Infrastructure.Paging;
using Yagohf.Cubo.FriendFinder.Model.DTO;

namespace Yagohf.Cubo.FriendFinder.Tests.Api
{
    [TestClass]
    public class AmigosControllerTests
    {
        private readonly Mock<IAmigoBusiness> _amigoBusinessMock;
        private AmigosController _amigosController;

        public AmigosControllerTests()
        {
            this._amigoBusinessMock = new Mock<IAmigoBusiness>();
        }

        [TestInitialize]
        public void Inicializar()
        {
            this._amigosController = new AmigosController(this._amigoBusinessMock.Object);

            //Mockar usuário logado.
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "yagohf"),
            };

            var identity = new ClaimsIdentity(claims);
            var claimsPrincipal = new ClaimsPrincipal(identity);

            var mockPrincipal = new Mock<IPrincipal>();
            mockPrincipal.Setup(x => x.Identity).Returns(identity);

            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(m => m.User).Returns(claimsPrincipal);

            this._amigosController.ControllerContext.HttpContext = mockHttpContext.Object;
        }

        [TestMethod]
        public async Task Testar_Get_Paginando()
        {
            //Arrange.
            int pagina = 1;
            int registrosPorPagina = 10;

            var listaAmigo = new List<AmigoDTO>();
            for (int i = 0; i < registrosPorPagina; i++)
            {
                listaAmigo.Add(new AmigoDTO()
                {
                    Id = i,
                    Nome = $"Amigo {i}",
                    Latitude = i,
                    Longitude = i
                });
            }

            var paginacao = new Paginacao(pagina, listaAmigo.Count, registrosPorPagina);
            var mockListaPaginada = new Listagem<AmigoDTO>(listaAmigo, paginacao);

            this._amigoBusinessMock
                .Setup(bsn => bsn.ListarPorUsuarioAsync(It.IsAny<string>(), pagina))
                .Returns(Task.FromResult(mockListaPaginada));

            //Act.
            var result = await this._amigosController.Get(pagina);
            var okResult = result as OkObjectResult;

            //Assert.
            Assert.IsNotNull(okResult);
            Assert.IsInstanceOfType(okResult.Value, typeof(Listagem<AmigoDTO>));
            Assert.AreEqual(mockListaPaginada, okResult.Value as Listagem<AmigoDTO>);
        }

        [TestMethod]
        public async Task Testar_Get_SemPaginar()
        {
            //Arrange.
            var listaAmigo = new List<AmigoDTO>();
            for (int i = 0; i < 5; i++)
            {
                listaAmigo.Add(new AmigoDTO()
                {
                    Id = i,
                    Nome = $"Amigo {i}",
                    Latitude = i,
                    Longitude = i
                });
            }

            var mockListaSemPaginacao = new Listagem<AmigoDTO>(listaAmigo);
            this._amigoBusinessMock
                .Setup(bsn => bsn.ListarPorUsuarioAsync(It.IsAny<string>(), null))
                .Returns(Task.FromResult(mockListaSemPaginacao));

            //Act.
            var result = await this._amigosController.Get(null);
            var okResult = result as OkObjectResult;

            //Assert.
            Assert.IsNotNull(okResult);
            Assert.IsInstanceOfType(okResult.Value, typeof(Listagem<AmigoDTO>));
            Assert.AreEqual(mockListaSemPaginacao, okResult.Value as Listagem<AmigoDTO>);
        }

        [TestMethod]
        public async Task Testar_GetPorId()
        {
            //Arrange.
            int idAmigo = 1;
            AmigoDTO mockAmigo = new AmigoDTO()
            {
                Id = 1,
                Nome = "Amigo",
                Latitude = 10,
                Longitude = 20
            };

            this._amigoBusinessMock
                .Setup(bsn => bsn.SelecionarPorIdAsync(idAmigo))
                .Returns(Task.FromResult(mockAmigo));

            //Act.
            var result = await this._amigosController.GetPorId(idAmigo);
            var okResult = result as OkObjectResult;

            //Assert.
            Assert.IsNotNull(okResult);
            Assert.IsInstanceOfType(okResult.Value, typeof(AmigoDTO));
            Assert.AreEqual(mockAmigo, okResult.Value as AmigoDTO);
        }

        [TestMethod]
        public async Task Testar_Post()
        {
            //Arrange.
            AmigoDTO mockAmigo = new AmigoDTO()
            {
                Id = 1,
                Nome = "Amigo",
                Latitude = 10,
                Longitude = 20
            };

            AmigoRegistrarDTO amigoRegistrar = new AmigoRegistrarDTO()
            {
                Nome = "Amigo",
                Latitude = 10,
                Longitude = 20
            };

            this._amigoBusinessMock
                .Setup(bsn => bsn.CriarAsync(It.IsAny<string>(), amigoRegistrar))
                .Returns(Task.FromResult(mockAmigo));

            //Act.
            var result = await this._amigosController.Post(amigoRegistrar);
            var createdAtResult = result as CreatedAtActionResult;

            //Assert.
            Assert.IsNotNull(createdAtResult);
            Assert.IsInstanceOfType(createdAtResult.Value, typeof(AmigoDTO));
            Assert.AreEqual(mockAmigo, createdAtResult.Value as AmigoDTO);

            //Testar URL de redirecionamento.
            Assert.AreEqual(createdAtResult.ActionName, nameof(this._amigosController.GetPorId));
            Assert.AreEqual(createdAtResult.RouteValues["id"], mockAmigo.Id);
        }

        [TestMethod]
        public async Task Testar_GetAmigosProximos()
        {
            //Arrange.
            int idAmigoReferencia = 1;
            AmigosProximosDTO amigosProximos = new AmigosProximosDTO()
            {
                Atual = new AmigoDTO()
                {
                    Id = 1,
                    Nome = "Amigo",
                    Latitude = 10,
                    Longitude = 20
                },
                Proximos = new List<AmigoDTO>()
                {
                    new AmigoDTO(){ Id = 2, Nome = "Amigo 2", Latitude = 30, Longitude = 40 },
                    new AmigoDTO(){ Id = 3, Nome = "Amigo 3", Latitude = 50, Longitude = 60 }
                }
            };


            this._amigoBusinessMock
                .Setup(bsn => bsn.ListarAmigosProximosPorUsuarioAsync(It.IsAny<string>(), idAmigoReferencia))
                .Returns(Task.FromResult(amigosProximos));

            //Act.
            var result = await this._amigosController.GetAmigosProximos(idAmigoReferencia);
            var okResult = result as OkObjectResult;

            //Assert.
            Assert.IsNotNull(okResult);
            Assert.IsInstanceOfType(okResult.Value, typeof(AmigosProximosDTO));
            Assert.AreEqual(amigosProximos, okResult.Value as AmigosProximosDTO);
        }

        [TestMethod]
        public async Task Testar_Delete()
        {
            //Arrange.
            int idExcluir = 1;

            this._amigoBusinessMock
                .Setup(bsn => bsn.ExcluirAsync(It.IsAny<string>(), idExcluir))
                .Returns(Task.CompletedTask);

            //Act.
            var result = await this._amigosController.Delete(idExcluir);

            //Assert.
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }
    }
}
