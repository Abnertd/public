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
    public interface ISupplierOnline
    {
        bool AddSupplierOnline(SupplierOnlineInfo entity);

        bool EditSupplierOnline(SupplierOnlineInfo entity);

        int DelSupplierOnline(int ID);

        SupplierOnlineInfo GetSupplierOnlineByID(int ID);

        IList<SupplierOnlineInfo> GetSupplierOnlines(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
    }


}
