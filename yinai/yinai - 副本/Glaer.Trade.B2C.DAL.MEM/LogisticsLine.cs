using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.Util.Tools;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public class LogisticsLine : ILogisticsLine
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public LogisticsLine()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddLogisticsLine(LogisticsLineInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Logistics_Line";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Logistics_Line_ID"] = entity.Logistics_Line_ID;
            DrAdd["Logistics_Line_Contact"] = entity.Logistics_Line_Contact;
            DrAdd["Logistics_Line_CarType"] = entity.Logistics_Line_CarType;
            DrAdd["Logistics_Line_Delivery_Address"] = entity.Logistics_Line_Delivery_Address;
            DrAdd["Logistics_Line_Receiving_Address"] = entity.Logistics_Line_Receiving_Address;
            DrAdd["Logistics_Line_DeliverTime"] = entity.Logistics_Line_DeliverTime;
            DrAdd["Logistics_Line_Deliver_Price"] = entity.Logistics_Line_Deliver_Price;
            DrAdd["Logistics_Line_Note"] = entity.Logistics_Line_Note;
            DrAdd["Logistics_ID"] = entity.Logistics_ID;

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

        public virtual bool EditLogisticsLine(LogisticsLineInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Logistics_Line WHERE Logistics_Line_ID = " + entity.Logistics_Line_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Logistics_Line_ID"] = entity.Logistics_Line_ID;
                    DrAdd["Logistics_Line_Contact"] = entity.Logistics_Line_Contact;
                    DrAdd["Logistics_Line_CarType"] = entity.Logistics_Line_CarType;
                    DrAdd["Logistics_Line_Delivery_Address"] = entity.Logistics_Line_Delivery_Address;
                    DrAdd["Logistics_Line_Receiving_Address"] = entity.Logistics_Line_Receiving_Address;
                    DrAdd["Logistics_Line_DeliverTime"] = entity.Logistics_Line_DeliverTime;
                    DrAdd["Logistics_Line_Deliver_Price"] = entity.Logistics_Line_Deliver_Price;
                    DrAdd["Logistics_Line_Note"] = entity.Logistics_Line_Note;
                    DrAdd["Logistics_ID"] = entity.Logistics_ID;

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

        public virtual int DelLogisticsLine(int ID)
        {
            string SqlAdd = "DELETE FROM Logistics_Line WHERE Logistics_Line_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual LogisticsLineInfo GetLogisticsLineByID(int ID)
        {
            LogisticsLineInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Logistics_Line WHERE Logistics_Line_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new LogisticsLineInfo();

                    entity.Logistics_Line_ID = Tools.NullInt(RdrList["Logistics_Line_ID"]);
                    entity.Logistics_Line_Contact = Tools.NullStr(RdrList["Logistics_Line_Contact"]);
                    entity.Logistics_Line_CarType = Tools.NullStr(RdrList["Logistics_Line_CarType"]);
                    entity.Logistics_Line_Delivery_Address = Tools.NullStr(RdrList["Logistics_Line_Delivery_Address"]);
                    entity.Logistics_Line_Receiving_Address = Tools.NullStr(RdrList["Logistics_Line_Receiving_Address"]);
                    entity.Logistics_Line_DeliverTime = Tools.NullDate(RdrList["Logistics_Line_DeliverTime"]);
                    entity.Logistics_Line_Deliver_Price = Tools.NullDbl(RdrList["Logistics_Line_Deliver_Price"]);
                    entity.Logistics_Line_Note = Tools.NullStr(RdrList["Logistics_Line_Note"]);
                    entity.Logistics_ID = Tools.NullInt(RdrList["Logistics_ID"]);

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

        public virtual IList<LogisticsLineInfo> GetLogisticsLines(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<LogisticsLineInfo> entitys = null;
            LogisticsLineInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Logistics_Line";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<LogisticsLineInfo>();
                    while (RdrList.Read())
                    {
                        entity = new LogisticsLineInfo();
                        entity.Logistics_Line_ID = Tools.NullInt(RdrList["Logistics_Line_ID"]);
                        entity.Logistics_Line_Contact = Tools.NullStr(RdrList["Logistics_Line_Contact"]);
                        entity.Logistics_Line_CarType = Tools.NullStr(RdrList["Logistics_Line_CarType"]);
                        entity.Logistics_Line_Delivery_Address = Tools.NullStr(RdrList["Logistics_Line_Delivery_Address"]);
                        entity.Logistics_Line_Receiving_Address = Tools.NullStr(RdrList["Logistics_Line_Receiving_Address"]);
                        entity.Logistics_Line_DeliverTime = Tools.NullDate(RdrList["Logistics_Line_DeliverTime"]);
                        entity.Logistics_Line_Deliver_Price = Tools.NullDbl(RdrList["Logistics_Line_Deliver_Price"]);
                        entity.Logistics_Line_Note = Tools.NullStr(RdrList["Logistics_Line_Note"]);
                        entity.Logistics_ID = Tools.NullInt(RdrList["Logistics_ID"]);

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
                SqlTable = "Logistics_Line";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Logistics_Line_ID) FROM " + SqlTable + SqlParam;

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
