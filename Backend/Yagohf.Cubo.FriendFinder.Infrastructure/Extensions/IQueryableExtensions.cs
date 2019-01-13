using System.Linq;

namespace Yagohf.Cubo.FriendFinder.Infrastructure.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> PrepararQueryParaPaginar<T>(this IQueryable<T> queryable, int pagina, int qtdRegistrosPorPagina)
        {
            return queryable.Skip((pagina - 1) * qtdRegistrosPorPagina).Take(qtdRegistrosPorPagina);
        }
    }
}
