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
    public interface ISupplierCloseShopApply
    {
        bool AddSupplierCloseShopApply(SupplierCloseShopApplyInfo entity);

        bool EditSupplierCloseShopApply(SupplierCloseShopApplyInfo entity);

        int DelSupplierCloseShopApply(int ID);

        SupplierCloseShopApplyInfo GetSupplierCloseShopApplyByID(int ID);

        IList<SupplierCloseShopApplyInfo> GetSupplierCloseShopApplys(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
    }



}
