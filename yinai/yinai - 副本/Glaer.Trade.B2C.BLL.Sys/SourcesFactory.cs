using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.Sys
{
    public class SourcesFactory
    {
        public static ISources CreateSources()
        {
            string path = ConfigurationManager.AppSettings["BLLRBACUser"].ToString();
            string classname = path + ".Sources";
            return (ISources)Assembly.Load(path).CreateInstance(classname);
        }

    }

}