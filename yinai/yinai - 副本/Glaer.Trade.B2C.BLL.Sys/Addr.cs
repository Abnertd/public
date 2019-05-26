using System;
using System.Data;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;


namespace Glaer.Trade.B2C.BLL.Sys
{
    public class Addr : IAddr
    {
        protected DAL.Sys.IAddr MyDAL;

        public Addr() {
            MyDAL = DAL.Sys.AddrFactory.CreateAddr();
        }

        public virtual IList<StateInfo> GetStatesByCountry(string countryCode) {
            return MyDAL.GetStatesByCountry(countryCode);

        }

        public virtual IList<CityInfo> GetCitysByState(string stateCode) {
            return MyDAL.GetCitysByState(stateCode);
        }

        public virtual IList<CountyInfo> GetCountysByCity(string cityCode) {
            return MyDAL.GetCountysByCity(cityCode);
        }

        public virtual StateInfo GetStateInfoByCode(string stateCode) {
            return MyDAL.GetStateInfoByCode(stateCode);
        }

        public virtual CityInfo GetCityInfoByCode(string cityCode) {
            return MyDAL.GetCityInfoByCode(cityCode);
        }

        public virtual CountyInfo GetCountyInfoByCode(string countyCode) {
            return MyDAL.GetCountyInfoByCode(countyCode);
        }

        /// <summary>
        /// 获取IP信息
        /// </summary>
        /// <param name="userIP">IP编码</param>
        /// <returns>IP实体信息集合</returns>
        public virtual IList<SysIPInfo> GetSysIPs(long userIP)
        {
            return MyDAL.GetSysIPs(userIP);
        }

    }
}
