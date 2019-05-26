using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.Util.Tools;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public interface ISupplierBank
    {
        bool AddSupplierBank(SupplierBankInfo entity);

        bool EditSupplierBank(SupplierBankInfo entity);

        int DelSupplierBank(int ID);

        SupplierBankInfo GetSupplierBankByID(int ID);

        SupplierBankInfo GetSupplierBankBySupplierID(int ID);

        IList<SupplierBankInfo> GetSupplierBanks(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
    }

}
