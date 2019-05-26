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
    public interface ISupplierAddress
    {
        bool AddSupplierAddress(SupplierAddressInfo entity);

        bool EditSupplierAddress(SupplierAddressInfo entity);

        int DelSupplierAddress(int ID);

        SupplierAddressInfo GetSupplierAddressByID(int ID);

        IList<SupplierAddressInfo> GetSupplierAddresss(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

    }
}
