using System;

namespace Yagohf.Cubo.FriendFinder.Model.Entidades
{
    public class CalculoHistoricoLog : EntidadeBase
    {
        public int IdUsuario { get; set; }
        public DateTime DataOcorrencia { get; set; }
        public string Resultado { get; set; }

        //Relacionamentos.
        public Usuario Usuario { get; set; }
    }
}
