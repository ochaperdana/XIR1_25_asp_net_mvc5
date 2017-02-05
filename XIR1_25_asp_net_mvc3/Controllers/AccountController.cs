using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using XIR1_25_asp_net_mvc3.Models.ViewModel;
using XIR1_25_asp_net_mvc3.Models.EntityManager;

namespace XIR1_25_asp_net_mvc3.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LogIn(UserLoginView ULV, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                UserManager UM = new UserManager();
                string password = UM.GetUserPassword(ULV.LoginName);

                if (string.IsNullOrEmpty(password))
                    ModelState.AddModelError("", "The user login or password provided is incorrect.");
                else
                {
                    if (ULV.Password.Equals(password))
                    {
                        FormsAuthentication.SetAuthCookie(ULV.LoginName, false);
                        return RedirectToAction("Welcome", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "The password provided is incorrect.");
                    }
                }
            }
            return View(ULV);
        }
        // GET: Account
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(UserSignUpView USV)
        {
            if (ModelState.IsValid)
            {
                UserManager UM = new UserManager();
                if (!UM.IsLoginNameExist(USV.LoginName))
                {
                    UM.AddUserAccount(USV);
                    FormsAuthentication.SetAuthCookie(USV.FirstName, false);
                    return RedirectToAction("Welcome", "Home");
                }
                else
                    ModelState.AddModelError("", "Login Name Already Exist.");
            }
            return View();
        }
        [Authorize]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}