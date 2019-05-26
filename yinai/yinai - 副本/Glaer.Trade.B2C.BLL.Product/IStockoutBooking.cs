using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.Util.Mail;
using Glaer.Trade.B2C.DAL;

namespace Glaer.Trade.B2C.BLL.Product
{
    public interface IStockoutBooking
    {
        bool AddStockoutBooking(StockoutBookingInfo entity, RBACUserInfo UserPrivilege);

        bool EditStockoutBooking(StockoutBookingInfo entity, RBACUserInfo UserPrivilege);

        int DelStockoutBooking(int ID, RBACUserInfo UserPrivilege);

        StockoutBookingInfo GetStockoutBookingByID(int ID, RBACUserInfo UserPrivilege);

        IList<StockoutBookingInfo> GetStockoutBookings(QueryInfo Query, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);

    }
}
