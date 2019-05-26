using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.DAL.ORD
{
    /// <summary>
    /// 配送方式定义
    /// </summary>
    public interface IDeliveryWay
    {
        bool AddDeliveryWay(DeliveryWayInfo entity);

        bool EditDeliveryWay(DeliveryWayInfo entity);

        int DelDeliveryWay(int ID);

        DeliveryWayInfo GetDeliveryWayByID(int ID);

        IList<DeliveryWayInfo> GetDeliveryWays(QueryInfo Query);

        IList<DeliveryWayInfo> GetDeliveryWaysByDistrict(string state, string city, string county);

        PageInfo GetPageInfo(QueryInfo Query);


        bool AddDeliveryWayDistrict(DeliveryWayDistrictInfo entity);

        bool EditDeliveryWayDistrict(DeliveryWayDistrictInfo entity);

        int DelDeliveryWayDistrict(int ID);

        DeliveryWayDistrictInfo GetDeliveryWayDistrictByID(int ID);

        IList<DeliveryWayDistrictInfo> GetDeliveryWayDistrictsByDWID(int ID);
    }

}



