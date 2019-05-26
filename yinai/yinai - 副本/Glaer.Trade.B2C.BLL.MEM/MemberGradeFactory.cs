using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public class MemberGradeFactory
    {
        public static IMemberGrade CreateMemberGrade()
        {
            string path = ConfigurationManager.AppSettings["BLLMEM"].ToString();
            string classname = path + ".MemberGrade";
            return (IMemberGrade)Assembly.Load(path).CreateInstance(classname);
        }
    }
}
