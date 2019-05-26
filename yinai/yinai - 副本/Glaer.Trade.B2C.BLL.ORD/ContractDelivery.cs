using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.DAL;
using Glaer.Trade.B2C.RBAC;

namespace Glaer.Trade.B2C.BLL.ORD
{
    public class ContractDelivery : IContractDelivery
    {
        protected DAL.ORD.IContractDelivery MyDAL;
        protected IRBAC RBAC;

        public ContractDelivery()
        {
            MyDAL = DAL.ORD.ContractDeliveryFactory.CreateContractDelivery();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddContractDelivery(ContractDeliveryInfo entity)
        {
            return MyDAL.AddContractDelivery(entity);
        }

        public virtual bool EditContractDelivery(ContractDeliveryInfo entity)
        {
            return MyDAL.EditContractDelivery(entity);
        }

        public virtual int DelContractDelivery(int ID)
        {
            return MyDAL.DelContractDelivery(ID);
        }

        public virtual ContractDeliveryInfo GetContractDeliveryByID(int ID)
        {
            return MyDAL.GetContractDeliveryByID(ID);
        }

        public virtual ContractDeliveryInfo GetContractDeliveryBySN(string SN)
        {
            return MyDAL.GetContractDeliveryBySN(SN);
        }

        public virtual IList<ContractDeliveryInfo> GetContractDeliverys(QueryInfo Query)
        {
            return MyDAL.GetContractDeliverys(Query);
        }

        public virtual IList<ContractDeliveryInfo> GetContractDeliverysByContractID(int ContractID)
        {
            return MyDAL.GetContractDeliverysByContractID(ContractID);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }

        public virtual int Get_Orders_Goods_DeliveryAmount(int Goods_ID)
        {
            return MyDAL.Get_Orders_Goods_DeliveryAmount(Goods_ID);
        }

        public virtual bool AddContractDeliveryGoods(ContractDeliveryGoodsInfo entity)
        {
            return MyDAL.AddContractDeliveryGoods(entity);
        }

        public virtual bool EditContractDeliveryGoods(ContractDeliveryGoodsInfo entity)
        {
            return MyDAL.EditContractDeliveryGoods(entity);
        }

        public virtual ContractDeliveryGoodsInfo GetContractDeliveryGoodsByID(int ID)
        {
            return MyDAL.GetContractDeliveryGoodsByID(ID);
        }

        public virtual IList<ContractDeliveryGoodsInfo> GetContractDeliveryGoodss(QueryInfo Query)
        {
            return MyDAL.GetContractDeliveryGoodss(Query);
        }

        public virtual IList<ContractDeliveryGoodsInfo> GetContractDeliveryGoodssByDeliveryID(int ID)
        {
            return MyDAL.GetContractDeliveryGoodssByDeliveryID(ID);
        }

    }

}
