using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.Util.Tools;

namespace Glaer.Trade.B2C.DAL.Product
{
    public interface IStockoutBooking
    {
        bool AddStockoutBooking(StockoutBookingInfo entity);

        bool EditStockoutBooking(StockoutBookingInfo entity);

        int DelStockoutBooking(int ID);

        StockoutBookingInfo GetStockoutBookingByID(int ID);

        IList<StockoutBookingInfo> GetStockoutBookings(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
    }
}
