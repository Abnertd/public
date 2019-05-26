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
    public class SupplierShopArticle : ISupplierShopArticle
    {
        protected DAL.MEM.ISupplierShopArticle MyDAL;
        protected IRBAC RBAC;

        public SupplierShopArticle()
        {
            MyDAL = DAL.MEM.SupplierShopArticleFactory.CreateSupplierShopArticle();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddSupplierShopArticle(SupplierShopArticleInfo entity)
        {
            return MyDAL.AddSupplierShopArticle(entity);
        }

        public virtual bool EditSupplierShopArticle(SupplierShopArticleInfo entity)
        {
            return MyDAL.EditSupplierShopArticle(entity);
        }

        public virtual int DelSupplierShopArticle(int ID)
        {
            return MyDAL.DelSupplierShopArticle(ID);
        }

        public virtual SupplierShopArticleInfo GetSupplierShopArticleByID(int ID)
        {
            return MyDAL.GetSupplierShopArticleByID(ID);
        }

        public virtual SupplierShopArticleInfo GetSupplierShopArticleByIDSupplier(int ID, int Supplier_ID)
        {
            return MyDAL.GetSupplierShopArticleByIDSupplier(ID, Supplier_ID);
        }

        public virtual IList<SupplierShopArticleInfo> GetSupplierShopArticles(QueryInfo Query)
        {
            return MyDAL.GetSupplierShopArticles(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }

    }



}

