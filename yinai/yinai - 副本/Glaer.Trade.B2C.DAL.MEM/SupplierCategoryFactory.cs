﻿using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public class SupplierCategoryFactory
    {
        public static ISupplierCategory CreateSupplierCategory()
        {
            string path = ConfigurationManager.AppSettings["DALSupplier"];
            string classname = path + ".SupplierCategory";
            return (ISupplierCategory)Assembly.Load(path).CreateInstance(classname);
        }

    }

}
