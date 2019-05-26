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
    public class SupplierAddress : ISupplierAddress
    {
        protected DAL.MEM.ISupplierAddress MyDAL;

        public SupplierAddress()
        {
            MyDAL = DAL.MEM.SupplierAddressFactory.CreateSupplierAddress();
        }

        public virtual bool AddSupplierAddress(SupplierAddressInfo entity)
        {
            return MyDAL.AddSupplierAddress(entity);
        }

        public virtual bool EditSupplierAddress(SupplierAddressInfo entity)
        {
            return MyDAL.EditSupplierAddress(entity);
        }

        public virtual int DelSupplierAddress(int ID)
        {
            return MyDAL.DelSupplierAddress(ID);
        }

        public virtual SupplierAddressInfo GetSupplierAddressByID(int ID)
        {
            return MyDAL.GetSupplierAddressByID(ID);
        }

        public virtual IList<SupplierAddressInfo> GetSupplierAddresss(QueryInfo Query)
        {
            return MyDAL.GetSupplierAddresss(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }

    }
}

