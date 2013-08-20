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
			PrehomeViewModel model = new PrehomeViewModel();
			model.LastThreeTutoEntries = (HttpContext.Application["TutoList"] as List<TutorialEntry>).OrderByDescending(t => t.CreationDate).Take(3).ToList();
			return View(model);
		}
		public ActionResult List()
		{
			TutorialListAndFilterModel model = new TutorialListAndFilterModel();
			model.ShowList = false;
			model.PotentialMessage = "Utilise le système de filtre à gauche en fonction de ce que tu cherches !";
			//model.TutorialEntries = TutoList;

			return View(model);
		}


		public ActionResult Filter(string filter)
		{
			if (string.IsNullOrWhiteSpace(filter))
				return RedirectToAction("List");
			TutorialListAndFilterModel model = new TutorialListAndFilterModel();
			model.Filter = new Sc2Filter(filter);
			IEnumerable<TutorialEntry> list = TutoList.AsEnumerable();
			model.Filter.Apply(ref list);
			model.ShowList = true;
			model.TutorialEntries = list.ToList();
			if (model.TutorialEntries.Count == 0)
			{
				model.ShowList = false;
				model.PotentialMessage = "Aucun tuto n'a encore été ajouté pour cette recherche !";
			}
			model.TutoListViewType = Request.Cookies["displayMode"] != null ? (Request.Cookies["displayMode"].Value == "m" ? TutoListViewType.Mosaic : TutoListViewType.List) : TutoListViewType.Mosaic;

			Session["LastSearch"] = filter;


			return View("List", model);
		}

	    public ActionResult test()
	    {
	        return View();
	    }

		public ActionResult SwitchDisplayMode(string mode)
		{
			if (string.IsNullOrWhiteSpace(mode))
			{
				mode = "m";
			}
			Response.Cookies.Add(new HttpCookie("displayMode", mode));
			return RedirectToAction("Filter", new { filter = Session["LastSearch"] ?? "____"});
		}

	}
}
