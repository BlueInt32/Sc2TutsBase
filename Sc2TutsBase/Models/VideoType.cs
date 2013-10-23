using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sc2TutsBase.Utils;

namespace Sc2TutsBase.Models
{
	public enum VideoType
	{
		[Token("o")]
		OMM,
		[Token("b")]
		BuildOrder,
		[Token("c")]
		Concepts
	}
}