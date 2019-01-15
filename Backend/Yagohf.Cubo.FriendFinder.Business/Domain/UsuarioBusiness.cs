using AutoMapper;
using System.Threading.Tasks;
using Yagohf.Cubo.FriendFinder.Business.Interface.Domain;
using Yagohf.Cubo.FriendFinder.Business.Interface.Helper;
using Yagohf.Cubo.FriendFinder.Data.Interface.Query;
using Yagohf.Cubo.FriendFinder.Data.Interface.Repository;
using Yagohf.Cubo.FriendFinder.Infrastructure.Exception;
using Yagohf.Cubo.FriendFinder.Model.DTO;
using Yagohf.Cubo.FriendFinder.Model.Entidades;

namespace Yagohf.Cubo.FriendFinder.Business.Domain
{
    public class UsuarioBusiness : IUsuarioBusiness
    {
        private readonly ITokenHelper _tokenHelper;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUsuarioQuery _usuarioQuery;
        private readonly IMapper _mapper;

        public UsuarioBusiness(ITokenHelper tokenHelper, IUsuarioRepository usuarioRepository, IUsuarioQuery usuarioQuery, IMapper mapper)
        {
            this._tokenHelper = tokenHelper;
            this._usuarioRepository = usuarioRepository;
            this._usuarioQuery = usuarioQuery;
            this._mapper = mapper;
        }

        public async Task<TokenDTO> GerarToken(AutenticacaoDTO login)
        {
            Usuario usuarioExistente = await this._usuarioRepository.SelecionarUnicoAsync(this._usuarioQuery.PorUsuarioSenha(login.Login, login.Senha));
            if (usuarioExistente == null)
                throw new BusinessException("Usuário ou senha inválidos");

            return this._tokenHelper.Gerar(usuarioExistente.Login, usuarioExistente.Nome);
        }

        public async Task<UsuarioDTO> RegistrarAsync(RegistroDTO registro)
        {
            Usuario usuario = this._mapper.Map<Usuario>(registro);
            await this._usuarioRepository.InserirAsync(usuario);
            return this._mapper.Map<UsuarioDTO>(usuario);
        }

        public async Task<UsuarioDTO> SelecionarPorIdAsync(int id)
        {
            Usuario usuario = await this._usuarioRepository.SelecionarUnicoAsync(this._usuarioQuery.PorId(id));
            return this._mapper.Map<UsuarioDTO>(usuario);
        }
    }
}
