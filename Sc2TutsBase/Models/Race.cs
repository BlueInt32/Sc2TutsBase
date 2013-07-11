using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sc2TutsBase.Utils;

namespace Sc2TutsBase.Models
{
	public enum Race
	{
		[Token("t")]Terran,
		[Token("p")]Protoss,
		[Token("z")]Zerg
	}
}