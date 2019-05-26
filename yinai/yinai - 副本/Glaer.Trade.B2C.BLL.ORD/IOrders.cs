using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.BLL.ORD
{
    /// <summary>
    /// 订单操作定义
    /// </summary>
    public interface IOrders
    {
        bool AddOrders(OrdersInfo entity);

        bool EditOrders(OrdersInfo entity);

        int DelOrders(int ID);

        OrdersInfo GetOrdersByID(int ID);

        OrdersInfo GetOrdersBySN(string SN);

        OrdersInfo GetSupplierOrderInfoByID(int ID, int Supplier_ID);

        OrdersInfo GetMemberOrderInfoByID(int ID, int Member_ID);

        OrdersInfo GetSupplierOrderInfoBySN(string SN, int Supplier_ID);

        OrdersInfo GetMemberOrderInfoBySN(string SN, int Member_ID);

        OrdersInfo GetMemberTopOrdersInfo(int Member_ID);

        OrdersInfo GetOrdersByPurchaseID(int Purchase_ID);

        OrdersInfo GetOrdersByPriceReportID(int PriceReportID);

        IList<OrdersInfo> GetOrderss(QueryInfo Query);

        IList<OrdersInfo> GetOrderssByContractID(int ID);

        IList<OrdersInfo> GetOrderssByMemberID(int Member_ID);

        PageInfo GetPageInfo(QueryInfo Query); 

        bool AddOrdersGoods(OrdersGoodsInfo entity);

        bool EditOrdersGoods(OrdersGoodsInfo entity);

        int DelOrdersGoods(int ID);

        int DelOrdersGoodsByOrdersID(int ID);

        bool AddOrdersCoupon(int Orders_ID, int Coupon_ID);

        string GetOrdersCoupons(int Orders_ID);

        bool AddOrdersFavorPolicy(int Orders_ID, int Policy_ID);

        string GetOrdersPolicys(int Orders_ID);

        int Get_Max_Goods_ID();

        OrdersGoodsInfo GetOrdersGoodsByID(int ID);

        IList<OrdersGoodsInfo> GetGoodsListByOrderID(int ID);

        string GetSupplierOrdersID(int Supplier_ID);

        IList<OrdersGoodsInfo> GetOrdersGoodsList(QueryInfo Query);

        PageInfo GetGoodsPageInfo(QueryInfo Query);



        int GetSupplierOrdersNum(int supplier_id);
    }

    public interface IOrdersContract
    {
        bool AddOrdersContract(OrdersContractInfo entity);

        bool EditOrdersContract(OrdersContractInfo entity);

        int DelOrdersContract(int ID);

        OrdersContractInfo GetOrdersContractByID(int ID);

        OrdersContractInfo GetOrdersContractByOrdersID(int Orders_ID);

        IList<OrdersContractInfo> GetOrdersContracts(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

    }

    public interface IOrdersLoanApply
    {
        bool AddOrdersLoanApply(OrdersLoanApplyInfo entity);

        bool EditOrdersLoanApply(OrdersLoanApplyInfo entity);

        int DelOrdersLoanApply(int ID);

        OrdersLoanApplyInfo GetOrdersLoanApplyByID(int ID);

        IList<OrdersLoanApplyInfo> GetOrdersLoanApplys(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

    }

}
