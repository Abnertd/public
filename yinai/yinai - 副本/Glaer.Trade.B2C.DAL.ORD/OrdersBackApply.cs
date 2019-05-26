using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.Util.Tools;

namespace Glaer.Trade.B2C.DAL.ORD
{
    public class OrdersBackApply : IOrdersBackApply
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public OrdersBackApply()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddOrdersBackApply(OrdersBackApplyInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Orders_BackApply";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Orders_BackApply_ID"] = entity.Orders_BackApply_ID;
            DrAdd["Orders_BackApply_OrdersCode"] = entity.Orders_BackApply_OrdersCode;
            DrAdd["Orders_BackApply_MemberID"] = entity.Orders_BackApply_MemberID;
            DrAdd["Orders_BackApply_Name"] = entity.Orders_BackApply_Name;
            DrAdd["Orders_BackApply_Type"] = entity.Orders_BackApply_Type;
            DrAdd["Orders_BackApply_DeliveryWay"] = entity.Orders_BackApply_DeliveryWay;
            DrAdd["Orders_BackApply_AmountBackType"] = entity.Orders_BackApply_AmountBackType;
            DrAdd["Orders_BackApply_Amount"] = entity.Orders_BackApply_Amount;
            DrAdd["Orders_BackApply_Note"] = entity.Orders_BackApply_Note;
            DrAdd["Orders_BackApply_Account"] = entity.Orders_BackApply_Account;
            DrAdd["Orders_BackApply_SupplierNote"] = entity.Orders_BackApply_SupplierNote;
            DrAdd["Orders_BackApply_AdminNote"] = entity.Orders_BackApply_AdminNote;
            DrAdd["Orders_BackApply_SupplierTime"] = entity.Orders_BackApply_SupplierTime;
            DrAdd["Orders_BackApply_AdminTime"] = entity.Orders_BackApply_AdminTime;
            DrAdd["Orders_BackApply_Status"] = entity.Orders_BackApply_Status;
            DrAdd["Orders_BackApply_Addtime"] = entity.Orders_BackApply_Addtime;
            DrAdd["Orders_BackApply_Site"] = entity.Orders_BackApply_Site;

