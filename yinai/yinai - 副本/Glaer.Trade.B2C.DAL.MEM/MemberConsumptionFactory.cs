using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public class MemberConsumptionFactory
    {
        public static IMemberConsumption CreateMemberConsumption()
        {
            string path = ConfigurationManager.AppSettings["DALMemberConsumption"];
            string classname = path + ".MemberConsumption";
            return (IMemberConsumption)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
