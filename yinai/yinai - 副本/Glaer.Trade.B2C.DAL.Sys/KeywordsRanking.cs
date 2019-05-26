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
    public class KeywordsRanking : IKeywordsRanking
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public KeywordsRanking()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddKeywordsRanking(KeywordsRankingInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Keywords_Ranking";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["ID"] = entity.ID;
            DrAdd["Type"] = entity.Type;
            DrAdd["Keyword"] = entity.Keyword;
            DrAdd["addtime"] = entity.addtime;
            DrAdd["Site"] = entity.Site;

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

        public virtual bool EditKeywordsRanking(KeywordsRankingInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Keywords_Ranking WHERE ID = " + entity.ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["ID"] = entity.ID;
                    DrAdd["Type"] = entity.Type;
                    DrAdd["Keyword"] = entity.Keyword;
                    DrAdd["addtime"] = entity.addtime;
                    DrAdd["Site"] = entity.Site;

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

        public virtual int DelKeywordsRanking(int ID)
        {
            string SqlAdd = "DELETE FROM Keywords_Ranking WHERE ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual KeywordsRankingInfo GetKeywordsRankingByID(int ID)
        {
            KeywordsRankingInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Keywords_Ranking WHERE ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new KeywordsRankingInfo();

                    entity.ID = Tools.NullInt(RdrList["ID"]);
                    entity.Type = Tools.NullInt(RdrList["Type"]);
                    entity.Keyword = Tools.NullStr(RdrList["Keyword"]);
                    entity.addtime = Tools.NullDate(RdrList["addtime"]);
                    entity.Site = Tools.NullStr(RdrList["Site"]);

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

        public virtual IList<KeywordsRankingInfo> GetKeywordsRankings(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<KeywordsRankingInfo> entitys = null;
            KeywordsRankingInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Keywords_Ranking";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<KeywordsRankingInfo>();
                    while (RdrList.Read())
                    {
                        entity = new KeywordsRankingInfo();
                        entity.ID = Tools.NullInt(RdrList["ID"]);
                        entity.Type = Tools.NullInt(RdrList["Type"]);
                        entity.Keyword = Tools.NullStr(RdrList["Keyword"]);
                        entity.addtime = Tools.NullDate(RdrList["addtime"]);
                        entity.Site = Tools.NullStr(RdrList["Site"]);

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
                SqlTable = "Keywords_Ranking";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(ID) FROM " + SqlTable + SqlParam;

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
