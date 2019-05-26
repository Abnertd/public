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
    public class ProductTypeExtend : IProductTypeExtend
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public ProductTypeExtend()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddProductTypeExtend(ProductTypeExtendInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM ProductType_Extend";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["ProductType_Extend_ID"] = entity.ProductType_Extend_ID;
            DrAdd["ProductType_Extend_ProductTypeID"] = entity.ProductType_Extend_ProductTypeID;
            DrAdd["ProductType_Extend_Name"] = entity.ProductType_Extend_Name;
            DrAdd["ProductType_Extend_Display"] = entity.ProductType_Extend_Display;
            DrAdd["ProductType_Extend_IsSearch"] = entity.ProductType_Extend_IsSearch;
            DrAdd["ProductType_Extend_Options"] = entity.ProductType_Extend_Options;
            DrAdd["ProductType_Extend_Default"] = entity.ProductType_Extend_Default;
            DrAdd["ProductType_Extend_IsActive"] = entity.ProductType_Extend_IsActive;
            DrAdd["ProductType_Extend_Gather"] = entity.ProductType_Extend_Gather;
            DrAdd["ProductType_Extend_DisplayForm"] = entity.ProductType_Extend_DisplayForm;
            DrAdd["ProductType_Extend_Sort"] = entity.ProductType_Extend_Sort;
            DrAdd["ProductType_Extend_Site"] = entity.ProductType_Extend_Site;

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

        public virtual bool EditProductTypeExtend(ProductTypeExtendInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM ProductType_Extend WHERE ProductType_Extend_ID = " + entity.ProductType_Extend_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["ProductType_Extend_ID"] = entity.ProductType_Extend_ID;
                    DrAdd["ProductType_Extend_ProductTypeID"] = entity.ProductType_Extend_ProductTypeID;
                    DrAdd["ProductType_Extend_Name"] = entity.ProductType_Extend_Name;
                    DrAdd["ProductType_Extend_Display"] = entity.ProductType_Extend_Display;
                    DrAdd["ProductType_Extend_IsSearch"] = entity.ProductType_Extend_IsSearch;
                    DrAdd["ProductType_Extend_Options"] = entity.ProductType_Extend_Options;
                    DrAdd["ProductType_Extend_Default"] = entity.ProductType_Extend_Default;
                    DrAdd["ProductType_Extend_IsActive"] = entity.ProductType_Extend_IsActive;
                    DrAdd["ProductType_Extend_Gather"] = entity.ProductType_Extend_Gather;
                    DrAdd["ProductType_Extend_DisplayForm"] = entity.ProductType_Extend_DisplayForm;
                    DrAdd["ProductType_Extend_Sort"] = entity.ProductType_Extend_Sort;
                    DrAdd["ProductType_Extend_Site"] = entity.ProductType_Extend_Site;

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

        public virtual int DelProductTypeExtend(int ProductType_Extend_ID)
        {
            string SqlAdd = "DELETE FROM ProductType_Extend WHERE ProductType_Extend_ID = " + ProductType_Extend_ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual ProductTypeExtendInfo GetProductTypeExtendByID(int ProductType_Extend_ID)
        {
            ProductTypeExtendInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM ProductType_Extend WHERE ProductType_Extend_ID = " + ProductType_Extend_ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new ProductTypeExtendInfo();

                    entity.ProductType_Extend_ID = Tools.NullInt(RdrList["ProductType_Extend_ID"]);
                    entity.ProductType_Extend_ProductTypeID = Tools.NullInt(RdrList["ProductType_Extend_ProductTypeID"]);
                    entity.ProductType_Extend_Name = Tools.NullStr(RdrList["ProductType_Extend_Name"]);
                    entity.ProductType_Extend_Display = Tools.NullStr(RdrList["ProductType_Extend_Display"]);
                    entity.ProductType_Extend_IsSearch = Tools.NullInt(RdrList["ProductType_Extend_IsSearch"]);
                    entity.ProductType_Extend_Options = Tools.NullInt(RdrList["ProductType_Extend_Options"]);
                    entity.ProductType_Extend_Default = Tools.NullStr(RdrList["ProductType_Extend_Default"]);
                    entity.ProductType_Extend_IsActive = Tools.NullInt(RdrList["ProductType_Extend_IsActive"]);
                    entity.ProductType_Extend_Gather = Tools.NullInt(RdrList["ProductType_Extend_Gather"]);
                    entity.ProductType_Extend_DisplayForm = Tools.NullInt(RdrList["ProductType_Extend_DisplayForm"]);
                    entity.ProductType_Extend_Sort = Tools.NullInt(RdrList["ProductType_Extend_Sort"]);
                    entity.ProductType_Extend_Site = Tools.NullStr(RdrList["ProductType_Extend_Site"]);

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

        public virtual IList<ProductTypeExtendInfo> GetProductTypeExtends(int ProductType_ID)
        {
            IList<ProductTypeExtendInfo> entitys = null;
            ProductTypeExtendInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "select * from ProductType_Extend where ProductType_Extend_ProductTypeID=" + ProductType_ID + " order by ProductType_Extend_Sort";
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<ProductTypeExtendInfo>();
                    while (RdrList.Read())
                    {
                        entity = new ProductTypeExtendInfo();
                        entity.ProductType_Extend_ID = Tools.NullInt(RdrList["ProductType_Extend_ID"]);
                        entity.ProductType_Extend_ProductTypeID = Tools.NullInt(RdrList["ProductType_Extend_ProductTypeID"]);
                        entity.ProductType_Extend_Name = Tools.NullStr(RdrList["ProductType_Extend_Name"]);
                        entity.ProductType_Extend_Display = Tools.NullStr(RdrList["ProductType_Extend_Display"]);
                        entity.ProductType_Extend_IsSearch = Tools.NullInt(RdrList["ProductType_Extend_IsSearch"]);
                        entity.ProductType_Extend_Options = Tools.NullInt(RdrList["ProductType_Extend_Options"]);
                        entity.ProductType_Extend_Default = Tools.NullStr(RdrList["ProductType_Extend_Default"]);
                        entity.ProductType_Extend_IsActive = Tools.NullInt(RdrList["ProductType_Extend_IsActive"]);
                        entity.ProductType_Extend_Gather = Tools.NullInt(RdrList["ProductType_Extend_Gather"]);
                        entity.ProductType_Extend_DisplayForm = Tools.NullInt(RdrList["ProductType_Extend_DisplayForm"]);
                        entity.ProductType_Extend_Sort = Tools.NullInt(RdrList["ProductType_Extend_Sort"]);
                        entity.ProductType_Extend_Site = Tools.NullStr(RdrList["ProductType_Extend_Site"]);

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
                SqlTable = "ProductType_Extend";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(ProductType_Extend_ID) FROM " + SqlTable + SqlParam;

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
