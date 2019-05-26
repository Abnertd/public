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
    public class Sources : ISources
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public Sources()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddSources(SourcesInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Sources";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Sources_ID"] = entity.Sources_ID;
            DrAdd["Sources_Name"] = entity.Sources_Name;
            DrAdd["Sources_Code"] = entity.Sources_Code;
            DrAdd["Sources_Site"] = entity.Sources_Site;

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

        public virtual bool EditSources(SourcesInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Sources WHERE Sources_ID = " + entity.Sources_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Sources_ID"] = entity.Sources_ID;
                    DrAdd["Sources_Name"] = entity.Sources_Name;
                    DrAdd["Sources_Code"] = entity.Sources_Code;
                    DrAdd["Sources_Site"] = entity.Sources_Site;

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

        public virtual int DelSources(int ID)
        {
            string SqlAdd = "DELETE FROM Sources WHERE Sources_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual SourcesInfo GetSourcesByID(int ID)
        {
            SourcesInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Sources WHERE Sources_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SourcesInfo();

                    entity.Sources_ID = Tools.NullInt(RdrList["Sources_ID"]);
                    entity.Sources_Name = Tools.NullStr(RdrList["Sources_Name"]);
                    entity.Sources_Code = Tools.NullStr(RdrList["Sources_Code"]);
                    entity.Sources_Site = Tools.NullStr(RdrList["Sources_Site"]);

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

        public virtual SourcesInfo GetSourcesByCode(string Code)
        {
            SourcesInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Sources WHERE Sources_Code = '" + Code + "'";
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SourcesInfo();
                    entity.Sources_ID = Tools.NullInt(RdrList["Sources_ID"]);
                    entity.Sources_Name = Tools.NullStr(RdrList["Sources_Name"]);
                    entity.Sources_Code = Tools.NullStr(RdrList["Sources_Code"]);
                    entity.Sources_Site = Tools.NullStr(RdrList["Sources_Site"]);
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

        public virtual IList<SourcesInfo> GetSourcess(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<SourcesInfo> entitys = null;
            SourcesInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Sources";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SourcesInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SourcesInfo();
                        entity.Sources_ID = Tools.NullInt(RdrList["Sources_ID"]);
                        entity.Sources_Name = Tools.NullStr(RdrList["Sources_Name"]);
                        entity.Sources_Code = Tools.NullStr(RdrList["Sources_Code"]);
                        entity.Sources_Site = Tools.NullStr(RdrList["Sources_Site"]);

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
                SqlTable = "Sources";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Sources_ID) FROM " + SqlTable + SqlParam;

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
