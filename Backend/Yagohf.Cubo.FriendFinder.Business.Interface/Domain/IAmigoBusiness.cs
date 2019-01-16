using System.Threading.Tasks;
using Yagohf.Cubo.FriendFinder.Infrastructure.Paging;
using Yagohf.Cubo.FriendFinder.Model.DTO;

namespace Yagohf.Cubo.FriendFinder.Business.Interface.Domain
{
    public interface IAmigoBusiness
    {
        Task<AmigoDTO> CriarAsync(string usuario, AmigoRegistrarDTO amigo);
        Task<AmigosProximosDTO> ListarAmigosProximosPorUsuarioAsync(string usuario, int amigoReferencia);
        Task<Listagem<AmigoDTO>> ListarPorUsuarioAsync(string usuario, int? pagina);
        Task<AmigoDTO> SelecionarPorIdAsync(int id);
        Task ExcluirAsync(int id);
    }
}
