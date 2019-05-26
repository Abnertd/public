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
    public class MemberInvoice : IMemberInvoice
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public MemberInvoice()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddMemberInvoice(MemberInvoiceInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Member_Invoice";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Invoice_ID"] = entity.Invoice_ID;
            DrAdd["Invoice_MemberID"] = entity.Invoice_MemberID;
            DrAdd["Invoice_Type"] = entity.Invoice_Type;
            DrAdd["Invoice_Title"] = entity.Invoice_Title;
            DrAdd["Invoice_Details"] = entity.Invoice_Details;
            DrAdd["Invoice_FirmName"] = entity.Invoice_FirmName;
            DrAdd["Invoice_VAT_FirmName"] = entity.Invoice_VAT_FirmName;
            DrAdd["Invoice_VAT_Code"] = entity.Invoice_VAT_Code;
            DrAdd["Invoice_VAT_RegAddr"] = entity.Invoice_VAT_RegAddr;
            DrAdd["Invoice_VAT_RegTel"] = entity.Invoice_VAT_RegTel;
            DrAdd["Invoice_VAT_Bank"] = entity.Invoice_VAT_Bank;
            DrAdd["Invoice_VAT_BankAccount"] = entity.Invoice_VAT_BankAccount;
            DrAdd["Invoice_VAT_Content"] = entity.Invoice_VAT_Content;
            DrAdd["Invoice_Address"] = entity.Invoice_Address;
            DrAdd["Invoice_Name"] = entity.Invoice_Name;
            DrAdd["Invoice_ZipCode"] = entity.Invoice_ZipCode;
            DrAdd["Invoice_Tel"] = entity.Invoice_Tel;
            DrAdd["Invoice_PersonelName"] = entity.Invoice_PersonelName;
            DrAdd["Invoice_PersonelCard"] = entity.Invoice_PersonelCard;
            DrAdd["Invoice_VAT_Cert"] = entity.Invoice_VAT_Cert;

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

        public virtual bool EditMemberInvoice(MemberInvoiceInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Member_Invoice WHERE Invoice_ID = " + entity.Invoice_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Invoice_ID"] = entity.Invoice_ID;
                    DrAdd["Invoice_MemberID"] = entity.Invoice_MemberID;
                    DrAdd["Invoice_Type"] = entity.Invoice_Type;
                    DrAdd["Invoice_Title"] = entity.Invoice_Title;
                    DrAdd["Invoice_Details"] = entity.Invoice_Details;
                    DrAdd["Invoice_FirmName"] = entity.Invoice_FirmName;
                    DrAdd["Invoice_VAT_FirmName"] = entity.Invoice_VAT_FirmName;
                    DrAdd["Invoice_VAT_Code"] = entity.Invoice_VAT_Code;
                    DrAdd["Invoice_VAT_RegAddr"] = entity.Invoice_VAT_RegAddr;
                    DrAdd["Invoice_VAT_RegTel"] = entity.Invoice_VAT_RegTel;
                    DrAdd["Invoice_VAT_Bank"] = entity.Invoice_VAT_Bank;
                    DrAdd["Invoice_VAT_BankAccount"] = entity.Invoice_VAT_BankAccount;
                    DrAdd["Invoice_VAT_Content"] = entity.Invoice_VAT_Content;
                    DrAdd["Invoice_Address"] = entity.Invoice_Address;
                    DrAdd["Invoice_Name"] = entity.Invoice_Name;
                    DrAdd["Invoice_ZipCode"] = entity.Invoice_ZipCode;
                    DrAdd["Invoice_Tel"] = entity.Invoice_Tel;
                    DrAdd["Invoice_PersonelName"] = entity.Invoice_PersonelName;
                    DrAdd["Invoice_PersonelCard"] = entity.Invoice_PersonelCard;
                    DrAdd["Invoice_VAT_Cert"] = entity.Invoice_VAT_Cert;

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

        public virtual int DelMemberInvoice(int ID)
        {
            string SqlAdd = "DELETE FROM Member_Invoice WHERE Invoice_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual MemberInvoiceInfo GetMemberInvoiceByID(int ID)
        {
            MemberInvoiceInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Member_Invoice WHERE Invoice_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new MemberInvoiceInfo();

                    entity.Invoice_ID = Tools.NullInt(RdrList["Invoice_ID"]);
                    entity.Invoice_MemberID = Tools.NullInt(RdrList["Invoice_MemberID"]);
                    entity.Invoice_Type = Tools.NullInt(RdrList["Invoice_Type"]);
                    entity.Invoice_Title = Tools.NullStr(RdrList["Invoice_Title"]);
                    entity.Invoice_Details = Tools.NullInt(RdrList["Invoice_Details"]);
                    entity.Invoice_FirmName = Tools.NullStr(RdrList["Invoice_FirmName"]);
                    entity.Invoice_VAT_FirmName = Tools.NullStr(RdrList["Invoice_VAT_FirmName"]);
                    entity.Invoice_VAT_Code = Tools.NullStr(RdrList["Invoice_VAT_Code"]);
                    entity.Invoice_VAT_RegAddr = Tools.NullStr(RdrList["Invoice_VAT_RegAddr"]);
                    entity.Invoice_VAT_RegTel = Tools.NullStr(RdrList["Invoice_VAT_RegTel"]);
                    entity.Invoice_VAT_Bank = Tools.NullStr(RdrList["Invoice_VAT_Bank"]);
                    entity.Invoice_VAT_BankAccount = Tools.NullStr(RdrList["Invoice_VAT_BankAccount"]);
                    entity.Invoice_VAT_Content = Tools.NullStr(RdrList["Invoice_VAT_Content"]);
                    entity.Invoice_Address = Tools.NullStr(RdrList["Invoice_Address"]);
                    entity.Invoice_Name = Tools.NullStr(RdrList["Invoice_Name"]);
                    entity.Invoice_ZipCode = Tools.NullStr(RdrList["Invoice_ZipCode"]);
                    entity.Invoice_Tel = Tools.NullStr(RdrList["Invoice_Tel"]);
                    entity.Invoice_PersonelName = Tools.NullStr(RdrList["Invoice_PersonelName"]);
                    entity.Invoice_PersonelCard = Tools.NullStr(RdrList["Invoice_PersonelCard"]);
                    entity.Invoice_VAT_Cert = Tools.NullStr(RdrList["Invoice_VAT_Cert"]);

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

        public virtual IList<MemberInvoiceInfo> GetMemberInvoicesByMemberID(int MemberID)
        {
            IList<MemberInvoiceInfo> entitys = null;
            MemberInvoiceInfo entity = null;
            string SqlList;
            SqlDataReader RdrList = null;
            try
            {
                SqlList = "SELECT * FROM Member_Invoice WHERE Invoice_MemberID = " + MemberID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<MemberInvoiceInfo>();
                    while (RdrList.Read())
                    {
                        entity = new MemberInvoiceInfo();
                        entity.Invoice_ID = Tools.NullInt(RdrList["Invoice_ID"]);
                        entity.Invoice_MemberID = Tools.NullInt(RdrList["Invoice_MemberID"]);
                        entity.Invoice_Type = Tools.NullInt(RdrList["Invoice_Type"]);
                        entity.Invoice_Title = Tools.NullStr(RdrList["Invoice_Title"]);
                        entity.Invoice_Details = Tools.NullInt(RdrList["Invoice_Details"]);
                        entity.Invoice_FirmName = Tools.NullStr(RdrList["Invoice_FirmName"]);
                        entity.Invoice_VAT_FirmName = Tools.NullStr(RdrList["Invoice_VAT_FirmName"]);
                        entity.Invoice_VAT_Code = Tools.NullStr(RdrList["Invoice_VAT_Code"]);
                        entity.Invoice_VAT_RegAddr = Tools.NullStr(RdrList["Invoice_VAT_RegAddr"]);
                        entity.Invoice_VAT_RegTel = Tools.NullStr(RdrList["Invoice_VAT_RegTel"]);
                        entity.Invoice_VAT_Bank = Tools.NullStr(RdrList["Invoice_VAT_Bank"]);
                        entity.Invoice_VAT_BankAccount = Tools.NullStr(RdrList["Invoice_VAT_BankAccount"]);
                        entity.Invoice_VAT_Content = Tools.NullStr(RdrList["Invoice_VAT_Content"]);
                        entity.Invoice_Address = Tools.NullStr(RdrList["Invoice_Address"]);
                        entity.Invoice_Name = Tools.NullStr(RdrList["Invoice_Name"]);
                        entity.Invoice_ZipCode = Tools.NullStr(RdrList["Invoice_ZipCode"]);
                        entity.Invoice_Tel = Tools.NullStr(RdrList["Invoice_Tel"]);
                        entity.Invoice_PersonelName = Tools.NullStr(RdrList["Invoice_PersonelName"]);
                        entity.Invoice_PersonelCard = Tools.NullStr(RdrList["Invoice_PersonelCard"]);
                        entity.Invoice_VAT_Cert = Tools.NullStr(RdrList["Invoice_VAT_Cert"]);

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

        public virtual IList<MemberInvoiceInfo> GetMemberInvoices(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<MemberInvoiceInfo> entitys = null;
            MemberInvoiceInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Member_Invoice";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<MemberInvoiceInfo>();
                    while (RdrList.Read())
                    {
                        entity = new MemberInvoiceInfo();
                        entity.Invoice_ID = Tools.NullInt(RdrList["Invoice_ID"]);
                        entity.Invoice_MemberID = Tools.NullInt(RdrList["Invoice_MemberID"]);
                        entity.Invoice_Type = Tools.NullInt(RdrList["Invoice_Type"]);
                        entity.Invoice_Title = Tools.NullStr(RdrList["Invoice_Title"]);
                        entity.Invoice_Details = Tools.NullInt(RdrList["Invoice_Details"]);
                        entity.Invoice_FirmName = Tools.NullStr(RdrList["Invoice_FirmName"]);
                        entity.Invoice_VAT_FirmName = Tools.NullStr(RdrList["Invoice_VAT_FirmName"]);
                        entity.Invoice_VAT_Code = Tools.NullStr(RdrList["Invoice_VAT_Code"]);
                        entity.Invoice_VAT_RegAddr = Tools.NullStr(RdrList["Invoice_VAT_RegAddr"]);
                        entity.Invoice_VAT_RegTel = Tools.NullStr(RdrList["Invoice_VAT_RegTel"]);
                        entity.Invoice_VAT_Bank = Tools.NullStr(RdrList["Invoice_VAT_Bank"]);
                        entity.Invoice_VAT_BankAccount = Tools.NullStr(RdrList["Invoice_VAT_BankAccount"]);
                        entity.Invoice_VAT_Content = Tools.NullStr(RdrList["Invoice_VAT_Content"]);
                        entity.Invoice_Address = Tools.NullStr(RdrList["Invoice_Address"]);
                        entity.Invoice_Name = Tools.NullStr(RdrList["Invoice_Name"]);
                        entity.Invoice_ZipCode = Tools.NullStr(RdrList["Invoice_ZipCode"]);
                        entity.Invoice_Tel = Tools.NullStr(RdrList["Invoice_Tel"]);
                        entity.Invoice_PersonelName = Tools.NullStr(RdrList["Invoice_PersonelName"]);
                        entity.Invoice_PersonelCard = Tools.NullStr(RdrList["Invoice_PersonelCard"]);
                        entity.Invoice_VAT_Cert = Tools.NullStr(RdrList["Invoice_VAT_Cert"]);

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
                SqlTable = "Member_Invoice";
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
