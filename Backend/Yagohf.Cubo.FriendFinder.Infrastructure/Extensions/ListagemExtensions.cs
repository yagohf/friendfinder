using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using Yagohf.Cubo.FriendFinder.Infrastructure.Paging;

namespace Yagohf.Cubo.FriendFinder.Infrastructure.Extensions
{
    public static class ListagemExtensions
    {
        public static Listagem<TDestino> Mapear<TOrigem, TDestino>(this Listagem<TOrigem> listaOriginal, IMapper mapper)
            where TDestino : class
            where TOrigem : class
        {
            Listagem<TDestino> retorno = new Listagem<TDestino>(
                listaOriginal.Lista.Mapear<TOrigem, TDestino>(mapper),
                listaOriginal.Paginacao);

            return retorno;
        }

        public static IEnumerable<TDestino> Mapear<TOrigem, TDestino>(this IEnumerable<TOrigem> listaOriginal, IMapper mapper)
           where TDestino : class
           where TOrigem : class
        {
            return listaOriginal.Select(x => mapper.Map<TDestino>(x)).AsEnumerable();
        }
    }
}
