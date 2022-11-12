using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Business.Helpers.Common
{
    public static class DecodeJWTToken
    {
        public static int GetId(string token)
        {
            var decode = new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken;
            var uid = Convert.ToInt32(decode.Claims.FirstOrDefault(claim => claim.Type == "Uid").Value);
            return uid;
        }
    }
}
