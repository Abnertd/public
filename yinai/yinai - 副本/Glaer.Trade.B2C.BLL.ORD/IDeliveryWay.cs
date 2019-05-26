using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.BLL.ORD
{
    /// <summary>
    /// 配送方式定义
    /// </summary>
    public interface IDeliveryWay
    {
        bool AddDeliveryWay(DeliveryWayInfo entity, RBACUserInfo UserPrivilege);

        bool EditDeliveryWay(DeliveryWayInfo entity, RBACUserInfo UserPrivilege);

        int DelDeliveryWay(int ID, RBACUserInfo UserPrivilege);

        DeliveryWayInfo GetDeliveryWayByID(int ID, RBACUserInfo UserPrivilege);

        IList<DeliveryWayInfo> GetDeliveryWays(QueryInfo Query, RBACUserInfo UserPrivilege);

        IList<DeliveryWayInfo> GetDeliveryWaysByDistrict(string state, string city, string county, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);

        bool AddDeliveryWayDistrict(DeliveryWayDistrictInfo entity, RBACUserInfo UserPrivilege);

        bool EditDeliveryWayDistrict(DeliveryWayDistrictInfo entity, RBACUserInfo UserPrivilege);

        int DelDeliveryWayDistrict(int ID, RBACUserInfo UserPrivilege);

        DeliveryWayDistrictInfo GetDeliveryWayDistrictByID(int ID, RBACUserInfo UserPrivilege);

        IList<DeliveryWayDistrictInfo> GetDeliveryWayDistrictsByDWID(int ID, RBACUserInfo UserPrivilege);
    }

}
