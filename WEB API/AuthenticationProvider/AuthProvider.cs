using Microsoft.Ajax.Utilities;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Owin.Host.SystemWeb;
using Microsoft.Owin.Cors;
using WEB_API.Models;


namespace WEB_API.AuthenticationProvider
{
    //public class AuthProvider : OAuthAuthorizationServerProvider
    //{
    //    public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
    //    {
    //        context.Validated();
    //    }

    //    public override async Task
    //        GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
    //    {
    //        var identity = new ClaimsIdentity(context.Options.AuthenticationType);
    //        if (context.UserName == "abc@gmail.com" && context.Password == "123456")
    //        {
    //            identity.AddClaim(new Claim("username", context.UserName));
    //            identity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
    //            context.Validated(identity);
    //        }
    //        else
    //        {
    //            context.SetError("invalid_grant", "Provided username and password is incorrect");
    //        };
    //        return;

    public class AuthProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            using (var db = new ProductContext())
            {
                var user = db.Users.SingleOrDefault(u => u.UserName == context.UserName);

                if (user == null || user.Password != context.Password)
                {
                    context.SetError("invalid_grant", "The username or password is incorrect.");
                    return;
                }

                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
                identity.AddClaim(new Claim(ClaimTypes.Role, "User")); 

                context.Validated(identity);
            }
        }
    }
}