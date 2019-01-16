using AutoMapper;
using System.Threading.Tasks;
using Yagohf.Cubo.FriendFinder.Business.Interface.Domain;
using Yagohf.Cubo.FriendFinder.Business.Interface.Helper;
using Yagohf.Cubo.FriendFinder.Data.Interface.Query;
using Yagohf.Cubo.FriendFinder.Data.Interface.Repository;
using Yagohf.Cubo.FriendFinder.Infrastructure.Exception;
using Yagohf.Cubo.FriendFinder.Infrastructure.Extensions;
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

        public async Task<TokenDTO> GerarTokenAsync(AutenticacaoDTO login)
        {
            if (string.IsNullOrEmpty(login.Login) || string.IsNullOrEmpty(login.Senha))
                throw new BusinessException("Usuário ou senha inválidos");

            Usuario usuario = await this._usuarioRepository.SelecionarUnicoAsync(this._usuarioQuery.PorUsuarioSenha(login.Login, login.Senha));
            if (usuario == null)
                throw new BusinessException("Usuário ou senha inválidos");

            return this._tokenHelper.Gerar(usuario.Login, usuario.Nome);
        }

        public async Task<UsuarioDTO> RegistrarAsync(RegistroDTO registro)
        {
            Usuario novoUsuario = this._mapper.Map<Usuario>(registro);

            if (string.IsNullOrEmpty(registro.Login) || string.IsNullOrEmpty(registro.Senha) || string.IsNullOrEmpty(registro.Nome))
                throw new BusinessException("Dados incompletos para registrar o usuário");
            else if (await this._usuarioRepository.ExisteAsync(this._usuarioQuery.PorUsuario(registro.Login)))
                throw new BusinessException("Esse nome de usuário não está disponível para registro");

            novoUsuario.Senha = novoUsuario.Senha.ToCipherText();
            await this._usuarioRepository.InserirAsync(novoUsuario);
            return this._mapper.Map<UsuarioDTO>(novoUsuario);
        }

        public async Task<UsuarioDTO> SelecionarPorIdAsync(int id)
        {
            Usuario usuario = await this._usuarioRepository.SelecionarUnicoAsync(this._usuarioQuery.PorId(id));
            return this._mapper.Map<UsuarioDTO>(usuario);
        }
    }
}
