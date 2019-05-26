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
    public class SupplierShopPages : ISupplierShopPages
    {
        protected DAL.MEM.ISupplierShopPages MyDAL;
        protected IRBAC RBAC;

        public SupplierShopPages()
        {
            MyDAL = DAL.MEM.SupplierShopPagesFactory.CreateSupplierShopPages();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddSupplierShopPages(SupplierShopPagesInfo entity)
        {
            return MyDAL.AddSupplierShopPages(entity);
        }

        public virtual bool EditSupplierShopPages(SupplierShopPagesInfo entity)
        {
            return MyDAL.EditSupplierShopPages(entity);
        }

        public virtual int DelSupplierShopPages(int ID)
        {
            return MyDAL.DelSupplierShopPages(ID);
        }

        public virtual SupplierShopPagesInfo GetSupplierShopPagesByID(int ID)
        {
            return MyDAL.GetSupplierShopPagesByID(ID);
        }

        public virtual SupplierShopPagesInfo GetSupplierShopPagesByIDSign(string Sign, int Supplier_ID)
        {
            return MyDAL.GetSupplierShopPagesByIDSign(Sign, Supplier_ID);
        }

        public virtual IList<SupplierShopPagesInfo> GetSupplierShopPagess(QueryInfo Query)
        {
            return MyDAL.GetSupplierShopPagess(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }

    }


}

