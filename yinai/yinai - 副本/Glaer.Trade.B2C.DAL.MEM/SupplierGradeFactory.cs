using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public class SupplierGradeFactory
    {
        public static ISupplierGrade CreateSupplierGrade()
        {
            string path = ConfigurationManager.AppSettings["DALSupplier"];
            string classname = path + ".SupplierGrade";
            return (ISupplierGrade)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
