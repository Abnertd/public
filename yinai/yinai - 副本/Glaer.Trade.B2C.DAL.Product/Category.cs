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
    public class Category : ICategory
    {
        IEncrypt Encrypt;
        ISQLHelper SQLHelper;
        ITools Tools;

        public Category()
        {
            Encrypt = EncryptFactory.CreateEncrypt();
            SQLHelper = SQLHelperFactory.CreateSQLHelper();
            Tools = ToolsFactory.CreateTools();
        }

        public virtual bool AddCategory(CategoryInfo entity)
        {

            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Category";
            DtAdd = SQLHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();
            DrAdd["Cate_ParentID"] = entity.Cate_ParentID;
            DrAdd["Cate_Name"] = entity.Cate_Name;
            DrAdd["Cate_TypeID"] = entity.Cate_TypeID;
            DrAdd["Cate_Img"] = entity.Cate_Img;
            DrAdd["Cate_ProductTypeID"] = entity.Cate_ProductTypeID;
            DrAdd["Cate_Sort"] = entity.Cate_Sort;
            DrAdd["Cate_IsFrequently"] = entity.Cate_IsFrequently;
            DrAdd["Cate_IsActive"] = entity.Cate_IsActive;
            DrAdd["Cate_Count_Brand"] = entity.Cate_Count_Brand;
            DrAdd["Cate_Count_Product"] = entity.Cate_Count_Product;
            DrAdd["Cate_SEO_Path"] = entity.Cate_SEO_Path;
            DrAdd["Cate_SEO_Title"] = entity.Cate_SEO_Title;
            DrAdd["Cate_SEO_Keyword"] = entity.Cate_SEO_Keyword;
            DrAdd["Cate_SEO_Description"] = entity.Cate_SEO_Description;
            DrAdd["Cate_Site"] = entity.Cate_Site;

            DtAdd.Rows.Add(DrAdd);
            try
            {
                SQLHelper.SaveChanges(SqlAdd, DtAdd);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual bool EditCategory(CategoryInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Category WHERE Cate_ID = " + entity.Cate_ID;
            DtAdd = SQLHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Cate_ID"] = entity.Cate_ID;
                    DrAdd["Cate_ParentID"] = entity.Cate_ParentID;
                    DrAdd["Cate_Name"] = entity.Cate_Name;
                    DrAdd["Cate_TypeID"] = entity.Cate_TypeID;
                    DrAdd["Cate_Img"] = entity.Cate_Img;
                    DrAdd["Cate_ProductTypeID"] = entity.Cate_ProductTypeID;
                    DrAdd["Cate_Sort"] = entity.Cate_Sort;
                    DrAdd["Cate_IsFrequently"] = entity.Cate_IsFrequently;
                    DrAdd["Cate_IsActive"] = entity.Cate_IsActive;
                    DrAdd["Cate_Count_Brand"] = entity.Cate_Count_Brand;
                    DrAdd["Cate_Count_Product"] = entity.Cate_Count_Product;
                    DrAdd["Cate_SEO_Path"] = entity.Cate_SEO_Path;
                    DrAdd["Cate_SEO_Title"] = entity.Cate_SEO_Title;
                    DrAdd["Cate_SEO_Keyword"] = entity.Cate_SEO_Keyword;
                    DrAdd["Cate_SEO_Description"] = entity.Cate_SEO_Description;
                    DrAdd["Cate_Site"] = entity.Cate_Site;

                    SQLHelper.SaveChanges(SqlAdd, DtAdd);
                    return true;
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
        }

        public virtual int DelCategory(int Cate_ID)
        {
            string SqlAdd = "DELETE FROM Category WHERE Cate_ID = " + Cate_ID;
            try
            {
                return SQLHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual CategoryInfo GetCategoryByID(int Cate_ID)
        {
            CategoryInfo Entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Category WHERE Cate_ID = " + Cate_ID;
                RdrList = SQLHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    Entity = new CategoryInfo();
                    Entity.Cate_ID = Tools.NullInt(RdrList["Cate_ID"]);
                    Entity.Cate_ParentID = Tools.NullInt(RdrList["Cate_ParentID"]);
                    Entity.Cate_Name = Tools.NullStr(RdrList["Cate_Name"]);
                    Entity.Cate_TypeID = Tools.NullInt(RdrList["Cate_TypeID"]);
                    Entity.Cate_Img = Tools.NullStr(RdrList["Cate_Img"]);
                    Entity.Cate_ProductTypeID = Tools.NullInt(RdrList["Cate_ProductTypeID"]);
                    Entity.Cate_Sort = Tools.NullInt(RdrList["Cate_Sort"]);
                    Entity.Cate_IsFrequently = Tools.NullInt(RdrList["Cate_IsFrequently"]);
                    Entity.Cate_IsActive = Tools.NullInt(RdrList["Cate_IsActive"]);
                    Entity.Cate_Count_Brand = Tools.NullInt(RdrList["Cate_Count_Brand"]);
                    Entity.Cate_Count_Product = Tools.NullInt(RdrList["Cate_Count_Product"]);
                    Entity.Cate_SEO_Path = Tools.NullStr(RdrList["Cate_SEO_Path"]);
                    Entity.Cate_SEO_Title = Tools.NullStr(RdrList["Cate_SEO_Title"]);
                    Entity.Cate_SEO_Keyword = Tools.NullStr(RdrList["Cate_SEO_Keyword"]);
                    Entity.Cate_SEO_Description = Tools.NullStr(RdrList["Cate_SEO_Description"]);
                    Entity.Cate_Site = Tools.NullStr(RdrList["Cate_Site"]);
                }
                
                return Entity;
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

        public virtual IList<CategoryInfo> GetCategorys(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<CategoryInfo> entitys = null;
            CategoryInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable; 
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Category";
                SqlField = "*";
                SqlParam = SQLHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = SQLHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = SQLHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = SQLHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<CategoryInfo>();
                    while (RdrList.Read())
                    {
                        entity = new CategoryInfo();
                        entity.Cate_ID = Tools.NullInt(RdrList["Cate_ID"]);
                        entity.Cate_ParentID = Tools.NullInt(RdrList["Cate_ParentID"]);
                        entity.Cate_Name = Tools.NullStr(RdrList["Cate_Name"]);
                        entity.Cate_TypeID = Tools.NullInt(RdrList["Cate_TypeID"]);
                        entity.Cate_Img = Tools.NullStr(RdrList["Cate_Img"]);
                        entity.Cate_ProductTypeID = Tools.NullInt(RdrList["Cate_ProductTypeID"]);
                        entity.Cate_Sort = Tools.NullInt(RdrList["Cate_Sort"]);
                        entity.Cate_IsFrequently = Tools.NullInt(RdrList["Cate_IsFrequently"]);
                        entity.Cate_IsActive = Tools.NullInt(RdrList["Cate_IsActive"]);
                        entity.Cate_Count_Brand = Tools.NullInt(RdrList["Cate_Count_Brand"]);
                        entity.Cate_Count_Product = Tools.NullInt(RdrList["Cate_Count_Product"]);
                        entity.Cate_SEO_Path = Tools.NullStr(RdrList["Cate_SEO_Path"]);
                        entity.Cate_SEO_Title = Tools.NullStr(RdrList["Cate_SEO_Title"]);
                        entity.Cate_SEO_Keyword = Tools.NullStr(RdrList["Cate_SEO_Keyword"]);
                        entity.Cate_SEO_Description = Tools.NullStr(RdrList["Cate_SEO_Description"]);
                        entity.Cate_Site = Tools.NullStr(RdrList["Cate_Site"]);
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
                SqlTable = "Category";
                SqlParam = SQLHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Cate_ID) FROM " + SqlTable + SqlParam;

                RecordCount = Tools.NullInt(SQLHelper.ExecuteScalar(SqlCount));
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

        public virtual int GetSubCateCount(int Cate_ID, string SiteSign)
        {
            try {
                return Tools.NullInt(SQLHelper.ExecuteScalar("SELECT COUNT(Cate_ID) FROM Category WHERE Cate_IsActive=1 AND Cate_ParentID = " + Cate_ID + " AND Cate_Site = '" + SiteSign + "'"));
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public virtual IList<CategoryInfo> GetSubCategorys(int Cate_ID, string SiteSign)
        {
            IList<CategoryInfo> entitys = null;
            CategoryInfo entity = null;
            string SqlList;
            SqlDataReader RdrList = null;
            try
            {
                SqlList = "SELECT * FROM Category WHERE Cate_IsActive=1 AND Cate_ParentID = " + Cate_ID + " AND Cate_Site = '" + SiteSign + "' Order By Cate_Sort Asc,Cate_ID Desc";
                RdrList = SQLHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<CategoryInfo>();
                    while (RdrList.Read())
                    {
                        entity = new CategoryInfo();
                        entity.Cate_ID = Tools.NullInt(RdrList["Cate_ID"]);
                        entity.Cate_ParentID = Tools.NullInt(RdrList["Cate_ParentID"]);
                        entity.Cate_Name = Tools.NullStr(RdrList["Cate_Name"]);
                        entity.Cate_TypeID = Tools.NullInt(RdrList["Cate_TypeID"]);
                        entity.Cate_Img = Tools.NullStr(RdrList["Cate_Img"]);
                        entity.Cate_ProductTypeID = Tools.NullInt(RdrList["Cate_ProductTypeID"]);
                        entity.Cate_Sort = Tools.NullInt(RdrList["Cate_Sort"]);
                        entity.Cate_IsFrequently = Tools.NullInt(RdrList["Cate_IsFrequently"]);
                        entity.Cate_IsActive = Tools.NullInt(RdrList["Cate_IsActive"]);
                        entity.Cate_Count_Brand = Tools.NullInt(RdrList["Cate_Count_Brand"]);
                        entity.Cate_Count_Product = Tools.NullInt(RdrList["Cate_Count_Product"]);
                        entity.Cate_SEO_Path = Tools.NullStr(RdrList["Cate_SEO_Path"]);
                        entity.Cate_SEO_Title = Tools.NullStr(RdrList["Cate_SEO_Title"]);
                        entity.Cate_SEO_Keyword = Tools.NullStr(RdrList["Cate_SEO_Keyword"]);
                        entity.Cate_SEO_Description = Tools.NullStr(RdrList["Cate_SEO_Description"]);
                        entity.Cate_Site = Tools.NullStr(RdrList["Cate_Site"]);
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

        public virtual string Get_All_SubCateID(int Cate_ID)
        {
            string SqlList, Cate_Arry;
            Cate_Arry = Cate_ID.ToString();
            if (Cate_ID == 0)
            {
                return Cate_Arry;
            }
            SqlDataReader RdrList = null;
            try
            {
                SqlList = "with a as (select cate_id from category where cate_id=" + Cate_ID + " union all select category.cate_id from category,a where category.cate_parentid=a.cate_id) select * from a";
                RdrList = SQLHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    while (RdrList.Read())
                    {
                        if (Cate_ID != Tools.NullInt(RdrList["Cate_ID"]))
                        {
                            Cate_Arry += "," + Tools.NullInt(RdrList["Cate_ID"]);
                        }
                    }
                }
                return Cate_Arry;
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
    }
}
