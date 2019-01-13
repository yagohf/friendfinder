namespace Yagohf.Cubo.FriendFinder.Model.DTO
{
    public class PontoDTO
    {
        private readonly double _x;
        private readonly double _y;

        public PontoDTO(decimal x, decimal y)
        {
            this._x = (double)x;
            this._y = (double)y;
        }

        public double X { get { return this._x; } }
        public double Y { get { return this._y; } }
    }
}
