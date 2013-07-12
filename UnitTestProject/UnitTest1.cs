using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sc2TutsBase.Models;
using System.Collections.Generic;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var type = typeof(League);
            var values = Enum.GetValues(type);
            var values2 = Enum.ToObject(type, 0);

            //string list = Sc2TutsBase.Utils.EnumHelper<League>.GetTokens<League>("-");


        }
    }
}
