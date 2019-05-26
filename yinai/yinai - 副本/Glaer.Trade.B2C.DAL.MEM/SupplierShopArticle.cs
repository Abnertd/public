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
    public class SupplierShopArticle : ISupplierShopArticle
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public SupplierShopArticle()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddSupplierShopArticle(SupplierShopArticleInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Supplier_Shop_Article";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Shop_Article_ID"] = entity.Shop_Article_ID;
            DrAdd["Shop_Article_SupplierID"] = entity.Shop_Article_SupplierID;
            DrAdd["Shop_Article_Title"] = entity.Shop_Article_Title;
            DrAdd["Shop_Article_Content"] = entity.Shop_Article_Content;
            DrAdd["Shop_Article_Hits"] = entity.Shop_Article_Hits;
            DrAdd["Shop_Article_Addtime"] = entity.Shop_Article_Addtime;
            DrAdd["Shop_Article_IsActive"] = entity.Shop_Article_IsActive;
            DrAdd["Shop_Article_Site"] = entity.Shop_Article_Site;

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

        public virtual bool EditSupplierShopArticle(SupplierShopArticleInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Supplier_Shop_Article WHERE Shop_Article_ID = " + entity.Shop_Article_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Shop_Article_ID"] = entity.Shop_Article_ID;
                    DrAdd["Shop_Article_SupplierID"] = entity.Shop_Article_SupplierID;
                    DrAdd["Shop_Article_Title"] = entity.Shop_Article_Title;
                    DrAdd["Shop_Article_Content"] = entity.Shop_Article_Content;
                    DrAdd["Shop_Article_Hits"] = entity.Shop_Article_Hits;
                    DrAdd["Shop_Article_Addtime"] = entity.Shop_Article_Addtime;
                    DrAdd["Shop_Article_IsActive"] = entity.Shop_Article_IsActive;
                    DrAdd["Shop_Article_Site"] = entity.Shop_Article_Site;

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

        public virtual int DelSupplierShopArticle(int ID)
        {
            string SqlAdd = "DELETE FROM Supplier_Shop_Article WHERE Shop_Article_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual SupplierShopArticleInfo GetSupplierShopArticleByID(int ID)
        {
            SupplierShopArticleInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Supplier_Shop_Article WHERE Shop_Article_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SupplierShopArticleInfo();

                    entity.Shop_Article_ID = Tools.NullInt(RdrList["Shop_Article_ID"]);
                    entity.Shop_Article_SupplierID = Tools.NullInt(RdrList["Shop_Article_SupplierID"]);
                    entity.Shop_Article_Title = Tools.NullStr(RdrList["Shop_Article_Title"]);
                    entity.Shop_Article_Content = Tools.NullStr(RdrList["Shop_Article_Content"]);
                    entity.Shop_Article_Hits = Tools.NullInt(RdrList["Shop_Article_Hits"]);
                    entity.Shop_Article_Addtime = Tools.NullDate(RdrList["Shop_Article_Addtime"]);
                    entity.Shop_Article_IsActive = Tools.NullInt(RdrList["Shop_Article_IsActive"]);
                    entity.Shop_Article_Site = Tools.NullStr(RdrList["Shop_Article_Site"]);

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

        public virtual SupplierShopArticleInfo GetSupplierShopArticleByIDSupplier(int ID, int Supplier_ID)
        {
            SupplierShopArticleInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Supplier_Shop_Article WHERE Shop_Article_SupplierID=" + Supplier_ID + " And Shop_Article_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SupplierShopArticleInfo();

                    entity.Shop_Article_ID = Tools.NullInt(RdrList["Shop_Article_ID"]);
                    entity.Shop_Article_SupplierID = Tools.NullInt(RdrList["Shop_Article_SupplierID"]);
                    entity.Shop_Article_Title = Tools.NullStr(RdrList["Shop_Article_Title"]);
                    entity.Shop_Article_Content = Tools.NullStr(RdrList["Shop_Article_Content"]);
                    entity.Shop_Article_Hits = Tools.NullInt(RdrList["Shop_Article_Hits"]);
                    entity.Shop_Article_Addtime = Tools.NullDate(RdrList["Shop_Article_Addtime"]);
                    entity.Shop_Article_IsActive = Tools.NullInt(RdrList["Shop_Article_IsActive"]);
                    entity.Shop_Article_Site = Tools.NullStr(RdrList["Shop_Article_Site"]);

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

        public virtual IList<SupplierShopArticleInfo> GetSupplierShopArticles(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<SupplierShopArticleInfo> entitys = null;
            SupplierShopArticleInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Supplier_Shop_Article";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SupplierShopArticleInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SupplierShopArticleInfo();
                        entity.Shop_Article_ID = Tools.NullInt(RdrList["Shop_Article_ID"]);
                        entity.Shop_Article_SupplierID = Tools.NullInt(RdrList["Shop_Article_SupplierID"]);
                        entity.Shop_Article_Title = Tools.NullStr(RdrList["Shop_Article_Title"]);
                        entity.Shop_Article_Content = Tools.NullStr(RdrList["Shop_Article_Content"]);
                        entity.Shop_Article_Hits = Tools.NullInt(RdrList["Shop_Article_Hits"]);
                        entity.Shop_Article_Addtime = Tools.NullDate(RdrList["Shop_Article_Addtime"]);
                        entity.Shop_Article_IsActive = Tools.NullInt(RdrList["Shop_Article_IsActive"]);
                        entity.Shop_Article_Site = Tools.NullStr(RdrList["Shop_Article_Site"]);

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
                SqlTable = "Supplier_Shop_Article";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Shop_Article_ID) FROM " + SqlTable + SqlParam;

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
