using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public interface ISupplierLogistics
    {
        bool AddSupplierLogistics(SupplierLogisticsInfo entity);

        bool EditSupplierLogistics(SupplierLogisticsInfo entity, RBACUserInfo UserPrivilege);

        int DelSupplierLogistics(int ID, RBACUserInfo UserPrivilege);

        SupplierLogisticsInfo GetSupplierLogisticsByID(int ID, RBACUserInfo UserPrivilege);

        IList<SupplierLogisticsInfo> GetSupplierLogisticss(QueryInfo Query, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);

    }

    public interface ILogisticsTender
    {
        bool AddLogisticsTender(LogisticsTenderInfo entity);

        bool EditLogisticsTender(LogisticsTenderInfo entity);

        int DelLogisticsTender(int ID);

        LogisticsTenderInfo GetLogisticsTenderByID(int ID);

        IList<LogisticsTenderInfo> GetLogisticsTenders(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

    }
}
