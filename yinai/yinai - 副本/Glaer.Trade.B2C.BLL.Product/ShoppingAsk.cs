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
    public class ShoppingAsk : IShoppingAsk
    {
        protected DAL.Product.IShoppingAsk MyDAL;
        protected IRBAC RBAC;

        public ShoppingAsk()
        {
            MyDAL = DAL.Product.ShoppingAskFactory.CreateShoppingAsk();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddShoppingAsk(ShoppingAskInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "7da52156-9a1e-46af-bad4-7611cef159e3"))
            {
                return MyDAL.AddShoppingAsk(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：7da52156-9a1e-46af-bad4-7611cef159e3错误");
            }
        }

        public virtual bool EditShoppingAsk(ShoppingAskInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "b2a9d36e-9ba5-45b6-8481-9da1cd12ace0"))
            {
                return MyDAL.EditShoppingAsk(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：b2a9d36e-9ba5-45b6-8481-9da1cd12ace0错误");
            }
        }

        public virtual int DelShoppingAsk(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "9cf98bf5-6f7c-4fbc-973e-a92c9a37c732"))
            {
                return MyDAL.DelShoppingAsk(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：9cf98bf5-6f7c-4fbc-973e-a92c9a37c732错误");
            }
        }

        public virtual ShoppingAskInfo GetShoppingAskByID(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "fe2e0dd7-2773-4748-915a-103065ed0334"))
            {
                return MyDAL.GetShoppingAskByID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：fe2e0dd7-2773-4748-915a-103065ed0334错误");
            }
        }

        public virtual IList<ShoppingAskInfo> GetShoppingAsks(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "fe2e0dd7-2773-4748-915a-103065ed0334"))
            {
                return MyDAL.GetShoppingAsks(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：fe2e0dd7-2773-4748-915a-103065ed0334错误");
            }
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "fe2e0dd7-2773-4748-915a-103065ed0334"))
            {
                return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：fe2e0dd7-2773-4748-915a-103065ed0334错误");
            }
        }

    }
}

