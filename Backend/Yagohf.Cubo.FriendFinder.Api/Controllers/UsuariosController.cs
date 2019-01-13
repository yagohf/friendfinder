using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Threading.Tasks;
using Yagohf.Cubo.FriendFinder.Business.Interface.Domain;
using Yagohf.Cubo.FriendFinder.Model.DTO;

namespace Yagohf.Cubo.FriendFinder.Api.Controllers
{
    [Route("api/v1/[controller]")]
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
        [HttpPost]
        [SwaggerResponse(201, typeof(UsuarioDTO))]
        public async Task<IActionResult> Post([FromBody]RegistroDTO model)
        {
            UsuarioDTO usuarioCriado = await this._usuarioBusiness.RegistrarAsync(model);
            return CreatedAtAction(nameof(Get), new { id = usuarioCriado.Id }, usuarioCriado);
        }
    }
}
