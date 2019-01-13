namespace Yagohf.Cubo.FriendFinder.Model.Entidades
{
    public class Amigo : EntidadeBase
    {
        public int IdUsuario { get; set; }
        public string Nome { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        //Relacionamentos.
        public Usuario Usuario { get; set; }
    }
}
