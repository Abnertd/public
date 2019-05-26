using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.DAL.Sys
{
    public class Addr : IAddr
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public Addr() {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual IList<StateInfo> GetStatesByCountry(string countryCode) {
            IList<StateInfo> entitys = null;
            StateInfo entity = null;
            string SqlList = "SELECT Sys_State_CountryCode, Sys_State_Code, Sys_State_CN FROM Sys_State WHERE Sys_State_CountryCode = '" + countryCode + "'";
            DataTable DtList = new DataTable();
            try { 
                DtList = DBHelper.Query(SqlList);

                if (DtList.Rows.Count > 0)
                    entitys = new List<StateInfo>();

                foreach (DataRow DrList in DtList.Rows) {
                    entity = new StateInfo();
                    entity.State_CountryCode = Tools.NullStr(DrList["Sys_State_CountryCode"]);
                    entity.State_Code = Tools.NullStr(DrList["Sys_State_Code"]);
                    entity.State_CN = Tools.NullStr(DrList["Sys_State_CN"]);
                    entitys.Add(entity);
                    entity = null;
                }
                return entitys; 
            }
            catch (Exception ex) { throw ex; }
            finally {  DtList = null; }
        }

        public virtual IList<CityInfo> GetCitysByState(string stateCode) {
            IList<CityInfo> entitys = null;
            CityInfo entity = null;
            string SqlList = "SELECT Sys_City_StateCode, Sys_City_Code, Sys_City_CN FROM Sys_City WHERE Sys_City_StateCode = '" + stateCode + "'";
            DataTable DtList = new DataTable();
            try {
                DtList = DBHelper.Query(SqlList);

                if (DtList.Rows.Count > 0)
                    entitys = new List<CityInfo>();

                foreach (DataRow DrList in DtList.Rows) {
                    entity = new CityInfo();
                    entity.City_StateCode = Tools.NullStr(DrList["Sys_City_StateCode"]);
                    entity.City_Code = Tools.NullStr(DrList["Sys_City_Code"]);
                    entity.City_CN = Tools.NullStr(DrList["Sys_City_CN"]);
                    entitys.Add(entity);
                    entity = null;
                }

                return entitys;
            }
            catch (Exception ex) { throw ex; }
            finally { DtList = null; }
        }

        public virtual IList<CountyInfo> GetCountysByCity(string cityCode) {
            IList<CountyInfo> entitys = null;
            CountyInfo entity = null;
            string SqlList = "SELECT Sys_County_CityCode, Sys_County_Code, Sys_County_CN FROM Sys_County WHERE Sys_County_CityCode = '" + cityCode + "'";
            DataTable DtList = new DataTable();
            try {
                DtList = DBHelper.Query(SqlList);

                if (DtList.Rows.Count > 0)
                    entitys = new List<CountyInfo>();

                foreach (DataRow DrList in DtList.Rows) {
                    entity = new CountyInfo();
                    entity.County_CityCode = Tools.NullStr(DrList["Sys_County_CityCode"]);
                    entity.County_Code = Tools.NullStr(DrList["Sys_County_Code"]);
                    entity.County_CN = Tools.NullStr(DrList["Sys_County_CN"]);
                    entitys.Add(entity);
                    entity = null;
                }

                return entitys;
            }
            catch (Exception ex) { throw ex; }
            finally { DtList = null; }
        }

        public virtual StateInfo GetStateInfoByCode(string stateCode) {
            StateInfo entity = null;
            string SqlList = "SELECT Sys_State_CountryCode, Sys_State_Code, Sys_State_CN FROM Sys_State WHERE Sys_State_Code = '" + stateCode + "'";
            DataTable DtList = DBHelper.Query(SqlList);
            if (DtList.Rows.Count > 0) {
                entity = new StateInfo();
                entity.State_CountryCode = Tools.NullStr(DtList.Rows[0]["Sys_State_CountryCode"]);
                entity.State_Code = Tools.NullStr(DtList.Rows[0]["Sys_State_Code"]);
                entity.State_CN = Tools.NullStr(DtList.Rows[0]["Sys_State_CN"]);
            }
            return entity;
        }

        public virtual CityInfo GetCityInfoByCode(string cityCode)
        {
            CityInfo entity = null;
            string SqlList = "SELECT Sys_City_StateCode, Sys_City_Code, Sys_City_CN FROM Sys_City WHERE Sys_City_Code = '" + cityCode + "'";
            DataTable DtList = DBHelper.Query(SqlList);
            if (DtList.Rows.Count > 0) {
                entity = new CityInfo();
                entity.City_StateCode = Tools.NullStr(DtList.Rows[0]["Sys_City_StateCode"]);
                entity.City_Code = Tools.NullStr(DtList.Rows[0]["Sys_City_Code"]);
                entity.City_CN = Tools.NullStr(DtList.Rows[0]["Sys_City_CN"]);
            }
            return entity;
        }

        public virtual CountyInfo GetCountyInfoByCode(string countyCode)
        {
            CountyInfo entity = null;
            string SqlList = "SELECT Sys_County_CityCode, Sys_County_Code, Sys_County_CN FROM Sys_County WHERE Sys_County_Code = '" + countyCode + "'";
            DataTable DtList = DBHelper.Query(SqlList);
            if (DtList.Rows.Count > 0) {
                entity = new CountyInfo();
                entity.County_CityCode = Tools.NullStr(DtList.Rows[0]["Sys_County_CityCode"]);
                entity.County_Code = Tools.NullStr(DtList.Rows[0]["Sys_County_Code"]);
                entity.County_CN = Tools.NullStr(DtList.Rows[0]["Sys_County_CN"]);
            }
            return entity;
        }

        /// <summary>
        /// 获取IP信息
        /// </summary>
        /// <param name="userIP">IP编码</param>
        /// <returns>IP实体信息集合</returns>
        public virtual IList<SysIPInfo> GetSysIPs(long userIP)
        {
            IList<SysIPInfo> entitys = null;
            SysIPInfo entity = null;
            string SqlList;
            SqlDataReader RdrList = null;
            try
            {
                SqlList = "select * from Sys_IP where (onip<=" + userIP + ") and (offip>=" + userIP + ")";
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SysIPInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SysIPInfo();
                        entity.oniptxt = Tools.NullStr(RdrList["oniptxt"]);
                        entity.offiptxt = Tools.NullStr(RdrList["offiptxt"]);
                        entity.ProvinceID = Tools.NullInt(RdrList["ProvinceID"]);
                        entity.CityID = Tools.NullInt(RdrList["CityID"]);
                        entity.CountyID = Tools.NullInt(RdrList["CountyID"]);
                        entity.country = Tools.NullStr(RdrList["country"]);
                        entity.city = Tools.NullStr(RdrList["city"]);

                        entitys.Add(entity);
                        entity = null;
                    }
                }
                return entitys;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (RdrList != null)
                {
                    RdrList.Close();
                    RdrList = null;
                }
            }
        }

    }
}
