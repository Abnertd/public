using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.RBAC;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.B2C.DAL;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public interface ISupplierShopGrade
    {
        bool AddSupplierShopGrade(SupplierShopGradeInfo entity, RBACUserInfo UserPrivilege);

        bool EditSupplierShopGrade(SupplierShopGradeInfo entity, RBACUserInfo UserPrivilege);

        int DelSupplierShopGrade(int ID, RBACUserInfo UserPrivilege);

        SupplierShopGradeInfo GetSupplierShopGradeByID(int ID, RBACUserInfo UserPrivilege);

        IList<SupplierShopGradeInfo> GetSupplierShopGrades(QueryInfo Query, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);

    }

}
