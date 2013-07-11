using Sc2TutsBase.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Sc2TutsBase.Utils;

namespace Sc2TutsBase.Areas.Admin.Controllers
{
    public class TutorialController : Controller
    {
        //
        // GET: /Admin/Tutorial/

		public ActionResult CreateTuto()
        {
            //dicoSizes.Add("small

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

            TutsListSerializer.SaveTutsToFile(existinglist);
            return View();
        }

    }
}
