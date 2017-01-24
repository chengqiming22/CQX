using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace CQX.Web.Identity
{
    // Configure the application sign-in manager which is used in this application.
    public class IdentitySignInManager : SignInManager<IdentityUser, int>
    {
        public IdentitySignInManager(IdentityUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(IdentityUser user)
        {
            return user.GenerateUserIdentityAsync((IdentityUserManager)UserManager, DefaultAuthenticationTypes.ApplicationCookie);
        }

        public static IdentitySignInManager Create(IdentityFactoryOptions<IdentitySignInManager> options, IOwinContext context)
        {
            return new IdentitySignInManager(context.GetUserManager<IdentityUserManager>(), context.Authentication);
        }
    }
}