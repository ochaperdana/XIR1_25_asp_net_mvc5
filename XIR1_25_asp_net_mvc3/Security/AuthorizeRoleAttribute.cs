using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XIR1_25_asp_net_mvc3.Models.DB;
using XIR1_25_asp_net_mvc3.Models.EntityManager;

namespace XIR1_25_asp_net_mvc3.Security
{
    public class AuthorizeRoleAttribute:AuthorizeAttribute
    {
        private readonly string[] userAssignedRoles;
        public AuthorizeRoleAttribute(params string[] roles)
        {
            this.userAssignedRoles = roles;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;
            using (TryDBEntities db = new TryDBEntities())
            {
                UserManager UM = new UserManager();
                foreach (var roles in userAssignedRoles)
                {
                    authorize = UM.IsUserInRole(httpContext.User.Identity.Name,
                    roles);
                    if (authorize)
                        return authorize;
                }
            }
            return authorize;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext
filterContext)
        {
            filterContext.Result = new RedirectResult("~/Home/UnAuthorized");
        }
    }
}