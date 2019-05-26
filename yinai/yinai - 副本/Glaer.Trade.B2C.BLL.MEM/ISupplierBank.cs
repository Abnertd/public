using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.Util.Mail;
using Glaer.Trade.B2C.DAL;

namespace Glaer.Trade.B2C.BLL.MEM
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
