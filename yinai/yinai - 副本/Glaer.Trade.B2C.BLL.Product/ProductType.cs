using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.Util.Mail;
using Glaer.Trade.B2C.DAL;

namespace Glaer.Trade.B2C.BLL.Product
{
    public class ProductType : IProductType
    {
        protected DAL.Product.IProductType MyDAL;

        public ProductType()
        {
            MyDAL = DAL.Product.ProductTypeFactory.CreateProductType();
        }

        public virtual bool AddProductType(ProductTypeInfo entity, RBACUserInfo UserPrivilege)
        {
            return MyDAL.AddProductType(entity);
        }

        public virtual bool AddProductType_Brand(int ProductType_ID, int Brand_ID, RBACUserInfo UserPrivilege)
        {
            return MyDAL.AddProductType_Brand(ProductType_ID,Brand_ID);
        }

        public virtual bool EditProductType(ProductTypeInfo entity, RBACUserInfo UserPrivilege)
        {
            return MyDAL.EditProductType(entity);
        }

        public virtual int DelProductType(int ID, RBACUserInfo UserPrivilege)
        {
            return MyDAL.DelProductType(ID);
        }

        public virtual int DelProductType_Brand(int ID, RBACUserInfo UserPrivilege)
        {
            return MyDAL.DelProductType_Brand(ID);
        }

        public virtual int DelProductType_Brand(int ID, int Brand_ID, RBACUserInfo UserPrivilege)
        {
            return MyDAL.DelProductType_Brand(ID, Brand_ID);
        }

        public virtual int DelProductType_Extend(int ID, RBACUserInfo UserPrivilege)
        {
            return MyDAL.DelProductType_Extend(ID);
        }

        public virtual ProductTypeInfo GetProductTypeByID(int ID, RBACUserInfo UserPrivilege)
        {
            return MyDAL.GetProductTypeByID(ID);
        }

        public virtual ProductTypeInfo GetProductTypeMax(RBACUserInfo UserPrivilege)
        {
            return MyDAL.GetProductTypeMax();
        }

        public virtual IList<ProductTypeInfo> GetProductTypes(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            return MyDAL.GetProductTypes(Query);
        }

        public virtual IList<BrandInfo> GetProductBrands(int ProductTypeID, RBACUserInfo UserPrivilege) 
        {
            return MyDAL.GetProductBrands(ProductTypeID);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            return MyDAL.GetPageInfo(Query);
        }

    }
}

