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
    public class SupplierOnline : ISupplierOnline
    {
        protected DAL.MEM.ISupplierOnline MyDAL;
        protected IRBAC RBAC;

        public SupplierOnline()
        {
            MyDAL = DAL.MEM.SupplierOnlineFactory.CreateSupplierOnline();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddSupplierOnline(SupplierOnlineInfo entity)
        {
            return MyDAL.AddSupplierOnline(entity);
        }

        public virtual bool EditSupplierOnline(SupplierOnlineInfo entity)
        {
            return MyDAL.EditSupplierOnline(entity);
        }

        public virtual int DelSupplierOnline(int ID)
        {
            return MyDAL.DelSupplierOnline(ID);
        }

        public virtual SupplierOnlineInfo GetSupplierOnlineByID(int ID)
        {
            return MyDAL.GetSupplierOnlineByID(ID);
        }

        public virtual IList<SupplierOnlineInfo> GetSupplierOnlines(QueryInfo Query)
        {
            return MyDAL.GetSupplierOnlines(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }

    }




}

