using Yagohf.Cubo.FriendFinder.Model.DTO;

namespace Yagohf.Cubo.FriendFinder.Business.Interface.Helper
{
    public interface ITokenHelper
    {
        TokenDTO Gerar(string usuario, string nome);
    }
}
