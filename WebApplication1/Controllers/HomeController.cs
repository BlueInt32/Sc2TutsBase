using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.WelcomeMessage = "Bienvenue chez Novedia !";
            return View();
        }
        [Authorize]
        public ActionResult AuthenticatedUsers()
        {
            ViewBag.WelcomeMessage = "Vous êtes authentifié.";
            return View();
        }
        [Authorize(Roles = "Admin, Super User")]
        public ActionResult AdministratorsOnly()
        {
            ViewBag.WelcomeMessage = "Bonjour, maître.";
            return View();
        }
        [Authorize(Users = "Simon, John")]
        public ActionResult SpecificUserOnly()
        {
            ViewBag.WelcomeMessage = string.Format("Salut {0} !", User.Identity.Name);
            return View();
        }
    }
}