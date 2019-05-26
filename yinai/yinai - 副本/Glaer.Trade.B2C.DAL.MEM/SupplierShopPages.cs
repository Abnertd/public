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
    public class SupplierShopPages : ISupplierShopPages
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public SupplierShopPages()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddSupplierShopPages(SupplierShopPagesInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Supplier_Shop_Pages";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Shop_Pages_ID"] = entity.Shop_Pages_ID;
            DrAdd["Shop_Pages_Title"] = entity.Shop_Pages_Title;
            DrAdd["Shop_Pages_SupplierID"] = entity.Shop_Pages_SupplierID;
            DrAdd["Shop_Pages_Sign"] = entity.Shop_Pages_Sign;
            DrAdd["Shop_Pages_Content"] = entity.Shop_Pages_Content;
            DrAdd["Shop_Pages_Ischeck"] = entity.Shop_Pages_Ischeck;
            DrAdd["Shop_Pages_IsActive"] = entity.Shop_Pages_IsActive;
            DrAdd["Shop_Pages_Sort"] = entity.Shop_Pages_Sort;
            DrAdd["Shop_Pages_Addtime"] = entity.Shop_Pages_Addtime;
            DrAdd["Shop_Pages_Site"] = entity.Shop_Pages_Site;

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

        public virtual bool EditSupplierShopPages(SupplierShopPagesInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Supplier_Shop_Pages WHERE Shop_Pages_ID = " + entity.Shop_Pages_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Shop_Pages_ID"] = entity.Shop_Pages_ID;
                    DrAdd["Shop_Pages_Title"] = entity.Shop_Pages_Title;
                    DrAdd["Shop_Pages_SupplierID"] = entity.Shop_Pages_SupplierID;
                    DrAdd["Shop_Pages_Sign"] = entity.Shop_Pages_Sign;
                    DrAdd["Shop_Pages_Content"] = entity.Shop_Pages_Content;
                    DrAdd["Shop_Pages_Ischeck"] = entity.Shop_Pages_Ischeck;
                    DrAdd["Shop_Pages_IsActive"] = entity.Shop_Pages_IsActive;
                    DrAdd["Shop_Pages_Sort"] = entity.Shop_Pages_Sort;
                    DrAdd["Shop_Pages_Addtime"] = entity.Shop_Pages_Addtime;
                    DrAdd["Shop_Pages_Site"] = entity.Shop_Pages_Site;

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

        public virtual int DelSupplierShopPages(int ID)
        {
            string SqlAdd = "DELETE FROM Supplier_Shop_Pages WHERE Shop_Pages_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual SupplierShopPagesInfo GetSupplierShopPagesByID(int ID)
        {
            SupplierShopPagesInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Supplier_Shop_Pages WHERE Shop_Pages_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SupplierShopPagesInfo();

                    entity.Shop_Pages_ID = Tools.NullInt(RdrList["Shop_Pages_ID"]);
                    entity.Shop_Pages_Title = Tools.NullStr(RdrList["Shop_Pages_Title"]);
                    entity.Shop_Pages_SupplierID = Tools.NullInt(RdrList["Shop_Pages_SupplierID"]);
                    entity.Shop_Pages_Sign = Tools.NullStr(RdrList["Shop_Pages_Sign"]);
                    entity.Shop_Pages_Content = Tools.NullStr(RdrList["Shop_Pages_Content"]);
                    entity.Shop_Pages_Ischeck = Tools.NullInt(RdrList["Shop_Pages_Ischeck"]);
                    entity.Shop_Pages_IsActive = Tools.NullInt(RdrList["Shop_Pages_IsActive"]);
                    entity.Shop_Pages_Sort = Tools.NullInt(RdrList["Shop_Pages_Sort"]);
                    entity.Shop_Pages_Addtime = Tools.NullDate(RdrList["Shop_Pages_Addtime"]);
                    entity.Shop_Pages_Site = Tools.NullStr(RdrList["Shop_Pages_Site"]);

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

        public virtual SupplierShopPagesInfo GetSupplierShopPagesByIDSign(string Sign,int Supplier_ID)
        {
            SupplierShopPagesInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Supplier_Shop_Pages WHERE Shop_Pages_Sign='" + Sign + "' And Shop_Pages_SupplierID = " + Supplier_ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SupplierShopPagesInfo();

                    entity.Shop_Pages_ID = Tools.NullInt(RdrList["Shop_Pages_ID"]);
                    entity.Shop_Pages_Title = Tools.NullStr(RdrList["Shop_Pages_Title"]);
                    entity.Shop_Pages_SupplierID = Tools.NullInt(RdrList["Shop_Pages_SupplierID"]);
                    entity.Shop_Pages_Sign = Tools.NullStr(RdrList["Shop_Pages_Sign"]);
                    entity.Shop_Pages_Content = Tools.NullStr(RdrList["Shop_Pages_Content"]);
                    entity.Shop_Pages_Ischeck = Tools.NullInt(RdrList["Shop_Pages_Ischeck"]);
                    entity.Shop_Pages_IsActive = Tools.NullInt(RdrList["Shop_Pages_IsActive"]);
                    entity.Shop_Pages_Sort = Tools.NullInt(RdrList["Shop_Pages_Sort"]);
                    entity.Shop_Pages_Addtime = Tools.NullDate(RdrList["Shop_Pages_Addtime"]);
                    entity.Shop_Pages_Site = Tools.NullStr(RdrList["Shop_Pages_Site"]);

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

        public virtual IList<SupplierShopPagesInfo> GetSupplierShopPagess(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<SupplierShopPagesInfo> entitys = null;
            SupplierShopPagesInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Supplier_Shop_Pages";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SupplierShopPagesInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SupplierShopPagesInfo();
                        entity.Shop_Pages_ID = Tools.NullInt(RdrList["Shop_Pages_ID"]);
                        entity.Shop_Pages_Title = Tools.NullStr(RdrList["Shop_Pages_Title"]);
                        entity.Shop_Pages_SupplierID = Tools.NullInt(RdrList["Shop_Pages_SupplierID"]);
                        entity.Shop_Pages_Sign = Tools.NullStr(RdrList["Shop_Pages_Sign"]);
                        entity.Shop_Pages_Content = Tools.NullStr(RdrList["Shop_Pages_Content"]);
                        entity.Shop_Pages_Ischeck = Tools.NullInt(RdrList["Shop_Pages_Ischeck"]);
                        entity.Shop_Pages_IsActive = Tools.NullInt(RdrList["Shop_Pages_IsActive"]);
                        entity.Shop_Pages_Sort = Tools.NullInt(RdrList["Shop_Pages_Sort"]);
                        entity.Shop_Pages_Addtime = Tools.NullDate(RdrList["Shop_Pages_Addtime"]);
                        entity.Shop_Pages_Site = Tools.NullStr(RdrList["Shop_Pages_Site"]);

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
                SqlTable = "Supplier_Shop_Pages";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Shop_Pages_ID) FROM " + SqlTable + SqlParam;

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
