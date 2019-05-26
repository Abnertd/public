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
    public class ShoppingAsk : IShoppingAsk
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public ShoppingAsk()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddShoppingAsk(ShoppingAskInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Shopping_Ask";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Ask_ID"] = entity.Ask_ID;
            DrAdd["Ask_Type"] = entity.Ask_Type;
            DrAdd["Ask_Contact"] = entity.Ask_Contact;
            DrAdd["Ask_Content"] = entity.Ask_Content;
            DrAdd["Ask_Reply"] = entity.Ask_Reply;
            DrAdd["Ask_Addtime"] = entity.Ask_Addtime;
            DrAdd["Ask_SupplierID"] = entity.Ask_SupplierID;
            DrAdd["Ask_MemberID"] = entity.Ask_MemberID;
            DrAdd["Ask_ProductID"] = entity.Ask_ProductID;
            DrAdd["Ask_Pleasurenum"] = entity.Ask_Pleasurenum;
            DrAdd["Ask_Displeasure"] = entity.Ask_Displeasure;
            DrAdd["Ask_IsCheck"] = entity.Ask_IsCheck;
            DrAdd["Ask_Isreply"] = entity.Ask_Isreply;
            DrAdd["Ask_Site"] = entity.Ask_Site;

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

        public virtual bool EditShoppingAsk(ShoppingAskInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Shopping_Ask WHERE Ask_ID = " + entity.Ask_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Ask_ID"] = entity.Ask_ID;
                    DrAdd["Ask_Type"] = entity.Ask_Type;
                    DrAdd["Ask_Contact"] = entity.Ask_Contact;
                    DrAdd["Ask_Content"] = entity.Ask_Content;
                    DrAdd["Ask_Reply"] = entity.Ask_Reply;
                    DrAdd["Ask_Addtime"] = entity.Ask_Addtime;
                    DrAdd["Ask_SupplierID"] = entity.Ask_SupplierID;
                    DrAdd["Ask_MemberID"] = entity.Ask_MemberID;
                    DrAdd["Ask_ProductID"] = entity.Ask_ProductID;
                    DrAdd["Ask_Pleasurenum"] = entity.Ask_Pleasurenum;
                    DrAdd["Ask_Displeasure"] = entity.Ask_Displeasure;
                    DrAdd["Ask_IsCheck"] = entity.Ask_IsCheck;
                    DrAdd["Ask_Isreply"] = entity.Ask_Isreply;
                    DrAdd["Ask_Site"] = entity.Ask_Site;

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

        public virtual int DelShoppingAsk(int ID)
        {
            string SqlAdd = "DELETE FROM Shopping_Ask WHERE Ask_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual ShoppingAskInfo GetShoppingAskByID(int ID)
        {
            ShoppingAskInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Shopping_Ask WHERE Ask_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new ShoppingAskInfo();

                    entity.Ask_ID = Tools.NullInt(RdrList["Ask_ID"]);
                    entity.Ask_Type = Tools.NullInt(RdrList["Ask_Type"]);
                    entity.Ask_Contact = Tools.NullStr(RdrList["Ask_Contact"]);
                    entity.Ask_Content = Tools.NullStr(RdrList["Ask_Content"]);
                    entity.Ask_Reply = Tools.NullStr(RdrList["Ask_Reply"]);
                    entity.Ask_Addtime = Tools.NullDate(RdrList["Ask_Addtime"]);
                    entity.Ask_SupplierID = Tools.NullInt(RdrList["Ask_SupplierID"]);
                    entity.Ask_MemberID = Tools.NullInt(RdrList["Ask_MemberID"]);
                    entity.Ask_ProductID = Tools.NullInt(RdrList["Ask_ProductID"]);
                    entity.Ask_Pleasurenum = Tools.NullInt(RdrList["Ask_Pleasurenum"]);
                    entity.Ask_Displeasure = Tools.NullInt(RdrList["Ask_Displeasure"]);
                    entity.Ask_Isreply = Tools.NullInt(RdrList["Ask_Isreply"]);
                    entity.Ask_IsCheck = Tools.NullInt(RdrList["Ask_IsCheck"]);
                    entity.Ask_Site = Tools.NullStr(RdrList["Ask_Site"]);

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

        public virtual IList<ShoppingAskInfo> GetShoppingAsks(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<ShoppingAskInfo> entitys = null;
            ShoppingAskInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Shopping_Ask";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<ShoppingAskInfo>();
                    while (RdrList.Read())
                    {
                        entity = new ShoppingAskInfo();
                        entity.Ask_ID = Tools.NullInt(RdrList["Ask_ID"]);
                        entity.Ask_Type = Tools.NullInt(RdrList["Ask_Type"]);
                        entity.Ask_Contact = Tools.NullStr(RdrList["Ask_Contact"]);
                        entity.Ask_Content = Tools.NullStr(RdrList["Ask_Content"]);
                        entity.Ask_Reply = Tools.NullStr(RdrList["Ask_Reply"]);
                        entity.Ask_Addtime = Tools.NullDate(RdrList["Ask_Addtime"]);
                        entity.Ask_SupplierID = Tools.NullInt(RdrList["Ask_SupplierID"]);
                        entity.Ask_MemberID = Tools.NullInt(RdrList["Ask_MemberID"]);
                        entity.Ask_ProductID = Tools.NullInt(RdrList["Ask_ProductID"]);
                        entity.Ask_Pleasurenum = Tools.NullInt(RdrList["Ask_Pleasurenum"]);
                        entity.Ask_Displeasure = Tools.NullInt(RdrList["Ask_Displeasure"]);
                        entity.Ask_Isreply = Tools.NullInt(RdrList["Ask_Isreply"]);
                        entity.Ask_IsCheck = Tools.NullInt(RdrList["Ask_IsCheck"]);
                        entity.Ask_Site = Tools.NullStr(RdrList["Ask_Site"]);

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
                SqlTable = "Shopping_Ask";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Ask_ID) FROM " + SqlTable + SqlParam;

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
