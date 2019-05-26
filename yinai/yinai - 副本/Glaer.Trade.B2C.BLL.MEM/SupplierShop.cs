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
    public class SupplierShop : ISupplierShop
    {
        protected DAL.MEM.ISupplierShop MyDAL;
        protected IRBAC RBAC;

        public SupplierShop()
        {
            MyDAL = DAL.MEM.SupplierShopFactory.CreateSupplierShop();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddSupplierShop(SupplierShopInfo entity)
        {
            return MyDAL.AddSupplierShop(entity);
        }

        public virtual bool EditSupplierShop(SupplierShopInfo entity)
        {
            return MyDAL.EditSupplierShop(entity);
        }

        public virtual int DelSupplierShop(int ID)
        {
            return MyDAL.DelSupplierShop(ID);
        }

        public virtual SupplierShopInfo GetSupplierShopByID(int ID)
        {
            return MyDAL.GetSupplierShopByID(ID);
        }

        public virtual SupplierShopInfo GetSupplierShopBySupplierID(int ID)
        {
            return MyDAL.GetSupplierShopBySupplierID(ID);
        }

        public virtual SupplierShopInfo GetSupplierShopByDomain(string Domain)
        {
            return MyDAL.GetSupplierShopByDomain(Domain);
        }

        public virtual IList<SupplierShopInfo> GetSupplierShops(QueryInfo Query)
        {
            return MyDAL.GetSupplierShops(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }

        public virtual void SaveShopCategory(int Shop_ID, string[] extends)
        {
            MyDAL.SaveShopCategory(Shop_ID, extends);
        }

        public virtual string GetShopCategory(int Shop_ID)
        {
            return MyDAL.GetShopCategory(Shop_ID);
        }

    }

    public class SupplierShopDomain : ISupplierShopDomain
    {
        protected DAL.MEM.ISupplierShopDomain MyDAL;
        protected IRBAC RBAC;

        public SupplierShopDomain()
        {
            MyDAL = DAL.MEM.SupplierShopDomainFactory.CreateSupplierShopDomain();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddSupplierShopDomain(SupplierShopDomainInfo entity)
        {
            return MyDAL.AddSupplierShopDomain(entity);
        }

        public virtual bool EditSupplierShopDomain(SupplierShopDomainInfo entity)
        {
            return MyDAL.EditSupplierShopDomain(entity);
        }

        public virtual int DelSupplierShopDomain(int ID)
        {
            return MyDAL.DelSupplierShopDomain(ID);
        }

        public virtual SupplierShopDomainInfo GetSupplierShopDomainByID(int ID)
        {
            return MyDAL.GetSupplierShopDomainByID(ID);
        }

        public virtual IList<SupplierShopDomainInfo> GetSupplierShopDomains(QueryInfo Query)
        {
            return MyDAL.GetSupplierShopDomains(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }

    }


}

