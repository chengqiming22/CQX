using CQX.Web.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using CQX.Web.Models;
using CQX.ServiceClient.Clients;
using CQMFramework.Utils.Common;
using CQX.ServiceContract.Models;

namespace CQX.Web.Controllers
{
    public class AccountController : Controller
    {
        private IdentitySignInManager _signInManager;
        private IdentityUserManager _userManager;

        public AccountController()
        {
        }
        public AccountController(IdentityUserManager userManager, IdentitySignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public IdentitySignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<IdentitySignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        public IdentityUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<IdentityUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "登录失败，用户名或密码错误");
                    return View(model);
            }
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.UserName, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var createResult = await new UsersServiceClient().CreateUserAsync(user.ConvertTo<UserModel>());
                    if (createResult.Code != 0)
                    {
                        ModelState.AddModelError("", "创建用户失败：" + createResult.Message);
                        return View(model);
                    }
                    else
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToAction("Index", "Home");
                    }
                }
                AddErrors(result);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Logout()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();
            return await Task.FromResult(RedirectToLocal(""));
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}