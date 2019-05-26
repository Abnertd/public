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

namespace Glaer.Trade.B2C.DAL.MEM
{
    public class SupplierShop : ISupplierShop
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public SupplierShop()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddSupplierShop(SupplierShopInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Supplier_Shop";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Shop_ID"] = entity.Shop_ID;
            DrAdd["Shop_Code"] = entity.Shop_Code;
            DrAdd["Shop_Type"] = entity.Shop_Type;
            DrAdd["Shop_Name"] = entity.Shop_Name;
            DrAdd["Shop_SupplierID"] = entity.Shop_SupplierID;
            DrAdd["Shop_Img"] = entity.Shop_Img;
            DrAdd["Shop_Css"] = entity.Shop_Css;
            DrAdd["Shop_Banner"] = entity.Shop_Banner;
            DrAdd["Shop_Banner_Title"] = entity.Shop_Banner_Title;
            DrAdd["Shop_Banner_Title_Family"] = entity.Shop_Banner_Title_Family;
            DrAdd["Shop_Banner_Title_Size"] = entity.Shop_Banner_Title_Size;
            DrAdd["Shop_Banner_Title_LeftPadding"] = entity.Shop_Banner_Title_LeftPadding;
            DrAdd["Shop_banner_Title_color"] = entity.Shop_banner_Title_color;
            DrAdd["Shop_Banner_Img"] = entity.Shop_Banner_Img;
            DrAdd["Shop_Domain"] = entity.Shop_Domain;
            DrAdd["Shop_MainProduct"] = entity.Shop_MainProduct;
            DrAdd["Shop_SEO_Title"] = entity.Shop_SEO_Title;
            DrAdd["Shop_SEO_Keyword"] = entity.Shop_SEO_Keyword;
            DrAdd["Shop_SEO_Description"] = entity.Shop_SEO_Description;
            DrAdd["Shop_Addtime"] = entity.Shop_Addtime;
            DrAdd["Shop_Evaluate"] = entity.Shop_Evaluate;
            DrAdd["Shop_Recommend"] = entity.Shop_Recommend;
            DrAdd["Shop_Status"] = entity.Shop_Status;
            DrAdd["Shop_Hits"] = entity.Shop_Hits;
            DrAdd["Shop_Site"] = entity.Shop_Site;
            DrAdd["Shop_Banner_IsActive"] = entity.Shop_Banner_IsActive;
            DrAdd["Shop_Top_IsActive"] = entity.Shop_Top_IsActive;
            DrAdd["Shop_TopNav_IsActive"] = entity.Shop_TopNav_IsActive;
            DrAdd["Shop_Info_IsActive"] = entity.Shop_Info_IsActive;
            DrAdd["Shop_LeftSearch_IsActive"] = entity.Shop_LeftSearch_IsActive;
            DrAdd["Shop_LeftCate_IsActive"] = entity.Shop_LeftCate_IsActive;
            DrAdd["Shop_LeftSale_IsActive"] = entity.Shop_LeftSale_IsActive;
            DrAdd["Shop_Left_IsActive"] = entity.Shop_Left_IsActive;
            DrAdd["Shop_Right_IsActive"] = entity.Shop_Right_IsActive;
            DrAdd["Shop_RightProduct_IsActive"] = entity.Shop_RightProduct_IsActive;

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

