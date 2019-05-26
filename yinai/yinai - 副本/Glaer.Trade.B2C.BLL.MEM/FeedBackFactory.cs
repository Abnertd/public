using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public class FeedBackFactory
    {
        public static IFeedBack CreateFeedBack()
        {
            string path = ConfigurationManager.AppSettings["BLLFeedBack"];
            string classname = path + ".FeedBack";
            return (IFeedBack)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
