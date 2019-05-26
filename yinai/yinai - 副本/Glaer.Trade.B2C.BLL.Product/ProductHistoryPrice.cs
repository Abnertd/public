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
    public class ProductHistoryPrice : IProductHistoryPrice
    {
        protected DAL.Product.IProductHistoryPrice MyDAL;

        public ProductHistoryPrice()
        {
            MyDAL = DAL.Product.ProductHistoryPriceFactory.CreateProductHistoryPrice();
        }

        public virtual bool AddProductHistoryPrice(ProductHistoryPriceInfo entity)
        {
            return MyDAL.AddProductHistoryPrice(entity);
        }

        public virtual bool EditProductHistoryPrice(ProductHistoryPriceInfo entity)
        {
            return MyDAL.EditProductHistoryPrice(entity);
        }

        public virtual int DelProductHistoryPrice(int ID)
        {
            return MyDAL.DelProductHistoryPrice(ID);
        }

        public virtual ProductHistoryPriceInfo GetProductHistoryPriceByID(int ID)
        {
            return MyDAL.GetProductHistoryPriceByID(ID);
        }

        public virtual IList<ProductHistoryPriceInfo> GetProductHistoryPrices(QueryInfo Query)
        {
            return MyDAL.GetProductHistoryPrices(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }

    }
}

