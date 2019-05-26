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
