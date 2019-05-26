using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.Util.Tools;
using System.Collections;

namespace Glaer.Trade.B2C.DAL.ORD
{
    public class OrdersAccompanying : IOrdersAccompanying
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public OrdersAccompanying()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddOrdersAccompanying(OrdersAccompanyingInfo entity,string[] imgArr)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Orders_Accompanying";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Accompanying_ID"] = entity.Accompanying_ID;
            DrAdd["Accompanying_OrdersID"] = entity.Accompanying_OrdersID;
            DrAdd["Accompanying_DeliveryID"] = entity.Accompanying_DeliveryID;
            DrAdd["Accompanying_SN"] = entity.Accompanying_SN;
            DrAdd["Accompanying_Name"] = entity.Accompanying_Name;
            DrAdd["Accompanying_Amount"] = entity.Accompanying_Amount;
            DrAdd["Accompanying_Unit"] = entity.Accompanying_Unit;
            DrAdd["Accompanying_Price"] = entity.Accompanying_Price;
            DrAdd["Accompanying_Status"] = entity.Accompanying_Status;
            DrAdd["Accompanying_Addtime"] = entity.Accompanying_Addtime;
            DrAdd["Accompanying_Site"] = entity.Accompanying_Site;

            DtAdd.Rows.Add(DrAdd);
            try
            {
                DBHelper.SaveChanges(SqlAdd, DtAdd);

                entity.Accompanying_ID = GetLastOrdersAccompanyimg(entity.Accompanying_SN);

                SaveOrdersAccompanyingImg(entity.Accompanying_ID,imgArr);

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

        //保存商品对应图片
        private void SaveOrdersAccompanyingImg(int AccompanyingID, string[] extends) 
        {
            ArrayList sqlList = new ArrayList(extends.GetLength(0));
            DBHelper.ExecuteNonQuery("DELETE FROM Orders_Accompanying_Img WHERE Img_AccompanyingID =" + AccompanyingID);
            foreach (string imgPath in extends)
            {
                if (imgPath != "")
                    sqlList.Add("INSERT INTO Orders_Accompanying_Img (Img_AccompanyingID, Img_Path) VALUES (" + AccompanyingID + ", '" + imgPath + "')");
            }
            DBHelper.ExecuteNonQuery(sqlList);
            sqlList = null;
        }

        public virtual bool EditOrdersAccompanying(OrdersAccompanyingInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Orders_Accompanying WHERE Accompanying_ID = " + entity.Accompanying_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Accompanying_ID"] = entity.Accompanying_ID;
                    DrAdd["Accompanying_OrdersID"] = entity.Accompanying_OrdersID;
                    DrAdd["Accompanying_DeliveryID"] = entity.Accompanying_DeliveryID;
                    DrAdd["Accompanying_SN"] = entity.Accompanying_SN;
                    DrAdd["Accompanying_Name"] = entity.Accompanying_Name;
                    DrAdd["Accompanying_Amount"] = entity.Accompanying_Amount;
                    DrAdd["Accompanying_Unit"] = entity.Accompanying_Unit;
                    DrAdd["Accompanying_Price"] = entity.Accompanying_Price;
                    DrAdd["Accompanying_Status"] = entity.Accompanying_Status;
                    DrAdd["Accompanying_Addtime"] = entity.Accompanying_Addtime;
                    DrAdd["Accompanying_Site"] = entity.Accompanying_Site;

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

        public virtual int DelOrdersAccompanying(int ID)
        {
            string SqlAdd = "DELETE FROM Orders_Accompanying WHERE Accompanying_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetLastOrdersAccompanyimg(string sn)
        {
            int Accompanying_ID = 0;
            string SqlList = "SELECT Accompanying_ID FROM Orders_Accompanying WHERE  Accompanying_SN = '" + sn + "' ORDER BY Accompanying_ID DESC";
            SqlDataReader RdrList = null;
            try
            {
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    Accompanying_ID = Tools.NullInt(RdrList[0]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                RdrList.Close();
                RdrList = null;
            }
            return Accompanying_ID;
        }

        public virtual OrdersAccompanyingInfo GetOrdersAccompanyingByID(int ID)
        {
            OrdersAccompanyingInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Orders_Accompanying WHERE Accompanying_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new OrdersAccompanyingInfo();

                    entity.Accompanying_ID = Tools.NullInt(RdrList["Accompanying_ID"]);
                    entity.Accompanying_OrdersID = Tools.NullInt(RdrList["Accompanying_OrdersID"]);
                    entity.Accompanying_DeliveryID = Tools.NullInt(RdrList["Accompanying_DeliveryID"]);
                    entity.Accompanying_SN = Tools.NullStr(RdrList["Accompanying_SN"]);
                    entity.Accompanying_Name = Tools.NullStr(RdrList["Accompanying_Name"]);
                    entity.Accompanying_Amount = Tools.NullDbl(RdrList["Accompanying_Amount"]);
                    entity.Accompanying_Unit = Tools.NullStr(RdrList["Accompanying_Unit"]);
                    entity.Accompanying_Price = Tools.NullDbl(RdrList["Accompanying_Price"]);
                    entity.Accompanying_Status = Tools.NullInt(RdrList["Accompanying_Status"]);
                    entity.Accompanying_Addtime = Tools.NullDate(RdrList["Accompanying_Addtime"]);
                    entity.Accompanying_Site = Tools.NullStr(RdrList["Accompanying_Site"]);

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

        public virtual OrdersAccompanyingInfo GetOrdersAccompanyingBySN(string sn)
        {
            OrdersAccompanyingInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Orders_Accompanying WHERE Accompanying_SN = '" + sn + "'";
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new OrdersAccompanyingInfo();

                    entity.Accompanying_ID = Tools.NullInt(RdrList["Accompanying_ID"]);
                    entity.Accompanying_OrdersID = Tools.NullInt(RdrList["Accompanying_OrdersID"]);
                    entity.Accompanying_DeliveryID = Tools.NullInt(RdrList["Accompanying_DeliveryID"]);
                    entity.Accompanying_SN = Tools.NullStr(RdrList["Accompanying_SN"]);
                    entity.Accompanying_Name = Tools.NullStr(RdrList["Accompanying_Name"]);
                    entity.Accompanying_Amount = Tools.NullDbl(RdrList["Accompanying_Amount"]);
                    entity.Accompanying_Unit = Tools.NullStr(RdrList["Accompanying_Unit"]);
                    entity.Accompanying_Price = Tools.NullDbl(RdrList["Accompanying_Price"]);
                    entity.Accompanying_Status = Tools.NullInt(RdrList["Accompanying_Status"]);
                    entity.Accompanying_Addtime = Tools.NullDate(RdrList["Accompanying_Addtime"]);
                    entity.Accompanying_Site = Tools.NullStr(RdrList["Accompanying_Site"]);

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

        public virtual IList<OrdersAccompanyingInfo> GetOrdersAccompanyings(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<OrdersAccompanyingInfo> entitys = null;
            OrdersAccompanyingInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Orders_Accompanying";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<OrdersAccompanyingInfo>();
                    while (RdrList.Read())
                    {
                        entity = new OrdersAccompanyingInfo();
                        entity.Accompanying_ID = Tools.NullInt(RdrList["Accompanying_ID"]);
                        entity.Accompanying_OrdersID = Tools.NullInt(RdrList["Accompanying_OrdersID"]);
                        entity.Accompanying_DeliveryID = Tools.NullInt(RdrList["Accompanying_DeliveryID"]);
                        entity.Accompanying_SN = Tools.NullStr(RdrList["Accompanying_SN"]);
                        entity.Accompanying_Name = Tools.NullStr(RdrList["Accompanying_Name"]);
                        entity.Accompanying_Amount = Tools.NullDbl(RdrList["Accompanying_Amount"]);
                        entity.Accompanying_Unit = Tools.NullStr(RdrList["Accompanying_Unit"]);
                        entity.Accompanying_Price = Tools.NullDbl(RdrList["Accompanying_Price"]);
                        entity.Accompanying_Status = Tools.NullInt(RdrList["Accompanying_Status"]);
                        entity.Accompanying_Addtime = Tools.NullDate(RdrList["Accompanying_Addtime"]);
                        entity.Accompanying_Site = Tools.NullStr(RdrList["Accompanying_Site"]);

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

        public virtual IList<OrdersAccompanyingInfo> GetOrdersAccompanyingsByOrders(int Orders_ID)
        {
            IList<OrdersAccompanyingInfo> entitys = null;
            OrdersAccompanyingInfo entity = null;
            string SqlList;
            SqlDataReader RdrList = null;
            try
            {
                SqlList = "SELECT * FROM Orders_Accompanying WHERE Accompanying_OrdersID =" + Orders_ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<OrdersAccompanyingInfo>();
                    while (RdrList.Read())
                    {
                        entity = new OrdersAccompanyingInfo();
                        entity.Accompanying_ID = Tools.NullInt(RdrList["Accompanying_ID"]);
                        entity.Accompanying_OrdersID = Tools.NullInt(RdrList["Accompanying_OrdersID"]);
                        entity.Accompanying_DeliveryID = Tools.NullInt(RdrList["Accompanying_DeliveryID"]);
                        entity.Accompanying_SN = Tools.NullStr(RdrList["Accompanying_SN"]);
                        entity.Accompanying_Name = Tools.NullStr(RdrList["Accompanying_Name"]);
                        entity.Accompanying_Amount = Tools.NullDbl(RdrList["Accompanying_Amount"]);
                        entity.Accompanying_Unit = Tools.NullStr(RdrList["Accompanying_Unit"]);
                        entity.Accompanying_Price = Tools.NullDbl(RdrList["Accompanying_Price"]);
                        entity.Accompanying_Status = Tools.NullInt(RdrList["Accompanying_Status"]);
                        entity.Accompanying_Addtime = Tools.NullDate(RdrList["Accompanying_Addtime"]);
                        entity.Accompanying_Site = Tools.NullStr(RdrList["Accompanying_Site"]);

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
                SqlTable = "Orders_Accompanying";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Accompanying_ID) FROM " + SqlTable + SqlParam;

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

        public virtual IList<OrdersAccompanyingImgInfo> GetOrdersAccompanyingImgsByAccompanyID(int Accompanying_ID)
        {
            IList<OrdersAccompanyingImgInfo> entitys = null;
            OrdersAccompanyingImgInfo entity = null;
            string SqlList;
            SqlDataReader RdrList = null;
            try
            {
                SqlList = "select  * from Orders_Accompanying_Img where Img_AccompanyingID=" + Accompanying_ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<OrdersAccompanyingImgInfo>();
                    while (RdrList.Read())
                    {
                        entity = new OrdersAccompanyingImgInfo();
                        entity.Img_ID = Tools.NullInt(RdrList["Img_ID"]);
                        entity.Img_AccompanyingID = Tools.NullInt(RdrList["Img_AccompanyingID"]);
                        entity.Img_Path = Tools.NullStr(RdrList["Img_Path"]);

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

    public class OrdersAccompanyingGoods : IOrdersAccompanyingGoods
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public OrdersAccompanyingGoods()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddOrdersAccompanyingGoods(OrdersAccompanyingGoodsInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Orders_Accompanying_Goods";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Goods_ID"] = entity.Goods_ID;
            DrAdd["Goods_GoodsID"] = entity.Goods_GoodsID;
            DrAdd["Goods_DeliveryID"] = entity.Goods_DeliveryID;
            DrAdd["Goods_Amount"] = entity.Goods_Amount;
            DrAdd["Goods_AcceptAmount"] = entity.Goods_AcceptAmount;

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

        public virtual bool EditOrdersAccompanyingGoods(OrdersAccompanyingGoodsInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Orders_Accompanying_Goods WHERE Goods_ID = " + entity.Goods_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Goods_ID"] = entity.Goods_ID;
                    DrAdd["Goods_GoodsID"] = entity.Goods_GoodsID;
                    DrAdd["Goods_DeliveryID"] = entity.Goods_DeliveryID;
                    DrAdd["Goods_Amount"] = entity.Goods_Amount;
                    DrAdd["Goods_AcceptAmount"] = entity.Goods_AcceptAmount;

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

        public virtual int DelOrdersAccompanyingGoods(int ID)
        {
            string SqlAdd = "DELETE FROM Orders_Accompanying_Goods WHERE Goods_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual OrdersAccompanyingGoodsInfo GetOrdersAccompanyingGoodsByID(int ID)
        {
            OrdersAccompanyingGoodsInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Orders_Accompanying_Goods WHERE Goods_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new OrdersAccompanyingGoodsInfo();

                    entity.Goods_ID = Tools.NullInt(RdrList["Goods_ID"]);
                    entity.Goods_GoodsID = Tools.NullInt(RdrList["Goods_GoodsID"]);
                    entity.Goods_DeliveryID = Tools.NullInt(RdrList["Goods_DeliveryID"]);
                    entity.Goods_Amount = Tools.NullInt(RdrList["Goods_Amount"]);
                    entity.Goods_AcceptAmount = Tools.NullInt(RdrList["Goods_AcceptAmount"]);

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

        public virtual IList<OrdersAccompanyingGoodsInfo> GetOrdersAccompanyingGoodss(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<OrdersAccompanyingGoodsInfo> entitys = null;
            OrdersAccompanyingGoodsInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Orders_Accompanying_Goods";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<OrdersAccompanyingGoodsInfo>();
                    while (RdrList.Read())
                    {
                        entity = new OrdersAccompanyingGoodsInfo();
                        entity.Goods_ID = Tools.NullInt(RdrList["Goods_ID"]);
                        entity.Goods_GoodsID = Tools.NullInt(RdrList["Goods_GoodsID"]);
                        entity.Goods_DeliveryID = Tools.NullInt(RdrList["Goods_DeliveryID"]);
                        entity.Goods_Amount = Tools.NullInt(RdrList["Goods_Amount"]);
                        entity.Goods_AcceptAmount = Tools.NullInt(RdrList["Goods_AcceptAmount"]);

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
                SqlTable = "Orders_Accompanying_Goods";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Goods_ID) FROM " + SqlTable + SqlParam;

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
