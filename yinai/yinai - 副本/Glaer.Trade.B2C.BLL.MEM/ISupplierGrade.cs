using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.BLL.MEM
{

    public interface ISupplierGrade
    {
        bool AddSupplierGrade(SupplierGradeInfo entity, RBACUserInfo UserPrivilege);

        bool EditSupplierGrade(SupplierGradeInfo entity, RBACUserInfo UserPrivilege);

        int DelSupplierGrade(int ID, RBACUserInfo UserPrivilege);

        SupplierGradeInfo GetSupplierGradeByID(int ID, RBACUserInfo UserPrivilege);

        IList<SupplierGradeInfo> GetSupplierGrades(QueryInfo Query, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);

        SupplierGradeInfo GetSupplierDefaultGrade();

    }

}