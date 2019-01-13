using Yagohf.Cubo.FriendFinder.Model.DTO;

namespace Yagohf.Cubo.FriendFinder.Business.Interface.Domain
{
    public interface ICalculadoraDistanciaPontosBusiness
    {
        double Calcular(PontoDTO a, PontoDTO b);
    }
}
