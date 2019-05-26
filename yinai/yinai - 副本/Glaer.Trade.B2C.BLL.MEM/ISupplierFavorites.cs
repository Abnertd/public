using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public interface ISupplierFavorites
    {
        bool AddSupplierFavorites(SupplierFavoritesInfo entity);

        bool EditSupplierFavorites(SupplierFavoritesInfo entity);

        int DelSupplierFavorites(int ID);

        SupplierFavoritesInfo GetSupplierFavoritesByID(int ID);

        IList<SupplierFavoritesInfo> GetSupplierFavoritess(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

        /// <summary>
        /// 根据商家编号、类型、产品编号获取商家收藏信息实体
        /// </summary>
        /// <param name="Supplier_ID">编号</param>
        /// <param name="type_id">类型</param>
        /// <param name="target_ID">收藏对象编号</param>
        /// <returns>商家收藏信息实体</returns>
        SupplierFavoritesInfo GetSupplierFavoritesByProductID(int Supplier_ID, int type_id, int target_ID);

    }
}
