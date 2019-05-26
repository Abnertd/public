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
    public class StockoutBooking : IStockoutBooking
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public StockoutBooking()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddStockoutBooking(StockoutBookingInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Stockout_Booking";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Stockout_ID"] = entity.Stockout_ID;
            DrAdd["Stockout_Product_Name"] = entity.Stockout_Product_Name;
            DrAdd["Stockout_Product_Describe"] = entity.Stockout_Product_Describe;
            DrAdd["Stockout_Member_Name"] = entity.Stockout_Member_Name;
            DrAdd["Stockout_Member_Tel"] = entity.Stockout_Member_Tel;
            DrAdd["Stockout_Member_Email"] = entity.Stockout_Member_Email;
            DrAdd["Stockout_IsRead"] = entity.Stockout_IsRead;
            DrAdd["Stockout_Addtime"] = entity.Stockout_Addtime;
            DrAdd["Stockout_Site"] = entity.Stockout_Site;

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

        public virtual bool EditStockoutBooking(StockoutBookingInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Stockout_Booking WHERE Stockout_ID = " + entity.Stockout_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Stockout_ID"] = entity.Stockout_ID;
                    DrAdd["Stockout_Product_Name"] = entity.Stockout_Product_Name;
                    DrAdd["Stockout_Product_Describe"] = entity.Stockout_Product_Describe;
                    DrAdd["Stockout_Member_Name"] = entity.Stockout_Member_Name;
                    DrAdd["Stockout_Member_Tel"] = entity.Stockout_Member_Tel;
                    DrAdd["Stockout_Member_Email"] = entity.Stockout_Member_Email;
                    DrAdd["Stockout_IsRead"] = entity.Stockout_IsRead;
                    DrAdd["Stockout_Addtime"] = entity.Stockout_Addtime;
                    DrAdd["Stockout_Site"] = entity.Stockout_Site;

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

        public virtual int DelStockoutBooking(int ID)
        {
            string SqlAdd = "DELETE FROM Stockout_Booking WHERE Stockout_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual StockoutBookingInfo GetStockoutBookingByID(int ID)
        {
            StockoutBookingInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Stockout_Booking WHERE Stockout_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new StockoutBookingInfo();

                    entity.Stockout_ID = Tools.NullInt(RdrList["Stockout_ID"]);
                    entity.Stockout_Product_Name = Tools.NullStr(RdrList["Stockout_Product_Name"]);
                    entity.Stockout_Product_Describe = Tools.NullStr(RdrList["Stockout_Product_Describe"]);
                    entity.Stockout_Member_Name = Tools.NullStr(RdrList["Stockout_Member_Name"]);
                    entity.Stockout_Member_Tel = Tools.NullStr(RdrList["Stockout_Member_Tel"]);
                    entity.Stockout_Member_Email = Tools.NullStr(RdrList["Stockout_Member_Email"]);
                    entity.Stockout_IsRead = Tools.NullInt(RdrList["Stockout_IsRead"]);
                    entity.Stockout_Addtime = Tools.NullDate(RdrList["Stockout_Addtime"]);
                    entity.Stockout_Site = Tools.NullStr(RdrList["Stockout_Site"]);
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

        public virtual IList<StockoutBookingInfo> GetStockoutBookings(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<StockoutBookingInfo> entitys = null;
            StockoutBookingInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Stockout_Booking";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<StockoutBookingInfo>();
                    while (RdrList.Read())
                    {
                        entity = new StockoutBookingInfo();
                        entity.Stockout_ID = Tools.NullInt(RdrList["Stockout_ID"]);
                        entity.Stockout_Product_Name = Tools.NullStr(RdrList["Stockout_Product_Name"]);
                        entity.Stockout_Product_Describe = Tools.NullStr(RdrList["Stockout_Product_Describe"]);
                        entity.Stockout_Member_Name = Tools.NullStr(RdrList["Stockout_Member_Name"]);
                        entity.Stockout_Member_Tel = Tools.NullStr(RdrList["Stockout_Member_Tel"]);
                        entity.Stockout_Member_Email = Tools.NullStr(RdrList["Stockout_Member_Email"]);
                        entity.Stockout_IsRead = Tools.NullInt(RdrList["Stockout_IsRead"]);
                        entity.Stockout_Addtime = Tools.NullDate(RdrList["Stockout_Addtime"]);
                        entity.Stockout_Site = Tools.NullStr(RdrList["Stockout_Site"]);

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
                SqlTable = "Stockout_Booking";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Stockout_ID) FROM " + SqlTable + SqlParam;

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