            DtAdd.Rows.Add(DrAdd);
            try
            {
                DBHelper.SaveChanges(SqlAdd, DtAdd);
                entity.Orders_BackApply_ID = GetLastOrderBackApply();
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

        public virtual bool EditOrdersBackApply(OrdersBackApplyInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Orders_BackApply WHERE Orders_BackApply_ID = " + entity.Orders_BackApply_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Orders_BackApply_ID"] = entity.Orders_BackApply_ID;
                    DrAdd["Orders_BackApply_OrdersCode"] = entity.Orders_BackApply_OrdersCode;
                    DrAdd["Orders_BackApply_MemberID"] = entity.Orders_BackApply_MemberID;
                    DrAdd["Orders_BackApply_Name"] = entity.Orders_BackApply_Name;
                    DrAdd["Orders_BackApply_Type"] = entity.Orders_BackApply_Type;
                    DrAdd["Orders_BackApply_DeliveryWay"] = entity.Orders_BackApply_DeliveryWay;
                    DrAdd["Orders_BackApply_AmountBackType"] = entity.Orders_BackApply_AmountBackType;
                    DrAdd["Orders_BackApply_Amount"] = entity.Orders_BackApply_Amount;
                    DrAdd["Orders_BackApply_Note"] = entity.Orders_BackApply_Note;
                    DrAdd["Orders_BackApply_Account"] = entity.Orders_BackApply_Account;
                    DrAdd["Orders_BackApply_SupplierNote"] = entity.Orders_BackApply_SupplierNote;
                    DrAdd["Orders_BackApply_AdminNote"] = entity.Orders_BackApply_AdminNote;
                    DrAdd["Orders_BackApply_SupplierTime"] = entity.Orders_BackApply_SupplierTime;
                    DrAdd["Orders_BackApply_AdminTime"] = entity.Orders_BackApply_AdminTime;
                    DrAdd["Orders_BackApply_Status"] = entity.Orders_BackApply_Status;
                    DrAdd["Orders_BackApply_Addtime"] = entity.Orders_BackApply_Addtime;
                    DrAdd["Orders_BackApply_Site"] = entity.Orders_BackApply_Site;
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

        public virtual int DelOrdersBackApply(int ID)
        {
            string SqlAdd = "DELETE FROM Orders_BackApply WHERE Orders_BackApply_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual OrdersBackApplyInfo GetOrdersBackApplyByID(int ID)
        {
            OrdersBackApplyInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Orders_BackApply WHERE Orders_BackApply_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new OrdersBackApplyInfo();

                    entity.Orders_BackApply_ID = Tools.NullInt(RdrList["Orders_BackApply_ID"]);
                    entity.Orders_BackApply_OrdersCode = Tools.NullStr(RdrList["Orders_BackApply_OrdersCode"]);
                    entity.Orders_BackApply_MemberID = Tools.NullInt(RdrList["Orders_BackApply_MemberID"]);
                    entity.Orders_BackApply_Name = Tools.NullStr(RdrList["Orders_BackApply_Name"]);
                    entity.Orders_BackApply_Type = Tools.NullInt(RdrList["Orders_BackApply_Type"]);
                    entity.Orders_BackApply_DeliveryWay = Tools.NullInt(RdrList["Orders_BackApply_DeliveryWay"]);
                    entity.Orders_BackApply_AmountBackType = Tools.NullInt(RdrList["Orders_BackApply_AmountBackType"]);
                    entity.Orders_BackApply_Amount = Tools.NullDbl(RdrList["Orders_BackApply_Amount"]);
                    entity.Orders_BackApply_Note = Tools.NullStr(RdrList["Orders_BackApply_Note"]);
                    entity.Orders_BackApply_Account = Tools.NullStr(RdrList["Orders_BackApply_Account"]);
                    entity.Orders_BackApply_AdminNote = Tools.NullStr(RdrList["Orders_BackApply_AdminNote"]);
                    entity.Orders_BackApply_SupplierNote = Tools.NullStr(RdrList["Orders_BackApply_SupplierNote"]);
                    entity.Orders_BackApply_SupplierTime = Tools.NullDate(RdrList["Orders_BackApply_SupplierTime"]);
                    entity.Orders_BackApply_AdminTime = Tools.NullDate(RdrList["Orders_BackApply_AdminTime"]);
                    entity.Orders_BackApply_Status = Tools.NullInt(RdrList["Orders_BackApply_Status"]);
                    entity.Orders_BackApply_Addtime = Tools.NullDate(RdrList["Orders_BackApply_Addtime"]);
                    entity.Orders_BackApply_Site = Tools.NullStr(RdrList["Orders_BackApply_Site"]);
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

        public virtual IList<OrdersBackApplyInfo> GetOrdersBackApplys(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<OrdersBackApplyInfo> entitys = null;
            OrdersBackApplyInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Orders_BackApply";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<OrdersBackApplyInfo>();
                    while (RdrList.Read())
                    {
                        entity = new OrdersBackApplyInfo();
                        entity.Orders_BackApply_ID = Tools.NullInt(RdrList["Orders_BackApply_ID"]);
                        entity.Orders_BackApply_OrdersCode = Tools.NullStr(RdrList["Orders_BackApply_OrdersCode"]);
                        entity.Orders_BackApply_MemberID = Tools.NullInt(RdrList["Orders_BackApply_MemberID"]);
                        entity.Orders_BackApply_Name = Tools.NullStr(RdrList["Orders_BackApply_Name"]);
                        entity.Orders_BackApply_Type = Tools.NullInt(RdrList["Orders_BackApply_Type"]);
                        entity.Orders_BackApply_DeliveryWay = Tools.NullInt(RdrList["Orders_BackApply_DeliveryWay"]);
                        entity.Orders_BackApply_AmountBackType = Tools.NullInt(RdrList["Orders_BackApply_AmountBackType"]);
                        entity.Orders_BackApply_Amount = Tools.NullDbl(RdrList["Orders_BackApply_Amount"]);
                        entity.Orders_BackApply_Note = Tools.NullStr(RdrList["Orders_BackApply_Note"]);
                        entity.Orders_BackApply_Account = Tools.NullStr(RdrList["Orders_BackApply_Account"]);
                        entity.Orders_BackApply_AdminNote = Tools.NullStr(RdrList["Orders_BackApply_AdminNote"]);
                        entity.Orders_BackApply_SupplierNote = Tools.NullStr(RdrList["Orders_BackApply_SupplierNote"]);
                        entity.Orders_BackApply_SupplierTime = Tools.NullDate(RdrList["Orders_BackApply_SupplierTime"]);
                        entity.Orders_BackApply_AdminTime = Tools.NullDate(RdrList["Orders_BackApply_AdminTime"]);
                        entity.Orders_BackApply_Status = Tools.NullInt(RdrList["Orders_BackApply_Status"]);
                        entity.Orders_BackApply_Addtime = Tools.NullDate(RdrList["Orders_BackApply_Addtime"]);
                        entity.Orders_BackApply_Site = Tools.NullStr(RdrList["Orders_BackApply_Site"]);

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
                SqlTable = "Orders_BackApply";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Orders_BackApply_ID) FROM " + SqlTable + SqlParam;

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

        private int GetLastOrderBackApply()
        {
            int Apply_ID = 0;
            string SqlList = "SELECT Orders_BackApply_ID FROM Orders_BackApply  ORDER BY Orders_BackApply_ID DESC";
            SqlDataReader RdrList = null;
            try
            {
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    Apply_ID = Tools.NullInt(RdrList[0]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                RdrList.Close();
                RdrList = null;
            }
            return Apply_ID;
        }

    }
}
