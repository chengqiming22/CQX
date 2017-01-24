using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System.Configuration;
using Microsoft.Owin.Security.DataProtection;
using CQX.Web.Identity;

namespace CQX.Web
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext<IdentityUserManager>((options, context) =>
            {
                options.DataProtectionProvider = app.GetDataProtectionProvider();
                return IdentityUserManager.Create(options, context);
            });
            app.CreatePerOwinContext<IdentitySignInManager>(IdentitySignInManager.Create);
            
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                LogoutPath = new PathString("/Account/Logout"),
                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<IdentityUserManager, IdentityUser, int>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentityCallback: (manager, user) => user.GenerateUserIdentityAsync(manager, DefaultAuthenticationTypes.ApplicationCookie),
                        getUserIdCallback: (claim) => int.Parse(claim.GetUserId()))
                }
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
        }
    }
}