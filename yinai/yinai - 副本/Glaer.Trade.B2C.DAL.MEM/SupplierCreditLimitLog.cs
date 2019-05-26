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
    public class SupplierCreditLimitLog : ISupplierCreditLimitLog
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public SupplierCreditLimitLog()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddSupplierCreditLimitLog(SupplierCreditLimitLogInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Supplier_CreditLimit_Log";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["CreditLimit_Log_ID"] = entity.CreditLimit_Log_ID;
            DrAdd["CreditLimit_Log_Type"] = entity.CreditLimit_Log_Type;
            DrAdd["CreditLimit_Log_SupplierID"] = entity.CreditLimit_Log_SupplierID;
            DrAdd["CreditLimit_Log_Amount"] = entity.CreditLimit_Log_Amount;
            DrAdd["CreditLimit_Log_AmountRemain"] = entity.CreditLimit_Log_AmountRemain;
            DrAdd["CreditLimit_Log_Note"] = entity.CreditLimit_Log_Note;
            DrAdd["CreditLimit_Log_Addtime"] = entity.CreditLimit_Log_Addtime;
            DrAdd["CreditLimit_Log_PaymentStatus"] = entity.CreditLimit_Log_PaymentStatus;

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

        public virtual bool EditSupplierCreditLimitLog(SupplierCreditLimitLogInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Supplier_CreditLimit_Log WHERE CreditLimit_Log_ID = " + entity.CreditLimit_Log_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["CreditLimit_Log_ID"] = entity.CreditLimit_Log_ID;
                    DrAdd["CreditLimit_Log_Type"] = entity.CreditLimit_Log_Type;
                    DrAdd["CreditLimit_Log_SupplierID"] = entity.CreditLimit_Log_SupplierID;
                    DrAdd["CreditLimit_Log_Amount"] = entity.CreditLimit_Log_Amount;
                    DrAdd["CreditLimit_Log_AmountRemain"] = entity.CreditLimit_Log_AmountRemain;
                    DrAdd["CreditLimit_Log_Note"] = entity.CreditLimit_Log_Note;
                    DrAdd["CreditLimit_Log_Addtime"] = entity.CreditLimit_Log_Addtime;
                    DrAdd["CreditLimit_Log_PaymentStatus"] = entity.CreditLimit_Log_PaymentStatus;
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

        public virtual int DelSupplierCreditLimitLog(int ID)
        {
            string SqlAdd = "DELETE FROM Supplier_CreditLimit_Log WHERE CreditLimit_Log_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual SupplierCreditLimitLogInfo GetSupplierCreditLimitLogByID(int ID)
        {
            SupplierCreditLimitLogInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Supplier_CreditLimit_Log WHERE CreditLimit_Log_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SupplierCreditLimitLogInfo();

                    entity.CreditLimit_Log_ID = Tools.NullInt(RdrList["CreditLimit_Log_ID"]);
                    entity.CreditLimit_Log_Type = Tools.NullInt(RdrList["CreditLimit_Log_Type"]);
                    entity.CreditLimit_Log_SupplierID = Tools.NullInt(RdrList["CreditLimit_Log_SupplierID"]);
                    entity.CreditLimit_Log_Amount = Tools.NullDbl(RdrList["CreditLimit_Log_Amount"]);
                    entity.CreditLimit_Log_AmountRemain = Tools.NullDbl(RdrList["CreditLimit_Log_AmountRemain"]);
                    entity.CreditLimit_Log_Note = Tools.NullStr(RdrList["CreditLimit_Log_Note"]);
                    entity.CreditLimit_Log_Addtime = Tools.NullDate(RdrList["CreditLimit_Log_Addtime"]);
                    entity.CreditLimit_Log_PaymentStatus = Tools.NullInt(RdrList["CreditLimit_Log_PaymentStatus"]);

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

        public virtual IList<SupplierCreditLimitLogInfo> GetSupplierCreditLimitLogBySupplierIDType(int Supplier_ID, int CreditLimit_Log_Type)
        {

            IList<SupplierCreditLimitLogInfo> entitys = null;
            SupplierCreditLimitLogInfo entity = null;
            string SqlList;
            SqlDataReader RdrList = null;
            try
            {

                SqlList = "Select * From Supplier_CreditLimit_Log Where CreditLimit_Log_SupplierID=" + Supplier_ID + " And CreditLimit_Log_Type" + CreditLimit_Log_Type;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SupplierCreditLimitLogInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SupplierCreditLimitLogInfo();
                        entity.CreditLimit_Log_ID = Tools.NullInt(RdrList["CreditLimit_Log_ID"]);
                        entity.CreditLimit_Log_Type = Tools.NullInt(RdrList["CreditLimit_Log_Type"]);
                        entity.CreditLimit_Log_SupplierID = Tools.NullInt(RdrList["CreditLimit_Log_SupplierID"]);
                        entity.CreditLimit_Log_Amount = Tools.NullDbl(RdrList["CreditLimit_Log_Amount"]);
                        entity.CreditLimit_Log_AmountRemain = Tools.NullDbl(RdrList["CreditLimit_Log_AmountRemain"]);
                        entity.CreditLimit_Log_Note = Tools.NullStr(RdrList["CreditLimit_Log_Note"]);
                        entity.CreditLimit_Log_Addtime = Tools.NullDate(RdrList["CreditLimit_Log_Addtime"]);
                        entity.CreditLimit_Log_PaymentStatus = Tools.NullInt(RdrList["CreditLimit_Log_PaymentStatus"]);
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

        public virtual IList<SupplierCreditLimitLogInfo> GetSupplierCreditLimitLogs(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<SupplierCreditLimitLogInfo> entitys = null;
            SupplierCreditLimitLogInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Supplier_CreditLimit_Log";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SupplierCreditLimitLogInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SupplierCreditLimitLogInfo();
                        entity.CreditLimit_Log_ID = Tools.NullInt(RdrList["CreditLimit_Log_ID"]);
                        entity.CreditLimit_Log_Type = Tools.NullInt(RdrList["CreditLimit_Log_Type"]);
                        entity.CreditLimit_Log_SupplierID = Tools.NullInt(RdrList["CreditLimit_Log_SupplierID"]);
                        entity.CreditLimit_Log_Amount = Tools.NullDbl(RdrList["CreditLimit_Log_Amount"]);
                        entity.CreditLimit_Log_AmountRemain = Tools.NullDbl(RdrList["CreditLimit_Log_AmountRemain"]);
                        entity.CreditLimit_Log_Note = Tools.NullStr(RdrList["CreditLimit_Log_Note"]);
                        entity.CreditLimit_Log_Addtime = Tools.NullDate(RdrList["CreditLimit_Log_Addtime"]);
                        entity.CreditLimit_Log_PaymentStatus = Tools.NullInt(RdrList["CreditLimit_Log_PaymentStatus"]);
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
                SqlTable = "Supplier_CreditLimit_Log";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(CreditLimit_Log_ID) FROM " + SqlTable + SqlParam;

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
