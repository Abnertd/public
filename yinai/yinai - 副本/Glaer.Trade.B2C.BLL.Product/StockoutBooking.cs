using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.Util.Mail;
using Glaer.Trade.B2C.DAL;
using Glaer.Trade.B2C.RBAC;

namespace Glaer.Trade.B2C.BLL.Product
{
    public class StockoutBooking : IStockoutBooking
    {
        protected DAL.Product.IStockoutBooking MyDAL;
        protected IRBAC RBAC;

        public StockoutBooking()
        {
            MyDAL = DAL.Product.StockoutBookingFactory.CreateStockoutBooking();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddStockoutBooking(StockoutBookingInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "342a2ee7-c8eb-4ed6-8ecc-eac99e7623ff"))
            {
                return MyDAL.AddStockoutBooking(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：342a2ee7-c8eb-4ed6-8ecc-eac99e7623ff错误");
            }
        }

        public virtual bool EditStockoutBooking(StockoutBookingInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "1da083a0-f751-4a93-995e-ca3c1edf44cc"))
            {
                return MyDAL.EditStockoutBooking(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：1da083a0-f751-4a93-995e-ca3c1edf44cc错误");
            }
        }

        public virtual int DelStockoutBooking(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "cd25a138-603d-445c-83f9-736de139c4c1"))
            {
                return MyDAL.DelStockoutBooking(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：cd25a138-603d-445c-83f9-736de139c4c1错误");
            }
        }

        public virtual StockoutBookingInfo GetStockoutBookingByID(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "6f896c98-c62f-43f6-a276-39e43697c771"))
            {
                return MyDAL.GetStockoutBookingByID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：6f896c98-c62f-43f6-a276-39e43697c771错误");
            }
        }

        public virtual IList<StockoutBookingInfo> GetStockoutBookings(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "6f896c98-c62f-43f6-a276-39e43697c771"))
            {
                return MyDAL.GetStockoutBookings(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：6f896c98-c62f-43f6-a276-39e43697c771错误");
            }
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "6f896c98-c62f-43f6-a276-39e43697c771"))
            {
                return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：6f896c98-c62f-43f6-a276-39e43697c771错误");
            }
        }

    }
}

