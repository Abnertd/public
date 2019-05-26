using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public class MemberSubAccountFactory
    { 
        public static IMemberSubAccount CreateMemberSubAccount()
        {
            string path = ConfigurationManager.AppSettings["DALMEM"];
            string classname = path + ".MemberSubAccount";
            return (IMemberSubAccount)Assembly.Load(path).CreateInstance(classname);
        }

        public static IMemberSubAccountLog CreateMemberSubAccountLog()
        {
            string path = ConfigurationManager.AppSettings["DALMEM"];
            string classname = path + ".MemberSubAccountLog";
            return (IMemberSubAccountLog)Assembly.Load(path).CreateInstance(classname);
        }
    }
}
