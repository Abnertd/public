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
    public class SupplierCategory : ISupplierCategory
    {
        protected DAL.MEM.ISupplierCategory MyDAL;
        protected IRBAC RBAC;

        public SupplierCategory()
        {
            MyDAL = DAL.MEM.SupplierCategoryFactory.CreateSupplierCategory();
            RBAC = RBACFactory.CreateRBAC();
        }

        #region 产品分组定义
        public virtual bool AddSupplierCategory(SupplierCategoryInfo entity)
        {
            return MyDAL.AddSupplierCategory(entity);
        }

        public virtual bool EditSupplierCategory(SupplierCategoryInfo entity)
        {
            return MyDAL.EditSupplierCategory(entity);
        }

        public virtual int DelSupplierCategory(int ID)
        {
            return MyDAL.DelSupplierCategory(ID);
        }

        public virtual SupplierCategoryInfo GetSupplierCategoryByID(int ID)
        {
            return MyDAL.GetSupplierCategoryByID(ID);
        }

        public virtual SupplierCategoryInfo GetSupplierCategoryByIDSupplier(int ID, int Supplier_ID)
        {
            return MyDAL.GetSupplierCategoryByIDSupplier(ID,Supplier_ID);
        }

        public virtual IList<SupplierCategoryInfo> GetSupplierCategorys(QueryInfo Query)
        {
            return MyDAL.GetSupplierCategorys(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }
        #endregion

        #region 产品/产品分组关系
        public virtual bool AddSupplierProductCategory(SupplierProductCategoryInfo entity)
        {
            return MyDAL.AddSupplierProductCategory(entity);
        }

        public virtual bool EditSupplierProductCategory(SupplierProductCategoryInfo entity)
        {
            return MyDAL.EditSupplierProductCategory(entity);
        }

        public virtual int DelSupplierProductCategoryByProductID(int ID)
        {
            return MyDAL.DelSupplierProductCategoryByProductID(ID);
        }

        public virtual int DelSupplierProductCategoryByCateID(int ID)
        {
            return MyDAL.DelSupplierProductCategoryByCateID(ID);
        }

        public virtual IList<SupplierProductCategoryInfo> GetSupplierProductCategorysByProductID(int Product_ID)
        {
            return MyDAL.GetSupplierProductCategorysByProductID(Product_ID);
        }

        public virtual IList<SupplierProductCategoryInfo> GetSupplierProductCategorysByCateID(int Cate_ID)
        {
            return MyDAL.GetSupplierProductCategorysByCateID(Cate_ID);
        }

        public virtual string GetSupplierProductCategorysByCateArry(string Cate_ID)
        {
            return MyDAL.GetSupplierProductCategorysByCateArry(Cate_ID);
        }
        #endregion
    }




}

