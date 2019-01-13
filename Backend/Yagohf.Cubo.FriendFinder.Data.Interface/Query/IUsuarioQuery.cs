using Yagohf.Cubo.FriendFinder.Model.Entidades;

namespace Yagohf.Cubo.FriendFinder.Data.Interface.Query
{
    public interface IUsuarioQuery
    {
        IQuery<Usuario> PorId(int id);
        IQuery<Usuario> PorUsuarioSenha(string usuario, string senha);
    }
}
