using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yagohf.Cubo.FriendFinder.Data.Context;
using Yagohf.Cubo.FriendFinder.Data.Interface.Query;
using Yagohf.Cubo.FriendFinder.Data.Query;
using Yagohf.Cubo.FriendFinder.Data.Repository;
using Yagohf.Cubo.FriendFinder.Model.Entidades;

namespace Yagohf.Cubo.FriendFinder.Tests.Data.Repository
{
    [TestClass]
    public class RepositoryBaseTests
    {
        private readonly FriendFinderContext _context;
        private readonly IAmigoQuery _amigoQuery;

        public RepositoryBaseTests()
        {
            var options = new DbContextOptionsBuilder<FriendFinderContext>()
            .UseInMemoryDatabase(databaseName: "REPOSITORY_BASE_TESTS")
            .Options;

            this._context = new FriendFinderContext(options);

            this._amigoQuery = new AmigoQuery();
        }

        [TestMethod]
        public async Task Testar_ContarAsync()
        {
            //Arrange
            RepositoryBase<Amigo> repositoryBase = new RepositoryBase<Amigo>(this._context);

            Amigo a = new Amigo()
            {
                Nome = "AMIGO_UNIT_TEST"
            };

            //Act
            await repositoryBase.InserirAsync(a);
            int contagem = await repositoryBase.ContarAsync(this._amigoQuery.PorId(a.Id));

            //Assert
            Assert.AreEqual(contagem, 1);
        }

        [TestMethod]
        public async Task Testar_AtualizarAsync()
        {
            //Arrange
            RepositoryBase<Amigo> repositoryBase = new RepositoryBase<Amigo>(this._context);
            string nomeAtualizar = "BATATA_UNIT_TEST";

            Amigo a = new Amigo()
            {
                Nome = "AMIGO_UNIT_TEST"
            };

            //Act
            await repositoryBase.InserirAsync(a);
            a.Nome = nomeAtualizar;
            await repositoryBase.AtualizarAsync(a);

            a = await repositoryBase.SelecionarUnicoAsync(this._amigoQuery.PorId(a.Id));

            //Assert
            Assert.AreEqual(nomeAtualizar, a.Nome);
        }

        [TestMethod]
        public async Task Testar_InserirAsync()
        {
            //Arrange
            RepositoryBase<Amigo> repositoryBase = new RepositoryBase<Amigo>(this._context);

            Amigo a = new Amigo()
            {
                Nome = "AMIGO_INSERIR_UNIT_TEST"
            };

            //Act
            await repositoryBase.InserirAsync(a);

            //Assert
            Assert.IsTrue(a.Id > 0);
        }

        [TestMethod]
        public async Task Testar_ExcluirAsync()
        {
            //Arrange
            RepositoryBase<Amigo> repositoryBase = new RepositoryBase<Amigo>(this._context);

            Amigo a = new Amigo()
            {
                Nome = "AMIGO_EXCLUIR_UNIT_TEST"
            };

            //Act
            await repositoryBase.InserirAsync(a);
            await repositoryBase.ExcluirAsync(a);
            var entidadeAposExclusao = await repositoryBase.SelecionarUnicoAsync(this._amigoQuery.PorId(a.Id));

            //Assert
            Assert.IsNull(entidadeAposExclusao);
        }

        [TestMethod]
        public async Task Testar_SelecionarUnicoAsync()
        {
            //Arrange
            RepositoryBase<Amigo> repositoryBase = new RepositoryBase<Amigo>(this._context);

            Amigo a = new Amigo()
            {
                Nome = "AMIGO_SELECIONARUNICO_UNIT_TEST"
            };

            //Act
            await repositoryBase.InserirAsync(a);
            var entidade = await repositoryBase.SelecionarUnicoAsync(this._amigoQuery.PorId(a.Id));

            //Assert
            Assert.IsNotNull(entidade);
            Assert.AreEqual(entidade.Id, a.Id);
        }

