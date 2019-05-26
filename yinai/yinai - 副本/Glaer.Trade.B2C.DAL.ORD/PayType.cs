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
    public class PayType : IPayType
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public PayType()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddPayType(PayTypeInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Pay_Type";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Pay_Type_ID"] = entity.Pay_Type_ID;
            DrAdd["Pay_Type_Name"] = entity.Pay_Type_Name;
            DrAdd["Pay_Type_Sort"] = entity.Pay_Type_Sort;
            DrAdd["Pay_Type_IsActive"] = entity.Pay_Type_IsActive;
            DrAdd["Pay_Type_Site"] = entity.Pay_Type_Site;

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

        public virtual bool EditPayType(PayTypeInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Pay_Type WHERE Pay_Type_ID = " + entity.Pay_Type_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Pay_Type_ID"] = entity.Pay_Type_ID;
                    DrAdd["Pay_Type_Name"] = entity.Pay_Type_Name;
                    DrAdd["Pay_Type_Sort"] = entity.Pay_Type_Sort;
                    DrAdd["Pay_Type_IsActive"] = entity.Pay_Type_IsActive;
                    DrAdd["Pay_Type_Site"] = entity.Pay_Type_Site;

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

        public virtual int DelPayType(int ID)
        {
            string SqlAdd = "DELETE FROM Pay_Type WHERE Pay_Type_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual PayTypeInfo GetPayTypeByID(int ID)
        {
            PayTypeInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Pay_Type WHERE Pay_Type_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new PayTypeInfo();

                    entity.Pay_Type_ID = Tools.NullInt(RdrList["Pay_Type_ID"]);
                    entity.Pay_Type_Name = Tools.NullStr(RdrList["Pay_Type_Name"]);
                    entity.Pay_Type_Sort = Tools.NullInt(RdrList["Pay_Type_Sort"]);
                    entity.Pay_Type_IsActive = Tools.NullInt(RdrList["Pay_Type_IsActive"]);
                    entity.Pay_Type_Site = Tools.NullStr(RdrList["Pay_Type_Site"]);

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

        public virtual IList<PayTypeInfo> GetPayTypes(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<PayTypeInfo> entitys = null;
            PayTypeInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Pay_Type";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<PayTypeInfo>();
                    while (RdrList.Read())
                    {
                        entity = new PayTypeInfo();
                        entity.Pay_Type_ID = Tools.NullInt(RdrList["Pay_Type_ID"]);
                        entity.Pay_Type_Name = Tools.NullStr(RdrList["Pay_Type_Name"]);
                        entity.Pay_Type_Sort = Tools.NullInt(RdrList["Pay_Type_Sort"]);
                        entity.Pay_Type_IsActive = Tools.NullInt(RdrList["Pay_Type_IsActive"]);
                        entity.Pay_Type_Site = Tools.NullStr(RdrList["Pay_Type_Site"]);

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
                SqlTable = "Pay_Type";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Pay_Type_ID) FROM " + SqlTable + SqlParam;

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
