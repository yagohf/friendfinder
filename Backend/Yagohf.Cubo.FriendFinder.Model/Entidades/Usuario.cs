using System.Collections.Generic;

namespace Yagohf.Cubo.FriendFinder.Model.Entidades
{
    public class Usuario : EntidadeBase
    {
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }

        //Relacionamentos.
        public ICollection<Amigo> Amigos { get; set; }
    }
}
