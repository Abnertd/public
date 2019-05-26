using System;
using System.Data;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.RBAC;


namespace Glaer.Trade.B2C.BLL.Sys
{
    public class Config : IConfig
    {
        protected DAL.Sys.IConfig MyDAL;
        protected IRBAC RBAC;

        public Config()
        {
            MyDAL = DAL.Sys.ConfigFactory.CreateConfig();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddConfig(ConfigInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "ef84a07f-6281-4f54-84f9-c345adf9d765"))
            {
                return MyDAL.AddConfig(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：ef84a07f-6281-4f54-84f9-c345adf9d765错误");
            }
        }

        public virtual bool EditConfig(ConfigInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "ef84a07f-6281-4f54-84f9-c345adf9d765"))
            {
                return MyDAL.EditConfig(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：ef84a07f-6281-4f54-84f9-c345adf9d765错误");
            }
        }

        public virtual int DelConfig(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "ef84a07f-6281-4f54-84f9-c345adf9d765"))
            {
                return MyDAL.DelConfig(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：ef84a07f-6281-4f54-84f9-c345adf9d765错误");
            }
        }

        public virtual ConfigInfo GetConfigByID(int ID, RBACUserInfo UserPrivilege)
        {
            return MyDAL.GetConfigByID(ID);
        }

        /// <summary>
        /// 根据站点获得配置信息
        /// </summary>
        /// <param name="Site">站点</param>
        /// <param name="UserPrivilege">用户权限</param>
        /// <returns></returns>
        public virtual ConfigInfo GetConfigBySite(string Site, RBACUserInfo UserPrivilege)
        {
            return MyDAL.GetConfigBySite(Site);
        }

        /// <summary>
        /// 根据域名获得配置信息
        /// </summary>
        /// <param name="Site">域名</param>
        /// <param name="UserPrivilege">用户权限</param>
        /// <returns></returns>
        public virtual ConfigInfo GetConfigByDomainName(string DomainName, RBACUserInfo UserPrivilege)
        {
            return MyDAL.GetConfigByDomainName(DomainName);
        }

        public virtual IList<ConfigInfo> GetConfigs(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "ef84a07f-6281-4f54-84f9-c345adf9d765"))
            {
                return MyDAL.GetConfigs(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：ef84a07f-6281-4f54-84f9-c345adf9d765错误");
            }
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "ef84a07f-6281-4f54-84f9-c345adf9d765"))
            {
                return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：ef84a07f-6281-4f54-84f9-c345adf9d765错误");
            }
        }

    }

    public class SysInterfaceLog : ISysInterfaceLog
    {
        protected DAL.Sys.ISysInterfaceLog MyDAL;
        protected IRBAC RBAC;

        public SysInterfaceLog()
        {
            MyDAL = DAL.Sys.SysInterfaceLogFactory.CreateSysInterfaceLog();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddSysInterfaceLog(SysInterfaceLogInfo entity)
        {
            return MyDAL.AddSysInterfaceLog(entity);
        }

        public virtual bool EditSysInterfaceLog(SysInterfaceLogInfo entity)
        {
            return MyDAL.EditSysInterfaceLog(entity);
        }

        public virtual int DelSysInterfaceLog(int ID)
        {
            return MyDAL.DelSysInterfaceLog(ID);
        }

        public virtual SysInterfaceLogInfo GetSysInterfaceLogByID(int ID)
        {
            return MyDAL.GetSysInterfaceLogByID(ID);
        }

        public virtual IList<SysInterfaceLogInfo> GetSysInterfaceLogs(QueryInfo Query)
        {
            return MyDAL.GetSysInterfaceLogs(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }
    }

}
