using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Sc2TutsBase.Utils
{
    public class EnumHelper<TEnum> where TEnum : IConvertible
    {
        public static string GetToken<TEnum>(TEnum value)
        {
            var memInfo = typeof(TEnum).GetMember(value.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(TokenAttribute), false);
            return ((TokenAttribute)attributes[0]).Token;
        }

        public static string GetTokens<TEnum>(string separator)
        {
            IEnumerable<TEnum> enumValues = Enum.GetValues(typeof(TEnum)).Cast<TEnum>();
            List<string> tokens = enumValues.ToList().ConvertAll(item => GetToken<TEnum>(item));
            return string.Join(separator, tokens.ToArray());
        }
    }
}