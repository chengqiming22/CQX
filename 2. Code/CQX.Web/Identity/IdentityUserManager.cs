using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CQX.Web.Identity
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class IdentityUserManager : UserManager<IdentityUser, int>
    {
        public IdentityUserManager(IUserStore<IdentityUser, int> store)
            : base(store)
        {
        }

        public static IdentityUserManager Create(IdentityFactoryOptions<IdentityUserManager> options, IOwinContext context)
        {
            var manager = new IdentityUserManager(new UserStore());

            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<IdentityUser, int>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = false
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 3,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };
            
            return manager;
        }
    }
}