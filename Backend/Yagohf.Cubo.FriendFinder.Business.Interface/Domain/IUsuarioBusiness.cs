using System.Threading.Tasks;
using Yagohf.Cubo.FriendFinder.Model.DTO;

namespace Yagohf.Cubo.FriendFinder.Business.Interface.Domain
{
    public interface IUsuarioBusiness
    {
        Task<UsuarioDTO> RegistrarAsync(RegistroDTO registro);
        Task<UsuarioDTO> SelecionarPorIdAsync(int id);
    }
}
