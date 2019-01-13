using AutoMapper;
using System;
using System.Threading.Tasks;
using Yagohf.Cubo.FriendFinder.Business.Interface.Domain;
using Yagohf.Cubo.FriendFinder.Data.Interface.Query;
using Yagohf.Cubo.FriendFinder.Data.Interface.Repository;
using Yagohf.Cubo.FriendFinder.Model.DTO;
using Yagohf.Cubo.FriendFinder.Model.Entidades;

namespace Yagohf.Cubo.FriendFinder.Business.Domain
{
    public class UsuarioBusiness : IUsuarioBusiness
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUsuarioQuery _usuarioQuery;
        private readonly IMapper _mapper;

        public UsuarioBusiness(IUsuarioRepository usuarioRepository, IUsuarioQuery usuarioQuery, IMapper mapper)
        {
            this._usuarioRepository = usuarioRepository;
            this._usuarioQuery = usuarioQuery;
            this._mapper = mapper;
        }

        public Task<UsuarioDTO> RegistrarAsync(RegistroDTO registro)
        {
            throw new NotImplementedException();
        }

        public async Task<UsuarioDTO> SelecionarPorIdAsync(int id)
        {
            Usuario usuario = await this._usuarioRepository.SelecionarUnicoAsync(this._usuarioQuery.PorId(id));
            return _mapper.Map<UsuarioDTO>(usuario);
        }
    }
}
