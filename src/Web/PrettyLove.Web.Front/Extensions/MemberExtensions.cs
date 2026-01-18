using PrettyLove.Core;
using PrettyLove.Domain;
using System.Collections.Generic;
using System.Security.Claims;

namespace PrettyLove.Web.Front
{
    public static class MemberExtensions
    {
        public static string GetAccessToken(this Member memberInfo, JwtOptions jwtOptions)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimConst.CLAINM_USER_ID, memberInfo.Id.Value.ToString()),
                new Claim(ClaimConst.CLAINM_USER_NAME, memberInfo.NickName),
                new Claim(ClaimConst.CLAINM_PHONE, memberInfo.Phone??string.Empty)
            };
            return JwtHelper.GenerateToken(claims, jwtOptions);
        }
    }
}
