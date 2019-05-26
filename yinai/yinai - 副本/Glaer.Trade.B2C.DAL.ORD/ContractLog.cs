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
    public class ContractLog : IContractLog
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public ContractLog()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddContractLog(ContractLogInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Contract_Log";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Log_ID"] = entity.Log_ID;
            DrAdd["Log_Operator"] = entity.Log_Operator;
            DrAdd["Log_Contact_ID"] = entity.Log_Contact_ID;
            DrAdd["Log_Result"] = entity.Log_Result;
            DrAdd["Log_Addtime"] = entity.Log_Addtime;
            DrAdd["Log_Action"] = entity.Log_Action;
            DrAdd["Log_Remark"] = entity.Log_Remark;

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

        public virtual bool EditContractLog(ContractLogInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Contract_Log WHERE Log_ID = " + entity.Log_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Log_ID"] = entity.Log_ID;
                    DrAdd["Log_Operator"] = entity.Log_Operator;
                    DrAdd["Log_Contact_ID"] = entity.Log_Contact_ID;
                    DrAdd["Log_Result"] = entity.Log_Result;
                    DrAdd["Log_Addtime"] = entity.Log_Addtime;
                    DrAdd["Log_Action"] = entity.Log_Action;
                    DrAdd["Log_Remark"] = entity.Log_Remark;

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

        public virtual int DelContractLog(int ID)
        {
            string SqlAdd = "DELETE FROM Contract_Log WHERE Log_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual ContractLogInfo GetContractLogByID(int ID)
        {
            ContractLogInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Contract_Log WHERE Log_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new ContractLogInfo();

                    entity.Log_ID = Tools.NullInt(RdrList["Log_ID"]);
                    entity.Log_Operator = Tools.NullStr(RdrList["Log_Operator"]);
                    entity.Log_Contact_ID = Tools.NullInt(RdrList["Log_Contact_ID"]);
                    entity.Log_Result = Tools.NullInt(RdrList["Log_Result"]);
                    entity.Log_Addtime = Tools.NullDate(RdrList["Log_Addtime"]);
                    entity.Log_Action = Tools.NullStr(RdrList["Log_Action"]);
                    entity.Log_Remark = Tools.NullStr(RdrList["Log_Remark"]);

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

        public virtual IList<ContractLogInfo> GetContractLogs(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<ContractLogInfo> entitys = null;
            ContractLogInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Contract_Log";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<ContractLogInfo>();
                    while (RdrList.Read())
                    {
                        entity = new ContractLogInfo();
                        entity.Log_ID = Tools.NullInt(RdrList["Log_ID"]);
                        entity.Log_Operator = Tools.NullStr(RdrList["Log_Operator"]);
                        entity.Log_Contact_ID = Tools.NullInt(RdrList["Log_Contact_ID"]);
                        entity.Log_Result = Tools.NullInt(RdrList["Log_Result"]);
                        entity.Log_Addtime = Tools.NullDate(RdrList["Log_Addtime"]);
                        entity.Log_Action = Tools.NullStr(RdrList["Log_Action"]);
                        entity.Log_Remark = Tools.NullStr(RdrList["Log_Remark"]);

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

        public virtual IList<ContractLogInfo> GetContractLogsByContractID(int Contract_ID)
        {
            IList<ContractLogInfo> entitys = null;
            ContractLogInfo entity = null;
            string SqlList;
            SqlDataReader RdrList = null;
            try
            {
                SqlList = "select * from Contract_Log where Log_Contact_ID="+Contract_ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<ContractLogInfo>();
                    while (RdrList.Read())
                    {
                        entity = new ContractLogInfo();
                        entity.Log_ID = Tools.NullInt(RdrList["Log_ID"]);
                        entity.Log_Operator = Tools.NullStr(RdrList["Log_Operator"]);
                        entity.Log_Contact_ID = Tools.NullInt(RdrList["Log_Contact_ID"]);
                        entity.Log_Result = Tools.NullInt(RdrList["Log_Result"]);
                        entity.Log_Addtime = Tools.NullDate(RdrList["Log_Addtime"]);
                        entity.Log_Action = Tools.NullStr(RdrList["Log_Action"]);
                        entity.Log_Remark = Tools.NullStr(RdrList["Log_Remark"]);

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
                SqlTable = "Contract_Log";
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
