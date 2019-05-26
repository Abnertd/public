using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.B2C.DAL.ORD;

namespace Glaer.Trade.B2C.DAL.ORD
{
    public class PaymentInformation : IPaymentInformation
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public PaymentInformation()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddPaymentInformation(PaymentInformationInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Payment_Information";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Payment_ID"] = entity.Payment_ID;
            DrAdd["Payment_PayingTeller"] = entity.Payment_PayingTeller;
            DrAdd["Payment_Account"] = entity.Payment_Account;
            DrAdd["Payment_Receivable"] = entity.Payment_Receivable;
            DrAdd["Payment_Account_Receivable"] = entity.Payment_Account_Receivable;
            DrAdd["Payment_Type"] = entity.Payment_Type;
            DrAdd["Payment_Amount"] = entity.Payment_Amount;
            DrAdd["Payment_Remarks"] = entity.Payment_Remarks;
            DrAdd["Payment_Account_Time"] = entity.Payment_Account_Time;
            DrAdd["Payment_Status"] = entity.Payment_Status;
            DrAdd["Payment_Flow"] = entity.Payment_Flow;
            DrAdd["Payment_Remarks1"] = entity.Payment_Remarks1;

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

        public virtual bool EditPaymentInformation(PaymentInformationInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Payment_Information WHERE Payment_ID = " + entity.Payment_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Payment_ID"] = entity.Payment_ID;
                    DrAdd["Payment_PayingTeller"] = entity.Payment_PayingTeller;
                    DrAdd["Payment_Account"] = entity.Payment_Account;
                    DrAdd["Payment_Receivable"] = entity.Payment_Receivable;
                    DrAdd["Payment_Account_Receivable"] = entity.Payment_Account_Receivable;
                    DrAdd["Payment_Type"] = entity.Payment_Type;
                    DrAdd["Payment_Amount"] = entity.Payment_Amount;
                    DrAdd["Payment_Remarks"] = entity.Payment_Remarks;
                    DrAdd["Payment_Account_Time"] = entity.Payment_Account_Time;
                    DrAdd["Payment_Status"] = entity.Payment_Status;
                    DrAdd["Payment_Flow"] = entity.Payment_Flow;
                    DrAdd["Payment_Remarks1"] = entity.Payment_Remarks1;

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

        public virtual int DelPaymentInformation(int ID)
        {
            string SqlAdd = "DELETE FROM Payment_Information WHERE Payment_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual PaymentInformationInfo GetPaymentInformationByID(int ID)
        {
            PaymentInformationInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Payment_Information WHERE Payment_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new PaymentInformationInfo();

                    entity.Payment_ID = Tools.NullInt(RdrList["Payment_ID"]);
                    entity.Payment_PayingTeller = Tools.NullStr(RdrList["Payment_PayingTeller"]);
                    entity.Payment_Account = Tools.NullStr(RdrList["Payment_Account"]);
                    entity.Payment_Receivable = Tools.NullStr(RdrList["Payment_Receivable"]);
                    entity.Payment_Account_Receivable = Tools.NullStr(RdrList["Payment_Account_Receivable"]);
                    entity.Payment_Type = Tools.NullInt(RdrList["Payment_Type"]);
                    entity.Payment_Amount = Tools.NullDbl(RdrList["Payment_Amount"]);
                    entity.Payment_Remarks = Tools.NullStr(RdrList["Payment_Remarks"]);
                    entity.Payment_Account_Time = Tools.NullDate(RdrList["Payment_Account_Time"]);
                    entity.Payment_Status = Tools.NullInt(RdrList["Payment_Status"]);
                    entity.Payment_Flow = Tools.NullStr(RdrList["Payment_Flow"]);
                    entity.Payment_Remarks1 = Tools.NullStr(RdrList["Payment_Remarks1"]);

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

        public virtual IList<PaymentInformationInfo> GetPaymentInformations(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<PaymentInformationInfo> entitys = null;
            PaymentInformationInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Payment_Information";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<PaymentInformationInfo>();
                    while (RdrList.Read())
                    {
                        entity = new PaymentInformationInfo();
                        entity.Payment_ID = Tools.NullInt(RdrList["Payment_ID"]);
                        entity.Payment_PayingTeller = Tools.NullStr(RdrList["Payment_PayingTeller"]);
                        entity.Payment_Account = Tools.NullStr(RdrList["Payment_Account"]);
                        entity.Payment_Receivable = Tools.NullStr(RdrList["Payment_Receivable"]);
                        entity.Payment_Account_Receivable = Tools.NullStr(RdrList["Payment_Account_Receivable"]);
                        entity.Payment_Type = Tools.NullInt(RdrList["Payment_Type"]);
                        entity.Payment_Amount = Tools.NullDbl(RdrList["Payment_Amount"]);
                        entity.Payment_Remarks = Tools.NullStr(RdrList["Payment_Remarks"]);
                        entity.Payment_Account_Time = Tools.NullDate(RdrList["Payment_Account_Time"]);
                        entity.Payment_Status = Tools.NullInt(RdrList["Payment_Status"]);
                        entity.Payment_Flow = Tools.NullStr(RdrList["Payment_Flow"]);
                        entity.Payment_Remarks1 = Tools.NullStr(RdrList["Payment_Remarks1"]);

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
                SqlTable = "Payment_Information";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Payment_ID) FROM " + SqlTable + SqlParam;

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
