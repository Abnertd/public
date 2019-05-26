using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.Sys
{
    public class KeywordsRankingFactory
    {
        public static IKeywordsRanking CreateKeywordsRanking()
        {
            string path = ConfigurationManager.AppSettings["BLLRBACUser"];
            string classname = path + ".KeywordsRanking";
            return (IKeywordsRanking)Assembly.Load(path).CreateInstance(classname);
        }
    }
}
