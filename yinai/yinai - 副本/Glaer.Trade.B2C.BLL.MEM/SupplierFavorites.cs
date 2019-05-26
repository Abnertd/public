using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.RBAC;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public class SupplierFavorites : ISupplierFavorites
    {
        protected DAL.MEM.ISupplierFavorites MyDAL;
        protected IRBAC RBAC;

        public SupplierFavorites()
        {
            MyDAL = DAL.MEM.SupplierFavoritesFactory.CreateSupplierFavorites();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddSupplierFavorites(SupplierFavoritesInfo entity)
        {
            return MyDAL.AddSupplierFavorites(entity);
        }

        public virtual bool EditSupplierFavorites(SupplierFavoritesInfo entity)
        {
            return MyDAL.EditSupplierFavorites(entity);
        }

        public virtual int DelSupplierFavorites(int ID)
        {
            return MyDAL.DelSupplierFavorites(ID);
        }

        public virtual SupplierFavoritesInfo GetSupplierFavoritesByID(int ID)
        {
            return MyDAL.GetSupplierFavoritesByID(ID);
        }

        public virtual IList<SupplierFavoritesInfo> GetSupplierFavoritess(QueryInfo Query)
        {
            return MyDAL.GetSupplierFavoritess(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }

        public virtual SupplierFavoritesInfo GetSupplierFavoritesByProductID(int Supplier_ID, int type_id, int target_ID)
        {
            return MyDAL.GetSupplierFavoritesByProductID(Supplier_ID, type_id, target_ID);
        }


    }
}
