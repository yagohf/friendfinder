using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace Yagohf.Cubo.FriendFinder.Api.Infrastructure.Extensions
{
    public static class ControllerExtensions
    {
        public static string ObterUsuarioLogado(this Controller controller)
        {
            return controller.HttpContext.User.Claims.Single(c => c.Type.Equals(ClaimTypes.Name)).Value;
        }
    }
}
