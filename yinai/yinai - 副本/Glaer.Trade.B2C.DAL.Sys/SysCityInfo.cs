using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.Util.Tools;

namespace Glaer.Trade.B2C.DAL.Sys
{
    public class SysCity : ISysCity
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public SysCity()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddSysCity(SysCityInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Sys_City";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Sys_City_ID"] = entity.Sys_City_ID;
            DrAdd["Sys_City_StateCode"] = entity.Sys_City_StateCode;
            DrAdd["Sys_City_Code"] = entity.Sys_City_Code;
            DrAdd["Sys_City_CN"] = entity.Sys_City_CN;
            DrAdd["Sys_City_IsActive"] = entity.Sys_City_IsActive;

            DtAdd.Rows.Add(DrAdd);
            try
            {
                DBHelper.SaveChanges(SqlAdd, DtAdd);
                entity = GetLastSysCityInfo();
                entity.Sys_City_Code = Tools.NullStr(entity.Sys_City_ID);
                EditSysCity(entity);
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

        public virtual bool EditSysCity(SysCityInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Sys_City WHERE Sys_City_ID = " + entity.Sys_City_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Sys_City_ID"] = entity.Sys_City_ID;
                    DrAdd["Sys_City_StateCode"] = entity.Sys_City_StateCode;
                    DrAdd["Sys_City_Code"] = entity.Sys_City_Code;
                    DrAdd["Sys_City_CN"] = entity.Sys_City_CN;
                    DrAdd["Sys_City_IsActive"] = entity.Sys_City_IsActive;

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

        public virtual int DelSysCity(int ID)
        {
            string SqlAdd = "DELETE FROM Sys_City WHERE Sys_City_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual SysCityInfo GetSysCityByID(int ID)
        {
            SysCityInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Sys_City WHERE Sys_City_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SysCityInfo();

                    entity.Sys_City_ID = Tools.NullInt(RdrList["Sys_City_ID"]);
                    entity.Sys_City_StateCode = Tools.NullStr(RdrList["Sys_City_StateCode"]);
                    entity.Sys_City_Code = Tools.NullStr(RdrList["Sys_City_Code"]);
                    entity.Sys_City_CN = Tools.NullStr(RdrList["Sys_City_CN"]);
                    entity.Sys_City_IsActive = Tools.NullInt(RdrList["Sys_City_IsActive"]);

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

        public virtual IList<SysCityInfo> GetSysCitys(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<SysCityInfo> entitys = null;
            SysCityInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Sys_City";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SysCityInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SysCityInfo();
                        entity.Sys_City_ID = Tools.NullInt(RdrList["Sys_City_ID"]);
                        entity.Sys_City_StateCode = Tools.NullStr(RdrList["Sys_City_StateCode"]);
                        entity.Sys_City_Code = Tools.NullStr(RdrList["Sys_City_Code"]);
                        entity.Sys_City_CN = Tools.NullStr(RdrList["Sys_City_CN"]);
                        entity.Sys_City_IsActive = Tools.NullInt(RdrList["Sys_City_IsActive"]);

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
                SqlTable = "Sys_City";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Sys_City_ID) FROM " + SqlTable + SqlParam;

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


        public SysCityInfo GetLastSysCityInfo()
        {
            string sql = "";
            sql = "select Top 1 * from Sys_City ORDER BY Sys_City_ID DESC";
            SqlDataReader RdrList = null;
            SysCityInfo entity = null;
            try
            {
                RdrList = DBHelper.ExecuteReader(sql);
                if (RdrList.Read())
                {
                    entity = new SysCityInfo();
                    entity.Sys_City_ID = Tools.NullInt(RdrList["Sys_City_ID"]);
                    entity.Sys_City_StateCode = Tools.NullStr(RdrList["Sys_City_StateCode"]);
                    entity.Sys_City_Code = Tools.NullStr(RdrList["Sys_City_Code"]);
                    entity.Sys_City_CN = Tools.NullStr(RdrList["Sys_City_CN"]);
                    entity.Sys_City_IsActive = Tools.NullInt(RdrList["Sys_City_IsActive"]);

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
