using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sc2TutsBase.Utils
{
	public class TokenAttribute : Attribute
	{
		public string Token { get; set; }
		public TokenAttribute(string token)
		{
			Token = token;
		}
	}
}