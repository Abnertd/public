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
    public class ContractDividedPayment : IContractDividedPayment
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public ContractDividedPayment()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddContractDividedPayment(ContractDividedPaymentInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Contract_Divided_Payment";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Payment_ID"] = entity.Payment_ID;
            DrAdd["Payment_ContractID"] = entity.Payment_ContractID;
            DrAdd["Payment_Name"] = entity.Payment_Name;
            DrAdd["Payment_Amount"] = entity.Payment_Amount;
            DrAdd["Payment_PaymentLine"] = entity.Payment_PaymentLine;
            DrAdd["Payment_PaymentStatus"] = entity.Payment_PaymentStatus;
            DrAdd["Payment_PaymentTime"] = entity.Payment_PaymentTime;
            DrAdd["Payment_Note"] = entity.Payment_Note;

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

        public virtual bool EditContractDividedPayment(ContractDividedPaymentInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Contract_Divided_Payment WHERE Payment_ID = " + entity.Payment_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Payment_ID"] = entity.Payment_ID;
                    DrAdd["Payment_ContractID"] = entity.Payment_ContractID;
                    DrAdd["Payment_Name"] = entity.Payment_Name;
                    DrAdd["Payment_Amount"] = entity.Payment_Amount;
                    DrAdd["Payment_PaymentLine"] = entity.Payment_PaymentLine;
                    DrAdd["Payment_PaymentStatus"] = entity.Payment_PaymentStatus;
                    DrAdd["Payment_PaymentTime"] = entity.Payment_PaymentTime;
                    DrAdd["Payment_Note"] = entity.Payment_Note;
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

        public virtual int DelContractDividedPayment(int ID)
        {
            string SqlAdd = "DELETE FROM Contract_Divided_Payment WHERE Payment_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual ContractDividedPaymentInfo GetContractDividedPaymentByID(int ID)
        {
            ContractDividedPaymentInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Contract_Divided_Payment WHERE Payment_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new ContractDividedPaymentInfo();

                    entity.Payment_ID = Tools.NullInt(RdrList["Payment_ID"]);
                    entity.Payment_ContractID = Tools.NullInt(RdrList["Payment_ContractID"]);
                    entity.Payment_Name = Tools.NullStr(RdrList["Payment_Name"]);
                    entity.Payment_Amount = Tools.NullDbl(RdrList["Payment_Amount"]);
                    entity.Payment_PaymentLine = Tools.NullDate(RdrList["Payment_PaymentLine"]);
                    entity.Payment_PaymentStatus = Tools.NullInt(RdrList["Payment_PaymentStatus"]);
                    entity.Payment_PaymentTime = Tools.NullDate(RdrList["Payment_PaymentTime"]);
                    entity.Payment_Note = Tools.NullStr(RdrList["Payment_Note"]);
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

        public virtual IList<ContractDividedPaymentInfo> GetContractDividedPaymentsByContractID(int ContractID)
        {
            IList<ContractDividedPaymentInfo> entitys = null;
            ContractDividedPaymentInfo entity = null;
            string SqlList;
            SqlDataReader RdrList = null;
            try
            {
                SqlList = "SELECT * FROM Contract_Divided_Payment WHERE Payment_ContractID = " + ContractID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<ContractDividedPaymentInfo>();
                    while (RdrList.Read())
                    {
                        entity = new ContractDividedPaymentInfo();
                        entity.Payment_ID = Tools.NullInt(RdrList["Payment_ID"]);
                        entity.Payment_ContractID = Tools.NullInt(RdrList["Payment_ContractID"]);
                        entity.Payment_Name = Tools.NullStr(RdrList["Payment_Name"]);
                        entity.Payment_Amount = Tools.NullDbl(RdrList["Payment_Amount"]);
                        entity.Payment_PaymentLine = Tools.NullDate(RdrList["Payment_PaymentLine"]);
                        entity.Payment_PaymentStatus = Tools.NullInt(RdrList["Payment_PaymentStatus"]);
                        entity.Payment_PaymentTime = Tools.NullDate(RdrList["Payment_PaymentTime"]);
                        entity.Payment_Note = Tools.NullStr(RdrList["Payment_Note"]);
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

        public virtual IList<ContractDividedPaymentInfo> GetContractDividedPayments(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<ContractDividedPaymentInfo> entitys = null;
            ContractDividedPaymentInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Contract_Divided_Payment";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<ContractDividedPaymentInfo>();
                    while (RdrList.Read())
                    {
                        entity = new ContractDividedPaymentInfo();
                        entity.Payment_ID = Tools.NullInt(RdrList["Payment_ID"]);
                        entity.Payment_ContractID = Tools.NullInt(RdrList["Payment_ContractID"]);
                        entity.Payment_Name = Tools.NullStr(RdrList["Payment_Name"]);
                        entity.Payment_Amount = Tools.NullDbl(RdrList["Payment_Amount"]);
                        entity.Payment_PaymentLine = Tools.NullDate(RdrList["Payment_PaymentLine"]);
                        entity.Payment_PaymentStatus = Tools.NullInt(RdrList["Payment_PaymentStatus"]);
                        entity.Payment_PaymentTime = Tools.NullDate(RdrList["Payment_PaymentTime"]);
                        entity.Payment_Note = Tools.NullStr(RdrList["Payment_Note"]);
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
                SqlTable = "Contract_Divided_Payment";
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
