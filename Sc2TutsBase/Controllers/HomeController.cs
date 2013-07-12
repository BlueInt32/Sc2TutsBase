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
        List<TutorialEntry> TutoList
        {
            get
            {
                return HttpContext.Application["TutoList"] as List<TutorialEntry>;
            }
        }
        public ActionResult Index()
        {
            TutorialListAndFilterModel model = new TutorialListAndFilterModel();
            model.TutorialEntries = TutoList;

            return View(model);
        }


		public ActionResult Filter(string filter)
		{
            TutorialListAndFilterModel model = new TutorialListAndFilterModel();
			model.Filter = new Sc2Filter(filter);
            IEnumerable<TutorialEntry> list = TutoList.AsEnumerable();
			model.Filter.Apply(ref list);

			model.TutorialEntries = list.ToList();


            return View("Index", model);
        }


    }
}
