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
    public class SysCounty : ISysCounty
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public SysCounty()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddSysCounty(SysCountyInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Sys_County";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Sys_County_ID"] = entity.Sys_County_ID;
            DrAdd["Sys_County_CityCode"] = entity.Sys_County_CityCode;
            DrAdd["Sys_County_Code"] = entity.Sys_County_Code;
            DrAdd["Sys_County_CN"] = entity.Sys_County_CN;
            DrAdd["Sys_County_IsActive"] = entity.Sys_County_IsActive;

            DtAdd.Rows.Add(DrAdd);
            try
            {
                DBHelper.SaveChanges(SqlAdd, DtAdd);
                entity = GetLastSysCountyInfo();
                entity.Sys_County_Code = Tools.NullStr(entity.Sys_County_ID);
                EditSysCounty(entity);
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

        public virtual bool EditSysCounty(SysCountyInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Sys_County WHERE Sys_County_ID = " + entity.Sys_County_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Sys_County_ID"] = entity.Sys_County_ID;
                    DrAdd["Sys_County_CityCode"] = entity.Sys_County_CityCode;
                    DrAdd["Sys_County_Code"] = entity.Sys_County_Code;
                    DrAdd["Sys_County_CN"] = entity.Sys_County_CN;
                    DrAdd["Sys_County_IsActive"] = entity.Sys_County_IsActive;

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

        public virtual int DelSysCounty(int ID)
        {
            string SqlAdd = "DELETE FROM Sys_County WHERE Sys_County_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual SysCountyInfo GetSysCountyByID(int ID)
        {
            SysCountyInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Sys_County WHERE Sys_County_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SysCountyInfo();

                    entity.Sys_County_ID = Tools.NullInt(RdrList["Sys_County_ID"]);
                    entity.Sys_County_CityCode = Tools.NullStr(RdrList["Sys_County_CityCode"]);
                    entity.Sys_County_Code = Tools.NullStr(RdrList["Sys_County_Code"]);
                    entity.Sys_County_CN = Tools.NullStr(RdrList["Sys_County_CN"]);
                    entity.Sys_County_IsActive = Tools.NullInt(RdrList["Sys_County_IsActive"]);

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

        public virtual IList<SysCountyInfo> GetSysCountys(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<SysCountyInfo> entitys = null;
            SysCountyInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Sys_County";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SysCountyInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SysCountyInfo();
                        entity.Sys_County_ID = Tools.NullInt(RdrList["Sys_County_ID"]);
                        entity.Sys_County_CityCode = Tools.NullStr(RdrList["Sys_County_CityCode"]);
                        entity.Sys_County_Code = Tools.NullStr(RdrList["Sys_County_Code"]);
                        entity.Sys_County_CN = Tools.NullStr(RdrList["Sys_County_CN"]);
                        entity.Sys_County_IsActive = Tools.NullInt(RdrList["Sys_County_IsActive"]);

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
                SqlTable = "Sys_County";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Sys_County_ID) FROM " + SqlTable + SqlParam;

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

        public SysCountyInfo GetLastSysCountyInfo()
        {
            string sql = "";
            sql = "select Top 1 * from Sys_County ORDER BY Sys_County_ID DESC";
            SqlDataReader RdrList = null;
            SysCountyInfo entity = null;
            try
            {
                RdrList = DBHelper.ExecuteReader(sql);
                if (RdrList.Read())
                {
                    entity = new SysCountyInfo();
                    entity.Sys_County_ID = Tools.NullInt(RdrList["Sys_County_ID"]);
                    entity.Sys_County_CityCode = Tools.NullStr(RdrList["Sys_County_CityCode"]);
                    entity.Sys_County_Code = Tools.NullStr(RdrList["Sys_County_Code"]);
                    entity.Sys_County_CN = Tools.NullStr(RdrList["Sys_County_CN"]);
                    entity.Sys_County_IsActive = Tools.NullInt(RdrList["Sys_County_IsActive"]);

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