        public virtual bool EditSupplierShop(SupplierShopInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Supplier_Shop WHERE Shop_ID = " + entity.Shop_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Shop_ID"] = entity.Shop_ID;
                    DrAdd["Shop_Code"] = entity.Shop_Code;
                    DrAdd["Shop_Type"] = entity.Shop_Type;
                    DrAdd["Shop_Name"] = entity.Shop_Name;
                    DrAdd["Shop_SupplierID"] = entity.Shop_SupplierID;
                    DrAdd["Shop_Img"] = entity.Shop_Img;
                    DrAdd["Shop_Css"] = entity.Shop_Css;
                    DrAdd["Shop_Banner"] = entity.Shop_Banner;
                    DrAdd["Shop_Banner_Title"] = entity.Shop_Banner_Title;
                    DrAdd["Shop_Banner_Title_Family"] = entity.Shop_Banner_Title_Family;
                    DrAdd["Shop_Banner_Title_Size"] = entity.Shop_Banner_Title_Size;
                    DrAdd["Shop_Banner_Title_LeftPadding"] = entity.Shop_Banner_Title_LeftPadding;
                    DrAdd["Shop_banner_Title_color"] = entity.Shop_banner_Title_color;
                    DrAdd["Shop_Banner_Img"] = entity.Shop_Banner_Img;
                    DrAdd["Shop_Domain"] = entity.Shop_Domain;
                    DrAdd["Shop_MainProduct"] = entity.Shop_MainProduct;
                    DrAdd["Shop_SEO_Title"] = entity.Shop_SEO_Title;
                    DrAdd["Shop_SEO_Keyword"] = entity.Shop_SEO_Keyword;
                    DrAdd["Shop_SEO_Description"] = entity.Shop_SEO_Description;
                    DrAdd["Shop_Addtime"] = entity.Shop_Addtime;
                    DrAdd["Shop_Evaluate"] = entity.Shop_Evaluate;
                    DrAdd["Shop_Recommend"] = entity.Shop_Recommend;
                    DrAdd["Shop_Status"] = entity.Shop_Status;
                    DrAdd["Shop_Hits"] = entity.Shop_Hits;
                    DrAdd["Shop_Site"] = entity.Shop_Site;
                    DrAdd["Shop_Banner_IsActive"] = entity.Shop_Banner_IsActive;
                    DrAdd["Shop_Top_IsActive"] = entity.Shop_Top_IsActive;
                    DrAdd["Shop_TopNav_IsActive"] = entity.Shop_TopNav_IsActive;
                    DrAdd["Shop_Info_IsActive"] = entity.Shop_Info_IsActive;
                    DrAdd["Shop_LeftSearch_IsActive"] = entity.Shop_LeftSearch_IsActive;
                    DrAdd["Shop_LeftCate_IsActive"] = entity.Shop_LeftCate_IsActive;
                    DrAdd["Shop_LeftSale_IsActive"] = entity.Shop_LeftSale_IsActive;
                    DrAdd["Shop_Left_IsActive"] = entity.Shop_Left_IsActive;
                    DrAdd["Shop_Right_IsActive"] = entity.Shop_Right_IsActive;
                    DrAdd["Shop_RightProduct_IsActive"] = entity.Shop_RightProduct_IsActive;

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

