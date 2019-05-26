using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public class ZhongXinFactory
    {
        public static IZhongXin Create()
        {
            string path = ConfigurationManager.AppSettings["DALSigning"];
            string classname = path + ".ZhongXin";
            return (IZhongXin)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
