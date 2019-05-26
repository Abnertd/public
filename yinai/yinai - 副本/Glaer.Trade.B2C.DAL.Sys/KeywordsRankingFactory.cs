using System;
using System.Reflection;
using System.Configuration;
namespace Glaer.Trade.B2C.DAL.Sys
{
    public class KeywordsRankingFactory
    {
        public static IKeywordsRanking CreateKeywordsRanking()
        {
            string path = ConfigurationManager.AppSettings["DALRBACUser"];
            string classname = path + ".KeywordsRanking";
            return (IKeywordsRanking)Assembly.Load(path).CreateInstance(classname);
        }
    }
}
