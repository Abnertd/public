using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.Util.Tools;
using System.Text;

namespace Glaer.Trade.B2C.DAL.Product
{
    public class ProductTag : IProductTag 
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public ProductTag() {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddProductTag (ProductTagInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM product_tag";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Product_Tag_ID"] = entity.Product_Tag_ID;
            DrAdd["Product_Tag_Name"] = entity.Product_Tag_Name;
            DrAdd["Product_Tag_IsSupplier"] = entity.Product_Tag_IsSupplier;
            DrAdd["Product_Tag_SupplierID"] = entity.Product_Tag_SupplierID;
            DrAdd["Product_Tag_IsActive"] = entity.Product_Tag_IsActive;
            DrAdd["Product_Tag_Sort"] = entity.Product_Tag_Sort;
            DrAdd["Product_Tag_Site"] = entity.Product_Tag_Site;

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

        public virtual bool EditProductTag (ProductTagInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM product_tag WHERE Product_Tag_ID = " + entity.Product_Tag_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try {
                if (DtAdd.Rows.Count > 0) {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Product_Tag_ID"] = entity.Product_Tag_ID;
                    DrAdd["Product_Tag_Name"] = entity.Product_Tag_Name;
                    DrAdd["Product_Tag_IsSupplier"] = entity.Product_Tag_IsSupplier;
                    DrAdd["Product_Tag_SupplierID"] = entity.Product_Tag_SupplierID;
                    DrAdd["Product_Tag_IsActive"] = entity.Product_Tag_IsActive;
                    DrAdd["Product_Tag_Sort"] = entity.Product_Tag_Sort;
                    DrAdd["Product_Tag_Site"] = entity.Product_Tag_Site;

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

        public virtual int DelProductTag(int Product_Tag_ID)
        {
            string SqlAdd = "DELETE FROM product_tag WHERE Product_Tag_ID = " + Product_Tag_ID;
            try {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public virtual ProductTagInfo GetProductTagByID(int product_tag_id)
        {
            ProductTagInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM product_tag WHERE Product_Tag_ID = " + product_tag_id;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new ProductTagInfo();
                    entity.Product_Tag_ID = Tools.NullInt(RdrList["Product_Tag_ID"]);
                    entity.Product_Tag_Name = Tools.NullStr(RdrList["Product_Tag_Name"]);
                    entity.Product_Tag_IsSupplier = Tools.NullInt(RdrList["Product_Tag_IsSupplier"]);
                    entity.Product_Tag_SupplierID = Tools.NullInt(RdrList["Product_Tag_SupplierID"]);
                    entity.Product_Tag_IsActive = Tools.NullInt(RdrList["Product_Tag_IsActive"]);
                    entity.Product_Tag_Sort = Tools.NullInt(RdrList["Product_Tag_Sort"]);
                    entity.Product_Tag_Site = Tools.NullStr(RdrList["Product_Tag_Site"]);
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

        public virtual ProductTagInfo GetProductTagByValue(string tag_Value)
        {
            ProductTagInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM product_tag WHERE Product_Tag_IsActive=1 and Product_Tag_Name = '" + tag_Value + "'";
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new ProductTagInfo();
                    entity.Product_Tag_ID = Tools.NullInt(RdrList["Product_Tag_ID"]);
                    entity.Product_Tag_Name = Tools.NullStr(RdrList["Product_Tag_Name"]);
                    entity.Product_Tag_IsSupplier = Tools.NullInt(RdrList["Product_Tag_IsSupplier"]);
                    entity.Product_Tag_SupplierID = Tools.NullInt(RdrList["Product_Tag_SupplierID"]);
                    entity.Product_Tag_IsActive = Tools.NullInt(RdrList["Product_Tag_IsActive"]);
                    entity.Product_Tag_Sort = Tools.NullInt(RdrList["Product_Tag_Sort"]);
                    entity.Product_Tag_Site = Tools.NullStr(RdrList["Product_Tag_Site"]);
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
        
        public virtual IList<ProductTagInfo> GetProductTags(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<ProductTagInfo> entitys = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable; 
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "product_tag";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<ProductTagInfo>();
                    while (RdrList.Read())
                    {
                        ProductTagInfo entity = new ProductTagInfo();
                        entity.Product_Tag_ID = Tools.NullInt(RdrList["Product_Tag_ID"]);
                        entity.Product_Tag_Name = Tools.NullStr(RdrList["Product_Tag_Name"]);
                        entity.Product_Tag_IsSupplier = Tools.NullInt(RdrList["Product_Tag_IsSupplier"]);
                        entity.Product_Tag_SupplierID = Tools.NullInt(RdrList["Product_Tag_SupplierID"]);
                        entity.Product_Tag_IsActive = Tools.NullInt(RdrList["Product_Tag_IsActive"]);
                        entity.Product_Tag_Sort = Tools.NullInt(RdrList["Product_Tag_Sort"]);
                        entity.Product_Tag_Site = Tools.NullStr(RdrList["Product_Tag_Site"]);
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
                SqlTable = "product_tag";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Product_Tag_ID) FROM " + SqlTable + SqlParam;

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

        public virtual int AddProductRelateTag(string Product_RelateTag_ProductID, int Product_RelateTag_TagID)
        {
            string SProduct_ID;
            int IProduct_ID;
            string strDelSql = "delete Product_RelateTag where Product_RelateTag_TagID =" + Product_RelateTag_TagID + "";
            DBHelper.ExecuteNonQuery(strDelSql);
            StringBuilder stbSql = new StringBuilder();

            string[] ArrayProd = Product_RelateTag_ProductID.Split(',');
            for (int i = 0; i < ArrayProd.Length; i++)
            {
                SProduct_ID = ArrayProd[i];
                IProduct_ID = Tools.NullInt(SProduct_ID);
                if (IProduct_ID > 0 && Product_RelateTag_TagID > 0)
                {
                    stbSql.Append("insert into Product_RelateTag (Product_RelateTag_ProductID,Product_RelateTag_TagID)");
                    stbSql.Append(" values ( " + IProduct_ID);
                    stbSql.Append("," + Product_RelateTag_TagID + ")");
                }

            }
            if (stbSql.Length > 0)
            {
                string strSql = Convert.ToString(stbSql);
                int iRet = Convert.ToInt32(DBHelper.ExecuteNonQuery(strSql));
                return iRet;
            }
            else
            {
                return 0;
            }

        }
        
    }
}
