using System.Collections.Generic;
using System.Threading.Tasks;
using Yagohf.Cubo.FriendFinder.Data.Interface.Query;
using Yagohf.Cubo.FriendFinder.Infrastructure.Paging;

namespace Yagohf.Cubo.FriendFinder.Data.Interface.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<T> SelecionarUnicoAsync(IQuery<T> query);
        Task<IEnumerable<T>> ListarTodosAsync();
        Task<IEnumerable<T>> ListarAsync(IQuery<T> query);
        Task<Listagem<T>> ListarPaginandoAsync(IQuery<T> query, int pagina, int qtdRegistrosPorPagina);
        Task<int> ContarAsync(IQuery<T> query);
        Task InserirAsync(T entidade);
        Task AtualizarAsync(T entidade);
        Task ExcluirAsync(int id);
        Task ExcluirAsync(T entidade);
        Task<bool> ExisteAsync(IQuery<T> query);
    }
}
