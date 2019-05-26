using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.BLL.ORD
{
    public interface IContractDelivery
    {
        bool AddContractDelivery(ContractDeliveryInfo entity);

        bool EditContractDelivery(ContractDeliveryInfo entity);

        int DelContractDelivery(int ID);

        ContractDeliveryInfo GetContractDeliveryByID(int ID);

        ContractDeliveryInfo GetContractDeliveryBySN(string SN);

        IList<ContractDeliveryInfo> GetContractDeliverys(QueryInfo Query);

        IList<ContractDeliveryInfo> GetContractDeliverysByContractID(int ContractID);

        PageInfo GetPageInfo(QueryInfo Query);

        int Get_Orders_Goods_DeliveryAmount(int Goods_ID);

        bool AddContractDeliveryGoods(ContractDeliveryGoodsInfo entity);

        bool EditContractDeliveryGoods(ContractDeliveryGoodsInfo entity);

        ContractDeliveryGoodsInfo GetContractDeliveryGoodsByID(int ID);

        IList<ContractDeliveryGoodsInfo> GetContractDeliveryGoodss(QueryInfo Query);

        IList<ContractDeliveryGoodsInfo> GetContractDeliveryGoodssByDeliveryID(int ID);

    }

}
