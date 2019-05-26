using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.Sys
{
    public class SourcesFactory
    {
        public static ISources CreateSources()
        {
            string path = ConfigurationManager.AppSettings["DALRBACUser"].ToString();
            string classname = path + ".Sources";
            return (ISources)Assembly.Load(path).CreateInstance(classname);
        }

    }

}