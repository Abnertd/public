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
    public class SupplierShopApply : ISupplierShopApply
    {
        protected DAL.MEM.ISupplierShopApply MyDAL;
        protected IRBAC RBAC;

        public SupplierShopApply()
        {
            MyDAL = DAL.MEM.SupplierShopApplyFactory.CreateSupplierShopApply();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddSupplierShopApply(SupplierShopApplyInfo entity)
        {
            return MyDAL.AddSupplierShopApply(entity);
        }

        public virtual bool EditSupplierShopApply(SupplierShopApplyInfo entity)
        {
            return MyDAL.EditSupplierShopApply(entity);
        }

        public virtual int DelSupplierShopApply(int ID)
        {
            return MyDAL.DelSupplierShopApply(ID);
        }

        public virtual SupplierShopApplyInfo GetSupplierShopApplyByID(int ID)
        {
            return MyDAL.GetSupplierShopApplyByID(ID);
        }

        public virtual SupplierShopApplyInfo GetSupplierShopApplyBySupplierID(int ID)
        {
            return MyDAL.GetSupplierShopApplyBySupplierID(ID);
        }

        public virtual IList<SupplierShopApplyInfo> GetSupplierShopApplys(QueryInfo Query)
        {
            return MyDAL.GetSupplierShopApplys(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }

    }

}

