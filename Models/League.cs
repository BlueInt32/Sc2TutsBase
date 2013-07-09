using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sc2TutsBase.Utils;

namespace Sc2TutsBase.Models
{
	public enum League
	{
		[Token("b")]Bronze,
		[Token("s")]Silver,
		[Token("g")]Gold,
		[Token("p")]Platinum,
		[Token("d")]Diamond,
		[Token("m")]Master
	}
}