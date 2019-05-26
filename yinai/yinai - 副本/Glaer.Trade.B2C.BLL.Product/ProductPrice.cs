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
    public class ProductPrice : IProductPrice
    {
        protected DAL.Product.IProductPrice MyDAL;

        public ProductPrice()
        {
            MyDAL = DAL.Product.ProductPriceFactory.CreateProductPrice();
        }

        public virtual bool AddProductPrice(ProductPriceInfo entity)
        {
            return MyDAL.AddProductPrice(entity);
        }

        public virtual bool EditProductPrice(ProductPriceInfo entity)
        {
            return MyDAL.EditProductPrice(entity);
        }

        public virtual int DelProductPrice(int ID)
        {
            return MyDAL.DelProductPrice(ID);
        }

        public virtual ProductPriceInfo GetProductPriceByID(int ID)
        {
            return MyDAL.GetProductPriceByID(ID);
        }

        public virtual IList<ProductPriceInfo> GetProductPrices(int Product_ID)
        {
            return MyDAL.GetProductPrices(Product_ID);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }

    }
}

