using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Threading.Tasks;
using Yagohf.Cubo.FriendFinder.Business.Interface.Domain;
using Yagohf.Cubo.FriendFinder.Model.DTO;

namespace Yagohf.Cubo.FriendFinder.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize]
    public class UsuariosController : Controller
    {
        private readonly IUsuarioBusiness _usuarioBusiness;

        public UsuariosController(IUsuarioBusiness usuarioBusiness)
        {
            this._usuarioBusiness = usuarioBusiness;
        }

        /// <summary>
        /// Consulta os detalhes de um usuário específico.
        /// </summary>
        /// <param name="id">Identificador único do usuário.</param>
        [HttpGet("{id}")]
        [SwaggerResponse(200, typeof(UsuarioDTO))]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await this._usuarioBusiness.SelecionarPorIdAsync(id));
        }

        /// <summary>
        /// Registra um usuário.
        /// </summary>
        /// <param name="model">Dados do novo usuário.</param>
        [AllowAnonymous]
        [HttpPost]
        [SwaggerResponse(201, typeof(UsuarioDTO))]
        public async Task<IActionResult> Post([FromBody]RegistroDTO model)
        {
            UsuarioDTO usuarioCriado = await this._usuarioBusiness.RegistrarAsync(model);
            return CreatedAtAction(nameof(Get), new { id = usuarioCriado.Id }, usuarioCriado);
        }

        /// <summary>
        /// Obtém um token a partir de credenciais válidas de autenticação.
        /// </summary>
        /// <param name="model">Credenciais para autenticação.</param>
        [AllowAnonymous]
        [HttpPost("token")]
        [SwaggerResponse(201, typeof(UsuarioDTO))]
        public async Task<IActionResult> PostAutenticacao([FromBody]AutenticacaoDTO model)
        {
            TokenDTO token = await this._usuarioBusiness.GerarToken(model);
            return Ok(token);
        }
    }
}
