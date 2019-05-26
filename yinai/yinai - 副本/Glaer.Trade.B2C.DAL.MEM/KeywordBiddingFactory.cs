using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public class KeywordBiddingFactory
    {
        public static IKeywordBidding CreateKeywordBidding()
        {
            string path = ConfigurationManager.AppSettings["DALSupplier"];
            string classname = path + ".KeywordBidding";
            return (IKeywordBidding)Assembly.Load(path).CreateInstance(classname);
        }

    }


}
