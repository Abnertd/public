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
    public interface ISupplierShopEvaluate
    {
        bool AddSupplierShopEvaluate(SupplierShopEvaluateInfo entity);

        bool EditSupplierShopEvaluate(SupplierShopEvaluateInfo entity);

        int DelSupplierShopEvaluate(int ID);

        SupplierShopEvaluateInfo GetSupplierShopEvaluateByID(int ID);

        IList<SupplierShopEvaluateInfo> GetSupplierShopEvaluates(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);


        /// <summary>
        /// 根据商品编号获取商品评论有效数量
        /// </summary>
        /// <param name="Product_ID">商品编号</param>
        /// <returns>商品评论有效数量</returns>
        int GetSupplierShopEvaluateReviewValidCount(int Product_ID);

    }

}
