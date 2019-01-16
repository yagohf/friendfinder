using System.Threading.Tasks;
using Yagohf.Cubo.FriendFinder.Model.DTO;

namespace Yagohf.Cubo.FriendFinder.Business.Interface.Helper
{
    public interface ICalculoHistoricoLogHelper
    {
        Task Logar(string usuario, AmigosProximosDTO resultado);
    }
}
