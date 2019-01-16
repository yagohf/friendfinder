using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using Yagohf.Cubo.FriendFinder.Business.Domain;
using Yagohf.Cubo.FriendFinder.Business.Interface.Helper;
using Yagohf.Cubo.FriendFinder.Business.MapperProfile;
using Yagohf.Cubo.FriendFinder.Data.Interface.Query;
using Yagohf.Cubo.FriendFinder.Data.Interface.Repository;
using Yagohf.Cubo.FriendFinder.Data.Query;
using Yagohf.Cubo.FriendFinder.Infrastructure.Exception;
using Yagohf.Cubo.FriendFinder.Model.DTO;
using Yagohf.Cubo.FriendFinder.Model.Entidades;

namespace Yagohf.Cubo.FriendFinder.Tests.Business
{
    [TestClass]
    public class UsuarioBusinessTests
    {
        private readonly Mock<ITokenHelper> _tokenHelperMock;
        private readonly Mock<IUsuarioRepository> _usuarioRepositoryMock;
        private readonly Mock<IUsuarioQuery> _usuarioQueryMock;
        private readonly IMapper _mapper;
        private UsuarioBusiness _usuarioBusiness;

        public UsuarioBusinessTests()
        {
            this._tokenHelperMock = new Mock<ITokenHelper>();
            this._usuarioRepositoryMock = new Mock<IUsuarioRepository>();
            this._usuarioQueryMock = new Mock<IUsuarioQuery>();

            MapperConfiguration mapperConfiguration = new MapperConfiguration(mConfig =>
            {
                mConfig.AddProfile(new BusinessMapperProfile());
            });

            this._mapper = mapperConfiguration.CreateMapper();
        }

        [TestInitialize]
        public void Inicializar()
        {
            this._usuarioBusiness = new UsuarioBusiness(
                this._tokenHelperMock.Object,
                this._usuarioRepositoryMock.Object,
                this._usuarioQueryMock.Object,
                this._mapper
                );
        }

