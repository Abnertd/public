using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public class KeywordBiddingFactory
    {
        public static IKeywordBidding CreateKeywordBidding()
        {
            string path = ConfigurationManager.AppSettings["BLLSupplier"];
            string classname = path + ".KeywordBidding";
            return (IKeywordBidding)Assembly.Load(path).CreateInstance(classname);
        }

    }

}
