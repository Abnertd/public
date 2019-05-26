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
    public class SupplierAccountLog : ISupplierAccountLog
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public SupplierAccountLog()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddSupplierAccountLog(SupplierAccountLogInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Supplier_Account_Log";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Account_Log_ID"] = entity.Account_Log_ID;
            DrAdd["Account_Log_Type"] = entity.Account_Log_Type;
            DrAdd["Account_Log_SupplierID"] = entity.Account_Log_SupplierID;
            DrAdd["Account_Log_Amount"] = entity.Account_Log_Amount;
            DrAdd["Account_Log_AmountRemain"] = entity.Account_Log_AmountRemain;
            DrAdd["Account_Log_Note"] = entity.Account_Log_Note;
            DrAdd["Account_Log_Addtime"] = entity.Account_Log_Addtime;

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

        public virtual bool EditSupplierAccountLog(SupplierAccountLogInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Supplier_Account_Log WHERE Account_Log_ID = " + entity.Account_Log_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Account_Log_ID"] = entity.Account_Log_ID;
                    DrAdd["Account_Log_Type"] = entity.Account_Log_Type;
                    DrAdd["Account_Log_SupplierID"] = entity.Account_Log_SupplierID;
                    DrAdd["Account_Log_Amount"] = entity.Account_Log_Amount;
                    DrAdd["Account_Log_AmountRemain"] = entity.Account_Log_AmountRemain;
                    DrAdd["Account_Log_Note"] = entity.Account_Log_Note;
                    DrAdd["Account_Log_Addtime"] = entity.Account_Log_Addtime;

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

        public virtual int DelSupplierAccountLog(int ID)
        {
            string SqlAdd = "DELETE FROM Supplier_Account_Log WHERE Account_Log_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual SupplierAccountLogInfo GetSupplierAccountLogByID(int ID)
        {
            SupplierAccountLogInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Supplier_Account_Log WHERE Account_Log_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SupplierAccountLogInfo();

                    entity.Account_Log_ID = Tools.NullInt(RdrList["Account_Log_ID"]);
                    entity.Account_Log_Type = Tools.NullInt(RdrList["Account_Log_Type"]);
                    entity.Account_Log_SupplierID = Tools.NullInt(RdrList["Account_Log_SupplierID"]);
                    entity.Account_Log_Amount = Tools.NullDbl(RdrList["Account_Log_Amount"]);
                    entity.Account_Log_AmountRemain = Tools.NullDbl(RdrList["Account_Log_AmountRemain"]);
                    entity.Account_Log_Note = Tools.NullStr(RdrList["Account_Log_Note"]);
                    entity.Account_Log_Addtime = Tools.NullDate(RdrList["Account_Log_Addtime"]);

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

        public virtual IList<SupplierAccountLogInfo> GetSupplierAccountLogBySupplierIDType(int Supplier_ID, int Account_Log_Type)
        {

            IList<SupplierAccountLogInfo> entitys = null;
            SupplierAccountLogInfo entity = null;
            string SqlList;
            SqlDataReader RdrList = null;
            try
            {

                SqlList = "Select * From Supplier_Account_Log Where Account_Log_SupplierID=" + Supplier_ID + " And Account_Log_Type" + Account_Log_Type;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SupplierAccountLogInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SupplierAccountLogInfo();
                        entity.Account_Log_ID = Tools.NullInt(RdrList["Account_Log_ID"]);
                        entity.Account_Log_Type = Tools.NullInt(RdrList["Account_Log_Type"]);
                        entity.Account_Log_SupplierID = Tools.NullInt(RdrList["Account_Log_SupplierID"]);
                        entity.Account_Log_Amount = Tools.NullDbl(RdrList["Account_Log_Amount"]);
                        entity.Account_Log_AmountRemain = Tools.NullDbl(RdrList["Account_Log_AmountRemain"]);
                        entity.Account_Log_Note = Tools.NullStr(RdrList["Account_Log_Note"]);
                        entity.Account_Log_Addtime = Tools.NullDate(RdrList["Account_Log_Addtime"]);

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

        public virtual IList<SupplierAccountLogInfo> GetSupplierAccountLogs(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<SupplierAccountLogInfo> entitys = null;
            SupplierAccountLogInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Supplier_Account_Log";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SupplierAccountLogInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SupplierAccountLogInfo();
                        entity.Account_Log_ID = Tools.NullInt(RdrList["Account_Log_ID"]);
                        entity.Account_Log_Type = Tools.NullInt(RdrList["Account_Log_Type"]);
                        entity.Account_Log_SupplierID = Tools.NullInt(RdrList["Account_Log_SupplierID"]);
                        entity.Account_Log_Amount = Tools.NullDbl(RdrList["Account_Log_Amount"]);
                        entity.Account_Log_AmountRemain = Tools.NullDbl(RdrList["Account_Log_AmountRemain"]);
                        entity.Account_Log_Note = Tools.NullStr(RdrList["Account_Log_Note"]);
                        entity.Account_Log_Addtime = Tools.NullDate(RdrList["Account_Log_Addtime"]);

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
                SqlTable = "Supplier_Account_Log";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Account_Log_ID) FROM " + SqlTable + SqlParam;

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
