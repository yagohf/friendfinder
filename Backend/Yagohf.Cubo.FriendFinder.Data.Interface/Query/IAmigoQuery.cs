using Yagohf.Cubo.FriendFinder.Model.Entidades;

namespace Yagohf.Cubo.FriendFinder.Data.Interface.Query
{
    public interface IAmigoQuery
    {
        IQuery<Amigo> PorId(int id);
        IQuery<Amigo> PorUsuario(string usuario);
    }
}
