using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.Util.Tools;

namespace Glaer.Trade.B2C.DAL.ORD
{
    public class OrdersBackApplyProduct : IOrdersBackApplyProduct
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public OrdersBackApplyProduct()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddOrdersBackApplyProduct(OrdersBackApplyProductInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Orders_BackApply_Product";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Orders_BackApply_Product_ID"] = entity.Orders_BackApply_Product_ID;
            DrAdd["Orders_BackApply_Product_ProductID"] = entity.Orders_BackApply_Product_ProductID;
            DrAdd["Orders_BackApply_Product_ApplyID"] = entity.Orders_BackApply_Product_ApplyID;
            DrAdd["Orders_BackApply_Product_ApplyAmount"] = entity.Orders_BackApply_Product_ApplyAmount;
            

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

        public virtual bool EditOrdersBackApplyProduct(OrdersBackApplyProductInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Orders_BackApply_Product WHERE Orders_BackApply_Product_ID = " + entity.Orders_BackApply_Product_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Orders_BackApply_Product_ID"] = entity.Orders_BackApply_Product_ID;
                    DrAdd["Orders_BackApply_Product_ProductID"] = entity.Orders_BackApply_Product_ProductID;
                    DrAdd["Orders_BackApply_Product_ApplyID"] = entity.Orders_BackApply_Product_ApplyID;
                    DrAdd["Orders_BackApply_Product_ApplyAmount"] = entity.Orders_BackApply_Product_ApplyAmount;
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

        public virtual int DelOrdersBackApplyProduct(int ID)
        {
            string SqlAdd = "DELETE FROM Orders_BackApply_Product WHERE Orders_BackApply_Product_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual int DelOrdersBackApplyProductByApplyID(int ID)
        {
            string SqlAdd = "DELETE FROM Orders_BackApply_Product WHERE Orders_BackApply_Product_ApplyID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual OrdersBackApplyProductInfo GetOrdersBackApplyProductByID(int ID)
        {
            OrdersBackApplyProductInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Orders_BackApply_Product WHERE Orders_BackApply_Product_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new OrdersBackApplyProductInfo();

                    entity.Orders_BackApply_Product_ID = Tools.NullInt(RdrList["Orders_BackApply_Product_ID"]);
                    entity.Orders_BackApply_Product_ProductID = Tools.NullInt(RdrList["Orders_BackApply_Product_ProductID"]);
                    entity.Orders_BackApply_Product_ApplyID = Tools.NullInt(RdrList["Orders_BackApply_Product_ApplyID"]);
                    entity.Orders_BackApply_Product_ApplyAmount = Tools.NullInt(RdrList["Orders_BackApply_Product_ApplyAmount"]);

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

        public virtual IList<OrdersBackApplyProductInfo> GetOrdersBackApplyProductByApplyID(int ID)
        {
            IList<OrdersBackApplyProductInfo> entitys = null;
            OrdersBackApplyProductInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Orders_BackApply_Product WHERE Orders_BackApply_Product_ApplyID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<OrdersBackApplyProductInfo>();
                    while (RdrList.Read())
                    {
                        entity = new OrdersBackApplyProductInfo();

                        entity.Orders_BackApply_Product_ID = Tools.NullInt(RdrList["Orders_BackApply_Product_ID"]);
                        entity.Orders_BackApply_Product_ProductID = Tools.NullInt(RdrList["Orders_BackApply_Product_ProductID"]);
                        entity.Orders_BackApply_Product_ApplyID = Tools.NullInt(RdrList["Orders_BackApply_Product_ApplyID"]);
                        entity.Orders_BackApply_Product_ApplyAmount = Tools.NullInt(RdrList["Orders_BackApply_Product_ApplyAmount"]);
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
                SqlTable = "Orders_BackApply_Product";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Orders_BackApply_Product_ID) FROM " + SqlTable + SqlParam;

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
