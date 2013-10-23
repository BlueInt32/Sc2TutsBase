using Sc2TutsBase.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Sc2TutsBase.Utils;

namespace Sc2TutsBase.Areas.administration.Controllers
{
    public class TutorialController : Controller
    {
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult CreateTuto()
        {
			ViewBag.Message = string.Empty;
            return View();
        }

        [HttpPost]
		public ActionResult CreateTuto(TutorialEntry model)
        {
            //string youtubeRegex = "<h1[^>]*?>(?<TagText>.*?)</h1>";
            string youtubeRegex = @"http://www\.youtube\.com/watch\?(feature=player_detailpage&)?v=(?<youtubeId>[^#]*)(#t=(?<youtubeTiming>.*)s)?";
            MatchCollection mc = Regex.Matches(model.VideoUrl, youtubeRegex);
            foreach (Match m in mc)
            {
                model.YoutubeId = m.Groups["youtubeId"].Value;
                model.StartTiming = m.Groups["youtubeTiming"].Value;
            }
            List<TutorialEntry> existinglist = TutsListSerializer.LoadTutsFromFile();

            existinglist.Add(model);
			int i = 1;
        	foreach (TutorialEntry tutorialEntry in existinglist)
        	{
        		tutorialEntry.Id = i++;
        	}

            TutsListSerializer.SaveTutsToFile(existinglist);

			HttpRuntime.UnloadAppDomain();

			HttpPostedFileBase pdfFile = Request.Files["pdfFile"];
			if (pdfFile.ContentLength != 0)
			{
				pdfFile.SaveAs(Server.MapPath(string.Format("~/pdfFiles/{0}.pdf", existinglist.Last().Id)));
			}
			ViewBag.Message = "Ok !";
            return View();
        }

		public ActionResult EditTuto(int id)
		{
			List<TutorialEntry> existinglist = TutsListSerializer.LoadTutsFromFile();
			TutorialEntry tuto = existinglist.First(t => t.Id == id);

			ViewBag.Message = string.Empty;

			return View(tuto);
		}
		[HttpPost]
		public ActionResult EditTuto(TutorialEntry model)
		{
			string youtubeRegex = @"http://www\.youtube\.com/watch\?(feature=player_detailpage&)?v=(?<youtubeId>[^#]*)(#t=(?<youtubeTiming>.*)s)?";
			MatchCollection mc = Regex.Matches(model.VideoUrl, youtubeRegex);
			foreach (Match m in mc)
			{
				model.YoutubeId = m.Groups["youtubeId"].Value;
				model.StartTiming = m.Groups["youtubeTiming"].Value;
			}



			List<TutorialEntry> existinglist = TutsListSerializer.LoadTutsFromFile();
			TutorialEntry tutoToChange = existinglist.First(t => t.Id == model.Id);
			tutoToChange.Against = model.Against;
			tutoToChange.Race = model.Race;
			tutoToChange.Title = model.Title;
			tutoToChange.Description = model.Description;
			tutoToChange.VideoUrl = model.VideoUrl;
			tutoToChange.CurrentLeague = model.CurrentLeague;
			tutoToChange.Author = model.Author;
			tutoToChange.VideoType = model.VideoType;
			TutsListSerializer.SaveTutsToFile(existinglist);

			HttpPostedFileBase pdfFile = Request.Files["pdfFile"];
			if (pdfFile != null && pdfFile.ContentLength != 0)
			{
				pdfFile.SaveAs(Server.MapPath(string.Format("~/pdfFiles/{0}.pdf", existinglist.Last().Id)));
			}
			HttpRuntime.UnloadAppDomain();


			ViewBag.Message = "Ok !";
			return View(tutoToChange);
		}

    }
}