        #region [ Gerar token ]
        [TestMethod]
        [ExpectedException(typeof(BusinessException))]
        public async Task Testar_GerarTokenAsync_SemInformarDados()
        {
            //Arrange.
            AutenticacaoDTO autenticacao = new AutenticacaoDTO()
            {
                Login = "",
                Senha  = ""
            };

            //Act.
            var token = await this._usuarioBusiness.GerarTokenAsync(autenticacao);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException))]
        public async Task Testar_GerarTokenAsync_SenhaIncorreta()
        {
            //Arrange.
            AutenticacaoDTO autenticacao = new AutenticacaoDTO()
            {
                Login = "yagohf",
                Senha = "123mudar"
            };

            //Mockar query de usuário por login e senha.
            var queryUsuarioPorLoginSenha = new Query<Usuario>();
            this._usuarioQueryMock.Setup(x => x.PorUsuarioSenha(autenticacao.Login, autenticacao.Senha))
                  .Returns(queryUsuarioPorLoginSenha);

            this._usuarioRepositoryMock.Setup(x => x.SelecionarUnicoAsync(It.Is<Query<Usuario>>(it => it.Equals(queryUsuarioPorLoginSenha))))
                  .Returns(Task.FromResult<Usuario>(null));

            //Act.
            var token = await this._usuarioBusiness.GerarTokenAsync(autenticacao);
        }

        [TestMethod]
        public async Task Testar_GerarTokenAsync_Valido()
        {
            //Arrange.
            AutenticacaoDTO autenticacao = new AutenticacaoDTO()
            {
                Login = "yagohf",
                Senha = "123mudar"
            };

            //Mockar query de usuário por login e senha.
            var queryUsuarioPorLoginSenha = new Query<Usuario>();
            this._usuarioQueryMock.Setup(x => x.PorUsuarioSenha(autenticacao.Login, autenticacao.Senha))
                  .Returns(queryUsuarioPorLoginSenha);

            //Mockar usuário por login e senha.
            var mockUsuario = new Usuario()
            {
                Id = 1,
                Login = autenticacao.Login,
                Nome = "Yago"
            };

            this._usuarioRepositoryMock.Setup(x => x.SelecionarUnicoAsync(It.Is<Query<Usuario>>(it => it.Equals(queryUsuarioPorLoginSenha))))
                  .Returns(Task.FromResult(mockUsuario));

            //Mockar gerador de token.
            var mockToken = new TokenDTO()
            {
                Login = mockUsuario.Login,
                Nome = mockUsuario.Nome,
                Token = "123456"
            };
            this._tokenHelperMock.Setup(x => x.Gerar(mockUsuario.Login, mockUsuario.Nome))
                .Returns(mockToken);

            //Act.
            var token = await this._usuarioBusiness.GerarTokenAsync(autenticacao);

            //Assert.
            Assert.IsNotNull(token);
            Assert.IsNotNull(token.Token);
            Assert.AreEqual(autenticacao.Login, token.Login);
        }
        #endregion

        #region [ Registrar ]
        [TestMethod]
        [ExpectedException(typeof(BusinessException))]
        public async Task Testar_RegistrarAsync_SemInformarDados()
        {
            //Arrange.
            RegistroDTO registro = new RegistroDTO()
            {
                Login = "",
                Senha = "",
                Nome = ""
            };

            //Act.
            var usuarioCriado = await this._usuarioBusiness.RegistrarAsync(registro);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException))]
        public async Task Testar_RegistrarAsync_UsuarioEmUso()
        {
            //Arrange.
            RegistroDTO registro = new RegistroDTO()
            {
                Login = "yagohf",
                Senha = "123mudar",
                Nome = "Yago"
            };

            //Mockar query de usuário existente por login.
            var queryUsuarioPorLogin = new Query<Usuario>();
            this._usuarioQueryMock.Setup(x => x.PorUsuario(registro.Login))
                  .Returns(queryUsuarioPorLogin);

            this._usuarioRepositoryMock.Setup(x => x.ExisteAsync(It.Is<Query<Usuario>>(it => it.Equals(queryUsuarioPorLogin))))
                  .Returns(Task.FromResult(true));

            //Act.
            var usuarioCriado = await this._usuarioBusiness.RegistrarAsync(registro);
        }

        [TestMethod]
        public async Task Testar_RegistrarAsync_Valido()
        {
            //Arrange.
            RegistroDTO registro = new RegistroDTO()
            {
                Login = "yagohf",
                Senha = "123mudar",
                Nome = "Yago"
            };

            //Mockar query de usuário existente por login.
            var queryUsuarioPorLogin = new Query<Usuario>();
            this._usuarioQueryMock.Setup(x => x.PorUsuario(registro.Login))
                  .Returns(queryUsuarioPorLogin);

            this._usuarioRepositoryMock.Setup(x => x.ExisteAsync(It.Is<Query<Usuario>>(it => it.Equals(queryUsuarioPorLogin))))
                  .Returns(Task.FromResult(false));

            //Mockar usuário criado.
            var mockUsuarioCriado = new Usuario()
            {
                Id = 1,
                Login = "yagohf",
                Nome = "Yago"
            };

            //Act.
            var usuarioCriado = await this._usuarioBusiness.RegistrarAsync(registro);

            //Assert.
            Assert.IsNotNull(usuarioCriado);
            Assert.AreEqual(mockUsuarioCriado.Nome, usuarioCriado.Nome);
            this._usuarioRepositoryMock.Verify(rep => rep.InserirAsync(It.IsAny<Usuario>()), Times.Once);
        }
        #endregion

        #region [ Selecionar por ID ]
        [TestMethod]
        public async Task Testar_SelecionarPorIdAsync()
        {
            //Arrange.
            int idUsuario = 1;
            Usuario mockUsuario = new Usuario()
            {
                Id = idUsuario,
                Nome = $"Usuario 1",
                Login = "yagohf"
            };

            this._usuarioRepositoryMock
                .Setup(rep => rep.SelecionarUnicoAsync(It.IsAny<IQuery<Usuario>>()))
                .Returns(Task.FromResult(mockUsuario));

            //Act.
            var result = await this._usuarioBusiness.SelecionarPorIdAsync(idUsuario);

            //Assert.
            Assert.IsNotNull(result);
            Assert.AreEqual(mockUsuario.Id, result.Id);
        }
        #endregion
    }
}
