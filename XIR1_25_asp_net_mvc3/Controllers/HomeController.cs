using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XIR1_25_asp_net_mvc3.Security;

namespace XIR1_25_asp_net_mvc3.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult Welcome()
        {
            return View();
        }
        [AuthorizeRole("Admin")]
        public ActionResult AdminOnly()
        {
            return View();
        }
        public ActionResult UnAuthorized()
        {
            return View();
        }
    }
}