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
    public class Brand : IBrand
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public Brand()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddBrand(BrandInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM brand";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Brand_ID"] = entity.Brand_ID;
            DrAdd["Brand_Name"] = entity.Brand_Name;
            DrAdd["Brand_Img"] = entity.Brand_Img;
            DrAdd["Brand_URL"] = entity.Brand_URL;
            DrAdd["Brand_Description"] = entity.Brand_Description;
            DrAdd["Brand_Sort"] = entity.Brand_Sort;
            DrAdd["Brand_IsRecommend"] = entity.Brand_IsRecommend;
            DrAdd["Brand_IsActive"] = entity.Brand_IsActive;
            DrAdd["Brand_Site"] = entity.Brand_Site;


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

        public virtual bool EditBrand(BrandInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM brand WHERE Brand_ID = " + entity.Brand_ID ;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Brand_ID"] = entity.Brand_ID;
                    DrAdd["Brand_Name"] = entity.Brand_Name;
                    DrAdd["Brand_Img"] = entity.Brand_Img;
                    DrAdd["Brand_URL"] = entity.Brand_URL;
                    DrAdd["Brand_Description"] = entity.Brand_Description;
                    DrAdd["Brand_Sort"] = entity.Brand_Sort;
                    DrAdd["Brand_IsRecommend"] = entity.Brand_IsRecommend;
                    DrAdd["Brand_IsActive"] = entity.Brand_IsActive;
                    DrAdd["Brand_Site"] = entity.Brand_Site;

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

        public virtual int DelBrand(int Brand_ID)
        {
            string SqlAdd = "DELETE FROM brand WHERE Brand_ID = " + Brand_ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual BrandInfo GetBrandByID(int Brand_ID)
        {
            BrandInfo Entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM brand WHERE Brand_ID = " + Brand_ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    Entity = new BrandInfo();

                    Entity.Brand_ID = Tools.NullInt(RdrList["Brand_ID"]);
                    Entity.Brand_Name = Tools.NullStr(RdrList["Brand_Name"]);
                    Entity.Brand_Img = Tools.NullStr(RdrList["Brand_Img"]);
                    Entity.Brand_URL = Tools.NullStr(RdrList["Brand_URL"]);
                    Entity.Brand_Description = Tools.NullStr(RdrList["Brand_Description"]);
                    Entity.Brand_Sort = Tools.NullInt(RdrList["Brand_Sort"]);
                    Entity.Brand_IsRecommend = Tools.NullInt(RdrList["Brand_IsRecommend"]);
                    Entity.Brand_IsActive = Tools.NullInt(RdrList["Brand_IsActive"]);
                    Entity.Brand_Site = Tools.NullStr(RdrList["Brand_Site"]);

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

        public virtual IList<BrandInfo> GetBrands(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<BrandInfo> entitys = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Brand";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<BrandInfo>();
                    while (RdrList.Read())
                    {
                        BrandInfo entity = new BrandInfo();
                        entity.Brand_ID = Tools.NullInt(RdrList["Brand_ID"]);
                        entity.Brand_Name = Tools.NullStr(RdrList["Brand_Name"]);
                        entity.Brand_Img = Tools.NullStr(RdrList["Brand_Img"]);
                        entity.Brand_URL = Tools.NullStr(RdrList["Brand_URL"]);
                        entity.Brand_Description = Tools.NullStr(RdrList["Brand_Description"]);
                        entity.Brand_Sort = Tools.NullInt(RdrList["Brand_Sort"]);
                        entity.Brand_IsActive = Tools.NullInt(RdrList["Brand_IsActive"]);
                        entity.Brand_Site = Tools.NullStr(RdrList["Brand_Site"]);


                        entitys.Add(entity);
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
                SqlTable = "brand";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Brand_ID) FROM " + SqlTable + SqlParam;

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

        public virtual string Get_Cate_Brand(string Cate_ID)
        {
            string brand_arry = "0";
            if (Cate_ID.Length > 0)
            {
                string SqlList = "";
                SqlDataReader RdrList = null;
                try
                {
                    SqlList = "Select ProductType_Brand_BrandID from ProductType_Brand where ProductType_Brand_ProductTypeID in (Select Cate_TypeID from Category where Cate_ID in (" + Cate_ID + "))";
                    RdrList = DBHelper.ExecuteReader(SqlList);
                    if (RdrList.HasRows)
                    {
                        while (RdrList.Read())
                        {
                            brand_arry = brand_arry + "," + Tools.NullInt(RdrList["ProductType_Brand_BrandID"]).ToString();
                        }
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
            }
            return brand_arry;
        }

    }
}
