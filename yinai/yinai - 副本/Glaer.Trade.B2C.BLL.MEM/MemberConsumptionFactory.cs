using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public class MemberConsumptionFactory
    {
        public static IMemberConsumption CreateMemberConsumption()
        {
            string path = ConfigurationManager.AppSettings["BLLMemberConsumption"];
            string classname = path + ".MemberConsumption";
            return (IMemberConsumption)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
