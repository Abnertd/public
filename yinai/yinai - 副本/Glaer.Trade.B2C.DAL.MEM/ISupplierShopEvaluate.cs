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
    public interface ISupplierShopEvaluate
    {
        bool AddSupplierShopEvaluate(SupplierShopEvaluateInfo entity);

        bool EditSupplierShopEvaluate(SupplierShopEvaluateInfo entity);

        int DelSupplierShopEvaluate(int ID);

        SupplierShopEvaluateInfo GetSupplierShopEvaluateByID(int ID);

        IList<SupplierShopEvaluateInfo> GetSupplierShopEvaluates(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);


        /// 根据编号获取商品评论信息实体
        /// </summary>
        /// <param name="ID">商品评论编号</param>
        /// <param name="UserPrivilege">权限</param>
        /// <returns>商品评论信息实体</returns>
        int GetSupplierShopEvaluateReviewValidCount(int Product_ID);
    }


}
