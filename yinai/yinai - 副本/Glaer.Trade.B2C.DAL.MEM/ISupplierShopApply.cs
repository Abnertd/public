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
    public interface ISupplierShopApply
    {
        bool AddSupplierShopApply(SupplierShopApplyInfo entity);

        bool EditSupplierShopApply(SupplierShopApplyInfo entity);

        int DelSupplierShopApply(int ID);

        SupplierShopApplyInfo GetSupplierShopApplyByID(int ID);

        SupplierShopApplyInfo GetSupplierShopApplyBySupplierID(int ID);

        IList<SupplierShopApplyInfo> GetSupplierShopApplys(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
    }

}
