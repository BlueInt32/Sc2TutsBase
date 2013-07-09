using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sc2TutsBase.Models;
using Sc2TutsBase.Utils;

namespace Sc2TutsBase.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            TutorialListAndFilterModel model = new TutorialListAndFilterModel();
            // model.LeagueFilter = null;
            model.TutorialEntries = TutsListSerializer.LoadTutsFromFile();


            return View(model);
        }
        
		public ActionResult 


    }
}
