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
    public class OrdersPayment : IOrdersPayment
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public OrdersPayment()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddOrdersPayment(OrdersPaymentInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Orders_Payment";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();
            DrAdd["Orders_Payment_ID"] = entity.Orders_Payment_ID;
            DrAdd["Orders_Payment_OrdersID"] = entity.Orders_Payment_OrdersID;
            DrAdd["Orders_Payment_MemberID"] = entity.Orders_Payment_MemberID;
            DrAdd["Orders_Payment_PaymentStatus"] = entity.Orders_Payment_PaymentStatus;
            DrAdd["Orders_Payment_SysUserID"] = entity.Orders_Payment_SysUserID;
            DrAdd["Orders_Payment_DocNo"] = entity.Orders_Payment_DocNo;
            DrAdd["Orders_Payment_Name"] = entity.Orders_Payment_Name;
            DrAdd["Orders_Payment_ApplyAmount"] = entity.Orders_Payment_ApplyAmount;
            DrAdd["Orders_Payment_Amount"] = entity.Orders_Payment_Amount;
            DrAdd["Orders_Payment_Status"] = entity.Orders_Payment_Status;
            DrAdd["Orders_Payment_Note"] = entity.Orders_Payment_Note;
            DrAdd["Orders_Payment_Addtime"] = entity.Orders_Payment_Addtime;
            DrAdd["Orders_Payment_Site"] = entity.Orders_Payment_Site;
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

        public virtual bool EditOrdersPayment(OrdersPaymentInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Orders_Payment WHERE Orders_Payment_ID = " + entity.Orders_Payment_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try {
                if (DtAdd.Rows.Count > 0) {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Orders_Payment_ID"] = entity.Orders_Payment_ID;
                    DrAdd["Orders_Payment_OrdersID"] = entity.Orders_Payment_OrdersID;
                    DrAdd["Orders_Payment_MemberID"] = entity.Orders_Payment_MemberID;
                    DrAdd["Orders_Payment_PaymentStatus"] = entity.Orders_Payment_PaymentStatus;
                    DrAdd["Orders_Payment_SysUserID"] = entity.Orders_Payment_SysUserID;
                    DrAdd["Orders_Payment_DocNo"] = entity.Orders_Payment_DocNo;
                    DrAdd["Orders_Payment_Name"] = entity.Orders_Payment_Name;
                    DrAdd["Orders_Payment_ApplyAmount"] = entity.Orders_Payment_ApplyAmount;
                    DrAdd["Orders_Payment_Amount"] = entity.Orders_Payment_Amount;
                    DrAdd["Orders_Payment_Status"] = entity.Orders_Payment_Status;
                    DrAdd["Orders_Payment_Note"] = entity.Orders_Payment_Note;
                    DrAdd["Orders_Payment_Addtime"] = entity.Orders_Payment_Addtime;
                    DrAdd["Orders_Payment_Site"] = entity.Orders_Payment_Site;
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

        public virtual int DelOrdersPayment(int ID)
        {
            string SqlAdd = "DELETE FROM Orders_Payment WHERE Orders_Payment_ID = " + ID;
            try {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public virtual OrdersPaymentInfo GetOrdersPaymentByID(int ID)
        {
            OrdersPaymentInfo entity = null;
            SqlDataReader RdrList = null;
            try {
                string SqlList;
                SqlList = "SELECT * FROM Orders_Payment WHERE Orders_Payment_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read()) {
                    entity = new OrdersPaymentInfo();
                    entity.Orders_Payment_ID = Tools.NullInt(RdrList["Orders_Payment_ID"]);
                    entity.Orders_Payment_OrdersID = Tools.NullInt(RdrList["Orders_Payment_OrdersID"]);
                    entity.Orders_Payment_MemberID = Tools.NullInt(RdrList["Orders_Payment_MemberID"]);
                    entity.Orders_Payment_PaymentStatus = Tools.NullInt(RdrList["Orders_Payment_PaymentStatus"]);
                    entity.Orders_Payment_SysUserID = Tools.NullInt(RdrList["Orders_Payment_SysUserID"]);
                    entity.Orders_Payment_DocNo = Tools.NullStr(RdrList["Orders_Payment_DocNo"]);
                    entity.Orders_Payment_Name = Tools.NullStr(RdrList["Orders_Payment_Name"]);
                    entity.Orders_Payment_ApplyAmount = Tools.NullDbl(RdrList["Orders_Payment_ApplyAmount"]);
                    entity.Orders_Payment_Amount = Tools.NullDbl(RdrList["Orders_Payment_Amount"]);
                    entity.Orders_Payment_Status = Tools.NullInt(RdrList["Orders_Payment_Status"]);
                    entity.Orders_Payment_Note = Tools.NullStr(RdrList["Orders_Payment_Note"]);
                    entity.Orders_Payment_Addtime = Tools.NullDate(RdrList["Orders_Payment_Addtime"]);
                    entity.Orders_Payment_Site = Tools.NullStr(RdrList["Orders_Payment_Site"]);
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

        public virtual OrdersPaymentInfo GetOrdersPaymentByOrdersID(int Orders_ID,int Payment_Status)
        {
            OrdersPaymentInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Orders_Payment WHERE Orders_Payment_OrdersID = " + Orders_ID ;
                if (Payment_Status > 0)
                {
                    SqlList += " AND Orders_Payment_PaymentStatus=" + Payment_Status;
                }
                SqlList += " ORDER BY Orders_Payment_ID DESC";
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new OrdersPaymentInfo();
                    entity.Orders_Payment_ID = Tools.NullInt(RdrList["Orders_Payment_ID"]);
                    entity.Orders_Payment_OrdersID = Tools.NullInt(RdrList["Orders_Payment_OrdersID"]);
                    entity.Orders_Payment_MemberID = Tools.NullInt(RdrList["Orders_Payment_MemberID"]);
                    entity.Orders_Payment_PaymentStatus = Tools.NullInt(RdrList["Orders_Payment_PaymentStatus"]);
                    entity.Orders_Payment_SysUserID = Tools.NullInt(RdrList["Orders_Payment_SysUserID"]);
                    entity.Orders_Payment_DocNo = Tools.NullStr(RdrList["Orders_Payment_DocNo"]);
                    entity.Orders_Payment_Name = Tools.NullStr(RdrList["Orders_Payment_Name"]);
                    entity.Orders_Payment_ApplyAmount = Tools.NullDbl(RdrList["Orders_Payment_ApplyAmount"]);
                    entity.Orders_Payment_Amount = Tools.NullDbl(RdrList["Orders_Payment_Amount"]);
                    entity.Orders_Payment_Status = Tools.NullInt(RdrList["Orders_Payment_Status"]);
                    entity.Orders_Payment_Note = Tools.NullStr(RdrList["Orders_Payment_Note"]);
                    entity.Orders_Payment_Addtime = Tools.NullDate(RdrList["Orders_Payment_Addtime"]);
                    entity.Orders_Payment_Site = Tools.NullStr(RdrList["Orders_Payment_Site"]);
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

        public virtual OrdersPaymentInfo GetOrdersPaymentBySn(string SN)
        {
            OrdersPaymentInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Orders_Payment WHERE Orders_Payment_DocNo = '" + SN + "'";
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new OrdersPaymentInfo();
                    entity.Orders_Payment_ID = Tools.NullInt(RdrList["Orders_Payment_ID"]);
                    entity.Orders_Payment_OrdersID = Tools.NullInt(RdrList["Orders_Payment_OrdersID"]);
                    entity.Orders_Payment_MemberID = Tools.NullInt(RdrList["Orders_Payment_MemberID"]);
                    entity.Orders_Payment_PaymentStatus = Tools.NullInt(RdrList["Orders_Payment_PaymentStatus"]);
                    entity.Orders_Payment_SysUserID = Tools.NullInt(RdrList["Orders_Payment_SysUserID"]);
                    entity.Orders_Payment_DocNo = Tools.NullStr(RdrList["Orders_Payment_DocNo"]);
                    entity.Orders_Payment_Name = Tools.NullStr(RdrList["Orders_Payment_Name"]);
                    entity.Orders_Payment_ApplyAmount = Tools.NullDbl(RdrList["Orders_Payment_ApplyAmount"]);
                    entity.Orders_Payment_Amount = Tools.NullDbl(RdrList["Orders_Payment_Amount"]);
                    entity.Orders_Payment_Status = Tools.NullInt(RdrList["Orders_Payment_Status"]);
                    entity.Orders_Payment_Note = Tools.NullStr(RdrList["Orders_Payment_Note"]);
                    entity.Orders_Payment_Addtime = Tools.NullDate(RdrList["Orders_Payment_Addtime"]);
                    entity.Orders_Payment_Site = Tools.NullStr(RdrList["Orders_Payment_Site"]);
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

        public virtual IList<OrdersPaymentInfo> GetOrdersPayments(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<OrdersPaymentInfo> entitys = null;
            OrdersPaymentInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Orders_Payment";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows) {
                    entitys = new List<OrdersPaymentInfo>();
                    while (RdrList.Read()) {
                        entity = new OrdersPaymentInfo();
                        entity.Orders_Payment_ID = Tools.NullInt(RdrList["Orders_Payment_ID"]);
                        entity.Orders_Payment_OrdersID = Tools.NullInt(RdrList["Orders_Payment_OrdersID"]);
                        entity.Orders_Payment_MemberID = Tools.NullInt(RdrList["Orders_Payment_MemberID"]);
                        entity.Orders_Payment_PaymentStatus = Tools.NullInt(RdrList["Orders_Payment_PaymentStatus"]);
                        entity.Orders_Payment_SysUserID = Tools.NullInt(RdrList["Orders_Payment_SysUserID"]);
                        entity.Orders_Payment_DocNo = Tools.NullStr(RdrList["Orders_Payment_DocNo"]);
                        entity.Orders_Payment_Name = Tools.NullStr(RdrList["Orders_Payment_Name"]);
                        entity.Orders_Payment_ApplyAmount = Tools.NullDbl(RdrList["Orders_Payment_ApplyAmount"]);
                        entity.Orders_Payment_Amount = Tools.NullDbl(RdrList["Orders_Payment_Amount"]);
                        entity.Orders_Payment_Status = Tools.NullInt(RdrList["Orders_Payment_Status"]);
                        entity.Orders_Payment_Note = Tools.NullStr(RdrList["Orders_Payment_Note"]);
                        entity.Orders_Payment_Addtime = Tools.NullDate(RdrList["Orders_Payment_Addtime"]);
                        entity.Orders_Payment_Site = Tools.NullStr(RdrList["Orders_Payment_Site"]);
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

        public virtual IList<OrdersPaymentInfo> GetOrdersPaymentsByOrdersID(int OrdersID)
        {
            IList<OrdersPaymentInfo> entitys = null;
            OrdersPaymentInfo entity = null;
            string SqlList="";
            SqlDataReader RdrList = null;
            try
            {
                SqlList = "SELECT * FROM Orders_Payment WHERE Orders_Payment_OrdersID = " + OrdersID;

                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<OrdersPaymentInfo>();
                    while (RdrList.Read())
                    {
                        entity = new OrdersPaymentInfo();
                        entity.Orders_Payment_ID = Tools.NullInt(RdrList["Orders_Payment_ID"]);
                        entity.Orders_Payment_OrdersID = Tools.NullInt(RdrList["Orders_Payment_OrdersID"]);
                        entity.Orders_Payment_MemberID = Tools.NullInt(RdrList["Orders_Payment_MemberID"]);
                        entity.Orders_Payment_PaymentStatus = Tools.NullInt(RdrList["Orders_Payment_PaymentStatus"]);
                        entity.Orders_Payment_SysUserID = Tools.NullInt(RdrList["Orders_Payment_SysUserID"]);
                        entity.Orders_Payment_DocNo = Tools.NullStr(RdrList["Orders_Payment_DocNo"]);
                        entity.Orders_Payment_Name = Tools.NullStr(RdrList["Orders_Payment_Name"]);
                        entity.Orders_Payment_ApplyAmount = Tools.NullDbl(RdrList["Orders_Payment_ApplyAmount"]);
                        entity.Orders_Payment_Amount = Tools.NullDbl(RdrList["Orders_Payment_Amount"]);
                        entity.Orders_Payment_Status = Tools.NullInt(RdrList["Orders_Payment_Status"]);
                        entity.Orders_Payment_Note = Tools.NullStr(RdrList["Orders_Payment_Note"]);
                        entity.Orders_Payment_Addtime = Tools.NullDate(RdrList["Orders_Payment_Addtime"]);
                        entity.Orders_Payment_Site = Tools.NullStr(RdrList["Orders_Payment_Site"]);
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
                SqlTable = "Orders_Payment";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Orders_Payment_ID) FROM " + SqlTable + SqlParam;

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

        public virtual bool AddMemberPayLog(MemberPayLogInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Member_Pay_Log";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();
            DrAdd["Member_Pay_Log_ID"] = entity.Member_Pay_Log_ID;
            DrAdd["Member_Pay_Log_OrderSN"] = entity.Member_Pay_Log_OrderSN;
            DrAdd["Member_Pay_Log_PaywaySign"] = entity.Member_Pay_Log_PaywaySign;
            DrAdd["Member_Pay_Log_IsSuccess"] = entity.Member_Pay_Log_IsSuccess;
            DrAdd["Member_Pay_Log_Amount"] = entity.Member_Pay_Log_Amount;
            DrAdd["Member_Pay_Log_Note"] = entity.Member_Pay_Log_Note;
            DrAdd["Member_Pay_Log_Detail"] = entity.Member_Pay_Log_Detail;
            DrAdd["Member_Pay_Log_Addtime"] = entity.Member_Pay_Log_Addtime;
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

        public virtual IList<MemberPayLogInfo> GetMemberPayLogByOrdersSn(string Sn)
        {
            IList<MemberPayLogInfo> entitys = null;
            MemberPayLogInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Member_Pay_Log WHERE Member_Pay_Log_OrderSN = '" + Sn + "'";
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<MemberPayLogInfo>();
                    while (RdrList.Read())
                    {
                        entity = new MemberPayLogInfo();
                        entity.Member_Pay_Log_ID = Tools.NullInt(RdrList["Member_Pay_Log_ID"]);
                        entity.Member_Pay_Log_OrderSN = Tools.NullStr(RdrList["Member_Pay_Log_OrderSN"]);
                        entity.Member_Pay_Log_PaywaySign = Tools.NullStr(RdrList["Member_Pay_Log_PaywaySign"]);
                        entity.Member_Pay_Log_IsSuccess = Tools.NullInt(RdrList["Member_Pay_Log_IsSuccess"]);
                        entity.Member_Pay_Log_Amount = Tools.NullDbl(RdrList["Member_Pay_Log_Amount"]);
                        entity.Member_Pay_Log_Note = Tools.NullStr(RdrList["Member_Pay_Log_Note"]);
                        entity.Member_Pay_Log_Detail = Tools.NullStr(RdrList["Member_Pay_Log_Detail"]);
                        entity.Member_Pay_Log_Addtime = Tools.NullDate(RdrList["Member_Pay_Log_Addtime"]);
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
