using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yagohf.Cubo.FriendFinder.Business.Domain;
using Yagohf.Cubo.FriendFinder.Business.Interface.Domain;
using Yagohf.Cubo.FriendFinder.Business.Interface.Helper;
using Yagohf.Cubo.FriendFinder.Business.MapperProfile;
using Yagohf.Cubo.FriendFinder.Data.Interface.Query;
using Yagohf.Cubo.FriendFinder.Data.Interface.Repository;
using Yagohf.Cubo.FriendFinder.Data.Query;
using Yagohf.Cubo.FriendFinder.Infrastructure.Configuration;
using Yagohf.Cubo.FriendFinder.Infrastructure.Exception;
using Yagohf.Cubo.FriendFinder.Infrastructure.Extensions;
using Yagohf.Cubo.FriendFinder.Infrastructure.Paging;
using Yagohf.Cubo.FriendFinder.Model.DTO;
using Yagohf.Cubo.FriendFinder.Model.Entidades;

namespace Yagohf.Cubo.FriendFinder.Tests.Business
{
    [TestClass]
    public class AmigoBusinessTests
    {
        private readonly Mock<ICalculadoraDistanciaPontosBusiness> _calculadoraDistanciaPontosBusinessMock;
        private readonly Mock<ICalculoHistoricoLogHelper> _calculoHistoricoLogHelperMock;
        private readonly Mock<IAmigoRepository> _amigoRepositoryMock;
        private readonly Mock<IUsuarioRepository> _usuarioRepositoryMock;
        private readonly Mock<IAmigoQuery> _amigoQueryMock;
        private readonly Mock<IUsuarioQuery> _usuarioQueryMock;
        private readonly Mock<IOptions<Parametros>> _parametrosMock;
        private readonly IMapper _mapper;
        private AmigoBusiness _amigoBusiness;

        public AmigoBusinessTests()
        {
            this._calculadoraDistanciaPontosBusinessMock = new Mock<ICalculadoraDistanciaPontosBusiness>();
            this._calculoHistoricoLogHelperMock = new Mock<ICalculoHistoricoLogHelper>();
            this._amigoRepositoryMock = new Mock<IAmigoRepository>();
            this._usuarioRepositoryMock = new Mock<IUsuarioRepository>();
            this._amigoQueryMock = new Mock<IAmigoQuery>();
            this._usuarioQueryMock = new Mock<IUsuarioQuery>();
            this._parametrosMock = new Mock<IOptions<Parametros>>();

            MapperConfiguration mapperConfiguration = new MapperConfiguration(mConfig =>
            {
                mConfig.AddProfile(new BusinessMapperProfile());
            });

            this._mapper = mapperConfiguration.CreateMapper();
        }

        [TestInitialize]
        public void Inicializar()
        {
            this._amigoBusiness = new AmigoBusiness(
                this._calculadoraDistanciaPontosBusinessMock.Object,
                this._calculoHistoricoLogHelperMock.Object,
                this._amigoRepositoryMock.Object,
                this._usuarioRepositoryMock.Object,
                this._amigoQueryMock.Object,
                this._usuarioQueryMock.Object,
                this._parametrosMock.Object,
                this._mapper
                );
        }

