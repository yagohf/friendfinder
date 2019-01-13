using System;
using Yagohf.Cubo.FriendFinder.Business.Interface.Domain;
using Yagohf.Cubo.FriendFinder.Model.DTO;

namespace Yagohf.Cubo.FriendFinder.Business.Domain
{
    public class CalculadoraDistanciaPontosBusiness : ICalculadoraDistanciaPontosBusiness
    {
        public double Calcular(PontoDTO a, PontoDTO b)
        {
            return Math.Sqrt(Math.Pow(b.X - a.X, 2) + Math.Pow(b.Y - a.Y, 2));
        }
    }
}
