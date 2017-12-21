using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Validation;

namespace QuickstartIdentityServer.Oauth2
{
    public class CResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            List<Claim> claims;
            if (validPwd(context.UserName , context.Password,out claims))
            {
                //使用subject可用于在资源服务器区分用户身份等等
                //获取：通过User.Claims.Where(l => l.Type == "sub").FirstOrDefault();获取
                context.Result = new GrantValidationResult(subject: "admin", authenticationMethod: "custom", claims:claims);
            }
            else
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "invalid custom credential");
            }
            return Task.FromResult(0);
        }
        public static bool validPwd(string userName, string pwd, out List<Claim> claims)
        {
            claims = new List<Claim>();
            var bRet = false;
            if (userName == "alice" && pwd == "password")       //可以从数据库中读取判断
            {
                bRet = true;
                claims.Add(new Claim("role", "admin")); //根据 user 对象，设置不同的 role
            }
            return bRet;
        }
    }
}
