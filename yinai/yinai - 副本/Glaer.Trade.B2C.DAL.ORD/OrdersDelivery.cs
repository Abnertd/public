using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.Util.Tools;

namespace Glaer.Trade.B2C.DAL.ORD
{
    public class OrdersDelivery : IOrdersDelivery
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public OrdersDelivery()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddOrdersDelivery(OrdersDeliveryInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Orders_Delivery";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();
            DrAdd["Orders_Delivery_ID"] = entity.Orders_Delivery_ID;
            DrAdd["Orders_Delivery_OrdersID"] = entity.Orders_Delivery_OrdersID;
            DrAdd["Orders_Delivery_DeliveryStatus"] = entity.Orders_Delivery_DeliveryStatus;
            DrAdd["Orders_Delivery_SysUserID"] = entity.Orders_Delivery_SysUserID;
            DrAdd["Orders_Delivery_DocNo"] = entity.Orders_Delivery_DocNo;
            DrAdd["Orders_Delivery_Name"] = entity.Orders_Delivery_Name;
            DrAdd["Orders_Delivery_companyName"] = entity.Orders_Delivery_companyName;
            DrAdd["Orders_Delivery_Code"] = entity.Orders_Delivery_Code;
            DrAdd["Orders_Delivery_Amount"] = entity.Orders_Delivery_Amount;
            DrAdd["Orders_Delivery_Note"] = entity.Orders_Delivery_Note;
            DrAdd["Orders_Delivery_ReceiveStatus"] = entity.Orders_Delivery_ReceiveStatus;
            DrAdd["Orders_Delivery_Addtime"] = entity.Orders_Delivery_Addtime;
            DrAdd["Orders_Delivery_Site"] = entity.Orders_Delivery_Site;
            //运输单详情新加字段
            DrAdd["Orders_Delivery_DriverMobile"] = entity.Orders_Delivery_DriverMobile;
            DrAdd["Orders_Delivery_PlateNum"] = entity.Orders_Delivery_PlateNum;
            DrAdd["Orders_Delivery_TransportType"] = entity.Orders_Delivery_TransportType;





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

        public virtual bool EditOrdersDelivery(OrdersDeliveryInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Orders_Delivery WHERE Orders_Delivery_ID = " + entity.Orders_Delivery_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try {
                if (DtAdd.Rows.Count > 0) {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Orders_Delivery_ID"] = entity.Orders_Delivery_ID;
                    DrAdd["Orders_Delivery_OrdersID"] = entity.Orders_Delivery_OrdersID;
                    DrAdd["Orders_Delivery_DeliveryStatus"] = entity.Orders_Delivery_DeliveryStatus;
                    DrAdd["Orders_Delivery_SysUserID"] = entity.Orders_Delivery_SysUserID;
                    DrAdd["Orders_Delivery_DocNo"] = entity.Orders_Delivery_DocNo;
                    DrAdd["Orders_Delivery_Name"] = entity.Orders_Delivery_Name;
                    DrAdd["Orders_Delivery_companyName"] = entity.Orders_Delivery_companyName;
                    DrAdd["Orders_Delivery_Code"] = entity.Orders_Delivery_Code;
                    DrAdd["Orders_Delivery_Amount"] = entity.Orders_Delivery_Amount;
                    DrAdd["Orders_Delivery_Note"] = entity.Orders_Delivery_Note;
                    DrAdd["Orders_Delivery_Addtime"] = entity.Orders_Delivery_Addtime;
                    DrAdd["Orders_Delivery_Site"] = entity.Orders_Delivery_Site;
                    DrAdd["Orders_Delivery_ReceiveStatus"] = entity.Orders_Delivery_ReceiveStatus;
                    //运输单详情新加字段  
                    DrAdd["Orders_Delivery_DriverMobile"] = entity.Orders_Delivery_DriverMobile;
                    DrAdd["Orders_Delivery_PlateNum"] = entity.Orders_Delivery_PlateNum;
                    DrAdd["Orders_Delivery_TransportType"] = entity.Orders_Delivery_TransportType;
                    DBHelper.SaveChanges(SqlAdd, DtAdd);
                }
                else {
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

        public virtual int DelOrdersDelivery(int ID)
        {
            string SqlAdd = "DELETE FROM Orders_Delivery WHERE Orders_Delivery_ID = " + ID;
            try {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public virtual OrdersDeliveryInfo GetOrdersDeliveryByID(int ID)
        {
            OrdersDeliveryInfo entity = null;
            SqlDataReader RdrList = null;
            try {
                string SqlList;
                SqlList = "SELECT * FROM Orders_Delivery WHERE Orders_Delivery_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read()) {
                    entity = new OrdersDeliveryInfo();
                    entity.Orders_Delivery_ID = Tools.NullInt(RdrList["Orders_Delivery_ID"]);
                    entity.Orders_Delivery_OrdersID = Tools.NullInt(RdrList["Orders_Delivery_OrdersID"]);
                    entity.Orders_Delivery_DeliveryStatus = Tools.NullInt(RdrList["Orders_Delivery_DeliveryStatus"]);
                    entity.Orders_Delivery_SysUserID = Tools.NullInt(RdrList["Orders_Delivery_SysUserID"]);
                    entity.Orders_Delivery_DocNo = Tools.NullStr(RdrList["Orders_Delivery_DocNo"]);
                    entity.Orders_Delivery_Name = Tools.NullStr(RdrList["Orders_Delivery_Name"]);
                    entity.Orders_Delivery_companyName = Tools.NullStr(RdrList["Orders_Delivery_companyName"]);
                    entity.Orders_Delivery_Code = Tools.NullStr(RdrList["Orders_Delivery_Code"]);
                    entity.Orders_Delivery_Amount = Tools.NullDbl(RdrList["Orders_Delivery_Amount"]);
                    entity.Orders_Delivery_Note = Tools.NullStr(RdrList["Orders_Delivery_Note"]);
                    entity.Orders_Delivery_ReceiveStatus = Tools.NullInt(RdrList["Orders_Delivery_ReceiveStatus"]);
                    entity.Orders_Delivery_Addtime = Tools.NullDate(RdrList["Orders_Delivery_Addtime"]);
                    entity.Orders_Delivery_Site = Tools.NullStr(RdrList["Orders_Delivery_Site"]);

                    //运输单详情新加字段  
                    entity.Orders_Delivery_DriverMobile = Tools.NullStr(RdrList["Orders_Delivery_DriverMobile"]);
                    entity.Orders_Delivery_PlateNum = Tools.NullStr(RdrList["Orders_Delivery_PlateNum"]);
                    entity.Orders_Delivery_TransportType = Tools.NullStr(RdrList["Orders_Delivery_TransportType"]);
                }
                return entity;
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                if (RdrList != null) {
                    RdrList.Close();
                    RdrList = null;
                }
            }
        }

        public virtual OrdersDeliveryInfo GetOrdersDeliveryBySn(string SN)
        {
            OrdersDeliveryInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Orders_Delivery WHERE Orders_Delivery_DocNo = '" + SN + "'";
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new OrdersDeliveryInfo();
                    entity.Orders_Delivery_ID = Tools.NullInt(RdrList["Orders_Delivery_ID"]);
                    entity.Orders_Delivery_OrdersID = Tools.NullInt(RdrList["Orders_Delivery_OrdersID"]);
                    entity.Orders_Delivery_DeliveryStatus = Tools.NullInt(RdrList["Orders_Delivery_DeliveryStatus"]);
                    entity.Orders_Delivery_SysUserID = Tools.NullInt(RdrList["Orders_Delivery_SysUserID"]);
                    entity.Orders_Delivery_DocNo = Tools.NullStr(RdrList["Orders_Delivery_DocNo"]);
                    entity.Orders_Delivery_Name = Tools.NullStr(RdrList["Orders_Delivery_Name"]);
                    entity.Orders_Delivery_companyName = Tools.NullStr(RdrList["Orders_Delivery_companyName"]);
                    entity.Orders_Delivery_Code = Tools.NullStr(RdrList["Orders_Delivery_Code"]);
                    entity.Orders_Delivery_Amount = Tools.NullDbl(RdrList["Orders_Delivery_Amount"]);
                    entity.Orders_Delivery_Note = Tools.NullStr(RdrList["Orders_Delivery_Note"]);
                    entity.Orders_Delivery_ReceiveStatus = Tools.NullInt(RdrList["Orders_Delivery_ReceiveStatus"]);
                    entity.Orders_Delivery_Addtime = Tools.NullDate(RdrList["Orders_Delivery_Addtime"]);
                    entity.Orders_Delivery_Site = Tools.NullStr(RdrList["Orders_Delivery_Site"]);


                    //运输单详情新加字段  
                    entity.Orders_Delivery_DriverMobile = Tools.NullStr(RdrList["Orders_Delivery_DriverMobile"]);
                    entity.Orders_Delivery_PlateNum = Tools.NullStr(RdrList["Orders_Delivery_PlateNum"]);
                    entity.Orders_Delivery_TransportType = Tools.NullStr(RdrList["Orders_Delivery_TransportType"]);
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

        public virtual OrdersDeliveryInfo GetOrdersDeliveryByOrdersID(int Orders_ID, int Delivery_Status)
        {
            OrdersDeliveryInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Orders_Delivery WHERE Orders_Delivery_OrdersID = " + Orders_ID + "";
                if (Delivery_Status > 0)
                {
                    SqlList += " AND Orders_Delivery_DeliveryStatus=" + Delivery_Status;
                }
                SqlList += " ORDER BY Orders_Delivery_ID DESC";
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new OrdersDeliveryInfo();
                    entity.Orders_Delivery_ID = Tools.NullInt(RdrList["Orders_Delivery_ID"]);
                    entity.Orders_Delivery_OrdersID = Tools.NullInt(RdrList["Orders_Delivery_OrdersID"]);
                    entity.Orders_Delivery_DeliveryStatus = Tools.NullInt(RdrList["Orders_Delivery_DeliveryStatus"]);
                    entity.Orders_Delivery_SysUserID = Tools.NullInt(RdrList["Orders_Delivery_SysUserID"]);
                    entity.Orders_Delivery_DocNo = Tools.NullStr(RdrList["Orders_Delivery_DocNo"]);
                    entity.Orders_Delivery_Name = Tools.NullStr(RdrList["Orders_Delivery_Name"]);
                    entity.Orders_Delivery_companyName = Tools.NullStr(RdrList["Orders_Delivery_companyName"]);
                    entity.Orders_Delivery_Code = Tools.NullStr(RdrList["Orders_Delivery_Code"]);
                    entity.Orders_Delivery_Amount = Tools.NullDbl(RdrList["Orders_Delivery_Amount"]);
                    entity.Orders_Delivery_Note = Tools.NullStr(RdrList["Orders_Delivery_Note"]);
                    entity.Orders_Delivery_ReceiveStatus = Tools.NullInt(RdrList["Orders_Delivery_ReceiveStatus"]);
                    entity.Orders_Delivery_Addtime = Tools.NullDate(RdrList["Orders_Delivery_Addtime"]);
                    entity.Orders_Delivery_Site = Tools.NullStr(RdrList["Orders_Delivery_Site"]);

                    //运输单详情新加字段  
                    entity.Orders_Delivery_DriverMobile = Tools.NullStr(RdrList["Orders_Delivery_DriverMobile"]);

                    entity.Orders_Delivery_PlateNum = Tools.NullStr(RdrList["Orders_Delivery_PlateNum"]);
                    entity.Orders_Delivery_TransportType = Tools.NullStr(RdrList["Orders_Delivery_TransportType"]);
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

        public virtual IList<OrdersDeliveryInfo> GetOrdersDeliverys(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<OrdersDeliveryInfo> entitys = null;
            OrdersDeliveryInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Orders_Delivery";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows) {
                    entitys = new List<OrdersDeliveryInfo>();
                    while (RdrList.Read()) {
                        entity = new OrdersDeliveryInfo();
                        entity.Orders_Delivery_ID = Tools.NullInt(RdrList["Orders_Delivery_ID"]);
                        entity.Orders_Delivery_OrdersID = Tools.NullInt(RdrList["Orders_Delivery_OrdersID"]);
                        entity.Orders_Delivery_DeliveryStatus = Tools.NullInt(RdrList["Orders_Delivery_DeliveryStatus"]);
                        entity.Orders_Delivery_SysUserID = Tools.NullInt(RdrList["Orders_Delivery_SysUserID"]);
                        entity.Orders_Delivery_DocNo = Tools.NullStr(RdrList["Orders_Delivery_DocNo"]);
                        entity.Orders_Delivery_Name = Tools.NullStr(RdrList["Orders_Delivery_Name"]);
                        entity.Orders_Delivery_companyName = Tools.NullStr(RdrList["Orders_Delivery_companyName"]);
                        entity.Orders_Delivery_Code = Tools.NullStr(RdrList["Orders_Delivery_Code"]);
                        entity.Orders_Delivery_Amount = Tools.NullDbl(RdrList["Orders_Delivery_Amount"]);
                        entity.Orders_Delivery_Note = Tools.NullStr(RdrList["Orders_Delivery_Note"]);
                        entity.Orders_Delivery_ReceiveStatus = Tools.NullInt(RdrList["Orders_Delivery_ReceiveStatus"]);
                        entity.Orders_Delivery_Addtime = Tools.NullDate(RdrList["Orders_Delivery_Addtime"]);
                        entity.Orders_Delivery_Site = Tools.NullStr(RdrList["Orders_Delivery_Site"]);

                        //运输单详情新加字段  
                        entity.Orders_Delivery_DriverMobile = Tools.NullStr(RdrList["Orders_Delivery_DriverMobile"]);
                        entity.Orders_Delivery_PlateNum = Tools.NullStr(RdrList["Orders_Delivery_PlateNum"]);
                        entity.Orders_Delivery_TransportType = Tools.NullStr(RdrList["Orders_Delivery_TransportType"]);
                        entitys.Add(entity);
                        entity = null;
                    }
                }
                return entitys;
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                if (RdrList != null) {
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

            try {
                Page = new PageInfo();
                SqlTable = "Orders_Delivery";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Orders_Delivery_ID) FROM " + SqlTable + SqlParam;

                RecordCount = Tools.NullInt(DBHelper.ExecuteScalar(SqlCount));
                PageCount = Tools.CalculatePages(RecordCount, Query.PageSize);
                CurrentPage = Tools.DeterminePage(Query.CurrentPage, PageCount);

                Page.RecordCount = RecordCount;
                Page.PageCount = PageCount;
                Page.CurrentPage = CurrentPage;
                Page.PageSize = Query.PageSize;

                return Page;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public virtual bool AddOrdersDeliveryGoods(IList<OrdersDeliveryGoodsInfo> entitys)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Orders_Delivery_Goods";
            DtAdd = DBHelper.Query(SqlAdd);
            foreach (OrdersDeliveryGoodsInfo entity in entitys)
            {

                DrAdd = DtAdd.NewRow();
                DrAdd["Orders_Delivery_Goods_ID"] = entity.Orders_Delivery_Goods_ID;
                DrAdd["Orders_Delivery_Goods_DeliveryID"] = entity.Orders_Delivery_Goods_DeliveryID;
                DrAdd["Orders_Delivery_Goods_GoodsID"] = entity.Orders_Delivery_Goods_GoodsID;
                DrAdd["Orders_Delivery_Goods_ProductID"] = entity.Orders_Delivery_Goods_ProductID;
                DrAdd["Orders_Delivery_Goods_ProductCateID"] = entity.Orders_Delivery_Goods_ProductCateID;
                DrAdd["Orders_Delivery_Goods_ProductCode"] = entity.Orders_Delivery_Goods_ProductCode;
                DrAdd["Orders_Delivery_Goods_ProductName"] = entity.Orders_Delivery_Goods_ProductName;
                DrAdd["Orders_Delivery_Goods_ProductImg"] = entity.Orders_Delivery_Goods_ProductImg;
                DrAdd["Orders_Delivery_Goods_ProductSpec"] = entity.Orders_Delivery_Goods_ProductSpec;
                DrAdd["Orders_Delivery_Goods_ProductPrice"] = entity.Orders_Delivery_Goods_ProductPrice;
                DrAdd["Orders_Delivery_Goods_ProductAmount"] = entity.Orders_Delivery_Goods_ProductAmount;
                DrAdd["Orders_Delivery_Goods_ReceivedAmount"] = entity.Orders_Delivery_Goods_ReceivedAmount;
                DrAdd["Orders_Delivery_Goods_brokerage"] = entity.Orders_Delivery_Goods_brokerage;
                DtAdd.Rows.Add(DrAdd);
            }
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

        public virtual bool EditOrdersDeliveryGoods(OrdersDeliveryGoodsInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Orders_Delivery_Goods WHERE Orders_Delivery_Goods_ID = " + entity.Orders_Delivery_Goods_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Orders_Delivery_Goods_ID"] = entity.Orders_Delivery_Goods_ID;
                    DrAdd["Orders_Delivery_Goods_DeliveryID"] = entity.Orders_Delivery_Goods_DeliveryID;
                    DrAdd["Orders_Delivery_Goods_GoodsID"] = entity.Orders_Delivery_Goods_GoodsID;
                    DrAdd["Orders_Delivery_Goods_ProductID"] = entity.Orders_Delivery_Goods_ProductID;
                    DrAdd["Orders_Delivery_Goods_ProductCateID"] = entity.Orders_Delivery_Goods_ProductCateID;
                    DrAdd["Orders_Delivery_Goods_ProductCode"] = entity.Orders_Delivery_Goods_ProductCode;
                    DrAdd["Orders_Delivery_Goods_ProductName"] = entity.Orders_Delivery_Goods_ProductName;
                    DrAdd["Orders_Delivery_Goods_ProductImg"] = entity.Orders_Delivery_Goods_ProductImg;
                    DrAdd["Orders_Delivery_Goods_ProductSpec"] = entity.Orders_Delivery_Goods_ProductSpec;
                    DrAdd["Orders_Delivery_Goods_ProductPrice"] = entity.Orders_Delivery_Goods_ProductPrice;
                    DrAdd["Orders_Delivery_Goods_ProductAmount"] = entity.Orders_Delivery_Goods_ProductAmount;
                    DrAdd["Orders_Delivery_Goods_ReceivedAmount"] = entity.Orders_Delivery_Goods_ReceivedAmount;
                    DrAdd["Orders_Delivery_Goods_brokerage"] = entity.Orders_Delivery_Goods_brokerage;

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

        public virtual int DelOrdersDeliveryGoods(int ID)
        {
            string SqlAdd = "DELETE FROM Orders_Delivery_Goods WHERE Orders_Delivery_Goods_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual OrdersDeliveryGoodsInfo GetOrdersDeliveryGoodsByID(int ID)
        {
            OrdersDeliveryGoodsInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Orders_Delivery_Goods WHERE Orders_Delivery_Goods_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new OrdersDeliveryGoodsInfo();

                    entity.Orders_Delivery_Goods_ID = Tools.NullInt(RdrList["Orders_Delivery_Goods_ID"]);
                    entity.Orders_Delivery_Goods_DeliveryID = Tools.NullInt(RdrList["Orders_Delivery_Goods_DeliveryID"]);
                    entity.Orders_Delivery_Goods_GoodsID = Tools.NullInt(RdrList["Orders_Delivery_Goods_GoodsID"]);
                    entity.Orders_Delivery_Goods_ProductID = Tools.NullInt(RdrList["Orders_Delivery_Goods_ProductID"]);
                    entity.Orders_Delivery_Goods_ProductCateID = Tools.NullInt(RdrList["Orders_Delivery_Goods_ProductCateID"]);
                    entity.Orders_Delivery_Goods_ProductCode = Tools.NullStr(RdrList["Orders_Delivery_Goods_ProductCode"]);
                    entity.Orders_Delivery_Goods_ProductName = Tools.NullStr(RdrList["Orders_Delivery_Goods_ProductName"]);
                    entity.Orders_Delivery_Goods_ProductImg = Tools.NullStr(RdrList["Orders_Delivery_Goods_ProductImg"]);
                    entity.Orders_Delivery_Goods_ProductSpec = Tools.NullStr(RdrList["Orders_Delivery_Goods_ProductSpec"]);
                    entity.Orders_Delivery_Goods_ProductPrice = Tools.NullDbl(RdrList["Orders_Delivery_Goods_ProductPrice"]);
                    entity.Orders_Delivery_Goods_ProductAmount = Tools.NullDbl(RdrList["Orders_Delivery_Goods_ProductAmount"]);
                    entity.Orders_Delivery_Goods_ReceivedAmount = Tools.NullDbl(RdrList["Orders_Delivery_Goods_ReceivedAmount"]);
                    entity.Orders_Delivery_Goods_brokerage = Tools.NullDbl(RdrList["Orders_Delivery_Goods_brokerage"]);
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

        public virtual IList<OrdersDeliveryGoodsInfo> GetOrdersDeliveryGoods(int DeliveryID)
        {
            IList<OrdersDeliveryGoodsInfo> entitys = null;
            OrdersDeliveryGoodsInfo entity = null;
            string SqlList;
            SqlDataReader RdrList = null;
            try
            {
                SqlList = "SELECT * FROM Orders_Delivery_Goods WHERE Orders_Delivery_Goods_DeliveryID = " + DeliveryID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<OrdersDeliveryGoodsInfo>();
                    while (RdrList.Read())
                    {
                        entity = new OrdersDeliveryGoodsInfo();
                        entity.Orders_Delivery_Goods_ID = Tools.NullInt(RdrList["Orders_Delivery_Goods_ID"]);
                        entity.Orders_Delivery_Goods_DeliveryID = Tools.NullInt(RdrList["Orders_Delivery_Goods_DeliveryID"]);
                        entity.Orders_Delivery_Goods_GoodsID = Tools.NullInt(RdrList["Orders_Delivery_Goods_GoodsID"]);
                        entity.Orders_Delivery_Goods_ProductID = Tools.NullInt(RdrList["Orders_Delivery_Goods_ProductID"]);
                        entity.Orders_Delivery_Goods_ProductCateID = Tools.NullInt(RdrList["Orders_Delivery_Goods_ProductCateID"]);
                        entity.Orders_Delivery_Goods_ProductCode = Tools.NullStr(RdrList["Orders_Delivery_Goods_ProductCode"]);
                        entity.Orders_Delivery_Goods_ProductName = Tools.NullStr(RdrList["Orders_Delivery_Goods_ProductName"]);
                        entity.Orders_Delivery_Goods_ProductImg = Tools.NullStr(RdrList["Orders_Delivery_Goods_ProductImg"]);
                        entity.Orders_Delivery_Goods_ProductSpec = Tools.NullStr(RdrList["Orders_Delivery_Goods_ProductSpec"]);
                        entity.Orders_Delivery_Goods_ProductPrice = Tools.NullDbl(RdrList["Orders_Delivery_Goods_ProductPrice"]);
                        entity.Orders_Delivery_Goods_ProductAmount = Tools.NullDbl(RdrList["Orders_Delivery_Goods_ProductAmount"]);
                        entity.Orders_Delivery_Goods_ReceivedAmount = Tools.NullDbl(RdrList["Orders_Delivery_Goods_ReceivedAmount"]);
                        entity.Orders_Delivery_Goods_brokerage = Tools.NullDbl(RdrList["Orders_Delivery_Goods_brokerage"]);

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

    }

}