        [TestMethod]
        public async Task Testar_ListarAsync()
        {
            //Arrange
            RepositoryBase<Amigo> repositoryBase = new RepositoryBase<Amigo>(this._context);
            RepositoryBase<Usuario> repositoryBaseUsuario = new RepositoryBase<Usuario>(this._context);

            Usuario u = new Usuario()
            {
                Login = $"yagohf_{nameof(Testar_ListarAsync)}",
                Senha = "123mudar",
                Nome = "Yago"
            };

            await repositoryBaseUsuario.InserirAsync(u);

            List<Amigo> amigos = new List<Amigo>();
            for (int i = 0; i < 10; i++)
            {
                Amigo a = new Amigo()
                {
                    IdUsuario = u.Id,
                    Latitude = i,
                    Longitude = 1,
                    Nome = $"Amigo LISTAR_UNIT_TEST {i}"
                };

                amigos.Add(a);
            }

            //Act
            foreach (var a in amigos)
            {
                await repositoryBase.InserirAsync(a);
            }

            var resultado = await repositoryBase.ListarAsync(this._amigoQuery.PorUsuario(u.Login));

            //Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual(amigos.Count, resultado.Count());
        }

        [TestMethod]
        public async Task Testar_ListarAsync_Ordenacao()
        {
            //Arrange
            RepositoryBase<Amigo> repositoryBase = new RepositoryBase<Amigo>(this._context);
            Amigo a1 = new Amigo() { Nome = "AM_1_LISTAR_ORDENACAO_UNIT_TEST" };
            Amigo a2 = new Amigo() { Nome = "AM_2_LISTAR_ORDENACAO_UNIT_TEST" };
            Amigo a3 = new Amigo() { Nome = "AM_MESMO_NOME_LISTAR_ORDENACAO_UNIT_TEST" };
            Amigo a4 = new Amigo() { Nome = "AM_MESMO_NOME_LISTAR_ORDENACAO_UNIT_TEST" };

            //Act
            await repositoryBase.InserirAsync(a1);
            await repositoryBase.InserirAsync(a2);
            await repositoryBase.InserirAsync(a3);
            await repositoryBase.InserirAsync(a4);

            var resultado = (await repositoryBase.ListarAsync(
                new Query<Amigo>()
                .OrdenarPor(x => x.Nome)
                .OrdenarPorDescendente(x => x.Id))
                ).ToList();

            //Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual(a1.Id, resultado[0].Id);
            Assert.AreEqual(a2.Id, resultado[1].Id);
            Assert.AreEqual(a4.Id, resultado[2].Id); //A4 deve vir antes de A3 por conta da ordenação por ID na descendente como desempate.
            Assert.AreEqual(a3.Id, resultado[3].Id);
        }

        [TestMethod]
        public async Task Testar_ListarPaginandoAsync_Pagina1()
        {
            //Arrange
            RepositoryBase<Usuario> repositoryBaseUsuario = new RepositoryBase<Usuario>(this._context);

            Usuario u = new Usuario()
            {
                Login = $"yagohf_{nameof(Testar_ListarPaginandoAsync_Pagina1)}",
                Senha = "123mudar",
                Nome = "Yago"
            };

            await repositoryBaseUsuario.InserirAsync(u);

            int quantidadeRegistrosPorPagina = 10;
            int paginaPesquisar = 1;
            int totalRegistros = 50;

            RepositoryBase<Amigo> repositoryBase = new RepositoryBase<Amigo>(this._context);
            List<Amigo> amigos = new List<Amigo>();
            for (int i = 0; i < totalRegistros; i++)
            {
                Amigo a = new Amigo()
                {
                    Nome = $"Amigo LISTAR_PAGINANDO_PAGINA1_ASYNC_UNITTEST {i}",
                    IdUsuario = u.Id,
                    Latitude = i,
                    Longitude = i
                };

                amigos.Add(a);
            }

            //Act
            foreach (var a in amigos)
            {
                await repositoryBase.InserirAsync(a);
            }

            var resultado = await repositoryBase.ListarPaginandoAsync(this._amigoQuery.PorUsuario(u.Login), paginaPesquisar, quantidadeRegistrosPorPagina);

            //Assert
            Assert.IsNotNull(resultado);
            Assert.IsNotNull(resultado.Paginacao);
            Assert.IsNotNull(resultado.Lista);
            Assert.AreEqual(paginaPesquisar, resultado.Paginacao.PaginaAtual);
            Assert.AreEqual(quantidadeRegistrosPorPagina, resultado.Paginacao.QtdRegistrosPorPagina);
            Assert.AreEqual(totalRegistros, resultado.Paginacao.TotalRegistros);
            Assert.AreEqual(quantidadeRegistrosPorPagina, resultado.Lista.Count());
        }

