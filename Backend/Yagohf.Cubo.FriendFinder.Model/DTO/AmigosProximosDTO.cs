using System.Collections.Generic;

namespace Yagohf.Cubo.FriendFinder.Model.DTO
{
    public class AmigosProximosDTO
    {
        public AmigoDTO PosicaoAtual { get; set; }
        public IEnumerable<AmigoDTO> AmigosProximos { get; set; }
    }
}
