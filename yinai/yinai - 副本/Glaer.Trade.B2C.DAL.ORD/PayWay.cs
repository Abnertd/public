using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.Util.Tools;

namespace Glaer.Trade.B2C.DAL.ORD
{
    public class PayWay : IPayWay
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public PayWay()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddPayWay(PayWayInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Pay_Way";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();
            DrAdd["Pay_Way_Type"] = entity.Pay_Way_Type;
            DrAdd["Pay_Way_Name"] = entity.Pay_Way_Name;
            DrAdd["Pay_Way_Sort"] = entity.Pay_Way_Sort;
            DrAdd["Pay_Way_Status"] = entity.Pay_Way_Status;
            DrAdd["Pay_Way_Cod"] = entity.Pay_Way_Cod;
            DrAdd["Pay_Way_Img"] = entity.Pay_Way_Img;
            DrAdd["Pay_Way_Intro"] = entity.Pay_Way_Intro;
            DrAdd["Pay_Way_Site"] = entity.Pay_Way_Site;
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

        public virtual bool EditPayWay(PayWayInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Pay_Way WHERE Pay_Way_ID = " + entity.Pay_Way_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try {
                if (DtAdd.Rows.Count > 0) {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Pay_Way_ID"] = entity.Pay_Way_ID;
                    DrAdd["Pay_Way_Type"] = entity.Pay_Way_Type;
                    DrAdd["Pay_Way_Name"] = entity.Pay_Way_Name;
                    DrAdd["Pay_Way_Sort"] = entity.Pay_Way_Sort;
                    DrAdd["Pay_Way_Status"] = entity.Pay_Way_Status;
                    DrAdd["Pay_Way_Cod"] = entity.Pay_Way_Cod;
                    DrAdd["Pay_Way_Img"] = entity.Pay_Way_Img;
                    DrAdd["Pay_Way_Intro"] = entity.Pay_Way_Intro;
                    DrAdd["Pay_Way_Site"] = entity.Pay_Way_Site;
                    DBHelper.SaveChanges(SqlAdd, DtAdd);
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
            return true;
        }

        public virtual int DelPayWay(int ID)
        {
            string SqlAdd = "DELETE FROM Pay_Way WHERE Pay_Way_ID = " + ID;
            try { return DBHelper.ExecuteNonQuery(SqlAdd);}
            catch (Exception ex) {
                throw ex;
            }
        }

        public virtual PayWayInfo GetPayWayByID(int ID)
        {
            PayWayInfo entity = null;
            SqlDataReader RdrList = null;
            try {
                string SqlList;
                SqlList = "SELECT * FROM Pay_Way WHERE Pay_Way_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read()) {
                    entity = new PayWayInfo();
                    entity.Pay_Way_ID = Tools.NullInt(RdrList["Pay_Way_ID"]);
                    entity.Pay_Way_Type = Tools.NullInt(RdrList["Pay_Way_Type"]);
                    entity.Pay_Way_Name = Tools.NullStr(RdrList["Pay_Way_Name"]);
                    entity.Pay_Way_Sort = Tools.NullInt(RdrList["Pay_Way_Sort"]);
                    entity.Pay_Way_Status = Tools.NullInt(RdrList["Pay_Way_Status"]);
                    entity.Pay_Way_Cod = Tools.NullInt(RdrList["Pay_Way_Cod"]);
                    entity.Pay_Way_Img = Tools.NullStr(RdrList["Pay_Way_Img"]);
                    entity.Pay_Way_Intro = Tools.NullStr(RdrList["Pay_Way_Intro"]);
                    entity.Pay_Way_Site = Tools.NullStr(RdrList["Pay_Way_Site"]);
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

        public virtual IList<PayWayInfo> GetPayWays(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<PayWayInfo> entitys = null;
            PayWayInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Pay_Way";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows) {
                    entitys = new List<PayWayInfo>();
                    while (RdrList.Read()) {
                        entity = new PayWayInfo();
                        entity.Pay_Way_ID = Tools.NullInt(RdrList["Pay_Way_ID"]);
                        entity.Pay_Way_Type = Tools.NullInt(RdrList["Pay_Way_Type"]);
                        entity.Pay_Way_Name = Tools.NullStr(RdrList["Pay_Way_Name"]);
                        entity.Pay_Way_Sort = Tools.NullInt(RdrList["Pay_Way_Sort"]);
                        entity.Pay_Way_Status = Tools.NullInt(RdrList["Pay_Way_Status"]);
                        entity.Pay_Way_Cod = Tools.NullInt(RdrList["Pay_Way_Cod"]);
                        entity.Pay_Way_Img = Tools.NullStr(RdrList["Pay_Way_Img"]);
                        entity.Pay_Way_Intro = Tools.NullStr(RdrList["Pay_Way_Intro"]);
                        entity.Pay_Way_Site = Tools.NullStr(RdrList["Pay_Way_Site"]);
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
                SqlTable = "Pay_Way";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Pay_Way_ID) FROM " + SqlTable + SqlParam;

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

        public virtual IList<PayInfo> GetPaysBySite(string siteCode)
        {
            IList<PayInfo> entitys = null;
            PayInfo entity = null;
            string SqlList = "SELECT Sys_Pay_ID, Sys_Pay_Name, Sys_Pay_Sign, Sys_Pay_Picture, Sys_Pay_Sort ";
            SqlList += "FROM Sys_Pay WHERE Sys_Pay_Site = '" + siteCode + "' AND Sys_Pay_Trash = 0 ORDER BY Sys_Pay_Sort ASC";
            SqlDataReader RdrList = null;
            try {
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows) {
                    entitys = new List<PayInfo>();
                    while (RdrList.Read())
                    {
                        entity = new PayInfo();
                        entity.Sys_Pay_ID = Tools.NullInt(RdrList["Sys_Pay_ID"]);
                        entity.Sys_Pay_Name = Tools.NullStr(RdrList["Sys_Pay_Name"]);
                        entity.Sys_Pay_Sign = Tools.NullStr(RdrList["Sys_Pay_Sign"]);
                        entity.Sys_Pay_Picture = Tools.NullStr(RdrList["Sys_Pay_Picture"]);
                        entity.Sys_Pay_Sort = Tools.NullInt(RdrList["Sys_Pay_Sort"]);
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
                RdrList.Close();
                RdrList = null; 
            }
        }

        public virtual PayInfo GetPayByID(int ID) {
            PayInfo entity = null;
            string SqlList = "SELECT Sys_Pay_ID, Sys_Pay_Name, Sys_Pay_Sign, Sys_Pay_Picture, Sys_Pay_Sort";
            SqlList += " FROM Sys_Pay WHERE Sys_Pay_ID = " + ID;
            SqlDataReader RdrList = null;
            try {
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read()) {
                    entity = new PayInfo();
                    entity.Sys_Pay_ID = Tools.NullInt(RdrList["Sys_Pay_ID"]);
                    entity.Sys_Pay_Name = Tools.NullStr(RdrList["Sys_Pay_Name"]);
                    entity.Sys_Pay_Sign = Tools.NullStr(RdrList["Sys_Pay_Sign"]);
                    entity.Sys_Pay_Picture = Tools.NullStr(RdrList["Sys_Pay_Picture"]);
                    entity.Sys_Pay_Sort = Tools.NullInt(RdrList["Sys_Pay_Sort"]);
                }
                return entity;
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                RdrList.Close();
                RdrList = null;
            }
        }

        

    }

}
