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
    public class SupplierPriceAsk : ISupplierPriceAsk
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public SupplierPriceAsk()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddSupplierPriceAsk(SupplierPriceAskInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Supplier_PriceAsk";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["PriceAsk_ID"] = entity.PriceAsk_ID;
            DrAdd["PriceAsk_ProductID"] = entity.PriceAsk_ProductID;
            DrAdd["PriceAsk_MemberID"] = entity.PriceAsk_MemberID;
            DrAdd["PriceAsk_Title"] = entity.PriceAsk_Title;
            DrAdd["PriceAsk_Name"] = entity.PriceAsk_Name;
            DrAdd["PriceAsk_Phone"] = entity.PriceAsk_Phone;
            DrAdd["PriceAsk_Quantity"] = entity.PriceAsk_Quantity;
            DrAdd["PriceAsk_Price"] = entity.PriceAsk_Price;
            DrAdd["PriceAsk_DeliveryDate"] = entity.PriceAsk_DeliveryDate;
            DrAdd["PriceAsk_Content"] = entity.PriceAsk_Content;
            DrAdd["PriceAsk_AddTime"] = entity.PriceAsk_AddTime;
            DrAdd["PriceAsk_ReplyContent"] = entity.PriceAsk_ReplyContent;
            DrAdd["PriceAsk_ReplyTime"] = entity.PriceAsk_ReplyTime;
            DrAdd["PriceAsk_IsReply"] = entity.PriceAsk_IsReply;

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

        public virtual bool EditSupplierPriceAsk(SupplierPriceAskInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Supplier_PriceAsk WHERE PriceAsk_ID = " + entity.PriceAsk_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["PriceAsk_ID"] = entity.PriceAsk_ID;
                    DrAdd["PriceAsk_ProductID"] = entity.PriceAsk_ProductID;
                    DrAdd["PriceAsk_MemberID"] = entity.PriceAsk_MemberID;
                    DrAdd["PriceAsk_Title"] = entity.PriceAsk_Title;
                    DrAdd["PriceAsk_Name"] = entity.PriceAsk_Name;
                    DrAdd["PriceAsk_Phone"] = entity.PriceAsk_Phone;
                    DrAdd["PriceAsk_Quantity"] = entity.PriceAsk_Quantity;
                    DrAdd["PriceAsk_Price"] = entity.PriceAsk_Price;
                    DrAdd["PriceAsk_DeliveryDate"] = entity.PriceAsk_DeliveryDate;
                    DrAdd["PriceAsk_Content"] = entity.PriceAsk_Content;
                    DrAdd["PriceAsk_AddTime"] = entity.PriceAsk_AddTime;
                    DrAdd["PriceAsk_ReplyContent"] = entity.PriceAsk_ReplyContent;
                    DrAdd["PriceAsk_ReplyTime"] = entity.PriceAsk_ReplyTime;
                    DrAdd["PriceAsk_IsReply"] = entity.PriceAsk_IsReply;

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

        public virtual int DelSupplierPriceAsk(int ID)
        {
            string SqlAdd = "DELETE FROM Supplier_PriceAsk WHERE PriceAsk_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual int DelSupplierPriceAskByProductID(int ProductID)
        {
            string SqlAdd = "DELETE FROM Supplier_PriceAsk WHERE PriceAsk_ProductID = " + ProductID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public virtual SupplierPriceAskInfo GetSupplierPriceAskByID(int ID)
        {
            SupplierPriceAskInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Supplier_PriceAsk WHERE PriceAsk_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SupplierPriceAskInfo();

                    entity.PriceAsk_ID = Tools.NullInt(RdrList["PriceAsk_ID"]);
                    entity.PriceAsk_ProductID = Tools.NullInt(RdrList["PriceAsk_ProductID"]);
                    entity.PriceAsk_MemberID = Tools.NullInt(RdrList["PriceAsk_MemberID"]);
                    entity.PriceAsk_Title = Tools.NullStr(RdrList["PriceAsk_Title"]);
                    entity.PriceAsk_Name = Tools.NullStr(RdrList["PriceAsk_Name"]);
                    entity.PriceAsk_Phone = Tools.NullStr(RdrList["PriceAsk_Phone"]);
                    entity.PriceAsk_Quantity = Tools.NullInt(RdrList["PriceAsk_Quantity"]);
                    entity.PriceAsk_Price = Tools.NullDbl(RdrList["PriceAsk_Price"]);
                    entity.PriceAsk_DeliveryDate = Tools.NullDate(RdrList["PriceAsk_DeliveryDate"]);
                    entity.PriceAsk_Content = Tools.NullStr(RdrList["PriceAsk_Content"]);
                    entity.PriceAsk_AddTime = Tools.NullDate(RdrList["PriceAsk_AddTime"]);
                    entity.PriceAsk_ReplyContent = Tools.NullStr(RdrList["PriceAsk_ReplyContent"]);
                    entity.PriceAsk_ReplyTime = Tools.NullDate(RdrList["PriceAsk_ReplyTime"]);
                    entity.PriceAsk_IsReply = Tools.NullInt(RdrList["PriceAsk_IsReply"]);

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

        public virtual IList<SupplierPriceAskInfo> GetSupplierPriceAsks(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<SupplierPriceAskInfo> entitys = null;
            SupplierPriceAskInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Supplier_PriceAsk";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SupplierPriceAskInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SupplierPriceAskInfo();
                        entity.PriceAsk_ID = Tools.NullInt(RdrList["PriceAsk_ID"]);
                        entity.PriceAsk_ProductID = Tools.NullInt(RdrList["PriceAsk_ProductID"]);
                        entity.PriceAsk_MemberID = Tools.NullInt(RdrList["PriceAsk_MemberID"]);
                        entity.PriceAsk_Title = Tools.NullStr(RdrList["PriceAsk_Title"]);
                        entity.PriceAsk_Name = Tools.NullStr(RdrList["PriceAsk_Name"]);
                        entity.PriceAsk_Phone = Tools.NullStr(RdrList["PriceAsk_Phone"]);
                        entity.PriceAsk_Quantity = Tools.NullInt(RdrList["PriceAsk_Quantity"]);
                        entity.PriceAsk_Price = Tools.NullDbl(RdrList["PriceAsk_Price"]);
                        entity.PriceAsk_DeliveryDate = Tools.NullDate(RdrList["PriceAsk_DeliveryDate"]);
                        entity.PriceAsk_Content = Tools.NullStr(RdrList["PriceAsk_Content"]);
                        entity.PriceAsk_AddTime = Tools.NullDate(RdrList["PriceAsk_AddTime"]);
                        entity.PriceAsk_ReplyContent = Tools.NullStr(RdrList["PriceAsk_ReplyContent"]);
                        entity.PriceAsk_ReplyTime = Tools.NullDate(RdrList["PriceAsk_ReplyTime"]);
                        entity.PriceAsk_IsReply = Tools.NullInt(RdrList["PriceAsk_IsReply"]);

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
                SqlTable = "Supplier_PriceAsk";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(PriceAsk_ID) FROM " + SqlTable + SqlParam;

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
