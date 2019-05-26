using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.DAL;
using Glaer.Trade.B2C.RBAC;

namespace Glaer.Trade.B2C.BLL.ORD
{
    public class OrdersDelivery : IOrdersDelivery
    {
        protected DAL.ORD.IOrdersDelivery MyDAL;
        protected IRBAC RBAC;

        public OrdersDelivery() {
            MyDAL = DAL.ORD.OrdersDeliveryFactory.CreateOrdersDelivery();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddOrdersDelivery(OrdersDeliveryInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "800fdc63-fa5d-44de-927e-8d4560c2f238"))
            {
                return MyDAL.AddOrdersDelivery(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：800fdc63-fa5d-44de-927e-8d4560c2f238错误");
            }
        }

        public virtual bool EditOrdersDelivery(OrdersDeliveryInfo entity, RBACUserInfo UserPrivilege)
        {
            return MyDAL.EditOrdersDelivery(entity);
        }

        public virtual int DelOrdersDelivery(int ID, RBACUserInfo UserPrivilege)
        {
            return MyDAL.DelOrdersDelivery(ID);
        }

        public virtual OrdersDeliveryInfo GetOrdersDeliveryByID(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "f606309a-2aa9-42e3-9d45-e0f306682a29"))
            {
                return MyDAL.GetOrdersDeliveryByID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：f606309a-2aa9-42e3-9d45-e0f306682a29错误");
            }
        }

        public virtual OrdersDeliveryInfo GetOrdersDeliveryBySn(string SN, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "f606309a-2aa9-42e3-9d45-e0f306682a29"))
            {
                return MyDAL.GetOrdersDeliveryBySn(SN);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：f606309a-2aa9-42e3-9d45-e0f306682a29错误");
            }
        }

        public virtual OrdersDeliveryInfo GetOrdersDeliveryByOrdersID(int Orders_ID, int Delivery_Status, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "f606309a-2aa9-42e3-9d45-e0f306682a29"))
            {
                return MyDAL.GetOrdersDeliveryByOrdersID(Orders_ID, Delivery_Status);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：f606309a-2aa9-42e3-9d45-e0f306682a29错误");
            }
        }

        public virtual IList<OrdersDeliveryInfo> GetOrdersDeliverys(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "f606309a-2aa9-42e3-9d45-e0f306682a29"))
            {
                return MyDAL.GetOrdersDeliverys(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：f606309a-2aa9-42e3-9d45-e0f306682a29错误");
            }
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "f606309a-2aa9-42e3-9d45-e0f306682a29"))
            {
                return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：f606309a-2aa9-42e3-9d45-e0f306682a29错误");
            }
        }

        public virtual bool AddOrdersDeliveryGoods(IList<OrdersDeliveryGoodsInfo> entity)
        {
            return MyDAL.AddOrdersDeliveryGoods(entity);
        }

        public virtual bool EditOrdersDeliveryGoods(OrdersDeliveryGoodsInfo entity)
        {
            return MyDAL.EditOrdersDeliveryGoods(entity);
        }

        public virtual int DelOrdersDeliveryGoods(int ID)
        {
            return MyDAL.DelOrdersDeliveryGoods(ID);
        }

        public virtual OrdersDeliveryGoodsInfo GetOrdersDeliveryGoodsByID(int ID)
        {
            return MyDAL.GetOrdersDeliveryGoodsByID(ID);
        }

        public virtual IList<OrdersDeliveryGoodsInfo> GetOrdersDeliveryGoods(int DeliveryID)
        {
            return MyDAL.GetOrdersDeliveryGoods(DeliveryID);
        }

    }

}
