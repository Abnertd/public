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
    public interface IOrdersGoodsTmp
    {
        bool AddOrdersGoodsTmp(OrdersGoodsTmpInfo entity);

        bool EditOrdersGoodsTmp(OrdersGoodsTmpInfo entity);

        int DelOrdersGoodsTmp(int ID, int goods_type, int parent_id, string sessionid);

        int ClearOrdersGoodsTmp(string sessionid);

        int ClearOrdersGoodsTmp(string sessionid, int supplyid);

        int ClearOrdersGoodsTmpByOrdersID(int Orders_ID);

        int ClearOrdersGoodsTmpByGoodsID(string Goods_IDs,int supplier_id);

        int ClearOrdersGoodsTmpByGoodsID(string Goods_IDs);

        OrdersGoodsTmpInfo GetOrdersGoodsTmpByID(int ID);

        IList<OrdersGoodsTmpInfo> GetOrdersGoodsTmps(QueryInfo Query);

        IList<OrdersGoodsTmpInfo> GetOrdersGoodsTmpsByOrdersID(int Orders_ID);

        PageInfo GetPageInfo(QueryInfo Query);

        int Get_Orders_Goods_Amount(string sessionid, int product_id);

        int Get_Orders_Goods_TypeAmount(string sessionid, int product_id, int product_type);

        int Get_Orders_Goods_PackageAmount(string sessionid, int package_id);

        int Get_Orders_Goods_ParentID(string sessionid, int product_id, int product_type);

        string GetOrdersGoodsTmpSupplierID(string sessionid, int buyer_id);
    }
}
