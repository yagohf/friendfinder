using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Yagohf.Cubo.FriendFinder.Business.Interface.Helper;
using Yagohf.Cubo.FriendFinder.Data.Interface.Query;
using Yagohf.Cubo.FriendFinder.Data.Interface.Repository;
using Yagohf.Cubo.FriendFinder.Model.DTO;
using Yagohf.Cubo.FriendFinder.Model.Entidades;

namespace Yagohf.Cubo.FriendFinder.Business.Helper
{
    public class CalculoHistoricoLogHelper : ICalculoHistoricoLogHelper
    {
        private readonly ICalculoHistoricoLogRepository _calculoHistoricoLogRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUsuarioQuery _usuarioQuery;

        public CalculoHistoricoLogHelper(ICalculoHistoricoLogRepository calculoHistoricoLogRepository, IUsuarioRepository usuarioRepository, IUsuarioQuery usuarioQuery)
        {
            this._calculoHistoricoLogRepository = calculoHistoricoLogRepository;
            this._usuarioRepository = usuarioRepository;
            this._usuarioQuery = usuarioQuery;
        }

        public async Task Logar(string usuario, AmigosProximosDTO resultado)
        {
            Usuario u = await this._usuarioRepository.SelecionarUnicoAsync(this._usuarioQuery.PorUsuario(usuario));

            CalculoHistoricoLog log = new CalculoHistoricoLog()
            {
                IdUsuario = u.Id,
                DataOcorrencia = DateTime.Now,
                Resultado = JsonConvert.SerializeObject(resultado)
            };

            await this._calculoHistoricoLogRepository.InserirAsync(log);
        }
    }
}
