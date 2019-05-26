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
    public class ProductNotify : IProductNotify
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public ProductNotify()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddProductNotify(ProductNotifyInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Product_Notify";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Product_Notify_ID"] = entity.Product_Notify_ID;
            DrAdd["Product_Notify_MemberID"] = entity.Product_Notify_MemberID;
            DrAdd["Product_Notify_Email"] = entity.Product_Notify_Email;
            DrAdd["Product_Notify_ProductID"] = entity.Product_Notify_ProductID;
            DrAdd["Product_Notify_IsNotify"] = entity.Product_Notify_IsNotify;
            DrAdd["Product_Notify_Addtime"] = entity.Product_Notify_Addtime;
            DrAdd["Product_Notify_Site"] = entity.Product_Notify_Site;

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

        public virtual bool EditProductNotify(ProductNotifyInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Product_Notify WHERE Product_Notify_ID = " + entity.Product_Notify_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Product_Notify_ID"] = entity.Product_Notify_ID;
                    DrAdd["Product_Notify_MemberID"] = entity.Product_Notify_MemberID;
                    DrAdd["Product_Notify_Email"] = entity.Product_Notify_Email;
                    DrAdd["Product_Notify_ProductID"] = entity.Product_Notify_ProductID;
                    DrAdd["Product_Notify_IsNotify"] = entity.Product_Notify_IsNotify;
                    DrAdd["Product_Notify_Addtime"] = entity.Product_Notify_Addtime;
                    DrAdd["Product_Notify_Site"] = entity.Product_Notify_Site;

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

        public virtual int DelProductNotify(int ID)
        {
            string SqlAdd = "DELETE FROM Product_Notify WHERE Product_Notify_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual ProductNotifyInfo GetProductNotifyByID(int ID)
        {
            ProductNotifyInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Product_Notify WHERE Product_Notify_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new ProductNotifyInfo();

                    entity.Product_Notify_ID = Tools.NullInt(RdrList["Product_Notify_ID"]);
                    entity.Product_Notify_MemberID = Tools.NullInt(RdrList["Product_Notify_MemberID"]);
                    entity.Product_Notify_Email = Tools.NullStr(RdrList["Product_Notify_Email"]);
                    entity.Product_Notify_ProductID = Tools.NullInt(RdrList["Product_Notify_ProductID"]);
                    entity.Product_Notify_IsNotify = Tools.NullInt(RdrList["Product_Notify_IsNotify"]);
                    entity.Product_Notify_Addtime = Tools.NullDate(RdrList["Product_Notify_Addtime"]);
                    entity.Product_Notify_Site = Tools.NullStr(RdrList["Product_Notify_Site"]);

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

        public virtual IList<ProductNotifyInfo> GetProductNotifys(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<ProductNotifyInfo> entitys = null;
            ProductNotifyInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Product_Notify";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<ProductNotifyInfo>();
                    while (RdrList.Read())
                    {
                        entity = new ProductNotifyInfo();
                        entity.Product_Notify_ID = Tools.NullInt(RdrList["Product_Notify_ID"]);
                        entity.Product_Notify_MemberID = Tools.NullInt(RdrList["Product_Notify_MemberID"]);
                        entity.Product_Notify_Email = Tools.NullStr(RdrList["Product_Notify_Email"]);
                        entity.Product_Notify_ProductID = Tools.NullInt(RdrList["Product_Notify_ProductID"]);
                        entity.Product_Notify_IsNotify = Tools.NullInt(RdrList["Product_Notify_IsNotify"]);
                        entity.Product_Notify_Addtime = Tools.NullDate(RdrList["Product_Notify_Addtime"]);
                        entity.Product_Notify_Site = Tools.NullStr(RdrList["Product_Notify_Site"]);


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
                SqlTable = "Product_Notify";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Product_Notify_ID) FROM " + SqlTable + SqlParam;

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
