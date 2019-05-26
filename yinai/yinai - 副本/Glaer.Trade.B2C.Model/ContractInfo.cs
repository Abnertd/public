using System;

namespace Glaer.Trade.B2C.Model
{
    public class ContractInfo
    {
        private int _Contract_ID;
        private int _Contract_Type;
        private int _Contract_BuyerID;
        private string _Contract_BuyerName;
        private int _Contract_SupplierID;
        private string _Contract_SupplierName;
        private string _Contract_SN;
        private string _Contract_Name;
        private int _Contract_Status;
        private int _Contract_Confirm_Status;
        private int _Contract_Payment_Status;
        private int _Contract_Delivery_Status;
        private double _Contract_AllPrice;
        private double _Contract_Price;
        private double _Contract_Freight;
        private double _Contract_ServiceFee;
        private double _Contract_Discount;
        private int _Contract_Delivery_ID;
        private string _Contract_Delivery_Name;
        private int _Contract_Payway_ID;
        private string _Contract_Payway_Name;
        private string _Contract_Note;
        private string _Contract_Template;
        private DateTime _Contract_Addtime;
        private string _Contract_Site;
        private int _Contract_Source;
        private int _Contract_IsEvaluate;

        public int Contract_ID
        {
            get { return _Contract_ID; }
            set { _Contract_ID = value; }
        }

        public int Contract_Type
        {
            get { return _Contract_Type; }
            set { _Contract_Type = value; }
        }

        public int Contract_BuyerID
        {
            get { return _Contract_BuyerID; }
            set { _Contract_BuyerID = value; }
        }

        public string Contract_BuyerName
        {
            get { return _Contract_BuyerName; }
            set { _Contract_BuyerName = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public int Contract_SupplierID
        {
            get { return _Contract_SupplierID; }
            set { _Contract_SupplierID = value; }
        }

        public string Contract_SupplierName
        {
            get { return _Contract_SupplierName; }
            set { _Contract_SupplierName = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Contract_SN
        {
            get { return _Contract_SN; }
            set { _Contract_SN = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Contract_Name
        {
            get { return _Contract_Name; }
            set { _Contract_Name = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public int Contract_Status
        {
            get { return _Contract_Status; }
            set { _Contract_Status = value; }
        }

        public int Contract_Confirm_Status
        {
            get { return _Contract_Confirm_Status; }
            set { _Contract_Confirm_Status = value; }
        }

        public int Contract_Payment_Status
        {
            get { return _Contract_Payment_Status; }
            set { _Contract_Payment_Status = value; }
        }

        public int Contract_Delivery_Status
        {
            get { return _Contract_Delivery_Status; }
            set { _Contract_Delivery_Status = value; }
        }

        public double Contract_AllPrice
        {
            get { return _Contract_AllPrice; }
            set { _Contract_AllPrice = value; }
        }

        public double Contract_Price
        {
            get { return _Contract_Price; }
            set { _Contract_Price = value; }
        }

        public double Contract_Freight
        {
            get { return _Contract_Freight; }
            set { _Contract_Freight = value; }
        }

        public double Contract_ServiceFee
        {
            get { return _Contract_ServiceFee; }
            set { _Contract_ServiceFee = value; }
        }

        public double Contract_Discount
        {
            get { return _Contract_Discount; }
            set { _Contract_Discount = value; }
        }

        public int Contract_Delivery_ID
        {
            get { return _Contract_Delivery_ID; }
            set { _Contract_Delivery_ID = value; }
        }

        public string Contract_Delivery_Name
        {
            get { return _Contract_Delivery_Name; }
            set { _Contract_Delivery_Name = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public int Contract_Payway_ID
        {
            get { return _Contract_Payway_ID; }
            set { _Contract_Payway_ID = value; }
        }

        public string Contract_Payway_Name
        {
            get { return _Contract_Payway_Name; }
            set { _Contract_Payway_Name = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Contract_Note
        {
            get { return _Contract_Note; }
            set { _Contract_Note = value; }
        }

        public string Contract_Template
        {
            get { return _Contract_Template; }
            set { _Contract_Template = value; }
        }

        public DateTime Contract_Addtime
        {
            get { return _Contract_Addtime; }
            set { _Contract_Addtime = value; }
        }

        public string Contract_Site
        {
            get { return _Contract_Site; }
            set { _Contract_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int Contract_Source
        {
            get { return _Contract_Source; }
            set { _Contract_Source = value; }
        }

        public int Contract_IsEvaluate
        {
            get { return _Contract_IsEvaluate; }
            set { _Contract_IsEvaluate = value; }
        }

    }
}
