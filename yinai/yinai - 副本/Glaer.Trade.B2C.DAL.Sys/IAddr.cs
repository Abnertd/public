using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.DAL.Sys
{
    public interface IAddr
    {
        /// <summary>
        /// 通过国家代码查询其下的省，返回所有省
        /// </summary>
        /// <param name="countryCode">国家代码</param>
        /// <returns></returns>
        IList<StateInfo> GetStatesByCountry(string countryCode);

        /// <summary>
        /// 通过省代码查询其下的市，返回所有市
        /// </summary>
        /// <param name="stateCode">省代码</param>
        /// <returns></returns>
        IList<CityInfo> GetCitysByState(string stateCode);

        /// <summary>
        /// 通过市代码查询其下的市，返回所有县区
        /// </summary>
        /// <param name="cityCode">市代码</param>
        /// <returns></returns>
        IList<CountyInfo> GetCountysByCity(string cityCode);

        /// <summary>
        /// 通过省代码查询其信息，返回省的实体
        /// </summary>
        /// <param name="stateCode">省代码</param>
        /// <returns></returns>
        StateInfo GetStateInfoByCode(string stateCode);

        /// <summary>
        /// 通过市代码查询其信息，返回市的实体
        /// </summary>
        /// <param name="cityCode">市代码</param>
        /// <returns></returns>
        CityInfo GetCityInfoByCode(string cityCode);

        /// <summary>
        /// 通过县代码查询其信息，返回县的实体
        /// </summary>
        /// <param name="countyCode">县代码</param>
        /// <returns></returns>
        CountyInfo GetCountyInfoByCode(string countyCode);

        /// <summary>
        /// 获取IP信息
        /// </summary>
        /// <param name="userIP">IP编码</param>
        /// <returns>IP实体信息集合</returns>
        IList<SysIPInfo> GetSysIPs(long userIP);
    }
}
