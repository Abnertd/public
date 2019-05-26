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
    public class ProductType : IProductType
    {
        ITools Tools;
        ISQLHelper DBHelper;
        IBrand MyBrand;
        IProductTypeExtend MyProductTypeExtend;

        public ProductType()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
            MyBrand = BrandFactory.CreateBrand();
            MyProductTypeExtend = ProductTypeExtendFactory.CreateProductTypeExtend();
        }

        public virtual bool AddProductType(ProductTypeInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM ProductType";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["ProductType_ID"] = entity.ProductType_ID;
            DrAdd["ProductType_Name"] = entity.ProductType_Name;
            DrAdd["ProductType_Sort"] = entity.ProductType_Sort;
            DrAdd["ProductType_IsActive"] = entity.ProductType_IsActive;
            DrAdd["ProductType_Site"] = entity.ProductType_Site;


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

        public virtual bool AddProductType_Brand(int ProductType_ID,int Brand_ID)
        {
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            string SqlAdd;
            SqlAdd = "SELECT TOP 0 * FROM ProductType_Brand";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["ProductType_Brand_BrandID"] = Brand_ID;
            DrAdd["ProductType_Brand_ProductTypeID"] = ProductType_ID;

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

        public virtual bool EditProductType(ProductTypeInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM ProductType WHERE ProductType_ID = " + entity.ProductType_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["ProductType_ID"] = entity.ProductType_ID;
                    DrAdd["ProductType_Name"] = entity.ProductType_Name;
                    DrAdd["ProductType_Sort"] = entity.ProductType_Sort;
                    DrAdd["ProductType_IsActive"] = entity.ProductType_IsActive;
                    DrAdd["ProductType_Site"] = entity.ProductType_Site;

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

        public virtual int DelProductType(int ID)
        {
            string SqlAdd = "DELETE FROM ProductType WHERE ProductType_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual int DelProductType_Brand(int ProductType_ID) {
            string SqlAdd = "DELETE FROM ProductType_Brand WHERE ProductType_Brand_ProductTypeID = " + ProductType_ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual int DelProductType_Brand(int ProductType_ID, int Brand_ID)
        {
            string SqlAdd = "DELETE FROM ProductType_Brand";

            if (ProductType_ID > 0)
            {
                SqlAdd += " WHERE ProductType_Brand_ProductTypeID = " + ProductType_ID + " and ProductType_Brand_BrandID=" + Brand_ID;
            }
            else
            {
                SqlAdd += " WHERE ProductType_Brand_BrandID=" + Brand_ID;
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

        public virtual int DelProductType_Extend(int ProductType_ID)
        {
            string SqlAdd = "DELETE FROM ProductType_Extend WHERE ProductType_Extend_ProductTypeID = " + ProductType_ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual ProductTypeInfo GetProductTypeMax()
        {
            ProductTypeInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT top 1 * FROM ProductType order by ProductType_ID desc";
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new ProductTypeInfo();

                    entity.ProductType_ID = Tools.NullInt(RdrList["ProductType_ID"]);
                    entity.ProductType_Name = Tools.NullStr(RdrList["ProductType_Name"]);
                    entity.ProductType_Sort = Tools.NullInt(RdrList["ProductType_Sort"]);
                    entity.ProductType_IsActive = Tools.NullInt(RdrList["ProductType_IsActive"]);
                    entity.ProductType_Site = Tools.NullStr(RdrList["ProductType_Site"]);

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

        public virtual ProductTypeInfo GetProductTypeByID(int ID)
        {
            ProductTypeInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM ProductType WHERE ProductType_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new ProductTypeInfo();

                    entity.ProductType_ID = Tools.NullInt(RdrList["ProductType_ID"]);
                    entity.ProductType_Name = Tools.NullStr(RdrList["ProductType_Name"]);
                    entity.ProductType_Sort = Tools.NullInt(RdrList["ProductType_Sort"]);
                    entity.ProductType_IsActive = Tools.NullInt(RdrList["ProductType_IsActive"]);
                    entity.ProductType_Site = Tools.NullStr(RdrList["ProductType_Site"]);
                    

                }
                RdrList.Close();
                RdrList = null;
                if (entity != null) {
                    entity.BrandInfos = GetProductBrands(entity.ProductType_ID);
                    entity.ProductTypeExtendInfos = MyProductTypeExtend.GetProductTypeExtends(entity.ProductType_ID);
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

        public virtual IList<ProductTypeInfo> GetProductTypes(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<ProductTypeInfo> entitys = null;
            ProductTypeInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "ProductType";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<ProductTypeInfo>();
                    while (RdrList.Read())
                    {
                        entity = new ProductTypeInfo();
                        entity.ProductType_ID = Tools.NullInt(RdrList["ProductType_ID"]);
                        entity.ProductType_Name = Tools.NullStr(RdrList["ProductType_Name"]);
                        entity.ProductType_Sort = Tools.NullInt(RdrList["ProductType_Sort"]);
                        entity.ProductType_IsActive = Tools.NullInt(RdrList["ProductType_IsActive"]);
                        entity.ProductType_Site = Tools.NullStr(RdrList["ProductType_Site"]);
                        entitys.Add(entity);
                        entity = null;
                    }
                }
                RdrList.Close();
                RdrList = null;
                if (entitys != null) 
                {
                    foreach (ProductTypeInfo obj in entitys)
                    {
                        obj.BrandInfos = GetProductBrands(obj.ProductType_ID);
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

        public virtual IList<BrandInfo> GetProductBrands(int ProductTypeID)
        {
            BrandInfo entity = null;
            SqlDataReader RdrList = null;
            IList<BrandInfo> entitys = null;
            string brand_arrystr="";
            try
            {
                string SqlList;

                SqlList = "SELECT * FROM ProductType_Brand WHERE ProductType_Brand_ProductTypeID = " + ProductTypeID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                   
                    while (RdrList.Read())
                    {

                        brand_arrystr = brand_arrystr + Tools.NullInt(RdrList["ProductType_Brand_BrandID"]) + ",";
                        
                    }
                }
                RdrList.Close();
                RdrList = null;

                if (brand_arrystr != "")
                { 
                    entitys=new List<BrandInfo>();
                    foreach (string i in brand_arrystr.Split(','))
                    {
                        if (Tools.CheckInt(i) > 0) 
                        {
                            entity = new BrandInfo();
                            entity = MyBrand.GetBrandByID(Tools.CheckInt(i));
                            if (entity != null)
                            {
                                entitys.Add(entity);
                            }
                            entity = null;
                        }
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
                SqlTable = "ProductType";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(ProductType_ID) FROM " + SqlTable + SqlParam;

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
