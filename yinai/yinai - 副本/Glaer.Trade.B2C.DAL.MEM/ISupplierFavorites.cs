using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public interface ISupplierFavorites
    {
        bool AddSupplierFavorites(SupplierFavoritesInfo entity);

        bool EditSupplierFavorites(SupplierFavoritesInfo entity);

        int DelSupplierFavorites(int ID);

        SupplierFavoritesInfo GetSupplierFavoritesByID(int ID);

        IList<SupplierFavoritesInfo> GetSupplierFavoritess(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
       SupplierFavoritesInfo GetSupplierFavoritesByProductID(int Supplier_ID, int type_id, int target_ID);
    }
}
