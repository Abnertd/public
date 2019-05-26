using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public class MemberAccountLogFactory
    {
        public static IMemberAccountLog CreateMemberAccountLog()
        {
            string path = ConfigurationManager.AppSettings["DALMemberAccountLog"];
            string classname = path + ".MemberAccountLog";
            return (IMemberAccountLog)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
