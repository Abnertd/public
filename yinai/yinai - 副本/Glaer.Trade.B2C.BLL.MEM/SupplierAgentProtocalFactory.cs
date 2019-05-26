using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public class SupplierAgentProtocalFactory
    {
        public static ISupplierAgentProtocal CreateSupplierAgentProtocal()
        {
            string path = ConfigurationManager.AppSettings["BLLSupplier"];
            string classname = path + ".SupplierAgentProtocal";
            return (ISupplierAgentProtocal)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
