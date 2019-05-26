using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.DAL;
using Glaer.Trade.B2C.RBAC;

namespace Glaer.Trade.B2C.BLL.ORD
{
    /// <summary>
    /// 订单操作
    /// </summary>
    public class Orders : IOrders
    {
        protected DAL.ORD.IOrders MyDAL;

        public Orders() {
            MyDAL = DAL.ORD.OrdersFactory.CreateOrders();
        }

        public virtual bool AddOrders(OrdersInfo entity) {
            return MyDAL.AddOrders(entity);
        }

        public virtual bool EditOrders(OrdersInfo entity) {
            return MyDAL.EditOrders(entity);
        }

        public virtual int DelOrders(int ID) {
            return MyDAL.DelOrders(ID);
        }

        public virtual OrdersInfo GetOrdersByID(int ID) {
            return MyDAL.GetOrdersByID(ID);
        }

        public virtual OrdersInfo GetOrdersBySN(string SN)
        {
            return MyDAL.GetOrdersBySN(SN);
        }

        public virtual OrdersInfo GetOrdersByPurchaseID(int Purchase_ID)
        {
            return MyDAL.GetOrdersByPurchaseID(Purchase_ID);
        }

        public virtual OrdersInfo GetOrdersByPriceReportID(int PriceReportID)
        {
            return MyDAL.GetOrdersByPriceReportID(PriceReportID);
        }

        public virtual IList<OrdersInfo> GetOrderss(QueryInfo Query) {
            return MyDAL.GetOrderss(Query);
        }

        public virtual IList<OrdersInfo> GetOrderssByContractID(int ID)
        {
            return MyDAL.GetOrderssByContractID(ID);
        }

        public virtual IList<OrdersInfo> GetOrderssByMemberID(int Member_ID)
        {
            return MyDAL.GetOrderssByMemberID(Member_ID);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query) {
            return MyDAL.GetPageInfo(Query);
        }

        public virtual bool AddOrdersGoods(OrdersGoodsInfo entity) {
            return MyDAL.AddOrdersGoods(entity);
        }

        public virtual bool EditOrdersGoods(OrdersGoodsInfo entity) {
            return MyDAL.EditOrdersGoods(entity);
        }

        public virtual int DelOrdersGoods(int ID) {
            return MyDAL.DelOrdersGoods(ID);
        }

        public virtual int DelOrdersGoodsByOrdersID(int ID)
        {
            return MyDAL.DelOrdersGoodsByOrdersID(ID);
        }

        public virtual bool AddOrdersCoupon(int Orders_ID, int Coupon_ID)
        {
            return MyDAL.AddOrdersCoupon(Orders_ID, Coupon_ID);
        }

        public virtual string GetOrdersCoupons(int Orders_ID)
        {
            return MyDAL.GetOrdersCoupons(Orders_ID);
        }

        public virtual bool AddOrdersFavorPolicy(int Orders_ID, int Policy_ID)
        {
            return MyDAL.AddOrdersFavorPolicy(Orders_ID, Policy_ID);
        }

        public virtual string GetOrdersPolicys(int Orders_ID)
        {
            return MyDAL.GetOrdersCoupons(Orders_ID);
        }

        public virtual int Get_Max_Goods_ID()
        {
            return MyDAL.Get_Max_Goods_ID();
        }

        public virtual OrdersGoodsInfo GetOrdersGoodsByID(int ID) {
            return MyDAL.GetOrdersGoodsByID(ID);
        }

        public virtual IList<OrdersGoodsInfo> GetGoodsListByOrderID(int ID) {
            return MyDAL.GetGoodsListByOrderID(ID);
        }

        public virtual string GetSupplierOrdersID(int Supplier_ID)
        {
            return MyDAL.GetSupplierOrdersID(Supplier_ID);
        }

        public virtual IList<OrdersGoodsInfo> GetOrdersGoodsList(QueryInfo Query)
        {
            return MyDAL.GetOrdersGoodsList(Query);
        }

        public virtual PageInfo GetGoodsPageInfo(QueryInfo Query)
        {
            return MyDAL.GetGoodsPageInfo(Query);
        }





        public OrdersInfo GetSupplierOrderInfoByID(int ID, int Supplier_ID)
        {
            return MyDAL.GetSupplierOrderInfoByID(ID,Supplier_ID);
        }

        public OrdersInfo GetMemberOrderInfoByID(int ID, int Member_ID)
        {
            return MyDAL.GetMemberOrderInfoByID(ID,Member_ID);
        }

        public OrdersInfo GetSupplierOrderInfoBySN(string SN, int Supplier_ID)
        {
            return MyDAL.GetSupplierOrderInfoBySN(SN,Supplier_ID);
        }

        public OrdersInfo GetMemberOrderInfoBySN(string SN, int Member_ID)
        {
            return MyDAL.GetMemberOrderInfoBySN(SN, Member_ID);
        }


        public OrdersInfo GetMemberTopOrdersInfo(int Member_ID)
        {
            return MyDAL.GetMemberTopOrdersInfo(Member_ID);
        }


        //获取商家订单交易数量
        public virtual int GetSupplierOrdersNum(int supplier_id)
        {
            return MyDAL.GetSupplierOrdersNum(supplier_id);
        }
    }

    public class OrdersContract : IOrdersContract
    {
        protected DAL.ORD.IOrdersContract MyDAL;
        protected IRBAC RBAC;

        public OrdersContract()
        {
            MyDAL = DAL.ORD.OrdersFactory.CreateOrdersContract();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddOrdersContract(OrdersContractInfo entity)
        {
            return MyDAL.AddOrdersContract(entity);
        }

        public virtual bool EditOrdersContract(OrdersContractInfo entity)
        {
            return MyDAL.EditOrdersContract(entity);
        }

        public virtual int DelOrdersContract(int ID)
        {
            return MyDAL.DelOrdersContract(ID);
        }

        public virtual OrdersContractInfo GetOrdersContractByID(int ID)
        {
            return MyDAL.GetOrdersContractByID(ID);
        }

        public virtual IList<OrdersContractInfo> GetOrdersContracts(QueryInfo Query)
        {
            return MyDAL.GetOrdersContracts(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }

        public OrdersContractInfo GetOrdersContractByOrdersID(int Orders_ID)
        {
            return MyDAL.GetOrdersContractByOrdersID(Orders_ID);
        }
    }

    public class OrdersLoanApply : IOrdersLoanApply
    {
        protected DAL.ORD.IOrdersLoanApply MyDAL;
        protected IRBAC RBAC;

        public OrdersLoanApply()
        {
            MyDAL = DAL.ORD.OrdersFactory.CreateOrdersLoanApply();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddOrdersLoanApply(OrdersLoanApplyInfo entity)
        {
            return MyDAL.AddOrdersLoanApply(entity);
        }

        public virtual bool EditOrdersLoanApply(OrdersLoanApplyInfo entity)
        {
            return MyDAL.EditOrdersLoanApply(entity);
        }

        public virtual int DelOrdersLoanApply(int ID)
        {
            return MyDAL.DelOrdersLoanApply(ID);
        }

        public virtual OrdersLoanApplyInfo GetOrdersLoanApplyByID(int ID)
        {
            return MyDAL.GetOrdersLoanApplyByID(ID);
        }

        public virtual IList<OrdersLoanApplyInfo> GetOrdersLoanApplys(QueryInfo Query)
        {
            return MyDAL.GetOrdersLoanApplys(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }


       
    }

}
