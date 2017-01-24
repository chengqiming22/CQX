using CQX.ServiceClient.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CQX.Web.Filters
{
    public class AuthenticatedFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controller = filterContext.Controller;
            var identity = filterContext.HttpContext.User.Identity;
            if (identity.IsAuthenticated)
            {
                var service = new UsersServiceClient();
                var getUserResult = service.GetUser(identity.Name);
                if (getUserResult.Code == 0 && getUserResult.ReturnObject != null)
                {
                    var result = new UsersServiceClient().GetPagesByUserName(identity.Name);
                    if (result.Code == 0)
                    {
                        controller.ViewBag.Modules = result.ReturnObject;
                    }
                }
                else
                {
                    var authManager = HttpContext.Current.GetOwinContext().Authentication;
                    authManager.SignOut();
                    filterContext.Result = new RedirectResult("~/Account/Login");
                    return;
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}