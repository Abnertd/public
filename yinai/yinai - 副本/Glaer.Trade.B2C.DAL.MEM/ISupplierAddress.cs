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
