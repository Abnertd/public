using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public class BidUpRequireQuickFactory
    {
        public static IBidUpRequireQuick CreateBidUpRequireQuick()
        {
            string path = ConfigurationManager.AppSettings["DALBidUpRequireQuick"];
            string classname = path + ".BidUpRequireQuick";
            return (IBidUpRequireQuick)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
