using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public interface ISupplierAgentProtocal
    {
        bool AddSupplierAgentProtocal(SupplierAgentProtocalInfo entity, RBACUserInfo UserPrivilege);

        bool EditSupplierAgentProtocal(SupplierAgentProtocalInfo entity, RBACUserInfo UserPrivilege);

        int DelSupplierAgentProtocal(int ID, RBACUserInfo UserPrivilege);

        SupplierAgentProtocalInfo GetSupplierAgentProtocalByID(int ID, RBACUserInfo UserPrivilege);
        SupplierAgentProtocalInfo GetSupplierAgentProtocalByPurchaseID(int PurchaseID, RBACUserInfo UserPrivilege);

        IList<SupplierAgentProtocalInfo> GetSupplierAgentProtocals(QueryInfo Query, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);

    }
}
