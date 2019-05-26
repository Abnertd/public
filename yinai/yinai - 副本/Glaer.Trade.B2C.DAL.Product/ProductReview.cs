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
    public class ProductReview : IProductReview
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public ProductReview()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddProductReview(ProductReviewInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Product_Review";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Product_Review_ID"] = entity.Product_Review_ID;
            DrAdd["Product_Review_ProductID"] = entity.Product_Review_ProductID;
            DrAdd["Product_Review_MemberID"] = entity.Product_Review_MemberID;
            DrAdd["Product_Review_Star"] = entity.Product_Review_Star;
            DrAdd["Product_Review_Subject"] = entity.Product_Review_Subject;
            DrAdd["Product_Review_Content"] = entity.Product_Review_Content;
            DrAdd["Product_Review_Useful"] = entity.Product_Review_Useful;
            DrAdd["Product_Review_Useless"] = entity.Product_Review_Useless;
            DrAdd["Product_Review_Addtime"] = entity.Product_Review_Addtime;
            DrAdd["Product_Review_IsShow"] = entity.Product_Review_IsShow;
            DrAdd["Product_Review_IsBuy"] = entity.Product_Review_IsBuy;
            DrAdd["Product_Review_IsGift"] = entity.Product_Review_IsGift;
            DrAdd["Product_Review_IsView"] = 0;
            DrAdd["Product_Review_IsRecommend"] = entity.Product_Review_IsRecommend;
            DrAdd["Product_Review_Site"] = entity.Product_Review_Site;

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

        public virtual bool EditProductReview(ProductReviewInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Product_Review WHERE Product_Review_ID = " + entity.Product_Review_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Product_Review_ID"] = entity.Product_Review_ID;
                    DrAdd["Product_Review_ProductID"] = entity.Product_Review_ProductID;
                    DrAdd["Product_Review_MemberID"] = entity.Product_Review_MemberID;
                    DrAdd["Product_Review_Star"] = entity.Product_Review_Star;
                    DrAdd["Product_Review_Subject"] = entity.Product_Review_Subject;
                    DrAdd["Product_Review_Content"] = entity.Product_Review_Content;
                    DrAdd["Product_Review_Useful"] = entity.Product_Review_Useful;
                    DrAdd["Product_Review_Useless"] = entity.Product_Review_Useless;
                    DrAdd["Product_Review_IsShow"] = entity.Product_Review_IsShow;
                    DrAdd["Product_Review_IsBuy"] = entity.Product_Review_IsBuy;
                    DrAdd["Product_Review_IsGift"] = entity.Product_Review_IsGift;
                    DrAdd["Product_Review_IsView"] = entity.Product_Review_IsView;
                    DrAdd["Product_Review_IsRecommend"] = entity.Product_Review_IsRecommend;
                    DrAdd["Product_Review_Site"] = entity.Product_Review_Site;

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

        public virtual int DelProductReview(int ID)
        {
            string SqlAdd = "DELETE FROM Product_Review WHERE Product_Review_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual ProductReviewInfo GetProductReviewByID(int ID)
        {
            ProductReviewInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Product_Review WHERE Product_Review_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new ProductReviewInfo();

                    entity.Product_Review_ID = Tools.NullInt(RdrList["Product_Review_ID"]);
                    entity.Product_Review_ProductID = Tools.NullInt(RdrList["Product_Review_ProductID"]);
                    entity.Product_Review_MemberID = Tools.NullInt(RdrList["Product_Review_MemberID"]);
                    entity.Product_Review_Star = Tools.NullInt(RdrList["Product_Review_Star"]);
                    entity.Product_Review_Subject = Tools.NullStr(RdrList["Product_Review_Subject"]);
                    entity.Product_Review_Content = Tools.NullStr(RdrList["Product_Review_Content"]);
                    entity.Product_Review_Useful = Tools.NullInt(RdrList["Product_Review_Useful"]);
                    entity.Product_Review_Useless = Tools.NullInt(RdrList["Product_Review_Useless"]);
                    entity.Product_Review_Addtime = Tools.NullDate(RdrList["Product_Review_Addtime"]);
                    entity.Product_Review_IsShow = Tools.NullInt(RdrList["Product_Review_IsShow"]);
                    entity.Product_Review_IsBuy = Tools.NullInt(RdrList["Product_Review_IsBuy"]);
                    entity.Product_Review_IsGift = Tools.NullInt(RdrList["Product_Review_IsGift"]);
                    entity.Product_Review_IsView = Tools.NullInt(RdrList["Product_Review_IsView"]);
                    entity.Product_Review_IsRecommend = Tools.NullInt(RdrList["Product_Review_IsRecommend"]);
                    entity.Product_Review_Site = Tools.NullStr(RdrList["Product_Review_Site"]);

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

        public virtual IList<ProductReviewInfo> GetProductReviews(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<ProductReviewInfo> entitys = null;
            ProductReviewInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Product_Review";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<ProductReviewInfo>();
                    while (RdrList.Read())
                    {
                        entity = new ProductReviewInfo();
                        entity.Product_Review_ID = Tools.NullInt(RdrList["Product_Review_ID"]);
                        entity.Product_Review_ProductID = Tools.NullInt(RdrList["Product_Review_ProductID"]);
                        entity.Product_Review_MemberID = Tools.NullInt(RdrList["Product_Review_MemberID"]);
                        entity.Product_Review_Star = Tools.NullInt(RdrList["Product_Review_Star"]);
                        entity.Product_Review_Subject = Tools.NullStr(RdrList["Product_Review_Subject"]);
                        entity.Product_Review_Content = Tools.NullStr(RdrList["Product_Review_Content"]);
                        entity.Product_Review_Useful = Tools.NullInt(RdrList["Product_Review_Useful"]);
                        entity.Product_Review_Useless = Tools.NullInt(RdrList["Product_Review_Useless"]);
                        entity.Product_Review_Addtime = Tools.NullDate(RdrList["Product_Review_Addtime"]);
                        entity.Product_Review_IsShow = Tools.NullInt(RdrList["Product_Review_IsShow"]);
                        entity.Product_Review_IsBuy = Tools.NullInt(RdrList["Product_Review_IsBuy"]);
                        entity.Product_Review_IsGift = Tools.NullInt(RdrList["Product_Review_IsGift"]);
                        entity.Product_Review_IsView = Tools.NullInt(RdrList["Product_Review_IsView"]);
                        entity.Product_Review_IsRecommend = Tools.NullInt(RdrList["Product_Review_IsRecommend"]);
                        entity.Product_Review_Site = Tools.NullStr(RdrList["Product_Review_Site"]);

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
                SqlTable = "Product_Review";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Product_Review_ID) FROM " + SqlTable + SqlParam;

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

        public virtual int GetProductReviewValidCount(int Product_ID) {
            string SqlAdd = "select count(Product_Review_ID) FROM Product_Review WHERE Product_Review_IsShow=1 and Product_Review_ProductID = " + Product_ID;
            try
            {
                return Tools.NullInt(DBHelper.ExecuteScalar(SqlAdd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual int GetProductStarCount(int Product_ID)
        {
            string SqlAdd = "select SUM(Product_Review_Star) FROM Product_Review WHERE Product_Review_IsShow=1 and Product_Review_ProductID = " + Product_ID;
            try
            {
                return Tools.NullInt(DBHelper.ExecuteScalar(SqlAdd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual bool UpdateProductReviewINfo(int Product_ID, double Review_Average, int Review_Count,int Review_validCount)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT Product_ID,product_Review_Average,product_Review_Count,product_Review_validCount FROM Product_Basic WHERE Product_ID = " + Product_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Product_Review_Average"] = Review_Average;
                    DrAdd["Product_Review_Count"] = Review_Count;
                    DrAdd["Product_Review_ValidCount"] = Review_validCount;

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

    }
}
