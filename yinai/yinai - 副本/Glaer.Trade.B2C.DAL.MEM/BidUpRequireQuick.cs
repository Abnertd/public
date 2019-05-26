using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.Util.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public class BidUpRequireQuick : IBidUpRequireQuick
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public BidUpRequireQuick()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddBidUpRequireQuick(BidUpRequireQuickInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Bid_Up_Require_Quick";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Bid_Up_ID"] = entity.Bid_Up_ID;
            DrAdd["Bid_Up_ContractMan"] = entity.Bid_Up_ContractMan;
            DrAdd["Bid_Up_ContractMobile"] = entity.Bid_Up_ContractMobile;
            DrAdd["Bid_Up_ContractContent"] = entity.Bid_Up_ContractContent;
            DrAdd["Bid_Up_Type"] = entity.Bid_Up_Type;
            DrAdd["Bid_Up_Note"] = entity.Bid_Up_Note;
            DrAdd["Bid_Up_Note1"] = entity.Bid_Up_Note1;
            DrAdd["Bid_Up_AddTime"] = entity.Bid_Up_AddTime;

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

        public virtual bool EditBidUpRequireQuick(BidUpRequireQuickInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Bid_Up_Require_Quick WHERE Bid_Up_ID = " + entity.Bid_Up_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Bid_Up_ID"] = entity.Bid_Up_ID;
                    DrAdd["Bid_Up_ContractMan"] = entity.Bid_Up_ContractMan;
                    DrAdd["Bid_Up_ContractMobile"] = entity.Bid_Up_ContractMobile;
                    DrAdd["Bid_Up_ContractContent"] = entity.Bid_Up_ContractContent;
                    DrAdd["Bid_Up_Type"] = entity.Bid_Up_Type;
                    DrAdd["Bid_Up_Note"] = entity.Bid_Up_Note;
                    DrAdd["Bid_Up_Note1"] = entity.Bid_Up_Note1;
                    DrAdd["Bid_Up_AddTime"] = entity.Bid_Up_AddTime;

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

        public virtual int DelBidUpRequireQuick(int ID)
        {
            string SqlAdd = "DELETE FROM Bid_Up_Require_Quick WHERE Bid_Up_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual BidUpRequireQuickInfo GetBidUpRequireQuickByID(int ID)
        {
            BidUpRequireQuickInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Bid_Up_Require_Quick WHERE Bid_Up_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new BidUpRequireQuickInfo();

                    entity.Bid_Up_ID = Tools.NullInt(RdrList["Bid_Up_ID"]);
                    entity.Bid_Up_ContractMan = Tools.NullStr(RdrList["Bid_Up_ContractMan"]);
                    entity.Bid_Up_ContractMobile = Tools.NullStr(RdrList["Bid_Up_ContractMobile"]);
                    entity.Bid_Up_ContractContent = Tools.NullStr(RdrList["Bid_Up_ContractContent"]);
                    entity.Bid_Up_Type = Tools.NullInt(RdrList["Bid_Up_Type"]);
                    entity.Bid_Up_Note = Tools.NullStr(RdrList["Bid_Up_Note"]);
                    entity.Bid_Up_Note1 = Tools.NullStr(RdrList["Bid_Up_Note1"]);
                    entity.Bid_Up_AddTime = Tools.NullDate(RdrList["Bid_Up_AddTime"]);

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

        public virtual IList<BidUpRequireQuickInfo> GetBidUpRequireQuicks(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<BidUpRequireQuickInfo> entitys = null;
            BidUpRequireQuickInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Bid_Up_Require_Quick";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<BidUpRequireQuickInfo>();
                    while (RdrList.Read())
                    {
                        entity = new BidUpRequireQuickInfo();
                        entity.Bid_Up_ID = Tools.NullInt(RdrList["Bid_Up_ID"]);
                        entity.Bid_Up_ContractMan = Tools.NullStr(RdrList["Bid_Up_ContractMan"]);
                        entity.Bid_Up_ContractMobile = Tools.NullStr(RdrList["Bid_Up_ContractMobile"]);
                        entity.Bid_Up_ContractContent = Tools.NullStr(RdrList["Bid_Up_ContractContent"]);
                        entity.Bid_Up_Type = Tools.NullInt(RdrList["Bid_Up_Type"]);
                        entity.Bid_Up_Note = Tools.NullStr(RdrList["Bid_Up_Note"]);
                        entity.Bid_Up_Note1 = Tools.NullStr(RdrList["Bid_Up_Note1"]);
                        entity.Bid_Up_AddTime = Tools.NullDate(RdrList["Bid_Up_AddTime"]);

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
                SqlTable = "Bid_Up_Require_Quick";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Bid_Up_ID) FROM " + SqlTable + SqlParam;

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
