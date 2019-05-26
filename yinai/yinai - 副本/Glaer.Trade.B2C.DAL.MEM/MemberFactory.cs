using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public class MemberFactory
    {
        public static IMember CreateMember()
        {
            string path = ConfigurationManager.AppSettings["DALMEM"].ToString();
            string classname = path + ".Member";
            return (IMember)Assembly.Load(path).CreateInstance(classname);
        }

    }

    public class MemberLogFactory
    {
        public static IMemberLog CreateMemberLog()
        {
            string path = ConfigurationManager.AppSettings["DALMemberLog"];
            string classname = path + ".MemberLog";
            return (IMemberLog)Assembly.Load(path).CreateInstance(classname);
        }

    }

    public class MemberPurchaseFactory
    {
        public static IMemberPurchase CreateMemberPurchase()
        {
            string path = ConfigurationManager.AppSettings["DALMEM"];
            string classname = path + ".MemberPurchase";
            return (IMemberPurchase)Assembly.Load(path).CreateInstance(classname);
        }

    }

    public class MemberPurchaseReplyFactory
    {
        public static IMemberPurchaseReply CreateMemberPurchaseReply()
        {
            string path = ConfigurationManager.AppSettings["DALMEM"];
            string classname = path + ".MemberPurchaseReply";
            return (IMemberPurchaseReply)Assembly.Load(path).CreateInstance(classname);
        }
    }

    public class MemberTokenFactory
    {
        public static IMemberToken CreateMemberToken()
        {
            string path = ConfigurationManager.AppSettings["DALMEM"];
            string classname = path + ".MemberToken";
            return (IMemberToken)Assembly.Load(path).CreateInstance(classname);
        }
    }
}
