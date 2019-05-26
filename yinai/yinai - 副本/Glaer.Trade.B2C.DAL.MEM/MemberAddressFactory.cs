using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public class MemberAddressFactory
    {
        public static IMemberAddress CreateMemberAddress()
        {
            string path = ConfigurationManager.AppSettings["DALMemberAddress"];
            string classname = path + ".MemberAddress";
            return (IMemberAddress)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