        [TestMethod]
        public async Task Testar_ListarPaginandoAsync_Pagina5()
        {
            //Arrange
            RepositoryBase<Usuario> repositoryBaseUsuario = new RepositoryBase<Usuario>(this._context);

            Usuario u = new Usuario()
            {
                Login = $"yagohf_{nameof(Testar_ListarPaginandoAsync_Pagina5)}",
                Senha = "123mudar",
                Nome = "Yago"
            };

            await repositoryBaseUsuario.InserirAsync(u);

            int quantidadeRegistrosPorPagina = 10;
            int quantidadeRegistrosUltimaPagina = 9;
            int paginaPesquisar = 5;
            int totalRegistros = 49;

            RepositoryBase<Amigo> repositoryBase = new RepositoryBase<Amigo>(this._context);
            List<Amigo> amigos = new List<Amigo>();
            for (int i = 0; i < totalRegistros; i++)
            {
                Amigo a = new Amigo()
                {
                    Nome = $"Amigo LISTAR_PAGINANDO_PAGINA5_ASYNC_UNITTEST {i}",
                    IdUsuario = u.Id,
                    Latitude = i,
                    Longitude = i
                };

                amigos.Add(a);
            }

            //Act
            foreach (var a in amigos)
            {
                await repositoryBase.InserirAsync(a);
            }

            var resultado = await repositoryBase.ListarPaginandoAsync(this._amigoQuery.PorUsuario(u.Login), paginaPesquisar, quantidadeRegistrosPorPagina);

            //Assert
            Assert.IsNotNull(resultado);
            Assert.IsNotNull(resultado.Paginacao);
            Assert.IsNotNull(resultado.Lista);
            Assert.AreEqual(paginaPesquisar, resultado.Paginacao.PaginaAtual);
            Assert.AreEqual(quantidadeRegistrosPorPagina, resultado.Paginacao.QtdRegistrosPorPagina);
            Assert.AreEqual(totalRegistros, resultado.Paginacao.TotalRegistros);
            Assert.AreEqual(quantidadeRegistrosUltimaPagina, resultado.Lista.Count());
        }

        [TestMethod]
        public async Task Testar_ListarTodosAsync()
        {
            //Arrange
            RepositoryBase<Amigo> repositoryBase = new RepositoryBase<Amigo>(this._context);
            List<Amigo> amigos = new List<Amigo>();
            for (int i = 0; i < 10; i++)
            {
                Amigo a = new Amigo()
                {
                    Nome = $"Amigo LISTARTODOS_UNIT_TEST {i}"
                };

                amigos.Add(a);
            }

            //Act
            foreach (var a in amigos)
            {
                await repositoryBase.InserirAsync(a);
            }

            var registrosDiretamenteContext = this._context.Set<Amigo>().ToList();
            var resultado = await repositoryBase.ListarTodosAsync();

            //Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual(registrosDiretamenteContext.Count(), resultado.Count());
        }

        [TestMethod]
        public async Task Testar_ExisteAsync_Existe()
        {
            //Arrange
            RepositoryBase<Amigo> repositoryBase = new RepositoryBase<Amigo>(this._context);

            Amigo a = new Amigo()
            {
                Nome = "AMIGO_EXISTE_UNIT_TEST"
            };

            //Act
            await repositoryBase.InserirAsync(a);
            bool existe = await repositoryBase.ExisteAsync(this._amigoQuery.PorId(a.Id));

            //Assert
            Assert.IsTrue(existe);
        }

        [TestMethod]
        public async Task Testar_ExisteAsync_NaoExiste()
        {
            //Arrange
            RepositoryBase<Amigo> repositoryBase = new RepositoryBase<Amigo>(this._context);

            //Act
            bool existe = await repositoryBase.ExisteAsync(this._amigoQuery.PorId(0));

            //Assert
            Assert.IsFalse(existe);
        }
    }
}
