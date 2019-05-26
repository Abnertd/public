using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public class MemberFavoritesFactory
    {
        public static IMemberFavorites CreateMemberFavorites()
        {
            string path = ConfigurationManager.AppSettings["DALMemberFavorites"];
            string classname = path + ".MemberFavorites";
            return (IMemberFavorites)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
