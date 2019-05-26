using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.Util.Tools;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public class SupplierMessage : ISupplierMessage
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public SupplierMessage()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddSupplierMessage(SupplierMessageInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Supplier_Message";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Supplier_Message_ID"] = entity.Supplier_Message_ID;
            DrAdd["Supplier_Message_SupplierID"] = entity.Supplier_Message_SupplierID;
            DrAdd["Supplier_Message_Title"] = entity.Supplier_Message_Title;
            DrAdd["Supplier_Message_Content"] = entity.Supplier_Message_Content;
            DrAdd["Supplier_Message_Addtime"] = entity.Supplier_Message_Addtime;
            DrAdd["Supplier_Message_IsRead"] = entity.Supplier_Message_IsRead;
            DrAdd["Supplier_Message_Site"] = entity.Supplier_Message_Site;

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

        public virtual bool EditSupplierMessage(SupplierMessageInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Supplier_Message WHERE Supplier_Message_ID = " + entity.Supplier_Message_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Supplier_Message_ID"] = entity.Supplier_Message_ID;
                    DrAdd["Supplier_Message_SupplierID"] = entity.Supplier_Message_SupplierID;
                    DrAdd["Supplier_Message_Title"] = entity.Supplier_Message_Title;
                    DrAdd["Supplier_Message_Content"] = entity.Supplier_Message_Content;
                    DrAdd["Supplier_Message_Addtime"] = entity.Supplier_Message_Addtime;
                    DrAdd["Supplier_Message_IsRead"] = entity.Supplier_Message_IsRead;
                    DrAdd["Supplier_Message_Site"] = entity.Supplier_Message_Site;

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

        public virtual int DelSupplierMessage(int ID)
        {
            string SqlAdd = "DELETE FROM Supplier_Message WHERE Supplier_Message_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual SupplierMessageInfo GetSupplierMessageByID(int ID)
        {
            SupplierMessageInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Supplier_Message WHERE Supplier_Message_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SupplierMessageInfo();

                    entity.Supplier_Message_ID = Tools.NullInt(RdrList["Supplier_Message_ID"]);
                    entity.Supplier_Message_SupplierID = Tools.NullInt(RdrList["Supplier_Message_SupplierID"]);
                    entity.Supplier_Message_Title = Tools.NullStr(RdrList["Supplier_Message_Title"]);
                    entity.Supplier_Message_Content = Tools.NullStr(RdrList["Supplier_Message_Content"]);
                    entity.Supplier_Message_Addtime = Tools.NullDate(RdrList["Supplier_Message_Addtime"]);
                    entity.Supplier_Message_IsRead = Tools.NullInt(RdrList["Supplier_Message_IsRead"]);
                    entity.Supplier_Message_Site = Tools.NullStr(RdrList["Supplier_Message_Site"]);

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

        public virtual IList<SupplierMessageInfo> GetSupplierMessages(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<SupplierMessageInfo> entitys = null;
            SupplierMessageInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Supplier_Message";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SupplierMessageInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SupplierMessageInfo();
                        entity.Supplier_Message_ID = Tools.NullInt(RdrList["Supplier_Message_ID"]);
                        entity.Supplier_Message_SupplierID = Tools.NullInt(RdrList["Supplier_Message_SupplierID"]);
                        entity.Supplier_Message_Title = Tools.NullStr(RdrList["Supplier_Message_Title"]);
                        entity.Supplier_Message_Content = Tools.NullStr(RdrList["Supplier_Message_Content"]);
                        entity.Supplier_Message_Addtime = Tools.NullDate(RdrList["Supplier_Message_Addtime"]);
                        entity.Supplier_Message_IsRead = Tools.NullInt(RdrList["Supplier_Message_IsRead"]);
                        entity.Supplier_Message_Site = Tools.NullStr(RdrList["Supplier_Message_Site"]);


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
                SqlTable = "Supplier_Message";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Supplier_Message_ID) FROM " + SqlTable + SqlParam;

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
