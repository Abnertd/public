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
    public class SupplierPriceReport : ISupplierPriceReport
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public SupplierPriceReport()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddSupplierPriceReport(SupplierPriceReportInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Supplier_PriceReport";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["PriceReport_ID"] = entity.PriceReport_ID;
            DrAdd["PriceReport_PurchaseID"] = entity.PriceReport_PurchaseID;
            DrAdd["PriceReport_MemberID"] = entity.PriceReport_MemberID;
            DrAdd["PriceReport_Title"] = entity.PriceReport_Title;
            DrAdd["PriceReport_Name"] = entity.PriceReport_Name;
            DrAdd["PriceReport_Phone"] = entity.PriceReport_Phone;
            DrAdd["PriceReport_DeliveryDate"] = entity.PriceReport_DeliveryDate;
            DrAdd["PriceReport_AddTime"] = entity.PriceReport_AddTime;
            DrAdd["PriceReport_ReplyContent"] = entity.PriceReport_ReplyContent;
            DrAdd["PriceReport_ReplyTime"] = entity.PriceReport_ReplyTime;
            DrAdd["PriceReport_IsReply"] = entity.PriceReport_IsReply;
            DrAdd["PriceReport_AuditStatus"] = entity.PriceReport_AuditStatus;
            DrAdd["PriceReport_Note"] = entity.PriceReport_Note;

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

        public virtual bool EditSupplierPriceReport(SupplierPriceReportInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Supplier_PriceReport WHERE PriceReport_ID = " + entity.PriceReport_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["PriceReport_ID"] = entity.PriceReport_ID;
                    DrAdd["PriceReport_PurchaseID"] = entity.PriceReport_PurchaseID;
                    DrAdd["PriceReport_MemberID"] = entity.PriceReport_MemberID;
                    DrAdd["PriceReport_Title"] = entity.PriceReport_Title;
                    DrAdd["PriceReport_Name"] = entity.PriceReport_Name;
                    DrAdd["PriceReport_Phone"] = entity.PriceReport_Phone;
                    DrAdd["PriceReport_DeliveryDate"] = entity.PriceReport_DeliveryDate;
                    DrAdd["PriceReport_AddTime"] = entity.PriceReport_AddTime;
                    DrAdd["PriceReport_ReplyContent"] = entity.PriceReport_ReplyContent;
                    DrAdd["PriceReport_ReplyTime"] = entity.PriceReport_ReplyTime;
                    DrAdd["PriceReport_IsReply"] = entity.PriceReport_IsReply;
                    DrAdd["PriceReport_AuditStatus"] = entity.PriceReport_AuditStatus;
                    DrAdd["PriceReport_Note"] = entity.PriceReport_Note;

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

        public virtual int DelSupplierPriceReport(int ID)
        {
            string SqlAdd = "DELETE FROM Supplier_PriceReport WHERE PriceReport_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual SupplierPriceReportInfo GetSupplierPriceReportByID(int ID)
        {
            SupplierPriceReportInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Supplier_PriceReport WHERE PriceReport_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SupplierPriceReportInfo();

                    entity.PriceReport_ID = Tools.NullInt(RdrList["PriceReport_ID"]);
                    entity.PriceReport_PurchaseID = Tools.NullInt(RdrList["PriceReport_PurchaseID"]);
                    entity.PriceReport_MemberID = Tools.NullInt(RdrList["PriceReport_MemberID"]);
                    entity.PriceReport_Title = Tools.NullStr(RdrList["PriceReport_Title"]);
                    entity.PriceReport_Name = Tools.NullStr(RdrList["PriceReport_Name"]);
                    entity.PriceReport_Phone = Tools.NullStr(RdrList["PriceReport_Phone"]);
                    entity.PriceReport_DeliveryDate = Tools.NullDate(RdrList["PriceReport_DeliveryDate"]);
                    entity.PriceReport_AddTime = Tools.NullDate(RdrList["PriceReport_AddTime"]);
                    entity.PriceReport_ReplyContent = Tools.NullStr(RdrList["PriceReport_ReplyContent"]);
                    entity.PriceReport_ReplyTime = Tools.NullDate(RdrList["PriceReport_ReplyTime"]);
                    entity.PriceReport_IsReply = Tools.NullInt(RdrList["PriceReport_IsReply"]);
                    entity.PriceReport_AuditStatus = Tools.NullInt(RdrList["PriceReport_AuditStatus"]);
                    entity.PriceReport_Note = Tools.NullStr(RdrList["PriceReport_Note"]);
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

        public virtual IList<SupplierPriceReportInfo> GetSupplierPriceReports(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<SupplierPriceReportInfo> entitys = null;
            SupplierPriceReportInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Supplier_PriceReport";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SupplierPriceReportInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SupplierPriceReportInfo();
                        entity.PriceReport_ID = Tools.NullInt(RdrList["PriceReport_ID"]);
                        entity.PriceReport_PurchaseID = Tools.NullInt(RdrList["PriceReport_PurchaseID"]);
                        entity.PriceReport_MemberID = Tools.NullInt(RdrList["PriceReport_MemberID"]);
                        entity.PriceReport_Title = Tools.NullStr(RdrList["PriceReport_Title"]);
                        entity.PriceReport_Name = Tools.NullStr(RdrList["PriceReport_Name"]);
                        entity.PriceReport_Phone = Tools.NullStr(RdrList["PriceReport_Phone"]);
                        entity.PriceReport_DeliveryDate = Tools.NullDate(RdrList["PriceReport_DeliveryDate"]);
                        entity.PriceReport_AddTime = Tools.NullDate(RdrList["PriceReport_AddTime"]);
                        entity.PriceReport_ReplyContent = Tools.NullStr(RdrList["PriceReport_ReplyContent"]);
                        entity.PriceReport_ReplyTime = Tools.NullDate(RdrList["PriceReport_ReplyTime"]);
                        entity.PriceReport_IsReply = Tools.NullInt(RdrList["PriceReport_IsReply"]);
                        entity.PriceReport_AuditStatus = Tools.NullInt(RdrList["PriceReport_AuditStatus"]);
                        entity.PriceReport_Note = Tools.NullStr(RdrList["PriceReport_Note"]);
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
                SqlTable = "Supplier_PriceReport";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(PriceReport_ID) FROM " + SqlTable + SqlParam;

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

    public class SupplierPriceReportDetail : ISupplierPriceReportDetail
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public SupplierPriceReportDetail()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddSupplierPriceReportDetail(SupplierPriceReportDetailInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Supplier_PriceReport_Detail";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Detail_ID"] = entity.Detail_ID;
            DrAdd["Detail_PriceReportID"] = entity.Detail_PriceReportID;
            DrAdd["Detail_PurchaseID"] = entity.Detail_PurchaseID;
            DrAdd["Detail_PurchaseDetailID"] = entity.Detail_PurchaseDetailID;
            DrAdd["Detail_Amount"] = entity.Detail_Amount;
            DrAdd["Detail_Price"] = entity.Detail_Price;

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

        public virtual bool EditSupplierPriceReportDetail(SupplierPriceReportDetailInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Supplier_PriceReport_Detail WHERE Detail_ID = " + entity.Detail_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Detail_ID"] = entity.Detail_ID;
                    DrAdd["Detail_PriceReportID"] = entity.Detail_PriceReportID;
                    DrAdd["Detail_PurchaseID"] = entity.Detail_PurchaseID;
                    DrAdd["Detail_PurchaseDetailID"] = entity.Detail_PurchaseDetailID;
                    DrAdd["Detail_Amount"] = entity.Detail_Amount;
                    DrAdd["Detail_Price"] = entity.Detail_Price;

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

        public virtual int DelSupplierPriceReportDetail(int ID)
        {
            string SqlAdd = "DELETE FROM Supplier_PriceReport_Detail WHERE Detail_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual int DelSupplierPriceReportDetailByPriceReportID(int ID)
        {
            string SqlAdd = "DELETE FROM Supplier_PriceReport_Detail WHERE Detail_PriceReportID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual SupplierPriceReportDetailInfo GetSupplierPriceReportDetailByID(int ID)
        {
            SupplierPriceReportDetailInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Supplier_PriceReport_Detail WHERE Detail_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SupplierPriceReportDetailInfo();

                    entity.Detail_ID = Tools.NullInt(RdrList["Detail_ID"]);
                    entity.Detail_PriceReportID = Tools.NullInt(RdrList["Detail_PriceReportID"]);
                    entity.Detail_PurchaseID = Tools.NullInt(RdrList["Detail_PurchaseID"]);
                    entity.Detail_PurchaseDetailID = Tools.NullInt(RdrList["Detail_PurchaseDetailID"]);
                    entity.Detail_Amount = Tools.NullInt(RdrList["Detail_Amount"]);
                    entity.Detail_Price = Tools.NullDbl(RdrList["Detail_Price"]);

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

        public virtual SupplierPriceReportDetailInfo GetSupplierPriceReportDetailByPurchaseDetailID(int Detail_PurchaseID, int Detail_PurchaseDetailID)
        {
            SupplierPriceReportDetailInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Supplier_PriceReport_Detail WHERE Detail_PurchaseID = " + Detail_PurchaseID + " AND Detail_PurchaseDetailID=" + Detail_PurchaseDetailID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SupplierPriceReportDetailInfo();

                    entity.Detail_ID = Tools.NullInt(RdrList["Detail_ID"]);
                    entity.Detail_PriceReportID = Tools.NullInt(RdrList["Detail_PriceReportID"]);
                    entity.Detail_PurchaseID = Tools.NullInt(RdrList["Detail_PurchaseID"]);
                    entity.Detail_PurchaseDetailID = Tools.NullInt(RdrList["Detail_PurchaseDetailID"]);
                    entity.Detail_Amount = Tools.NullInt(RdrList["Detail_Amount"]);
                    entity.Detail_Price = Tools.NullDbl(RdrList["Detail_Price"]);

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

        public virtual IList<SupplierPriceReportDetailInfo> GetSupplierPriceReportDetailsByPriceReportID(int ID)
        {
            IList<SupplierPriceReportDetailInfo> entitys = null;
            SupplierPriceReportDetailInfo entity = null;
            string SqlList;
            SqlDataReader RdrList = null;
            try
            {

                SqlList = "select * from Supplier_PriceReport_Detail where Detail_PriceReportID=" + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SupplierPriceReportDetailInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SupplierPriceReportDetailInfo();
                        entity.Detail_ID = Tools.NullInt(RdrList["Detail_ID"]);
                        entity.Detail_PriceReportID = Tools.NullInt(RdrList["Detail_PriceReportID"]);
                        entity.Detail_PurchaseID = Tools.NullInt(RdrList["Detail_PurchaseID"]);
                        entity.Detail_PurchaseDetailID = Tools.NullInt(RdrList["Detail_PurchaseDetailID"]);
                        entity.Detail_Amount = Tools.NullInt(RdrList["Detail_Amount"]);
                        entity.Detail_Price = Tools.NullDbl(RdrList["Detail_Price"]);

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

        public virtual IList<SupplierPriceReportDetailInfo> GetSupplierPriceReportDetails(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<SupplierPriceReportDetailInfo> entitys = null;
            SupplierPriceReportDetailInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Supplier_PriceReport_Detail";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SupplierPriceReportDetailInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SupplierPriceReportDetailInfo();
                        entity.Detail_ID = Tools.NullInt(RdrList["Detail_ID"]);
                        entity.Detail_PriceReportID = Tools.NullInt(RdrList["Detail_PriceReportID"]);
                        entity.Detail_PurchaseID = Tools.NullInt(RdrList["Detail_PurchaseID"]);
                        entity.Detail_PurchaseDetailID = Tools.NullInt(RdrList["Detail_PurchaseDetailID"]);
                        entity.Detail_Amount = Tools.NullInt(RdrList["Detail_Amount"]);
                        entity.Detail_Price = Tools.NullDbl(RdrList["Detail_Price"]);

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
                SqlTable = "Supplier_PriceReport_Detail";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Detail_ID) FROM " + SqlTable + SqlParam;

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
