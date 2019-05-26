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
    public class DeliveryTime : IDeliveryTime
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public DeliveryTime()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddDeliveryTime(DeliveryTimeInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Delivery_Time";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Delivery_Time_ID"] = entity.Delivery_Time_ID;
            DrAdd["Delivery_Time_Name"] = entity.Delivery_Time_Name;
            DrAdd["Delivery_Time_Sort"] = entity.Delivery_Time_Sort;
            DrAdd["Delivery_Time_IsActive"] = entity.Delivery_Time_IsActive;
            DrAdd["Delivery_Time_Site"] = entity.Delivery_Time_Site;

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

        public virtual bool EditDeliveryTime(DeliveryTimeInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Delivery_Time WHERE Delivery_Time_ID = " + entity.Delivery_Time_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Delivery_Time_ID"] = entity.Delivery_Time_ID;
                    DrAdd["Delivery_Time_Name"] = entity.Delivery_Time_Name;
                    DrAdd["Delivery_Time_Sort"] = entity.Delivery_Time_Sort;
                    DrAdd["Delivery_Time_IsActive"] = entity.Delivery_Time_IsActive;
                    DrAdd["Delivery_Time_Site"] = entity.Delivery_Time_Site;

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

        public virtual int DelDeliveryTime(int ID)
        {
            string SqlAdd = "DELETE FROM Delivery_Time WHERE Delivery_Time_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual DeliveryTimeInfo GetDeliveryTimeByID(int ID)
        {
            DeliveryTimeInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Delivery_Time WHERE Delivery_Time_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new DeliveryTimeInfo();

                    entity.Delivery_Time_ID = Tools.NullInt(RdrList["Delivery_Time_ID"]);
                    entity.Delivery_Time_Name = Tools.NullStr(RdrList["Delivery_Time_Name"]);
                    entity.Delivery_Time_Sort = Tools.NullInt(RdrList["Delivery_Time_Sort"]);
                    entity.Delivery_Time_IsActive = Tools.NullInt(RdrList["Delivery_Time_IsActive"]);
                    entity.Delivery_Time_Site = Tools.NullStr(RdrList["Delivery_Time_Site"]);

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

        public virtual IList<DeliveryTimeInfo> GetDeliveryTimes(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<DeliveryTimeInfo> entitys = null;
            DeliveryTimeInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Delivery_Time";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<DeliveryTimeInfo>();
                    while (RdrList.Read())
                    {
                        entity = new DeliveryTimeInfo();
                        entity.Delivery_Time_ID = Tools.NullInt(RdrList["Delivery_Time_ID"]);
                        entity.Delivery_Time_Name = Tools.NullStr(RdrList["Delivery_Time_Name"]);
                        entity.Delivery_Time_Sort = Tools.NullInt(RdrList["Delivery_Time_Sort"]);
                        entity.Delivery_Time_IsActive = Tools.NullInt(RdrList["Delivery_Time_IsActive"]);
                        entity.Delivery_Time_Site = Tools.NullStr(RdrList["Delivery_Time_Site"]);

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
                SqlTable = "Delivery_Time";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Delivery_Time_ID) FROM " + SqlTable + SqlParam;

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
