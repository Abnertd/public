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
    public class Supplier : ISupplier
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public Supplier()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddSupplier(SupplierInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Supplier";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Supplier_ID"] = entity.Supplier_ID;
            DrAdd["Supplier_Type"] = entity.Supplier_Type;
            DrAdd["Supplier_GradeID"] = entity.Supplier_GradeID;
            DrAdd["Supplier_Nickname"] = entity.Supplier_Nickname;
            DrAdd["Supplier_Email"] = entity.Supplier_Email;
            DrAdd["Supplier_Password"] = entity.Supplier_Password;
            DrAdd["Supplier_CompanyName"] = entity.Supplier_CompanyName;
            DrAdd["Supplier_County"] = entity.Supplier_County;
            DrAdd["Supplier_City"] = entity.Supplier_City;
            DrAdd["Supplier_State"] = entity.Supplier_State;
            DrAdd["Supplier_Country"] = entity.Supplier_Country;
            DrAdd["Supplier_Address"] = entity.Supplier_Address;
            DrAdd["Supplier_Phone"] = entity.Supplier_Phone;
            DrAdd["Supplier_Fax"] = entity.Supplier_Fax;
            DrAdd["Supplier_Zip"] = entity.Supplier_Zip;
            DrAdd["Supplier_Contactman"] = entity.Supplier_Contactman;
            DrAdd["Supplier_Mobile"] = entity.Supplier_Mobile;
            DrAdd["Supplier_IsHaveShop"] = entity.Supplier_IsHaveShop;
            DrAdd["Supplier_IsApply"] = entity.Supplier_IsApply;
            DrAdd["Supplier_ShopType"] = entity.Supplier_ShopType;
            DrAdd["Supplier_Mode"] = entity.Supplier_Mode;
            DrAdd["Supplier_DeliveryMode"] = entity.Supplier_DeliveryMode;
            DrAdd["Supplier_Account"] = entity.Supplier_Account;
            DrAdd["Supplier_Adv_Account"] = entity.Supplier_Adv_Account;
            DrAdd["Supplier_Security_Account"] = entity.Supplier_Security_Account;
            DrAdd["Supplier_CreditLimit"] = entity.Supplier_CreditLimit;
            DrAdd["Supplier_CreditLimitRemain"] = entity.Supplier_CreditLimitRemain;
            DrAdd["Supplier_CreditLimit_Expires"] = entity.Supplier_CreditLimit_Expires;
            DrAdd["Supplier_TempCreditLimit"] = entity.Supplier_TempCreditLimit;
            DrAdd["Supplier_TempCreditLimitRemain"] = entity.Supplier_TempCreditLimitRemain;
            DrAdd["Supplier_TempCreditLimit_ContractSN"] = entity.Supplier_TempCreditLimit_ContractSN;
            DrAdd["Supplier_TempCreditLimit_Expires"] = entity.Supplier_TempCreditLimit_Expires;
            DrAdd["Supplier_CoinRemain"] = entity.Supplier_CoinRemain;
            DrAdd["Supplier_Status"] = entity.Supplier_Status;
            DrAdd["Supplier_AuditStatus"] = entity.Supplier_AuditStatus;
            DrAdd["Supplier_Cert_Status"] = entity.Supplier_Cert_Status;
            DrAdd["Supplier_CertType"] = entity.Supplier_CertType;
            DrAdd["Supplier_LoginCount"] = entity.Supplier_LoginCount;
            DrAdd["Supplier_LoginIP"] = entity.Supplier_LoginIP;
            DrAdd["Supplier_Lastlogintime"] = entity.Supplier_Lastlogintime;
            DrAdd["Supplier_VerifyCode"] = entity.Supplier_VerifyCode;
            DrAdd["Supplier_RegIP"] = entity.Supplier_RegIP;
            DrAdd["Supplier_Addtime"] = entity.Supplier_Addtime;
            DrAdd["Supplier_AllowSysMessage"] = entity.Supplier_AllowSysMessage;
            DrAdd["Supplier_SysMobile"] = entity.Supplier_SysMobile;
            DrAdd["Supplier_AllowSysEmail"] = entity.Supplier_AllowSysEmail;
            DrAdd["Supplier_SysEmail"] = entity.Supplier_SysEmail;
            DrAdd["Supplier_AllowOrderEmail"] = entity.Supplier_AllowOrderEmail;
            DrAdd["Supplier_Trash"] = entity.Supplier_Trash;
            DrAdd["Supplier_FavorMonth"] = entity.Supplier_FavorMonth;
            DrAdd["Supplier_AgentRate"] = entity.Supplier_AgentRate;
            DrAdd["Supplier_Site"] = entity.Supplier_Site;
            DrAdd["Supplier_Emailverify"] = entity.Supplier_Emailverify;
            DrAdd["Supplier_ContractID"] = entity.Supplier_ContractID;
            DrAdd["Supplier_SealImg"] = entity.Supplier_SealImg;
            DrAdd["Supplier_VfinanceID"] = entity.Supplier_VfinanceID;
            DrAdd["Supplier_Corporate"] = entity.Supplier_Corporate;
            DrAdd["Supplier_CorporateMobile"] = entity.Supplier_CorporateMobile;
            DrAdd["Supplier_RegisterFunds"] = entity.Supplier_RegisterFunds;
            DrAdd["Supplier_BusinessCode"] = entity.Supplier_BusinessCode;
            DrAdd["Supplier_OrganizationCode"] = entity.Supplier_OrganizationCode;
            DrAdd["Supplier_TaxationCode"] = entity.Supplier_TaxationCode;
            DrAdd["Supplier_BankAccountCode"] = entity.Supplier_BankAccountCode;
            DrAdd["Supplier_IsAuthorize"] = entity.Supplier_IsAuthorize;
            DrAdd["Supplier_IsTrademark"] = entity.Supplier_IsTrademark;
            DrAdd["Supplier_ServicesPhone"] = entity.Supplier_ServicesPhone;
            DrAdd["Supplier_OperateYear"] = entity.Supplier_OperateYear;
            DrAdd["Supplier_ContactEmail"] = entity.Supplier_ContactEmail;
            DrAdd["Supplier_ContactQQ"] = entity.Supplier_ContactQQ;
            DrAdd["Supplier_Category"] = entity.Supplier_Category;
            DrAdd["Supplier_SaleType"] = entity.Supplier_SaleType;
            DrAdd["Supplier_MerchantMar_Status"] = entity.Supplier_MerchantMar_Status;

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

        public virtual bool EditSupplier(SupplierInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Supplier WHERE Supplier_ID = " + entity.Supplier_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Supplier_ID"] = entity.Supplier_ID;
                    DrAdd["Supplier_Type"] = entity.Supplier_Type;
                    DrAdd["Supplier_GradeID"] = entity.Supplier_GradeID;
                    DrAdd["Supplier_Nickname"] = entity.Supplier_Nickname;
                    DrAdd["Supplier_Email"] = entity.Supplier_Email;
                    DrAdd["Supplier_Password"] = entity.Supplier_Password;
                    DrAdd["Supplier_CompanyName"] = entity.Supplier_CompanyName;
                    DrAdd["Supplier_County"] = entity.Supplier_County;
                    DrAdd["Supplier_City"] = entity.Supplier_City;
                    DrAdd["Supplier_State"] = entity.Supplier_State;
                    DrAdd["Supplier_Country"] = entity.Supplier_Country;
                    DrAdd["Supplier_Address"] = entity.Supplier_Address;
                    DrAdd["Supplier_Phone"] = entity.Supplier_Phone;
                    DrAdd["Supplier_Fax"] = entity.Supplier_Fax;
                    DrAdd["Supplier_Zip"] = entity.Supplier_Zip;
                    DrAdd["Supplier_Contactman"] = entity.Supplier_Contactman;
                    DrAdd["Supplier_Mobile"] = entity.Supplier_Mobile;
                    DrAdd["Supplier_IsHaveShop"] = entity.Supplier_IsHaveShop;
                    DrAdd["Supplier_IsApply"] = entity.Supplier_IsApply;
                    DrAdd["Supplier_ShopType"] = entity.Supplier_ShopType;
                    DrAdd["Supplier_Mode"] = entity.Supplier_Mode;
                    DrAdd["Supplier_DeliveryMode"] = entity.Supplier_DeliveryMode;
                    DrAdd["Supplier_Account"] = entity.Supplier_Account;
                    DrAdd["Supplier_Adv_Account"] = entity.Supplier_Adv_Account;
                    DrAdd["Supplier_Security_Account"] = entity.Supplier_Security_Account;
                    DrAdd["Supplier_CreditLimit"] = entity.Supplier_CreditLimit;
                    DrAdd["Supplier_CreditLimitRemain"] = entity.Supplier_CreditLimitRemain;
                    DrAdd["Supplier_CreditLimit_Expires"] = entity.Supplier_CreditLimit_Expires;
                    DrAdd["Supplier_TempCreditLimit"] = entity.Supplier_TempCreditLimit;
                    DrAdd["Supplier_TempCreditLimitRemain"] = entity.Supplier_TempCreditLimitRemain;
                    DrAdd["Supplier_TempCreditLimit_ContractSN"] = entity.Supplier_TempCreditLimit_ContractSN;
                    DrAdd["Supplier_TempCreditLimit_Expires"] = entity.Supplier_TempCreditLimit_Expires;
                    DrAdd["Supplier_CoinCount"] = entity.Supplier_CoinCount;
                    DrAdd["Supplier_CoinRemain"] = entity.Supplier_CoinRemain;
                    DrAdd["Supplier_Status"] = entity.Supplier_Status;
                    DrAdd["Supplier_AuditStatus"] = entity.Supplier_AuditStatus;
                    DrAdd["Supplier_Cert_Status"] = entity.Supplier_Cert_Status;
                    DrAdd["Supplier_CertType"] = entity.Supplier_CertType;
                    DrAdd["Supplier_LoginCount"] = entity.Supplier_LoginCount;
                    DrAdd["Supplier_LoginIP"] = entity.Supplier_LoginIP;
                    DrAdd["Supplier_Lastlogintime"] = entity.Supplier_Lastlogintime;
                    DrAdd["Supplier_VerifyCode"] = entity.Supplier_VerifyCode;
                    DrAdd["Supplier_RegIP"] = entity.Supplier_RegIP;
                    DrAdd["Supplier_Addtime"] = entity.Supplier_Addtime;
                    DrAdd["Supplier_AllowSysMessage"] = entity.Supplier_AllowSysMessage;
                    DrAdd["Supplier_SysMobile"] = entity.Supplier_SysMobile;
                    DrAdd["Supplier_AllowSysEmail"] = entity.Supplier_AllowSysEmail;
                    DrAdd["Supplier_SysEmail"] = entity.Supplier_SysEmail;
                    DrAdd["Supplier_AllowOrderEmail"] = entity.Supplier_AllowOrderEmail;
                    DrAdd["Supplier_Trash"] = entity.Supplier_Trash;
                    DrAdd["Supplier_FavorMonth"] = entity.Supplier_FavorMonth;
                    DrAdd["Supplier_AgentRate"] = entity.Supplier_AgentRate;
                    DrAdd["Supplier_Site"] = entity.Supplier_Site;
                    DrAdd["Supplier_Emailverify"] = entity.Supplier_Emailverify;
                    DrAdd["Supplier_ContractID"] = entity.Supplier_ContractID;
                    DrAdd["Supplier_SealImg"] = entity.Supplier_SealImg;
                    DrAdd["Supplier_VfinanceID"] = entity.Supplier_VfinanceID;
                    DrAdd["Supplier_Corporate"] = entity.Supplier_Corporate;
                    DrAdd["Supplier_CorporateMobile"] = entity.Supplier_CorporateMobile;
                    DrAdd["Supplier_RegisterFunds"] = entity.Supplier_RegisterFunds;
                    DrAdd["Supplier_BusinessCode"] = entity.Supplier_BusinessCode;
                    DrAdd["Supplier_OrganizationCode"] = entity.Supplier_OrganizationCode;
                    DrAdd["Supplier_TaxationCode"] = entity.Supplier_TaxationCode;
                    DrAdd["Supplier_BankAccountCode"] = entity.Supplier_BankAccountCode;
                    DrAdd["Supplier_IsAuthorize"] = entity.Supplier_IsAuthorize;
                    DrAdd["Supplier_IsTrademark"] = entity.Supplier_IsTrademark;
                    DrAdd["Supplier_ServicesPhone"] = entity.Supplier_ServicesPhone;
                    DrAdd["Supplier_OperateYear"] = entity.Supplier_OperateYear;
                    DrAdd["Supplier_ContactEmail"] = entity.Supplier_ContactEmail;
                    DrAdd["Supplier_ContactQQ"] = entity.Supplier_ContactQQ;
                    DrAdd["Supplier_Category"] = entity.Supplier_Category;
                    DrAdd["Supplier_SaleType"] = entity.Supplier_SaleType;
                    DrAdd["Supplier_MerchantMar_Status"] = entity.Supplier_MerchantMar_Status;

                    DBHelper.SaveChanges(SqlAdd, DtAdd);

                    if (entity.Supplier_Status != 1)
                    {
                        DBHelper.ExecuteNonQuery("update product_basic set product_isinsale=0 where product_supplierid=" + entity.Supplier_ID);
                    }
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

        public virtual bool UpdateSupplierLogin(int Supplier_ID, int Count, string Remote_IP)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Supplier WHERE Supplier_ID = " + Supplier_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Supplier_LoginCount"] = Count;
                    DrAdd["Supplier_LoginIP"] = Remote_IP;
                    DrAdd["Supplier_Lastlogintime"] = DateTime.Now;

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

        public virtual int DelSupplier(int ID)
        {
            string SqlAdd = "DELETE FROM Supplier WHERE Supplier_ID = " + ID;
            try
            {
                DelSupplierRelateCertBySupplierID(ID);
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual SupplierInfo GetSupplierByID(int ID)
        {
            SupplierInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Supplier WHERE Supplier_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SupplierInfo();

                    entity.Supplier_ID = Tools.NullInt(RdrList["Supplier_ID"]);
                    entity.Supplier_Type = Tools.NullInt(RdrList["Supplier_Type"]);
                    entity.Supplier_GradeID = Tools.NullInt(RdrList["Supplier_GradeID"]);
                    entity.Supplier_Nickname = Tools.NullStr(RdrList["Supplier_Nickname"]);
                    entity.Supplier_Email = Tools.NullStr(RdrList["Supplier_Email"]);
                    entity.Supplier_Password = Tools.NullStr(RdrList["Supplier_Password"]);
                    entity.Supplier_CompanyName = Tools.NullStr(RdrList["Supplier_CompanyName"]);
                    entity.Supplier_County = Tools.NullStr(RdrList["Supplier_County"]);
                    entity.Supplier_City = Tools.NullStr(RdrList["Supplier_City"]);
                    entity.Supplier_State = Tools.NullStr(RdrList["Supplier_State"]);
                    entity.Supplier_Country = Tools.NullStr(RdrList["Supplier_Country"]);
                    entity.Supplier_Address = Tools.NullStr(RdrList["Supplier_Address"]);
                    entity.Supplier_Phone = Tools.NullStr(RdrList["Supplier_Phone"]);
                    entity.Supplier_Fax = Tools.NullStr(RdrList["Supplier_Fax"]);
                    entity.Supplier_Zip = Tools.NullStr(RdrList["Supplier_Zip"]);
                    entity.Supplier_Contactman = Tools.NullStr(RdrList["Supplier_Contactman"]);
                    entity.Supplier_Mobile = Tools.NullStr(RdrList["Supplier_Mobile"]);
                    entity.Supplier_IsHaveShop = Tools.NullInt(RdrList["Supplier_IsHaveShop"]);
                    entity.Supplier_IsApply = Tools.NullInt(RdrList["Supplier_IsApply"]);
                    entity.Supplier_ShopType = Tools.NullInt(RdrList["Supplier_ShopType"]);
                    entity.Supplier_Mode = Tools.NullInt(RdrList["Supplier_Mode"]);
                    entity.Supplier_DeliveryMode = Tools.NullInt(RdrList["Supplier_DeliveryMode"]);
                    entity.Supplier_Account = Tools.NullDbl(RdrList["Supplier_Account"]);
                    entity.Supplier_Adv_Account = Tools.NullDbl(RdrList["Supplier_Adv_Account"]);
                    entity.Supplier_Security_Account = Tools.NullDbl(RdrList["Supplier_Security_Account"]);
                    entity.Supplier_CreditLimit = Tools.NullDbl(RdrList["Supplier_CreditLimit"]);
                    entity.Supplier_CreditLimitRemain = Tools.NullDbl(RdrList["Supplier_CreditLimitRemain"]);
                    entity.Supplier_CreditLimit_Expires = Tools.NullInt(RdrList["Supplier_CreditLimit_Expires"]);
                    entity.Supplier_TempCreditLimit = Tools.NullDbl(RdrList["Supplier_TempCreditLimit"]);
                    entity.Supplier_TempCreditLimitRemain = Tools.NullDbl(RdrList["Supplier_TempCreditLimitRemain"]);
                    entity.Supplier_TempCreditLimit_ContractSN = Tools.NullStr(RdrList["Supplier_TempCreditLimit_ContractSN"]);
                    entity.Supplier_TempCreditLimit_Expires = Tools.NullInt(RdrList["Supplier_TempCreditLimit_Expires"]);
                    entity.Supplier_CoinCount = Tools.NullInt(RdrList["Supplier_CoinCount"]);
                    entity.Supplier_CoinRemain = Tools.NullInt(RdrList["Supplier_CoinRemain"]);
                    entity.Supplier_Status = Tools.NullInt(RdrList["Supplier_Status"]);
                    entity.Supplier_AuditStatus = Tools.NullInt(RdrList["Supplier_AuditStatus"]);
                    entity.Supplier_Cert_Status = Tools.NullInt(RdrList["Supplier_Cert_Status"]);
                    entity.Supplier_CertType = Tools.NullInt(RdrList["Supplier_CertType"]);
                    entity.Supplier_LoginCount = Tools.NullInt(RdrList["Supplier_LoginCount"]);
                    entity.Supplier_LoginIP = Tools.NullStr(RdrList["Supplier_LoginIP"]);
                    entity.Supplier_Addtime = Tools.NullDate(RdrList["Supplier_Addtime"]);
                    entity.Supplier_Lastlogintime = Tools.NullDate(RdrList["Supplier_Lastlogintime"]);
                    entity.Supplier_VerifyCode = Tools.NullStr(RdrList["Supplier_VerifyCode"]);
                    entity.Supplier_RegIP = Tools.NullStr(RdrList["Supplier_RegIP"]);
                    entity.Supplier_Trash = Tools.NullInt(RdrList["Supplier_Trash"]);
                    entity.Supplier_FavorMonth = Tools.NullInt(RdrList["Supplier_FavorMonth"]);
                    entity.Supplier_AllowOrderEmail = Tools.NullInt(RdrList["Supplier_AllowOrderEmail"]);
                    entity.Supplier_Site = Tools.NullStr(RdrList["Supplier_Site"]);
                    entity.Supplier_AgentRate = Tools.NullDbl(RdrList["Supplier_AgentRate"]);
                    entity.SupplierRelateCertInfos = GetSupplierRelateCerts(entity.Supplier_ID);

                    entity.Supplier_AllowSysMessage = Tools.NullInt(RdrList["Supplier_AllowSysMessage"]);
                    entity.Supplier_AllowSysEmail = Tools.NullInt(RdrList["Supplier_AllowSysEmail"]);
                    entity.Supplier_SysMobile = Tools.NullStr(RdrList["Supplier_SysMobile"]);

                    entity.Supplier_SysEmail = Tools.NullStr(RdrList["Supplier_SysEmail"]);
                    entity.Supplier_Emailverify = Tools.NullInt(RdrList["Supplier_Emailverify"]);
                    entity.Supplier_ContractID = Tools.NullInt(RdrList["Supplier_ContractID"]);
                    entity.Supplier_SealImg = Tools.NullStr(RdrList["Supplier_SealImg"]);
                    entity.Supplier_VfinanceID = Tools.NullStr(RdrList["Supplier_VfinanceID"]);
                    entity.Supplier_Corporate = Tools.NullStr(RdrList["Supplier_Corporate"]);
                    entity.Supplier_CorporateMobile = Tools.NullStr(RdrList["Supplier_CorporateMobile"]);
                    entity.Supplier_RegisterFunds = Tools.NullDbl(RdrList["Supplier_RegisterFunds"]);
                    entity.Supplier_BusinessCode = Tools.NullStr(RdrList["Supplier_BusinessCode"]);
                    entity.Supplier_OrganizationCode = Tools.NullStr(RdrList["Supplier_OrganizationCode"]);
                    entity.Supplier_TaxationCode = Tools.NullStr(RdrList["Supplier_TaxationCode"]);
                    entity.Supplier_BankAccountCode = Tools.NullStr(RdrList["Supplier_BankAccountCode"]);
                    entity.Supplier_IsAuthorize = Tools.NullInt(RdrList["Supplier_IsAuthorize"]);
                    entity.Supplier_IsTrademark = Tools.NullInt(RdrList["Supplier_IsTrademark"]);
                    entity.Supplier_ServicesPhone = Tools.NullStr(RdrList["Supplier_ServicesPhone"]);
                    entity.Supplier_OperateYear = Tools.NullInt(RdrList["Supplier_OperateYear"]);
                    entity.Supplier_ContactEmail = Tools.NullStr(RdrList["Supplier_ContactEmail"]);
                    entity.Supplier_ContactQQ = Tools.NullStr(RdrList["Supplier_ContactQQ"]);
                    entity.Supplier_Category = Tools.NullStr(RdrList["Supplier_Category"]);
                    entity.Supplier_SaleType = Tools.NullInt(RdrList["Supplier_SaleType"]);
                    entity.Supplier_MerchantMar_Status = Tools.NullInt(RdrList["Supplier_MerchantMar_Status"]);
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

        public virtual SupplierInfo GetSupplierByEmail(string Email)
        {
            SupplierInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Supplier WHERE Supplier_Email = '" + Email + "'";
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SupplierInfo();

                    entity.Supplier_ID = Tools.NullInt(RdrList["Supplier_ID"]);
                    entity.Supplier_Type = Tools.NullInt(RdrList["Supplier_Type"]);
                    entity.Supplier_GradeID = Tools.NullInt(RdrList["Supplier_GradeID"]);
                    entity.Supplier_Nickname = Tools.NullStr(RdrList["Supplier_Nickname"]);
                    entity.Supplier_Email = Tools.NullStr(RdrList["Supplier_Email"]);
                    entity.Supplier_Password = Tools.NullStr(RdrList["Supplier_Password"]);
                    entity.Supplier_CompanyName = Tools.NullStr(RdrList["Supplier_CompanyName"]);
                    entity.Supplier_County = Tools.NullStr(RdrList["Supplier_County"]);
                    entity.Supplier_City = Tools.NullStr(RdrList["Supplier_City"]);
                    entity.Supplier_State = Tools.NullStr(RdrList["Supplier_State"]);
                    entity.Supplier_Country = Tools.NullStr(RdrList["Supplier_Country"]);
                    entity.Supplier_Address = Tools.NullStr(RdrList["Supplier_Address"]);
                    entity.Supplier_Phone = Tools.NullStr(RdrList["Supplier_Phone"]);
                    entity.Supplier_Fax = Tools.NullStr(RdrList["Supplier_Fax"]);
                    entity.Supplier_Zip = Tools.NullStr(RdrList["Supplier_Zip"]);
                    entity.Supplier_Contactman = Tools.NullStr(RdrList["Supplier_Contactman"]);
                    entity.Supplier_Mobile = Tools.NullStr(RdrList["Supplier_Mobile"]);
                    entity.Supplier_IsHaveShop = Tools.NullInt(RdrList["Supplier_IsHaveShop"]);
                    entity.Supplier_IsApply = Tools.NullInt(RdrList["Supplier_IsApply"]);
                    entity.Supplier_ShopType = Tools.NullInt(RdrList["Supplier_ShopType"]);
                    entity.Supplier_Mode = Tools.NullInt(RdrList["Supplier_Mode"]);
                    entity.Supplier_DeliveryMode = Tools.NullInt(RdrList["Supplier_DeliveryMode"]);
                    entity.Supplier_Account = Tools.NullDbl(RdrList["Supplier_Account"]);
                    entity.Supplier_Adv_Account = Tools.NullDbl(RdrList["Supplier_Adv_Account"]);
                    entity.Supplier_Security_Account = Tools.NullDbl(RdrList["Supplier_Security_Account"]);
                    entity.Supplier_CreditLimit = Tools.NullDbl(RdrList["Supplier_CreditLimit"]);
                    entity.Supplier_CreditLimitRemain = Tools.NullDbl(RdrList["Supplier_CreditLimitRemain"]);
                    entity.Supplier_CreditLimit_Expires = Tools.NullInt(RdrList["Supplier_CreditLimit_Expires"]);
                    entity.Supplier_TempCreditLimit = Tools.NullDbl(RdrList["Supplier_TempCreditLimit"]);
                    entity.Supplier_TempCreditLimitRemain = Tools.NullDbl(RdrList["Supplier_TempCreditLimitRemain"]);
                    entity.Supplier_TempCreditLimit_ContractSN = Tools.NullStr(RdrList["Supplier_TempCreditLimit_ContractSN"]);
                    entity.Supplier_TempCreditLimit_Expires = Tools.NullInt(RdrList["Supplier_TempCreditLimit_Expires"]);
                    entity.Supplier_CoinCount = Tools.NullInt(RdrList["Supplier_CoinCount"]);
                    entity.Supplier_CoinRemain = Tools.NullInt(RdrList["Supplier_CoinRemain"]);
                    entity.Supplier_Status = Tools.NullInt(RdrList["Supplier_Status"]);
                    entity.Supplier_AuditStatus = Tools.NullInt(RdrList["Supplier_AuditStatus"]);
                    entity.Supplier_Cert_Status = Tools.NullInt(RdrList["Supplier_Cert_Status"]);
                    entity.Supplier_CertType = Tools.NullInt(RdrList["Supplier_CertType"]);
                    entity.Supplier_LoginCount = Tools.NullInt(RdrList["Supplier_LoginCount"]);
                    entity.Supplier_LoginIP = Tools.NullStr(RdrList["Supplier_LoginIP"]);
                    entity.Supplier_Addtime = Tools.NullDate(RdrList["Supplier_Addtime"]);
                    entity.Supplier_Lastlogintime = Tools.NullDate(RdrList["Supplier_Lastlogintime"]);
                    entity.Supplier_VerifyCode = Tools.NullStr(RdrList["Supplier_VerifyCode"]);
                    entity.Supplier_RegIP = Tools.NullStr(RdrList["Supplier_RegIP"]);
                    entity.Supplier_Trash = Tools.NullInt(RdrList["Supplier_Trash"]);
                    entity.Supplier_FavorMonth = Tools.NullInt(RdrList["Supplier_FavorMonth"]);
                    entity.Supplier_AgentRate = Tools.NullDbl(RdrList["Supplier_AgentRate"]);
                    entity.Supplier_AllowOrderEmail = Tools.NullInt(RdrList["Supplier_AllowOrderEmail"]);
                    entity.Supplier_Site = Tools.NullStr(RdrList["Supplier_Site"]);
                    entity.SupplierRelateCertInfos = GetSupplierRelateCerts(entity.Supplier_ID);

                    entity.Supplier_AllowSysMessage = Tools.NullInt(RdrList["Supplier_AllowSysMessage"]);
                    entity.Supplier_AllowSysEmail = Tools.NullInt(RdrList["Supplier_AllowSysEmail"]);
                    entity.Supplier_SysMobile = Tools.NullStr(RdrList["Supplier_SysMobile"]);
                    entity.Supplier_SysEmail = Tools.NullStr(RdrList["Supplier_SysEmail"]);
                    entity.Supplier_Emailverify = Tools.NullInt(RdrList["Supplier_Emailverify"]);
                    entity.Supplier_ContractID = Tools.NullInt(RdrList["Supplier_ContractID"]);
                    entity.Supplier_SealImg = Tools.NullStr(RdrList["Supplier_SealImg"]);
                    entity.Supplier_VfinanceID = Tools.NullStr(RdrList["Supplier_VfinanceID"]);
                    entity.Supplier_Corporate = Tools.NullStr(RdrList["Supplier_Corporate"]);
                    entity.Supplier_CorporateMobile = Tools.NullStr(RdrList["Supplier_CorporateMobile"]);
                    entity.Supplier_RegisterFunds = Tools.NullDbl(RdrList["Supplier_RegisterFunds"]);
                    entity.Supplier_BusinessCode = Tools.NullStr(RdrList["Supplier_BusinessCode"]);
                    entity.Supplier_OrganizationCode = Tools.NullStr(RdrList["Supplier_OrganizationCode"]);
                    entity.Supplier_TaxationCode = Tools.NullStr(RdrList["Supplier_TaxationCode"]);
                    entity.Supplier_BankAccountCode = Tools.NullStr(RdrList["Supplier_BankAccountCode"]);
                    entity.Supplier_IsAuthorize = Tools.NullInt(RdrList["Supplier_IsAuthorize"]);
                    entity.Supplier_IsTrademark = Tools.NullInt(RdrList["Supplier_IsTrademark"]);
                    entity.Supplier_ServicesPhone = Tools.NullStr(RdrList["Supplier_ServicesPhone"]);
                    entity.Supplier_OperateYear = Tools.NullInt(RdrList["Supplier_OperateYear"]);
                    entity.Supplier_ContactEmail = Tools.NullStr(RdrList["Supplier_ContactEmail"]);
                    entity.Supplier_ContactQQ = Tools.NullStr(RdrList["Supplier_ContactQQ"]);
                    entity.Supplier_Category = Tools.NullStr(RdrList["Supplier_Category"]);
                    entity.Supplier_SaleType = Tools.NullInt(RdrList["Supplier_SaleType"]);
                    entity.Supplier_MerchantMar_Status = Tools.NullInt(RdrList["Supplier_MerchantMar_Status"]);
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

        public virtual SupplierInfo SupplierLogin(string name)
        {
            SupplierInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Supplier WHERE Supplier_Email = '" + name + "' OR Supplier_Nickname='" + name + "' OR Supplier_Mobile = '" + name + "'";
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SupplierInfo();

                    entity.Supplier_ID = Tools.NullInt(RdrList["Supplier_ID"]);
                    entity.Supplier_Type = Tools.NullInt(RdrList["Supplier_Type"]);
                    entity.Supplier_GradeID = Tools.NullInt(RdrList["Supplier_GradeID"]);
                    entity.Supplier_Nickname = Tools.NullStr(RdrList["Supplier_Nickname"]);
                    entity.Supplier_Email = Tools.NullStr(RdrList["Supplier_Email"]);
                    entity.Supplier_Password = Tools.NullStr(RdrList["Supplier_Password"]);
                    entity.Supplier_CompanyName = Tools.NullStr(RdrList["Supplier_CompanyName"]);
                    entity.Supplier_County = Tools.NullStr(RdrList["Supplier_County"]);
                    entity.Supplier_City = Tools.NullStr(RdrList["Supplier_City"]);
                    entity.Supplier_State = Tools.NullStr(RdrList["Supplier_State"]);
                    entity.Supplier_Country = Tools.NullStr(RdrList["Supplier_Country"]);
                    entity.Supplier_Address = Tools.NullStr(RdrList["Supplier_Address"]);
                    entity.Supplier_Phone = Tools.NullStr(RdrList["Supplier_Phone"]);
                    entity.Supplier_Fax = Tools.NullStr(RdrList["Supplier_Fax"]);
                    entity.Supplier_Zip = Tools.NullStr(RdrList["Supplier_Zip"]);
                    entity.Supplier_Contactman = Tools.NullStr(RdrList["Supplier_Contactman"]);
                    entity.Supplier_Mobile = Tools.NullStr(RdrList["Supplier_Mobile"]);
                    entity.Supplier_IsHaveShop = Tools.NullInt(RdrList["Supplier_IsHaveShop"]);
                    entity.Supplier_IsApply = Tools.NullInt(RdrList["Supplier_IsApply"]);
                    entity.Supplier_ShopType = Tools.NullInt(RdrList["Supplier_ShopType"]);
                    entity.Supplier_Mode = Tools.NullInt(RdrList["Supplier_Mode"]);
                    entity.Supplier_DeliveryMode = Tools.NullInt(RdrList["Supplier_DeliveryMode"]);
                    entity.Supplier_Account = Tools.NullDbl(RdrList["Supplier_Account"]);
                    entity.Supplier_Adv_Account = Tools.NullDbl(RdrList["Supplier_Adv_Account"]);
                    entity.Supplier_Security_Account = Tools.NullDbl(RdrList["Supplier_Security_Account"]);
                    entity.Supplier_CreditLimit = Tools.NullDbl(RdrList["Supplier_CreditLimit"]);
                    entity.Supplier_CreditLimitRemain = Tools.NullDbl(RdrList["Supplier_CreditLimitRemain"]);
                    entity.Supplier_CreditLimit_Expires = Tools.NullInt(RdrList["Supplier_CreditLimit_Expires"]);
                    entity.Supplier_TempCreditLimit = Tools.NullDbl(RdrList["Supplier_TempCreditLimit"]);
                    entity.Supplier_TempCreditLimitRemain = Tools.NullDbl(RdrList["Supplier_TempCreditLimitRemain"]);
                    entity.Supplier_TempCreditLimit_ContractSN = Tools.NullStr(RdrList["Supplier_TempCreditLimit_ContractSN"]);
                    entity.Supplier_TempCreditLimit_Expires = Tools.NullInt(RdrList["Supplier_TempCreditLimit_Expires"]);
                    entity.Supplier_CoinCount = Tools.NullInt(RdrList["Supplier_CoinCount"]);
                    entity.Supplier_CoinRemain = Tools.NullInt(RdrList["Supplier_CoinRemain"]);
                    entity.Supplier_Status = Tools.NullInt(RdrList["Supplier_Status"]);
                    entity.Supplier_AuditStatus = Tools.NullInt(RdrList["Supplier_AuditStatus"]);
                    entity.Supplier_Cert_Status = Tools.NullInt(RdrList["Supplier_Cert_Status"]);
                    entity.Supplier_CertType = Tools.NullInt(RdrList["Supplier_CertType"]);
                    entity.Supplier_LoginCount = Tools.NullInt(RdrList["Supplier_LoginCount"]);
                    entity.Supplier_LoginIP = Tools.NullStr(RdrList["Supplier_LoginIP"]);
                    entity.Supplier_Addtime = Tools.NullDate(RdrList["Supplier_Addtime"]);
                    entity.Supplier_Lastlogintime = Tools.NullDate(RdrList["Supplier_Lastlogintime"]);
                    entity.Supplier_VerifyCode = Tools.NullStr(RdrList["Supplier_VerifyCode"]);
                    entity.Supplier_RegIP = Tools.NullStr(RdrList["Supplier_RegIP"]);
                    entity.Supplier_Trash = Tools.NullInt(RdrList["Supplier_Trash"]);
                    entity.Supplier_FavorMonth = Tools.NullInt(RdrList["Supplier_FavorMonth"]);
                    entity.Supplier_AgentRate = Tools.NullDbl(RdrList["Supplier_AgentRate"]);
                    entity.Supplier_AllowOrderEmail = Tools.NullInt(RdrList["Supplier_AllowOrderEmail"]);
                    entity.Supplier_Site = Tools.NullStr(RdrList["Supplier_Site"]);
                    entity.SupplierRelateCertInfos = GetSupplierRelateCerts(entity.Supplier_ID);

                    entity.Supplier_AllowSysMessage = Tools.NullInt(RdrList["Supplier_AllowSysMessage"]);
                    entity.Supplier_AllowSysEmail = Tools.NullInt(RdrList["Supplier_AllowSysEmail"]);
                    entity.Supplier_SysMobile = Tools.NullStr(RdrList["Supplier_SysMobile"]);
                    entity.Supplier_SysEmail = Tools.NullStr(RdrList["Supplier_SysEmail"]);
                    entity.Supplier_Emailverify = Tools.NullInt(RdrList["Supplier_Emailverify"]);
                    entity.Supplier_ContractID = Tools.NullInt(RdrList["Supplier_ContractID"]);
                    entity.Supplier_SealImg = Tools.NullStr(RdrList["Supplier_SealImg"]);
                    entity.Supplier_VfinanceID = Tools.NullStr(RdrList["Supplier_VfinanceID"]);
                    entity.Supplier_Corporate = Tools.NullStr(RdrList["Supplier_Corporate"]);
                    entity.Supplier_CorporateMobile = Tools.NullStr(RdrList["Supplier_CorporateMobile"]);
                    entity.Supplier_RegisterFunds = Tools.NullDbl(RdrList["Supplier_RegisterFunds"]);
                    entity.Supplier_BusinessCode = Tools.NullStr(RdrList["Supplier_BusinessCode"]);
                    entity.Supplier_OrganizationCode = Tools.NullStr(RdrList["Supplier_OrganizationCode"]);
                    entity.Supplier_TaxationCode = Tools.NullStr(RdrList["Supplier_TaxationCode"]);
                    entity.Supplier_BankAccountCode = Tools.NullStr(RdrList["Supplier_BankAccountCode"]);
                    entity.Supplier_IsAuthorize = Tools.NullInt(RdrList["Supplier_IsAuthorize"]);
                    entity.Supplier_IsTrademark = Tools.NullInt(RdrList["Supplier_IsTrademark"]);
                    entity.Supplier_ServicesPhone = Tools.NullStr(RdrList["Supplier_ServicesPhone"]);
                    entity.Supplier_OperateYear = Tools.NullInt(RdrList["Supplier_OperateYear"]);
                    entity.Supplier_ContactEmail = Tools.NullStr(RdrList["Supplier_ContactEmail"]);
                    entity.Supplier_ContactQQ = Tools.NullStr(RdrList["Supplier_ContactQQ"]);
                    entity.Supplier_Category = Tools.NullStr(RdrList["Supplier_Category"]);
                    entity.Supplier_SaleType = Tools.NullInt(RdrList["Supplier_SaleType"]);
                    entity.Supplier_MerchantMar_Status = Tools.NullInt(RdrList["Supplier_MerchantMar_Status"]);
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

        public virtual IList<SupplierInfo> GetSuppliers(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<SupplierInfo> entitys = null;

            SupplierInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Supplier";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SupplierInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SupplierInfo();
                        entity.Supplier_ID = Tools.NullInt(RdrList["Supplier_ID"]);
                        entity.Supplier_Type = Tools.NullInt(RdrList["Supplier_Type"]);
                        entity.Supplier_GradeID = Tools.NullInt(RdrList["Supplier_GradeID"]);
                        entity.Supplier_Nickname = Tools.NullStr(RdrList["Supplier_Nickname"]);
                        entity.Supplier_Email = Tools.NullStr(RdrList["Supplier_Email"]);
                        entity.Supplier_Password = Tools.NullStr(RdrList["Supplier_Password"]);
                        entity.Supplier_CompanyName = Tools.NullStr(RdrList["Supplier_CompanyName"]);
                        entity.Supplier_County = Tools.NullStr(RdrList["Supplier_County"]);
                        entity.Supplier_City = Tools.NullStr(RdrList["Supplier_City"]);
                        entity.Supplier_State = Tools.NullStr(RdrList["Supplier_State"]);
                        entity.Supplier_Country = Tools.NullStr(RdrList["Supplier_Country"]);
                        entity.Supplier_Address = Tools.NullStr(RdrList["Supplier_Address"]);
                        entity.Supplier_Phone = Tools.NullStr(RdrList["Supplier_Phone"]);
                        entity.Supplier_Fax = Tools.NullStr(RdrList["Supplier_Fax"]);
                        entity.Supplier_Zip = Tools.NullStr(RdrList["Supplier_Zip"]);
                        entity.Supplier_Contactman = Tools.NullStr(RdrList["Supplier_Contactman"]);
                        entity.Supplier_Mobile = Tools.NullStr(RdrList["Supplier_Mobile"]);
                        entity.Supplier_IsHaveShop = Tools.NullInt(RdrList["Supplier_IsHaveShop"]);
                        entity.Supplier_IsApply = Tools.NullInt(RdrList["Supplier_IsApply"]);
                        entity.Supplier_ShopType = Tools.NullInt(RdrList["Supplier_ShopType"]);
                        entity.Supplier_Mode = Tools.NullInt(RdrList["Supplier_Mode"]);
                        entity.Supplier_DeliveryMode = Tools.NullInt(RdrList["Supplier_DeliveryMode"]);
                        entity.Supplier_Account = Tools.NullDbl(RdrList["Supplier_Account"]);
                        entity.Supplier_Adv_Account = Tools.NullDbl(RdrList["Supplier_Adv_Account"]);
                        entity.Supplier_Security_Account = Tools.NullDbl(RdrList["Supplier_Security_Account"]);
                        entity.Supplier_CreditLimit = Tools.NullDbl(RdrList["Supplier_CreditLimit"]);
                        entity.Supplier_CreditLimitRemain = Tools.NullDbl(RdrList["Supplier_CreditLimitRemain"]);
                        entity.Supplier_CreditLimit_Expires = Tools.NullInt(RdrList["Supplier_CreditLimit_Expires"]);
                        entity.Supplier_TempCreditLimit = Tools.NullDbl(RdrList["Supplier_TempCreditLimit"]);
                        entity.Supplier_TempCreditLimitRemain = Tools.NullDbl(RdrList["Supplier_TempCreditLimitRemain"]);
                        entity.Supplier_TempCreditLimit_ContractSN = Tools.NullStr(RdrList["Supplier_TempCreditLimit_ContractSN"]);
                        entity.Supplier_TempCreditLimit_Expires = Tools.NullInt(RdrList["Supplier_TempCreditLimit_Expires"]);
                        entity.Supplier_CoinCount = Tools.NullInt(RdrList["Supplier_CoinCount"]);
                        entity.Supplier_CoinRemain = Tools.NullInt(RdrList["Supplier_CoinRemain"]);
                        entity.Supplier_Status = Tools.NullInt(RdrList["Supplier_Status"]);
                        entity.Supplier_AuditStatus = Tools.NullInt(RdrList["Supplier_AuditStatus"]);
                        entity.Supplier_Cert_Status = Tools.NullInt(RdrList["Supplier_Cert_Status"]);
                        entity.Supplier_CertType = Tools.NullInt(RdrList["Supplier_CertType"]);
                        entity.Supplier_LoginCount = Tools.NullInt(RdrList["Supplier_LoginCount"]);
                        entity.Supplier_LoginIP = Tools.NullStr(RdrList["Supplier_LoginIP"]);
                        entity.Supplier_Addtime = Tools.NullDate(RdrList["Supplier_Addtime"]);
                        entity.Supplier_Lastlogintime = Tools.NullDate(RdrList["Supplier_Lastlogintime"]);
                        entity.Supplier_VerifyCode = Tools.NullStr(RdrList["Supplier_VerifyCode"]);
                        entity.Supplier_RegIP = Tools.NullStr(RdrList["Supplier_RegIP"]);
                        entity.Supplier_Trash = Tools.NullInt(RdrList["Supplier_Trash"]);
                        entity.Supplier_FavorMonth = Tools.NullInt(RdrList["Supplier_FavorMonth"]);
                        entity.Supplier_AgentRate = Tools.NullDbl(RdrList["Supplier_AgentRate"]);
                        entity.Supplier_AllowOrderEmail = Tools.NullInt(RdrList["Supplier_AllowOrderEmail"]);
                        entity.Supplier_Site = Tools.NullStr(RdrList["Supplier_Site"]);
                        entity.SupplierRelateCertInfos = GetSupplierRelateCerts(entity.Supplier_ID);


                        entity.Supplier_AllowSysMessage = Tools.NullInt(RdrList["Supplier_AllowSysMessage"]);
                        entity.Supplier_AllowSysEmail = Tools.NullInt(RdrList["Supplier_AllowSysEmail"]);
                        entity.Supplier_SysMobile = Tools.NullStr(RdrList["Supplier_SysMobile"]);
                        entity.Supplier_SysEmail = Tools.NullStr(RdrList["Supplier_SysEmail"]);
                        entity.Supplier_Emailverify = Tools.NullInt(RdrList["Supplier_Emailverify"]);
                        entity.Supplier_ContractID = Tools.NullInt(RdrList["Supplier_ContractID"]);
                        entity.Supplier_SealImg = Tools.NullStr(RdrList["Supplier_SealImg"]);
                        entity.Supplier_VfinanceID = Tools.NullStr(RdrList["Supplier_VfinanceID"]);
                        entity.Supplier_Corporate = Tools.NullStr(RdrList["Supplier_Corporate"]);
                        entity.Supplier_CorporateMobile = Tools.NullStr(RdrList["Supplier_CorporateMobile"]);
                        entity.Supplier_RegisterFunds = Tools.NullDbl(RdrList["Supplier_RegisterFunds"]);
                        entity.Supplier_BusinessCode = Tools.NullStr(RdrList["Supplier_BusinessCode"]);
                        entity.Supplier_OrganizationCode = Tools.NullStr(RdrList["Supplier_OrganizationCode"]);
                        entity.Supplier_TaxationCode = Tools.NullStr(RdrList["Supplier_TaxationCode"]);
                        entity.Supplier_BankAccountCode = Tools.NullStr(RdrList["Supplier_BankAccountCode"]);
                        entity.Supplier_IsAuthorize = Tools.NullInt(RdrList["Supplier_IsAuthorize"]);
                        entity.Supplier_IsTrademark = Tools.NullInt(RdrList["Supplier_IsTrademark"]);
                        entity.Supplier_ServicesPhone = Tools.NullStr(RdrList["Supplier_ServicesPhone"]);
                        entity.Supplier_OperateYear = Tools.NullInt(RdrList["Supplier_OperateYear"]);
                        entity.Supplier_ContactEmail = Tools.NullStr(RdrList["Supplier_ContactEmail"]);
                        entity.Supplier_ContactQQ = Tools.NullStr(RdrList["Supplier_ContactQQ"]);
                        entity.Supplier_Category = Tools.NullStr(RdrList["Supplier_Category"]);
                        entity.Supplier_SaleType = Tools.NullInt(RdrList["Supplier_SaleType"]);
                        entity.Supplier_MerchantMar_Status = Tools.NullInt(RdrList["Supplier_MerchantMar_Status"]);

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
                SqlTable = "Supplier";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Supplier_ID) FROM " + SqlTable + SqlParam;

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

        public virtual bool EditSupplierDeliveryFee(SupplierDeliveryFeeInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Supplier_Delivery_Fee WHERE Supplier_Delivery_Fee_DeliveryID=" + entity.Supplier_Delivery_Fee_DeliveryID + " And Supplier_Delivery_Fee_SupplierID = " + entity.Supplier_Delivery_Fee_SupplierID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Supplier_Delivery_Fee_ID"] = entity.Supplier_Delivery_Fee_ID;
                    DrAdd["Supplier_Delivery_Fee_SupplierID"] = entity.Supplier_Delivery_Fee_SupplierID;
                    DrAdd["Supplier_Delivery_Fee_DeliveryID"] = entity.Supplier_Delivery_Fee_DeliveryID;
                    DrAdd["Supplier_Delivery_Fee_Amount"] = entity.Supplier_Delivery_Fee_Amount;
                    DrAdd["Supplier_Delivery_Fee_Type"] = entity.Supplier_Delivery_Fee_Type;
                    DrAdd["Supplier_Delivery_Fee_InitialWeight"] = entity.Supplier_Delivery_Fee_InitialWeight;
                    DrAdd["Supplier_Delivery_Fee_UpWeight"] = entity.Supplier_Delivery_Fee_UpWeight;
                    DrAdd["Supplier_Delivery_Fee_InitialFee"] = entity.Supplier_Delivery_Fee_InitialFee;
                    DrAdd["Supplier_Delivery_Fee_UpFee"] = entity.Supplier_Delivery_Fee_UpFee;

                    DBHelper.SaveChanges(SqlAdd, DtAdd);
                }
                else
                {
                    DrAdd = DtAdd.NewRow();

                    DrAdd["Supplier_Delivery_Fee_ID"] = entity.Supplier_Delivery_Fee_ID;
                    DrAdd["Supplier_Delivery_Fee_SupplierID"] = entity.Supplier_Delivery_Fee_SupplierID;
                    DrAdd["Supplier_Delivery_Fee_DeliveryID"] = entity.Supplier_Delivery_Fee_DeliveryID;
                    DrAdd["Supplier_Delivery_Fee_Amount"] = entity.Supplier_Delivery_Fee_Amount;
                    DrAdd["Supplier_Delivery_Fee_Type"] = entity.Supplier_Delivery_Fee_Type;
                    DrAdd["Supplier_Delivery_Fee_InitialWeight"] = entity.Supplier_Delivery_Fee_InitialWeight;
                    DrAdd["Supplier_Delivery_Fee_UpWeight"] = entity.Supplier_Delivery_Fee_UpWeight;
                    DrAdd["Supplier_Delivery_Fee_InitialFee"] = entity.Supplier_Delivery_Fee_InitialFee;
                    DrAdd["Supplier_Delivery_Fee_UpFee"] = entity.Supplier_Delivery_Fee_UpFee;

                    DtAdd.Rows.Add(DrAdd);
                    DBHelper.SaveChanges(SqlAdd, DtAdd);
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

        public virtual int DelSupplierDeliveryFee(int Supplier_ID, int Delivery_ID)
        {
            string SqlAdd = "DELETE FROM Supplier_Delivery_Fee WHERE Supplier_Delivery_Fee_DeliveryID = " + Delivery_ID + " OR Supplier_Delivery_Fee_SupplierID=" + Supplier_ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual SupplierDeliveryFeeInfo GetSupplierDeliveryFeeByID(int Supplier_ID, int Delivery_ID)
        {
            SupplierDeliveryFeeInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Supplier_Delivery_Fee WHERE Supplier_Delivery_Fee_DeliveryID = " + Delivery_ID + " And Supplier_Delivery_Fee_SupplierID=" + Supplier_ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SupplierDeliveryFeeInfo();

                    entity.Supplier_Delivery_Fee_ID = Tools.NullInt(RdrList["Supplier_Delivery_Fee_ID"]);
                    entity.Supplier_Delivery_Fee_SupplierID = Tools.NullInt(RdrList["Supplier_Delivery_Fee_SupplierID"]);
                    entity.Supplier_Delivery_Fee_DeliveryID = Tools.NullInt(RdrList["Supplier_Delivery_Fee_DeliveryID"]);
                    entity.Supplier_Delivery_Fee_Amount = Tools.NullDbl(RdrList["Supplier_Delivery_Fee_Amount"]);
                    entity.Supplier_Delivery_Fee_Type = Tools.NullInt(RdrList["Supplier_Delivery_Fee_Type"]);
                    entity.Supplier_Delivery_Fee_InitialWeight = Tools.NullInt(RdrList["Supplier_Delivery_Fee_InitialWeight"]);
                    entity.Supplier_Delivery_Fee_UpWeight = Tools.NullInt(RdrList["Supplier_Delivery_Fee_UpWeight"]);
                    entity.Supplier_Delivery_Fee_InitialFee = Tools.NullDbl(RdrList["Supplier_Delivery_Fee_InitialFee"]);
                    entity.Supplier_Delivery_Fee_UpFee = Tools.NullDbl(RdrList["Supplier_Delivery_Fee_UpFee"]);

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

        public virtual bool AddSupplierRelateCert(SupplierRelateCertInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Supplier_Relate_Cert";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Cert_ID"] = entity.Cert_ID;
            DrAdd["Cert_SupplierID"] = entity.Cert_SupplierID;
            DrAdd["Cert_CertID"] = entity.Cert_CertID;
            DrAdd["Cert_Img"] = entity.Cert_Img;
            DrAdd["Cert_Img1"] = entity.Cert_Img1;

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

        public virtual bool EditSupplierRelateCert(SupplierRelateCertInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 1 * FROM Supplier_Relate_Cert Where Cert_ID=" + entity.Cert_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];

                    DrAdd["Cert_ID"] = entity.Cert_ID;
                    DrAdd["Cert_SupplierID"] = entity.Cert_SupplierID;
                    DrAdd["Cert_CertID"] = entity.Cert_CertID;
                    DrAdd["Cert_Img"] = entity.Cert_Img;
                    DrAdd["Cert_Img1"] = entity.Cert_Img1;
                }

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

        public virtual int DelSupplierRelateCertBySupplierID(int ID)
        {
            string SqlAdd = "DELETE FROM Supplier_Relate_Cert WHERE Cert_SupplierID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual IList<SupplierRelateCertInfo> GetSupplierRelateCerts(int Cert_SupplierID)
        {
            IList<SupplierRelateCertInfo> entitys = null;
            SupplierRelateCertInfo entity = null;
            string SqlList;
            SqlDataReader RdrList = null;
            try
            {
                SqlList = "select * from Supplier_Relate_Cert where Cert_SupplierID=" + Cert_SupplierID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SupplierRelateCertInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SupplierRelateCertInfo();
                        entity.Cert_ID = Tools.NullInt(RdrList["Cert_ID"]);
                        entity.Cert_SupplierID = Tools.NullInt(RdrList["Cert_SupplierID"]);
                        entity.Cert_CertID = Tools.NullInt(RdrList["Cert_CertID"]);
                        entity.Cert_Img = Tools.NullStr(RdrList["Cert_Img"]);
                        entity.Cert_Img1 = Tools.NullStr(RdrList["Cert_Img1"]);

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
    }

    public class SupplierCommissionCategory : ISupplierCommissionCategory
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public SupplierCommissionCategory()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddSupplierCommissionCategory(SupplierCommissionCategoryInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Supplier_Commission_Category";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Supplier_Commission_Cate_ID"] = entity.Supplier_Commission_Cate_ID;
            DrAdd["Supplier_Commission_Cate_SupplierID"] = entity.Supplier_Commission_Cate_SupplierID;
            DrAdd["Supplier_Commission_Cate_Name"] = entity.Supplier_Commission_Cate_Name;
            DrAdd["Supplier_Commission_Cate_Amount"] = entity.Supplier_Commission_Cate_Amount;
            DrAdd["Supplier_Commission_Cate_Site"] = entity.Supplier_Commission_Cate_Site;

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

        public virtual bool EditSupplierCommissionCategory(SupplierCommissionCategoryInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Supplier_Commission_Category WHERE Supplier_Commission_Cate_ID = " + entity.Supplier_Commission_Cate_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Supplier_Commission_Cate_ID"] = entity.Supplier_Commission_Cate_ID;
                    DrAdd["Supplier_Commission_Cate_SupplierID"] = entity.Supplier_Commission_Cate_SupplierID;
                    DrAdd["Supplier_Commission_Cate_Name"] = entity.Supplier_Commission_Cate_Name;
                    DrAdd["Supplier_Commission_Cate_Amount"] = entity.Supplier_Commission_Cate_Amount;
                    DrAdd["Supplier_Commission_Cate_Site"] = entity.Supplier_Commission_Cate_Site;

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

        public virtual int DelSupplierCommissionCategory(int ID)
        {
            string SqlAdd = "DELETE FROM Supplier_Commission_Category WHERE Supplier_Commission_Cate_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual SupplierCommissionCategoryInfo GetSupplierCommissionCategoryByID(int ID)
        {
            SupplierCommissionCategoryInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Supplier_Commission_Category WHERE Supplier_Commission_Cate_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SupplierCommissionCategoryInfo();

                    entity.Supplier_Commission_Cate_ID = Tools.NullInt(RdrList["Supplier_Commission_Cate_ID"]);
                    entity.Supplier_Commission_Cate_SupplierID = Tools.NullInt(RdrList["Supplier_Commission_Cate_SupplierID"]);
                    entity.Supplier_Commission_Cate_Name = Tools.NullStr(RdrList["Supplier_Commission_Cate_Name"]);
                    entity.Supplier_Commission_Cate_Amount = Tools.NullDbl(RdrList["Supplier_Commission_Cate_Amount"]);
                    entity.Supplier_Commission_Cate_Site = Tools.NullStr(RdrList["Supplier_Commission_Cate_Site"]);

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

        public virtual IList<SupplierCommissionCategoryInfo> GetSupplierCommissionCategorys(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<SupplierCommissionCategoryInfo> entitys = null;
            SupplierCommissionCategoryInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Supplier_Commission_Category";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SupplierCommissionCategoryInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SupplierCommissionCategoryInfo();
                        entity.Supplier_Commission_Cate_ID = Tools.NullInt(RdrList["Supplier_Commission_Cate_ID"]);
                        entity.Supplier_Commission_Cate_SupplierID = Tools.NullInt(RdrList["Supplier_Commission_Cate_SupplierID"]);
                        entity.Supplier_Commission_Cate_Name = Tools.NullStr(RdrList["Supplier_Commission_Cate_Name"]);
                        entity.Supplier_Commission_Cate_Amount = Tools.NullDbl(RdrList["Supplier_Commission_Cate_Amount"]);
                        entity.Supplier_Commission_Cate_Site = Tools.NullStr(RdrList["Supplier_Commission_Cate_Site"]);

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
                SqlTable = "Supplier_Commission_Category";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Supplier_Commission_Cate_ID) FROM " + SqlTable + SqlParam;

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

    public class SupplierMerchants : ISupplierMerchants
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public SupplierMerchants()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddSupplierMerchants(SupplierMerchantsInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Supplier_Merchants";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Merchants_ID"] = entity.Merchants_ID;
            DrAdd["Merchants_SupplierID"] = entity.Merchants_SupplierID;
            DrAdd["Merchants_Name"] = entity.Merchants_Name;
            DrAdd["Merchants_Validity"] = entity.Merchants_Validity;
            DrAdd["Merchants_Channel"] = entity.Merchants_Channel;
            DrAdd["Merchants_Advantage"] = entity.Merchants_Advantage;
            DrAdd["Merchants_Trem"] = entity.Merchants_Trem;
            DrAdd["Merchants_Intro"] = entity.Merchants_Intro;
            DrAdd["Merchants_Img"] = entity.Merchants_Img;
            DrAdd["Merchants_AddTime"] = entity.Merchants_AddTime;
            DrAdd["Merchants_Site"] = entity.Merchants_Site;

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

        public virtual bool EditSupplierMerchants(SupplierMerchantsInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Supplier_Merchants WHERE Merchants_ID = " + entity.Merchants_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Merchants_ID"] = entity.Merchants_ID;
                    DrAdd["Merchants_SupplierID"] = entity.Merchants_SupplierID;
                    DrAdd["Merchants_Name"] = entity.Merchants_Name;
                    DrAdd["Merchants_Validity"] = entity.Merchants_Validity;
                    DrAdd["Merchants_Channel"] = entity.Merchants_Channel;
                    DrAdd["Merchants_Advantage"] = entity.Merchants_Advantage;
                    DrAdd["Merchants_Trem"] = entity.Merchants_Trem;
                    DrAdd["Merchants_Intro"] = entity.Merchants_Intro;
                    DrAdd["Merchants_Img"] = entity.Merchants_Img;
                    DrAdd["Merchants_AddTime"] = entity.Merchants_AddTime;
                    DrAdd["Merchants_Site"] = entity.Merchants_Site;

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

        public virtual int DelSupplierMerchants(int ID)
        {
            string SqlAdd = "DELETE FROM Supplier_Merchants WHERE Merchants_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual SupplierMerchantsInfo GetSupplierMerchantsByID(int ID)
        {
            SupplierMerchantsInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Supplier_Merchants WHERE Merchants_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SupplierMerchantsInfo();

                    entity.Merchants_ID = Tools.NullInt(RdrList["Merchants_ID"]);
                    entity.Merchants_SupplierID = Tools.NullInt(RdrList["Merchants_SupplierID"]);
                    entity.Merchants_Name = Tools.NullStr(RdrList["Merchants_Name"]);
                    entity.Merchants_Validity = Tools.NullInt(RdrList["Merchants_Validity"]);
                    entity.Merchants_Channel = Tools.NullStr(RdrList["Merchants_Channel"]);
                    entity.Merchants_Advantage = Tools.NullStr(RdrList["Merchants_Advantage"]);
                    entity.Merchants_Trem = Tools.NullStr(RdrList["Merchants_Trem"]);
                    entity.Merchants_Intro = Tools.NullStr(RdrList["Merchants_Intro"]);
                    entity.Merchants_Img = Tools.NullStr(RdrList["Merchants_Img"]);
                    entity.Merchants_AddTime = Tools.NullDate(RdrList["Merchants_AddTime"]);
                    entity.Merchants_Site = Tools.NullStr(RdrList["Merchants_Site"]);
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

        public virtual IList<SupplierMerchantsInfo> GetSupplierMerchantss(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<SupplierMerchantsInfo> entitys = null;
            SupplierMerchantsInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Supplier_Merchants";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SupplierMerchantsInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SupplierMerchantsInfo();
                        entity.Merchants_ID = Tools.NullInt(RdrList["Merchants_ID"]);
                        entity.Merchants_SupplierID = Tools.NullInt(RdrList["Merchants_SupplierID"]);
                        entity.Merchants_Name = Tools.NullStr(RdrList["Merchants_Name"]);
                        entity.Merchants_Validity = Tools.NullInt(RdrList["Merchants_Validity"]);
                        entity.Merchants_Channel = Tools.NullStr(RdrList["Merchants_Channel"]);
                        entity.Merchants_Advantage = Tools.NullStr(RdrList["Merchants_Advantage"]);
                        entity.Merchants_Trem = Tools.NullStr(RdrList["Merchants_Trem"]);
                        entity.Merchants_Intro = Tools.NullStr(RdrList["Merchants_Intro"]);
                        entity.Merchants_Img = Tools.NullStr(RdrList["Merchants_Img"]);
                        entity.Merchants_AddTime = Tools.NullDate(RdrList["Merchants_AddTime"]);
                        entity.Merchants_Site = Tools.NullStr(RdrList["Merchants_Site"]);
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
                SqlTable = "Supplier_Merchants";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Merchants_ID) FROM " + SqlTable + SqlParam;

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

    public class SupplierMerchantsMessage : ISupplierMerchantsMessage
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public SupplierMerchantsMessage()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddSupplierMerchantsMessage(SupplierMerchantsMessageInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Supplier_Merchants_Message";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Message_ID"] = entity.Message_ID;
            DrAdd["Message_MemberID"] = entity.Message_MemberID;
            DrAdd["Message_MerchantsID"] = entity.Message_MerchantsID;
            DrAdd["Message_Content"] = entity.Message_Content;
            DrAdd["Message_Contactman"] = entity.Message_Contactman;
            DrAdd["Message_ContactMobile"] = entity.Message_ContactMobile;
            DrAdd["Message_ContactEmail"] = entity.Message_ContactEmail;
            DrAdd["Message_AddTime"] = entity.Message_AddTime;

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

        public virtual bool EditSupplierMerchantsMessage(SupplierMerchantsMessageInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Supplier_Merchants_Message WHERE Message_ID = " + entity.Message_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Message_ID"] = entity.Message_ID;
                    DrAdd["Message_MemberID"] = entity.Message_MemberID;
                    DrAdd["Message_MerchantsID"] = entity.Message_MerchantsID;
                    DrAdd["Message_Content"] = entity.Message_Content;
                    DrAdd["Message_Contactman"] = entity.Message_Contactman;
                    DrAdd["Message_ContactMobile"] = entity.Message_ContactMobile;
                    DrAdd["Message_ContactEmail"] = entity.Message_ContactEmail;
                    DrAdd["Message_AddTime"] = entity.Message_AddTime;

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

        public virtual int DelSupplierMerchantsMessage(int ID)
        {
            string SqlAdd = "DELETE FROM Supplier_Merchants_Message WHERE Message_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual SupplierMerchantsMessageInfo GetSupplierMerchantsMessageByID(int ID)
        {
            SupplierMerchantsMessageInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Supplier_Merchants_Message WHERE Message_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SupplierMerchantsMessageInfo();

                    entity.Message_ID = Tools.NullInt(RdrList["Message_ID"]);
                    entity.Message_MemberID = Tools.NullInt(RdrList["Message_MemberID"]);
                    entity.Message_MerchantsID = Tools.NullInt(RdrList["Message_MerchantsID"]);
                    entity.Message_Content = Tools.NullStr(RdrList["Message_Content"]);
                    entity.Message_Contactman = Tools.NullStr(RdrList["Message_Contactman"]);
                    entity.Message_ContactMobile = Tools.NullStr(RdrList["Message_ContactMobile"]);
                    entity.Message_ContactEmail = Tools.NullStr(RdrList["Message_ContactEmail"]);
                    entity.Message_AddTime = Tools.NullDate(RdrList["Message_AddTime"]);

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

        public virtual IList<SupplierMerchantsMessageInfo> GetSupplierMerchantsMessages(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<SupplierMerchantsMessageInfo> entitys = null;
            SupplierMerchantsMessageInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Supplier_Merchants_Message";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SupplierMerchantsMessageInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SupplierMerchantsMessageInfo();
                        entity.Message_ID = Tools.NullInt(RdrList["Message_ID"]);
                        entity.Message_MemberID = Tools.NullInt(RdrList["Message_MemberID"]);
                        entity.Message_MerchantsID = Tools.NullInt(RdrList["Message_MerchantsID"]);
                        entity.Message_Content = Tools.NullStr(RdrList["Message_Content"]);
                        entity.Message_Contactman = Tools.NullStr(RdrList["Message_Contactman"]);
                        entity.Message_ContactMobile = Tools.NullStr(RdrList["Message_ContactMobile"]);
                        entity.Message_ContactEmail = Tools.NullStr(RdrList["Message_ContactEmail"]);
                        entity.Message_AddTime = Tools.NullDate(RdrList["Message_AddTime"]);

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
                SqlTable = "Supplier_Merchants_Message";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Message_ID) FROM " + SqlTable + SqlParam;

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

    public class SupplierMargin : ISupplierMargin
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public SupplierMargin()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddSupplierMargin(SupplierMarginInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Supplier_Margin";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Supplier_Margin_ID"] = entity.Supplier_Margin_ID;
            DrAdd["Supplier_Margin_Type"] = entity.Supplier_Margin_Type;
            DrAdd["Supplier_Margin_Amount"] = entity.Supplier_Margin_Amount;
            DrAdd["Supplier_Margin_Site"] = entity.Supplier_Margin_Site;

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

        public virtual bool EditSupplierMargin(SupplierMarginInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Supplier_Margin WHERE Supplier_Margin_ID = " + entity.Supplier_Margin_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Supplier_Margin_ID"] = entity.Supplier_Margin_ID;
                    DrAdd["Supplier_Margin_Type"] = entity.Supplier_Margin_Type;
                    DrAdd["Supplier_Margin_Amount"] = entity.Supplier_Margin_Amount;
                    DrAdd["Supplier_Margin_Site"] = entity.Supplier_Margin_Site;

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

        public virtual int DelSupplierMargin(int ID)
        {
            string SqlAdd = "DELETE FROM Supplier_Margin WHERE Supplier_Margin_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual SupplierMarginInfo GetSupplierMarginByID(int ID)
        {
            SupplierMarginInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Supplier_Margin WHERE Supplier_Margin_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SupplierMarginInfo();

                    entity.Supplier_Margin_ID = Tools.NullInt(RdrList["Supplier_Margin_ID"]);
                    entity.Supplier_Margin_Type = Tools.NullInt(RdrList["Supplier_Margin_Type"]);
                    entity.Supplier_Margin_Amount = Tools.NullDbl(RdrList["Supplier_Margin_Amount"]);
                    entity.Supplier_Margin_Site = Tools.NullStr(RdrList["Supplier_Margin_Site"]);

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

        public virtual IList<SupplierMarginInfo> GetSupplierMargins(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<SupplierMarginInfo> entitys = null;
            SupplierMarginInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Supplier_Margin";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SupplierMarginInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SupplierMarginInfo();
                        entity.Supplier_Margin_ID = Tools.NullInt(RdrList["Supplier_Margin_ID"]);
                        entity.Supplier_Margin_Type = Tools.NullInt(RdrList["Supplier_Margin_Type"]);
                        entity.Supplier_Margin_Amount = Tools.NullDbl(RdrList["Supplier_Margin_Amount"]);
                        entity.Supplier_Margin_Site = Tools.NullStr(RdrList["Supplier_Margin_Site"]);

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
                SqlTable = "Supplier_Margin";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Supplier_Margin_ID) FROM " + SqlTable + SqlParam;

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


        public SupplierMarginInfo GetSupplierMarginByTypeID(int Type_ID)
        {
            SupplierMarginInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Supplier_Margin WHERE Supplier_Margin_Type = " + Type_ID ;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SupplierMarginInfo();

                    entity.Supplier_Margin_ID = Tools.NullInt(RdrList["Supplier_Margin_ID"]);
                    entity.Supplier_Margin_Type = Tools.NullInt(RdrList["Supplier_Margin_Type"]);
                    entity.Supplier_Margin_Amount = Tools.NullDbl(RdrList["Supplier_Margin_Amount"]);
                    entity.Supplier_Margin_Site = Tools.NullStr(RdrList["Supplier_Margin_Site"]);
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
    }
}
