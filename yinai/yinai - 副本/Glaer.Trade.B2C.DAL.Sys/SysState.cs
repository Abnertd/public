using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.Util.Tools;

namespace Glaer.Trade.B2C.DAL.Sys
{
    public class SysState : ISysState
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public SysState()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddSysState(SysStateInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Sys_State";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Sys_State_ID"] = entity.Sys_State_ID;
            DrAdd["Sys_State_CountryCode"] = entity.Sys_State_CountryCode;
            DrAdd["Sys_State_Code"] = entity.Sys_State_Code;
            DrAdd["Sys_State_CN"] = entity.Sys_State_CN;
            DrAdd["Sys_State_IsActive"] = entity.Sys_State_IsActive;

            DtAdd.Rows.Add(DrAdd);
            try
            {
                DBHelper.SaveChanges(SqlAdd, DtAdd);
                entity = GetLastSysStateInfo();
                entity.Sys_State_Code = Tools.NullStr(entity.Sys_State_ID);
                EditSysState(entity);
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

        public virtual bool EditSysState(SysStateInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Sys_State WHERE Sys_State_ID = " + entity.Sys_State_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Sys_State_ID"] = entity.Sys_State_ID;
                    DrAdd["Sys_State_CountryCode"] = entity.Sys_State_CountryCode;
                    DrAdd["Sys_State_Code"] = entity.Sys_State_Code;
                    DrAdd["Sys_State_CN"] = entity.Sys_State_CN;
                    DrAdd["Sys_State_IsActive"] = entity.Sys_State_IsActive;

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

        public virtual int DelSysState(int ID)
        {
            string SqlAdd = "DELETE FROM Sys_State WHERE Sys_State_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual SysStateInfo GetSysStateByID(int ID)
        {
            SysStateInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Sys_State WHERE Sys_State_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SysStateInfo();

                    entity.Sys_State_ID = Tools.NullInt(RdrList["Sys_State_ID"]);
                    entity.Sys_State_CountryCode = Tools.NullStr(RdrList["Sys_State_CountryCode"]);
                    entity.Sys_State_Code = Tools.NullStr(RdrList["Sys_State_Code"]);
                    entity.Sys_State_CN = Tools.NullStr(RdrList["Sys_State_CN"]);
                    entity.Sys_State_IsActive = Tools.NullInt(RdrList["Sys_State_IsActive"]);

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

        public virtual IList<SysStateInfo> GetSysStates(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<SysStateInfo> entitys = null;
            SysStateInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Sys_State";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SysStateInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SysStateInfo();
                        entity.Sys_State_ID = Tools.NullInt(RdrList["Sys_State_ID"]);
                        entity.Sys_State_CountryCode = Tools.NullStr(RdrList["Sys_State_CountryCode"]);
                        entity.Sys_State_Code = Tools.NullStr(RdrList["Sys_State_Code"]);
                        entity.Sys_State_CN = Tools.NullStr(RdrList["Sys_State_CN"]);
                        entity.Sys_State_IsActive = Tools.NullInt(RdrList["Sys_State_IsActive"]);

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
                SqlTable = "Sys_State";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Sys_State_ID) FROM " + SqlTable + SqlParam;

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

        public SysStateInfo GetLastSysStateInfo()
        {
            string sql = "";
            sql = "select Top 1 * from Sys_State ORDER BY Sys_State_ID DESC";
            SqlDataReader RdrList = null;
            SysStateInfo entity = null;
            try
            {
                RdrList = DBHelper.ExecuteReader(sql);
                if (RdrList.Read())
                {
                    entity = new SysStateInfo();
                    entity.Sys_State_ID = Tools.NullInt(RdrList["Sys_State_ID"]);
                    entity.Sys_State_CountryCode = Tools.NullStr(RdrList["Sys_State_CountryCode"]);
                    entity.Sys_State_Code = Tools.NullStr(RdrList["Sys_State_Code"]);
                    entity.Sys_State_CN = Tools.NullStr(RdrList["Sys_State_CN"]);
                    entity.Sys_State_IsActive = Tools.NullInt(RdrList["Sys_State_IsActive"]);

                }
                return entity;
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

        }

    }

}
