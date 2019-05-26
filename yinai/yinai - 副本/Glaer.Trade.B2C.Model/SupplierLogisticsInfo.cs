using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glaer.Trade.B2C.Model
{
    public class SupplierLogisticsInfo
    {
        private int _Supplier_Logistics_ID;
        private int _Supplier_SupplierID;
        private int _Supplier_OrdersID;
        private int _Supplier_LogisticsID;
        private int _Supplier_Status;
        private string _Supplier_Orders_Address_Country;
        private string _Supplier_Orders_Address_State;
        private string _Supplier_Orders_Address_City;
        private string _Supplier_Orders_Address_County;
        private string _Supplier_Orders_Address_StreetAddress;
        private string _Supplier_Address_Country;
        private string _Supplier_Address_State;
        private string _Supplier_Address_City;
        private string _Supplier_Address_County;
        private string _Supplier_Address_StreetAddress;
        private string _Supplier_Logistics_Name;
        private string _Supplier_Logistics_Number;
        private DateTime _Supplier_Logistics_DeliveryTime;
        private int _Supplier_Logistics_IsAudit;
        private DateTime _Supplier_Logistics_AuditTime;
        private string _Supplier_Logistics_AuditRemarks;
        private DateTime _Supplier_Logistics_FinishTime;
        private int _Supplier_Logistics_TenderID;
        private double _Supplier_Logistics_Price;

        public int Supplier_Logistics_ID
        {
            get { return _Supplier_Logistics_ID; }
            set { _Supplier_Logistics_ID = value; }
        }

        public int Supplier_SupplierID
        {
            get { return _Supplier_SupplierID; }
            set { _Supplier_SupplierID = value; }
        }

        public int Supplier_OrdersID
        {
            get { return _Supplier_OrdersID; }
            set { _Supplier_OrdersID = value; }
        }

        public int Supplier_LogisticsID
        {
            get { return _Supplier_LogisticsID; }
            set { _Supplier_LogisticsID = value; }
        }

        public int Supplier_Status
        {
            get { return _Supplier_Status; }
            set { _Supplier_Status = value; }
        }

        public string Supplier_Orders_Address_Country
        {
            get { return _Supplier_Orders_Address_Country; }
            set { _Supplier_Orders_Address_Country = value.Length > 8 ? value.Substring(0, 8) : value.ToString(); }
        }

        public string Supplier_Orders_Address_State
        {
            get { return _Supplier_Orders_Address_State; }
            set { _Supplier_Orders_Address_State = value.Length > 8 ? value.Substring(0, 8) : value.ToString(); }
        }

        public string Supplier_Orders_Address_City
        {
            get { return _Supplier_Orders_Address_City; }
            set { _Supplier_Orders_Address_City = value.Length > 8 ? value.Substring(0, 8) : value.ToString(); }
        }

        public string Supplier_Orders_Address_County
        {
            get { return _Supplier_Orders_Address_County; }
            set { _Supplier_Orders_Address_County = value.Length > 8 ? value.Substring(0, 8) : value.ToString(); }
        }

        public string Supplier_Orders_Address_StreetAddress
        {
            get { return _Supplier_Orders_Address_StreetAddress; }
            set { _Supplier_Orders_Address_StreetAddress = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Supplier_Address_Country
        {
            get { return _Supplier_Address_Country; }
            set { _Supplier_Address_Country = value.Length > 8 ? value.Substring(0, 8) : value.ToString(); }
        }

        public string Supplier_Address_State
        {
            get { return _Supplier_Address_State; }
            set { _Supplier_Address_State = value.Length > 8 ? value.Substring(0, 8) : value.ToString(); }
        }

        public string Supplier_Address_City
        {
            get { return _Supplier_Address_City; }
            set { _Supplier_Address_City = value.Length > 8 ? value.Substring(0, 8) : value.ToString(); }
        }

        public string Supplier_Address_County
        {
            get { return _Supplier_Address_County; }
            set { _Supplier_Address_County = value.Length > 8 ? value.Substring(0, 8) : value.ToString(); }
        }

        public string Supplier_Address_StreetAddress
        {
            get { return _Supplier_Address_StreetAddress; }
            set { _Supplier_Address_StreetAddress = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Supplier_Logistics_Name
        {
            get { return _Supplier_Logistics_Name; }
            set { _Supplier_Logistics_Name = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Supplier_Logistics_Number
        {
            get { return _Supplier_Logistics_Number; }
            set { _Supplier_Logistics_Number = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public DateTime Supplier_Logistics_DeliveryTime
        {
            get { return _Supplier_Logistics_DeliveryTime; }
            set { _Supplier_Logistics_DeliveryTime = value; }
        }

        public int Supplier_Logistics_IsAudit
        {
            get { return _Supplier_Logistics_IsAudit; }
            set { _Supplier_Logistics_IsAudit = value; }
        }

        public DateTime Supplier_Logistics_AuditTime
        {
            get { return _Supplier_Logistics_AuditTime; }
            set { _Supplier_Logistics_AuditTime = value; }
        }

        public string Supplier_Logistics_AuditRemarks
        {
            get { return _Supplier_Logistics_AuditRemarks; }
            set { _Supplier_Logistics_AuditRemarks = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public DateTime Supplier_Logistics_FinishTime
        {
            get { return _Supplier_Logistics_FinishTime; }
            set { _Supplier_Logistics_FinishTime = value; }
        }

        public int Supplier_Logistics_TenderID
        {
            get { return _Supplier_Logistics_TenderID; }
            set { _Supplier_Logistics_TenderID = value; }
        }

        public double Supplier_Logistics_Price
        {
            get { return _Supplier_Logistics_Price; }
            set { _Supplier_Logistics_Price = value; }
        }

    }

    public class LogisticsTenderInfo
    {
        private int _Logistics_Tender_ID;
        private int _Logistics_Tender_LogisticsID;
        private int _Logistics_Tender_SupplierLogisticsID;
        private int _Logistics_Tender_OrderID;
        private double _Logistics_Tender_Price;
        private DateTime _Logistics_Tender_AddTime;
        private int _Logistics_Tender_IsWin;
        private DateTime _Logistics_Tender_FinishTime;

        public int Logistics_Tender_ID
        {
            get { return _Logistics_Tender_ID; }
            set { _Logistics_Tender_ID = value; }
        }

        public int Logistics_Tender_LogisticsID
        {
            get { return _Logistics_Tender_LogisticsID; }
            set { _Logistics_Tender_LogisticsID = value; }
        }

        public int Logistics_Tender_SupplierLogisticsID
        {
            get { return _Logistics_Tender_SupplierLogisticsID; }
            set { _Logistics_Tender_SupplierLogisticsID = value; }
        }

        public int Logistics_Tender_OrderID
        {
            get { return _Logistics_Tender_OrderID; }
            set { _Logistics_Tender_OrderID = value; }
        }

        public double Logistics_Tender_Price
        {
            get { return _Logistics_Tender_Price; }
            set { _Logistics_Tender_Price = value; }
        }

        public DateTime Logistics_Tender_AddTime
        {
            get { return _Logistics_Tender_AddTime; }
            set { _Logistics_Tender_AddTime = value; }
        }

        public int Logistics_Tender_IsWin
        {
            get { return _Logistics_Tender_IsWin; }
            set { _Logistics_Tender_IsWin = value; }
        }

        public DateTime Logistics_Tender_FinishTime
        {
            get { return _Logistics_Tender_FinishTime; }
            set { _Logistics_Tender_FinishTime = value; }
        }

    }
}
