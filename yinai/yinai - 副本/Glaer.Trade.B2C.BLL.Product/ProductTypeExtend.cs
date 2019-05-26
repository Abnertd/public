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
    public class ProductTypeExtend : IProductTypeExtend
    {
        protected DAL.Product.IProductTypeExtend MyDAL;

        public ProductTypeExtend()
        {
            MyDAL = DAL.Product.ProductTypeExtendFactory.CreateProductTypeExtend();
        }

        public virtual bool AddProductTypeExtend(ProductTypeExtendInfo entity)
        {
            return MyDAL.AddProductTypeExtend(entity);
        }

        public virtual bool EditProductTypeExtend(ProductTypeExtendInfo entity)
        {
            return MyDAL.EditProductTypeExtend(entity);
        }

        public virtual int DelProductTypeExtend(int ID)
        {
            return MyDAL.DelProductTypeExtend(ID);
        }

        public virtual ProductTypeExtendInfo GetProductTypeExtendByID(int ID)
        {
            return MyDAL.GetProductTypeExtendByID(ID);
        }

        public virtual IList<ProductTypeExtendInfo> GetProductTypeExtends(int ID)
        {
            return MyDAL.GetProductTypeExtends(ID);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }

    }
}

