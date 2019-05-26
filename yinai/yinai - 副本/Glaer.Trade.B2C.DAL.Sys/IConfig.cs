using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.DAL.Sys
{
    /// <summary>
    /// 系统配置接口
    /// </summary>
    public interface IConfig
    {
        bool AddConfig(ConfigInfo entity);

        bool EditConfig(ConfigInfo entity);

        int DelConfig(int ID);

        ConfigInfo GetConfigByID(int ID);

        /// <summary>
        /// 根据站点获得配置信息
        /// </summary>
        /// <param name="Site">站点</param>
        /// <returns></returns>
        ConfigInfo GetConfigBySite(string Site);

        /// <summary>
        /// 根据域名获得配置信息
        /// </summary>
        /// <param name="DomainName">域名</param>
        /// <returns></returns>
        ConfigInfo GetConfigByDomainName(string DomainName);

        IList<ConfigInfo> GetConfigs(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
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
