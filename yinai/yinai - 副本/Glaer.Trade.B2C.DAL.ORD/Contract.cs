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
    public class Contract : IContract
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public Contract()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddContract(ContractInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Contract";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Contract_ID"] = entity.Contract_ID;
            DrAdd["Contract_Type"] = entity.Contract_Type;
            DrAdd["Contract_BuyerID"] = entity.Contract_BuyerID;
            DrAdd["Contract_BuyerName"] = entity.Contract_BuyerName;
            DrAdd["Contract_SupplierID"] = entity.Contract_SupplierID;
            DrAdd["Contract_SupplierName"] = entity.Contract_SupplierName;
            DrAdd["Contract_SN"] = entity.Contract_SN;
            DrAdd["Contract_Name"] = entity.Contract_Name;
            DrAdd["Contract_Status"] = entity.Contract_Status;
            DrAdd["Contract_Confirm_Status"] = entity.Contract_Confirm_Status;
            DrAdd["Contract_Payment_Status"] = entity.Contract_Payment_Status;
            DrAdd["Contract_Delivery_Status"] = entity.Contract_Delivery_Status;
            DrAdd["Contract_AllPrice"] = entity.Contract_AllPrice;
            DrAdd["Contract_Price"] = entity.Contract_Price;
            DrAdd["Contract_Freight"] = entity.Contract_Freight;
            DrAdd["Contract_ServiceFee"] = entity.Contract_ServiceFee;
            DrAdd["Contract_Discount"] = entity.Contract_Discount;
            DrAdd["Contract_Delivery_ID"] = entity.Contract_Delivery_ID;
            DrAdd["Contract_Delivery_Name"] = entity.Contract_Delivery_Name;
            DrAdd["Contract_Payway_ID"] = entity.Contract_Payway_ID;
            DrAdd["Contract_Payway_Name"] = entity.Contract_Payway_Name;
            DrAdd["Contract_Note"] = entity.Contract_Note;
            DrAdd["Contract_Template"] = entity.Contract_Template;
            DrAdd["Contract_Addtime"] = entity.Contract_Addtime;
            DrAdd["Contract_Site"] = entity.Contract_Site;
            DrAdd["Contract_Source"] = entity.Contract_Source;
            DrAdd["Contract_IsEvaluate"] = entity.Contract_IsEvaluate;

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

        public virtual bool EditContract(ContractInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Contract WHERE Contract_ID = " + entity.Contract_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Contract_ID"] = entity.Contract_ID;
                    DrAdd["Contract_Type"] = entity.Contract_Type;
                    DrAdd["Contract_BuyerID"] = entity.Contract_BuyerID;
                    DrAdd["Contract_BuyerName"] = entity.Contract_BuyerName;
                    DrAdd["Contract_SupplierID"] = entity.Contract_SupplierID;
                    DrAdd["Contract_SupplierName"] = entity.Contract_SupplierName;
                    DrAdd["Contract_SN"] = entity.Contract_SN;
                    DrAdd["Contract_Name"] = entity.Contract_Name;
                    DrAdd["Contract_Status"] = entity.Contract_Status;
                    DrAdd["Contract_Confirm_Status"] = entity.Contract_Confirm_Status;
                    DrAdd["Contract_Payment_Status"] = entity.Contract_Payment_Status;
                    DrAdd["Contract_Delivery_Status"] = entity.Contract_Delivery_Status;
                    DrAdd["Contract_AllPrice"] = entity.Contract_AllPrice;
                    DrAdd["Contract_Price"] = entity.Contract_Price;
                    DrAdd["Contract_Freight"] = entity.Contract_Freight;
                    DrAdd["Contract_ServiceFee"] = entity.Contract_ServiceFee;
                    DrAdd["Contract_Discount"] = entity.Contract_Discount;
                    DrAdd["Contract_Delivery_ID"] = entity.Contract_Delivery_ID;
                    DrAdd["Contract_Delivery_Name"] = entity.Contract_Delivery_Name;
                    DrAdd["Contract_Payway_ID"] = entity.Contract_Payway_ID;
                    DrAdd["Contract_Payway_Name"] = entity.Contract_Payway_Name;
                    DrAdd["Contract_Note"] = entity.Contract_Note;
                    DrAdd["Contract_Template"] = entity.Contract_Template;
                    DrAdd["Contract_Addtime"] = entity.Contract_Addtime;
                    DrAdd["Contract_Site"] = entity.Contract_Site;
                    DrAdd["Contract_Source"] = entity.Contract_Source;
                    DrAdd["Contract_IsEvaluate"] = entity.Contract_IsEvaluate;

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

        public virtual int DelContract(int ID)
        {
            string SqlAdd = "DELETE FROM Contract WHERE Contract_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual ContractInfo GetContractByID(int ID)
        {
            ContractInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Contract WHERE Contract_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new ContractInfo();

                    entity.Contract_ID = Tools.NullInt(RdrList["Contract_ID"]);
                    entity.Contract_Type = Tools.NullInt(RdrList["Contract_Type"]);
                    entity.Contract_BuyerID = Tools.NullInt(RdrList["Contract_BuyerID"]);
                    entity.Contract_BuyerName = Tools.NullStr(RdrList["Contract_BuyerName"]);
                    entity.Contract_SupplierID = Tools.NullInt(RdrList["Contract_SupplierID"]);
                    entity.Contract_SupplierName = Tools.NullStr(RdrList["Contract_SupplierName"]);
                    entity.Contract_SN = Tools.NullStr(RdrList["Contract_SN"]);
                    entity.Contract_Name = Tools.NullStr(RdrList["Contract_Name"]);
                    entity.Contract_Status = Tools.NullInt(RdrList["Contract_Status"]);
                    entity.Contract_Confirm_Status = Tools.NullInt(RdrList["Contract_Confirm_Status"]);
                    entity.Contract_Payment_Status = Tools.NullInt(RdrList["Contract_Payment_Status"]);
                    entity.Contract_Delivery_Status = Tools.NullInt(RdrList["Contract_Delivery_Status"]);
                    entity.Contract_AllPrice = Tools.NullDbl(RdrList["Contract_AllPrice"]);
                    entity.Contract_Price = Tools.NullDbl(RdrList["Contract_Price"]);
                    entity.Contract_Freight = Tools.NullDbl(RdrList["Contract_Freight"]);
                    entity.Contract_ServiceFee = Tools.NullDbl(RdrList["Contract_ServiceFee"]);
                    entity.Contract_Discount = Tools.NullDbl(RdrList["Contract_Discount"]);
                    entity.Contract_Delivery_ID = Tools.NullInt(RdrList["Contract_Delivery_ID"]);
                    entity.Contract_Delivery_Name = Tools.NullStr(RdrList["Contract_Delivery_Name"]);
                    entity.Contract_Payway_ID = Tools.NullInt(RdrList["Contract_Payway_ID"]);
                    entity.Contract_Payway_Name = Tools.NullStr(RdrList["Contract_Payway_Name"]);
                    entity.Contract_Note = Tools.NullStr(RdrList["Contract_Note"]);
                    entity.Contract_Template = Tools.NullStr(RdrList["Contract_Template"]);
                    entity.Contract_Addtime = Tools.NullDate(RdrList["Contract_Addtime"]);
                    entity.Contract_Site = Tools.NullStr(RdrList["Contract_Site"]);
                    entity.Contract_Source = Tools.NullInt(RdrList["Contract_Source"]);
                    entity.Contract_IsEvaluate = Tools.NullInt(RdrList["Contract_IsEvaluate"]);
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

        public virtual ContractInfo GetContractBySn(string Sn)
        {
            ContractInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Contract WHERE Contract_SN = '" + Sn+"'";
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new ContractInfo();

                    entity.Contract_ID = Tools.NullInt(RdrList["Contract_ID"]);
                    entity.Contract_Type = Tools.NullInt(RdrList["Contract_Type"]);
                    entity.Contract_BuyerID = Tools.NullInt(RdrList["Contract_BuyerID"]);
                    entity.Contract_BuyerName = Tools.NullStr(RdrList["Contract_BuyerName"]);
                    entity.Contract_SupplierID = Tools.NullInt(RdrList["Contract_SupplierID"]);
                    entity.Contract_SupplierName = Tools.NullStr(RdrList["Contract_SupplierName"]);
                    entity.Contract_SN = Tools.NullStr(RdrList["Contract_SN"]);
                    entity.Contract_Name = Tools.NullStr(RdrList["Contract_Name"]);
                    entity.Contract_Status = Tools.NullInt(RdrList["Contract_Status"]);
                    entity.Contract_Confirm_Status = Tools.NullInt(RdrList["Contract_Confirm_Status"]);
                    entity.Contract_Payment_Status = Tools.NullInt(RdrList["Contract_Payment_Status"]);
                    entity.Contract_Delivery_Status = Tools.NullInt(RdrList["Contract_Delivery_Status"]);
                    entity.Contract_AllPrice = Tools.NullDbl(RdrList["Contract_AllPrice"]);
                    entity.Contract_Price = Tools.NullDbl(RdrList["Contract_Price"]);
                    entity.Contract_Freight = Tools.NullDbl(RdrList["Contract_Freight"]);
                    entity.Contract_ServiceFee = Tools.NullDbl(RdrList["Contract_ServiceFee"]);
                    entity.Contract_Discount = Tools.NullDbl(RdrList["Contract_Discount"]);
                    entity.Contract_Delivery_ID = Tools.NullInt(RdrList["Contract_Delivery_ID"]);
                    entity.Contract_Delivery_Name = Tools.NullStr(RdrList["Contract_Delivery_Name"]);
                    entity.Contract_Payway_ID = Tools.NullInt(RdrList["Contract_Payway_ID"]);
                    entity.Contract_Payway_Name = Tools.NullStr(RdrList["Contract_Payway_Name"]);
                    entity.Contract_Note = Tools.NullStr(RdrList["Contract_Note"]);
                    entity.Contract_Template = Tools.NullStr(RdrList["Contract_Template"]);
                    entity.Contract_Addtime = Tools.NullDate(RdrList["Contract_Addtime"]);
                    entity.Contract_Site = Tools.NullStr(RdrList["Contract_Site"]);
                    entity.Contract_Source = Tools.NullInt(RdrList["Contract_Source"]);
                    entity.Contract_IsEvaluate = Tools.NullInt(RdrList["Contract_IsEvaluate"]);
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

        public virtual IList<ContractInfo> GetContracts(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<ContractInfo> entitys = null;
            ContractInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Contract";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<ContractInfo>();
                    while (RdrList.Read())
                    {
                        entity = new ContractInfo();
                        entity.Contract_ID = Tools.NullInt(RdrList["Contract_ID"]);
                        entity.Contract_Type = Tools.NullInt(RdrList["Contract_Type"]);
                        entity.Contract_BuyerID = Tools.NullInt(RdrList["Contract_BuyerID"]);
                        entity.Contract_BuyerName = Tools.NullStr(RdrList["Contract_BuyerName"]);
                        entity.Contract_SupplierID = Tools.NullInt(RdrList["Contract_SupplierID"]);
                        entity.Contract_SupplierName = Tools.NullStr(RdrList["Contract_SupplierName"]);
                        entity.Contract_SN = Tools.NullStr(RdrList["Contract_SN"]);
                        entity.Contract_Name = Tools.NullStr(RdrList["Contract_Name"]);
                        entity.Contract_Status = Tools.NullInt(RdrList["Contract_Status"]);
                        entity.Contract_Confirm_Status = Tools.NullInt(RdrList["Contract_Confirm_Status"]);
                        entity.Contract_Payment_Status = Tools.NullInt(RdrList["Contract_Payment_Status"]);
                        entity.Contract_Delivery_Status = Tools.NullInt(RdrList["Contract_Delivery_Status"]);
                        entity.Contract_AllPrice = Tools.NullDbl(RdrList["Contract_AllPrice"]);
                        entity.Contract_Price = Tools.NullDbl(RdrList["Contract_Price"]);
                        entity.Contract_Freight = Tools.NullDbl(RdrList["Contract_Freight"]);
                        entity.Contract_ServiceFee = Tools.NullDbl(RdrList["Contract_ServiceFee"]);
                        entity.Contract_Discount = Tools.NullDbl(RdrList["Contract_Discount"]);
                        entity.Contract_Delivery_ID = Tools.NullInt(RdrList["Contract_Delivery_ID"]);
                        entity.Contract_Delivery_Name = Tools.NullStr(RdrList["Contract_Delivery_Name"]);
                        entity.Contract_Payway_ID = Tools.NullInt(RdrList["Contract_Payway_ID"]);
                        entity.Contract_Payway_Name = Tools.NullStr(RdrList["Contract_Payway_Name"]);
                        entity.Contract_Note = Tools.NullStr(RdrList["Contract_Note"]);
                        entity.Contract_Template = Tools.NullStr(RdrList["Contract_Template"]);
                        entity.Contract_Addtime = Tools.NullDate(RdrList["Contract_Addtime"]);
                        entity.Contract_Site = Tools.NullStr(RdrList["Contract_Site"]);
                        entity.Contract_Source = Tools.NullInt(RdrList["Contract_Source"]);
                        entity.Contract_IsEvaluate = Tools.NullInt(RdrList["Contract_IsEvaluate"]);
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
                SqlTable = "Contract";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Contract_ID) FROM " + SqlTable + SqlParam;

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

        public virtual int GetContractAmount(string Status, string Sn_Front)
        {
            SqlDataReader RdrList = null;
            int contract_amount;
            RdrList = DBHelper.ExecuteReader("Select Count(Contract_ID) As Counter From Contract Where Contract_Status In (" + Status + ") And Contract_Sn like '%" + Sn_Front + "%'");
            if (RdrList.Read())
            {
                contract_amount = Tools.NullInt(RdrList["Counter"]);
            }
            else
            {
                contract_amount = 0;
            }
            RdrList.Close();
            RdrList = null;
            return contract_amount;
        }

        public virtual bool AddContractInvoice(ContractInvoiceInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Contract_Invoice";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Invoice_ID"] = entity.Invoice_ID;
            DrAdd["Invoice_ContractID"] = entity.Invoice_ContractID;
            DrAdd["Invoice_Type"] = entity.Invoice_Type;
            DrAdd["Invoice_Title"] = entity.Invoice_Title;
            DrAdd["Invoice_Content"] = entity.Invoice_Content;
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
            DrAdd["Invoice_Status"] = entity.Invoice_Status;
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

        public virtual bool EditContractInvoice(ContractInvoiceInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Contract_Invoice WHERE Invoice_ID = " + entity.Invoice_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Invoice_ID"] = entity.Invoice_ID;
                    DrAdd["Invoice_ContractID"] = entity.Invoice_ContractID;
                    DrAdd["Invoice_Type"] = entity.Invoice_Type;
                    DrAdd["Invoice_Title"] = entity.Invoice_Title;
                    DrAdd["Invoice_Content"] = entity.Invoice_Content;
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
                    DrAdd["Invoice_Status"] = entity.Invoice_Status;
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

        public virtual int DelContractInvoice(int ID)
        {
            string SqlAdd = "DELETE FROM Contract_Invoice WHERE Invoice_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual ContractInvoiceInfo GetContractInvoiceByID(int ID)
        {
            ContractInvoiceInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Contract_Invoice WHERE Invoice_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new ContractInvoiceInfo();

                    entity.Invoice_ID = Tools.NullInt(RdrList["Invoice_ID"]);
                    entity.Invoice_ContractID = Tools.NullInt(RdrList["Invoice_ContractID"]);
                    entity.Invoice_Type = Tools.NullInt(RdrList["Invoice_Type"]);
                    entity.Invoice_Title = Tools.NullStr(RdrList["Invoice_Title"]);
                    entity.Invoice_Content = Tools.NullInt(RdrList["Invoice_Content"]);
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
                    entity.Invoice_Status = Tools.NullInt(RdrList["Invoice_Status"]);
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

        public virtual ContractInvoiceInfo GetContractInvoiceByContractID(int ID)
        {
            ContractInvoiceInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Contract_Invoice WHERE Invoice_ContractID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new ContractInvoiceInfo();

                    entity.Invoice_ID = Tools.NullInt(RdrList["Invoice_ID"]);
                    entity.Invoice_ContractID = Tools.NullInt(RdrList["Invoice_ContractID"]);
                    entity.Invoice_Type = Tools.NullInt(RdrList["Invoice_Type"]);
                    entity.Invoice_Title = Tools.NullStr(RdrList["Invoice_Title"]);
                    entity.Invoice_Content = Tools.NullInt(RdrList["Invoice_Content"]);
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
                    entity.Invoice_Status = Tools.NullInt(RdrList["Invoice_Status"]);
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

        public virtual bool AddContractInvoiceApply(ContractInvoiceApplyInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Contract_Invoice_Apply";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Invoice_Apply_ID"] = entity.Invoice_Apply_ID;
            DrAdd["Invoice_Apply_ContractID"] = entity.Invoice_Apply_ContractID;
            DrAdd["Invoice_Apply_InvoiceID"] = entity.Invoice_Apply_InvoiceID;
            DrAdd["Invoice_Apply_ApplyAmount"] = entity.Invoice_Apply_ApplyAmount;
            DrAdd["Invoice_Apply_Amount"] = entity.Invoice_Apply_Amount;
            DrAdd["Invoice_Apply_Status"] = entity.Invoice_Apply_Status;
            DrAdd["Invoice_Apply_Note"] = entity.Invoice_Apply_Note;
            DrAdd["Invoice_Apply_Addtime"] = entity.Invoice_Apply_Addtime;

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

        public virtual bool EditContractInvoiceApply(ContractInvoiceApplyInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Contract_Invoice_Apply WHERE Invoice_Apply_ID = " + entity.Invoice_Apply_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Invoice_Apply_ID"] = entity.Invoice_Apply_ID;
                    DrAdd["Invoice_Apply_ContractID"] = entity.Invoice_Apply_ContractID;
                    DrAdd["Invoice_Apply_InvoiceID"] = entity.Invoice_Apply_InvoiceID;
                    DrAdd["Invoice_Apply_ApplyAmount"] = entity.Invoice_Apply_ApplyAmount;
                    DrAdd["Invoice_Apply_Amount"] = entity.Invoice_Apply_Amount;
                    DrAdd["Invoice_Apply_Status"] = entity.Invoice_Apply_Status;
                    DrAdd["Invoice_Apply_Note"] = entity.Invoice_Apply_Note;
                    DrAdd["Invoice_Apply_Addtime"] = entity.Invoice_Apply_Addtime;

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

        public virtual int DelContractInvoiceApply(int ID)
        {
            string SqlAdd = "DELETE FROM Contract_Invoice_Apply WHERE Invoice_Apply_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual ContractInvoiceApplyInfo GetContractInvoiceApplyByID(int ID)
        {
            ContractInvoiceApplyInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Contract_Invoice_Apply WHERE Invoice_Apply_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new ContractInvoiceApplyInfo();

                    entity.Invoice_Apply_ID = Tools.NullInt(RdrList["Invoice_Apply_ID"]);
                    entity.Invoice_Apply_ContractID = Tools.NullInt(RdrList["Invoice_Apply_ContractID"]);
                    entity.Invoice_Apply_InvoiceID = Tools.NullInt(RdrList["Invoice_Apply_InvoiceID"]);
                    entity.Invoice_Apply_ApplyAmount = Tools.NullDbl(RdrList["Invoice_Apply_ApplyAmount"]);
                    entity.Invoice_Apply_Amount = Tools.NullDbl(RdrList["Invoice_Apply_Amount"]);
                    entity.Invoice_Apply_Status = Tools.NullInt(RdrList["Invoice_Apply_Status"]);
                    entity.Invoice_Apply_Note = Tools.NullStr(RdrList["Invoice_Apply_Note"]);
                    entity.Invoice_Apply_Addtime = Tools.NullDate(RdrList["Invoice_Apply_Addtime"]);

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

        public virtual IList<ContractInvoiceApplyInfo> GetContractInvoiceApplysByContractID(int Contract_ID)
        {
            IList<ContractInvoiceApplyInfo> entitys = null;
            ContractInvoiceApplyInfo entity = null;
            string SqlList;
            SqlDataReader RdrList = null;
            try
            {
                SqlList = "Select * From Contract_Invoice_Apply Where Invoice_Apply_ContractID=" + Contract_ID + " Order By Invoice_Apply_ID";
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<ContractInvoiceApplyInfo>();
                    while (RdrList.Read())
                    {
                        entity = new ContractInvoiceApplyInfo();
                        entity.Invoice_Apply_ID = Tools.NullInt(RdrList["Invoice_Apply_ID"]);
                        entity.Invoice_Apply_ContractID = Tools.NullInt(RdrList["Invoice_Apply_ContractID"]);
                        entity.Invoice_Apply_InvoiceID = Tools.NullInt(RdrList["Invoice_Apply_InvoiceID"]);
                        entity.Invoice_Apply_ApplyAmount = Tools.NullDbl(RdrList["Invoice_Apply_ApplyAmount"]);
                        entity.Invoice_Apply_Amount = Tools.NullDbl(RdrList["Invoice_Apply_Amount"]);
                        entity.Invoice_Apply_Status = Tools.NullInt(RdrList["Invoice_Apply_Status"]);
                        entity.Invoice_Apply_Note = Tools.NullStr(RdrList["Invoice_Apply_Note"]);
                        entity.Invoice_Apply_Addtime = Tools.NullDate(RdrList["Invoice_Apply_Addtime"]);

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

        public virtual double Get_Contract_PayedAmount(int Contract_ID)
        {
            string SqlAdd = "Select Sum(Contract_Payment_Amount) as amount FROM Contract_Payment WHERE Contract_Payment_PaymentStatus>0 and Contract_Payment_ContractID = " + Contract_ID;
            try
            {
                return Tools.NullDbl(DBHelper.ExecuteScalar(SqlAdd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

}
