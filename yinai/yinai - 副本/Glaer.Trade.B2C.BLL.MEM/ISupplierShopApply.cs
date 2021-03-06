﻿using System;
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
