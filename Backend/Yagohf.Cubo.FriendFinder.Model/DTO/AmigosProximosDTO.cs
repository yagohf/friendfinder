using System.Collections.Generic;

namespace Yagohf.Cubo.FriendFinder.Model.DTO
{
    public class AmigosProximosDTO
    {
        public AmigoDTO Atual { get; set; }
        public IEnumerable<AmigoDTO> Proximos { get; set; }
    }
}
