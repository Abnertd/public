using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.RBAC;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.B2C.DAL;

namespace Glaer.Trade.B2C.BLL.Sys
{
    public class Sources : ISources
    {
        protected DAL.Sys.ISources MyDAL;
        protected IRBAC RBAC;

        public Sources()
        {
            MyDAL = DAL.Sys.SourcesFactory.CreateSources();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddSources(SourcesInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "97e064d7-080e-4c2e-a700-1fc5e7d318fc"))
            {
                return MyDAL.AddSources(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：97e064d7-080e-4c2e-a700-1fc5e7d318fc错误");
            }
        }

        public virtual bool EditSources(SourcesInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "485f0ee2-f5a3-41a0-a778-68a87c5b5d89"))
            {
                return MyDAL.EditSources(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：485f0ee2-f5a3-41a0-a778-68a87c5b5d89错误");
            }
        }

        public virtual int DelSources(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "820c7d2f-a000-4122-858b-ff98a77c7eb1"))
            {
                return MyDAL.DelSources(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：820c7d2f-a000-4122-858b-ff98a77c7eb1错误");
            }
        }

        public virtual SourcesInfo GetSourcesByID(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "51ce468d-a37a-42dd-9bef-3a0d7ab4eff7"))
            {
                return MyDAL.GetSourcesByID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：51ce468d-a37a-42dd-9bef-3a0d7ab4eff7错误");
            }
        }

        public virtual SourcesInfo GetSourcesByCode(string Code)
        {
            return MyDAL.GetSourcesByCode(Code);
        }

        public virtual IList<SourcesInfo> GetSourcess(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "51ce468d-a37a-42dd-9bef-3a0d7ab4eff7"))
            {
                return MyDAL.GetSourcess(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：51ce468d-a37a-42dd-9bef-3a0d7ab4eff7错误");
            }
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "51ce468d-a37a-42dd-9bef-3a0d7ab4eff7"))
            {
                return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：51ce468d-a37a-42dd-9bef-3a0d7ab4eff7错误");
            }
        }

    }

}
