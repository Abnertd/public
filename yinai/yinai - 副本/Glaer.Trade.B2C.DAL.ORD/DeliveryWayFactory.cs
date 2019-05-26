﻿using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.ORD
{
    public class DeliveryWayFactory
    {
        public static IDeliveryWay CreateDeliveryWay()
        {
            string path = ConfigurationManager.AppSettings["DALORD"].ToString();
            string classname = path + ".DeliveryWay";
            return (IDeliveryWay)Assembly.Load(path).CreateInstance(classname);
        }

    }
}