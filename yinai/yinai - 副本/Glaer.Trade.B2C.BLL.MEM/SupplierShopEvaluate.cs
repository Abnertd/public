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
    public class SupplierShopEvaluate : ISupplierShopEvaluate
    {
        protected DAL.MEM.ISupplierShopEvaluate MyDAL;
        protected IRBAC RBAC;

        public SupplierShopEvaluate()
        {
            MyDAL = DAL.MEM.SupplierShopEvaluateFactory.CreateSupplierShopEvaluate();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddSupplierShopEvaluate(SupplierShopEvaluateInfo entity)
        {
            return MyDAL.AddSupplierShopEvaluate(entity);
        }

        public virtual bool EditSupplierShopEvaluate(SupplierShopEvaluateInfo entity)
        {
            return MyDAL.EditSupplierShopEvaluate(entity);
        }

        public virtual int DelSupplierShopEvaluate(int ID)
        {
            return MyDAL.DelSupplierShopEvaluate(ID);
        }

        public virtual SupplierShopEvaluateInfo GetSupplierShopEvaluateByID(int ID)
        {
            return MyDAL.GetSupplierShopEvaluateByID(ID);
        }

        public virtual IList<SupplierShopEvaluateInfo> GetSupplierShopEvaluates(QueryInfo Query)
        {
            return MyDAL.GetSupplierShopEvaluates(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }
        public virtual int GetSupplierShopEvaluateReviewValidCount(int Product_ID)
        {
            return MyDAL.GetSupplierShopEvaluateReviewValidCount(Product_ID);
        }

    }




}

