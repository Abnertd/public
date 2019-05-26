using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.RBAC;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.B2C.DAL;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public class SupplierMessage : ISupplierMessage
    {
        protected DAL.MEM.ISupplierMessage MyDAL;
        protected IRBAC RBAC;

        public SupplierMessage()
        {
            MyDAL = DAL.MEM.SupplierMessageFactory.CreateSupplierMessage();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddSupplierMessage(SupplierMessageInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "11fe78b3-c971-4ed1-bb5e-3a31b60b19cd"))
            {
                return MyDAL.AddSupplierMessage(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：11fe78b3-c971-4ed1-bb5e-3a31b60b19cd错误");
            } 
        }

        public virtual bool EditSupplierMessage(SupplierMessageInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "b7d38ac5-000c-4d07-9ca3-46df47367554"))
            {
                return MyDAL.EditSupplierMessage(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：b7d38ac5-000c-4d07-9ca3-46df47367554错误");
            } 
        }

        public virtual int DelSupplierMessage(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "ba7a4b2e-b6d1-473d-b0ba-2d3041c30aa7"))
            {
                return MyDAL.DelSupplierMessage(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：ba7a4b2e-b6d1-473d-b0ba-2d3041c30aa7错误");
            } 
        }

        public virtual SupplierMessageInfo GetSupplierMessageByID(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "d8b3c47b-26c4-435f-884e-c9951464b633"))
            {
                return MyDAL.GetSupplierMessageByID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：d8b3c47b-26c4-435f-884e-c9951464b633错误");
            } 
        }

        public virtual IList<SupplierMessageInfo> GetSupplierMessages(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "d8b3c47b-26c4-435f-884e-c9951464b633"))
            {
                return MyDAL.GetSupplierMessages(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：d8b3c47b-26c4-435f-884e-c9951464b633错误");
            } 
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "d8b3c47b-26c4-435f-884e-c9951464b633"))
            {
                return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：d8b3c47b-26c4-435f-884e-c9951464b633错误");
            }
        }

    }
}

