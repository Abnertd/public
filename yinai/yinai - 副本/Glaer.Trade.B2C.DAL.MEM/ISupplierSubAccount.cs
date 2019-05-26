using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public interface ISupplierSubAccount 
    {
        bool AddSupplierSubAccount(SupplierSubAccountInfo entity);

        bool EditSupplierSubAccount(SupplierSubAccountInfo entity);

        int DelSupplierSubAccount(int ID);
        SupplierSubAccountInfo GetSupplierSubAccountByName(string name);

        SupplierSubAccountInfo GetSupplierSubAccountByID(int ID);

        IList<SupplierSubAccountInfo> GetSupplierSubAccounts(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
    }
}
