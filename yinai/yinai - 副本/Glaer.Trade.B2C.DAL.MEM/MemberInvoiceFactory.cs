using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public class MemberInvoiceFactory
    {
        public static IMemberInvoice CreateMemberInvoice()
        {
            string path = ConfigurationManager.AppSettings["DALMEM"];
            string classname = path + ".MemberInvoice";
            return (IMemberInvoice)Assembly.Load(path).CreateInstance(classname);
        }
    }
}
