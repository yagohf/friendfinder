using Yagohf.Cubo.FriendFinder.Data.Context;
using Yagohf.Cubo.FriendFinder.Data.Interface.Repository;
using Yagohf.Cubo.FriendFinder.Model.Entidades;

namespace Yagohf.Cubo.FriendFinder.Data.Repository
{
    public class AmigoRepository : RepositoryBase<Amigo>, IAmigoRepository
    {
        public AmigoRepository(FriendFinderContext context) : base(context)
        {
        }
    }
}
