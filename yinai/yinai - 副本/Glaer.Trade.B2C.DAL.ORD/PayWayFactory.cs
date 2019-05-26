﻿using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.ORD
{
    public class PayWayFactory
    {
        public static IPayWay CreatePayWay()
        {
            string path = ConfigurationManager.AppSettings["DALORD"].ToString();
            string classname = path + ".PayWay";
            return (IPayWay)Assembly.Load(path).CreateInstance(classname);
        }

    }
}