        #region [ Criar ]
        [TestMethod]
        [ExpectedException(typeof(BusinessException))]
        public async Task Testar_CriarAsync_SemNome()
        {
            //Arrange.
            string usuario = "yagohf";
            AmigoRegistrarDTO amigo = new AmigoRegistrarDTO()
            {
                Nome = "",
                Latitude = 20,
                Longitude = 30
            };

            //Act.
            await this._amigoBusiness.CriarAsync(usuario, amigo);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException))]
        public async Task Testar_CriarAsync_LatLngRepetida()
        {
            //Arrange.
            string usuario = "yagohf";
            AmigoRegistrarDTO amigo = new AmigoRegistrarDTO()
            {
                Nome = "Leandro",
                Latitude = 20,
                Longitude = 30
            };

            //Mockar query de usuário por login.
            var queryUsuarioPorLogin = new Query<Usuario>();
            this._usuarioQueryMock.Setup(x => x.PorUsuario(usuario))
                  .Returns(queryUsuarioPorLogin);

            //Mockar usuário por login.
            var mockUsuario = new Usuario()
            {
                Id = 1,
                Login = usuario,
                Nome = "Yago"
            };

            this._usuarioRepositoryMock.Setup(x => x.SelecionarUnicoAsync(It.Is<Query<Usuario>>(it => it.Equals(queryUsuarioPorLogin))))
                  .Returns(Task.FromResult(mockUsuario));

            //Mockar query de listagem de amigos por usuário.
            var queryAmigosUsuario = new Query<Amigo>();
            this._amigoQueryMock.Setup(x => x.PorUsuario(usuario))
                  .Returns(queryAmigosUsuario);

            //Mockar lista de amigos por usuário.
            var mockAmigos = new List<Amigo>()
            {
                new Amigo() { Id = 1, Nome = "Mauri", Latitude= 20, Longitude = 30 },
                new Amigo() { Id = 2, Nome = "Kofazu", Latitude= 30, Longitude = 50 }
            };

            this._amigoRepositoryMock.Setup(x => x.ListarAsync(It.Is<Query<Amigo>>(it => it.Equals(queryAmigosUsuario))))
                  .Returns(Task.FromResult(mockAmigos.AsEnumerable()));

            //Act.
            await this._amigoBusiness.CriarAsync(usuario, amigo);
        }

        [TestMethod]
        public async Task Testar_CriarAsync_Valido()
        {
            //Arrange.
            string usuario = "yagohf";
            AmigoRegistrarDTO amigo = new AmigoRegistrarDTO()
            {
                Nome = "Leandro",
                Latitude = 20,
                Longitude = 30
            };

            //Mockar query de usuário por login.
            var queryUsuarioPorLogin = new Query<Usuario>();
            this._usuarioQueryMock.Setup(x => x.PorUsuario(usuario))
                  .Returns(queryUsuarioPorLogin);

            //Mockar usuário por login.
            var mockUsuario = new Usuario()
            {
                Id = 1,
                Login = usuario,
                Nome = "Yago"
            };

            this._usuarioRepositoryMock.Setup(x => x.SelecionarUnicoAsync(It.Is<Query<Usuario>>(it => it.Equals(queryUsuarioPorLogin))))
                  .Returns(Task.FromResult(mockUsuario));

            //Mockar query de listagem de amigos por usuário.
            var queryAmigosUsuario = new Query<Amigo>();
            this._amigoQueryMock.Setup(x => x.PorUsuario(usuario))
                  .Returns(queryAmigosUsuario);

            //Mockar lista de amigos por usuário.
            var mockAmigos = new List<Amigo>()
            {
                new Amigo() { Id = 1, Nome = "Mauri", Latitude= 15, Longitude = 25 },
                new Amigo() { Id = 2, Nome = "Kofazu", Latitude= 30, Longitude = 50 }
            };

            this._amigoRepositoryMock.Setup(x => x.ListarAsync(It.Is<Query<Amigo>>(it => it.Equals(queryAmigosUsuario))))
                  .Returns(Task.FromResult(mockAmigos.AsEnumerable()));

            //Mockar amigo criado.
            var mockAmigoCriado = new Amigo()
            {
                Id = 1,
                IdUsuario = mockUsuario.Id,
                Latitude = 20,
                Longitude = 30,
                Nome = "Leandro"
            };

            //Act.
            var amigoCriado = await this._amigoBusiness.CriarAsync(usuario, amigo);

            //Assert.
            Assert.IsNotNull(amigoCriado);
            Assert.AreEqual(mockAmigoCriado.Nome, amigoCriado.Nome);
            this._amigoRepositoryMock.Verify(rep => rep.InserirAsync(It.IsAny<Amigo>()), Times.Once);
        }
        #endregion

        #region [ Excluir ]
        [TestMethod]
        [ExpectedException(typeof(BusinessException))]
        public async Task Testar_ExcluirAsync_UsuarioSemPermissao()
        {
            //Arrange.
            string usuario = "yagohf";
            int idExcluir = 1;

            //Mockar query de listagem de amigos por usuário.
            var queryAmigosUsuario = new Query<Amigo>();
            this._amigoQueryMock.Setup(x => x.PorUsuario(usuario))
                  .Returns(queryAmigosUsuario);

            //Mockar lista de amigos do usuário, sem o usuário que se deseja excluir.
            var mockAmigos = new List<Amigo>()
            {
                new Amigo() { Id = 2, Nome = "Mauri", Latitude= 20, Longitude = 30 },
                new Amigo() { Id = 3, Nome = "Kofazu", Latitude= 30, Longitude = 50 }
            };
            this._amigoRepositoryMock.Setup(x => x.ListarAsync(It.Is<Query<Amigo>>(it => it.Equals(queryAmigosUsuario))))
                  .Returns(Task.FromResult(mockAmigos.AsEnumerable()));

            //Act.
            await this._amigoBusiness.ExcluirAsync(usuario, idExcluir);
        }

        [TestMethod]
        public async Task Testar_ExcluirAsync_Valido()
        {
            //Arrange.
            string usuario = "yagohf";
            int idExcluir = 1;

            //Mockar query de listagem de amigos por usuário.
            var queryAmigosUsuario = new Query<Amigo>();
            this._amigoQueryMock.Setup(x => x.PorUsuario(usuario))
                  .Returns(queryAmigosUsuario);

            //Mockar lista de amigos do usuário, com o usuário que se deseja excluir.
            var mockAmigos = new List<Amigo>()
            {
                new Amigo() { Id = 1, Nome = "Mauri", Latitude= 20, Longitude = 30 },
                new Amigo() { Id = 3, Nome = "Kofazu", Latitude= 30, Longitude = 50 }
            };
            this._amigoRepositoryMock.Setup(x => x.ListarAsync(It.Is<Query<Amigo>>(it => it.Equals(queryAmigosUsuario))))
                  .Returns(Task.FromResult(mockAmigos.AsEnumerable()));

            //Act.
            await this._amigoBusiness.ExcluirAsync(usuario, idExcluir);

            //Assert.
            this._amigoRepositoryMock.Verify(rep => rep.ExcluirAsync(idExcluir), Times.Once);
        }
        #endregion

        #region [ Listar amigos próximos ]
        [TestMethod]
        public async Task Testar_ListarAmigosProximosPorUsuarioAsync_SemAmigos()
        {
            //Arrange.
            string usuario = "yagohf";
            int idAmigoReferencia = 1;

            //Mockar query de listagem de amigos por usuário.
            var queryAmigosUsuario = new Query<Amigo>();
            this._amigoQueryMock.Setup(x => x.PorUsuario(usuario))
                  .Returns(queryAmigosUsuario);

            //Mockar lista de amigos do usuário, com o usuário que se deseja excluir.
            this._amigoRepositoryMock.Setup(x => x.ListarAsync(It.Is<Query<Amigo>>(it => it.Equals(queryAmigosUsuario))))
                  .Returns(Task.FromResult(Enumerable.Empty<Amigo>()));

            //Act.
            var amigosProximos = await this._amigoBusiness.ListarAmigosProximosPorUsuarioAsync(usuario, idAmigoReferencia);

            //Assert.
            Assert.IsNull(amigosProximos.Proximos);
        }

        [TestMethod]
        public async Task Testar_ListarAmigosProximosPorUsuarioAsync_ComMenosAmigosQueLimite()
        {
            //Arrange.
            string usuario = "yagohf";
            int idAmigoReferencia = 1;

            //Mockar configurações.
            this._parametrosMock.SetupGet(x => x.Value)
                .Returns(new Parametros() { QuantidadeAmigosProximosExibir = 3 });

            //Mockar query de listagem de amigos por usuário.
            var queryAmigosUsuario = new Query<Amigo>();
            this._amigoQueryMock.Setup(x => x.PorUsuario(usuario))
                  .Returns(queryAmigosUsuario);

            //Mockar lista de amigos do usuário, com o usuário que se deseja excluir.
            var mockAmigos = new List<Amigo>()
            {
                new Amigo() { Id = 1, Nome = "Mauri", Latitude= 20, Longitude = 30 },
                 new Amigo() { Id = 2, Nome = "Kofazu", Latitude= 30, Longitude = 50 },
                new Amigo() { Id = 3, Nome = "Kofazu", Latitude= 30, Longitude = 50 }
            };
            this._amigoRepositoryMock.Setup(x => x.ListarAsync(It.Is<Query<Amigo>>(it => it.Equals(queryAmigosUsuario))))
                  .Returns(Task.FromResult(mockAmigos.AsEnumerable()));

            //Act.
            var amigosProximos = await this._amigoBusiness.ListarAmigosProximosPorUsuarioAsync(usuario, idAmigoReferencia);

            //Assert.
            Assert.IsNotNull(amigosProximos.Proximos);
            Assert.AreEqual(2, amigosProximos.Proximos.Count());
        }

        [TestMethod]
        public async Task Testar_ListarAmigosProximosPorUsuarioAsync_ComMaisAmigosQueLimite()
        {
            //Arrange.
            string usuario = "yagohf";
            int idAmigoReferencia = 1;

            //Mockar configurações.
            this._parametrosMock.SetupGet(x => x.Value)
                .Returns(new Parametros() { QuantidadeAmigosProximosExibir = 3 });

            //Mockar query de listagem de amigos por usuário.
            var queryAmigosUsuario = new Query<Amigo>();
            this._amigoQueryMock.Setup(x => x.PorUsuario(usuario))
                  .Returns(queryAmigosUsuario);

            //Mockar lista de amigos do usuário, com o usuário que se deseja excluir.
            var mockAmigos = new List<Amigo>()
            {
                new Amigo() { Id = 1, Nome = "Mauri", Latitude= 20, Longitude = 30 },
                new Amigo() { Id = 2, Nome = "Kofazu", Latitude= 30, Longitude = 50 },
                new Amigo() { Id = 3, Nome = "Leandro", Latitude= 40, Longitude = 30 },
                new Amigo() { Id = 4, Nome = "Marcus", Latitude= 50, Longitude = 50 }
            };
            this._amigoRepositoryMock.Setup(x => x.ListarAsync(It.Is<Query<Amigo>>(it => it.Equals(queryAmigosUsuario))))
                  .Returns(Task.FromResult(mockAmigos.AsEnumerable()));

            //Act.
            var amigosProximos = await this._amigoBusiness.ListarAmigosProximosPorUsuarioAsync(usuario, idAmigoReferencia);

            //Assert.
            Assert.IsNotNull(amigosProximos.Proximos);
            Assert.AreEqual(3, amigosProximos.Proximos.Count());
        }
        #endregion

        #region [ Listar por usuário ]
        [TestMethod]
        public async Task Testar_ListarAsync_Paginando()
        {
            //Arrange.
            int pagina = 1;
            int registrosPorPagina = 10;

            var listaAmigos = new List<Amigo>();
            for (int i = 0; i < registrosPorPagina; i++)
            {
                listaAmigos.Add(new Amigo()
                {
                    Id = i,
                    Nome = $"Amigo {i}"
                });
            }

            var paginacao = new Paginacao(pagina, listaAmigos.Count, registrosPorPagina);
            var mockListaOriginal = new Listagem<Amigo>(listaAmigos, paginacao);
            var mockListaPaginada = new Listagem<AmigoDTO>(listaAmigos.Mapear<Amigo, AmigoDTO>(this._mapper), paginacao);

            this._amigoRepositoryMock
                .Setup(rep => rep.ListarPaginandoAsync(It.IsAny<IQuery<Amigo>>(), pagina, registrosPorPagina))
                .Returns(Task.FromResult(mockListaOriginal));

            //Act.
            var result = await this._amigoBusiness.ListarPorUsuarioAsync(null, pagina);

            //Assert.
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Lista);
            Assert.IsNotNull(result.Paginacao);
            CollectionAssert.AreEquivalent(mockListaOriginal.Lista.Select(x => x.Id).ToList(), result.Lista.Select(x => x.Id).ToList());
            Assert.AreEqual(mockListaPaginada.Lista.Count(), result.Lista.Count());
        }

        [TestMethod]
        public async Task Testar_ListarAsync_SemPaginar()
        {
            //Arrange.
            int registrosPorPagina = 10;
            var listaAmigos = new List<Amigo>();
            for (int i = 0; i < registrosPorPagina; i++)
            {
                listaAmigos.Add(new Amigo()
                {
                    Id = i,
                    Nome = $"Amigo {i}"
                });
            }

            var mockListaOriginal = new Listagem<Amigo>(listaAmigos);
            this._amigoRepositoryMock
                .Setup(rep => rep.ListarAsync(It.IsAny<IQuery<Amigo>>()))
                .Returns(Task.FromResult(listaAmigos as IEnumerable<Amigo>));

            //Act.
            var result = await this._amigoBusiness.ListarPorUsuarioAsync(null, null);

            //Assert.
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Lista);
            Assert.IsNull(result.Paginacao);
            Assert.AreEqual(mockListaOriginal.Lista.Count(), result.Lista.Count());
            CollectionAssert.AreEquivalent(mockListaOriginal.Lista.Select(x => x.Id).ToList(), result.Lista.Select(x => x.Id).ToList());
        }
        #endregion

        #region [ Selecionar por ID ]
        [TestMethod]
        public async Task Testar_SelecionarPorIdAsync()
        {
            //Arrange.
            int idAmigo = 1;
            Amigo mockAmigo = new Amigo()
            {
                Id = idAmigo,
                Nome = $"Amigo 1"
            };

            this._amigoRepositoryMock
                .Setup(rep => rep.SelecionarUnicoAsync(It.IsAny<IQuery<Amigo>>()))
                .Returns(Task.FromResult(mockAmigo));

            //Act.
            var result = await this._amigoBusiness.SelecionarPorIdAsync(idAmigo);

            //Assert.
            Assert.IsNotNull(result);
            Assert.AreEqual(mockAmigo.Id, result.Id);
        } 
        #endregion
    }
}
