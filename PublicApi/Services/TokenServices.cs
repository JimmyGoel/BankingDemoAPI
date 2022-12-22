using ApplicationCore.Entity;
using ApplicationCore.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PublicApi.Services
{
    public class TokenServices : ITokenServices
    {
        private readonly SymmetricSecurityKey _key;
        public TokenServices(IConfiguration config)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }

        string ITokenServices.TokenServices(clsUserEntity clsUser)
        {
            var claims = new List<Claim>
           {
               new Claim(JwtRegisteredClaimNames.UniqueName,clsUser.userName),
               new Claim(JwtRegisteredClaimNames.NameId,Convert.ToString(clsUser.Id))
           };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescription);
            return tokenHandler.WriteToken(token);

        }
    }

}
