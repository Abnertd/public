using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.Util.Mail;
using Glaer.Trade.B2C.DAL;

namespace Glaer.Trade.B2C.BLL.ORD
{
    public class DeliveryTime : IDeliveryTime
    {
        protected DAL.ORD.IDeliveryTime MyDAL;

        public DeliveryTime()
        {
            MyDAL = DAL.ORD.DeliveryTimeFactory.CreateDeliveryTime();
        }

        public virtual bool AddDeliveryTime(DeliveryTimeInfo entity)
        {
            return MyDAL.AddDeliveryTime(entity);
        }

        public virtual bool EditDeliveryTime(DeliveryTimeInfo entity)
        {
            return MyDAL.EditDeliveryTime(entity);
        }

        public virtual int DelDeliveryTime(int ID)
        {
            return MyDAL.DelDeliveryTime(ID);
        }

        public virtual DeliveryTimeInfo GetDeliveryTimeByID(int ID)
        {
            return MyDAL.GetDeliveryTimeByID(ID);
        }

        public virtual IList<DeliveryTimeInfo> GetDeliveryTimes(QueryInfo Query)
        {
            return MyDAL.GetDeliveryTimes(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }

    }
}

