using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public class MemberCertTypeFactory
    {
        public static IMemberCertType CreateMemberCertType()
        {
            string path = ConfigurationManager.AppSettings["DALMEM"];
            string classname = path + ".MemberCertType";
            return (IMemberCertType)Assembly.Load(path).CreateInstance(classname);
        }
    }


    public class MemberCertFactory
    {
        public static IMemberCert CreateMemberCert()
        {
            string path = ConfigurationManager.AppSettings["DALMEM"];
            string classname = path + ".MemberCert";
            return (IMemberCert)Assembly.Load(path).CreateInstance(classname);
        }
    }
}
