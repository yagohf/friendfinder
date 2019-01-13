using AutoMapper;
using System;
using System.Threading.Tasks;
using Yagohf.Cubo.FriendFinder.Business.Interface.Domain;
using Yagohf.Cubo.FriendFinder.Data.Interface.Query;
using Yagohf.Cubo.FriendFinder.Data.Interface.Repository;
using Yagohf.Cubo.FriendFinder.Infrastructure.Extensions;
using Yagohf.Cubo.FriendFinder.Infrastructure.Paging;
using Yagohf.Cubo.FriendFinder.Model.DTO;
using Yagohf.Cubo.FriendFinder.Model.Entidades;

namespace Yagohf.Cubo.FriendFinder.Business.Domain
{
    public class AmigoBusiness : IAmigoBusiness
    {
        private readonly IAmigoRepository _amigoRepository;
        private readonly IAmigoQuery _amigoQuery;
        private readonly IMapper _mapper;

        public AmigoBusiness(IAmigoRepository amigoRepository, IAmigoQuery amigoQuery, IMapper mapper)
        {
            this._amigoRepository = amigoRepository;
            this._amigoQuery = amigoQuery;
            this._mapper = mapper;
        }

        public Task<AmigosProximosDTO> ListarAmigosProximosPorUsuarioAsync(string usuario, int amigoReferencia)
        {
            throw new NotImplementedException();
        }

        public async Task<Listagem<AmigoDTO>> ListarPorUsuarioAsync(string usuario, int? pagina)
        {
            Listagem<Amigo> listagem;
            if (pagina.HasValue)
            {
                listagem = await this._amigoRepository.ListarPaginandoAsync(this._amigoQuery.PorUsuario(usuario), pagina.Value, 10);
            }
            else
            {
                listagem = new Listagem<Amigo>(await this._amigoRepository.ListarAsync(this._amigoQuery.PorUsuario(usuario)));
            }

            return listagem.Mapear<Amigo, AmigoDTO>(this._mapper);
        }
    }
}
