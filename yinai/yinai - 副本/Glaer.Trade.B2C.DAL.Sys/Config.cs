using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.DAL.Sys
{
    public class Config : IConfig
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public Config()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddConfig(ConfigInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Sys_Config";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();
            DrAdd["Upload_Server_URL"] = entity.Upload_Server_URL;
            DrAdd["Upload_Server_Return_WWW"] = entity.Upload_Server_Return_WWW;
            DrAdd["Upload_Server_Return_Admin"] = entity.Upload_Server_Return_Admin;
            DrAdd["Upload_Server_Return_Supplier"] = entity.Upload_Server_Return_Supplier;
            DrAdd["Language_Define"] = entity.Language_Define;
            DrAdd["Site_DomainName"] = entity.Site_DomainName;
            DrAdd["Site_Name"] = entity.Site_Name;
            DrAdd["Site_URL"] = entity.Site_URL;
            DrAdd["Site_Logo"] = entity.Site_Logo;
            DrAdd["Site_Tel"] = entity.Site_Tel;
            DrAdd["Site_Fax"] = entity.Site_Fax;
            DrAdd["Site_Email"] = entity.Site_Email;
            DrAdd["Site_Keyword"] = entity.Site_Keyword;
            DrAdd["Site_Description"] = entity.Site_Description;
            DrAdd["Site_Title"] = entity.Site_Title;
            DrAdd["Mail_Server"] = entity.Mail_Server;
            DrAdd["Mail_ServerPort"] = entity.Mail_ServerPort;
            DrAdd["Mail_EnableSsl"] = entity.Mail_EnableSsl;
            DrAdd["Mail_ServerUserName"] = entity.Mail_ServerUserName;
            DrAdd["Mail_ServerPassWord"] = entity.Mail_ServerPassWord;
            DrAdd["Mail_FromName"] = entity.Mail_FromName;
            DrAdd["Mail_From"] = entity.Mail_From;
            DrAdd["Mail_Replyto"] = entity.Mail_Replyto;
            DrAdd["Mail_Encode"] = entity.Mail_Encode;
            DrAdd["Coin_Name"] = entity.Coin_Name;
            DrAdd["Coin_Rate"] = entity.Coin_Rate;
            DrAdd["Trade_Contract_IsActive"] = entity.Trade_Contract_IsActive;
            DrAdd["Sys_Config_Site"] = entity.Sys_Config_Site;
            DrAdd["Static_IsEnable"] = entity.Static_IsEnable;
            DrAdd["Chinabank_Code"] = entity.Chinabank_Code;
            DrAdd["Chinabank_Key"] = entity.Chinabank_Key;
            DrAdd["Alipay_Email"] = entity.Alipay_Email;
            DrAdd["Alipay_Code"] = entity.Alipay_Code;
            DrAdd["Alipay_Key"] = entity.Alipay_Key;
            DrAdd["Tenpay_Code"] = entity.Tenpay_Code;
            DrAdd["Tenpay_Key"] = entity.Tenpay_Key;
            DrAdd["CreditPayment_Code"] = entity.CreditPayment_Code;
            DrAdd["RepidBuy_IsEnable"] = entity.RepidBuy_IsEnable;
            DrAdd["Coupon_UsedAmount"] = entity.Coupon_UsedAmount;
            DrAdd["Sys_Sensitive_Keyword"] = entity.Sys_Sensitive_Keyword;
            DrAdd["Shop_Second_Domain"] = entity.Shop_Second_Domain;
            DrAdd["Keyword_Adv_MinPrice"] = entity.Keyword_Adv_MinPrice;
            DrAdd["Instant_GoldPrice"] = entity.Instant_GoldPrice;
            DrAdd["Instant_SilverPrice"] = entity.Instant_SilverPrice;
            DrAdd["Sys_Delivery_Code"] = entity.Sys_Delivery_Code;
            DtAdd.Rows.Add(DrAdd);
            try {
                DBHelper.SaveChanges(SqlAdd, DtAdd);
                return true;
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                DtAdd.Dispose();
            }
        }

        public virtual bool EditConfig(ConfigInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Sys_Config WHERE Sys_Config_ID = " + entity.Sys_Config_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try {
                if (DtAdd.Rows.Count > 0) {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Upload_Server_URL"] = entity.Upload_Server_URL;
                    DrAdd["Upload_Server_Return_WWW"] = entity.Upload_Server_Return_WWW;
                    DrAdd["Upload_Server_Return_Admin"] = entity.Upload_Server_Return_Admin;
                    DrAdd["Upload_Server_Return_Supplier"] = entity.Upload_Server_Return_Supplier;
                    DrAdd["Language_Define"] = entity.Language_Define;
                    DrAdd["Site_DomainName"] = entity.Site_DomainName;
                    DrAdd["Site_Name"] = entity.Site_Name;
                    DrAdd["Site_URL"] = entity.Site_URL;
                    DrAdd["Site_Logo"] = entity.Site_Logo;
                    DrAdd["Site_Tel"] = entity.Site_Tel;
                    DrAdd["Site_Fax"] = entity.Site_Fax;
                    DrAdd["Site_Email"] = entity.Site_Email;
                    DrAdd["Site_Keyword"] = entity.Site_Keyword;
                    DrAdd["Site_Description"] = entity.Site_Description;
                    DrAdd["Site_Title"] = entity.Site_Title;
                    DrAdd["Mail_Server"] = entity.Mail_Server;
                    DrAdd["Mail_ServerPort"] = entity.Mail_ServerPort;
                    DrAdd["Mail_EnableSsl"] = entity.Mail_EnableSsl;
                    DrAdd["Mail_ServerUserName"] = entity.Mail_ServerUserName;
                    DrAdd["Mail_ServerPassWord"] = entity.Mail_ServerPassWord;
                    DrAdd["Mail_FromName"] = entity.Mail_FromName;
                    DrAdd["Mail_From"] = entity.Mail_From;
                    DrAdd["Mail_Replyto"] = entity.Mail_Replyto;
                    DrAdd["Mail_Encode"] = entity.Mail_Encode;
                    DrAdd["Coin_Name"] = entity.Coin_Name;
                    DrAdd["Coin_Rate"] = entity.Coin_Rate;
                    DrAdd["Trade_Contract_IsActive"] = entity.Trade_Contract_IsActive;
                    DrAdd["Sys_Config_Site"] = entity.Sys_Config_Site;
                    DrAdd["Static_IsEnable"] = entity.Static_IsEnable;
                    DrAdd["Chinabank_Code"] = entity.Chinabank_Code;
                    DrAdd["Chinabank_Key"] = entity.Chinabank_Key;
                    DrAdd["Alipay_Email"] = entity.Alipay_Email;
                    DrAdd["Alipay_Code"] = entity.Alipay_Code;
                    DrAdd["Alipay_Key"] = entity.Alipay_Key;
                    DrAdd["Tenpay_Code"] = entity.Tenpay_Code;
                    DrAdd["Tenpay_Key"] = entity.Tenpay_Key;
                    DrAdd["CreditPayment_Code"] = entity.CreditPayment_Code;
                    DrAdd["RepidBuy_IsEnable"] = entity.RepidBuy_IsEnable;
                    DrAdd["Coupon_UsedAmount"] = entity.Coupon_UsedAmount;
                    DrAdd["Sys_Sensitive_Keyword"] = entity.Sys_Sensitive_Keyword;
                    DrAdd["Shop_Second_Domain"] = entity.Shop_Second_Domain;
                    DrAdd["Keyword_Adv_MinPrice"] = entity.Keyword_Adv_MinPrice;
                    DrAdd["Sys_Delivery_Code"] = entity.Sys_Delivery_Code;
                    DrAdd["Instant_GoldPrice"] = entity.Instant_GoldPrice;
                    DrAdd["Instant_SilverPrice"] = entity.Instant_SilverPrice;
                    DBHelper.SaveChanges(SqlAdd, DtAdd);
                    return true;
                }
                else {
                    return false;
                }
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                DtAdd.Dispose();
            }
        }

        public virtual int DelConfig(int ID)
        {
            string SqlAdd = "DELETE FROM Sys_Config WHERE Sys_Config_ID = " + ID;
            try {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex) {
                throw ex;
                //TraceError.Log(ex);
                //return 0;
            }
        }

        public virtual ConfigInfo GetConfigByID(int ID)
        {
            ConfigInfo entity = null;
            SqlDataReader RdrList = null;
            try {
                string SqlList;
                SqlList = "SELECT * FROM Sys_Config WHERE Sys_Config_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read()) {
                    entity = new ConfigInfo();
                    entity.Sys_Config_ID = Tools.NullInt(RdrList["Sys_Config_ID"]);
                    entity.Upload_Server_URL = Tools.NullStr(RdrList["Upload_Server_URL"]);
                    entity.Upload_Server_Return_WWW = Tools.NullStr(RdrList["Upload_Server_Return_WWW"]);
                    entity.Upload_Server_Return_Admin = Tools.NullStr(RdrList["Upload_Server_Return_Admin"]);
                    entity.Upload_Server_Return_Supplier = Tools.NullStr(RdrList["Upload_Server_Return_Supplier"]);
                    entity.Language_Define = Tools.NullStr(RdrList["Language_Define"]);
                    entity.Site_DomainName = Tools.NullStr(RdrList["Site_DomainName"]);
                    entity.Site_Name = Tools.NullStr(RdrList["Site_Name"]);
                    entity.Site_URL = Tools.NullStr(RdrList["Site_URL"]);
                    entity.Site_Logo = Tools.NullStr(RdrList["Site_Logo"]);
                    entity.Site_Tel = Tools.NullStr(RdrList["Site_Tel"]);
                    entity.Site_Fax = Tools.NullStr(RdrList["Site_Fax"]);
                    entity.Site_Email = Tools.NullStr(RdrList["Site_Email"]);
                    entity.Site_Keyword = Tools.NullStr(RdrList["Site_Keyword"]);
                    entity.Site_Description = Tools.NullStr(RdrList["Site_Description"]);
                    entity.Site_Title = Tools.NullStr(RdrList["Site_Title"]);
                    entity.Mail_Server = Tools.NullStr(RdrList["Mail_Server"]);
                    entity.Mail_ServerPort = Tools.NullInt(RdrList["Mail_ServerPort"]);
                    entity.Mail_EnableSsl = Tools.NullInt(RdrList["Mail_EnableSsl"]);
                    entity.Mail_ServerUserName = Tools.NullStr(RdrList["Mail_ServerUserName"]);
                    entity.Mail_ServerPassWord = Tools.NullStr(RdrList["Mail_ServerPassWord"]);
                    entity.Mail_FromName = Tools.NullStr(RdrList["Mail_FromName"]);
                    entity.Mail_From = Tools.NullStr(RdrList["Mail_From"]);
                    entity.Mail_Replyto = Tools.NullStr(RdrList["Mail_Replyto"]);
                    entity.Mail_Encode = Tools.NullStr(RdrList["Mail_Encode"]);
                    entity.Coin_Name = Tools.NullStr(RdrList["Coin_Name"]);
                    entity.Coin_Rate = Tools.NullInt(RdrList["Coin_Rate"]);
                    entity.Trade_Contract_IsActive = Tools.NullInt(RdrList["Trade_Contract_IsActive"]);
                    entity.Sys_Config_Site = Tools.NullStr(RdrList["Sys_Config_Site"]);
                    entity.Static_IsEnable = Tools.NullInt(RdrList["Static_IsEnable"]);
                    entity.Chinabank_Code = Tools.NullStr(RdrList["Chinabank_Code"]);
                    entity.Chinabank_Key = Tools.NullStr(RdrList["Chinabank_Key"]);
                    entity.Alipay_Email = Tools.NullStr(RdrList["Alipay_Email"]);
                    entity.Alipay_Code = Tools.NullStr(RdrList["Alipay_Code"]);
                    entity.Alipay_Key = Tools.NullStr(RdrList["Alipay_Key"]);
                    entity.Tenpay_Code = Tools.NullStr(RdrList["Tenpay_Code"]);
                    entity.Tenpay_Key = Tools.NullStr(RdrList["Tenpay_Key"]);
                    entity.CreditPayment_Code = Tools.NullStr(RdrList["CreditPayment_Code"]);
                    entity.RepidBuy_IsEnable = Tools.NullInt(RdrList["RepidBuy_IsEnable"]);
                    entity.Coupon_UsedAmount = Tools.NullInt(RdrList["Coupon_UsedAmount"]);
                    entity.Sys_Sensitive_Keyword = Tools.NullStr(RdrList["Sys_Sensitive_Keyword"]);
                    entity.Shop_Second_Domain = Tools.NullStr(RdrList["Shop_Second_Domain"]);
                    entity.Keyword_Adv_MinPrice = Tools.NullDbl(RdrList["Keyword_Adv_MinPrice"]);
                    entity.Sys_Delivery_Code = Tools.NullStr(RdrList["Sys_Delivery_Code"]);
                    entity.Instant_GoldPrice = Tools.NullDbl(RdrList["Instant_GoldPrice"]);
                    entity.Instant_SilverPrice = Tools.NullDbl(RdrList["Instant_SilverPrice"]);
                }
                return entity;
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                if (RdrList != null) {
                    RdrList.Close();
                    RdrList = null;
                }
            }
        }

        /// <summary>
        /// 根据站点获得配置信息
        /// </summary>
        /// <param name="Site">站点</param>
        /// <returns></returns>
        public virtual ConfigInfo GetConfigBySite(string Site)
        {
            ConfigInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT TOP 1 * FROM Sys_Config WHERE Sys_Config_Site = '" + Site + "' ORDER BY Sys_Config_ID DESC";
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new ConfigInfo();
                    entity.Sys_Config_ID = Tools.NullInt(RdrList["Sys_Config_ID"]);
                    entity.Upload_Server_URL = Tools.NullStr(RdrList["Upload_Server_URL"]);
                    entity.Upload_Server_Return_WWW = Tools.NullStr(RdrList["Upload_Server_Return_WWW"]);
                    entity.Upload_Server_Return_Admin = Tools.NullStr(RdrList["Upload_Server_Return_Admin"]);
                    entity.Upload_Server_Return_Supplier = Tools.NullStr(RdrList["Upload_Server_Return_Supplier"]);
                    entity.Language_Define = Tools.NullStr(RdrList["Language_Define"]);
                    entity.Site_DomainName = Tools.NullStr(RdrList["Site_DomainName"]);
                    entity.Site_Name = Tools.NullStr(RdrList["Site_Name"]);
                    entity.Site_URL = Tools.NullStr(RdrList["Site_URL"]);
                    entity.Site_Logo = Tools.NullStr(RdrList["Site_Logo"]);
                    entity.Site_Tel = Tools.NullStr(RdrList["Site_Tel"]);
                    entity.Site_Fax = Tools.NullStr(RdrList["Site_Fax"]);
                    entity.Site_Email = Tools.NullStr(RdrList["Site_Email"]);
                    entity.Site_Keyword = Tools.NullStr(RdrList["Site_Keyword"]);
                    entity.Site_Description = Tools.NullStr(RdrList["Site_Description"]);
                    entity.Site_Title = Tools.NullStr(RdrList["Site_Title"]);
                    entity.Mail_Server = Tools.NullStr(RdrList["Mail_Server"]);
                    entity.Mail_ServerPort = Tools.NullInt(RdrList["Mail_ServerPort"]);
                    entity.Mail_EnableSsl = Tools.NullInt(RdrList["Mail_EnableSsl"]);
                    entity.Mail_ServerUserName = Tools.NullStr(RdrList["Mail_ServerUserName"]);
                    entity.Mail_ServerPassWord = Tools.NullStr(RdrList["Mail_ServerPassWord"]);
                    entity.Mail_FromName = Tools.NullStr(RdrList["Mail_FromName"]);
                    entity.Mail_From = Tools.NullStr(RdrList["Mail_From"]);
                    entity.Mail_Replyto = Tools.NullStr(RdrList["Mail_Replyto"]);
                    entity.Mail_Encode = Tools.NullStr(RdrList["Mail_Encode"]);
                    entity.Coin_Name = Tools.NullStr(RdrList["Coin_Name"]);
                    entity.Coin_Rate = Tools.NullInt(RdrList["Coin_Rate"]);
                    entity.Trade_Contract_IsActive = Tools.NullInt(RdrList["Trade_Contract_IsActive"]);
                    entity.Sys_Config_Site = Tools.NullStr(RdrList["Sys_Config_Site"]);
                    entity.Static_IsEnable = Tools.NullInt(RdrList["Static_IsEnable"]);
                    entity.Chinabank_Code = Tools.NullStr(RdrList["Chinabank_Code"]);
                    entity.Chinabank_Key = Tools.NullStr(RdrList["Chinabank_Key"]);
                    entity.Alipay_Email = Tools.NullStr(RdrList["Alipay_Email"]);
                    entity.Alipay_Code = Tools.NullStr(RdrList["Alipay_Code"]);
                    entity.Alipay_Key = Tools.NullStr(RdrList["Alipay_Key"]);
                    entity.Tenpay_Code = Tools.NullStr(RdrList["Tenpay_Code"]);
                    entity.Tenpay_Key = Tools.NullStr(RdrList["Tenpay_Key"]);
                    entity.CreditPayment_Code = Tools.NullStr(RdrList["CreditPayment_Code"]);
                    entity.RepidBuy_IsEnable = Tools.NullInt(RdrList["RepidBuy_IsEnable"]);
                    entity.Coupon_UsedAmount = Tools.NullInt(RdrList["Coupon_UsedAmount"]);
                    entity.Sys_Sensitive_Keyword = Tools.NullStr(RdrList["Sys_Sensitive_Keyword"]);
                    entity.Shop_Second_Domain = Tools.NullStr(RdrList["Shop_Second_Domain"]);
                    entity.Keyword_Adv_MinPrice = Tools.NullDbl(RdrList["Keyword_Adv_MinPrice"]);
                    entity.Sys_Delivery_Code = Tools.NullStr(RdrList["Sys_Delivery_Code"]);
                    entity.Instant_GoldPrice = Tools.NullDbl(RdrList["Instant_GoldPrice"]);
                    entity.Instant_SilverPrice = Tools.NullDbl(RdrList["Instant_SilverPrice"]);
                }
                return entity;
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

        /// <summary>
        /// 根据域名获得配置信息
        /// </summary>
        /// <param name="DomainName">域名</param>
        /// <returns></returns>
        public virtual ConfigInfo GetConfigByDomainName(string DomainName)
        {
            ConfigInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT TOP 1 * FROM Sys_Config WHERE Site_DomainName = '" + DomainName + "' ORDER BY Sys_Config_ID DESC";
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new ConfigInfo();
                    entity.Sys_Config_ID = Tools.NullInt(RdrList["Sys_Config_ID"]);
                    entity.Upload_Server_URL = Tools.NullStr(RdrList["Upload_Server_URL"]);
                    entity.Upload_Server_Return_WWW = Tools.NullStr(RdrList["Upload_Server_Return_WWW"]);
                    entity.Upload_Server_Return_Admin = Tools.NullStr(RdrList["Upload_Server_Return_Admin"]);
                    entity.Upload_Server_Return_Supplier = Tools.NullStr(RdrList["Upload_Server_Return_Supplier"]);
                    entity.Language_Define = Tools.NullStr(RdrList["Language_Define"]);
                    entity.Site_DomainName = Tools.NullStr(RdrList["Site_DomainName"]);
                    entity.Site_Name = Tools.NullStr(RdrList["Site_Name"]);
                    entity.Site_URL = Tools.NullStr(RdrList["Site_URL"]);
                    entity.Site_Logo = Tools.NullStr(RdrList["Site_Logo"]);
                    entity.Site_Tel = Tools.NullStr(RdrList["Site_Tel"]);
                    entity.Site_Fax = Tools.NullStr(RdrList["Site_Fax"]);
                    entity.Site_Email = Tools.NullStr(RdrList["Site_Email"]);
                    entity.Site_Keyword = Tools.NullStr(RdrList["Site_Keyword"]);
                    entity.Site_Description = Tools.NullStr(RdrList["Site_Description"]);
                    entity.Site_Title = Tools.NullStr(RdrList["Site_Title"]);
                    entity.Mail_Server = Tools.NullStr(RdrList["Mail_Server"]);
                    entity.Mail_ServerPort = Tools.NullInt(RdrList["Mail_ServerPort"]);
                    entity.Mail_EnableSsl = Tools.NullInt(RdrList["Mail_EnableSsl"]);
                    entity.Mail_ServerUserName = Tools.NullStr(RdrList["Mail_ServerUserName"]);
                    entity.Mail_ServerPassWord = Tools.NullStr(RdrList["Mail_ServerPassWord"]);
                    entity.Mail_FromName = Tools.NullStr(RdrList["Mail_FromName"]);
                    entity.Mail_From = Tools.NullStr(RdrList["Mail_From"]);
                    entity.Mail_Replyto = Tools.NullStr(RdrList["Mail_Replyto"]);
                    entity.Mail_Encode = Tools.NullStr(RdrList["Mail_Encode"]);
                    entity.Coin_Name = Tools.NullStr(RdrList["Coin_Name"]);
                    entity.Coin_Rate = Tools.NullInt(RdrList["Coin_Rate"]);
                    entity.Trade_Contract_IsActive = Tools.NullInt(RdrList["Trade_Contract_IsActive"]);
                    entity.Sys_Config_Site = Tools.NullStr(RdrList["Sys_Config_Site"]);
                    entity.Static_IsEnable = Tools.NullInt(RdrList["Static_IsEnable"]);
                    entity.Chinabank_Code = Tools.NullStr(RdrList["Chinabank_Code"]);
                    entity.Chinabank_Key = Tools.NullStr(RdrList["Chinabank_Key"]);
                    entity.Alipay_Email = Tools.NullStr(RdrList["Alipay_Email"]);
                    entity.Alipay_Code = Tools.NullStr(RdrList["Alipay_Code"]);
                    entity.Alipay_Key = Tools.NullStr(RdrList["Alipay_Key"]);
                    entity.Tenpay_Code = Tools.NullStr(RdrList["Tenpay_Code"]);
                    entity.Tenpay_Key = Tools.NullStr(RdrList["Tenpay_Key"]);
                    entity.CreditPayment_Code = Tools.NullStr(RdrList["CreditPayment_Code"]);
                    entity.RepidBuy_IsEnable = Tools.NullInt(RdrList["RepidBuy_IsEnable"]);
                    entity.Coupon_UsedAmount = Tools.NullInt(RdrList["Coupon_UsedAmount"]);
                    entity.Sys_Sensitive_Keyword = Tools.NullStr(RdrList["Sys_Sensitive_Keyword"]);
                    entity.Shop_Second_Domain = Tools.NullStr(RdrList["Shop_Second_Domain"]);
                    entity.Keyword_Adv_MinPrice = Tools.NullDbl(RdrList["Keyword_Adv_MinPrice"]);
                    entity.Sys_Delivery_Code = Tools.NullStr(RdrList["Sys_Delivery_Code"]);
                    entity.Instant_GoldPrice = Tools.NullDbl(RdrList["Instant_GoldPrice"]);
                    entity.Instant_SilverPrice = Tools.NullDbl(RdrList["Instant_SilverPrice"]);
                }
                return entity;
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

        public virtual IList<ConfigInfo> GetConfigs(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<ConfigInfo> entitys = null;
            ConfigInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Sys_Config";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows) {
                    entitys = new List<ConfigInfo>();
                    while (RdrList.Read()) {
                        entity = new ConfigInfo();
                        entity.Sys_Config_ID = Tools.NullInt(RdrList["Sys_Config_ID"]);
                        entity.Upload_Server_URL = Tools.NullStr(RdrList["Upload_Server_URL"]);
                        entity.Upload_Server_Return_WWW = Tools.NullStr(RdrList["Upload_Server_Return_WWW"]);
                        entity.Upload_Server_Return_Admin = Tools.NullStr(RdrList["Upload_Server_Return_Admin"]);
                        entity.Upload_Server_Return_Supplier = Tools.NullStr(RdrList["Upload_Server_Return_Supplier"]);
                        entity.Language_Define = Tools.NullStr(RdrList["Language_Define"]);
                        entity.Site_DomainName = Tools.NullStr(RdrList["Site_DomainName"]);
                        entity.Site_Name = Tools.NullStr(RdrList["Site_Name"]);
                        entity.Site_URL = Tools.NullStr(RdrList["Site_URL"]);
                        entity.Site_Logo = Tools.NullStr(RdrList["Site_Logo"]);
                        entity.Site_Tel = Tools.NullStr(RdrList["Site_Tel"]);
                        entity.Site_Fax = Tools.NullStr(RdrList["Site_Fax"]);
                        entity.Site_Email = Tools.NullStr(RdrList["Site_Email"]);
                        entity.Site_Keyword = Tools.NullStr(RdrList["Site_Keyword"]);
                        entity.Site_Description = Tools.NullStr(RdrList["Site_Description"]);
                        entity.Site_Title = Tools.NullStr(RdrList["Site_Title"]);
                        entity.Mail_Server = Tools.NullStr(RdrList["Mail_Server"]);
                        entity.Mail_ServerPort = Tools.NullInt(RdrList["Mail_ServerPort"]);
                        entity.Mail_EnableSsl = Tools.NullInt(RdrList["Mail_EnableSsl"]);
                        entity.Mail_ServerUserName = Tools.NullStr(RdrList["Mail_ServerUserName"]);
                        entity.Mail_ServerPassWord = Tools.NullStr(RdrList["Mail_ServerPassWord"]);
                        entity.Mail_FromName = Tools.NullStr(RdrList["Mail_FromName"]);
                        entity.Mail_From = Tools.NullStr(RdrList["Mail_From"]);
                        entity.Mail_Replyto = Tools.NullStr(RdrList["Mail_Replyto"]);
                        entity.Mail_Encode = Tools.NullStr(RdrList["Mail_Encode"]);
                        entity.Coin_Name = Tools.NullStr(RdrList["Coin_Name"]);
                        entity.Coin_Rate = Tools.NullInt(RdrList["Coin_Rate"]);
                        entity.Sys_Config_Site = Tools.NullStr(RdrList["Sys_Config_Site"]);
                        entity.Static_IsEnable = Tools.NullInt(RdrList["Static_IsEnable"]);
                        entity.Chinabank_Code = Tools.NullStr(RdrList["Chinabank_Code"]);
                        entity.Chinabank_Key = Tools.NullStr(RdrList["Chinabank_Key"]);
                        entity.Alipay_Email = Tools.NullStr(RdrList["Alipay_Email"]);
                        entity.Alipay_Code = Tools.NullStr(RdrList["Alipay_Code"]);
                        entity.Alipay_Key = Tools.NullStr(RdrList["Alipay_Key"]);
                        entity.Tenpay_Code = Tools.NullStr(RdrList["Tenpay_Code"]);
                        entity.Tenpay_Key = Tools.NullStr(RdrList["Tenpay_Key"]);
                        entity.CreditPayment_Code = Tools.NullStr(RdrList["CreditPayment_Code"]);
                        entity.RepidBuy_IsEnable = Tools.NullInt(RdrList["RepidBuy_IsEnable"]);
                        entity.Coupon_UsedAmount = Tools.NullInt(RdrList["Coupon_UsedAmount"]);
                        entity.Sys_Sensitive_Keyword = Tools.NullStr(RdrList["Sys_Sensitive_Keyword"]);
                        entity.Shop_Second_Domain = Tools.NullStr(RdrList["Shop_Second_Domain"]);
                        entity.Keyword_Adv_MinPrice = Tools.NullDbl(RdrList["Keyword_Adv_MinPrice"]);
                        entity.Sys_Delivery_Code = Tools.NullStr(RdrList["Sys_Delivery_Code"]);
                        entity.Instant_GoldPrice = Tools.NullDbl(RdrList["Instant_GoldPrice"]);
                        entity.Instant_SilverPrice = Tools.NullDbl(RdrList["Instant_SilverPrice"]);
                        entitys.Add(entity);
                        entity = null;
                    }
                }
                return entitys;
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                if (RdrList != null) {
                    RdrList.Close();
                    RdrList = null;
                }
            }
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            int RecordCount, PageCount, CurrentPage;
            string SqlCount, SqlParam, SqlTable;
            PageInfo Page;

            try {
                Page = new PageInfo();
                SqlTable = "Sys_Config";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Sys_Config_ID) FROM " + SqlTable + SqlParam;

                RecordCount = Tools.NullInt(DBHelper.ExecuteScalar(SqlCount));
                PageCount = Tools.CalculatePages(RecordCount, Query.PageSize);
                CurrentPage = Tools.DeterminePage(Query.CurrentPage, PageCount);

                Page.RecordCount = RecordCount;
                Page.PageCount = PageCount;
                Page.CurrentPage = CurrentPage;
                Page.PageSize = Query.PageSize;

                return Page;
            }
            catch (Exception ex) {
                throw ex;
            }
        }
    }

    public class SysInterfaceLog : ISysInterfaceLog
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public SysInterfaceLog()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddSysInterfaceLog(SysInterfaceLogInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Sys_Interface_Log";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Log_ID"] = entity.Log_ID;
            DrAdd["Log_Type"] = entity.Log_Type;
            DrAdd["Log_Action"] = entity.Log_Action;
            DrAdd["Log_Result"] = entity.Log_Result;
            DrAdd["Log_Parameters"] = entity.Log_Parameters;
            DrAdd["Log_Remark"] = entity.Log_Remark;
            DrAdd["Log_Addtime"] = entity.Log_Addtime;

            DtAdd.Rows.Add(DrAdd);
            try
            {
                DBHelper.SaveChanges(SqlAdd, DtAdd);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                DtAdd.Dispose();
            }
        }

        public virtual bool EditSysInterfaceLog(SysInterfaceLogInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Sys_Interface_Log WHERE Log_ID = " + entity.Log_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Log_ID"] = entity.Log_ID;
                    DrAdd["Log_Type"] = entity.Log_Type;
                    DrAdd["Log_Action"] = entity.Log_Action;
                    DrAdd["Log_Result"] = entity.Log_Result;
                    DrAdd["Log_Parameters"] = entity.Log_Parameters;
                    DrAdd["Log_Remark"] = entity.Log_Remark;
                    DrAdd["Log_Addtime"] = entity.Log_Addtime;

                    DBHelper.SaveChanges(SqlAdd, DtAdd);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                DtAdd.Dispose();
            }
            return true;

        }

        public virtual int DelSysInterfaceLog(int ID)
        {
            string SqlAdd = "DELETE FROM Sys_Interface_Log WHERE Log_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual SysInterfaceLogInfo GetSysInterfaceLogByID(int ID)
        {
            SysInterfaceLogInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Sys_Interface_Log WHERE Log_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SysInterfaceLogInfo();

                    entity.Log_ID = Tools.NullInt(RdrList["Log_ID"]);
                    entity.Log_Type = Tools.NullInt(RdrList["Log_Type"]);
                    entity.Log_Action = Tools.NullStr(RdrList["Log_Action"]);
                    entity.Log_Result = Tools.NullStr(RdrList["Log_Result"]);
                    entity.Log_Parameters = Tools.NullStr(RdrList["Log_Parameters"]);
                    entity.Log_Remark = Tools.NullStr(RdrList["Log_Remark"]);
                    entity.Log_Addtime = Tools.NullDate(RdrList["Log_Addtime"]);

                }

                return entity;
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

        public virtual IList<SysInterfaceLogInfo> GetSysInterfaceLogs(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<SysInterfaceLogInfo> entitys = null;
            SysInterfaceLogInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Sys_Interface_Log";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SysInterfaceLogInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SysInterfaceLogInfo();
                        entity.Log_ID = Tools.NullInt(RdrList["Log_ID"]);
                        entity.Log_Type = Tools.NullInt(RdrList["Log_Type"]);
                        entity.Log_Action = Tools.NullStr(RdrList["Log_Action"]);
                        entity.Log_Result = Tools.NullStr(RdrList["Log_Result"]);
                        entity.Log_Parameters = Tools.NullStr(RdrList["Log_Parameters"]);
                        entity.Log_Remark = Tools.NullStr(RdrList["Log_Remark"]);
                        entity.Log_Addtime = Tools.NullDate(RdrList["Log_Addtime"]);

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

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            int RecordCount, PageCount, CurrentPage;
            string SqlCount, SqlParam, SqlTable;
            PageInfo Page;

            try
            {
                Page = new PageInfo();
                SqlTable = "Sys_Interface_Log";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Log_ID) FROM " + SqlTable + SqlParam;

                RecordCount = Tools.NullInt(DBHelper.ExecuteScalar(SqlCount));
                PageCount = Tools.CalculatePages(RecordCount, Query.PageSize);
                CurrentPage = Tools.DeterminePage(Query.CurrentPage, PageCount);

                Page.RecordCount = RecordCount;
                Page.PageCount = PageCount;
                Page.CurrentPage = CurrentPage;
                Page.PageSize = Query.PageSize;

                return Page;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
