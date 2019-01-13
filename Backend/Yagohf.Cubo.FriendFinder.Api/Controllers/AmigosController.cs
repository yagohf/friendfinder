using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Threading.Tasks;
using Yagohf.Cubo.FriendFinder.Api.Infrastructure.Extensions;
using Yagohf.Cubo.FriendFinder.Business.Interface.Domain;
using Yagohf.Cubo.FriendFinder.Infrastructure.Paging;
using Yagohf.Cubo.FriendFinder.Model.DTO;

namespace Yagohf.Cubo.FriendFinder.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize]
    public class AmigosController : Controller
    {
        private readonly IAmigoBusiness _amigoBusiness;

        public AmigosController(IAmigoBusiness amigoBusiness)
        {
            this._amigoBusiness = amigoBusiness;
        }

        /// <summary>
        /// Consulta os amigos do usuário logado. Permite paginação.
        /// </summary>
        /// <param name="pagina">Página da listagem a ser exibida.</param>
        [HttpGet]
        [SwaggerResponse(200, typeof(Listagem<AmigoDTO>))]
        [SwaggerResponse(401)]
        public async Task<IActionResult> Get(int? pagina)
        {
            return Ok(await this._amigoBusiness.ListarPorUsuarioAsync(this.ObterUsuarioLogado(), pagina));
        }

        /// <summary>
        /// Consulta um amigo através de seu identificador único.
        /// </summary>
        /// <param name="id">Identificador único do amigo.</param>
        [HttpGet("{id}")]
        [SwaggerResponse(200, typeof(Listagem<AmigoDTO>))]
        [SwaggerResponse(401)]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await this._amigoBusiness.SelecionarPorIdAsync(id));
        }

        /// <summary>
        /// Consulta os amigos mais próximos da localização do amigo em que o usuário logado se encontra.
        /// </summary>
        /// <param name="amigo">Identificador único do amigo em que o usuário logado se encontra.</param>
        [HttpGet("{amigo}/proximos")]
        [SwaggerResponse(200, typeof(AmigosProximosDTO))]
        [SwaggerResponse(401)]
        public async Task<IActionResult> GetAmigosProximos(int amigo)
        {
            return Ok(await this._amigoBusiness.ListarAmigosProximosPorUsuarioAsync(this.ObterUsuarioLogado(), amigo));
        }

        /// <summary>
        /// Cria um amigo relacionado ao usuário logado.
        /// </summary>
        /// <param name="model">Dados do novo usuário.</param>
        [AllowAnonymous]
        [HttpPost]
        [SwaggerResponse(201, typeof(UsuarioDTO))]
        public async Task<IActionResult> Post([FromBody]AmigoRegistrarDTO model)
        {
            AmigoDTO amigoCriado = await this._amigoBusiness.CriarAsync(this.ObterUsuarioLogado(), model);
            return CreatedAtAction(nameof(Get), new { id = amigoCriado.Id }, amigoCriado);
        }
    }
}
