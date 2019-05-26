using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.DAL;
using Glaer.Trade.B2C.RBAC;

namespace Glaer.Trade.B2C.BLL.ORD
{
    public class PayType : IPayType
    {
        protected DAL.ORD.IPayType MyDAL;
        protected IRBAC RBAC;

        public PayType()
        {
            MyDAL = DAL.ORD.PayTypeFactory.CreatePayType();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddPayType(PayTypeInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "4f0daa32-ae16-4398-ae1e-d2400ca0fff0"))
            {
                return MyDAL.AddPayType(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：4f0daa32-ae16-4398-ae1e-d2400ca0fff0错误");
            } 
        }

        public virtual bool EditPayType(PayTypeInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "fcd3782c-b791-40c6-a29d-9b43092de04f"))
            {
                return MyDAL.EditPayType(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：fcd3782c-b791-40c6-a29d-9b43092de04f错误");
            } 
        }

        public virtual int DelPayType(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "52b0dc84-893a-4f1d-a15d-2023250ac8a6"))
            {
                return MyDAL.DelPayType(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：52b0dc84-893a-4f1d-a15d-2023250ac8a6错误");
            } 
        }

        public virtual PayTypeInfo GetPayTypeByID(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "80924a02-c37c-409b-ac63-43d7d4340fc5"))
            {
                return MyDAL.GetPayTypeByID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：80924a02-c37c-409b-ac63-43d7d4340fc5错误");
            } 
        }

        public virtual IList<PayTypeInfo> GetPayTypes(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "80924a02-c37c-409b-ac63-43d7d4340fc5"))
            {
                return MyDAL.GetPayTypes(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：80924a02-c37c-409b-ac63-43d7d4340fc5错误");
            } 
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "80924a02-c37c-409b-ac63-43d7d4340fc5"))
            {
                return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：80924a02-c37c-409b-ac63-43d7d4340fc5错误");
            } 
        }

    }

}
