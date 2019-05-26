using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public class MemberCertFactory
    {
        public static IMemberCert CreateMemberCert()
        {
            string path = ConfigurationManager.AppSettings["BLLMEM"];
            string classname = path + ".MemberCert";
            return (IMemberCert)Assembly.Load(path).CreateInstance(classname);
        }
    }

    public class MemberCertTypeFactory
    {
        public static IMemberCertType CreateMemberCertType()
        {
            string path = ConfigurationManager.AppSettings["BLLMEM"];
            string classname = path + ".MemberCertType";
            return (IMemberCertType)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
