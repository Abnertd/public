using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public interface ISupplierAgentProtocal
    {
        bool AddSupplierAgentProtocal(SupplierAgentProtocalInfo entity);

        bool EditSupplierAgentProtocal(SupplierAgentProtocalInfo entity);

        int DelSupplierAgentProtocal(int ID);

        SupplierAgentProtocalInfo GetSupplierAgentProtocalByID(int ID);

        SupplierAgentProtocalInfo GetSupplierAgentProtocalByPurchaseID(int PurchaseID);

        IList<SupplierAgentProtocalInfo> GetSupplierAgentProtocals(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
    }
}
