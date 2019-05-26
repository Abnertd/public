using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public class MemberAccountLogFactory
    {
        public static IMemberAccountLog CreateMemberAccountLog()
        {
            string path = ConfigurationManager.AppSettings["BLLMemberAccountLog"];
            string classname = path + ".MemberAccountLog";
            return (IMemberAccountLog)Assembly.Load(path).CreateInstance(classname);
        }

    }

}
