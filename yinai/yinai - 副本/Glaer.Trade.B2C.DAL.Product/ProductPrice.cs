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
    public class ProductPrice : IProductPrice
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public ProductPrice()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddProductPrice(ProductPriceInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Product_Price";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Product_Price_ID"] = entity.Product_Price_ID;
            DrAdd["Product_Price_ProcutID"] = entity.Product_Price_ProcutID;
            DrAdd["Product_Price_MemberGradeID"] = entity.Product_Price_MemberGradeID;
            DrAdd["Product_Price_Price"] = entity.Product_Price_Price;

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

        public virtual bool EditProductPrice(ProductPriceInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Product_Price WHERE Product_Price_ID = " + entity.Product_Price_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Product_Price_ID"] = entity.Product_Price_ID;
                    DrAdd["Product_Price_ProcutID"] = entity.Product_Price_ProcutID;
                    DrAdd["Product_Price_MemberGradeID"] = entity.Product_Price_MemberGradeID;
                    DrAdd["Product_Price_Price"] = entity.Product_Price_Price;

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

        public virtual int DelProductPrice(int Product_ID)
        {
            string SqlAdd = "DELETE FROM Product_Price WHERE Product_Price_ProcutID = " + Product_ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual ProductPriceInfo GetProductPriceByID(int ID)
        {
            ProductPriceInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Product_Price WHERE Product_Price_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new ProductPriceInfo();

                    entity.Product_Price_ID = Tools.NullInt(RdrList["Product_Price_ID"]);
                    entity.Product_Price_ProcutID = Tools.NullInt(RdrList["Product_Price_ProcutID"]);
                    entity.Product_Price_MemberGradeID = Tools.NullInt(RdrList["Product_Price_MemberGradeID"]);
                    entity.Product_Price_Price = Tools.NullDbl(RdrList["Product_Price_Price"]);

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

        public virtual IList<ProductPriceInfo> GetProductPrices(int Product_ID)
        {
            string SqlList;
            IList<ProductPriceInfo> entitys = null;
            ProductPriceInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                SqlList = "SELECT * FROM Product_Price WHERE Product_Price_ProcutID = " + Product_ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<ProductPriceInfo>();
                    while (RdrList.Read())
                    {
                        entity = new ProductPriceInfo();
                        entity.Product_Price_ID = Tools.NullInt(RdrList["Product_Price_ID"]);
                        entity.Product_Price_ProcutID = Tools.NullInt(RdrList["Product_Price_ProcutID"]);
                        entity.Product_Price_MemberGradeID = Tools.NullInt(RdrList["Product_Price_MemberGradeID"]);
                        entity.Product_Price_Price = Tools.NullDbl(RdrList["Product_Price_Price"]);

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
                SqlTable = "Product_Price";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Product_Price_ID) FROM " + SqlTable + SqlParam;

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
