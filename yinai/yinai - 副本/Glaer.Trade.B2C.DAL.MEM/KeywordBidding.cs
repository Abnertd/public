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
    public class KeywordBidding : IKeywordBidding
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public KeywordBidding()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddKeywordBidding(KeywordBiddingInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM KeywordBidding";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["KeywordBidding_ID"] = entity.KeywordBidding_ID;
            DrAdd["KeywordBidding_SupplierID"] = entity.KeywordBidding_SupplierID;
            DrAdd["KeywordBidding_ProductID"] = entity.KeywordBidding_ProductID;
            DrAdd["KeywordBidding_KeywordID"] = entity.KeywordBidding_KeywordID;
            DrAdd["KeywordBidding_Price"] = entity.KeywordBidding_Price;
            DrAdd["KeywordBidding_StartDate"] = entity.KeywordBidding_StartDate;
            DrAdd["KeywordBidding_EndDate"] = entity.KeywordBidding_EndDate;
            DrAdd["KeywordBidding_ShowTimes"] = entity.KeywordBidding_ShowTimes;
            DrAdd["KeywordBidding_Hits"] = entity.KeywordBidding_Hits;
            DrAdd["KeywordBidding_Audit"] = entity.KeywordBidding_Audit;
            DrAdd["KeywordBidding_Site"] = entity.KeywordBidding_Site;

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

        public virtual bool EditKeywordBidding(KeywordBiddingInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM KeywordBidding WHERE KeywordBidding_ID = " + entity.KeywordBidding_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["KeywordBidding_ID"] = entity.KeywordBidding_ID;
                    DrAdd["KeywordBidding_SupplierID"] = entity.KeywordBidding_SupplierID;
                    DrAdd["KeywordBidding_ProductID"] = entity.KeywordBidding_ProductID;
                    DrAdd["KeywordBidding_KeywordID"] = entity.KeywordBidding_KeywordID;
                    DrAdd["KeywordBidding_Price"] = entity.KeywordBidding_Price;
                    DrAdd["KeywordBidding_StartDate"] = entity.KeywordBidding_StartDate;
                    DrAdd["KeywordBidding_EndDate"] = entity.KeywordBidding_EndDate;
                    DrAdd["KeywordBidding_ShowTimes"] = entity.KeywordBidding_ShowTimes;
                    DrAdd["KeywordBidding_Hits"] = entity.KeywordBidding_Hits;
                    DrAdd["KeywordBidding_Audit"] = entity.KeywordBidding_Audit;
                    DrAdd["KeywordBidding_Site"] = entity.KeywordBidding_Site;

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

        public virtual int DelKeywordBidding(int Supplier_ID,int ID)
        {
            string SqlAdd = "DELETE FROM KeywordBidding WHERE KeywordBidding_ID = " + ID;
            if (Supplier_ID > 0)
            {
                SqlAdd += " and KeywordBidding_SupplierID=" + Supplier_ID;
            }
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual KeywordBiddingInfo GetKeywordBiddingByID(int ID)
        {
            KeywordBiddingInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM KeywordBidding WHERE KeywordBidding_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new KeywordBiddingInfo();

                    entity.KeywordBidding_ID = Tools.NullInt(RdrList["KeywordBidding_ID"]);
                    entity.KeywordBidding_SupplierID = Tools.NullInt(RdrList["KeywordBidding_SupplierID"]);
                    entity.KeywordBidding_ProductID = Tools.NullInt(RdrList["KeywordBidding_ProductID"]);
                    entity.KeywordBidding_KeywordID = Tools.NullInt(RdrList["KeywordBidding_KeywordID"]);
                    entity.KeywordBidding_Price = Tools.NullDbl(RdrList["KeywordBidding_Price"]);
                    entity.KeywordBidding_StartDate = Tools.NullDate(RdrList["KeywordBidding_StartDate"]);
                    entity.KeywordBidding_EndDate = Tools.NullDate(RdrList["KeywordBidding_EndDate"]);
                    entity.KeywordBidding_ShowTimes = Tools.NullInt(RdrList["KeywordBidding_ShowTimes"]);
                    entity.KeywordBidding_Hits = Tools.NullInt(RdrList["KeywordBidding_Hits"]);
                    entity.KeywordBidding_Audit = Tools.NullInt(RdrList["KeywordBidding_Audit"]);
                    entity.KeywordBidding_Site = Tools.NullStr(RdrList["KeywordBidding_Site"]);

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

        public virtual IList<KeywordBiddingInfo> GetKeywordBiddings(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<KeywordBiddingInfo> entitys = null;
            KeywordBiddingInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "KeywordBidding";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<KeywordBiddingInfo>();
                    while (RdrList.Read())
                    {
                        entity = new KeywordBiddingInfo();
                        entity.KeywordBidding_ID = Tools.NullInt(RdrList["KeywordBidding_ID"]);
                        entity.KeywordBidding_SupplierID = Tools.NullInt(RdrList["KeywordBidding_SupplierID"]);
                        entity.KeywordBidding_ProductID = Tools.NullInt(RdrList["KeywordBidding_ProductID"]);
                        entity.KeywordBidding_KeywordID = Tools.NullInt(RdrList["KeywordBidding_KeywordID"]);
                        entity.KeywordBidding_Price = Tools.NullDbl(RdrList["KeywordBidding_Price"]);
                        entity.KeywordBidding_StartDate = Tools.NullDate(RdrList["KeywordBidding_StartDate"]);
                        entity.KeywordBidding_EndDate = Tools.NullDate(RdrList["KeywordBidding_EndDate"]);
                        entity.KeywordBidding_ShowTimes = Tools.NullInt(RdrList["KeywordBidding_ShowTimes"]);
                        entity.KeywordBidding_Hits = Tools.NullInt(RdrList["KeywordBidding_Hits"]);
                        entity.KeywordBidding_Audit = Tools.NullInt(RdrList["KeywordBidding_Audit"]);
                        entity.KeywordBidding_Site = Tools.NullStr(RdrList["KeywordBidding_Site"]);

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
                SqlTable = "KeywordBidding";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(KeywordBidding_ID) FROM " + SqlTable + SqlParam;

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

        public virtual bool AddKeywordBiddingKeyword (KeywordBiddingKeywordInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM KeywordBidding_Keyword";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();
            
            DrAdd["Keyword_ID"] = entity.Keyword_ID;
DrAdd["Keyword_Name"] = entity.Keyword_Name;
DrAdd["Keyword_MinPrice"] = entity.Keyword_MinPrice;

            DtAdd.Rows.Add(DrAdd);
            try {
                DBHelper.SaveChanges(SqlAdd, DtAdd);
                return true;
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                DtAdd.Dispose();
            }
        }

        public virtual bool EditKeywordBiddingKeyword (KeywordBiddingKeywordInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM KeywordBidding_Keyword WHERE Keyword_ID = " + entity.Keyword_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try {
                if (DtAdd.Rows.Count > 0) {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Keyword_ID"] = entity.Keyword_ID;
                    DrAdd["Keyword_Name"] = entity.Keyword_Name;
                    DrAdd["Keyword_MinPrice"] = entity.Keyword_MinPrice;

                    DBHelper.SaveChanges(SqlAdd, DtAdd);
                }
                else  {
                    return false;
                }
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                DtAdd.Dispose();
            }
            return true;
        
        }

        public virtual int DelKeywordBiddingKeyword (int ID) {
            string SqlAdd = "DELETE FROM KeywordBidding_Keyword WHERE Keyword_ID = " + ID;
            try {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        
       public virtual KeywordBiddingKeywordInfo GetKeywordBiddingKeywordByID(int ID)
        {
            KeywordBiddingKeywordInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM KeywordBidding_Keyword WHERE Keyword_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new KeywordBiddingKeywordInfo();
                    
                     entity.Keyword_ID = Tools.NullInt(RdrList["Keyword_ID"]);
 entity.Keyword_Name = Tools.NullStr(RdrList["Keyword_Name"]);
 entity.Keyword_MinPrice = Tools.NullDbl(RdrList["Keyword_MinPrice"]);

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

       public virtual KeywordBiddingKeywordInfo GetKeywordBiddingKeywordByName(string Keyword)
       {
           KeywordBiddingKeywordInfo entity = null;
           SqlDataReader RdrList = null;
           try
           {
               string SqlList;
               SqlList = "SELECT * FROM KeywordBidding_Keyword WHERE Keyword_Name = '" + Keyword+"'";
               RdrList = DBHelper.ExecuteReader(SqlList);
               if (RdrList.Read())
               {
                   entity = new KeywordBiddingKeywordInfo();

                   entity.Keyword_ID = Tools.NullInt(RdrList["Keyword_ID"]);
                   entity.Keyword_Name = Tools.NullStr(RdrList["Keyword_Name"]);
                   entity.Keyword_MinPrice = Tools.NullDbl(RdrList["Keyword_MinPrice"]);

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
        
        public virtual IList<KeywordBiddingKeywordInfo> GetKeywordBiddingKeywords(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<KeywordBiddingKeywordInfo> entitys = null;
            KeywordBiddingKeywordInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable; 
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "KeywordBidding_Keyword";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<KeywordBiddingKeywordInfo>();
                    while (RdrList.Read())
                    {
                       entity = new KeywordBiddingKeywordInfo();
                         entity.Keyword_ID = Tools.NullInt(RdrList["Keyword_ID"]);
 entity.Keyword_Name = Tools.NullStr(RdrList["Keyword_Name"]);
 entity.Keyword_MinPrice = Tools.NullDbl(RdrList["Keyword_MinPrice"]);

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

        public virtual PageInfo GetKeywordPageInfo(QueryInfo Query)
        {
            int RecordCount, PageCount, CurrentPage;
            string SqlCount, SqlParam, SqlTable;
            PageInfo Page;

            try
            {
                Page = new PageInfo();
                SqlTable = "KeywordBidding_Keyword";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Keyword_ID) FROM " + SqlTable + SqlParam;

                RecordCount = Tools.NullInt(DBHelper.ExecuteScalar(SqlCount));
                PageCount = Tools.CalculatePages(RecordCount, Query.PageSize);
                CurrentPage = Tools.DeterminePage(Query.CurrentPage, PageCount);

                Page.RecordCount = RecordCount;
                Page.PageCount = PageCount;
                Page.CurrentPage = CurrentPage;
                Page.PageSize= Query.PageSize;

                return Page;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
    }



}
