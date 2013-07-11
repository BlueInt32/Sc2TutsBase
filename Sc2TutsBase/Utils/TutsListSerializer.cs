using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using Sc2TutsBase.Models;

namespace Sc2TutsBase.Utils
{
	public class TutsListSerializer
	{
		private static readonly string filePath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["ConfigFilePath"]);
		public static void SaveTutsToFile(List<TutorialEntry> list)
		{
			XmlSerializer xs = new XmlSerializer(typeof(List<TutorialEntry>));
			using (StreamWriter wr = new StreamWriter(filePath))
			{
				xs.Serialize(wr, list);
			}
		}

		public static List<TutorialEntry> LoadTutsFromFile()
		{
			List<TutorialEntry> isis = new List<TutorialEntry>();
			XmlSerializer xs = new XmlSerializer(typeof(List<TutorialEntry>));

			XmlReader xd;
			xd = XmlReader.Create(filePath);
			if (xs.CanDeserialize(xd))
			{

				isis = xs.Deserialize(xd) as List<TutorialEntry>;
				//ManageXmlFieldsNotDeserialisable(isis);
				//ManageNewLines(isis);
				xd.Close();
			}
			else
			{
				xd.Close();
				// Bad Characters in XML
				throw new Exception("Probleme dans le fichier de configuration List<TutorialEntry>.");
			}
			return isis;
		}
	}
}