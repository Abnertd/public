using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.Util.Tools;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public class SupplierBank : ISupplierBank
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public SupplierBank()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddSupplierBank(SupplierBankInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Supplier_Bank";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Supplier_Bank_ID"] = entity.Supplier_Bank_ID;
            DrAdd["Supplier_Bank_SupplierID"] = entity.Supplier_Bank_SupplierID; 
            DrAdd["Supplier_Bank_Name"] = entity.Supplier_Bank_Name;
            DrAdd["Supplier_Bank_NetWork"] = entity.Supplier_Bank_NetWork;
            DrAdd["Supplier_Bank_SName"] = entity.Supplier_Bank_SName;
            DrAdd["Supplier_Bank_Account"] = entity.Supplier_Bank_Account;
            DrAdd["Supplier_Bank_Attachment"] = entity.Supplier_Bank_Attachment;

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

        public virtual bool EditSupplierBank(SupplierBankInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Supplier_Bank WHERE Supplier_Bank_ID = " + entity.Supplier_Bank_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Supplier_Bank_ID"] = entity.Supplier_Bank_ID;
                    DrAdd["Supplier_Bank_SupplierID"] = entity.Supplier_Bank_SupplierID; 
                    DrAdd["Supplier_Bank_Name"] = entity.Supplier_Bank_Name;
                    DrAdd["Supplier_Bank_NetWork"] = entity.Supplier_Bank_NetWork;
                    DrAdd["Supplier_Bank_SName"] = entity.Supplier_Bank_SName;
                    DrAdd["Supplier_Bank_Account"] = entity.Supplier_Bank_Account;
                    DrAdd["Supplier_Bank_Attachment"] = entity.Supplier_Bank_Attachment;

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

        public virtual int DelSupplierBank(int ID)
        {
            string SqlAdd = "DELETE FROM Supplier_Bank WHERE Supplier_Bank_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual SupplierBankInfo GetSupplierBankByID(int ID)
        {
            SupplierBankInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Supplier_Bank WHERE Supplier_Bank_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SupplierBankInfo();

                    entity.Supplier_Bank_ID = Tools.NullInt(RdrList["Supplier_Bank_ID"]);
                    entity.Supplier_Bank_SupplierID = Tools.NullInt(RdrList["Supplier_Bank_SupplierID"]);
                    entity.Supplier_Bank_Name = Tools.NullStr(RdrList["Supplier_Bank_Name"]);
                    entity.Supplier_Bank_NetWork = Tools.NullStr(RdrList["Supplier_Bank_NetWork"]);
                    entity.Supplier_Bank_SName = Tools.NullStr(RdrList["Supplier_Bank_SName"]);
                    entity.Supplier_Bank_Account = Tools.NullStr(RdrList["Supplier_Bank_Account"]);
                    entity.Supplier_Bank_Attachment = Tools.NullStr(RdrList["Supplier_Bank_Attachment"]);
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

        public virtual SupplierBankInfo GetSupplierBankBySupplierID(int ID)
        {
            SupplierBankInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Supplier_Bank WHERE Supplier_Bank_SupplierID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SupplierBankInfo();

                    entity.Supplier_Bank_ID = Tools.NullInt(RdrList["Supplier_Bank_ID"]);
                    entity.Supplier_Bank_SupplierID = Tools.NullInt(RdrList["Supplier_Bank_SupplierID"]);
                    entity.Supplier_Bank_Name = Tools.NullStr(RdrList["Supplier_Bank_Name"]);
                    entity.Supplier_Bank_NetWork = Tools.NullStr(RdrList["Supplier_Bank_NetWork"]);
                    entity.Supplier_Bank_SName = Tools.NullStr(RdrList["Supplier_Bank_SName"]);
                    entity.Supplier_Bank_Account = Tools.NullStr(RdrList["Supplier_Bank_Account"]);
                    entity.Supplier_Bank_Attachment = Tools.NullStr(RdrList["Supplier_Bank_Attachment"]);
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

        public virtual IList<SupplierBankInfo> GetSupplierBanks(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<SupplierBankInfo> entitys = null;
            SupplierBankInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Supplier_Bank";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SupplierBankInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SupplierBankInfo();
                        entity.Supplier_Bank_ID = Tools.NullInt(RdrList["Supplier_Bank_ID"]);
                        entity.Supplier_Bank_SupplierID = Tools.NullInt(RdrList["Supplier_Bank_SupplierID"]);
                        entity.Supplier_Bank_Name = Tools.NullStr(RdrList["Supplier_Bank_Name"]);
                        entity.Supplier_Bank_NetWork = Tools.NullStr(RdrList["Supplier_Bank_NetWork"]);
                        entity.Supplier_Bank_SName = Tools.NullStr(RdrList["Supplier_Bank_SName"]);
                        entity.Supplier_Bank_Account = Tools.NullStr(RdrList["Supplier_Bank_Account"]);
                        entity.Supplier_Bank_Attachment = Tools.NullStr(RdrList["Supplier_Bank_Attachment"]);

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
                SqlTable = "Supplier_Bank";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Supplier_Bank_ID) FROM " + SqlTable + SqlParam;

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
