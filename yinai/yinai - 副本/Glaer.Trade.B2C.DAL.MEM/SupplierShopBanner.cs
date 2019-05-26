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
    public class SupplierShopBanner : ISupplierShopBanner
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public SupplierShopBanner()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddSupplierShopBanner(SupplierShopBannerInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Supplier_Shop_Banner";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Shop_Banner_ID"] = entity.Shop_Banner_ID;
            DrAdd["Shop_Banner_Type"] = entity.Shop_Banner_Type;
            DrAdd["Shop_Banner_Name"] = entity.Shop_Banner_Name;
            DrAdd["Shop_Banner_Url"] = entity.Shop_Banner_Url;
            DrAdd["Shop_Banner_IsActive"] = entity.Shop_Banner_IsActive;
            DrAdd["Shop_Banner_Site"] = entity.Shop_Banner_Site;

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

        public virtual bool EditSupplierShopBanner(SupplierShopBannerInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Supplier_Shop_Banner WHERE Shop_Banner_ID = " + entity.Shop_Banner_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Shop_Banner_ID"] = entity.Shop_Banner_ID;
                    DrAdd["Shop_Banner_Type"] = entity.Shop_Banner_Type;
                    DrAdd["Shop_Banner_Name"] = entity.Shop_Banner_Name;
                    DrAdd["Shop_Banner_Url"] = entity.Shop_Banner_Url;
                    DrAdd["Shop_Banner_IsActive"] = entity.Shop_Banner_IsActive;
                    DrAdd["Shop_Banner_Site"] = entity.Shop_Banner_Site;

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

        public virtual int DelSupplierShopBanner(int ID)
        {
            string SqlAdd = "DELETE FROM Supplier_Shop_Banner WHERE Shop_Banner_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual SupplierShopBannerInfo GetSupplierShopBannerByID(int ID)
        {
            SupplierShopBannerInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Supplier_Shop_Banner WHERE Shop_Banner_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SupplierShopBannerInfo();

                    entity.Shop_Banner_ID = Tools.NullInt(RdrList["Shop_Banner_ID"]);
                    entity.Shop_Banner_Type = Tools.NullInt(RdrList["Shop_Banner_Type"]);
                    entity.Shop_Banner_Name = Tools.NullStr(RdrList["Shop_Banner_Name"]);
                    entity.Shop_Banner_Url = Tools.NullStr(RdrList["Shop_Banner_Url"]);
                    entity.Shop_Banner_IsActive = Tools.NullInt(RdrList["Shop_Banner_IsActive"]);
                    entity.Shop_Banner_Site = Tools.NullStr(RdrList["Shop_Banner_Site"]);

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

        public virtual IList<SupplierShopBannerInfo> GetSupplierShopBanners(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<SupplierShopBannerInfo> entitys = null;
            SupplierShopBannerInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Supplier_Shop_Banner";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SupplierShopBannerInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SupplierShopBannerInfo();
                        entity.Shop_Banner_ID = Tools.NullInt(RdrList["Shop_Banner_ID"]);
                        entity.Shop_Banner_Type = Tools.NullInt(RdrList["Shop_Banner_Type"]);
                        entity.Shop_Banner_Name = Tools.NullStr(RdrList["Shop_Banner_Name"]);
                        entity.Shop_Banner_Url = Tools.NullStr(RdrList["Shop_Banner_Url"]);
                        entity.Shop_Banner_IsActive = Tools.NullInt(RdrList["Shop_Banner_IsActive"]);
                        entity.Shop_Banner_Site = Tools.NullStr(RdrList["Shop_Banner_Site"]);

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
                SqlTable = "Supplier_Shop_Banner";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Shop_Banner_ID) FROM " + SqlTable + SqlParam;

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
