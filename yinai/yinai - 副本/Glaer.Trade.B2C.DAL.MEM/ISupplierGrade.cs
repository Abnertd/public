using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public interface ISupplierGrade
    {
        bool AddSupplierGrade(SupplierGradeInfo entity);

        bool EditSupplierGrade(SupplierGradeInfo entity);

        int DelSupplierGrade(int ID);

        SupplierGradeInfo GetSupplierGradeByID(int ID);

        IList<SupplierGradeInfo> GetSupplierGrades(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

        SupplierGradeInfo GetSupplierDefaultGrade();
    }

}
