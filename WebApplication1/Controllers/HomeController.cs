using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sc2TutsBase.Filters;
using WebApplication1.Filters;

namespace WebApplication1.Controllers
{
    [HandleError(ExceptionType= typeof(HttpException), View = "MyErrorView")]
    public class HomeController : Controller
    {
		[PostDataFilter]
        public ActionResult Index()
        {
            ViewBag.WelcomeMessage = "Bienvenue chez Novedia !";
            return View();
        }

		[CheckSession(ActionName ="Question")]
		public ActionResult Question(int questionIndex1Based)
		{
			QuestionModel model = new QuestionModel(questionIndex1Based);
			return View(model);
		}

		[CheckSession(Order = 1, ActionName ="Question"), CheckQuizValid(Order = 2)]
		public ActionResult QuizOk(int questionIndex1Based)
		{
			ViewBag.FinalMessage = "Bravo, vous avez correctement répondu au quiz !";
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