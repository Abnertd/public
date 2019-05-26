using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public class BidUpRequireQuickFactory
    {
        public static IBidUpRequireQuick CreateBidUpRequireQuick()
        {
            string path = ConfigurationManager.AppSettings["BLLBidUpRequireQuick"];
            string classname = path + ".BidUpRequireQuick";
            return (IBidUpRequireQuick)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
