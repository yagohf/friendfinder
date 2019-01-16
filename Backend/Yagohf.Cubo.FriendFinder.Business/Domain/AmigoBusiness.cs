using AutoMapper;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yagohf.Cubo.FriendFinder.Business.Interface.Domain;
using Yagohf.Cubo.FriendFinder.Business.Interface.Helper;
using Yagohf.Cubo.FriendFinder.Data.Interface.Query;
using Yagohf.Cubo.FriendFinder.Data.Interface.Repository;
using Yagohf.Cubo.FriendFinder.Infrastructure.Configuration;
using Yagohf.Cubo.FriendFinder.Infrastructure.Extensions;
using Yagohf.Cubo.FriendFinder.Infrastructure.Paging;
using Yagohf.Cubo.FriendFinder.Model.DTO;
using Yagohf.Cubo.FriendFinder.Model.Entidades;

namespace Yagohf.Cubo.FriendFinder.Business.Domain
{
    public class AmigoBusiness : IAmigoBusiness
    {
        private readonly ICalculadoraDistanciaPontosBusiness _calculadoraDistanciaPontosBusiness;
        private readonly ICalculoHistoricoLogHelper _calculoHistoricoLogHelper;
        private readonly IAmigoRepository _amigoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IAmigoQuery _amigoQuery;
        private readonly IUsuarioQuery _usuarioQuery;
        private readonly IOptions<Parametros> _parametros;
        private readonly IMapper _mapper;

        public AmigoBusiness(ICalculadoraDistanciaPontosBusiness calculadoraDistanciaPontosBusiness, ICalculoHistoricoLogHelper calculoHistoricoLogHelper, IAmigoRepository amigoRepository, IUsuarioRepository usuarioRepository, IAmigoQuery amigoQuery, IUsuarioQuery usuarioQuery, IOptions<Parametros> parametros, IMapper mapper)
        {
            this._calculadoraDistanciaPontosBusiness = calculadoraDistanciaPontosBusiness;
            this._calculoHistoricoLogHelper = calculoHistoricoLogHelper;
            this._amigoRepository = amigoRepository;
            this._usuarioRepository = usuarioRepository;
            this._amigoQuery = amigoQuery;
            this._usuarioQuery = usuarioQuery;
            this._parametros = parametros;
            this._mapper = mapper;
        }

        public async Task<AmigoDTO> CriarAsync(string usuario, AmigoRegistrarDTO amigo)
        {
            Usuario usuarioRelacionar = await this._usuarioRepository.SelecionarUnicoAsync(this._usuarioQuery.PorUsuario(usuario));
            Amigo amigoCriar = this._mapper.Map<Amigo>(amigo);
            amigoCriar.IdUsuario = usuarioRelacionar.Id;
            await this._amigoRepository.InserirAsync(amigoCriar);
            return this._mapper.Map<AmigoDTO>(amigoCriar);
        }

        public async Task ExcluirAsync(int id)
        {
            await this._amigoRepository.ExcluirAsync(id);
        }

        public async Task<AmigosProximosDTO> ListarAmigosProximosPorUsuarioAsync(string usuario, int amigoReferencia)
        {
            IEnumerable<Amigo> amigos = await this._amigoRepository.ListarAsync(this._amigoQuery.PorUsuario(usuario));
            Amigo amigoAtual = amigos.SingleOrDefault(x => x.Id == amigoReferencia);
            IEnumerable<Amigo> amigosMaisProximos = null;

            if (amigoAtual != null && amigos.Any(x => x.Id != amigoAtual.Id))
            {
                amigosMaisProximos = amigos.Where(x => x.Id != amigoAtual.Id)
                    .OrderBy(x => this._calculadoraDistanciaPontosBusiness.Calcular(new PontoDTO(x.Latitude, x.Longitude), new PontoDTO(amigoAtual.Latitude, amigoAtual.Longitude)))
                    .Take(this._parametros.Value.QuantidadeAmigosProximosExibir);
            }

            AmigosProximosDTO resultado = new AmigosProximosDTO()
            {
                Atual = amigoAtual == null ? null : this._mapper.Map<AmigoDTO>(amigoAtual),
                Proximos = amigosMaisProximos?.Mapear<Amigo, AmigoDTO>(this._mapper)
            };

            await this._calculoHistoricoLogHelper.Logar(usuario, resultado);
            return resultado;
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

        public async Task<AmigoDTO> SelecionarPorIdAsync(int id)
        {
            Amigo amigo = await this._amigoRepository.SelecionarUnicoAsync(this._amigoQuery.PorId(id));
            return this._mapper.Map<AmigoDTO>(amigo);
        }
    }
}
