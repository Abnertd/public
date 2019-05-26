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
    public class OrdersGoodsTmp : IOrdersGoodsTmp
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public OrdersGoodsTmp()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddOrdersGoodsTmp(OrdersGoodsTmpInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Orders_Goods_tmp";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Orders_Goods_ID"] = entity.Orders_Goods_ID;
            DrAdd["Orders_Goods_Type"] = entity.Orders_Goods_Type;
            DrAdd["Orders_Goods_BuyerID"] = entity.Orders_Goods_BuyerID;
            DrAdd["Orders_Goods_SessionID"] = entity.Orders_Goods_SessionID;
            DrAdd["Orders_Goods_ParentID"] = entity.Orders_Goods_ParentID;
            DrAdd["Orders_Goods_Product_ID"] = entity.Orders_Goods_Product_ID;
            DrAdd["Orders_Goods_Product_SupplierID"] = entity.Orders_Goods_Product_SupplierID;
            DrAdd["Orders_Goods_Product_Code"] = entity.Orders_Goods_Product_Code;
            DrAdd["Orders_Goods_Product_CateID"] = entity.Orders_Goods_Product_CateID;
            DrAdd["Orders_Goods_Product_BrandID"] = entity.Orders_Goods_Product_BrandID;
            DrAdd["Orders_Goods_Product_Name"] = entity.Orders_Goods_Product_Name;
            DrAdd["Orders_Goods_Product_Img"] = entity.Orders_Goods_Product_Img;
            DrAdd["Orders_Goods_Product_Price"] = entity.Orders_Goods_Product_Price;
            DrAdd["Orders_Goods_Product_MKTPrice"] = entity.Orders_Goods_Product_MKTPrice;
            DrAdd["Orders_Goods_Product_Maker"] = entity.Orders_Goods_Product_Maker;
            DrAdd["Orders_Goods_Product_Spec"] = entity.Orders_Goods_Product_Spec;
            DrAdd["Orders_Goods_Product_DeliveryDate"] = entity.Orders_Goods_Product_DeliveryDate;
            DrAdd["Orders_Goods_Product_AuthorizeCode"] = entity.Orders_Goods_Product_AuthorizeCode;
            DrAdd["Orders_Goods_Product_brokerage"] = entity.Orders_Goods_Product_brokerage;
            DrAdd["Orders_Goods_Product_SalePrice"] = entity.Orders_Goods_Product_SalePrice;
            DrAdd["Orders_Goods_Product_PurchasingPrice"] = entity.Orders_Goods_Product_PurchasingPrice;
            DrAdd["Orders_Goods_Product_Coin"] = entity.Orders_Goods_Product_Coin;
            DrAdd["Orders_Goods_Product_IsFavor"] = entity.Orders_Goods_Product_IsFavor;
            DrAdd["Orders_Goods_Product_UseCoin"] = entity.Orders_Goods_Product_UseCoin;
            DrAdd["Orders_Goods_Amount"] = entity.Orders_Goods_Amount;
            DrAdd["Orders_Goods_Addtime"] = entity.Orders_Goods_Addtime;
            DrAdd["Orders_Goods_OrdersID"] = entity.Orders_Goods_OrdersID;

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

        public virtual bool EditOrdersGoodsTmp(OrdersGoodsTmpInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Orders_Goods_tmp WHERE Orders_Goods_ID = " + entity.Orders_Goods_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Orders_Goods_ID"] = entity.Orders_Goods_ID;
                    DrAdd["Orders_Goods_Type"] = entity.Orders_Goods_Type;
                    DrAdd["Orders_Goods_BuyerID"] = entity.Orders_Goods_BuyerID;
                    DrAdd["Orders_Goods_SessionID"] = entity.Orders_Goods_SessionID;
                    DrAdd["Orders_Goods_ParentID"] = entity.Orders_Goods_ParentID;
                    DrAdd["Orders_Goods_Product_ID"] = entity.Orders_Goods_Product_ID;
                    DrAdd["Orders_Goods_Product_SupplierID"] = entity.Orders_Goods_Product_SupplierID;
                    DrAdd["Orders_Goods_Product_Code"] = entity.Orders_Goods_Product_Code;
                    DrAdd["Orders_Goods_Product_CateID"] = entity.Orders_Goods_Product_CateID;
                    DrAdd["Orders_Goods_Product_BrandID"] = entity.Orders_Goods_Product_BrandID;
                    DrAdd["Orders_Goods_Product_Name"] = entity.Orders_Goods_Product_Name;
                    DrAdd["Orders_Goods_Product_Img"] = entity.Orders_Goods_Product_Img;
                    DrAdd["Orders_Goods_Product_Price"] = entity.Orders_Goods_Product_Price;
                    DrAdd["Orders_Goods_Product_MKTPrice"] = entity.Orders_Goods_Product_MKTPrice;
                    DrAdd["Orders_Goods_Product_Maker"] = entity.Orders_Goods_Product_Maker;
                    DrAdd["Orders_Goods_Product_Spec"] = entity.Orders_Goods_Product_Spec;
                    DrAdd["Orders_Goods_Product_DeliveryDate"] = entity.Orders_Goods_Product_DeliveryDate;
                    DrAdd["Orders_Goods_Product_AuthorizeCode"] = entity.Orders_Goods_Product_AuthorizeCode;
                    DrAdd["Orders_Goods_Product_brokerage"] = entity.Orders_Goods_Product_brokerage;
                    DrAdd["Orders_Goods_Product_SalePrice"] = entity.Orders_Goods_Product_SalePrice;
                    DrAdd["Orders_Goods_Product_PurchasingPrice"] = entity.Orders_Goods_Product_PurchasingPrice;
                    DrAdd["Orders_Goods_Product_Coin"] = entity.Orders_Goods_Product_Coin;
                    DrAdd["Orders_Goods_Product_IsFavor"] = entity.Orders_Goods_Product_IsFavor;
                    DrAdd["Orders_Goods_Product_UseCoin"] = entity.Orders_Goods_Product_UseCoin;
                    DrAdd["Orders_Goods_Amount"] = entity.Orders_Goods_Amount;
                    DrAdd["Orders_Goods_Addtime"] = entity.Orders_Goods_Addtime;
                    DrAdd["Orders_Goods_OrdersID"] = entity.Orders_Goods_OrdersID;

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

        public virtual int DelOrdersGoodsTmp(int ID,int goods_type,int parent_id,string sessionid)
        {
            string SqlAdd = "";
            if (parent_id == 0)
            {
                SqlAdd = "DELETE FROM Orders_Goods_tmp WHERE Orders_Goods_SessionID='" + sessionid + "' and Orders_Goods_Type=" + goods_type + " and Orders_Goods_ParentID=" + parent_id + " and Orders_Goods_ID = " + ID;
            }
            else
            {
                SqlAdd = "DELETE FROM Orders_Goods_tmp WHERE Orders_Goods_SessionID='" + sessionid + "' and Orders_Goods_Type=" + goods_type + " and Orders_Goods_ParentID=" + parent_id + "";
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

        public virtual int ClearOrdersGoodsTmp(string sessionid)
        {
            string SqlAdd = "";
            SqlAdd = "DELETE FROM Orders_Goods_tmp WHERE Orders_Goods_SessionID='" + sessionid + "'";

            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual int ClearOrdersGoodsTmp(string sessionid, int supplyid)
        {
            string SqlAdd = "";
            SqlAdd = "DELETE FROM Orders_Goods_tmp WHERE Orders_Goods_SessionID='" + sessionid + "'";
            SqlAdd += "AND Orders_Goods_Product_SupplierID=" + supplyid;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual int ClearOrdersGoodsTmpByOrdersID(int Orders_ID)
        {
            string SqlAdd = "";
            SqlAdd = "DELETE FROM Orders_Goods_tmp WHERE Orders_Goods_OrdersID=" + Orders_ID + "";

            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual int ClearOrdersGoodsTmpByGoodsID(string Goods_IDs,int supplier_id)
        {
            string SqlAdd = "";
            SqlAdd = "DELETE FROM Orders_Goods_tmp WHERE Orders_Goods_ID in (" + Goods_IDs + ") and Orders_Goods_Product_SupplierID =" + supplier_id;

            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual int ClearOrdersGoodsTmpByGoodsID(string Goods_IDs)
        {
            string SqlAdd = "";
            SqlAdd = "DELETE FROM Orders_Goods_tmp WHERE Orders_Goods_ID in (" + Goods_IDs + ") ";

            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual OrdersGoodsTmpInfo GetOrdersGoodsTmpByID(int ID)
        {
            OrdersGoodsTmpInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Orders_Goods_tmp WHERE Orders_Goods_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new OrdersGoodsTmpInfo();

                    entity.Orders_Goods_ID = Tools.NullInt(RdrList["Orders_Goods_ID"]);
                    entity.Orders_Goods_Type = Tools.NullInt(RdrList["Orders_Goods_Type"]);
                    entity.Orders_Goods_BuyerID = Tools.NullInt(RdrList["Orders_Goods_BuyerID"]);
                    entity.Orders_Goods_SessionID = Tools.NullStr(RdrList["Orders_Goods_SessionID"]);
                    entity.Orders_Goods_ParentID = Tools.NullInt(RdrList["Orders_Goods_ParentID"]);
                    entity.Orders_Goods_Product_ID = Tools.NullInt(RdrList["Orders_Goods_Product_ID"]);
                    entity.Orders_Goods_Product_SupplierID = Tools.NullInt(RdrList["Orders_Goods_Product_SupplierID"]);
                    entity.Orders_Goods_Product_Code = Tools.NullStr(RdrList["Orders_Goods_Product_Code"]);
                    entity.Orders_Goods_Product_CateID = Tools.NullInt(RdrList["Orders_Goods_Product_CateID"]);
                    entity.Orders_Goods_Product_BrandID = Tools.NullInt(RdrList["Orders_Goods_Product_BrandID"]);
                    entity.Orders_Goods_Product_Name = Tools.NullStr(RdrList["Orders_Goods_Product_Name"]);
                    entity.Orders_Goods_Product_Img = Tools.NullStr(RdrList["Orders_Goods_Product_Img"]);
                    entity.Orders_Goods_Product_Price = Tools.NullDbl(RdrList["Orders_Goods_Product_Price"]);
                    entity.Orders_Goods_Product_MKTPrice = Tools.NullDbl(RdrList["Orders_Goods_Product_MKTPrice"]);
                    entity.Orders_Goods_Product_Maker = Tools.NullStr(RdrList["Orders_Goods_Product_Maker"]);
                    entity.Orders_Goods_Product_Spec = Tools.NullStr(RdrList["Orders_Goods_Product_Spec"]);
                    entity.Orders_Goods_Product_DeliveryDate = Tools.NullStr(RdrList["Orders_Goods_Product_DeliveryDate"]);
                    entity.Orders_Goods_Product_AuthorizeCode = Tools.NullStr(RdrList["Orders_Goods_Product_AuthorizeCode"]);
                    entity.Orders_Goods_Product_brokerage = Tools.NullDbl(RdrList["Orders_Goods_Product_brokerage"]);
                    entity.Orders_Goods_Product_SalePrice = Tools.NullDbl(RdrList["Orders_Goods_Product_SalePrice"]);
                    entity.Orders_Goods_Product_PurchasingPrice = Tools.NullDbl(RdrList["Orders_Goods_Product_PurchasingPrice"]);
                    entity.Orders_Goods_Product_Coin = Tools.NullInt(RdrList["Orders_Goods_Product_Coin"]);
                    entity.Orders_Goods_Product_IsFavor = Tools.NullInt(RdrList["Orders_Goods_Product_IsFavor"]);
                    entity.Orders_Goods_Product_UseCoin = Tools.NullInt(RdrList["Orders_Goods_Product_UseCoin"]);
                    entity.Orders_Goods_Amount = Tools.NullInt(RdrList["Orders_Goods_Amount"]);
                    entity.Orders_Goods_Addtime = Tools.NullDate(RdrList["Orders_Goods_Addtime"]);
                    entity.Orders_Goods_OrdersID = Tools.NullInt(RdrList["Orders_Goods_OrdersID"]);

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

        public virtual IList<OrdersGoodsTmpInfo> GetOrdersGoodsTmps(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<OrdersGoodsTmpInfo> entitys = null;
            OrdersGoodsTmpInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Orders_Goods_tmp";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<OrdersGoodsTmpInfo>();
                    while (RdrList.Read())
                    {
                        entity = new OrdersGoodsTmpInfo();
                        entity.Orders_Goods_ID = Tools.NullInt(RdrList["Orders_Goods_ID"]);
                        entity.Orders_Goods_Type = Tools.NullInt(RdrList["Orders_Goods_Type"]);
                        entity.Orders_Goods_BuyerID = Tools.NullInt(RdrList["Orders_Goods_BuyerID"]);
                        entity.Orders_Goods_SessionID = Tools.NullStr(RdrList["Orders_Goods_SessionID"]);
                        entity.Orders_Goods_ParentID = Tools.NullInt(RdrList["Orders_Goods_ParentID"]);
                        entity.Orders_Goods_Product_ID = Tools.NullInt(RdrList["Orders_Goods_Product_ID"]);
                        entity.Orders_Goods_Product_SupplierID = Tools.NullInt(RdrList["Orders_Goods_Product_SupplierID"]);
                        entity.Orders_Goods_Product_Code = Tools.NullStr(RdrList["Orders_Goods_Product_Code"]);
                        entity.Orders_Goods_Product_CateID = Tools.NullInt(RdrList["Orders_Goods_Product_CateID"]);
                        entity.Orders_Goods_Product_BrandID = Tools.NullInt(RdrList["Orders_Goods_Product_BrandID"]);
                        entity.Orders_Goods_Product_Name = Tools.NullStr(RdrList["Orders_Goods_Product_Name"]);
                        entity.Orders_Goods_Product_Img = Tools.NullStr(RdrList["Orders_Goods_Product_Img"]);
                        entity.Orders_Goods_Product_Price = Tools.NullDbl(RdrList["Orders_Goods_Product_Price"]);
                        entity.Orders_Goods_Product_MKTPrice = Tools.NullDbl(RdrList["Orders_Goods_Product_MKTPrice"]);
                        entity.Orders_Goods_Product_Maker = Tools.NullStr(RdrList["Orders_Goods_Product_Maker"]);
                        entity.Orders_Goods_Product_Spec = Tools.NullStr(RdrList["Orders_Goods_Product_Spec"]);
                        entity.Orders_Goods_Product_DeliveryDate = Tools.NullStr(RdrList["Orders_Goods_Product_DeliveryDate"]);
                        entity.Orders_Goods_Product_AuthorizeCode = Tools.NullStr(RdrList["Orders_Goods_Product_AuthorizeCode"]);
                        entity.Orders_Goods_Product_brokerage = Tools.NullDbl(RdrList["Orders_Goods_Product_brokerage"]);
                        entity.Orders_Goods_Product_SalePrice = Tools.NullDbl(RdrList["Orders_Goods_Product_SalePrice"]);
                        entity.Orders_Goods_Product_PurchasingPrice = Tools.NullDbl(RdrList["Orders_Goods_Product_PurchasingPrice"]);
                        entity.Orders_Goods_Product_Coin = Tools.NullInt(RdrList["Orders_Goods_Product_Coin"]);
                        entity.Orders_Goods_Product_IsFavor = Tools.NullInt(RdrList["Orders_Goods_Product_IsFavor"]);
                        entity.Orders_Goods_Product_UseCoin = Tools.NullInt(RdrList["Orders_Goods_Product_UseCoin"]);
                        entity.Orders_Goods_Amount = Tools.NullInt(RdrList["Orders_Goods_Amount"]);
                        entity.Orders_Goods_Addtime = Tools.NullDate(RdrList["Orders_Goods_Addtime"]);
                        entity.Orders_Goods_OrdersID = Tools.NullInt(RdrList["Orders_Goods_OrdersID"]);

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

        public virtual IList<OrdersGoodsTmpInfo> GetOrdersGoodsTmpsByOrdersID(int Orders_ID)
        {
            IList<OrdersGoodsTmpInfo> entitys = null;
            OrdersGoodsTmpInfo entity = null;
            string SqlList;
            SqlDataReader RdrList = null;
            try
            {
                SqlList = "Select * from Orders_goods_Tmp where Orders_Goods_OrdersID=" + Orders_ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<OrdersGoodsTmpInfo>();
                    while (RdrList.Read())
                    {
                        entity = new OrdersGoodsTmpInfo();
                        entity.Orders_Goods_ID = Tools.NullInt(RdrList["Orders_Goods_ID"]);
                        entity.Orders_Goods_Type = Tools.NullInt(RdrList["Orders_Goods_Type"]);
                        entity.Orders_Goods_BuyerID = Tools.NullInt(RdrList["Orders_Goods_BuyerID"]);
                        entity.Orders_Goods_SessionID = Tools.NullStr(RdrList["Orders_Goods_SessionID"]);
                        entity.Orders_Goods_ParentID = Tools.NullInt(RdrList["Orders_Goods_ParentID"]);
                        entity.Orders_Goods_Product_ID = Tools.NullInt(RdrList["Orders_Goods_Product_ID"]);
                        entity.Orders_Goods_Product_SupplierID = Tools.NullInt(RdrList["Orders_Goods_Product_SupplierID"]);
                        entity.Orders_Goods_Product_Code = Tools.NullStr(RdrList["Orders_Goods_Product_Code"]);
                        entity.Orders_Goods_Product_CateID = Tools.NullInt(RdrList["Orders_Goods_Product_CateID"]);
                        entity.Orders_Goods_Product_BrandID = Tools.NullInt(RdrList["Orders_Goods_Product_BrandID"]);
                        entity.Orders_Goods_Product_Name = Tools.NullStr(RdrList["Orders_Goods_Product_Name"]);
                        entity.Orders_Goods_Product_Img = Tools.NullStr(RdrList["Orders_Goods_Product_Img"]);
                        entity.Orders_Goods_Product_Price = Tools.NullDbl(RdrList["Orders_Goods_Product_Price"]);
                        entity.Orders_Goods_Product_MKTPrice = Tools.NullDbl(RdrList["Orders_Goods_Product_MKTPrice"]);
                        entity.Orders_Goods_Product_Maker = Tools.NullStr(RdrList["Orders_Goods_Product_Maker"]);
                        entity.Orders_Goods_Product_Spec = Tools.NullStr(RdrList["Orders_Goods_Product_Spec"]);
                        entity.Orders_Goods_Product_DeliveryDate = Tools.NullStr(RdrList["Orders_Goods_Product_DeliveryDate"]);
                        entity.Orders_Goods_Product_AuthorizeCode = Tools.NullStr(RdrList["Orders_Goods_Product_AuthorizeCode"]);
                        entity.Orders_Goods_Product_brokerage = Tools.NullDbl(RdrList["Orders_Goods_Product_brokerage"]);
                        entity.Orders_Goods_Product_SalePrice = Tools.NullDbl(RdrList["Orders_Goods_Product_SalePrice"]);
                        entity.Orders_Goods_Product_PurchasingPrice = Tools.NullDbl(RdrList["Orders_Goods_Product_PurchasingPrice"]);
                        entity.Orders_Goods_Product_Coin = Tools.NullInt(RdrList["Orders_Goods_Product_Coin"]);
                        entity.Orders_Goods_Product_IsFavor = Tools.NullInt(RdrList["Orders_Goods_Product_IsFavor"]);
                        entity.Orders_Goods_Product_UseCoin = Tools.NullInt(RdrList["Orders_Goods_Product_UseCoin"]);
                        entity.Orders_Goods_Amount = Tools.NullInt(RdrList["Orders_Goods_Amount"]);
                        entity.Orders_Goods_Addtime = Tools.NullDate(RdrList["Orders_Goods_Addtime"]);
                        entity.Orders_Goods_OrdersID = Tools.NullInt(RdrList["Orders_Goods_OrdersID"]);

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
                SqlTable = "Orders_Goods_tmp";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Orders_Goods_ID) FROM " + SqlTable + SqlParam;

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

        public virtual int Get_Orders_Goods_Amount(string sessionid, int product_id)
        {
            string SqlAdd = "Select SUM(Orders_Goods_Amount) FROM Orders_Goods_tmp WHERE Orders_Goods_SessionID='" + sessionid + "' and (Orders_Goods_Type=0 or Orders_Goods_Type=3 or Orders_Goods_Type=1 or (Orders_Goods_Type=2 and Orders_Goods_ParentID>0)) and Orders_Goods_Product_ID = " + product_id;
            try
            {
                return Tools.NullInt(DBHelper.ExecuteScalar(SqlAdd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual int Get_Orders_Goods_TypeAmount(string sessionid, int product_id, int product_type)
        {
            string SqlAdd = "Select SUM(Orders_Goods_Amount) FROM Orders_Goods_tmp WHERE Orders_Goods_SessionID='" + sessionid + "' and (Orders_Goods_Type=" + product_type + " and Orders_Goods_ParentID=0) and Orders_Goods_Product_ID = " + product_id;
            try
            {
                return Tools.NullInt(DBHelper.ExecuteScalar(SqlAdd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual int Get_Orders_Goods_PackageAmount(string sessionid, int package_id)
        {
            string SqlAdd = "Select SUM(Orders_Goods_Amount) FROM Orders_Goods_tmp WHERE Orders_Goods_SessionID='" + sessionid + "' and (Orders_Goods_Type=2 and Orders_Goods_ParentID=0) and Orders_Goods_Product_ID = " + package_id;
            try
            {
                return Tools.NullInt(DBHelper.ExecuteScalar(SqlAdd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual int Get_Orders_Goods_ParentID(string sessionid, int product_id,int product_type)
        {
            string SqlAdd = "Select Orders_Goods_ID FROM Orders_Goods_tmp WHERE Orders_Goods_SessionID='" + sessionid + "' and (Orders_Goods_Type=" + product_type + " and Orders_Goods_ParentID=0) and Orders_Goods_Product_ID = " + product_id + " order by Orders_Goods_ID desc";
            try
            {
                return Tools.NullInt(DBHelper.ExecuteScalar(SqlAdd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual string GetOrdersGoodsTmpSupplierID(string sessionid, int buyer_id)
        {
            string SqlList = "", strSupplierID = "";
            SqlDataReader RdrList = null;

            //SqlList = "select distinct Orders_Goods_Product_SupplierID from Orders_Goods_tmp where  1=1 ";
            SqlList = "select distinct Orders_Goods_Product_SupplierID from Orders_Goods_tmp where  Orders_Goods_SessionID='" + sessionid + "'";

            if (buyer_id > 0)
            {
                SqlList = SqlList + " and Orders_Goods_BuyerID=" + buyer_id;
            }
            else
            {
                SqlList = SqlList + " and Orders_Goods_SessionID='" + sessionid + "'";
            }
            try
            {
                RdrList = DBHelper.ExecuteReader(SqlList);
                while (RdrList.Read())
                {
                    strSupplierID += Tools.NullStr(RdrList["Orders_Goods_Product_SupplierID"]) + ",";
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
            return strSupplierID.TrimEnd(',');
        }




        #region   

        #endregion
    }
}
