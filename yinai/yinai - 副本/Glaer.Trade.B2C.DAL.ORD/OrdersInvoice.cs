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
    public class OrdersInvoice : IOrdersInvoice
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public OrdersInvoice()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddOrdersInvoice(OrdersInvoiceInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Orders_Invoice";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Invoice_ID"] = entity.Invoice_ID;
            DrAdd["Invoice_OrdersID"] = entity.Invoice_OrdersID;
            DrAdd["Invoice_Type"] = entity.Invoice_Type;
            DrAdd["Invoice_Title"] = entity.Invoice_Title;
            DrAdd["Invoice_Content"] = entity.Invoice_Content;
            DrAdd["Invoice_FirmName"] = entity.Invoice_FirmName;
            DrAdd["Invoice_VAT_FirmName"] = entity.Invoice_VAT_FirmName;
            DrAdd["Invoice_VAT_Code"] = entity.Invoice_VAT_Code;
            DrAdd["Invoice_VAT_RegAddr"] = entity.Invoice_VAT_RegAddr;
            DrAdd["Invoice_VAT_RegTel"] = entity.Invoice_VAT_RegTel;
            DrAdd["Invoice_VAT_Bank"] = entity.Invoice_VAT_Bank;
            DrAdd["Invoice_VAT_BankAcount"] = entity.Invoice_VAT_BankAcount;
            DrAdd["Invoice_VAT_Content"] = entity.Invoice_VAT_Content;

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

        public virtual bool EditOrdersInvoice(OrdersInvoiceInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Orders_Invoice WHERE Invoice_ID = " + entity.Invoice_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Invoice_ID"] = entity.Invoice_ID;
                    DrAdd["Invoice_OrdersID"] = entity.Invoice_OrdersID;
                    DrAdd["Invoice_Type"] = entity.Invoice_Type;
                    DrAdd["Invoice_Title"] = entity.Invoice_Title;
                    DrAdd["Invoice_Content"] = entity.Invoice_Content;
                    DrAdd["Invoice_FirmName"] = entity.Invoice_FirmName;
                    DrAdd["Invoice_VAT_FirmName"] = entity.Invoice_VAT_FirmName;
                    DrAdd["Invoice_VAT_Code"] = entity.Invoice_VAT_Code;
                    DrAdd["Invoice_VAT_RegAddr"] = entity.Invoice_VAT_RegAddr;
                    DrAdd["Invoice_VAT_RegTel"] = entity.Invoice_VAT_RegTel;
                    DrAdd["Invoice_VAT_Bank"] = entity.Invoice_VAT_Bank;
                    DrAdd["Invoice_VAT_BankAcount"] = entity.Invoice_VAT_BankAcount;
                    DrAdd["Invoice_VAT_Content"] = entity.Invoice_VAT_Content;

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

        public virtual int DelOrdersInvoice(int ID)
        {
            string SqlAdd = "DELETE FROM Orders_Invoice WHERE Invoice_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual OrdersInvoiceInfo GetOrdersInvoiceByID(int ID)
        {
            OrdersInvoiceInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Orders_Invoice WHERE Invoice_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new OrdersInvoiceInfo();

                    entity.Invoice_ID = Tools.NullInt(RdrList["Invoice_ID"]);
                    entity.Invoice_OrdersID = Tools.NullInt(RdrList["Invoice_OrdersID"]);
                    entity.Invoice_Type = Tools.NullInt(RdrList["Invoice_Type"]);
                    entity.Invoice_Title = Tools.NullStr(RdrList["Invoice_Title"]);
                    entity.Invoice_Content = Tools.NullInt(RdrList["Invoice_Content"]);
                    entity.Invoice_FirmName = Tools.NullStr(RdrList["Invoice_FirmName"]);
                    entity.Invoice_VAT_FirmName = Tools.NullStr(RdrList["Invoice_VAT_FirmName"]);
                    entity.Invoice_VAT_Code = Tools.NullStr(RdrList["Invoice_VAT_Code"]);
                    entity.Invoice_VAT_RegAddr = Tools.NullStr(RdrList["Invoice_VAT_RegAddr"]);
                    entity.Invoice_VAT_RegTel = Tools.NullStr(RdrList["Invoice_VAT_RegTel"]);
                    entity.Invoice_VAT_Bank = Tools.NullStr(RdrList["Invoice_VAT_Bank"]);
                    entity.Invoice_VAT_BankAcount = Tools.NullStr(RdrList["Invoice_VAT_BankAcount"]);
                    entity.Invoice_VAT_Content = Tools.NullStr(RdrList["Invoice_VAT_Content"]);

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

        public virtual OrdersInvoiceInfo GetOrdersInvoiceByOrdersID(int ID)
        {
            OrdersInvoiceInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Orders_Invoice WHERE Invoice_OrdersID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new OrdersInvoiceInfo();

                    entity.Invoice_ID = Tools.NullInt(RdrList["Invoice_ID"]);
                    entity.Invoice_OrdersID = Tools.NullInt(RdrList["Invoice_OrdersID"]);
                    entity.Invoice_Type = Tools.NullInt(RdrList["Invoice_Type"]);
                    entity.Invoice_Title = Tools.NullStr(RdrList["Invoice_Title"]);
                    entity.Invoice_Content = Tools.NullInt(RdrList["Invoice_Content"]);
                    entity.Invoice_FirmName = Tools.NullStr(RdrList["Invoice_FirmName"]);
                    entity.Invoice_VAT_FirmName = Tools.NullStr(RdrList["Invoice_VAT_FirmName"]);
                    entity.Invoice_VAT_Code = Tools.NullStr(RdrList["Invoice_VAT_Code"]);
                    entity.Invoice_VAT_RegAddr = Tools.NullStr(RdrList["Invoice_VAT_RegAddr"]);
                    entity.Invoice_VAT_RegTel = Tools.NullStr(RdrList["Invoice_VAT_RegTel"]);
                    entity.Invoice_VAT_Bank = Tools.NullStr(RdrList["Invoice_VAT_Bank"]);
                    entity.Invoice_VAT_BankAcount = Tools.NullStr(RdrList["Invoice_VAT_BankAcount"]);
                    entity.Invoice_VAT_Content = Tools.NullStr(RdrList["Invoice_VAT_Content"]);

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

        public virtual IList<OrdersInvoiceInfo> GetOrdersInvoices(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<OrdersInvoiceInfo> entitys = null;
            OrdersInvoiceInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Orders_Invoice";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<OrdersInvoiceInfo>();
                    while (RdrList.Read())
                    {
                        entity = new OrdersInvoiceInfo();
                        entity.Invoice_ID = Tools.NullInt(RdrList["Invoice_ID"]);
                        entity.Invoice_OrdersID = Tools.NullInt(RdrList["Invoice_OrdersID"]);
                        entity.Invoice_Type = Tools.NullInt(RdrList["Invoice_Type"]);
                        entity.Invoice_Title = Tools.NullStr(RdrList["Invoice_Title"]);
                        entity.Invoice_Content = Tools.NullInt(RdrList["Invoice_Content"]);
                        entity.Invoice_FirmName = Tools.NullStr(RdrList["Invoice_FirmName"]);
                        entity.Invoice_VAT_FirmName = Tools.NullStr(RdrList["Invoice_VAT_FirmName"]);
                        entity.Invoice_VAT_Code = Tools.NullStr(RdrList["Invoice_VAT_Code"]);
                        entity.Invoice_VAT_RegAddr = Tools.NullStr(RdrList["Invoice_VAT_RegAddr"]);
                        entity.Invoice_VAT_RegTel = Tools.NullStr(RdrList["Invoice_VAT_RegTel"]);
                        entity.Invoice_VAT_Bank = Tools.NullStr(RdrList["Invoice_VAT_Bank"]);
                        entity.Invoice_VAT_BankAcount = Tools.NullStr(RdrList["Invoice_VAT_BankAcount"]);
                        entity.Invoice_VAT_Content = Tools.NullStr(RdrList["Invoice_VAT_Content"]);

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
                SqlTable = "Orders_Invoice";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Invoice_ID) FROM " + SqlTable + SqlParam;

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
