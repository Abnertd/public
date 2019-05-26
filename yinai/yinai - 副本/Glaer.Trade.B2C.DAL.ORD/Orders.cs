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
    /// <summary>
    /// 订单操作实现
    /// </summary>
    public class Orders : IOrders
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public Orders()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddOrders(OrdersInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Orders";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();
            DrAdd["Orders_ID"] = entity.Orders_ID;
            DrAdd["Orders_SN"] = entity.Orders_SN;
            DrAdd["Orders_Type"] = entity.Orders_Type;
            DrAdd["Orders_ContractID"] = entity.Orders_ContractID;
            DrAdd["Orders_BuyerType"] = entity.Orders_BuyerType;
            DrAdd["Orders_BuyerID"] = entity.Orders_BuyerID;
            DrAdd["Orders_SysUserID"] = entity.Orders_SysUserID;
            DrAdd["Orders_Status"] = entity.Orders_Status;
            DrAdd["Orders_ERPSyncStatus"] = entity.Orders_ERPSyncStatus;
            DrAdd["Orders_PaymentStatus"] = entity.Orders_PaymentStatus;
            DrAdd["Orders_PaymentStatus_Time"] = entity.Orders_PaymentStatus_Time;
            DrAdd["Orders_DeliveryStatus"] = entity.Orders_DeliveryStatus;
            DrAdd["Orders_DeliveryStatus_Time"] = entity.Orders_DeliveryStatus_Time;
            DrAdd["Orders_InvoiceStatus"] = entity.Orders_InvoiceStatus;
            DrAdd["Orders_Fail_SysUserID"] = entity.Orders_Fail_SysUserID;
            DrAdd["Orders_Fail_Note"] = entity.Orders_Fail_Note;
            DrAdd["Orders_Fail_Addtime"] = entity.Orders_Fail_Addtime;
            DrAdd["Orders_IsReturnCoin"] = entity.Orders_IsReturnCoin;
            DrAdd["Orders_Total_MKTPrice"] = entity.Orders_Total_MKTPrice;
            DrAdd["Orders_Total_Price"] = entity.Orders_Total_Price;
            DrAdd["Orders_Total_Freight"] = entity.Orders_Total_Freight;
            DrAdd["Orders_Total_Coin"] = entity.Orders_Total_Coin;
            DrAdd["Orders_Total_UseCoin"] = entity.Orders_Total_UseCoin;
            DrAdd["Orders_Total_PriceDiscount"] = entity.Orders_Total_PriceDiscount;
            DrAdd["Orders_Total_FreightDiscount"] = entity.Orders_Total_FreightDiscount;
            DrAdd["Orders_Total_PriceDiscount_Note"] = entity.Orders_Total_PriceDiscount_Note;
            DrAdd["Orders_Total_FreightDiscount_Note"] = entity.Orders_Total_FreightDiscount_Note;
            DrAdd["Orders_Total_AllPrice"] = entity.Orders_Total_AllPrice;
            DrAdd["Orders_Address_ID"] = entity.Orders_Address_ID;
            DrAdd["Orders_Address_Country"] = entity.Orders_Address_Country;
            DrAdd["Orders_Address_State"] = entity.Orders_Address_State;
            DrAdd["Orders_Address_City"] = entity.Orders_Address_City;
            DrAdd["Orders_Address_County"] = entity.Orders_Address_County;
            DrAdd["Orders_Address_StreetAddress"] = entity.Orders_Address_StreetAddress;
            DrAdd["Orders_Address_Zip"] = entity.Orders_Address_Zip;
            DrAdd["Orders_Address_Name"] = entity.Orders_Address_Name;
            DrAdd["Orders_Address_Phone_Countrycode"] = entity.Orders_Address_Phone_Countrycode;
            DrAdd["Orders_Address_Phone_Areacode"] = entity.Orders_Address_Phone_Areacode;
            DrAdd["Orders_Address_Phone_Number"] = entity.Orders_Address_Phone_Number;
            DrAdd["Orders_Address_Mobile"] = entity.Orders_Address_Mobile;
            DrAdd["Orders_Delivery_Time_ID"] = entity.Orders_Delivery_Time_ID;
            DrAdd["Orders_Delivery"] = entity.Orders_Delivery;
            DrAdd["Orders_Delivery_Name"] = entity.Orders_Delivery_Name;
            DrAdd["Orders_Payway"] = entity.Orders_Payway;
            DrAdd["Orders_Payway_Name"] = entity.Orders_Payway_Name;
            DrAdd["Orders_PayType"] = entity.Orders_PayType;
            DrAdd["Orders_PayType_Name"] = entity.Orders_PayType_Name;
            DrAdd["Orders_Note"] = entity.Orders_Note;
            DrAdd["Orders_Admin_Note"] = entity.Orders_Admin_Note;
            DrAdd["Orders_Admin_Sign"] = entity.Orders_Admin_Sign;
            DrAdd["Orders_Site"] = entity.Orders_Site;
            DrAdd["Orders_SourceType"] = entity.Orders_SourceType;
            DrAdd["Orders_Source"] = entity.Orders_Source;
            DrAdd["Orders_VerifyCode"] = entity.Orders_VerifyCode;
            DrAdd["U_Orders_IsMonitor"] = entity.U_Orders_IsMonitor;
            DrAdd["Orders_Addtime"] = entity.Orders_Addtime;
            DrAdd["Orders_From"] = entity.Orders_From;
            DrAdd["Orders_Account_Pay"] = entity.Orders_Account_Pay;
            DrAdd["Orders_IsEvaluate"] = entity.Orders_IsEvaluate;
            DrAdd["Orders_IsSettling"] = entity.Orders_IsSettling;
            DrAdd["Orders_SupplierID"] = entity.Orders_SupplierID;
            DrAdd["Orders_PurchaseID"] = entity.Orders_PurchaseID;
            DrAdd["Orders_PriceReportID"] = entity.Orders_PriceReportID;
            DrAdd["Orders_MemberStatus"] = entity.Orders_MemberStatus;
            DrAdd["Orders_SupplierStatus"] = entity.Orders_SupplierStatus;
            DrAdd["Orders_MemberStatus_Time"] = entity.Orders_MemberStatus_Time;
            DrAdd["Orders_SupplierStatus_Time"] = entity.Orders_SupplierStatus_Time;
            DrAdd["Orders_ContractAdd"] = entity.Orders_ContractAdd;
            DrAdd["Orders_ApplyCreditAmount"] = entity.Orders_ApplyCreditAmount;
            DrAdd["Orders_AgreementNo"] = entity.Orders_AgreementNo;
            DrAdd["Orders_LoanTermID"] = entity.Orders_LoanTermID;
            DrAdd["Orders_LoanMethodID"] = entity.Orders_LoanMethodID;
            DrAdd["Orders_Fee"] = entity.Orders_Fee;
            DrAdd["Orders_MarginFee"] = entity.Orders_MarginFee;
            DrAdd["Orders_FeeRate"] = entity.Orders_FeeRate;
            DrAdd["Orders_MarginRate"] = entity.Orders_MarginRate;
            DrAdd["Orders_cashier_url"] = entity.Orders_cashier_url;
            DrAdd["Orders_Loan_proj_no"] = entity.Orders_Loan_proj_no;
            DrAdd["Orders_Responsible"] = entity.Orders_Responsible;
            DrAdd["Orders_IsShow"] = entity.Orders_IsShow;
            DtAdd.Rows.Add(DrAdd);
            try {
                DBHelper.SaveChanges(SqlAdd, DtAdd);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally {
                DtAdd.Dispose();
            }
        }

        public virtual bool EditOrders(OrdersInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Orders WHERE Orders_ID = " + entity.Orders_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Orders_ID"] = entity.Orders_ID;
                    DrAdd["Orders_SN"] = entity.Orders_SN;
                    DrAdd["Orders_Type"] = entity.Orders_Type;
                    DrAdd["Orders_ContractID"] = entity.Orders_ContractID;
                    DrAdd["Orders_BuyerType"] = entity.Orders_BuyerType;
                    DrAdd["Orders_BuyerID"] = entity.Orders_BuyerID;
                    DrAdd["Orders_SysUserID"] = entity.Orders_SysUserID;
                    DrAdd["Orders_Status"] = entity.Orders_Status;
                    DrAdd["Orders_ERPSyncStatus"] = entity.Orders_ERPSyncStatus;
                    DrAdd["Orders_PaymentStatus"] = entity.Orders_PaymentStatus;
                    DrAdd["Orders_PaymentStatus_Time"] = entity.Orders_PaymentStatus_Time;
                    DrAdd["Orders_DeliveryStatus"] = entity.Orders_DeliveryStatus;
                    DrAdd["Orders_DeliveryStatus_Time"] = entity.Orders_DeliveryStatus_Time;
                    DrAdd["Orders_InvoiceStatus"] = entity.Orders_InvoiceStatus;
                    DrAdd["Orders_Fail_SysUserID"] = entity.Orders_Fail_SysUserID;
                    DrAdd["Orders_Fail_Note"] = entity.Orders_Fail_Note;
                    DrAdd["Orders_Fail_Addtime"] = entity.Orders_Fail_Addtime;
                    DrAdd["Orders_IsReturnCoin"] = entity.Orders_IsReturnCoin;
                    DrAdd["Orders_Total_MKTPrice"] = entity.Orders_Total_MKTPrice;
                    DrAdd["Orders_Total_Price"] = entity.Orders_Total_Price;
                    DrAdd["Orders_Total_Freight"] = entity.Orders_Total_Freight;
                    DrAdd["Orders_Total_Coin"] = entity.Orders_Total_Coin;
                    DrAdd["Orders_Total_UseCoin"] = entity.Orders_Total_UseCoin;
                    DrAdd["Orders_Total_PriceDiscount"] = entity.Orders_Total_PriceDiscount;
                    DrAdd["Orders_Total_FreightDiscount"] = entity.Orders_Total_FreightDiscount;
                    DrAdd["Orders_Total_PriceDiscount_Note"] = entity.Orders_Total_PriceDiscount_Note;
                    DrAdd["Orders_Total_FreightDiscount_Note"] = entity.Orders_Total_FreightDiscount_Note;
                    DrAdd["Orders_Total_AllPrice"] = entity.Orders_Total_AllPrice;
                    DrAdd["Orders_Address_ID"] = entity.Orders_Address_ID;
                    DrAdd["Orders_Address_Country"] = entity.Orders_Address_Country;
                    DrAdd["Orders_Address_State"] = entity.Orders_Address_State;
                    DrAdd["Orders_Address_City"] = entity.Orders_Address_City;
                    DrAdd["Orders_Address_County"] = entity.Orders_Address_County;
                    DrAdd["Orders_Address_StreetAddress"] = entity.Orders_Address_StreetAddress;
                    DrAdd["Orders_Address_Zip"] = entity.Orders_Address_Zip;
                    DrAdd["Orders_Address_Name"] = entity.Orders_Address_Name;
                    DrAdd["Orders_Address_Phone_Countrycode"] = entity.Orders_Address_Phone_Countrycode;
                    DrAdd["Orders_Address_Phone_Areacode"] = entity.Orders_Address_Phone_Areacode;
                    DrAdd["Orders_Address_Phone_Number"] = entity.Orders_Address_Phone_Number;
                    DrAdd["Orders_Address_Mobile"] = entity.Orders_Address_Mobile;
                    DrAdd["Orders_Delivery_Time_ID"] = entity.Orders_Delivery_Time_ID;
                    DrAdd["Orders_Delivery"] = entity.Orders_Delivery;
                    DrAdd["Orders_Delivery_Name"] = entity.Orders_Delivery_Name;
                    DrAdd["Orders_Payway"] = entity.Orders_Payway;
                    DrAdd["Orders_Payway_Name"] = entity.Orders_Payway_Name;
                    DrAdd["Orders_PayType"] = entity.Orders_PayType;
                    DrAdd["Orders_PayType_Name"] = entity.Orders_PayType_Name;
                    DrAdd["Orders_Note"] = entity.Orders_Note;
                    DrAdd["Orders_Admin_Note"] = entity.Orders_Admin_Note;
                    DrAdd["Orders_Admin_Sign"] = entity.Orders_Admin_Sign;
                    DrAdd["Orders_Site"] = entity.Orders_Site;
                    DrAdd["Orders_SourceType"] = entity.Orders_SourceType;
                    DrAdd["Orders_Source"] = entity.Orders_Source;
                    DrAdd["Orders_VerifyCode"] = entity.Orders_VerifyCode;
                    DrAdd["U_Orders_IsMonitor"] = entity.U_Orders_IsMonitor;
                    DrAdd["Orders_Addtime"] = entity.Orders_Addtime;
                    DrAdd["Orders_From"] = entity.Orders_From;
                    DrAdd["Orders_Account_Pay"] = entity.Orders_Account_Pay;
                    DrAdd["Orders_IsEvaluate"] = entity.Orders_IsEvaluate;
                    DrAdd["Orders_IsSettling"] = entity.Orders_IsSettling;
                    DrAdd["Orders_SupplierID"] = entity.Orders_SupplierID;
                    DrAdd["Orders_PurchaseID"] = entity.Orders_PurchaseID;
                    DrAdd["Orders_PriceReportID"] = entity.Orders_PriceReportID;
                    DrAdd["Orders_MemberStatus"] = entity.Orders_MemberStatus;
                    DrAdd["Orders_SupplierStatus"] = entity.Orders_SupplierStatus;
                    DrAdd["Orders_MemberStatus_Time"] = entity.Orders_MemberStatus_Time;
                    DrAdd["Orders_SupplierStatus_Time"] = entity.Orders_SupplierStatus_Time;
                    DrAdd["Orders_ContractAdd"] = entity.Orders_ContractAdd;
                    DrAdd["Orders_ApplyCreditAmount"] = entity.Orders_ApplyCreditAmount;
                    DrAdd["Orders_AgreementNo"] = entity.Orders_AgreementNo;
                    DrAdd["Orders_LoanTermID"] = entity.Orders_LoanTermID;
                    DrAdd["Orders_LoanMethodID"] = entity.Orders_LoanMethodID;
                    DrAdd["Orders_Fee"] = entity.Orders_Fee;
                    DrAdd["Orders_MarginFee"] = entity.Orders_MarginFee;
                    DrAdd["Orders_FeeRate"] = entity.Orders_FeeRate;
                    DrAdd["Orders_MarginRate"] = entity.Orders_MarginRate;
                    DrAdd["Orders_cashier_url"] = entity.Orders_cashier_url;
                    DrAdd["Orders_Loan_proj_no"] = entity.Orders_Loan_proj_no;
                    DrAdd["Orders_Responsible"] = entity.Orders_Responsible;
                    DrAdd["Orders_IsShow"] = entity.Orders_IsShow;

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

        public virtual int DelOrders(int ID)
        {
            string SqlAdd = "DELETE FROM Orders WHERE Orders_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual OrdersInfo GetOrdersByID(int ID)
        {
            OrdersInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Orders WHERE Orders_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new OrdersInfo();

                    entity.Orders_ID = Tools.NullInt(RdrList["Orders_ID"]);
                    entity.Orders_SN = Tools.NullStr(RdrList["Orders_SN"]);
                    entity.Orders_Type = Tools.NullInt(RdrList["Orders_Type"]);
                    entity.Orders_ContractID = Tools.NullInt(RdrList["Orders_ContractID"]);
                    entity.Orders_BuyerType = Tools.NullInt(RdrList["Orders_BuyerType"]);
                    entity.Orders_BuyerID = Tools.NullInt(RdrList["Orders_BuyerID"]);
                    entity.Orders_SysUserID = Tools.NullInt(RdrList["Orders_SysUserID"]);
                    entity.Orders_Status = Tools.NullInt(RdrList["Orders_Status"]);
                    entity.Orders_ERPSyncStatus = Tools.NullInt(RdrList["Orders_ERPSyncStatus"]);
                    entity.Orders_PaymentStatus = Tools.NullInt(RdrList["Orders_PaymentStatus"]);
                    entity.Orders_PaymentStatus_Time = Tools.NullDate(RdrList["Orders_PaymentStatus_Time"]);
                    entity.Orders_DeliveryStatus = Tools.NullInt(RdrList["Orders_DeliveryStatus"]);
                    entity.Orders_DeliveryStatus_Time = Tools.NullDate(RdrList["Orders_DeliveryStatus_Time"]);
                    entity.Orders_InvoiceStatus = Tools.NullInt(RdrList["Orders_InvoiceStatus"]);
                    entity.Orders_Fail_SysUserID = Tools.NullInt(RdrList["Orders_Fail_SysUserID"]);
                    entity.Orders_Fail_Note = Tools.NullStr(RdrList["Orders_Fail_Note"]);
                    entity.Orders_Fail_Addtime = Tools.NullDate(RdrList["Orders_Fail_Addtime"]);
                    entity.Orders_IsReturnCoin = Tools.NullInt(RdrList["Orders_IsReturnCoin"]);
                    entity.Orders_Total_MKTPrice = Tools.NullDbl(RdrList["Orders_Total_MKTPrice"]);
                    entity.Orders_Total_Price = Tools.NullDbl(RdrList["Orders_Total_Price"]);
                    entity.Orders_Total_Freight = Tools.NullDbl(RdrList["Orders_Total_Freight"]);
                    entity.Orders_Total_Coin = Tools.NullInt(RdrList["Orders_Total_Coin"]);
                    entity.Orders_Total_UseCoin = Tools.NullInt(RdrList["Orders_Total_UseCoin"]);
                    entity.Orders_Total_PriceDiscount = Tools.NullDbl(RdrList["Orders_Total_PriceDiscount"]);
                    entity.Orders_Total_FreightDiscount = Tools.NullDbl(RdrList["Orders_Total_FreightDiscount"]);
                    entity.Orders_Total_PriceDiscount_Note = Tools.NullStr(RdrList["Orders_Total_PriceDiscount_Note"]);
                    entity.Orders_Total_FreightDiscount_Note = Tools.NullStr(RdrList["Orders_Total_FreightDiscount_Note"]);
                    entity.Orders_Total_AllPrice = Tools.NullDbl(RdrList["Orders_Total_AllPrice"]);
                    entity.Orders_Address_ID = Tools.NullInt(RdrList["Orders_Address_ID"]);
                    entity.Orders_Address_Country = Tools.NullStr(RdrList["Orders_Address_Country"]);
                    entity.Orders_Address_State = Tools.NullStr(RdrList["Orders_Address_State"]);
                    entity.Orders_Address_City = Tools.NullStr(RdrList["Orders_Address_City"]);
                    entity.Orders_Address_County = Tools.NullStr(RdrList["Orders_Address_County"]);
                    entity.Orders_Address_StreetAddress = Tools.NullStr(RdrList["Orders_Address_StreetAddress"]);
                    entity.Orders_Address_Zip = Tools.NullStr(RdrList["Orders_Address_Zip"]);
                    entity.Orders_Address_Name = Tools.NullStr(RdrList["Orders_Address_Name"]);
                    entity.Orders_Address_Phone_Countrycode = Tools.NullStr(RdrList["Orders_Address_Phone_Countrycode"]);
                    entity.Orders_Address_Phone_Areacode = Tools.NullStr(RdrList["Orders_Address_Phone_Areacode"]);
                    entity.Orders_Address_Phone_Number = Tools.NullStr(RdrList["Orders_Address_Phone_Number"]);
                    entity.Orders_Address_Mobile = Tools.NullStr(RdrList["Orders_Address_Mobile"]);
                    entity.Orders_Delivery_Time_ID = Tools.NullInt(RdrList["Orders_Delivery_Time_ID"]);
                    entity.Orders_Delivery = Tools.NullInt(RdrList["Orders_Delivery"]);
                    entity.Orders_Delivery_Name = Tools.NullStr(RdrList["Orders_Delivery_Name"]);
                    entity.Orders_Payway = Tools.NullInt(RdrList["Orders_Payway"]);
                    entity.Orders_Payway_Name = Tools.NullStr(RdrList["Orders_Payway_Name"]);
                    entity.Orders_PayType = Tools.NullInt(RdrList["Orders_PayType"]);
                    entity.Orders_PayType_Name = Tools.NullStr(RdrList["Orders_PayType_Name"]);
                    entity.Orders_Note = Tools.NullStr(RdrList["Orders_Note"]);
                    entity.Orders_Admin_Note = Tools.NullStr(RdrList["Orders_Admin_Note"]);
                    entity.Orders_Admin_Sign = Tools.NullInt(RdrList["Orders_Admin_Sign"]);
                    entity.Orders_Site = Tools.NullStr(RdrList["Orders_Site"]);
                    entity.Orders_SourceType = Tools.NullInt(RdrList["Orders_SourceType"]);
                    entity.Orders_Source = Tools.NullStr(RdrList["Orders_Source"]);
                    entity.Orders_VerifyCode = Tools.NullStr(RdrList["Orders_VerifyCode"]);
                    entity.U_Orders_IsMonitor = Tools.NullInt(RdrList["U_Orders_IsMonitor"]);
                    entity.Orders_Addtime = Tools.NullDate(RdrList["Orders_Addtime"]);
                    entity.Orders_From = Tools.NullStr(RdrList["Orders_From"]);
                    entity.Orders_Account_Pay = Tools.NullDbl(RdrList["Orders_Account_Pay"]);
                    entity.Orders_IsEvaluate = Tools.NullInt(RdrList["Orders_IsEvaluate"]);
                    entity.Orders_IsSettling = Tools.NullInt(RdrList["Orders_IsSettling"]);
                    entity.Orders_SupplierID = Tools.NullInt(RdrList["Orders_SupplierID"]);
                    entity.Orders_PurchaseID = Tools.NullInt(RdrList["Orders_PurchaseID"]);
                    entity.Orders_PriceReportID = Tools.NullInt(RdrList["Orders_PriceReportID"]);
                    entity.Orders_MemberStatus = Tools.NullInt(RdrList["Orders_MemberStatus"]);
                    entity.Orders_SupplierStatus = Tools.NullInt(RdrList["Orders_SupplierStatus"]);
                    entity.Orders_MemberStatus_Time = Tools.NullDate(RdrList["Orders_MemberStatus_Time"]);
                    entity.Orders_SupplierStatus_Time = Tools.NullDate(RdrList["Orders_SupplierStatus_Time"]);
                    entity.Orders_ContractAdd = Tools.NullStr(RdrList["Orders_ContractAdd"]);
                    entity.Orders_ApplyCreditAmount = Tools.NullDbl(RdrList["Orders_ApplyCreditAmount"]);
                    entity.Orders_AgreementNo = Tools.NullStr(RdrList["Orders_AgreementNo"]);
                    entity.Orders_LoanTermID = Tools.NullInt(RdrList["Orders_LoanTermID"]);
                    entity.Orders_LoanMethodID = Tools.NullInt(RdrList["Orders_LoanMethodID"]);
                    entity.Orders_Fee = Tools.NullDbl(RdrList["Orders_Fee"]);
                    entity.Orders_MarginFee = Tools.NullDbl(RdrList["Orders_MarginFee"]);
                    entity.Orders_FeeRate = Tools.NullDbl(RdrList["Orders_FeeRate"]);
                    entity.Orders_MarginRate = Tools.NullDbl(RdrList["Orders_MarginRate"]);
                    entity.Orders_cashier_url = Tools.NullStr(RdrList["Orders_cashier_url"]);
                    entity.Orders_Loan_proj_no = Tools.NullStr(RdrList["Orders_Loan_proj_no"]);
                    entity.Orders_Responsible = Tools.NullInt(RdrList["Orders_Responsible"]);
                    entity.Orders_IsShow = Tools.NullInt(RdrList["Orders_IsShow"]);
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

        public virtual OrdersInfo GetSupplierOrderInfoByID(int ID, int Supplier_ID)
        {
            OrdersInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Orders WHERE Orders_ID = " + ID + " and Orders_SupplierID=" + Supplier_ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new OrdersInfo();

                    entity.Orders_ID = Tools.NullInt(RdrList["Orders_ID"]);
                    entity.Orders_SN = Tools.NullStr(RdrList["Orders_SN"]);
                    entity.Orders_Type = Tools.NullInt(RdrList["Orders_Type"]);
                    entity.Orders_ContractID = Tools.NullInt(RdrList["Orders_ContractID"]);
                    entity.Orders_BuyerType = Tools.NullInt(RdrList["Orders_BuyerType"]);
                    entity.Orders_BuyerID = Tools.NullInt(RdrList["Orders_BuyerID"]);
                    entity.Orders_SysUserID = Tools.NullInt(RdrList["Orders_SysUserID"]);
                    entity.Orders_Status = Tools.NullInt(RdrList["Orders_Status"]);
                    entity.Orders_ERPSyncStatus = Tools.NullInt(RdrList["Orders_ERPSyncStatus"]);
                    entity.Orders_PaymentStatus = Tools.NullInt(RdrList["Orders_PaymentStatus"]);
                    entity.Orders_PaymentStatus_Time = Tools.NullDate(RdrList["Orders_PaymentStatus_Time"]);
                    entity.Orders_DeliveryStatus = Tools.NullInt(RdrList["Orders_DeliveryStatus"]);
                    entity.Orders_DeliveryStatus_Time = Tools.NullDate(RdrList["Orders_DeliveryStatus_Time"]);
                    entity.Orders_InvoiceStatus = Tools.NullInt(RdrList["Orders_InvoiceStatus"]);
                    entity.Orders_Fail_SysUserID = Tools.NullInt(RdrList["Orders_Fail_SysUserID"]);
                    entity.Orders_Fail_Note = Tools.NullStr(RdrList["Orders_Fail_Note"]);
                    entity.Orders_Fail_Addtime = Tools.NullDate(RdrList["Orders_Fail_Addtime"]);
                    entity.Orders_IsReturnCoin = Tools.NullInt(RdrList["Orders_IsReturnCoin"]);
                    entity.Orders_Total_MKTPrice = Tools.NullDbl(RdrList["Orders_Total_MKTPrice"]);
                    entity.Orders_Total_Price = Tools.NullDbl(RdrList["Orders_Total_Price"]);
                    entity.Orders_Total_Freight = Tools.NullDbl(RdrList["Orders_Total_Freight"]);
                    entity.Orders_Total_Coin = Tools.NullInt(RdrList["Orders_Total_Coin"]);
                    entity.Orders_Total_UseCoin = Tools.NullInt(RdrList["Orders_Total_UseCoin"]);
                    entity.Orders_Total_PriceDiscount = Tools.NullDbl(RdrList["Orders_Total_PriceDiscount"]);
                    entity.Orders_Total_FreightDiscount = Tools.NullDbl(RdrList["Orders_Total_FreightDiscount"]);
                    entity.Orders_Total_PriceDiscount_Note = Tools.NullStr(RdrList["Orders_Total_PriceDiscount_Note"]);
                    entity.Orders_Total_FreightDiscount_Note = Tools.NullStr(RdrList["Orders_Total_FreightDiscount_Note"]);
                    entity.Orders_Total_AllPrice = Tools.NullDbl(RdrList["Orders_Total_AllPrice"]);
                    entity.Orders_Address_ID = Tools.NullInt(RdrList["Orders_Address_ID"]);
                    entity.Orders_Address_Country = Tools.NullStr(RdrList["Orders_Address_Country"]);
                    entity.Orders_Address_State = Tools.NullStr(RdrList["Orders_Address_State"]);
                    entity.Orders_Address_City = Tools.NullStr(RdrList["Orders_Address_City"]);
                    entity.Orders_Address_County = Tools.NullStr(RdrList["Orders_Address_County"]);
                    entity.Orders_Address_StreetAddress = Tools.NullStr(RdrList["Orders_Address_StreetAddress"]);
                    entity.Orders_Address_Zip = Tools.NullStr(RdrList["Orders_Address_Zip"]);
                    entity.Orders_Address_Name = Tools.NullStr(RdrList["Orders_Address_Name"]);
                    entity.Orders_Address_Phone_Countrycode = Tools.NullStr(RdrList["Orders_Address_Phone_Countrycode"]);
                    entity.Orders_Address_Phone_Areacode = Tools.NullStr(RdrList["Orders_Address_Phone_Areacode"]);
                    entity.Orders_Address_Phone_Number = Tools.NullStr(RdrList["Orders_Address_Phone_Number"]);
                    entity.Orders_Address_Mobile = Tools.NullStr(RdrList["Orders_Address_Mobile"]);
                    entity.Orders_Delivery_Time_ID = Tools.NullInt(RdrList["Orders_Delivery_Time_ID"]);
                    entity.Orders_Delivery = Tools.NullInt(RdrList["Orders_Delivery"]);
                    entity.Orders_Delivery_Name = Tools.NullStr(RdrList["Orders_Delivery_Name"]);
                    entity.Orders_Payway = Tools.NullInt(RdrList["Orders_Payway"]);
                    entity.Orders_Payway_Name = Tools.NullStr(RdrList["Orders_Payway_Name"]);
                    entity.Orders_PayType = Tools.NullInt(RdrList["Orders_PayType"]);
                    entity.Orders_PayType_Name = Tools.NullStr(RdrList["Orders_PayType_Name"]);
                    entity.Orders_Note = Tools.NullStr(RdrList["Orders_Note"]);
                    entity.Orders_Admin_Note = Tools.NullStr(RdrList["Orders_Admin_Note"]);
                    entity.Orders_Admin_Sign = Tools.NullInt(RdrList["Orders_Admin_Sign"]);
                    entity.Orders_Site = Tools.NullStr(RdrList["Orders_Site"]);
                    entity.Orders_SourceType = Tools.NullInt(RdrList["Orders_SourceType"]);
                    entity.Orders_Source = Tools.NullStr(RdrList["Orders_Source"]);
                    entity.Orders_VerifyCode = Tools.NullStr(RdrList["Orders_VerifyCode"]);
                    entity.U_Orders_IsMonitor = Tools.NullInt(RdrList["U_Orders_IsMonitor"]);
                    entity.Orders_Addtime = Tools.NullDate(RdrList["Orders_Addtime"]);
                    entity.Orders_From = Tools.NullStr(RdrList["Orders_From"]);
                    entity.Orders_Account_Pay = Tools.NullDbl(RdrList["Orders_Account_Pay"]);
                    entity.Orders_IsEvaluate = Tools.NullInt(RdrList["Orders_IsEvaluate"]);
                    entity.Orders_IsSettling = Tools.NullInt(RdrList["Orders_IsSettling"]);
                    entity.Orders_SupplierID = Tools.NullInt(RdrList["Orders_SupplierID"]);
                    entity.Orders_PurchaseID = Tools.NullInt(RdrList["Orders_PurchaseID"]);
                    entity.Orders_PriceReportID = Tools.NullInt(RdrList["Orders_PriceReportID"]);
                    entity.Orders_MemberStatus = Tools.NullInt(RdrList["Orders_MemberStatus"]);
                    entity.Orders_SupplierStatus = Tools.NullInt(RdrList["Orders_SupplierStatus"]);
                    entity.Orders_MemberStatus_Time = Tools.NullDate(RdrList["Orders_MemberStatus_Time"]);
                    entity.Orders_SupplierStatus_Time = Tools.NullDate(RdrList["Orders_SupplierStatus_Time"]);
                    entity.Orders_ContractAdd = Tools.NullStr(RdrList["Orders_ContractAdd"]);
                    entity.Orders_ApplyCreditAmount = Tools.NullDbl(RdrList["Orders_ApplyCreditAmount"]);
                    entity.Orders_AgreementNo = Tools.NullStr(RdrList["Orders_AgreementNo"]);
                    entity.Orders_LoanTermID = Tools.NullInt(RdrList["Orders_LoanTermID"]);
                    entity.Orders_LoanMethodID = Tools.NullInt(RdrList["Orders_LoanMethodID"]);
                    entity.Orders_Fee = Tools.NullDbl(RdrList["Orders_Fee"]);
                    entity.Orders_MarginFee = Tools.NullDbl(RdrList["Orders_MarginFee"]);
                    entity.Orders_FeeRate = Tools.NullDbl(RdrList["Orders_FeeRate"]);
                    entity.Orders_MarginRate = Tools.NullDbl(RdrList["Orders_MarginRate"]);
                    entity.Orders_cashier_url = Tools.NullStr(RdrList["Orders_cashier_url"]);
                    entity.Orders_Loan_proj_no = Tools.NullStr(RdrList["Orders_Loan_proj_no"]);
                    entity.Orders_Responsible = Tools.NullInt(RdrList["Orders_Responsible"]);
                    entity.Orders_IsShow = Tools.NullInt(RdrList["Orders_IsShow"]);
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

        public virtual OrdersInfo GetMemberOrderInfoByID(int ID, int Member_ID)
        {
            OrdersInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Orders WHERE Orders_ID = " + ID + " and Orders_BuyerID=" + Member_ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new OrdersInfo();

                    entity.Orders_ID = Tools.NullInt(RdrList["Orders_ID"]);
                    entity.Orders_SN = Tools.NullStr(RdrList["Orders_SN"]);
                    entity.Orders_Type = Tools.NullInt(RdrList["Orders_Type"]);
                    entity.Orders_ContractID = Tools.NullInt(RdrList["Orders_ContractID"]);
                    entity.Orders_BuyerType = Tools.NullInt(RdrList["Orders_BuyerType"]);
                    entity.Orders_BuyerID = Tools.NullInt(RdrList["Orders_BuyerID"]);
                    entity.Orders_SysUserID = Tools.NullInt(RdrList["Orders_SysUserID"]);
                    entity.Orders_Status = Tools.NullInt(RdrList["Orders_Status"]);
                    entity.Orders_ERPSyncStatus = Tools.NullInt(RdrList["Orders_ERPSyncStatus"]);
                    entity.Orders_PaymentStatus = Tools.NullInt(RdrList["Orders_PaymentStatus"]);
                    entity.Orders_PaymentStatus_Time = Tools.NullDate(RdrList["Orders_PaymentStatus_Time"]);
                    entity.Orders_DeliveryStatus = Tools.NullInt(RdrList["Orders_DeliveryStatus"]);
                    entity.Orders_DeliveryStatus_Time = Tools.NullDate(RdrList["Orders_DeliveryStatus_Time"]);
                    entity.Orders_InvoiceStatus = Tools.NullInt(RdrList["Orders_InvoiceStatus"]);
                    entity.Orders_Fail_SysUserID = Tools.NullInt(RdrList["Orders_Fail_SysUserID"]);
                    entity.Orders_Fail_Note = Tools.NullStr(RdrList["Orders_Fail_Note"]);
                    entity.Orders_Fail_Addtime = Tools.NullDate(RdrList["Orders_Fail_Addtime"]);
                    entity.Orders_IsReturnCoin = Tools.NullInt(RdrList["Orders_IsReturnCoin"]);
                    entity.Orders_Total_MKTPrice = Tools.NullDbl(RdrList["Orders_Total_MKTPrice"]);
                    entity.Orders_Total_Price = Tools.NullDbl(RdrList["Orders_Total_Price"]);
                    entity.Orders_Total_Freight = Tools.NullDbl(RdrList["Orders_Total_Freight"]);
                    entity.Orders_Total_Coin = Tools.NullInt(RdrList["Orders_Total_Coin"]);
                    entity.Orders_Total_UseCoin = Tools.NullInt(RdrList["Orders_Total_UseCoin"]);
                    entity.Orders_Total_PriceDiscount = Tools.NullDbl(RdrList["Orders_Total_PriceDiscount"]);
                    entity.Orders_Total_FreightDiscount = Tools.NullDbl(RdrList["Orders_Total_FreightDiscount"]);
                    entity.Orders_Total_PriceDiscount_Note = Tools.NullStr(RdrList["Orders_Total_PriceDiscount_Note"]);
                    entity.Orders_Total_FreightDiscount_Note = Tools.NullStr(RdrList["Orders_Total_FreightDiscount_Note"]);
                    entity.Orders_Total_AllPrice = Tools.NullDbl(RdrList["Orders_Total_AllPrice"]);
                    entity.Orders_Address_ID = Tools.NullInt(RdrList["Orders_Address_ID"]);
                    entity.Orders_Address_Country = Tools.NullStr(RdrList["Orders_Address_Country"]);
                    entity.Orders_Address_State = Tools.NullStr(RdrList["Orders_Address_State"]);
                    entity.Orders_Address_City = Tools.NullStr(RdrList["Orders_Address_City"]);
                    entity.Orders_Address_County = Tools.NullStr(RdrList["Orders_Address_County"]);
                    entity.Orders_Address_StreetAddress = Tools.NullStr(RdrList["Orders_Address_StreetAddress"]);
                    entity.Orders_Address_Zip = Tools.NullStr(RdrList["Orders_Address_Zip"]);
                    entity.Orders_Address_Name = Tools.NullStr(RdrList["Orders_Address_Name"]);
                    entity.Orders_Address_Phone_Countrycode = Tools.NullStr(RdrList["Orders_Address_Phone_Countrycode"]);
                    entity.Orders_Address_Phone_Areacode = Tools.NullStr(RdrList["Orders_Address_Phone_Areacode"]);
                    entity.Orders_Address_Phone_Number = Tools.NullStr(RdrList["Orders_Address_Phone_Number"]);
                    entity.Orders_Address_Mobile = Tools.NullStr(RdrList["Orders_Address_Mobile"]);
                    entity.Orders_Delivery_Time_ID = Tools.NullInt(RdrList["Orders_Delivery_Time_ID"]);
                    entity.Orders_Delivery = Tools.NullInt(RdrList["Orders_Delivery"]);
                    entity.Orders_Delivery_Name = Tools.NullStr(RdrList["Orders_Delivery_Name"]);
                    entity.Orders_Payway = Tools.NullInt(RdrList["Orders_Payway"]);
                    entity.Orders_Payway_Name = Tools.NullStr(RdrList["Orders_Payway_Name"]);
                    entity.Orders_PayType = Tools.NullInt(RdrList["Orders_PayType"]);
                    entity.Orders_PayType_Name = Tools.NullStr(RdrList["Orders_PayType_Name"]);
                    entity.Orders_Note = Tools.NullStr(RdrList["Orders_Note"]);
                    entity.Orders_Admin_Note = Tools.NullStr(RdrList["Orders_Admin_Note"]);
                    entity.Orders_Admin_Sign = Tools.NullInt(RdrList["Orders_Admin_Sign"]);
                    entity.Orders_Site = Tools.NullStr(RdrList["Orders_Site"]);
                    entity.Orders_SourceType = Tools.NullInt(RdrList["Orders_SourceType"]);
                    entity.Orders_Source = Tools.NullStr(RdrList["Orders_Source"]);
                    entity.Orders_VerifyCode = Tools.NullStr(RdrList["Orders_VerifyCode"]);
                    entity.U_Orders_IsMonitor = Tools.NullInt(RdrList["U_Orders_IsMonitor"]);
                    entity.Orders_Addtime = Tools.NullDate(RdrList["Orders_Addtime"]);
                    entity.Orders_From = Tools.NullStr(RdrList["Orders_From"]);
                    entity.Orders_Account_Pay = Tools.NullDbl(RdrList["Orders_Account_Pay"]);
                    entity.Orders_IsEvaluate = Tools.NullInt(RdrList["Orders_IsEvaluate"]);
                    entity.Orders_IsSettling = Tools.NullInt(RdrList["Orders_IsSettling"]);
                    entity.Orders_SupplierID = Tools.NullInt(RdrList["Orders_SupplierID"]);
                    entity.Orders_PurchaseID = Tools.NullInt(RdrList["Orders_PurchaseID"]);
                    entity.Orders_PriceReportID = Tools.NullInt(RdrList["Orders_PriceReportID"]);
                    entity.Orders_MemberStatus = Tools.NullInt(RdrList["Orders_MemberStatus"]);
                    entity.Orders_SupplierStatus = Tools.NullInt(RdrList["Orders_SupplierStatus"]);
                    entity.Orders_MemberStatus_Time = Tools.NullDate(RdrList["Orders_MemberStatus_Time"]);
                    entity.Orders_SupplierStatus_Time = Tools.NullDate(RdrList["Orders_SupplierStatus_Time"]);
                    entity.Orders_ContractAdd = Tools.NullStr(RdrList["Orders_ContractAdd"]);
                    entity.Orders_ApplyCreditAmount = Tools.NullDbl(RdrList["Orders_ApplyCreditAmount"]);
                    entity.Orders_AgreementNo = Tools.NullStr(RdrList["Orders_AgreementNo"]);
                    entity.Orders_LoanTermID = Tools.NullInt(RdrList["Orders_LoanTermID"]);
                    entity.Orders_LoanMethodID = Tools.NullInt(RdrList["Orders_LoanMethodID"]);
                    entity.Orders_Fee = Tools.NullDbl(RdrList["Orders_Fee"]);
                    entity.Orders_MarginFee = Tools.NullDbl(RdrList["Orders_MarginFee"]);
                    entity.Orders_FeeRate = Tools.NullDbl(RdrList["Orders_FeeRate"]);
                    entity.Orders_MarginRate = Tools.NullDbl(RdrList["Orders_MarginRate"]);
                    entity.Orders_cashier_url = Tools.NullStr(RdrList["Orders_cashier_url"]);
                    entity.Orders_Loan_proj_no = Tools.NullStr(RdrList["Orders_Loan_proj_no"]);
                    entity.Orders_Responsible = Tools.NullInt(RdrList["Orders_Responsible"]);
                    entity.Orders_IsShow = Tools.NullInt(RdrList["Orders_IsShow"]);
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

        public virtual OrdersInfo GetOrdersBySN(string SN)
        {
            OrdersInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Orders WHERE Orders_SN = '"+SN+"'";
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new OrdersInfo();

                    entity.Orders_ID = Tools.NullInt(RdrList["Orders_ID"]);
                    entity.Orders_SN = Tools.NullStr(RdrList["Orders_SN"]);
                    entity.Orders_Type = Tools.NullInt(RdrList["Orders_Type"]);
                    entity.Orders_ContractID = Tools.NullInt(RdrList["Orders_ContractID"]);
                    entity.Orders_BuyerType = Tools.NullInt(RdrList["Orders_BuyerType"]);
                    entity.Orders_BuyerID = Tools.NullInt(RdrList["Orders_BuyerID"]);
                    entity.Orders_SysUserID = Tools.NullInt(RdrList["Orders_SysUserID"]);
                    entity.Orders_Status = Tools.NullInt(RdrList["Orders_Status"]);
                    entity.Orders_ERPSyncStatus = Tools.NullInt(RdrList["Orders_ERPSyncStatus"]);
                    entity.Orders_PaymentStatus = Tools.NullInt(RdrList["Orders_PaymentStatus"]);
                    entity.Orders_PaymentStatus_Time = Tools.NullDate(RdrList["Orders_PaymentStatus_Time"]);
                    entity.Orders_DeliveryStatus = Tools.NullInt(RdrList["Orders_DeliveryStatus"]);
                    entity.Orders_DeliveryStatus_Time = Tools.NullDate(RdrList["Orders_DeliveryStatus_Time"]);
                    entity.Orders_InvoiceStatus = Tools.NullInt(RdrList["Orders_InvoiceStatus"]);
                    entity.Orders_Fail_SysUserID = Tools.NullInt(RdrList["Orders_Fail_SysUserID"]);
                    entity.Orders_Fail_Note = Tools.NullStr(RdrList["Orders_Fail_Note"]);
                    entity.Orders_Fail_Addtime = Tools.NullDate(RdrList["Orders_Fail_Addtime"]);
                    entity.Orders_IsReturnCoin = Tools.NullInt(RdrList["Orders_IsReturnCoin"]);
                    entity.Orders_Total_MKTPrice = Tools.NullDbl(RdrList["Orders_Total_MKTPrice"]);
                    entity.Orders_Total_Price = Tools.NullDbl(RdrList["Orders_Total_Price"]);
                    entity.Orders_Total_Freight = Tools.NullDbl(RdrList["Orders_Total_Freight"]);
                    entity.Orders_Total_Coin = Tools.NullInt(RdrList["Orders_Total_Coin"]);
                    entity.Orders_Total_UseCoin = Tools.NullInt(RdrList["Orders_Total_UseCoin"]);
                    entity.Orders_Total_PriceDiscount = Tools.NullDbl(RdrList["Orders_Total_PriceDiscount"]);
                    entity.Orders_Total_FreightDiscount = Tools.NullDbl(RdrList["Orders_Total_FreightDiscount"]);
                    entity.Orders_Total_PriceDiscount_Note = Tools.NullStr(RdrList["Orders_Total_PriceDiscount_Note"]);
                    entity.Orders_Total_FreightDiscount_Note = Tools.NullStr(RdrList["Orders_Total_FreightDiscount_Note"]);
                    entity.Orders_Total_AllPrice = Tools.NullDbl(RdrList["Orders_Total_AllPrice"]);
                    entity.Orders_Address_ID = Tools.NullInt(RdrList["Orders_Address_ID"]);
                    entity.Orders_Address_Country = Tools.NullStr(RdrList["Orders_Address_Country"]);
                    entity.Orders_Address_State = Tools.NullStr(RdrList["Orders_Address_State"]);
                    entity.Orders_Address_City = Tools.NullStr(RdrList["Orders_Address_City"]);
                    entity.Orders_Address_County = Tools.NullStr(RdrList["Orders_Address_County"]);
                    entity.Orders_Address_StreetAddress = Tools.NullStr(RdrList["Orders_Address_StreetAddress"]);
                    entity.Orders_Address_Zip = Tools.NullStr(RdrList["Orders_Address_Zip"]);
                    entity.Orders_Address_Name = Tools.NullStr(RdrList["Orders_Address_Name"]);
                    entity.Orders_Address_Phone_Countrycode = Tools.NullStr(RdrList["Orders_Address_Phone_Countrycode"]);
                    entity.Orders_Address_Phone_Areacode = Tools.NullStr(RdrList["Orders_Address_Phone_Areacode"]);
                    entity.Orders_Address_Phone_Number = Tools.NullStr(RdrList["Orders_Address_Phone_Number"]);
                    entity.Orders_Address_Mobile = Tools.NullStr(RdrList["Orders_Address_Mobile"]);
                    entity.Orders_Delivery_Time_ID = Tools.NullInt(RdrList["Orders_Delivery_Time_ID"]);
                    entity.Orders_Delivery = Tools.NullInt(RdrList["Orders_Delivery"]);
                    entity.Orders_Delivery_Name = Tools.NullStr(RdrList["Orders_Delivery_Name"]);
                    entity.Orders_Payway = Tools.NullInt(RdrList["Orders_Payway"]);
                    entity.Orders_Payway_Name = Tools.NullStr(RdrList["Orders_Payway_Name"]);
                    entity.Orders_PayType = Tools.NullInt(RdrList["Orders_PayType"]);
                    entity.Orders_PayType_Name = Tools.NullStr(RdrList["Orders_PayType_Name"]);
                    entity.Orders_Note = Tools.NullStr(RdrList["Orders_Note"]);
                    entity.Orders_Admin_Note = Tools.NullStr(RdrList["Orders_Admin_Note"]);
                    entity.Orders_Admin_Sign = Tools.NullInt(RdrList["Orders_Admin_Sign"]);
                    entity.Orders_Site = Tools.NullStr(RdrList["Orders_Site"]);
                    entity.Orders_SourceType = Tools.NullInt(RdrList["Orders_SourceType"]);
                    entity.Orders_Source = Tools.NullStr(RdrList["Orders_Source"]);
                    entity.Orders_VerifyCode = Tools.NullStr(RdrList["Orders_VerifyCode"]);
                    entity.U_Orders_IsMonitor = Tools.NullInt(RdrList["U_Orders_IsMonitor"]);
                    entity.Orders_Addtime = Tools.NullDate(RdrList["Orders_Addtime"]);
                    entity.Orders_From = Tools.NullStr(RdrList["Orders_From"]);
                    entity.Orders_Account_Pay = Tools.NullDbl(RdrList["Orders_Account_Pay"]);
                    entity.Orders_IsEvaluate = Tools.NullInt(RdrList["Orders_IsEvaluate"]);
                    entity.Orders_IsSettling = Tools.NullInt(RdrList["Orders_IsSettling"]);
                    entity.Orders_SupplierID = Tools.NullInt(RdrList["Orders_SupplierID"]);
                    entity.Orders_PurchaseID = Tools.NullInt(RdrList["Orders_PurchaseID"]);
                    entity.Orders_PriceReportID = Tools.NullInt(RdrList["Orders_PriceReportID"]);
                    entity.Orders_MemberStatus = Tools.NullInt(RdrList["Orders_MemberStatus"]);
                    entity.Orders_SupplierStatus = Tools.NullInt(RdrList["Orders_SupplierStatus"]);
                    entity.Orders_MemberStatus_Time = Tools.NullDate(RdrList["Orders_MemberStatus_Time"]);
                    entity.Orders_SupplierStatus_Time = Tools.NullDate(RdrList["Orders_SupplierStatus_Time"]);
                    entity.Orders_ContractAdd = Tools.NullStr(RdrList["Orders_ContractAdd"]);
                    entity.Orders_ApplyCreditAmount = Tools.NullDbl(RdrList["Orders_ApplyCreditAmount"]);
                    entity.Orders_AgreementNo = Tools.NullStr(RdrList["Orders_AgreementNo"]);
                    entity.Orders_LoanTermID = Tools.NullInt(RdrList["Orders_LoanTermID"]);
                    entity.Orders_LoanMethodID = Tools.NullInt(RdrList["Orders_LoanMethodID"]);
                    entity.Orders_Fee = Tools.NullDbl(RdrList["Orders_Fee"]);
                    entity.Orders_MarginFee = Tools.NullDbl(RdrList["Orders_MarginFee"]);
                    entity.Orders_FeeRate = Tools.NullDbl(RdrList["Orders_FeeRate"]);
                    entity.Orders_MarginRate = Tools.NullDbl(RdrList["Orders_MarginRate"]);
                    entity.Orders_cashier_url = Tools.NullStr(RdrList["Orders_cashier_url"]);
                    entity.Orders_Loan_proj_no = Tools.NullStr(RdrList["Orders_Loan_proj_no"]);
                    entity.Orders_Responsible = Tools.NullInt(RdrList["Orders_Responsible"]);
                    entity.Orders_IsShow = Tools.NullInt(RdrList["Orders_IsShow"]);
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

        public virtual OrdersInfo GetSupplierOrderInfoBySN(string SN, int Supplier_ID)
        {
            OrdersInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Orders WHERE Orders_SN = '" + SN + "' and Orders_SupplierID=" + Supplier_ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new OrdersInfo();

                    entity.Orders_ID = Tools.NullInt(RdrList["Orders_ID"]);
                    entity.Orders_SN = Tools.NullStr(RdrList["Orders_SN"]);
                    entity.Orders_Type = Tools.NullInt(RdrList["Orders_Type"]);
                    entity.Orders_ContractID = Tools.NullInt(RdrList["Orders_ContractID"]);
                    entity.Orders_BuyerType = Tools.NullInt(RdrList["Orders_BuyerType"]);
                    entity.Orders_BuyerID = Tools.NullInt(RdrList["Orders_BuyerID"]);
                    entity.Orders_SysUserID = Tools.NullInt(RdrList["Orders_SysUserID"]);
                    entity.Orders_Status = Tools.NullInt(RdrList["Orders_Status"]);
                    entity.Orders_ERPSyncStatus = Tools.NullInt(RdrList["Orders_ERPSyncStatus"]);
                    entity.Orders_PaymentStatus = Tools.NullInt(RdrList["Orders_PaymentStatus"]);
                    entity.Orders_PaymentStatus_Time = Tools.NullDate(RdrList["Orders_PaymentStatus_Time"]);
                    entity.Orders_DeliveryStatus = Tools.NullInt(RdrList["Orders_DeliveryStatus"]);
                    entity.Orders_DeliveryStatus_Time = Tools.NullDate(RdrList["Orders_DeliveryStatus_Time"]);
                    entity.Orders_InvoiceStatus = Tools.NullInt(RdrList["Orders_InvoiceStatus"]);
                    entity.Orders_Fail_SysUserID = Tools.NullInt(RdrList["Orders_Fail_SysUserID"]);
                    entity.Orders_Fail_Note = Tools.NullStr(RdrList["Orders_Fail_Note"]);
                    entity.Orders_Fail_Addtime = Tools.NullDate(RdrList["Orders_Fail_Addtime"]);
                    entity.Orders_IsReturnCoin = Tools.NullInt(RdrList["Orders_IsReturnCoin"]);
                    entity.Orders_Total_MKTPrice = Tools.NullDbl(RdrList["Orders_Total_MKTPrice"]);
                    entity.Orders_Total_Price = Tools.NullDbl(RdrList["Orders_Total_Price"]);
                    entity.Orders_Total_Freight = Tools.NullDbl(RdrList["Orders_Total_Freight"]);
                    entity.Orders_Total_Coin = Tools.NullInt(RdrList["Orders_Total_Coin"]);
                    entity.Orders_Total_UseCoin = Tools.NullInt(RdrList["Orders_Total_UseCoin"]);
                    entity.Orders_Total_PriceDiscount = Tools.NullDbl(RdrList["Orders_Total_PriceDiscount"]);
                    entity.Orders_Total_FreightDiscount = Tools.NullDbl(RdrList["Orders_Total_FreightDiscount"]);
                    entity.Orders_Total_PriceDiscount_Note = Tools.NullStr(RdrList["Orders_Total_PriceDiscount_Note"]);
                    entity.Orders_Total_FreightDiscount_Note = Tools.NullStr(RdrList["Orders_Total_FreightDiscount_Note"]);
                    entity.Orders_Total_AllPrice = Tools.NullDbl(RdrList["Orders_Total_AllPrice"]);
                    entity.Orders_Address_ID = Tools.NullInt(RdrList["Orders_Address_ID"]);
                    entity.Orders_Address_Country = Tools.NullStr(RdrList["Orders_Address_Country"]);
                    entity.Orders_Address_State = Tools.NullStr(RdrList["Orders_Address_State"]);
                    entity.Orders_Address_City = Tools.NullStr(RdrList["Orders_Address_City"]);
                    entity.Orders_Address_County = Tools.NullStr(RdrList["Orders_Address_County"]);
                    entity.Orders_Address_StreetAddress = Tools.NullStr(RdrList["Orders_Address_StreetAddress"]);
                    entity.Orders_Address_Zip = Tools.NullStr(RdrList["Orders_Address_Zip"]);
                    entity.Orders_Address_Name = Tools.NullStr(RdrList["Orders_Address_Name"]);
                    entity.Orders_Address_Phone_Countrycode = Tools.NullStr(RdrList["Orders_Address_Phone_Countrycode"]);
                    entity.Orders_Address_Phone_Areacode = Tools.NullStr(RdrList["Orders_Address_Phone_Areacode"]);
                    entity.Orders_Address_Phone_Number = Tools.NullStr(RdrList["Orders_Address_Phone_Number"]);
                    entity.Orders_Address_Mobile = Tools.NullStr(RdrList["Orders_Address_Mobile"]);
                    entity.Orders_Delivery_Time_ID = Tools.NullInt(RdrList["Orders_Delivery_Time_ID"]);
                    entity.Orders_Delivery = Tools.NullInt(RdrList["Orders_Delivery"]);
                    entity.Orders_Delivery_Name = Tools.NullStr(RdrList["Orders_Delivery_Name"]);
                    entity.Orders_Payway = Tools.NullInt(RdrList["Orders_Payway"]);
                    entity.Orders_Payway_Name = Tools.NullStr(RdrList["Orders_Payway_Name"]);
                    entity.Orders_PayType = Tools.NullInt(RdrList["Orders_PayType"]);
                    entity.Orders_PayType_Name = Tools.NullStr(RdrList["Orders_PayType_Name"]);
                    entity.Orders_Note = Tools.NullStr(RdrList["Orders_Note"]);
                    entity.Orders_Admin_Note = Tools.NullStr(RdrList["Orders_Admin_Note"]);
                    entity.Orders_Admin_Sign = Tools.NullInt(RdrList["Orders_Admin_Sign"]);
                    entity.Orders_Site = Tools.NullStr(RdrList["Orders_Site"]);
                    entity.Orders_SourceType = Tools.NullInt(RdrList["Orders_SourceType"]);
                    entity.Orders_Source = Tools.NullStr(RdrList["Orders_Source"]);
                    entity.Orders_VerifyCode = Tools.NullStr(RdrList["Orders_VerifyCode"]);
                    entity.U_Orders_IsMonitor = Tools.NullInt(RdrList["U_Orders_IsMonitor"]);
                    entity.Orders_Addtime = Tools.NullDate(RdrList["Orders_Addtime"]);
                    entity.Orders_From = Tools.NullStr(RdrList["Orders_From"]);
                    entity.Orders_Account_Pay = Tools.NullDbl(RdrList["Orders_Account_Pay"]);
                    entity.Orders_IsEvaluate = Tools.NullInt(RdrList["Orders_IsEvaluate"]);
                    entity.Orders_IsSettling = Tools.NullInt(RdrList["Orders_IsSettling"]);
                    entity.Orders_SupplierID = Tools.NullInt(RdrList["Orders_SupplierID"]);
                    entity.Orders_PurchaseID = Tools.NullInt(RdrList["Orders_PurchaseID"]);
                    entity.Orders_PriceReportID = Tools.NullInt(RdrList["Orders_PriceReportID"]);
                    entity.Orders_MemberStatus = Tools.NullInt(RdrList["Orders_MemberStatus"]);
                    entity.Orders_SupplierStatus = Tools.NullInt(RdrList["Orders_SupplierStatus"]);
                    entity.Orders_MemberStatus_Time = Tools.NullDate(RdrList["Orders_MemberStatus_Time"]);
                    entity.Orders_SupplierStatus_Time = Tools.NullDate(RdrList["Orders_SupplierStatus_Time"]);
                    entity.Orders_ContractAdd = Tools.NullStr(RdrList["Orders_ContractAdd"]);
                    entity.Orders_ApplyCreditAmount = Tools.NullDbl(RdrList["Orders_ApplyCreditAmount"]);
                    entity.Orders_AgreementNo = Tools.NullStr(RdrList["Orders_AgreementNo"]);
                    entity.Orders_LoanTermID = Tools.NullInt(RdrList["Orders_LoanTermID"]);
                    entity.Orders_LoanMethodID = Tools.NullInt(RdrList["Orders_LoanMethodID"]);
                    entity.Orders_Fee = Tools.NullDbl(RdrList["Orders_Fee"]);
                    entity.Orders_MarginFee = Tools.NullDbl(RdrList["Orders_MarginFee"]);
                    entity.Orders_FeeRate = Tools.NullDbl(RdrList["Orders_FeeRate"]);
                    entity.Orders_MarginRate = Tools.NullDbl(RdrList["Orders_MarginRate"]);
                    entity.Orders_cashier_url = Tools.NullStr(RdrList["Orders_cashier_url"]);
                    entity.Orders_Loan_proj_no = Tools.NullStr(RdrList["Orders_Loan_proj_no"]);
                    entity.Orders_Responsible = Tools.NullInt(RdrList["Orders_Responsible"]);
                    entity.Orders_IsShow = Tools.NullInt(RdrList["Orders_IsShow"]);
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
         
        public virtual OrdersInfo GetMemberOrderInfoBySN(string SN, int Member_ID)
        {
            OrdersInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Orders WHERE Orders_SN = '" + SN + "' and Orders_BuyerID=" + Member_ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new OrdersInfo();

                    entity.Orders_ID = Tools.NullInt(RdrList["Orders_ID"]);
                    entity.Orders_SN = Tools.NullStr(RdrList["Orders_SN"]);
                    entity.Orders_Type = Tools.NullInt(RdrList["Orders_Type"]);
                    entity.Orders_ContractID = Tools.NullInt(RdrList["Orders_ContractID"]);
                    entity.Orders_BuyerType = Tools.NullInt(RdrList["Orders_BuyerType"]);
                    entity.Orders_BuyerID = Tools.NullInt(RdrList["Orders_BuyerID"]);
                    entity.Orders_SysUserID = Tools.NullInt(RdrList["Orders_SysUserID"]);
                    entity.Orders_Status = Tools.NullInt(RdrList["Orders_Status"]);
                    entity.Orders_ERPSyncStatus = Tools.NullInt(RdrList["Orders_ERPSyncStatus"]);
                    entity.Orders_PaymentStatus = Tools.NullInt(RdrList["Orders_PaymentStatus"]);
                    entity.Orders_PaymentStatus_Time = Tools.NullDate(RdrList["Orders_PaymentStatus_Time"]);
                    entity.Orders_DeliveryStatus = Tools.NullInt(RdrList["Orders_DeliveryStatus"]);
                    entity.Orders_DeliveryStatus_Time = Tools.NullDate(RdrList["Orders_DeliveryStatus_Time"]);
                    entity.Orders_InvoiceStatus = Tools.NullInt(RdrList["Orders_InvoiceStatus"]);
                    entity.Orders_Fail_SysUserID = Tools.NullInt(RdrList["Orders_Fail_SysUserID"]);
                    entity.Orders_Fail_Note = Tools.NullStr(RdrList["Orders_Fail_Note"]);
                    entity.Orders_Fail_Addtime = Tools.NullDate(RdrList["Orders_Fail_Addtime"]);
                    entity.Orders_IsReturnCoin = Tools.NullInt(RdrList["Orders_IsReturnCoin"]);
                    entity.Orders_Total_MKTPrice = Tools.NullDbl(RdrList["Orders_Total_MKTPrice"]);
                    entity.Orders_Total_Price = Tools.NullDbl(RdrList["Orders_Total_Price"]);
                    entity.Orders_Total_Freight = Tools.NullDbl(RdrList["Orders_Total_Freight"]);
                    entity.Orders_Total_Coin = Tools.NullInt(RdrList["Orders_Total_Coin"]);
                    entity.Orders_Total_UseCoin = Tools.NullInt(RdrList["Orders_Total_UseCoin"]);
                    entity.Orders_Total_PriceDiscount = Tools.NullDbl(RdrList["Orders_Total_PriceDiscount"]);
                    entity.Orders_Total_FreightDiscount = Tools.NullDbl(RdrList["Orders_Total_FreightDiscount"]);
                    entity.Orders_Total_PriceDiscount_Note = Tools.NullStr(RdrList["Orders_Total_PriceDiscount_Note"]);
                    entity.Orders_Total_FreightDiscount_Note = Tools.NullStr(RdrList["Orders_Total_FreightDiscount_Note"]);
                    entity.Orders_Total_AllPrice = Tools.NullDbl(RdrList["Orders_Total_AllPrice"]);
                    entity.Orders_Address_ID = Tools.NullInt(RdrList["Orders_Address_ID"]);
                    entity.Orders_Address_Country = Tools.NullStr(RdrList["Orders_Address_Country"]);
                    entity.Orders_Address_State = Tools.NullStr(RdrList["Orders_Address_State"]);
                    entity.Orders_Address_City = Tools.NullStr(RdrList["Orders_Address_City"]);
                    entity.Orders_Address_County = Tools.NullStr(RdrList["Orders_Address_County"]);
                    entity.Orders_Address_StreetAddress = Tools.NullStr(RdrList["Orders_Address_StreetAddress"]);
                    entity.Orders_Address_Zip = Tools.NullStr(RdrList["Orders_Address_Zip"]);
                    entity.Orders_Address_Name = Tools.NullStr(RdrList["Orders_Address_Name"]);
                    entity.Orders_Address_Phone_Countrycode = Tools.NullStr(RdrList["Orders_Address_Phone_Countrycode"]);
                    entity.Orders_Address_Phone_Areacode = Tools.NullStr(RdrList["Orders_Address_Phone_Areacode"]);
                    entity.Orders_Address_Phone_Number = Tools.NullStr(RdrList["Orders_Address_Phone_Number"]);
                    entity.Orders_Address_Mobile = Tools.NullStr(RdrList["Orders_Address_Mobile"]);
                    entity.Orders_Delivery_Time_ID = Tools.NullInt(RdrList["Orders_Delivery_Time_ID"]);
                    entity.Orders_Delivery = Tools.NullInt(RdrList["Orders_Delivery"]);
                    entity.Orders_Delivery_Name = Tools.NullStr(RdrList["Orders_Delivery_Name"]);
                    entity.Orders_Payway = Tools.NullInt(RdrList["Orders_Payway"]);
                    entity.Orders_Payway_Name = Tools.NullStr(RdrList["Orders_Payway_Name"]);
                    entity.Orders_PayType = Tools.NullInt(RdrList["Orders_PayType"]);
                    entity.Orders_PayType_Name = Tools.NullStr(RdrList["Orders_PayType_Name"]);
                    entity.Orders_Note = Tools.NullStr(RdrList["Orders_Note"]);
                    entity.Orders_Admin_Note = Tools.NullStr(RdrList["Orders_Admin_Note"]);
                    entity.Orders_Admin_Sign = Tools.NullInt(RdrList["Orders_Admin_Sign"]);
                    entity.Orders_Site = Tools.NullStr(RdrList["Orders_Site"]);
                    entity.Orders_SourceType = Tools.NullInt(RdrList["Orders_SourceType"]);
                    entity.Orders_Source = Tools.NullStr(RdrList["Orders_Source"]);
                    entity.Orders_VerifyCode = Tools.NullStr(RdrList["Orders_VerifyCode"]);
                    entity.U_Orders_IsMonitor = Tools.NullInt(RdrList["U_Orders_IsMonitor"]);
                    entity.Orders_Addtime = Tools.NullDate(RdrList["Orders_Addtime"]);
                    entity.Orders_From = Tools.NullStr(RdrList["Orders_From"]);
                    entity.Orders_Account_Pay = Tools.NullDbl(RdrList["Orders_Account_Pay"]);
                    entity.Orders_IsEvaluate = Tools.NullInt(RdrList["Orders_IsEvaluate"]);
                    entity.Orders_IsSettling = Tools.NullInt(RdrList["Orders_IsSettling"]);
                    entity.Orders_SupplierID = Tools.NullInt(RdrList["Orders_SupplierID"]);
                    entity.Orders_PurchaseID = Tools.NullInt(RdrList["Orders_PurchaseID"]);
                    entity.Orders_PriceReportID = Tools.NullInt(RdrList["Orders_PriceReportID"]);
                    entity.Orders_MemberStatus = Tools.NullInt(RdrList["Orders_MemberStatus"]);
                    entity.Orders_SupplierStatus = Tools.NullInt(RdrList["Orders_SupplierStatus"]);
                    entity.Orders_MemberStatus_Time = Tools.NullDate(RdrList["Orders_MemberStatus_Time"]);
                    entity.Orders_SupplierStatus_Time = Tools.NullDate(RdrList["Orders_SupplierStatus_Time"]);
                    entity.Orders_ContractAdd = Tools.NullStr(RdrList["Orders_ContractAdd"]);
                    entity.Orders_ApplyCreditAmount = Tools.NullDbl(RdrList["Orders_ApplyCreditAmount"]);
                    entity.Orders_AgreementNo = Tools.NullStr(RdrList["Orders_AgreementNo"]);
                    entity.Orders_LoanTermID = Tools.NullInt(RdrList["Orders_LoanTermID"]);
                    entity.Orders_LoanMethodID = Tools.NullInt(RdrList["Orders_LoanMethodID"]);
                    entity.Orders_Fee = Tools.NullDbl(RdrList["Orders_Fee"]);
                    entity.Orders_MarginFee = Tools.NullDbl(RdrList["Orders_MarginFee"]);
                    entity.Orders_FeeRate = Tools.NullDbl(RdrList["Orders_FeeRate"]);
                    entity.Orders_MarginRate = Tools.NullDbl(RdrList["Orders_MarginRate"]);
                    entity.Orders_cashier_url = Tools.NullStr(RdrList["Orders_cashier_url"]);
                    entity.Orders_Loan_proj_no = Tools.NullStr(RdrList["Orders_Loan_proj_no"]);
                    entity.Orders_Responsible = Tools.NullInt(RdrList["Orders_Responsible"]);
                    entity.Orders_IsShow = Tools.NullInt(RdrList["Orders_IsShow"]);
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

        public virtual OrdersInfo GetMemberTopOrdersInfo(int Member_ID)
        {
            OrdersInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT top(1) * FROM Orders WHERE Orders_BuyerID=" + Member_ID+" order by Orders_ID desc";
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new OrdersInfo();

                    entity.Orders_ID = Tools.NullInt(RdrList["Orders_ID"]);
                    entity.Orders_SN = Tools.NullStr(RdrList["Orders_SN"]);
                    entity.Orders_Type = Tools.NullInt(RdrList["Orders_Type"]);
                    entity.Orders_ContractID = Tools.NullInt(RdrList["Orders_ContractID"]);
                    entity.Orders_BuyerType = Tools.NullInt(RdrList["Orders_BuyerType"]);
                    entity.Orders_BuyerID = Tools.NullInt(RdrList["Orders_BuyerID"]);
                    entity.Orders_SysUserID = Tools.NullInt(RdrList["Orders_SysUserID"]);
                    entity.Orders_Status = Tools.NullInt(RdrList["Orders_Status"]);
                    entity.Orders_ERPSyncStatus = Tools.NullInt(RdrList["Orders_ERPSyncStatus"]);
                    entity.Orders_PaymentStatus = Tools.NullInt(RdrList["Orders_PaymentStatus"]);
                    entity.Orders_PaymentStatus_Time = Tools.NullDate(RdrList["Orders_PaymentStatus_Time"]);
                    entity.Orders_DeliveryStatus = Tools.NullInt(RdrList["Orders_DeliveryStatus"]);
                    entity.Orders_DeliveryStatus_Time = Tools.NullDate(RdrList["Orders_DeliveryStatus_Time"]);
                    entity.Orders_InvoiceStatus = Tools.NullInt(RdrList["Orders_InvoiceStatus"]);
                    entity.Orders_Fail_SysUserID = Tools.NullInt(RdrList["Orders_Fail_SysUserID"]);
                    entity.Orders_Fail_Note = Tools.NullStr(RdrList["Orders_Fail_Note"]);
                    entity.Orders_Fail_Addtime = Tools.NullDate(RdrList["Orders_Fail_Addtime"]);
                    entity.Orders_IsReturnCoin = Tools.NullInt(RdrList["Orders_IsReturnCoin"]);
                    entity.Orders_Total_MKTPrice = Tools.NullDbl(RdrList["Orders_Total_MKTPrice"]);
                    entity.Orders_Total_Price = Tools.NullDbl(RdrList["Orders_Total_Price"]);
                    entity.Orders_Total_Freight = Tools.NullDbl(RdrList["Orders_Total_Freight"]);
                    entity.Orders_Total_Coin = Tools.NullInt(RdrList["Orders_Total_Coin"]);
                    entity.Orders_Total_UseCoin = Tools.NullInt(RdrList["Orders_Total_UseCoin"]);
                    entity.Orders_Total_PriceDiscount = Tools.NullDbl(RdrList["Orders_Total_PriceDiscount"]);
                    entity.Orders_Total_FreightDiscount = Tools.NullDbl(RdrList["Orders_Total_FreightDiscount"]);
                    entity.Orders_Total_PriceDiscount_Note = Tools.NullStr(RdrList["Orders_Total_PriceDiscount_Note"]);
                    entity.Orders_Total_FreightDiscount_Note = Tools.NullStr(RdrList["Orders_Total_FreightDiscount_Note"]);
                    entity.Orders_Total_AllPrice = Tools.NullDbl(RdrList["Orders_Total_AllPrice"]);
                    entity.Orders_Address_ID = Tools.NullInt(RdrList["Orders_Address_ID"]);
                    entity.Orders_Address_Country = Tools.NullStr(RdrList["Orders_Address_Country"]);
                    entity.Orders_Address_State = Tools.NullStr(RdrList["Orders_Address_State"]);
                    entity.Orders_Address_City = Tools.NullStr(RdrList["Orders_Address_City"]);
                    entity.Orders_Address_County = Tools.NullStr(RdrList["Orders_Address_County"]);
                    entity.Orders_Address_StreetAddress = Tools.NullStr(RdrList["Orders_Address_StreetAddress"]);
                    entity.Orders_Address_Zip = Tools.NullStr(RdrList["Orders_Address_Zip"]);
                    entity.Orders_Address_Name = Tools.NullStr(RdrList["Orders_Address_Name"]);
                    entity.Orders_Address_Phone_Countrycode = Tools.NullStr(RdrList["Orders_Address_Phone_Countrycode"]);
                    entity.Orders_Address_Phone_Areacode = Tools.NullStr(RdrList["Orders_Address_Phone_Areacode"]);
                    entity.Orders_Address_Phone_Number = Tools.NullStr(RdrList["Orders_Address_Phone_Number"]);
                    entity.Orders_Address_Mobile = Tools.NullStr(RdrList["Orders_Address_Mobile"]);
                    entity.Orders_Delivery_Time_ID = Tools.NullInt(RdrList["Orders_Delivery_Time_ID"]);
                    entity.Orders_Delivery = Tools.NullInt(RdrList["Orders_Delivery"]);
                    entity.Orders_Delivery_Name = Tools.NullStr(RdrList["Orders_Delivery_Name"]);
                    entity.Orders_Payway = Tools.NullInt(RdrList["Orders_Payway"]);
                    entity.Orders_Payway_Name = Tools.NullStr(RdrList["Orders_Payway_Name"]);
                    entity.Orders_PayType = Tools.NullInt(RdrList["Orders_PayType"]);
                    entity.Orders_PayType_Name = Tools.NullStr(RdrList["Orders_PayType_Name"]);
                    entity.Orders_Note = Tools.NullStr(RdrList["Orders_Note"]);
                    entity.Orders_Admin_Note = Tools.NullStr(RdrList["Orders_Admin_Note"]);
                    entity.Orders_Admin_Sign = Tools.NullInt(RdrList["Orders_Admin_Sign"]);
                    entity.Orders_Site = Tools.NullStr(RdrList["Orders_Site"]);
                    entity.Orders_SourceType = Tools.NullInt(RdrList["Orders_SourceType"]);
                    entity.Orders_Source = Tools.NullStr(RdrList["Orders_Source"]);
                    entity.Orders_VerifyCode = Tools.NullStr(RdrList["Orders_VerifyCode"]);
                    entity.U_Orders_IsMonitor = Tools.NullInt(RdrList["U_Orders_IsMonitor"]);
                    entity.Orders_Addtime = Tools.NullDate(RdrList["Orders_Addtime"]);
                    entity.Orders_From = Tools.NullStr(RdrList["Orders_From"]);
                    entity.Orders_Account_Pay = Tools.NullDbl(RdrList["Orders_Account_Pay"]);
                    entity.Orders_IsEvaluate = Tools.NullInt(RdrList["Orders_IsEvaluate"]);
                    entity.Orders_IsSettling = Tools.NullInt(RdrList["Orders_IsSettling"]);
                    entity.Orders_SupplierID = Tools.NullInt(RdrList["Orders_SupplierID"]);
                    entity.Orders_PurchaseID = Tools.NullInt(RdrList["Orders_PurchaseID"]);
                    entity.Orders_PriceReportID = Tools.NullInt(RdrList["Orders_PriceReportID"]);
                    entity.Orders_MemberStatus = Tools.NullInt(RdrList["Orders_MemberStatus"]);
                    entity.Orders_SupplierStatus = Tools.NullInt(RdrList["Orders_SupplierStatus"]);
                    entity.Orders_MemberStatus_Time = Tools.NullDate(RdrList["Orders_MemberStatus_Time"]);
                    entity.Orders_SupplierStatus_Time = Tools.NullDate(RdrList["Orders_SupplierStatus_Time"]);
                    entity.Orders_ContractAdd = Tools.NullStr(RdrList["Orders_ContractAdd"]);
                    entity.Orders_ApplyCreditAmount = Tools.NullDbl(RdrList["Orders_ApplyCreditAmount"]);
                    entity.Orders_AgreementNo = Tools.NullStr(RdrList["Orders_AgreementNo"]);
                    entity.Orders_LoanTermID = Tools.NullInt(RdrList["Orders_LoanTermID"]);
                    entity.Orders_LoanMethodID = Tools.NullInt(RdrList["Orders_LoanMethodID"]);
                    entity.Orders_Fee = Tools.NullDbl(RdrList["Orders_Fee"]);
                    entity.Orders_MarginFee = Tools.NullDbl(RdrList["Orders_MarginFee"]);
                    entity.Orders_FeeRate = Tools.NullDbl(RdrList["Orders_FeeRate"]);
                    entity.Orders_MarginRate = Tools.NullDbl(RdrList["Orders_MarginRate"]);
                    entity.Orders_cashier_url = Tools.NullStr(RdrList["Orders_cashier_url"]);
                    entity.Orders_Loan_proj_no = Tools.NullStr(RdrList["Orders_Loan_proj_no"]);
                    entity.Orders_Responsible = Tools.NullInt(RdrList["Orders_Responsible"]);
                    entity.Orders_IsShow = Tools.NullInt(RdrList["Orders_IsShow"]);
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

        public virtual OrdersInfo GetOrdersByPurchaseID(int Purchase_ID)
        {
            OrdersInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Orders WHERE Orders_PurchaseID = " + Purchase_ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new OrdersInfo();

                    entity.Orders_ID = Tools.NullInt(RdrList["Orders_ID"]);
                    entity.Orders_SN = Tools.NullStr(RdrList["Orders_SN"]);
                    entity.Orders_Type = Tools.NullInt(RdrList["Orders_Type"]);
                    entity.Orders_ContractID = Tools.NullInt(RdrList["Orders_ContractID"]);
                    entity.Orders_BuyerType = Tools.NullInt(RdrList["Orders_BuyerType"]);
                    entity.Orders_BuyerID = Tools.NullInt(RdrList["Orders_BuyerID"]);
                    entity.Orders_SysUserID = Tools.NullInt(RdrList["Orders_SysUserID"]);
                    entity.Orders_Status = Tools.NullInt(RdrList["Orders_Status"]);
                    entity.Orders_ERPSyncStatus = Tools.NullInt(RdrList["Orders_ERPSyncStatus"]);
                    entity.Orders_PaymentStatus = Tools.NullInt(RdrList["Orders_PaymentStatus"]);
                    entity.Orders_PaymentStatus_Time = Tools.NullDate(RdrList["Orders_PaymentStatus_Time"]);
                    entity.Orders_DeliveryStatus = Tools.NullInt(RdrList["Orders_DeliveryStatus"]);
                    entity.Orders_DeliveryStatus_Time = Tools.NullDate(RdrList["Orders_DeliveryStatus_Time"]);
                    entity.Orders_InvoiceStatus = Tools.NullInt(RdrList["Orders_InvoiceStatus"]);
                    entity.Orders_Fail_SysUserID = Tools.NullInt(RdrList["Orders_Fail_SysUserID"]);
                    entity.Orders_Fail_Note = Tools.NullStr(RdrList["Orders_Fail_Note"]);
                    entity.Orders_Fail_Addtime = Tools.NullDate(RdrList["Orders_Fail_Addtime"]);
                    entity.Orders_IsReturnCoin = Tools.NullInt(RdrList["Orders_IsReturnCoin"]);
                    entity.Orders_Total_MKTPrice = Tools.NullDbl(RdrList["Orders_Total_MKTPrice"]);
                    entity.Orders_Total_Price = Tools.NullDbl(RdrList["Orders_Total_Price"]);
                    entity.Orders_Total_Freight = Tools.NullDbl(RdrList["Orders_Total_Freight"]);
                    entity.Orders_Total_Coin = Tools.NullInt(RdrList["Orders_Total_Coin"]);
                    entity.Orders_Total_UseCoin = Tools.NullInt(RdrList["Orders_Total_UseCoin"]);
                    entity.Orders_Total_PriceDiscount = Tools.NullDbl(RdrList["Orders_Total_PriceDiscount"]);
                    entity.Orders_Total_FreightDiscount = Tools.NullDbl(RdrList["Orders_Total_FreightDiscount"]);
                    entity.Orders_Total_PriceDiscount_Note = Tools.NullStr(RdrList["Orders_Total_PriceDiscount_Note"]);
                    entity.Orders_Total_FreightDiscount_Note = Tools.NullStr(RdrList["Orders_Total_FreightDiscount_Note"]);
                    entity.Orders_Total_AllPrice = Tools.NullDbl(RdrList["Orders_Total_AllPrice"]);
                    entity.Orders_Address_ID = Tools.NullInt(RdrList["Orders_Address_ID"]);
                    entity.Orders_Address_Country = Tools.NullStr(RdrList["Orders_Address_Country"]);
                    entity.Orders_Address_State = Tools.NullStr(RdrList["Orders_Address_State"]);
                    entity.Orders_Address_City = Tools.NullStr(RdrList["Orders_Address_City"]);
                    entity.Orders_Address_County = Tools.NullStr(RdrList["Orders_Address_County"]);
                    entity.Orders_Address_StreetAddress = Tools.NullStr(RdrList["Orders_Address_StreetAddress"]);
                    entity.Orders_Address_Zip = Tools.NullStr(RdrList["Orders_Address_Zip"]);
                    entity.Orders_Address_Name = Tools.NullStr(RdrList["Orders_Address_Name"]);
                    entity.Orders_Address_Phone_Countrycode = Tools.NullStr(RdrList["Orders_Address_Phone_Countrycode"]);
                    entity.Orders_Address_Phone_Areacode = Tools.NullStr(RdrList["Orders_Address_Phone_Areacode"]);
                    entity.Orders_Address_Phone_Number = Tools.NullStr(RdrList["Orders_Address_Phone_Number"]);
                    entity.Orders_Address_Mobile = Tools.NullStr(RdrList["Orders_Address_Mobile"]);
                    entity.Orders_Delivery_Time_ID = Tools.NullInt(RdrList["Orders_Delivery_Time_ID"]);
                    entity.Orders_Delivery = Tools.NullInt(RdrList["Orders_Delivery"]);
                    entity.Orders_Delivery_Name = Tools.NullStr(RdrList["Orders_Delivery_Name"]);
                    entity.Orders_Payway = Tools.NullInt(RdrList["Orders_Payway"]);
                    entity.Orders_Payway_Name = Tools.NullStr(RdrList["Orders_Payway_Name"]);
                    entity.Orders_PayType = Tools.NullInt(RdrList["Orders_PayType"]);
                    entity.Orders_PayType_Name = Tools.NullStr(RdrList["Orders_PayType_Name"]);
                    entity.Orders_Note = Tools.NullStr(RdrList["Orders_Note"]);
                    entity.Orders_Admin_Note = Tools.NullStr(RdrList["Orders_Admin_Note"]);
                    entity.Orders_Admin_Sign = Tools.NullInt(RdrList["Orders_Admin_Sign"]);
                    entity.Orders_Site = Tools.NullStr(RdrList["Orders_Site"]);
                    entity.Orders_SourceType = Tools.NullInt(RdrList["Orders_SourceType"]);
                    entity.Orders_Source = Tools.NullStr(RdrList["Orders_Source"]);
                    entity.Orders_VerifyCode = Tools.NullStr(RdrList["Orders_VerifyCode"]);
                    entity.U_Orders_IsMonitor = Tools.NullInt(RdrList["U_Orders_IsMonitor"]);
                    entity.Orders_Addtime = Tools.NullDate(RdrList["Orders_Addtime"]);
                    entity.Orders_From = Tools.NullStr(RdrList["Orders_From"]);
                    entity.Orders_Account_Pay = Tools.NullDbl(RdrList["Orders_Account_Pay"]);
                    entity.Orders_IsEvaluate = Tools.NullInt(RdrList["Orders_IsEvaluate"]);
                    entity.Orders_IsSettling = Tools.NullInt(RdrList["Orders_IsSettling"]);
                    entity.Orders_SupplierID = Tools.NullInt(RdrList["Orders_SupplierID"]);
                    entity.Orders_PurchaseID = Tools.NullInt(RdrList["Orders_PurchaseID"]);
                    entity.Orders_PriceReportID = Tools.NullInt(RdrList["Orders_PriceReportID"]);
                    entity.Orders_MemberStatus = Tools.NullInt(RdrList["Orders_MemberStatus"]);
                    entity.Orders_SupplierStatus = Tools.NullInt(RdrList["Orders_SupplierStatus"]);
                    entity.Orders_MemberStatus_Time = Tools.NullDate(RdrList["Orders_MemberStatus_Time"]);
                    entity.Orders_SupplierStatus_Time = Tools.NullDate(RdrList["Orders_SupplierStatus_Time"]);
                    entity.Orders_ContractAdd = Tools.NullStr(RdrList["Orders_ContractAdd"]);
                    entity.Orders_ApplyCreditAmount = Tools.NullDbl(RdrList["Orders_ApplyCreditAmount"]);
                    entity.Orders_AgreementNo = Tools.NullStr(RdrList["Orders_AgreementNo"]);
                    entity.Orders_LoanTermID = Tools.NullInt(RdrList["Orders_LoanTermID"]);
                    entity.Orders_LoanMethodID = Tools.NullInt(RdrList["Orders_LoanMethodID"]);
                    entity.Orders_Fee = Tools.NullDbl(RdrList["Orders_Fee"]);
                    entity.Orders_MarginFee = Tools.NullDbl(RdrList["Orders_MarginFee"]);
                    entity.Orders_FeeRate = Tools.NullDbl(RdrList["Orders_FeeRate"]);
                    entity.Orders_MarginRate = Tools.NullDbl(RdrList["Orders_MarginRate"]);
                    entity.Orders_cashier_url = Tools.NullStr(RdrList["Orders_cashier_url"]);
                    entity.Orders_Loan_proj_no = Tools.NullStr(RdrList["Orders_Loan_proj_no"]);
                    entity.Orders_Responsible = Tools.NullInt(RdrList["Orders_Responsible"]);
                    entity.Orders_IsShow = Tools.NullInt(RdrList["Orders_IsShow"]);
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

        public virtual OrdersInfo GetOrdersByPriceReportID(int PriceReportID)
        {
            OrdersInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Orders WHERE Orders_PriceReportID = " + PriceReportID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new OrdersInfo();

                    entity.Orders_ID = Tools.NullInt(RdrList["Orders_ID"]);
                    entity.Orders_SN = Tools.NullStr(RdrList["Orders_SN"]);
                    entity.Orders_Type = Tools.NullInt(RdrList["Orders_Type"]);
                    entity.Orders_ContractID = Tools.NullInt(RdrList["Orders_ContractID"]);
                    entity.Orders_BuyerType = Tools.NullInt(RdrList["Orders_BuyerType"]);
                    entity.Orders_BuyerID = Tools.NullInt(RdrList["Orders_BuyerID"]);
                    entity.Orders_SysUserID = Tools.NullInt(RdrList["Orders_SysUserID"]);
                    entity.Orders_Status = Tools.NullInt(RdrList["Orders_Status"]);
                    entity.Orders_ERPSyncStatus = Tools.NullInt(RdrList["Orders_ERPSyncStatus"]);
                    entity.Orders_PaymentStatus = Tools.NullInt(RdrList["Orders_PaymentStatus"]);
                    entity.Orders_PaymentStatus_Time = Tools.NullDate(RdrList["Orders_PaymentStatus_Time"]);
                    entity.Orders_DeliveryStatus = Tools.NullInt(RdrList["Orders_DeliveryStatus"]);
                    entity.Orders_DeliveryStatus_Time = Tools.NullDate(RdrList["Orders_DeliveryStatus_Time"]);
                    entity.Orders_InvoiceStatus = Tools.NullInt(RdrList["Orders_InvoiceStatus"]);
                    entity.Orders_Fail_SysUserID = Tools.NullInt(RdrList["Orders_Fail_SysUserID"]);
                    entity.Orders_Fail_Note = Tools.NullStr(RdrList["Orders_Fail_Note"]);
                    entity.Orders_Fail_Addtime = Tools.NullDate(RdrList["Orders_Fail_Addtime"]);
                    entity.Orders_IsReturnCoin = Tools.NullInt(RdrList["Orders_IsReturnCoin"]);
                    entity.Orders_Total_MKTPrice = Tools.NullDbl(RdrList["Orders_Total_MKTPrice"]);
                    entity.Orders_Total_Price = Tools.NullDbl(RdrList["Orders_Total_Price"]);
                    entity.Orders_Total_Freight = Tools.NullDbl(RdrList["Orders_Total_Freight"]);
                    entity.Orders_Total_Coin = Tools.NullInt(RdrList["Orders_Total_Coin"]);
                    entity.Orders_Total_UseCoin = Tools.NullInt(RdrList["Orders_Total_UseCoin"]);
                    entity.Orders_Total_PriceDiscount = Tools.NullDbl(RdrList["Orders_Total_PriceDiscount"]);
                    entity.Orders_Total_FreightDiscount = Tools.NullDbl(RdrList["Orders_Total_FreightDiscount"]);
                    entity.Orders_Total_PriceDiscount_Note = Tools.NullStr(RdrList["Orders_Total_PriceDiscount_Note"]);
                    entity.Orders_Total_FreightDiscount_Note = Tools.NullStr(RdrList["Orders_Total_FreightDiscount_Note"]);
                    entity.Orders_Total_AllPrice = Tools.NullDbl(RdrList["Orders_Total_AllPrice"]);
                    entity.Orders_Address_ID = Tools.NullInt(RdrList["Orders_Address_ID"]);
                    entity.Orders_Address_Country = Tools.NullStr(RdrList["Orders_Address_Country"]);
                    entity.Orders_Address_State = Tools.NullStr(RdrList["Orders_Address_State"]);
                    entity.Orders_Address_City = Tools.NullStr(RdrList["Orders_Address_City"]);
                    entity.Orders_Address_County = Tools.NullStr(RdrList["Orders_Address_County"]);
                    entity.Orders_Address_StreetAddress = Tools.NullStr(RdrList["Orders_Address_StreetAddress"]);
                    entity.Orders_Address_Zip = Tools.NullStr(RdrList["Orders_Address_Zip"]);
                    entity.Orders_Address_Name = Tools.NullStr(RdrList["Orders_Address_Name"]);
                    entity.Orders_Address_Phone_Countrycode = Tools.NullStr(RdrList["Orders_Address_Phone_Countrycode"]);
                    entity.Orders_Address_Phone_Areacode = Tools.NullStr(RdrList["Orders_Address_Phone_Areacode"]);
                    entity.Orders_Address_Phone_Number = Tools.NullStr(RdrList["Orders_Address_Phone_Number"]);
                    entity.Orders_Address_Mobile = Tools.NullStr(RdrList["Orders_Address_Mobile"]);
                    entity.Orders_Delivery_Time_ID = Tools.NullInt(RdrList["Orders_Delivery_Time_ID"]);
                    entity.Orders_Delivery = Tools.NullInt(RdrList["Orders_Delivery"]);
                    entity.Orders_Delivery_Name = Tools.NullStr(RdrList["Orders_Delivery_Name"]);
                    entity.Orders_Payway = Tools.NullInt(RdrList["Orders_Payway"]);
                    entity.Orders_Payway_Name = Tools.NullStr(RdrList["Orders_Payway_Name"]);
                    entity.Orders_PayType = Tools.NullInt(RdrList["Orders_PayType"]);
                    entity.Orders_PayType_Name = Tools.NullStr(RdrList["Orders_PayType_Name"]);
                    entity.Orders_Note = Tools.NullStr(RdrList["Orders_Note"]);
                    entity.Orders_Admin_Note = Tools.NullStr(RdrList["Orders_Admin_Note"]);
                    entity.Orders_Admin_Sign = Tools.NullInt(RdrList["Orders_Admin_Sign"]);
                    entity.Orders_Site = Tools.NullStr(RdrList["Orders_Site"]);
                    entity.Orders_SourceType = Tools.NullInt(RdrList["Orders_SourceType"]);
                    entity.Orders_Source = Tools.NullStr(RdrList["Orders_Source"]);
                    entity.Orders_VerifyCode = Tools.NullStr(RdrList["Orders_VerifyCode"]);
                    entity.U_Orders_IsMonitor = Tools.NullInt(RdrList["U_Orders_IsMonitor"]);
                    entity.Orders_Addtime = Tools.NullDate(RdrList["Orders_Addtime"]);
                    entity.Orders_From = Tools.NullStr(RdrList["Orders_From"]);
                    entity.Orders_Account_Pay = Tools.NullDbl(RdrList["Orders_Account_Pay"]);
                    entity.Orders_IsEvaluate = Tools.NullInt(RdrList["Orders_IsEvaluate"]);
                    entity.Orders_IsSettling = Tools.NullInt(RdrList["Orders_IsSettling"]);
                    entity.Orders_SupplierID = Tools.NullInt(RdrList["Orders_SupplierID"]);
                    entity.Orders_PurchaseID = Tools.NullInt(RdrList["Orders_PurchaseID"]);
                    entity.Orders_PriceReportID = Tools.NullInt(RdrList["Orders_PriceReportID"]);
                    entity.Orders_MemberStatus = Tools.NullInt(RdrList["Orders_MemberStatus"]);
                    entity.Orders_SupplierStatus = Tools.NullInt(RdrList["Orders_SupplierStatus"]);
                    entity.Orders_MemberStatus_Time = Tools.NullDate(RdrList["Orders_MemberStatus_Time"]);
                    entity.Orders_SupplierStatus_Time = Tools.NullDate(RdrList["Orders_SupplierStatus_Time"]);
                    entity.Orders_ContractAdd = Tools.NullStr(RdrList["Orders_ContractAdd"]);
                    entity.Orders_ApplyCreditAmount = Tools.NullDbl(RdrList["Orders_ApplyCreditAmount"]);
                    entity.Orders_AgreementNo = Tools.NullStr(RdrList["Orders_AgreementNo"]);
                    entity.Orders_LoanTermID = Tools.NullInt(RdrList["Orders_LoanTermID"]);
                    entity.Orders_LoanMethodID = Tools.NullInt(RdrList["Orders_LoanMethodID"]);
                    entity.Orders_Fee = Tools.NullDbl(RdrList["Orders_Fee"]);
                    entity.Orders_MarginFee = Tools.NullDbl(RdrList["Orders_MarginFee"]);
                    entity.Orders_FeeRate = Tools.NullDbl(RdrList["Orders_FeeRate"]);
                    entity.Orders_MarginRate = Tools.NullDbl(RdrList["Orders_MarginRate"]);
                    entity.Orders_cashier_url = Tools.NullStr(RdrList["Orders_cashier_url"]);
                    entity.Orders_Loan_proj_no = Tools.NullStr(RdrList["Orders_Loan_proj_no"]);
                    entity.Orders_Responsible = Tools.NullInt(RdrList["Orders_Responsible"]);
                    entity.Orders_IsShow = Tools.NullInt(RdrList["Orders_IsShow"]);
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

        public virtual IList<OrdersInfo> GetOrderss(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<OrdersInfo> entitys = null;
            OrdersInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Orders";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<OrdersInfo>();
                    while (RdrList.Read())
                    {
                        entity = new OrdersInfo();
                        entity.Orders_ID = Tools.NullInt(RdrList["Orders_ID"]);
                        entity.Orders_SN = Tools.NullStr(RdrList["Orders_SN"]);
                        entity.Orders_Type = Tools.NullInt(RdrList["Orders_Type"]);
                        entity.Orders_ContractID = Tools.NullInt(RdrList["Orders_ContractID"]);
                        entity.Orders_BuyerType = Tools.NullInt(RdrList["Orders_BuyerType"]);
                        entity.Orders_BuyerID = Tools.NullInt(RdrList["Orders_BuyerID"]);
                        entity.Orders_SysUserID = Tools.NullInt(RdrList["Orders_SysUserID"]);
                        entity.Orders_Status = Tools.NullInt(RdrList["Orders_Status"]);
                        entity.Orders_ERPSyncStatus = Tools.NullInt(RdrList["Orders_ERPSyncStatus"]);
                        entity.Orders_PaymentStatus = Tools.NullInt(RdrList["Orders_PaymentStatus"]);
                        entity.Orders_PaymentStatus_Time = Tools.NullDate(RdrList["Orders_PaymentStatus_Time"]);
                        entity.Orders_DeliveryStatus = Tools.NullInt(RdrList["Orders_DeliveryStatus"]);
                        entity.Orders_DeliveryStatus_Time = Tools.NullDate(RdrList["Orders_DeliveryStatus_Time"]);
                        entity.Orders_InvoiceStatus = Tools.NullInt(RdrList["Orders_InvoiceStatus"]);
                        entity.Orders_Fail_SysUserID = Tools.NullInt(RdrList["Orders_Fail_SysUserID"]);
                        entity.Orders_Fail_Note = Tools.NullStr(RdrList["Orders_Fail_Note"]);
                        entity.Orders_Fail_Addtime = Tools.NullDate(RdrList["Orders_Fail_Addtime"]);
                        entity.Orders_IsReturnCoin = Tools.NullInt(RdrList["Orders_IsReturnCoin"]);
                        entity.Orders_Total_MKTPrice = Tools.NullDbl(RdrList["Orders_Total_MKTPrice"]);
                        entity.Orders_Total_Price = Tools.NullDbl(RdrList["Orders_Total_Price"]);
                        entity.Orders_Total_Freight = Tools.NullDbl(RdrList["Orders_Total_Freight"]);
                        entity.Orders_Total_Coin = Tools.NullInt(RdrList["Orders_Total_Coin"]);
                        entity.Orders_Total_UseCoin = Tools.NullInt(RdrList["Orders_Total_UseCoin"]);
                        entity.Orders_Total_PriceDiscount = Tools.NullDbl(RdrList["Orders_Total_PriceDiscount"]);
                        entity.Orders_Total_FreightDiscount = Tools.NullDbl(RdrList["Orders_Total_FreightDiscount"]);
                        entity.Orders_Total_PriceDiscount_Note = Tools.NullStr(RdrList["Orders_Total_PriceDiscount_Note"]);
                        entity.Orders_Total_FreightDiscount_Note = Tools.NullStr(RdrList["Orders_Total_FreightDiscount_Note"]);
                        entity.Orders_Total_AllPrice = Tools.NullDbl(RdrList["Orders_Total_AllPrice"]);
                        entity.Orders_Address_ID = Tools.NullInt(RdrList["Orders_Address_ID"]);
                        entity.Orders_Address_Country = Tools.NullStr(RdrList["Orders_Address_Country"]);
                        entity.Orders_Address_State = Tools.NullStr(RdrList["Orders_Address_State"]);
                        entity.Orders_Address_City = Tools.NullStr(RdrList["Orders_Address_City"]);
                        entity.Orders_Address_County = Tools.NullStr(RdrList["Orders_Address_County"]);
                        entity.Orders_Address_StreetAddress = Tools.NullStr(RdrList["Orders_Address_StreetAddress"]);
                        entity.Orders_Address_Zip = Tools.NullStr(RdrList["Orders_Address_Zip"]);
                        entity.Orders_Address_Name = Tools.NullStr(RdrList["Orders_Address_Name"]);
                        entity.Orders_Address_Phone_Countrycode = Tools.NullStr(RdrList["Orders_Address_Phone_Countrycode"]);
                        entity.Orders_Address_Phone_Areacode = Tools.NullStr(RdrList["Orders_Address_Phone_Areacode"]);
                        entity.Orders_Address_Phone_Number = Tools.NullStr(RdrList["Orders_Address_Phone_Number"]);
                        entity.Orders_Address_Mobile = Tools.NullStr(RdrList["Orders_Address_Mobile"]);
                        entity.Orders_Delivery_Time_ID = Tools.NullInt(RdrList["Orders_Delivery_Time_ID"]);
                        entity.Orders_Delivery = Tools.NullInt(RdrList["Orders_Delivery"]);
                        entity.Orders_Delivery_Name = Tools.NullStr(RdrList["Orders_Delivery_Name"]);
                        entity.Orders_Payway = Tools.NullInt(RdrList["Orders_Payway"]);
                        entity.Orders_Payway_Name = Tools.NullStr(RdrList["Orders_Payway_Name"]);
                        entity.Orders_PayType = Tools.NullInt(RdrList["Orders_PayType"]);
                        entity.Orders_PayType_Name = Tools.NullStr(RdrList["Orders_PayType_Name"]);
                        entity.Orders_Note = Tools.NullStr(RdrList["Orders_Note"]);
                        entity.Orders_Admin_Note = Tools.NullStr(RdrList["Orders_Admin_Note"]);
                        entity.Orders_Admin_Sign = Tools.NullInt(RdrList["Orders_Admin_Sign"]);
                        entity.Orders_Site = Tools.NullStr(RdrList["Orders_Site"]);
                        entity.Orders_SourceType = Tools.NullInt(RdrList["Orders_SourceType"]);
                        entity.Orders_Source = Tools.NullStr(RdrList["Orders_Source"]);
                        entity.Orders_VerifyCode = Tools.NullStr(RdrList["Orders_VerifyCode"]);
                        entity.U_Orders_IsMonitor = Tools.NullInt(RdrList["U_Orders_IsMonitor"]);
                        entity.Orders_Addtime = Tools.NullDate(RdrList["Orders_Addtime"]);
                        entity.Orders_From = Tools.NullStr(RdrList["Orders_From"]);
                        entity.Orders_Account_Pay = Tools.NullDbl(RdrList["Orders_Account_Pay"]);
                        entity.Orders_IsEvaluate = Tools.NullInt(RdrList["Orders_IsEvaluate"]);
                        entity.Orders_IsSettling = Tools.NullInt(RdrList["Orders_IsSettling"]);
                        entity.Orders_SupplierID = Tools.NullInt(RdrList["Orders_SupplierID"]);
                        entity.Orders_PurchaseID = Tools.NullInt(RdrList["Orders_PurchaseID"]);
                        entity.Orders_PriceReportID = Tools.NullInt(RdrList["Orders_PriceReportID"]);
                        entity.Orders_MemberStatus = Tools.NullInt(RdrList["Orders_MemberStatus"]);
                        entity.Orders_SupplierStatus = Tools.NullInt(RdrList["Orders_SupplierStatus"]);
                        entity.Orders_MemberStatus_Time = Tools.NullDate(RdrList["Orders_MemberStatus_Time"]);
                        entity.Orders_SupplierStatus_Time = Tools.NullDate(RdrList["Orders_SupplierStatus_Time"]);
                        entity.Orders_ContractAdd = Tools.NullStr(RdrList["Orders_ContractAdd"]);
                        entity.Orders_ApplyCreditAmount = Tools.NullDbl(RdrList["Orders_ApplyCreditAmount"]);
                        entity.Orders_AgreementNo = Tools.NullStr(RdrList["Orders_AgreementNo"]);
                        entity.Orders_LoanTermID = Tools.NullInt(RdrList["Orders_LoanTermID"]);
                        entity.Orders_LoanMethodID = Tools.NullInt(RdrList["Orders_LoanMethodID"]);
                        entity.Orders_Fee = Tools.NullDbl(RdrList["Orders_Fee"]);
                        entity.Orders_MarginFee = Tools.NullDbl(RdrList["Orders_MarginFee"]);
                        entity.Orders_FeeRate = Tools.NullDbl(RdrList["Orders_FeeRate"]);
                        entity.Orders_MarginRate = Tools.NullDbl(RdrList["Orders_MarginRate"]);
                        entity.Orders_cashier_url = Tools.NullStr(RdrList["Orders_cashier_url"]);
                        entity.Orders_Loan_proj_no = Tools.NullStr(RdrList["Orders_Loan_proj_no"]);
                        entity.Orders_Responsible = Tools.NullInt(RdrList["Orders_Responsible"]);
                        entity.Orders_IsShow = Tools.NullInt(RdrList["Orders_IsShow"]);
                        //entity.Orders_IsShow = Tools.NullInt(RdrList["Orders_IsShow"]);

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

        public virtual IList<OrdersInfo> GetOrderssByContractID(int ID)
        {
            IList<OrdersInfo> entitys = null;
            OrdersInfo entity = null;
            string SqlList;
            SqlDataReader RdrList = null;
            try
            {
                SqlList = "Select * from Orders where Orders_ContractID=" + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<OrdersInfo>();
                    while (RdrList.Read())
                    {
                        entity = new OrdersInfo();
                        entity.Orders_ID = Tools.NullInt(RdrList["Orders_ID"]);
                        entity.Orders_SN = Tools.NullStr(RdrList["Orders_SN"]);
                        entity.Orders_Type = Tools.NullInt(RdrList["Orders_Type"]);
                        entity.Orders_ContractID = Tools.NullInt(RdrList["Orders_ContractID"]);
                        entity.Orders_BuyerType = Tools.NullInt(RdrList["Orders_BuyerType"]);
                        entity.Orders_BuyerID = Tools.NullInt(RdrList["Orders_BuyerID"]);
                        entity.Orders_SysUserID = Tools.NullInt(RdrList["Orders_SysUserID"]);
                        entity.Orders_Status = Tools.NullInt(RdrList["Orders_Status"]);
                        entity.Orders_ERPSyncStatus = Tools.NullInt(RdrList["Orders_ERPSyncStatus"]);
                        entity.Orders_PaymentStatus = Tools.NullInt(RdrList["Orders_PaymentStatus"]);
                        entity.Orders_PaymentStatus_Time = Tools.NullDate(RdrList["Orders_PaymentStatus_Time"]);
                        entity.Orders_DeliveryStatus = Tools.NullInt(RdrList["Orders_DeliveryStatus"]);
                        entity.Orders_DeliveryStatus_Time = Tools.NullDate(RdrList["Orders_DeliveryStatus_Time"]);
                        entity.Orders_InvoiceStatus = Tools.NullInt(RdrList["Orders_InvoiceStatus"]);
                        entity.Orders_Fail_SysUserID = Tools.NullInt(RdrList["Orders_Fail_SysUserID"]);
                        entity.Orders_Fail_Note = Tools.NullStr(RdrList["Orders_Fail_Note"]);
                        entity.Orders_Fail_Addtime = Tools.NullDate(RdrList["Orders_Fail_Addtime"]);
                        entity.Orders_IsReturnCoin = Tools.NullInt(RdrList["Orders_IsReturnCoin"]);
                        entity.Orders_Total_MKTPrice = Tools.NullDbl(RdrList["Orders_Total_MKTPrice"]);
                        entity.Orders_Total_Price = Tools.NullDbl(RdrList["Orders_Total_Price"]);
                        entity.Orders_Total_Freight = Tools.NullDbl(RdrList["Orders_Total_Freight"]);
                        entity.Orders_Total_Coin = Tools.NullInt(RdrList["Orders_Total_Coin"]);
                        entity.Orders_Total_UseCoin = Tools.NullInt(RdrList["Orders_Total_UseCoin"]);
                        entity.Orders_Total_PriceDiscount = Tools.NullDbl(RdrList["Orders_Total_PriceDiscount"]);
                        entity.Orders_Total_FreightDiscount = Tools.NullDbl(RdrList["Orders_Total_FreightDiscount"]);
                        entity.Orders_Total_PriceDiscount_Note = Tools.NullStr(RdrList["Orders_Total_PriceDiscount_Note"]);
                        entity.Orders_Total_FreightDiscount_Note = Tools.NullStr(RdrList["Orders_Total_FreightDiscount_Note"]);
                        entity.Orders_Total_AllPrice = Tools.NullDbl(RdrList["Orders_Total_AllPrice"]);
                        entity.Orders_Address_ID = Tools.NullInt(RdrList["Orders_Address_ID"]);
                        entity.Orders_Address_Country = Tools.NullStr(RdrList["Orders_Address_Country"]);
                        entity.Orders_Address_State = Tools.NullStr(RdrList["Orders_Address_State"]);
                        entity.Orders_Address_City = Tools.NullStr(RdrList["Orders_Address_City"]);
                        entity.Orders_Address_County = Tools.NullStr(RdrList["Orders_Address_County"]);
                        entity.Orders_Address_StreetAddress = Tools.NullStr(RdrList["Orders_Address_StreetAddress"]);
                        entity.Orders_Address_Zip = Tools.NullStr(RdrList["Orders_Address_Zip"]);
                        entity.Orders_Address_Name = Tools.NullStr(RdrList["Orders_Address_Name"]);
                        entity.Orders_Address_Phone_Countrycode = Tools.NullStr(RdrList["Orders_Address_Phone_Countrycode"]);
                        entity.Orders_Address_Phone_Areacode = Tools.NullStr(RdrList["Orders_Address_Phone_Areacode"]);
                        entity.Orders_Address_Phone_Number = Tools.NullStr(RdrList["Orders_Address_Phone_Number"]);
                        entity.Orders_Address_Mobile = Tools.NullStr(RdrList["Orders_Address_Mobile"]);
                        entity.Orders_Delivery_Time_ID = Tools.NullInt(RdrList["Orders_Delivery_Time_ID"]);
                        entity.Orders_Delivery = Tools.NullInt(RdrList["Orders_Delivery"]);
                        entity.Orders_Delivery_Name = Tools.NullStr(RdrList["Orders_Delivery_Name"]);
                        entity.Orders_Payway = Tools.NullInt(RdrList["Orders_Payway"]);
                        entity.Orders_Payway_Name = Tools.NullStr(RdrList["Orders_Payway_Name"]);
                        entity.Orders_PayType = Tools.NullInt(RdrList["Orders_PayType"]);
                        entity.Orders_PayType_Name = Tools.NullStr(RdrList["Orders_PayType_Name"]);
                        entity.Orders_Note = Tools.NullStr(RdrList["Orders_Note"]);
                        entity.Orders_Admin_Note = Tools.NullStr(RdrList["Orders_Admin_Note"]);
                        entity.Orders_Admin_Sign = Tools.NullInt(RdrList["Orders_Admin_Sign"]);
                        entity.Orders_Site = Tools.NullStr(RdrList["Orders_Site"]);
                        entity.Orders_SourceType = Tools.NullInt(RdrList["Orders_SourceType"]);
                        entity.Orders_Source = Tools.NullStr(RdrList["Orders_Source"]);
                        entity.Orders_VerifyCode = Tools.NullStr(RdrList["Orders_VerifyCode"]);
                        entity.U_Orders_IsMonitor = Tools.NullInt(RdrList["U_Orders_IsMonitor"]);
                        entity.Orders_Addtime = Tools.NullDate(RdrList["Orders_Addtime"]);
                        entity.Orders_From = Tools.NullStr(RdrList["Orders_From"]);
                        entity.Orders_Account_Pay = Tools.NullDbl(RdrList["Orders_Account_Pay"]);
                        entity.Orders_IsEvaluate = Tools.NullInt(RdrList["Orders_IsEvaluate"]);
                        entity.Orders_IsSettling = Tools.NullInt(RdrList["Orders_IsSettling"]);
                        entity.Orders_SupplierID = Tools.NullInt(RdrList["Orders_SupplierID"]);
                        entity.Orders_PurchaseID = Tools.NullInt(RdrList["Orders_PurchaseID"]);
                        entity.Orders_PriceReportID = Tools.NullInt(RdrList["Orders_PriceReportID"]);
                        entity.Orders_MemberStatus = Tools.NullInt(RdrList["Orders_MemberStatus"]);
                        entity.Orders_SupplierStatus = Tools.NullInt(RdrList["Orders_SupplierStatus"]);
                        entity.Orders_MemberStatus_Time = Tools.NullDate(RdrList["Orders_MemberStatus_Time"]);
                        entity.Orders_SupplierStatus_Time = Tools.NullDate(RdrList["Orders_SupplierStatus_Time"]);
                        entity.Orders_ContractAdd = Tools.NullStr(RdrList["Orders_ContractAdd"]);
                        entity.Orders_ApplyCreditAmount = Tools.NullDbl(RdrList["Orders_ApplyCreditAmount"]);
                        entity.Orders_AgreementNo = Tools.NullStr(RdrList["Orders_AgreementNo"]);
                        entity.Orders_LoanTermID = Tools.NullInt(RdrList["Orders_LoanTermID"]);
                        entity.Orders_LoanMethodID = Tools.NullInt(RdrList["Orders_LoanMethodID"]);
                        entity.Orders_Fee = Tools.NullDbl(RdrList["Orders_Fee"]);
                        entity.Orders_MarginFee = Tools.NullDbl(RdrList["Orders_MarginFee"]);
                        entity.Orders_FeeRate = Tools.NullDbl(RdrList["Orders_FeeRate"]);
                        entity.Orders_MarginRate = Tools.NullDbl(RdrList["Orders_MarginRate"]);
                        entity.Orders_cashier_url = Tools.NullStr(RdrList["Orders_cashier_url"]);
                        entity.Orders_Loan_proj_no = Tools.NullStr(RdrList["Orders_Loan_proj_no"]);
                        entity.Orders_Responsible = Tools.NullInt(RdrList["Orders_Responsible"]);
                        entity.Orders_IsShow = Tools.NullInt(RdrList["Orders_IsShow"]);

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

        public virtual IList<OrdersInfo> GetOrderssByMemberID(int Member_ID)
        {
            IList<OrdersInfo> entitys = null;
            OrdersInfo entity = null;
            string SqlList;
            SqlDataReader RdrList = null; 
            try
            {
                SqlList = "Select * from Orders where Orders_BuyerID=" + Member_ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<OrdersInfo>();
                    while (RdrList.Read())
                    {
                        entity = new OrdersInfo();
                        entity.Orders_ID = Tools.NullInt(RdrList["Orders_ID"]);
                        entity.Orders_SN = Tools.NullStr(RdrList["Orders_SN"]);
                        entity.Orders_Type = Tools.NullInt(RdrList["Orders_Type"]);
                        entity.Orders_ContractID = Tools.NullInt(RdrList["Orders_ContractID"]);
                        entity.Orders_BuyerType = Tools.NullInt(RdrList["Orders_BuyerType"]);
                        entity.Orders_BuyerID = Tools.NullInt(RdrList["Orders_BuyerID"]);
                        entity.Orders_SysUserID = Tools.NullInt(RdrList["Orders_SysUserID"]);
                        entity.Orders_Status = Tools.NullInt(RdrList["Orders_Status"]);
                        entity.Orders_ERPSyncStatus = Tools.NullInt(RdrList["Orders_ERPSyncStatus"]);
                        entity.Orders_PaymentStatus = Tools.NullInt(RdrList["Orders_PaymentStatus"]);
                        entity.Orders_PaymentStatus_Time = Tools.NullDate(RdrList["Orders_PaymentStatus_Time"]);
                        entity.Orders_DeliveryStatus = Tools.NullInt(RdrList["Orders_DeliveryStatus"]);
                        entity.Orders_DeliveryStatus_Time = Tools.NullDate(RdrList["Orders_DeliveryStatus_Time"]);
                        entity.Orders_InvoiceStatus = Tools.NullInt(RdrList["Orders_InvoiceStatus"]);
                        entity.Orders_Fail_SysUserID = Tools.NullInt(RdrList["Orders_Fail_SysUserID"]);
                        entity.Orders_Fail_Note = Tools.NullStr(RdrList["Orders_Fail_Note"]);
                        entity.Orders_Fail_Addtime = Tools.NullDate(RdrList["Orders_Fail_Addtime"]);
                        entity.Orders_IsReturnCoin = Tools.NullInt(RdrList["Orders_IsReturnCoin"]);
                        entity.Orders_Total_MKTPrice = Tools.NullDbl(RdrList["Orders_Total_MKTPrice"]);
                        entity.Orders_Total_Price = Tools.NullDbl(RdrList["Orders_Total_Price"]);
                        entity.Orders_Total_Freight = Tools.NullDbl(RdrList["Orders_Total_Freight"]);
                        entity.Orders_Total_Coin = Tools.NullInt(RdrList["Orders_Total_Coin"]);
                        entity.Orders_Total_UseCoin = Tools.NullInt(RdrList["Orders_Total_UseCoin"]);
                        entity.Orders_Total_PriceDiscount = Tools.NullDbl(RdrList["Orders_Total_PriceDiscount"]);
                        entity.Orders_Total_FreightDiscount = Tools.NullDbl(RdrList["Orders_Total_FreightDiscount"]);
                        entity.Orders_Total_PriceDiscount_Note = Tools.NullStr(RdrList["Orders_Total_PriceDiscount_Note"]);
                        entity.Orders_Total_FreightDiscount_Note = Tools.NullStr(RdrList["Orders_Total_FreightDiscount_Note"]);
                        entity.Orders_Total_AllPrice = Tools.NullDbl(RdrList["Orders_Total_AllPrice"]);
                        entity.Orders_Address_ID = Tools.NullInt(RdrList["Orders_Address_ID"]);
                        entity.Orders_Address_Country = Tools.NullStr(RdrList["Orders_Address_Country"]);
                        entity.Orders_Address_State = Tools.NullStr(RdrList["Orders_Address_State"]);
                        entity.Orders_Address_City = Tools.NullStr(RdrList["Orders_Address_City"]);
                        entity.Orders_Address_County = Tools.NullStr(RdrList["Orders_Address_County"]);
                        entity.Orders_Address_StreetAddress = Tools.NullStr(RdrList["Orders_Address_StreetAddress"]);
                        entity.Orders_Address_Zip = Tools.NullStr(RdrList["Orders_Address_Zip"]);
                        entity.Orders_Address_Name = Tools.NullStr(RdrList["Orders_Address_Name"]);
                        entity.Orders_Address_Phone_Countrycode = Tools.NullStr(RdrList["Orders_Address_Phone_Countrycode"]);
                        entity.Orders_Address_Phone_Areacode = Tools.NullStr(RdrList["Orders_Address_Phone_Areacode"]);
                        entity.Orders_Address_Phone_Number = Tools.NullStr(RdrList["Orders_Address_Phone_Number"]);
                        entity.Orders_Address_Mobile = Tools.NullStr(RdrList["Orders_Address_Mobile"]);
                        entity.Orders_Delivery_Time_ID = Tools.NullInt(RdrList["Orders_Delivery_Time_ID"]);
                        entity.Orders_Delivery = Tools.NullInt(RdrList["Orders_Delivery"]);
                        entity.Orders_Delivery_Name = Tools.NullStr(RdrList["Orders_Delivery_Name"]);
                        entity.Orders_Payway = Tools.NullInt(RdrList["Orders_Payway"]);
                        entity.Orders_Payway_Name = Tools.NullStr(RdrList["Orders_Payway_Name"]);
                        entity.Orders_PayType = Tools.NullInt(RdrList["Orders_PayType"]);
                        entity.Orders_PayType_Name = Tools.NullStr(RdrList["Orders_PayType_Name"]);
                        entity.Orders_Note = Tools.NullStr(RdrList["Orders_Note"]);
                        entity.Orders_Admin_Note = Tools.NullStr(RdrList["Orders_Admin_Note"]);
                        entity.Orders_Admin_Sign = Tools.NullInt(RdrList["Orders_Admin_Sign"]);
                        entity.Orders_Site = Tools.NullStr(RdrList["Orders_Site"]);
                        entity.Orders_SourceType = Tools.NullInt(RdrList["Orders_SourceType"]);
                        entity.Orders_Source = Tools.NullStr(RdrList["Orders_Source"]);
                        entity.Orders_VerifyCode = Tools.NullStr(RdrList["Orders_VerifyCode"]);
                        entity.U_Orders_IsMonitor = Tools.NullInt(RdrList["U_Orders_IsMonitor"]);
                        entity.Orders_Addtime = Tools.NullDate(RdrList["Orders_Addtime"]);
                        entity.Orders_From = Tools.NullStr(RdrList["Orders_From"]);
                        entity.Orders_Account_Pay = Tools.NullDbl(RdrList["Orders_Account_Pay"]);
                        entity.Orders_IsEvaluate = Tools.NullInt(RdrList["Orders_IsEvaluate"]);
                        entity.Orders_IsSettling = Tools.NullInt(RdrList["Orders_IsSettling"]);
                        entity.Orders_SupplierID = Tools.NullInt(RdrList["Orders_SupplierID"]);
                        entity.Orders_PurchaseID = Tools.NullInt(RdrList["Orders_PurchaseID"]);
                        entity.Orders_PriceReportID = Tools.NullInt(RdrList["Orders_PriceReportID"]);
                        entity.Orders_MemberStatus = Tools.NullInt(RdrList["Orders_MemberStatus"]);
                        entity.Orders_SupplierStatus = Tools.NullInt(RdrList["Orders_SupplierStatus"]);
                        entity.Orders_MemberStatus_Time = Tools.NullDate(RdrList["Orders_MemberStatus_Time"]);
                        entity.Orders_SupplierStatus_Time = Tools.NullDate(RdrList["Orders_SupplierStatus_Time"]);
                        entity.Orders_ContractAdd = Tools.NullStr(RdrList["Orders_ContractAdd"]);
                        entity.Orders_ApplyCreditAmount = Tools.NullDbl(RdrList["Orders_ApplyCreditAmount"]);
                        entity.Orders_AgreementNo = Tools.NullStr(RdrList["Orders_AgreementNo"]);
                        entity.Orders_LoanTermID = Tools.NullInt(RdrList["Orders_LoanTermID"]);
                        entity.Orders_LoanMethodID = Tools.NullInt(RdrList["Orders_LoanMethodID"]);
                        entity.Orders_Fee = Tools.NullDbl(RdrList["Orders_Fee"]);
                        entity.Orders_MarginFee = Tools.NullDbl(RdrList["Orders_MarginFee"]);
                        entity.Orders_FeeRate = Tools.NullDbl(RdrList["Orders_FeeRate"]);
                        entity.Orders_MarginRate = Tools.NullDbl(RdrList["Orders_MarginRate"]);
                        entity.Orders_cashier_url = Tools.NullStr(RdrList["Orders_cashier_url"]);
                        entity.Orders_Loan_proj_no = Tools.NullStr(RdrList["Orders_Loan_proj_no"]);
                        entity.Orders_Responsible = Tools.NullInt(RdrList["Orders_Responsible"]);
                        entity.Orders_IsShow = Tools.NullInt(RdrList["Orders_IsShow"]);

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
                SqlTable = "Orders";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Orders_ID) FROM " + SqlTable + SqlParam;

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

        public virtual bool AddOrdersGoods(OrdersGoodsInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Orders_Goods";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();
            DrAdd["Orders_Goods_ID"] = entity.Orders_Goods_ID;
            DrAdd["Orders_Goods_Type"] = entity.Orders_Goods_Type;
            DrAdd["Orders_Goods_ParentID"] = entity.Orders_Goods_ParentID;
            DrAdd["Orders_Goods_OrdersID"] = entity.Orders_Goods_OrdersID;
            DrAdd["Orders_Goods_Product_ID"] = entity.Orders_Goods_Product_ID;
            DrAdd["Orders_Goods_Product_SupplierID"] = entity.Orders_Goods_Product_SupplierID;
            DrAdd["Orders_Goods_Product_Code"] = entity.Orders_Goods_Product_Code;
            DrAdd["Orders_Goods_Product_CateID"] = entity.Orders_Goods_Product_CateID;
            DrAdd["Orders_Goods_Product_BrandID"] = entity.Orders_Goods_Product_BrandID;
            DrAdd["Orders_Goods_Product_Name"] = entity.Orders_Goods_Product_Name;
            DrAdd["Orders_Goods_Product_Img"] = entity.Orders_Goods_Product_Img;
            DrAdd["Orders_Goods_Product_Price"] = entity.Orders_Goods_Product_Price;
            DrAdd["Orders_Goods_Product_MKTPrice"] = entity.Orders_Goods_Product_MKTPrice;
            DrAdd["Orders_Goods_Product_Maker"] = entity.Orders_Goods_Product_Maker;
            DrAdd["Orders_Goods_Product_Spec"] = entity.Orders_Goods_Product_Spec;
            DrAdd["Orders_Goods_Product_DeliveryDate"] = entity.Orders_Goods_Product_DeliveryDate;
            DrAdd["Orders_Goods_Product_AuthorizeCode"] = entity.Orders_Goods_Product_AuthorizeCode;
            DrAdd["U_Orders_Goods_Product_BatchCode"] = "";
            DrAdd["U_Orders_Goods_Product_BuyChannel"] = "";
            DrAdd["U_Orders_Goods_Product_BuyAmount"] = 0;
            DrAdd["U_Orders_Goods_Product_BuyPrice"] = 0;
            DrAdd["Orders_Goods_Product_brokerage"] = entity.Orders_Goods_Product_brokerage;
            DrAdd["Orders_Goods_Product_SalePrice"] = entity.Orders_Goods_Product_SalePrice;
            DrAdd["Orders_Goods_Product_PurchasingPrice"] = entity.Orders_Goods_Product_PurchasingPrice;
            DrAdd["Orders_Goods_Product_Coin"] = entity.Orders_Goods_Product_Coin;
            DrAdd["Orders_Goods_Product_IsFavor"] = entity.Orders_Goods_Product_IsFavor;
            DrAdd["Orders_Goods_Product_UseCoin"] = entity.Orders_Goods_Product_UseCoin;
            DrAdd["Orders_Goods_Amount"] = entity.Orders_Goods_Amount;
            DtAdd.Rows.Add(DrAdd);
            try {
                DBHelper.SaveChanges(SqlAdd, DtAdd);
                return true;
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                DtAdd.Dispose();
            }
        }

        public virtual bool EditOrdersGoods(OrdersGoodsInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Orders_Goods WHERE Orders_Goods_ID = " + entity.Orders_Goods_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try {
                if (DtAdd.Rows.Count > 0) {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Orders_Goods_ID"] = entity.Orders_Goods_ID;
                    DrAdd["Orders_Goods_Type"] = entity.Orders_Goods_Type;
                    DrAdd["Orders_Goods_ParentID"] = entity.Orders_Goods_ParentID;
                    DrAdd["Orders_Goods_OrdersID"] = entity.Orders_Goods_OrdersID;
                    DrAdd["Orders_Goods_Product_ID"] = entity.Orders_Goods_Product_ID;
                    DrAdd["Orders_Goods_Product_SupplierID"] = entity.Orders_Goods_Product_SupplierID;
                    DrAdd["Orders_Goods_Product_Code"] = entity.Orders_Goods_Product_Code;
                    DrAdd["Orders_Goods_Product_CateID"] = entity.Orders_Goods_Product_CateID;
                    DrAdd["Orders_Goods_Product_BrandID"] = entity.Orders_Goods_Product_BrandID;
                    DrAdd["Orders_Goods_Product_Name"] = entity.Orders_Goods_Product_Name;
                    DrAdd["Orders_Goods_Product_Img"] = entity.Orders_Goods_Product_Img;
                    DrAdd["Orders_Goods_Product_Price"] = entity.Orders_Goods_Product_Price;
                    DrAdd["Orders_Goods_Product_MKTPrice"] = entity.Orders_Goods_Product_MKTPrice;
                    DrAdd["Orders_Goods_Product_Maker"] = entity.Orders_Goods_Product_Maker;
                    DrAdd["Orders_Goods_Product_Spec"] = entity.Orders_Goods_Product_Spec;
                    DrAdd["Orders_Goods_Product_DeliveryDate"] = entity.Orders_Goods_Product_DeliveryDate;
                    DrAdd["Orders_Goods_Product_AuthorizeCode"] = entity.Orders_Goods_Product_AuthorizeCode;
                    DrAdd["U_Orders_Goods_Product_BatchCode"] = entity.U_Orders_Goods_Product_BatchCode;
                    DrAdd["U_Orders_Goods_Product_BuyChannel"] = entity.U_Orders_Goods_Product_BuyChannel;
                    DrAdd["U_Orders_Goods_Product_BuyAmount"] = entity.U_Orders_Goods_Product_BuyAmount;
                    DrAdd["U_Orders_Goods_Product_BuyPrice"] = entity.U_Orders_Goods_Product_BuyPrice;
                    DrAdd["Orders_Goods_Product_brokerage"] = entity.Orders_Goods_Product_brokerage;
                    DrAdd["Orders_Goods_Product_SalePrice"] = entity.Orders_Goods_Product_SalePrice;
                    DrAdd["Orders_Goods_Product_PurchasingPrice"] = entity.Orders_Goods_Product_PurchasingPrice;
                    DrAdd["Orders_Goods_Product_Coin"] = entity.Orders_Goods_Product_Coin;
                    DrAdd["Orders_Goods_Product_IsFavor"] = entity.Orders_Goods_Product_IsFavor;
                    DrAdd["Orders_Goods_Product_UseCoin"] = entity.Orders_Goods_Product_UseCoin;
                    DrAdd["Orders_Goods_Amount"] = entity.Orders_Goods_Amount;
                    DBHelper.SaveChanges(SqlAdd, DtAdd);
                }
                else {
                    return false;
                }
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                DtAdd.Dispose();
            }
            return true;
        }

        public virtual int DelOrdersGoods(int ID)
        {
            string SqlAdd = "DELETE FROM Orders_Goods WHERE Orders_Goods_ID = " + ID;
            try {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public virtual bool AddOrdersCoupon(int Orders_ID,int Coupon_ID)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Orders_Coupon";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();
            DrAdd["Orders_Coupon_OrdersID"] = Orders_ID;
            DrAdd["Orders_Coupon_CouponID"] = Coupon_ID;
           
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

        public virtual string GetOrdersCoupons(int Orders_ID)
        {
            string Coupon_ID="";
            SqlDataReader RdrList = null;
            string SqlAdd = "SELECT * FROM Orders_Coupon Where Orders_Coupon_OrdersID=" + Orders_ID.ToString();
            try
            {
                RdrList = DBHelper.ExecuteReader(SqlAdd);
                if (RdrList.HasRows)
                {
                    while (RdrList.Read())
                    {
                        if (Coupon_ID == "")
                        {
                            Coupon_ID = Tools.NullInt(RdrList["Orders_Coupon_CouponID"]).ToString();
                        }
                        else
                        {
                            Coupon_ID =Coupon_ID + "," + Tools.NullInt(RdrList["Orders_Coupon_CouponID"]).ToString();
                        }
                    }
                }
                return Coupon_ID;
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

        public virtual bool AddOrdersFavorPolicy(int Orders_ID, int Policy_ID)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Orders_FavorPolicy";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();
            DrAdd["Orders_FavorPolicy_OrdersID"] = Orders_ID;
            DrAdd["Orders_FavorPolicy_PolicyID"] = Policy_ID;

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

        public virtual string GetOrdersPolicys(int Orders_ID)
        {
            string Policy_ID = "";
            SqlDataReader RdrList = null;
            string SqlAdd = "SELECT * FROM Orders_FavorPolicy Where Orders_FavorPolicy_OrdersID=" + Orders_ID.ToString();
            try
            {
                RdrList = DBHelper.ExecuteReader(SqlAdd);
                if (RdrList.HasRows)
                {
                    while (RdrList.Read())
                    {
                        if (Policy_ID == "")
                        {
                            Policy_ID = Tools.NullInt(RdrList["Orders_FavorPolicy_PolicyID"]).ToString();
                        }
                        else
                        {
                            Policy_ID = Policy_ID + "," + Tools.NullInt(RdrList["Orders_Coupon_CouponID"]).ToString();
                        }
                    }
                }
                return Policy_ID;
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

        public virtual int DelOrdersGoodsByOrdersID(int ID)
        {
            string SqlAdd = "DELETE FROM Orders_Goods WHERE Orders_Goods_OrdersID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual int Get_Max_Goods_ID()
        {
            string SqlAdd = "select Max(Orders_Goods_ID) FROM Orders_Goods";
            try
            {
                return Tools.NullInt(DBHelper.ExecuteScalar(SqlAdd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual OrdersGoodsInfo GetOrdersGoodsByID(int ID)
        {
            OrdersGoodsInfo entity = null;
            SqlDataReader RdrList = null;
            try {
                string SqlList;
                SqlList = "SELECT * FROM Orders_Goods WHERE Orders_Goods_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read()) {
                    entity = new OrdersGoodsInfo();
                    entity.Orders_Goods_ID = Tools.NullInt(RdrList["Orders_Goods_ID"]);
                    entity.Orders_Goods_Type = Tools.NullInt(RdrList["Orders_Goods_Type"]);
                    entity.Orders_Goods_ParentID = Tools.NullInt(RdrList["Orders_Goods_ParentID"]);
                    entity.Orders_Goods_OrdersID = Tools.NullInt(RdrList["Orders_Goods_OrdersID"]);
                    entity.Orders_Goods_Product_ID = Tools.NullInt(RdrList["Orders_Goods_Product_ID"]);
                    entity.Orders_Goods_Product_SupplierID = Tools.NullInt(RdrList["Orders_Goods_Product_SupplierID"]);
                    entity.Orders_Goods_Product_Code = Tools.NullStr(RdrList["Orders_Goods_Product_Code"]);
                    entity.Orders_Goods_Product_CateID = Tools.NullInt(RdrList["Orders_Goods_Product_CateID"]);
                    entity.Orders_Goods_Product_BrandID = Tools.NullInt(RdrList["Orders_Goods_Product_BrandID"]);
                    entity.Orders_Goods_Product_Name = Tools.NullStr(RdrList["Orders_Goods_Product_Name"]);
                    entity.Orders_Goods_Product_Img = Tools.NullStr(RdrList["Orders_Goods_Product_Img"]);
                    entity.Orders_Goods_Product_Price = Tools.NullDbl(RdrList["Orders_Goods_Product_Price"]);
                    entity.Orders_Goods_Product_MKTPrice = Tools.NullDbl(RdrList["Orders_Goods_Product_MKTPrice"]);
                    entity.Orders_Goods_Product_Maker = Tools.NullStr(RdrList["Orders_Goods_Product_Maker"]);
                    entity.Orders_Goods_Product_Spec = Tools.NullStr(RdrList["Orders_Goods_Product_Spec"]);
                    entity.Orders_Goods_Product_DeliveryDate = Tools.NullStr(RdrList["Orders_Goods_Product_DeliveryDate"]);
                    entity.Orders_Goods_Product_AuthorizeCode = Tools.NullStr(RdrList["Orders_Goods_Product_AuthorizeCode"]);
                    entity.U_Orders_Goods_Product_BatchCode = Tools.NullStr(RdrList["U_Orders_Goods_Product_BatchCode"]);
                    entity.U_Orders_Goods_Product_BuyChannel = Tools.NullStr(RdrList["U_Orders_Goods_Product_BuyChannel"]);
                    entity.U_Orders_Goods_Product_BuyAmount = Tools.NullInt(RdrList["U_Orders_Goods_Product_BuyAmount"]);
                    entity.U_Orders_Goods_Product_BuyPrice = Tools.NullDbl(RdrList["U_Orders_Goods_Product_BuyPrice"]);
                    entity.Orders_Goods_Product_brokerage = Tools.NullDbl(RdrList["Orders_Goods_Product_brokerage"]);
                    entity.Orders_Goods_Product_SalePrice = Tools.NullDbl(RdrList["Orders_Goods_Product_SalePrice"]);
                    entity.Orders_Goods_Product_PurchasingPrice = Tools.NullDbl(RdrList["Orders_Goods_Product_PurchasingPrice"]);
                    entity.Orders_Goods_Product_Coin = Tools.NullInt(RdrList["Orders_Goods_Product_Coin"]);
                    entity.Orders_Goods_Product_IsFavor = Tools.NullInt(RdrList["Orders_Goods_Product_IsFavor"]);
                    entity.Orders_Goods_Product_UseCoin = Tools.NullInt(RdrList["Orders_Goods_Product_UseCoin"]);
                    entity.Orders_Goods_Amount = Tools.NullInt(RdrList["Orders_Goods_Amount"]);
                }
                return entity;
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                if (RdrList != null) {
                    RdrList.Close();
                    RdrList = null;
                }
            }
        }

        public virtual IList<OrdersGoodsInfo> GetGoodsListByOrderID(int ID)
        {
            IList<OrdersGoodsInfo> entityList = null;
            OrdersGoodsInfo entity = null;
            string SqlList = "SELECT * FROM Orders_Goods WHERE Orders_Goods_OrdersID = " + ID;
            SqlDataReader RdrList = null;
            try
            {
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows) {
                    entityList = new List<OrdersGoodsInfo>();
                    while (RdrList.Read()) {
                        entity = new OrdersGoodsInfo();
                        entity.Orders_Goods_ID = Tools.NullInt(RdrList["Orders_Goods_ID"]);
                        entity.Orders_Goods_Type = Tools.NullInt(RdrList["Orders_Goods_Type"]);
                        entity.Orders_Goods_ParentID = Tools.NullInt(RdrList["Orders_Goods_ParentID"]);
                        entity.Orders_Goods_OrdersID = Tools.NullInt(RdrList["Orders_Goods_OrdersID"]);
                        entity.Orders_Goods_Product_ID = Tools.NullInt(RdrList["Orders_Goods_Product_ID"]);
                        entity.Orders_Goods_Product_SupplierID = Tools.NullInt(RdrList["Orders_Goods_Product_SupplierID"]);
                        entity.Orders_Goods_Product_Code = Tools.NullStr(RdrList["Orders_Goods_Product_Code"]);
                        entity.Orders_Goods_Product_CateID = Tools.NullInt(RdrList["Orders_Goods_Product_CateID"]);
                        entity.Orders_Goods_Product_BrandID = Tools.NullInt(RdrList["Orders_Goods_Product_BrandID"]);
                        entity.Orders_Goods_Product_Name = Tools.NullStr(RdrList["Orders_Goods_Product_Name"]);
                        entity.Orders_Goods_Product_Img = Tools.NullStr(RdrList["Orders_Goods_Product_Img"]);
                        entity.Orders_Goods_Product_Price = Tools.NullDbl(RdrList["Orders_Goods_Product_Price"]);
                        entity.Orders_Goods_Product_MKTPrice = Tools.NullDbl(RdrList["Orders_Goods_Product_MKTPrice"]);
                        entity.Orders_Goods_Product_Maker = Tools.NullStr(RdrList["Orders_Goods_Product_Maker"]);
                        entity.Orders_Goods_Product_Spec = Tools.NullStr(RdrList["Orders_Goods_Product_Spec"]);
                        entity.Orders_Goods_Product_DeliveryDate = Tools.NullStr(RdrList["Orders_Goods_Product_DeliveryDate"]);
                        entity.Orders_Goods_Product_AuthorizeCode = Tools.NullStr(RdrList["Orders_Goods_Product_AuthorizeCode"]);
                        entity.U_Orders_Goods_Product_BatchCode = Tools.NullStr(RdrList["U_Orders_Goods_Product_BatchCode"]);
                        entity.U_Orders_Goods_Product_BuyChannel = Tools.NullStr(RdrList["U_Orders_Goods_Product_BuyChannel"]);
                        entity.U_Orders_Goods_Product_BuyAmount = Tools.NullInt(RdrList["U_Orders_Goods_Product_BuyAmount"]);
                        entity.U_Orders_Goods_Product_BuyPrice = Tools.NullDbl(RdrList["U_Orders_Goods_Product_BuyPrice"]);
                        entity.Orders_Goods_Product_brokerage = Tools.NullDbl(RdrList["Orders_Goods_Product_brokerage"]);
                        entity.Orders_Goods_Product_SalePrice = Tools.NullDbl(RdrList["Orders_Goods_Product_SalePrice"]);
                        entity.Orders_Goods_Product_PurchasingPrice = Tools.NullDbl(RdrList["Orders_Goods_Product_PurchasingPrice"]);
                        entity.Orders_Goods_Product_Coin = Tools.NullInt(RdrList["Orders_Goods_Product_Coin"]);
                        entity.Orders_Goods_Product_IsFavor = Tools.NullInt(RdrList["Orders_Goods_Product_IsFavor"]);
                        entity.Orders_Goods_Product_UseCoin = Tools.NullInt(RdrList["Orders_Goods_Product_UseCoin"]);
                        entity.Orders_Goods_Amount = Tools.NullDbl(RdrList["Orders_Goods_Amount"]);
                        entityList.Add(entity);
                        entity = null;
                    }
                }
                return entityList;
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                RdrList.Close();
                RdrList = null;
            }
        }

        public virtual string GetSupplierOrdersID(int Supplier_ID)
        {
            string orders_ID = "0";
            string SqlList = "SELECT distinct Orders_Goods_OrdersID FROM Orders_Goods WHERE Orders_Goods_Product_SupplierID = " + Supplier_ID;
            SqlDataReader RdrList = null;
            try
            {
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    while (RdrList.Read())
                    {
                        orders_ID = orders_ID + "," + Tools.NullInt(RdrList["Orders_Goods_OrdersID"]).ToString();
                    }
                }
                return orders_ID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                RdrList.Close();
                RdrList = null;
            }
        }

        public virtual IList<OrdersGoodsInfo> GetOrdersGoodsList(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<OrdersGoodsInfo> entitys = null;
            OrdersGoodsInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Orders_Goods";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<OrdersGoodsInfo>();
                    while (RdrList.Read())
                    {
                        entity = new OrdersGoodsInfo();

                        entity.Orders_Goods_ID = Tools.NullInt(RdrList["Orders_Goods_ID"]);
                        entity.Orders_Goods_Type = Tools.NullInt(RdrList["Orders_Goods_Type"]);
                        entity.Orders_Goods_ParentID = Tools.NullInt(RdrList["Orders_Goods_ParentID"]);
                        entity.Orders_Goods_OrdersID = Tools.NullInt(RdrList["Orders_Goods_OrdersID"]);
                        entity.Orders_Goods_Product_ID = Tools.NullInt(RdrList["Orders_Goods_Product_ID"]);
                        entity.Orders_Goods_Product_SupplierID = Tools.NullInt(RdrList["Orders_Goods_Product_SupplierID"]);
                        entity.Orders_Goods_Product_Code = Tools.NullStr(RdrList["Orders_Goods_Product_Code"]);
                        entity.Orders_Goods_Product_CateID = Tools.NullInt(RdrList["Orders_Goods_Product_CateID"]);
                        entity.Orders_Goods_Product_BrandID = Tools.NullInt(RdrList["Orders_Goods_Product_BrandID"]);
                        entity.Orders_Goods_Product_Name = Tools.NullStr(RdrList["Orders_Goods_Product_Name"]);
                        entity.Orders_Goods_Product_Img = Tools.NullStr(RdrList["Orders_Goods_Product_Img"]);
                        entity.Orders_Goods_Product_Price = Tools.NullDbl(RdrList["Orders_Goods_Product_Price"]);
                        entity.Orders_Goods_Product_MKTPrice = Tools.NullDbl(RdrList["Orders_Goods_Product_MKTPrice"]);
                        entity.Orders_Goods_Product_Maker = Tools.NullStr(RdrList["Orders_Goods_Product_Maker"]);
                        entity.Orders_Goods_Product_Spec = Tools.NullStr(RdrList["Orders_Goods_Product_Spec"]);
                        entity.Orders_Goods_Product_DeliveryDate = Tools.NullStr(RdrList["Orders_Goods_Product_DeliveryDate"]);
                        entity.Orders_Goods_Product_AuthorizeCode = Tools.NullStr(RdrList["Orders_Goods_Product_AuthorizeCode"]);
                        entity.U_Orders_Goods_Product_BatchCode = Tools.NullStr(RdrList["U_Orders_Goods_Product_BatchCode"]);
                        entity.U_Orders_Goods_Product_BuyChannel = Tools.NullStr(RdrList["U_Orders_Goods_Product_BuyChannel"]);
                        entity.U_Orders_Goods_Product_BuyAmount = Tools.NullInt(RdrList["U_Orders_Goods_Product_BuyAmount"]);
                        entity.U_Orders_Goods_Product_BuyPrice = Tools.NullDbl(RdrList["U_Orders_Goods_Product_BuyPrice"]);
                        entity.Orders_Goods_Product_brokerage = Tools.NullDbl(RdrList["Orders_Goods_Product_brokerage"]);
                        entity.Orders_Goods_Product_SalePrice = Tools.NullDbl(RdrList["Orders_Goods_Product_SalePrice"]);
                        entity.Orders_Goods_Product_PurchasingPrice = Tools.NullDbl(RdrList["Orders_Goods_Product_PurchasingPrice"]);
                        entity.Orders_Goods_Product_Coin = Tools.NullInt(RdrList["Orders_Goods_Product_Coin"]);
                        entity.Orders_Goods_Product_IsFavor = Tools.NullInt(RdrList["Orders_Goods_Product_IsFavor"]);
                        entity.Orders_Goods_Product_UseCoin = Tools.NullInt(RdrList["Orders_Goods_Product_UseCoin"]);
                        entity.Orders_Goods_Amount = Tools.NullInt(RdrList["Orders_Goods_Amount"]);

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

        public virtual PageInfo GetGoodsPageInfo(QueryInfo Query)
        {
            int RecordCount, PageCount, CurrentPage;
            string SqlCount, SqlParam, SqlTable;
            PageInfo Page;

            try
            {
                Page = new PageInfo();
                SqlTable = "Orders_Goods";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Orders_Goods_ID) FROM " + SqlTable + SqlParam;

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


        public virtual int GetSupplierOrdersNum(int supplier_id)
        {
         
            SqlDataReader RdrList = null;
            int i = 0;
            string SqlAdd = "select distinct Orders_ID from Orders o  join Orders_Goods og on o.Orders_ID  = og.Orders_Goods_OrdersID where o.Orders_Status=2  and  og.Orders_Goods_Product_SupplierID=" + supplier_id.ToString();
            try
            {
                RdrList = DBHelper.ExecuteReader(SqlAdd);
                if (RdrList.HasRows)
                {
                    while (RdrList.Read())
                    {
                        i++;
                    }
                }
                return i;
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

    public class OrdersContract : IOrdersContract
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public OrdersContract()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddOrdersContract(OrdersContractInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Orders_Contract";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["ID"] = entity.ID;
            DrAdd["SN"] = entity.SN;
            DrAdd["Name"] = entity.Name;
            DrAdd["Orders_ID"] = entity.Orders_ID;
            DrAdd["Path"] = entity.Path;
            DrAdd["Addtime"] = entity.Addtime;
            DrAdd["Site"] = entity.Site;

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

        public virtual bool EditOrdersContract(OrdersContractInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Orders_Contract WHERE ID = " + entity.ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["ID"] = entity.ID;
                    DrAdd["SN"] = entity.SN;
                    DrAdd["Name"] = entity.Name;
                    DrAdd["Orders_ID"] = entity.Orders_ID;
                    DrAdd["Path"] = entity.Path;
                    DrAdd["Addtime"] = entity.Addtime;
                    DrAdd["Site"] = entity.Site;

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

        public virtual int DelOrdersContract(int ID)
        {
            string SqlAdd = "DELETE FROM Orders_Contract WHERE ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual OrdersContractInfo GetOrdersContractByID(int ID)
        {
            OrdersContractInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Orders_Contract WHERE ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new OrdersContractInfo();

                    entity.ID = Tools.NullInt(RdrList["ID"]);
                    entity.SN = Tools.NullStr(RdrList["SN"]);
                    entity.Name = Tools.NullStr(RdrList["Name"]);
                    entity.Orders_ID = Tools.NullInt(RdrList["Orders_ID"]);
                    entity.Path = Tools.NullStr(RdrList["Path"]);
                    entity.Addtime = Tools.NullDate(RdrList["Addtime"]);
                    entity.Site = Tools.NullStr(RdrList["Site"]);

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

        public virtual OrdersContractInfo GetOrdersContractByOrdersID(int Orders_ID)
        {
            OrdersContractInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Orders_Contract WHERE Orders_ID = " + Orders_ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new OrdersContractInfo();

                    entity.ID = Tools.NullInt(RdrList["ID"]);
                    entity.SN = Tools.NullStr(RdrList["SN"]);
                    entity.Name = Tools.NullStr(RdrList["Name"]);
                    entity.Orders_ID = Tools.NullInt(RdrList["Orders_ID"]);
                    entity.Path = Tools.NullStr(RdrList["Path"]);
                    entity.Addtime = Tools.NullDate(RdrList["Addtime"]);
                    entity.Site = Tools.NullStr(RdrList["Site"]);

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

        public virtual IList<OrdersContractInfo> GetOrdersContracts(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<OrdersContractInfo> entitys = null;
            OrdersContractInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Orders_Contract";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<OrdersContractInfo>();
                    while (RdrList.Read())
                    {
                        entity = new OrdersContractInfo();
                        entity.ID = Tools.NullInt(RdrList["ID"]);
                        entity.SN = Tools.NullStr(RdrList["SN"]);
                        entity.Name = Tools.NullStr(RdrList["Name"]);
                        entity.Orders_ID = Tools.NullInt(RdrList["Orders_ID"]);
                        entity.Path = Tools.NullStr(RdrList["Path"]);
                        entity.Addtime = Tools.NullDate(RdrList["Addtime"]);
                        entity.Site = Tools.NullStr(RdrList["Site"]);

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
                SqlTable = "Orders_Contract";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(ID) FROM " + SqlTable + SqlParam;

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

    public class OrdersLoanApply : IOrdersLoanApply
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public OrdersLoanApply()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddOrdersLoanApply(OrdersLoanApplyInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Orders_LoanApply";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["ID"] = entity.ID;
            DrAdd["MemberID"] = entity.MemberID;
            DrAdd["Orders_SN"] = entity.Orders_SN;
            DrAdd["Loan_proj_No"] = entity.Loan_proj_No;
            DrAdd["Loan_Amount"] = entity.Loan_Amount;
            DrAdd["Interest_Rate"] = entity.Interest_Rate;
            DrAdd["Interest_Rate_Unit"] = entity.Interest_Rate_Unit;
            DrAdd["Trem"] = entity.Trem;
            DrAdd["Trem_Unit"] = entity.Trem_Unit;
            DrAdd["Fee_Amount"] = entity.Fee_Amount;
            DrAdd["Repay_Method"] = entity.Repay_Method;
            DrAdd["Margin_Amount"] = entity.Margin_Amount;

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

        public virtual bool EditOrdersLoanApply(OrdersLoanApplyInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Orders_LoanApply WHERE ID = " + entity.ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["ID"] = entity.ID;
                    DrAdd["MemberID"] = entity.MemberID;
                    DrAdd["Orders_SN"] = entity.Orders_SN;
                    DrAdd["Loan_proj_No"] = entity.Loan_proj_No;
                    DrAdd["Loan_Amount"] = entity.Loan_Amount;
                    DrAdd["Interest_Rate"] = entity.Interest_Rate;
                    DrAdd["Interest_Rate_Unit"] = entity.Interest_Rate_Unit;
                    DrAdd["Trem"] = entity.Trem;
                    DrAdd["Trem_Unit"] = entity.Trem_Unit;
                    DrAdd["Fee_Amount"] = entity.Fee_Amount;
                    DrAdd["Repay_Method"] = entity.Repay_Method;
                    DrAdd["Margin_Amount"] = entity.Margin_Amount;

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

        public virtual int DelOrdersLoanApply(int ID)
        {
            string SqlAdd = "DELETE FROM Orders_LoanApply WHERE ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual OrdersLoanApplyInfo GetOrdersLoanApplyByID(int ID)
        {
            OrdersLoanApplyInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Orders_LoanApply WHERE ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new OrdersLoanApplyInfo();

                    entity.ID = Tools.NullInt(RdrList["ID"]);
                    entity.MemberID = Tools.NullInt(RdrList["MemberID"]);
                    entity.Orders_SN = Tools.NullStr(RdrList["Orders_SN"]);
                    entity.Loan_proj_No = Tools.NullStr(RdrList["Loan_proj_No"]);
                    entity.Loan_Amount = Tools.NullDbl(RdrList["Loan_Amount"]);
                    entity.Interest_Rate = Tools.NullDbl(RdrList["Interest_Rate"]);
                    entity.Interest_Rate_Unit = Tools.NullStr(RdrList["Interest_Rate_Unit"]);
                    entity.Trem = Tools.NullDbl(RdrList["Trem"]);
                    entity.Trem_Unit = Tools.NullStr(RdrList["Trem_Unit"]);
                    entity.Fee_Amount = Tools.NullDbl(RdrList["Fee_Amount"]);
                    entity.Repay_Method = Tools.NullStr(RdrList["Repay_Method"]);
                    entity.Margin_Amount = Tools.NullDbl(RdrList["Margin_Amount"]);

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

        public virtual IList<OrdersLoanApplyInfo> GetOrdersLoanApplys(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<OrdersLoanApplyInfo> entitys = null;
            OrdersLoanApplyInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Orders_LoanApply";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<OrdersLoanApplyInfo>();
                    while (RdrList.Read())
                    {
                        entity = new OrdersLoanApplyInfo();
                        entity.ID = Tools.NullInt(RdrList["ID"]);
                        entity.MemberID = Tools.NullInt(RdrList["MemberID"]);
                        entity.Orders_SN = Tools.NullStr(RdrList["Orders_SN"]);
                        entity.Loan_proj_No = Tools.NullStr(RdrList["Loan_proj_No"]);
                        entity.Loan_Amount = Tools.NullDbl(RdrList["Loan_Amount"]);
                        entity.Interest_Rate = Tools.NullDbl(RdrList["Interest_Rate"]);
                        entity.Interest_Rate_Unit = Tools.NullStr(RdrList["Interest_Rate_Unit"]);
                        entity.Trem = Tools.NullDbl(RdrList["Trem"]);
                        entity.Trem_Unit = Tools.NullStr(RdrList["Trem_Unit"]);
                        entity.Fee_Amount = Tools.NullDbl(RdrList["Fee_Amount"]);
                        entity.Repay_Method = Tools.NullStr(RdrList["Repay_Method"]);
                        entity.Margin_Amount = Tools.NullDbl(RdrList["Margin_Amount"]);

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
                SqlTable = "Orders_LoanApply";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(ID) FROM " + SqlTable + SqlParam;

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
