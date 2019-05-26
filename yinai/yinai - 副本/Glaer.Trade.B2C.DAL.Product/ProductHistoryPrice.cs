using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.Util.Tools;

namespace Glaer.Trade.B2C.DAL.Product
{
    public class ProductHistoryPrice : IProductHistoryPrice
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public ProductHistoryPrice()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddProductHistoryPrice(ProductHistoryPriceInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Product_HistoryPrice";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["History_ID"] = entity.History_ID;
            DrAdd["History_SysName"] = entity.History_SysName;
            DrAdd["History_ProductID"] = entity.History_ProductID;
            DrAdd["History_PriceType"] = entity.History_PriceType;
            DrAdd["History_Price_Original"] = entity.History_Price_Original;
            DrAdd["History_Price_New"] = entity.History_Price_New;
            DrAdd["History_Addtime"] = entity.History_Addtime;

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

        public virtual bool EditProductHistoryPrice(ProductHistoryPriceInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Product_HistoryPrice WHERE History_ID = " + entity.History_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["History_ID"] = entity.History_ID;
                    DrAdd["History_SysName"] = entity.History_SysName;
                    DrAdd["History_ProductID"] = entity.History_ProductID;
                    DrAdd["History_PriceType"] = entity.History_PriceType;
                    DrAdd["History_Price_Original"] = entity.History_Price_Original;
                    DrAdd["History_Price_New"] = entity.History_Price_New;
                    DrAdd["History_Addtime"] = entity.History_Addtime;

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

        public virtual int DelProductHistoryPrice(int ID)
        {
            string SqlAdd = "DELETE FROM Product_HistoryPrice WHERE History_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual ProductHistoryPriceInfo GetProductHistoryPriceByID(int ID)
        {
            ProductHistoryPriceInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Product_HistoryPrice WHERE History_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new ProductHistoryPriceInfo();

                    entity.History_ID = Tools.NullInt(RdrList["History_ID"]);
                    entity.History_SysName = Tools.NullStr(RdrList["History_SysName"]);
                    entity.History_ProductID = Tools.NullInt(RdrList["History_ProductID"]);
                    entity.History_PriceType = Tools.NullStr(RdrList["History_PriceType"]);
                    entity.History_Price_Original = Tools.NullDbl(RdrList["History_Price_Original"]);
                    entity.History_Price_New = Tools.NullDbl(RdrList["History_Price_New"]);
                    entity.History_Addtime = Tools.NullDate(RdrList["History_Addtime"]);

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

        public virtual IList<ProductHistoryPriceInfo> GetProductHistoryPrices(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<ProductHistoryPriceInfo> entitys = null;
            ProductHistoryPriceInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Product_HistoryPrice";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<ProductHistoryPriceInfo>();
                    while (RdrList.Read())
                    {
                        entity = new ProductHistoryPriceInfo();
                        entity.History_ID = Tools.NullInt(RdrList["History_ID"]);
                        entity.History_SysName = Tools.NullStr(RdrList["History_SysName"]);
                        entity.History_ProductID = Tools.NullInt(RdrList["History_ProductID"]);
                        entity.History_PriceType = Tools.NullStr(RdrList["History_PriceType"]);
                        entity.History_Price_Original = Tools.NullDbl(RdrList["History_Price_Original"]);
                        entity.History_Price_New = Tools.NullDbl(RdrList["History_Price_New"]);
                        entity.History_Addtime = Tools.NullDate(RdrList["History_Addtime"]);

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
                SqlTable = "Product_HistoryPrice";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(History_ID) FROM " + SqlTable + SqlParam;

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
