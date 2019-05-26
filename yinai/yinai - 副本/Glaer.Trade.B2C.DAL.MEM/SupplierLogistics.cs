using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.Util.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public class SupplierLogistics : ISupplierLogistics
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public SupplierLogistics()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddSupplierLogistics(SupplierLogisticsInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Supplier_Logistics";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Supplier_Logistics_ID"] = entity.Supplier_Logistics_ID;
            DrAdd["Supplier_SupplierID"] = entity.Supplier_SupplierID;
            DrAdd["Supplier_OrdersID"] = entity.Supplier_OrdersID;
            DrAdd["Supplier_LogisticsID"] = entity.Supplier_LogisticsID;
            DrAdd["Supplier_Status"] = entity.Supplier_Status;
            DrAdd["Supplier_Orders_Address_Country"] = entity.Supplier_Orders_Address_Country;
            DrAdd["Supplier_Orders_Address_State"] = entity.Supplier_Orders_Address_State;
            DrAdd["Supplier_Orders_Address_City"] = entity.Supplier_Orders_Address_City;
            DrAdd["Supplier_Orders_Address_County"] = entity.Supplier_Orders_Address_County;
            DrAdd["Supplier_Orders_Address_StreetAddress"] = entity.Supplier_Orders_Address_StreetAddress;
            DrAdd["Supplier_Address_Country"] = entity.Supplier_Address_Country;
            DrAdd["Supplier_Address_State"] = entity.Supplier_Address_State;
            DrAdd["Supplier_Address_City"] = entity.Supplier_Address_City;
            DrAdd["Supplier_Address_County"] = entity.Supplier_Address_County;
            DrAdd["Supplier_Address_StreetAddress"] = entity.Supplier_Address_StreetAddress;
            DrAdd["Supplier_Logistics_Name"] = entity.Supplier_Logistics_Name;
            DrAdd["Supplier_Logistics_Number"] = entity.Supplier_Logistics_Number;
            DrAdd["Supplier_Logistics_DeliveryTime"] = entity.Supplier_Logistics_DeliveryTime;
            DrAdd["Supplier_Logistics_IsAudit"] = entity.Supplier_Logistics_IsAudit;
            DrAdd["Supplier_Logistics_AuditTime"] = entity.Supplier_Logistics_AuditTime;
            DrAdd["Supplier_Logistics_AuditRemarks"] = entity.Supplier_Logistics_AuditRemarks;
            DrAdd["Supplier_Logistics_FinishTime"] = entity.Supplier_Logistics_FinishTime;
            DrAdd["Supplier_Logistics_TenderID"] = entity.Supplier_Logistics_TenderID;
            DrAdd["Supplier_Logistics_Price"] = entity.Supplier_Logistics_Price;

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

        public virtual bool EditSupplierLogistics(SupplierLogisticsInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Supplier_Logistics WHERE Supplier_Logistics_ID = " + entity.Supplier_Logistics_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Supplier_Logistics_ID"] = entity.Supplier_Logistics_ID;
                    DrAdd["Supplier_SupplierID"] = entity.Supplier_SupplierID;
                    DrAdd["Supplier_OrdersID"] = entity.Supplier_OrdersID;
                    DrAdd["Supplier_LogisticsID"] = entity.Supplier_LogisticsID;
                    DrAdd["Supplier_Status"] = entity.Supplier_Status;
                    DrAdd["Supplier_Orders_Address_Country"] = entity.Supplier_Orders_Address_Country;
                    DrAdd["Supplier_Orders_Address_State"] = entity.Supplier_Orders_Address_State;
                    DrAdd["Supplier_Orders_Address_City"] = entity.Supplier_Orders_Address_City;
                    DrAdd["Supplier_Orders_Address_County"] = entity.Supplier_Orders_Address_County;
                    DrAdd["Supplier_Orders_Address_StreetAddress"] = entity.Supplier_Orders_Address_StreetAddress;
                    DrAdd["Supplier_Address_Country"] = entity.Supplier_Address_Country;
                    DrAdd["Supplier_Address_State"] = entity.Supplier_Address_State;
                    DrAdd["Supplier_Address_City"] = entity.Supplier_Address_City;
                    DrAdd["Supplier_Address_County"] = entity.Supplier_Address_County;
                    DrAdd["Supplier_Address_StreetAddress"] = entity.Supplier_Address_StreetAddress;
                    DrAdd["Supplier_Logistics_Name"] = entity.Supplier_Logistics_Name;
                    DrAdd["Supplier_Logistics_Number"] = entity.Supplier_Logistics_Number;
                    DrAdd["Supplier_Logistics_DeliveryTime"] = entity.Supplier_Logistics_DeliveryTime;
                    DrAdd["Supplier_Logistics_IsAudit"] = entity.Supplier_Logistics_IsAudit;
                    DrAdd["Supplier_Logistics_AuditTime"] = entity.Supplier_Logistics_AuditTime;
                    DrAdd["Supplier_Logistics_AuditRemarks"] = entity.Supplier_Logistics_AuditRemarks;
                    DrAdd["Supplier_Logistics_FinishTime"] = entity.Supplier_Logistics_FinishTime;
                    DrAdd["Supplier_Logistics_TenderID"] = entity.Supplier_Logistics_TenderID;
                    DrAdd["Supplier_Logistics_Price"] = entity.Supplier_Logistics_Price;

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

        public virtual int DelSupplierLogistics(int ID)
        {
            string SqlAdd = "DELETE FROM Supplier_Logistics WHERE Supplier_Logistics_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual SupplierLogisticsInfo GetSupplierLogisticsByID(int ID)
        {
            SupplierLogisticsInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Supplier_Logistics WHERE Supplier_Logistics_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SupplierLogisticsInfo();

                    entity.Supplier_Logistics_ID = Tools.NullInt(RdrList["Supplier_Logistics_ID"]);
                    entity.Supplier_SupplierID = Tools.NullInt(RdrList["Supplier_SupplierID"]);
                    entity.Supplier_OrdersID = Tools.NullInt(RdrList["Supplier_OrdersID"]);
                    entity.Supplier_LogisticsID = Tools.NullInt(RdrList["Supplier_LogisticsID"]);
                    entity.Supplier_Status = Tools.NullInt(RdrList["Supplier_Status"]);
                    entity.Supplier_Orders_Address_Country = Tools.NullStr(RdrList["Supplier_Orders_Address_Country"]);
                    entity.Supplier_Orders_Address_State = Tools.NullStr(RdrList["Supplier_Orders_Address_State"]);
                    entity.Supplier_Orders_Address_City = Tools.NullStr(RdrList["Supplier_Orders_Address_City"]);
                    entity.Supplier_Orders_Address_County = Tools.NullStr(RdrList["Supplier_Orders_Address_County"]);
                    entity.Supplier_Orders_Address_StreetAddress = Tools.NullStr(RdrList["Supplier_Orders_Address_StreetAddress"]);
                    entity.Supplier_Address_Country = Tools.NullStr(RdrList["Supplier_Address_Country"]);
                    entity.Supplier_Address_State = Tools.NullStr(RdrList["Supplier_Address_State"]);
                    entity.Supplier_Address_City = Tools.NullStr(RdrList["Supplier_Address_City"]);
                    entity.Supplier_Address_County = Tools.NullStr(RdrList["Supplier_Address_County"]);
                    entity.Supplier_Address_StreetAddress = Tools.NullStr(RdrList["Supplier_Address_StreetAddress"]);
                    entity.Supplier_Logistics_Name = Tools.NullStr(RdrList["Supplier_Logistics_Name"]);
                    entity.Supplier_Logistics_Number = Tools.NullStr(RdrList["Supplier_Logistics_Number"]);
                    entity.Supplier_Logistics_DeliveryTime = Tools.NullDate(RdrList["Supplier_Logistics_DeliveryTime"]);
                    entity.Supplier_Logistics_IsAudit = Tools.NullInt(RdrList["Supplier_Logistics_IsAudit"]);
                    entity.Supplier_Logistics_AuditTime = Tools.NullDate(RdrList["Supplier_Logistics_AuditTime"]);
                    entity.Supplier_Logistics_AuditRemarks = Tools.NullStr(RdrList["Supplier_Logistics_AuditRemarks"]);
                    entity.Supplier_Logistics_FinishTime = Tools.NullDate(RdrList["Supplier_Logistics_FinishTime"]);
                    entity.Supplier_Logistics_TenderID = Tools.NullInt(RdrList["Supplier_Logistics_TenderID"]);
                    entity.Supplier_Logistics_Price = Tools.NullDbl(RdrList["Supplier_Logistics_Price"]);

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

        public virtual IList<SupplierLogisticsInfo> GetSupplierLogisticss(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<SupplierLogisticsInfo> entitys = null;
            SupplierLogisticsInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Supplier_Logistics";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SupplierLogisticsInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SupplierLogisticsInfo();
                        entity.Supplier_Logistics_ID = Tools.NullInt(RdrList["Supplier_Logistics_ID"]);
                        entity.Supplier_SupplierID = Tools.NullInt(RdrList["Supplier_SupplierID"]);
                        entity.Supplier_OrdersID = Tools.NullInt(RdrList["Supplier_OrdersID"]);
                        entity.Supplier_LogisticsID = Tools.NullInt(RdrList["Supplier_LogisticsID"]);
                        entity.Supplier_Status = Tools.NullInt(RdrList["Supplier_Status"]);
                        entity.Supplier_Orders_Address_Country = Tools.NullStr(RdrList["Supplier_Orders_Address_Country"]);
                        entity.Supplier_Orders_Address_State = Tools.NullStr(RdrList["Supplier_Orders_Address_State"]);
                        entity.Supplier_Orders_Address_City = Tools.NullStr(RdrList["Supplier_Orders_Address_City"]);
                        entity.Supplier_Orders_Address_County = Tools.NullStr(RdrList["Supplier_Orders_Address_County"]);
                        entity.Supplier_Orders_Address_StreetAddress = Tools.NullStr(RdrList["Supplier_Orders_Address_StreetAddress"]);
                        entity.Supplier_Address_Country = Tools.NullStr(RdrList["Supplier_Address_Country"]);
                        entity.Supplier_Address_State = Tools.NullStr(RdrList["Supplier_Address_State"]);
                        entity.Supplier_Address_City = Tools.NullStr(RdrList["Supplier_Address_City"]);
                        entity.Supplier_Address_County = Tools.NullStr(RdrList["Supplier_Address_County"]);
                        entity.Supplier_Address_StreetAddress = Tools.NullStr(RdrList["Supplier_Address_StreetAddress"]);
                        entity.Supplier_Logistics_Name = Tools.NullStr(RdrList["Supplier_Logistics_Name"]);
                        entity.Supplier_Logistics_Number = Tools.NullStr(RdrList["Supplier_Logistics_Number"]);
                        entity.Supplier_Logistics_DeliveryTime = Tools.NullDate(RdrList["Supplier_Logistics_DeliveryTime"]);
                        entity.Supplier_Logistics_IsAudit = Tools.NullInt(RdrList["Supplier_Logistics_IsAudit"]);
                        entity.Supplier_Logistics_AuditTime = Tools.NullDate(RdrList["Supplier_Logistics_AuditTime"]);
                        entity.Supplier_Logistics_AuditRemarks = Tools.NullStr(RdrList["Supplier_Logistics_AuditRemarks"]);
                        entity.Supplier_Logistics_FinishTime = Tools.NullDate(RdrList["Supplier_Logistics_FinishTime"]);
                        entity.Supplier_Logistics_TenderID = Tools.NullInt(RdrList["Supplier_Logistics_TenderID"]);
                        entity.Supplier_Logistics_Price = Tools.NullDbl(RdrList["Supplier_Logistics_Price"]);

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
                SqlTable = "Supplier_Logistics";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Supplier_Logistics_ID) FROM " + SqlTable + SqlParam;

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


    public class LogisticsTender : ILogisticsTender
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public LogisticsTender()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddLogisticsTender(LogisticsTenderInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Logistics_Tender";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Logistics_Tender_ID"] = entity.Logistics_Tender_ID;
            DrAdd["Logistics_Tender_LogisticsID"] = entity.Logistics_Tender_LogisticsID;
            DrAdd["Logistics_Tender_SupplierLogisticsID"] = entity.Logistics_Tender_SupplierLogisticsID;
            DrAdd["Logistics_Tender_OrderID"] = entity.Logistics_Tender_OrderID;
            DrAdd["Logistics_Tender_Price"] = entity.Logistics_Tender_Price;
            DrAdd["Logistics_Tender_AddTime"] = entity.Logistics_Tender_AddTime;
            DrAdd["Logistics_Tender_IsWin"] = entity.Logistics_Tender_IsWin;
            DrAdd["Logistics_Tender_FinishTime"] = entity.Logistics_Tender_FinishTime;

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

        public virtual bool EditLogisticsTender(LogisticsTenderInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Logistics_Tender WHERE Logistics_Tender_ID = " + entity.Logistics_Tender_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Logistics_Tender_ID"] = entity.Logistics_Tender_ID;
                    DrAdd["Logistics_Tender_LogisticsID"] = entity.Logistics_Tender_LogisticsID;
                    DrAdd["Logistics_Tender_SupplierLogisticsID"] = entity.Logistics_Tender_SupplierLogisticsID;
                    DrAdd["Logistics_Tender_OrderID"] = entity.Logistics_Tender_OrderID;
                    DrAdd["Logistics_Tender_Price"] = entity.Logistics_Tender_Price;
                    DrAdd["Logistics_Tender_AddTime"] = entity.Logistics_Tender_AddTime;
                    DrAdd["Logistics_Tender_IsWin"] = entity.Logistics_Tender_IsWin;
                    DrAdd["Logistics_Tender_FinishTime"] = entity.Logistics_Tender_FinishTime;

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

        public virtual int DelLogisticsTender(int ID)
        {
            string SqlAdd = "DELETE FROM Logistics_Tender WHERE Logistics_Tender_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual LogisticsTenderInfo GetLogisticsTenderByID(int ID)
        {
            LogisticsTenderInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Logistics_Tender WHERE Logistics_Tender_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new LogisticsTenderInfo();

                    entity.Logistics_Tender_ID = Tools.NullInt(RdrList["Logistics_Tender_ID"]);
                    entity.Logistics_Tender_LogisticsID = Tools.NullInt(RdrList["Logistics_Tender_LogisticsID"]);
                    entity.Logistics_Tender_SupplierLogisticsID = Tools.NullInt(RdrList["Logistics_Tender_SupplierLogisticsID"]);
                    entity.Logistics_Tender_OrderID = Tools.NullInt(RdrList["Logistics_Tender_OrderID"]);
                    entity.Logistics_Tender_Price = Tools.NullDbl(RdrList["Logistics_Tender_Price"]);
                    entity.Logistics_Tender_AddTime = Tools.NullDate(RdrList["Logistics_Tender_AddTime"]);
                    entity.Logistics_Tender_IsWin = Tools.NullInt(RdrList["Logistics_Tender_IsWin"]);
                    entity.Logistics_Tender_FinishTime = Tools.NullDate(RdrList["Logistics_Tender_FinishTime"]);

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

        public virtual IList<LogisticsTenderInfo> GetLogisticsTenders(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<LogisticsTenderInfo> entitys = null;
            LogisticsTenderInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Logistics_Tender";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<LogisticsTenderInfo>();
                    while (RdrList.Read())
                    {
                        entity = new LogisticsTenderInfo();
                        entity.Logistics_Tender_ID = Tools.NullInt(RdrList["Logistics_Tender_ID"]);
                        entity.Logistics_Tender_LogisticsID = Tools.NullInt(RdrList["Logistics_Tender_LogisticsID"]);
                        entity.Logistics_Tender_SupplierLogisticsID = Tools.NullInt(RdrList["Logistics_Tender_SupplierLogisticsID"]);
                        entity.Logistics_Tender_OrderID = Tools.NullInt(RdrList["Logistics_Tender_OrderID"]);
                        entity.Logistics_Tender_Price = Tools.NullDbl(RdrList["Logistics_Tender_Price"]);
                        entity.Logistics_Tender_AddTime = Tools.NullDate(RdrList["Logistics_Tender_AddTime"]);
                        entity.Logistics_Tender_IsWin = Tools.NullInt(RdrList["Logistics_Tender_IsWin"]);
                        entity.Logistics_Tender_FinishTime = Tools.NullDate(RdrList["Logistics_Tender_FinishTime"]);

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
                SqlTable = "Logistics_Tender";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Logistics_Tender_ID) FROM " + SqlTable + SqlParam;

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
