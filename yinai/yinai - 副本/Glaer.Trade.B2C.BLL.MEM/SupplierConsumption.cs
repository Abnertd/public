using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.Util.Mail;
using Glaer.Trade.B2C.DAL;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public class SupplierConsumption : ISupplierConsumption
    {
        protected DAL.MEM.ISupplierConsumption MyDAL;

        public SupplierConsumption()
        {
            MyDAL = DAL.MEM.SupplierConsumptionFactory.CreateSupplierConsumption();
        }

        public virtual bool AddSupplierConsumption(SupplierConsumptionInfo entity)
        {
            return MyDAL.AddSupplierConsumption(entity);
        }

        public virtual int DelSupplierConsumption(int ID)
        {
            return MyDAL.DelSupplierConsumption(ID);
        }

        public virtual IList<SupplierConsumptionInfo> GetSupplierConsumptions(QueryInfo Query)
        {
            return MyDAL.GetSupplierConsumptions(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }

    }
}

