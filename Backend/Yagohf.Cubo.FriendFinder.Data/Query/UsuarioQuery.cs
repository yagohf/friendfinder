using Yagohf.Cubo.FriendFinder.Data.Interface.Query;
using Yagohf.Cubo.FriendFinder.Model.Entidades;

namespace Yagohf.Cubo.FriendFinder.Data.Query
{
    public class UsuarioQuery : IUsuarioQuery
    {
        public IQuery<Usuario> PorId(int id)
        {
            return new Query<Usuario>()
                 .Filtrar(x => x.Id == id);
        }

        public IQuery<Usuario> PorUsuarioSenha(string usuario, string senha)
        {
            return new Query<Usuario>()
                .Filtrar(x => x.Login == usuario && x.Senha == senha);
        }
    }
}
