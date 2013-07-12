using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Sc2TutsBase.Utils
{
    public class EnumHelper
    {
        public static string GetToken<TEnum>(TEnum value)
        {
            var memInfo = typeof(TEnum).GetMember(value.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(TokenAttribute), false);
            return ((TokenAttribute)attributes[0]).Token;
        }
		public static List<string> GetTokensList<TEnum>()
        {
            IEnumerable<TEnum> enumValues = Enum.GetValues(typeof(TEnum)).Cast<TEnum>();
            return enumValues.ToList().ConvertAll(GetToken);
           
        }
        public static string GetTokens<TEnum>(string separator)
        {
			return string.Join(separator, GetTokensList<TEnum>().ToArray());
        }

		public static TEnum GetEnumValueFromToken<TEnum>(string token)
		{
			List<string> tokensList = GetTokensList<TEnum>();
			int index = tokensList.IndexOf(token);
			return Enum.GetValues(typeof(TEnum)).Cast<TEnum>().ToList()[index];
		}

		//public static Dictionary<string, TEnum> TokenDictionary<TEnum>()
		//{
		//    Dictionary<string, TEnum> resultDico = new Dictionary<string,TEnum>();
		//    List<string> listTokens = GetTokensList<TEnum>();
		//    List<TEnum> listValues = Enum.GetValues(typeof(TEnum)).Cast<TEnum>().ToList();
		//    for (int i = 0; i < listTokens.Count; i++)
		//    {
		//        resultDico.Add(listTokens[i], listValues[i]);
		//    }

		//    return resultDico;
		//}
    }
}