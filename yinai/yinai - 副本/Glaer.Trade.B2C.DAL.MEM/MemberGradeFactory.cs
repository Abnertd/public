using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public class MemberGradeFactory
    {
        public static IMemberGrade CreateMemberGrade()
        {
            string path = ConfigurationManager.AppSettings["DALMEM"].ToString();
            string classname = path + ".MemberGrade";
            return (IMemberGrade)Assembly.Load(path).CreateInstance(classname);
        }
    }
}
