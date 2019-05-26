using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.Util.Tools;

namespace Glaer.Trade.B2C.DAL.ORD
{
    public interface IDeliveryTime
    {
        bool AddDeliveryTime(DeliveryTimeInfo entity);

        bool EditDeliveryTime(DeliveryTimeInfo entity);

        int DelDeliveryTime(int ID);

        DeliveryTimeInfo GetDeliveryTimeByID(int ID);

        IList<DeliveryTimeInfo> GetDeliveryTimes(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
    }
}
