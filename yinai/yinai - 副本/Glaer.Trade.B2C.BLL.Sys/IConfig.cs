using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.BLL.Sys
{
    /// <summary>
    /// 系统配置接口
    /// </summary>
    public interface IConfig
    {
        bool AddConfig(ConfigInfo entity, RBACUserInfo UserPrivilege);

        bool EditConfig(ConfigInfo entity, RBACUserInfo UserPrivilege);

        int DelConfig(int ID, RBACUserInfo UserPrivilege);

        ConfigInfo GetConfigByID(int ID, RBACUserInfo UserPrivilege);

        /// <summary>
        /// 根据站点获得配置信息
        /// </summary>
        /// <param name="Site">站点</param>
        /// <param name="UserPrivilege">用户权限</param>
        /// <returns></returns>
        ConfigInfo GetConfigBySite(string Site, RBACUserInfo UserPrivilege);

        /// <summary>
        /// 根据域名获得配置信息
        /// </summary>
        /// <param name="DomainName">域名</param>
        /// <returns></returns>
        ConfigInfo GetConfigByDomainName(string DomainName, RBACUserInfo UserPrivilege);

        IList<ConfigInfo> GetConfigs(QueryInfo Query, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);
    }

    public interface ISysInterfaceLog
    {
        bool AddSysInterfaceLog(SysInterfaceLogInfo entity);

        bool EditSysInterfaceLog(SysInterfaceLogInfo entity);

        int DelSysInterfaceLog(int ID);

        SysInterfaceLogInfo GetSysInterfaceLogByID(int ID);

        IList<SysInterfaceLogInfo> GetSysInterfaceLogs(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

    }
}
