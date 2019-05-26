using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.B2C.Model;
using System.Data;
using System.Data.SqlClient;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public class SupplierSubAccountLog : ISupplierSubAccountLog
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public SupplierSubAccountLog()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddSupplierSubAccountLog(SupplierSubAccountLogInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Supplier_SubAccount_Log";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Log_ID"] = entity.Log_ID;
            DrAdd["Log_Supplier_ID"] = entity.Log_Supplier_ID;
            DrAdd["Log_SubAccount_ID"] = entity.Log_SubAccount_ID;
            DrAdd["Log_SubAccount_Action"] = entity.Log_SubAccount_Action;
            DrAdd["Log_SubAccount_Note"] = entity.Log_SubAccount_Note;
            DrAdd["Log_Addtime"] = entity.Log_Addtime;

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

        public virtual bool EditSupplierSubAccountLog(SupplierSubAccountLogInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Supplier_SubAccount_Log WHERE Log_ID = " + entity.Log_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Log_ID"] = entity.Log_ID;
                    DrAdd["Log_Supplier_ID"] = entity.Log_Supplier_ID;
                    DrAdd["Log_SubAccount_ID"] = entity.Log_SubAccount_ID;
                    DrAdd["Log_SubAccount_Action"] = entity.Log_SubAccount_Action;
                    DrAdd["Log_SubAccount_Note"] = entity.Log_SubAccount_Note;
                    DrAdd["Log_Addtime"] = entity.Log_Addtime;

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

        public virtual int DelSupplierSubAccountLog(int ID)
        {
            string SqlAdd = "DELETE FROM Supplier_SubAccount_Log WHERE Log_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual SupplierSubAccountLogInfo GetSupplierSubAccountLogByID(int ID)
        {
            SupplierSubAccountLogInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Supplier_SubAccount_Log WHERE Log_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SupplierSubAccountLogInfo();

                    entity.Log_ID = Tools.NullInt(RdrList["Log_ID"]);
                    entity.Log_Supplier_ID = Tools.NullInt(RdrList["Log_Supplier_ID"]);
                    entity.Log_SubAccount_ID = Tools.NullInt(RdrList["Log_SubAccount_ID"]);
                    entity.Log_SubAccount_Action = Tools.NullStr(RdrList["Log_SubAccount_Action"]);
                    entity.Log_SubAccount_Note = Tools.NullStr(RdrList["Log_SubAccount_Note"]);
                    entity.Log_Addtime = Tools.NullDate(RdrList["Log_Addtime"]);

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

        public virtual IList<SupplierSubAccountLogInfo> GetSupplierSubAccountLogs(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<SupplierSubAccountLogInfo> entitys = null;
            SupplierSubAccountLogInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Supplier_SubAccount_Log";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SupplierSubAccountLogInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SupplierSubAccountLogInfo();
                        entity.Log_ID = Tools.NullInt(RdrList["Log_ID"]);
                        entity.Log_Supplier_ID = Tools.NullInt(RdrList["Log_Supplier_ID"]);
                        entity.Log_SubAccount_ID = Tools.NullInt(RdrList["Log_SubAccount_ID"]);
                        entity.Log_SubAccount_Action = Tools.NullStr(RdrList["Log_SubAccount_Action"]);
                        entity.Log_SubAccount_Note = Tools.NullStr(RdrList["Log_SubAccount_Note"]);
                        entity.Log_Addtime = Tools.NullDate(RdrList["Log_Addtime"]);

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
                SqlTable = "Supplier_SubAccount_Log";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Log_ID) FROM " + SqlTable + SqlParam;

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
