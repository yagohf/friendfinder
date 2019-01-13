using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Yagohf.Cubo.FriendFinder.Business.Interface.Helper;
using Yagohf.Cubo.FriendFinder.Infrastructure.Configuration;
using Yagohf.Cubo.FriendFinder.Model.DTO;

namespace Yagohf.Cubo.FriendFinder.Business.Helper
{
    public class TokenHelper : ITokenHelper
    {
        private readonly IOptions<Autenticacao> _configuracoesAutenticacao;

        public TokenHelper(IOptions<Autenticacao> configuracoesAutenticacao)
        {
            this._configuracoesAutenticacao = configuracoesAutenticacao;
        }

        public TokenDTO Gerar(string usuario)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(this._configuracoesAutenticacao.Value.ChaveCriptografia);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
            return new TokenDTO()
            {
                Nome = usuario,
                Token = tokenHandler.WriteToken(securityToken)
            };
        }
    }
}
