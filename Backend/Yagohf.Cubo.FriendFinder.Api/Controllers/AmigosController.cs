using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Threading.Tasks;
using Yagohf.Cubo.FriendFinder.Business.Interface.Domain;
using Yagohf.Cubo.FriendFinder.Infrastructure.Paging;
using Yagohf.Cubo.FriendFinder.Model.DTO;

namespace Yagohf.Cubo.FriendFinder.Api.Controllers
{
    [Route("api/v1/[controller]")]
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
            //TODO - recuperar usuário logado.
            string usuario = "yagohf";
            return Ok(await this._amigoBusiness.ListarPorUsuarioAsync(usuario, pagina));
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
            //TODO - recuperar usuário logado.
            string usuario = "yagohf";
            return Ok(await this._amigoBusiness.ListarAmigosProximosPorUsuarioAsync(usuario, amigo));
        }
    }
}
