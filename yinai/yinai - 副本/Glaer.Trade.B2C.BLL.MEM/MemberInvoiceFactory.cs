using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public class MemberInvoiceFactory
    {
        public static IMemberInvoice CreateMemberInvoice()
        {
            string path = ConfigurationManager.AppSettings["BLLMEM"];
            string classname = path + ".MemberInvoice";
            return (IMemberInvoice)Assembly.Load(path).CreateInstance(classname);
        }
    }
}