        public virtual int DelSupplierShop(int ID)
        {
            string SqlAdd = "DELETE FROM Supplier_Shop WHERE Shop_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual SupplierShopInfo GetSupplierShopByID(int ID)
        {
            SupplierShopInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Supplier_Shop WHERE Shop_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SupplierShopInfo();

                    entity.Shop_ID = Tools.NullInt(RdrList["Shop_ID"]);
                    entity.Shop_Code = Tools.NullStr(RdrList["Shop_Code"]);
                    entity.Shop_Type = Tools.NullInt(RdrList["Shop_Type"]);
                    entity.Shop_Name = Tools.NullStr(RdrList["Shop_Name"]);
                    entity.Shop_SupplierID = Tools.NullInt(RdrList["Shop_SupplierID"]);
                    entity.Shop_Img = Tools.NullStr(RdrList["Shop_Img"]);
                    entity.Shop_Css = Tools.NullInt(RdrList["Shop_Css"]);
                    entity.Shop_Banner = Tools.NullInt(RdrList["Shop_Banner"]);
                    entity.Shop_Banner_Title = Tools.NullStr(RdrList["Shop_Banner_Title"]);
                    entity.Shop_Banner_Title_Family = Tools.NullStr(RdrList["Shop_Banner_Title_Family"]);
                    entity.Shop_Banner_Title_Size = Tools.NullInt(RdrList["Shop_Banner_Title_Size"]);
                    entity.Shop_Banner_Title_LeftPadding = Tools.NullInt(RdrList["Shop_Banner_Title_LeftPadding"]);
                    entity.Shop_banner_Title_color = Tools.NullStr(RdrList["Shop_banner_Title_color"]);
                    entity.Shop_Banner_Img = Tools.NullStr(RdrList["Shop_Banner_Img"]);
                    entity.Shop_Domain = Tools.NullStr(RdrList["Shop_Domain"]);
                    entity.Shop_MainProduct = Tools.NullStr(RdrList["Shop_MainProduct"]);
                    entity.Shop_SEO_Title = Tools.NullStr(RdrList["Shop_SEO_Title"]);
                    entity.Shop_SEO_Keyword = Tools.NullStr(RdrList["Shop_SEO_Keyword"]);
                    entity.Shop_SEO_Description = Tools.NullStr(RdrList["Shop_SEO_Description"]);
                    entity.Shop_Addtime = Tools.NullDate(RdrList["Shop_Addtime"]);
                    entity.Shop_Evaluate = Tools.NullInt(RdrList["Shop_Evaluate"]);
                    entity.Shop_Recommend = Tools.NullInt(RdrList["Shop_Recommend"]);
                    entity.Shop_Status = Tools.NullInt(RdrList["Shop_Status"]);
                    entity.Shop_Hits = Tools.NullInt(RdrList["Shop_Hits"]);
                    entity.Shop_Site = Tools.NullStr(RdrList["Shop_Site"]);
                    entity.Shop_Banner_IsActive = Tools.NullInt(RdrList["Shop_Banner_IsActive"]);
                    entity.Shop_Top_IsActive = Tools.NullInt(RdrList["Shop_Top_IsActive"]);
                    entity.Shop_TopNav_IsActive = Tools.NullInt(RdrList["Shop_TopNav_IsActive"]);
                    entity.Shop_Info_IsActive = Tools.NullInt(RdrList["Shop_Info_IsActive"]);
                    entity.Shop_LeftSearch_IsActive = Tools.NullInt(RdrList["Shop_LeftSearch_IsActive"]);
                    entity.Shop_LeftCate_IsActive = Tools.NullInt(RdrList["Shop_LeftCate_IsActive"]);
                    entity.Shop_LeftSale_IsActive = Tools.NullInt(RdrList["Shop_LeftSale_IsActive"]);
                    entity.Shop_Left_IsActive = Tools.NullInt(RdrList["Shop_Left_IsActive"]);
                    entity.Shop_Right_IsActive = Tools.NullInt(RdrList["Shop_Right_IsActive"]);
                    entity.Shop_RightProduct_IsActive = Tools.NullInt(RdrList["Shop_RightProduct_IsActive"]);

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

        public virtual SupplierShopInfo GetSupplierShopBySupplierID(int ID)
        {
            SupplierShopInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Supplier_Shop WHERE Shop_SupplierID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SupplierShopInfo();

                    entity.Shop_ID = Tools.NullInt(RdrList["Shop_ID"]);
                    entity.Shop_Code = Tools.NullStr(RdrList["Shop_Code"]);
                    entity.Shop_Type = Tools.NullInt(RdrList["Shop_Type"]);
                    entity.Shop_Name = Tools.NullStr(RdrList["Shop_Name"]);
                    entity.Shop_SupplierID = Tools.NullInt(RdrList["Shop_SupplierID"]);
                    entity.Shop_Img = Tools.NullStr(RdrList["Shop_Img"]);
                    entity.Shop_Css = Tools.NullInt(RdrList["Shop_Css"]);
                    entity.Shop_Banner = Tools.NullInt(RdrList["Shop_Banner"]);
                    entity.Shop_Banner_Title = Tools.NullStr(RdrList["Shop_Banner_Title"]);
                    entity.Shop_Banner_Title_Family = Tools.NullStr(RdrList["Shop_Banner_Title_Family"]);
                    entity.Shop_Banner_Title_Size = Tools.NullInt(RdrList["Shop_Banner_Title_Size"]);
                    entity.Shop_Banner_Title_LeftPadding = Tools.NullInt(RdrList["Shop_Banner_Title_LeftPadding"]);
                    entity.Shop_banner_Title_color = Tools.NullStr(RdrList["Shop_banner_Title_color"]);
                    entity.Shop_Banner_Img = Tools.NullStr(RdrList["Shop_Banner_Img"]);
                    entity.Shop_Domain = Tools.NullStr(RdrList["Shop_Domain"]);
                    entity.Shop_MainProduct = Tools.NullStr(RdrList["Shop_MainProduct"]);
                    entity.Shop_SEO_Title = Tools.NullStr(RdrList["Shop_SEO_Title"]);
                    entity.Shop_SEO_Keyword = Tools.NullStr(RdrList["Shop_SEO_Keyword"]);
                    entity.Shop_SEO_Description = Tools.NullStr(RdrList["Shop_SEO_Description"]);
                    entity.Shop_Addtime = Tools.NullDate(RdrList["Shop_Addtime"]);
                    entity.Shop_Evaluate = Tools.NullInt(RdrList["Shop_Evaluate"]);
                    entity.Shop_Recommend = Tools.NullInt(RdrList["Shop_Recommend"]);
                    entity.Shop_Status = Tools.NullInt(RdrList["Shop_Status"]);
                    entity.Shop_Hits = Tools.NullInt(RdrList["Shop_Hits"]);
                    entity.Shop_Site = Tools.NullStr(RdrList["Shop_Site"]);
                    entity.Shop_Banner_IsActive = Tools.NullInt(RdrList["Shop_Banner_IsActive"]);
                    entity.Shop_Top_IsActive = Tools.NullInt(RdrList["Shop_Top_IsActive"]);
                    entity.Shop_TopNav_IsActive = Tools.NullInt(RdrList["Shop_TopNav_IsActive"]);
                    entity.Shop_Info_IsActive = Tools.NullInt(RdrList["Shop_Info_IsActive"]);
                    entity.Shop_LeftSearch_IsActive = Tools.NullInt(RdrList["Shop_LeftSearch_IsActive"]);
                    entity.Shop_LeftCate_IsActive = Tools.NullInt(RdrList["Shop_LeftCate_IsActive"]);
                    entity.Shop_LeftSale_IsActive = Tools.NullInt(RdrList["Shop_LeftSale_IsActive"]);
                    entity.Shop_Left_IsActive = Tools.NullInt(RdrList["Shop_Left_IsActive"]);
                    entity.Shop_Right_IsActive = Tools.NullInt(RdrList["Shop_Right_IsActive"]);
                    entity.Shop_RightProduct_IsActive = Tools.NullInt(RdrList["Shop_RightProduct_IsActive"]);

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

        public virtual SupplierShopInfo GetSupplierShopByDomain(string Domain)
        {
            SupplierShopInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Supplier_Shop WHERE Shop_Domain = '" + Domain + "'";
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SupplierShopInfo();

                    entity.Shop_ID = Tools.NullInt(RdrList["Shop_ID"]);
                    entity.Shop_Code = Tools.NullStr(RdrList["Shop_Code"]);
                    entity.Shop_Type = Tools.NullInt(RdrList["Shop_Type"]);
                    entity.Shop_Name = Tools.NullStr(RdrList["Shop_Name"]);
                    entity.Shop_SupplierID = Tools.NullInt(RdrList["Shop_SupplierID"]);
                    entity.Shop_Img = Tools.NullStr(RdrList["Shop_Img"]);
                    entity.Shop_Css = Tools.NullInt(RdrList["Shop_Css"]);
                    entity.Shop_Banner = Tools.NullInt(RdrList["Shop_Banner"]);
                    entity.Shop_Banner_Title = Tools.NullStr(RdrList["Shop_Banner_Title"]);
                    entity.Shop_Banner_Title_Family = Tools.NullStr(RdrList["Shop_Banner_Title_Family"]);
                    entity.Shop_Banner_Title_Size = Tools.NullInt(RdrList["Shop_Banner_Title_Size"]);
                    entity.Shop_Banner_Title_LeftPadding = Tools.NullInt(RdrList["Shop_Banner_Title_LeftPadding"]);
                    entity.Shop_banner_Title_color = Tools.NullStr(RdrList["Shop_banner_Title_color"]);
                    entity.Shop_Banner_Img = Tools.NullStr(RdrList["Shop_Banner_Img"]);
                    entity.Shop_Domain = Tools.NullStr(RdrList["Shop_Domain"]);
                    entity.Shop_MainProduct = Tools.NullStr(RdrList["Shop_MainProduct"]);
                    entity.Shop_SEO_Title = Tools.NullStr(RdrList["Shop_SEO_Title"]);
                    entity.Shop_SEO_Keyword = Tools.NullStr(RdrList["Shop_SEO_Keyword"]);
                    entity.Shop_SEO_Description = Tools.NullStr(RdrList["Shop_SEO_Description"]);
                    entity.Shop_Addtime = Tools.NullDate(RdrList["Shop_Addtime"]);
                    entity.Shop_Evaluate = Tools.NullInt(RdrList["Shop_Evaluate"]);
                    entity.Shop_Recommend = Tools.NullInt(RdrList["Shop_Recommend"]);
                    entity.Shop_Status = Tools.NullInt(RdrList["Shop_Status"]);
                    entity.Shop_Hits = Tools.NullInt(RdrList["Shop_Hits"]);
                    entity.Shop_Site = Tools.NullStr(RdrList["Shop_Site"]);
                    entity.Shop_Banner_IsActive = Tools.NullInt(RdrList["Shop_Banner_IsActive"]);
                    entity.Shop_Top_IsActive = Tools.NullInt(RdrList["Shop_Top_IsActive"]);
                    entity.Shop_TopNav_IsActive = Tools.NullInt(RdrList["Shop_TopNav_IsActive"]);
                    entity.Shop_Info_IsActive = Tools.NullInt(RdrList["Shop_Info_IsActive"]);
                    entity.Shop_LeftSearch_IsActive = Tools.NullInt(RdrList["Shop_LeftSearch_IsActive"]);
                    entity.Shop_LeftCate_IsActive = Tools.NullInt(RdrList["Shop_LeftCate_IsActive"]);
                    entity.Shop_LeftSale_IsActive = Tools.NullInt(RdrList["Shop_LeftSale_IsActive"]);
                    entity.Shop_Left_IsActive = Tools.NullInt(RdrList["Shop_Left_IsActive"]);
                    entity.Shop_Right_IsActive = Tools.NullInt(RdrList["Shop_Right_IsActive"]);
                    entity.Shop_RightProduct_IsActive = Tools.NullInt(RdrList["Shop_RightProduct_IsActive"]);

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

        public virtual IList<SupplierShopInfo> GetSupplierShops(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<SupplierShopInfo> entitys = null;


            SupplierShopInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Supplier_Shop";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SupplierShopInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SupplierShopInfo();
                        entity.Shop_ID = Tools.NullInt(RdrList["Shop_ID"]);
                        entity.Shop_Code = Tools.NullStr(RdrList["Shop_Code"]);
                        entity.Shop_Type = Tools.NullInt(RdrList["Shop_Type"]);
                        entity.Shop_Name = Tools.NullStr(RdrList["Shop_Name"]);
                        entity.Shop_SupplierID = Tools.NullInt(RdrList["Shop_SupplierID"]);
                        entity.Shop_Img = Tools.NullStr(RdrList["Shop_Img"]);
                        entity.Shop_Css = Tools.NullInt(RdrList["Shop_Css"]);
                        entity.Shop_Banner = Tools.NullInt(RdrList["Shop_Banner"]);
                        entity.Shop_Banner_Title = Tools.NullStr(RdrList["Shop_Banner_Title"]);
                        entity.Shop_Banner_Title_Family = Tools.NullStr(RdrList["Shop_Banner_Title_Family"]);
                        entity.Shop_Banner_Title_Size = Tools.NullInt(RdrList["Shop_Banner_Title_Size"]);
                        entity.Shop_Banner_Title_LeftPadding = Tools.NullInt(RdrList["Shop_Banner_Title_LeftPadding"]);
                        entity.Shop_banner_Title_color = Tools.NullStr(RdrList["Shop_banner_Title_color"]);
                        entity.Shop_Banner_Img = Tools.NullStr(RdrList["Shop_Banner_Img"]);
                        entity.Shop_Domain = Tools.NullStr(RdrList["Shop_Domain"]);
                        entity.Shop_MainProduct = Tools.NullStr(RdrList["Shop_MainProduct"]);
                        entity.Shop_SEO_Title = Tools.NullStr(RdrList["Shop_SEO_Title"]);
                        entity.Shop_SEO_Keyword = Tools.NullStr(RdrList["Shop_SEO_Keyword"]);
                        entity.Shop_SEO_Description = Tools.NullStr(RdrList["Shop_SEO_Description"]);
                        entity.Shop_Addtime = Tools.NullDate(RdrList["Shop_Addtime"]);
                        entity.Shop_Evaluate = Tools.NullInt(RdrList["Shop_Evaluate"]);
                        entity.Shop_Recommend = Tools.NullInt(RdrList["Shop_Recommend"]);
                        entity.Shop_Status = Tools.NullInt(RdrList["Shop_Status"]);
                        entity.Shop_Hits = Tools.NullInt(RdrList["Shop_Hits"]);
                        entity.Shop_Site = Tools.NullStr(RdrList["Shop_Site"]);
                        entity.Shop_Banner_IsActive = Tools.NullInt(RdrList["Shop_Banner_IsActive"]);
                        entity.Shop_Top_IsActive = Tools.NullInt(RdrList["Shop_Top_IsActive"]);
                        entity.Shop_TopNav_IsActive = Tools.NullInt(RdrList["Shop_TopNav_IsActive"]);
                        entity.Shop_Info_IsActive = Tools.NullInt(RdrList["Shop_Info_IsActive"]);
                        entity.Shop_LeftSearch_IsActive = Tools.NullInt(RdrList["Shop_LeftSearch_IsActive"]);
                        entity.Shop_LeftCate_IsActive = Tools.NullInt(RdrList["Shop_LeftCate_IsActive"]);
                        entity.Shop_LeftSale_IsActive = Tools.NullInt(RdrList["Shop_LeftSale_IsActive"]);
                        entity.Shop_Left_IsActive = Tools.NullInt(RdrList["Shop_Left_IsActive"]);
                        entity.Shop_Right_IsActive = Tools.NullInt(RdrList["Shop_Right_IsActive"]);
                        entity.Shop_RightProduct_IsActive = Tools.NullInt(RdrList["Shop_RightProduct_IsActive"]);

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
                SqlTable = "Supplier_Shop";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Shop_ID) FROM " + SqlTable + SqlParam;

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

        //保存店铺对应类别
        public virtual void SaveShopCategory(int Shop_ID, string[] extends)
        {
            ArrayList sqlList = new ArrayList(extends.GetLength(0));
            DBHelper.ExecuteNonQuery("DELETE FROM Supplier_Shop_Category WHERE Shop_Cate_ShopID =" + Shop_ID);
            foreach (string cateid in extends)
            {
                if (Tools.CheckInt(cateid)>0)
                    sqlList.Add("INSERT INTO Supplier_Shop_Category (Shop_Cate_CateID, Shop_Cate_ShopID) VALUES (" + cateid + ", " + Shop_ID + ")");
            }
            DBHelper.ExecuteNonQuery(sqlList);
            sqlList = null;
        }

        public virtual string GetShopCategory(int Shop_ID)
        {
            string SqlList = "", strCate = "";
            SqlDataReader RdrList = null;

            SqlList = "SELECT Shop_Cate_CateID FROM Supplier_Shop_Category WHERE Shop_Cate_ShopID =" + Shop_ID;
            try
            {
                RdrList = DBHelper.ExecuteReader(SqlList);
                while (RdrList.Read())
                {
                    if (strCate.Length > 0) { strCate += "," + Tools.NullStr(RdrList[0]); }
                    else { strCate = Tools.NullStr(RdrList[0]); }
                }

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
            return strCate;

        }

    }

    public class SupplierShopDomain : ISupplierShopDomain
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public SupplierShopDomain()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddSupplierShopDomain(SupplierShopDomainInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Supplier_Shop_Domain";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Shop_Domain_ID"] = entity.Shop_Domain_ID;
            DrAdd["Shop_Domain_SupplierID"] = entity.Shop_Domain_SupplierID;
            DrAdd["Shop_Domain_Type"] = entity.Shop_Domain_Type;
            DrAdd["Shop_Domain_ShopID"] = entity.Shop_Domain_ShopID;
            DrAdd["Shop_Domain_Name"] = entity.Shop_Domain_Name;
            DrAdd["Shop_Domain_Status"] = entity.Shop_Domain_Status;
            DrAdd["Shop_Domain_Addtime"] = entity.Shop_Domain_Addtime;
            DrAdd["Shop_Domain_Site"] = entity.Shop_Domain_Site;

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

        public virtual bool EditSupplierShopDomain(SupplierShopDomainInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Supplier_Shop_Domain WHERE Shop_Domain_ID = " + entity.Shop_Domain_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Shop_Domain_ID"] = entity.Shop_Domain_ID;
                    DrAdd["Shop_Domain_SupplierID"] = entity.Shop_Domain_SupplierID;
                    DrAdd["Shop_Domain_Type"] = entity.Shop_Domain_Type;
                    DrAdd["Shop_Domain_ShopID"] = entity.Shop_Domain_ShopID;
                    DrAdd["Shop_Domain_Name"] = entity.Shop_Domain_Name;
                    DrAdd["Shop_Domain_Status"] = entity.Shop_Domain_Status;
                    DrAdd["Shop_Domain_Addtime"] = entity.Shop_Domain_Addtime;
                    DrAdd["Shop_Domain_Site"] = entity.Shop_Domain_Site;

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

        public virtual int DelSupplierShopDomain(int ID)
        {
            string SqlAdd = "DELETE FROM Supplier_Shop_Domain WHERE Shop_Domain_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual SupplierShopDomainInfo GetSupplierShopDomainByID(int ID)
        {
            SupplierShopDomainInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Supplier_Shop_Domain WHERE Shop_Domain_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SupplierShopDomainInfo();

                    entity.Shop_Domain_ID = Tools.NullInt(RdrList["Shop_Domain_ID"]);
                    entity.Shop_Domain_SupplierID = Tools.NullInt(RdrList["Shop_Domain_SupplierID"]);
                    entity.Shop_Domain_Type = Tools.NullInt(RdrList["Shop_Domain_Type"]);
                    entity.Shop_Domain_ShopID = Tools.NullInt(RdrList["Shop_Domain_ShopID"]);
                    entity.Shop_Domain_Name = Tools.NullStr(RdrList["Shop_Domain_Name"]);
                    entity.Shop_Domain_Status = Tools.NullInt(RdrList["Shop_Domain_Status"]);
                    entity.Shop_Domain_Addtime = Tools.NullDate(RdrList["Shop_Domain_Addtime"]);
                    entity.Shop_Domain_Site = Tools.NullStr(RdrList["Shop_Domain_Site"]);

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

        public virtual IList<SupplierShopDomainInfo> GetSupplierShopDomains(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<SupplierShopDomainInfo> entitys = null;
            SupplierShopDomainInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Supplier_Shop_Domain";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SupplierShopDomainInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SupplierShopDomainInfo();
                        entity.Shop_Domain_ID = Tools.NullInt(RdrList["Shop_Domain_ID"]);
                        entity.Shop_Domain_SupplierID = Tools.NullInt(RdrList["Shop_Domain_SupplierID"]);
                        entity.Shop_Domain_Type = Tools.NullInt(RdrList["Shop_Domain_Type"]);
                        entity.Shop_Domain_ShopID = Tools.NullInt(RdrList["Shop_Domain_ShopID"]);
                        entity.Shop_Domain_Name = Tools.NullStr(RdrList["Shop_Domain_Name"]);
                        entity.Shop_Domain_Status = Tools.NullInt(RdrList["Shop_Domain_Status"]);
                        entity.Shop_Domain_Addtime = Tools.NullDate(RdrList["Shop_Domain_Addtime"]);
                        entity.Shop_Domain_Site = Tools.NullStr(RdrList["Shop_Domain_Site"]);

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
                SqlTable = "Supplier_Shop_Domain";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Shop_Domain_ID) FROM " + SqlTable + SqlParam;

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
