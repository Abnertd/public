using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public class MemberAddressFactory
    {
        public static IMemberAddress CreateMemberAddress()
        {
            string path = ConfigurationManager.AppSettings["BLLMemberAddress"];
            string classname = path + ".MemberAddress";
            return (IMemberAddress)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
