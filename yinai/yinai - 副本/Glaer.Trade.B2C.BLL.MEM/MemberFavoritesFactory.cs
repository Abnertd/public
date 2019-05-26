using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public class MemberFavoritesFactory
    {
        public static IMemberFavorites CreateMemberFavorites()
        {
            string path = ConfigurationManager.AppSettings["BLLMemberFavorites"];
            string classname = path + ".MemberFavorites";
            return (IMemberFavorites)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
