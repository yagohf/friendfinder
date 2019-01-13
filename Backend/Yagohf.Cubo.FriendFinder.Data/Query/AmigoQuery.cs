using Yagohf.Cubo.FriendFinder.Data.Interface.Query;
using Yagohf.Cubo.FriendFinder.Model.Entidades;

namespace Yagohf.Cubo.FriendFinder.Data.Query
{
    public class AmigoQuery : IAmigoQuery
    {
        public IQuery<Amigo> PorUsuario(string usuario)
        {
            return new Query<Amigo>()
                .AdicionarInclude(x => x.Usuario)
                .Filtrar(x => x.Usuario.Login == usuario)
                .OrdenarPor(x => x.Nome);
        }
    }
}
