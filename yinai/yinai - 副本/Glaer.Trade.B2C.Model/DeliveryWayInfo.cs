using System;

namespace Glaer.Trade.B2C.Model
{
    public class DeliveryWayInfo
    {
        private int _Delivery_Way_ID;
        private int _Delivery_Way_SupplierID;
        private string _Delivery_Way_Name;
        private int _Delivery_Way_Sort;
        private int _Delivery_Way_InitialWeight;
        private int _Delivery_Way_UpWeight;
        private int _Delivery_Way_FeeType;
        private double _Delivery_Way_Fee;
        private double _Delivery_Way_InitialFee;
        private double _Delivery_Way_UpFee;
        private int _Delivery_Way_Status;
        private int _Delivery_Way_Cod;
        private string _Delivery_Way_Img;
        private string _Delivery_Way_Url;
        private string _Delivery_Way_Intro;
        private string _Delivery_Way_Site;

        public int Delivery_Way_ID
        {
            get { return _Delivery_Way_ID; }
            set { _Delivery_Way_ID = value; }
        }

        public int Delivery_Way_SupplierID
        {
            get { return _Delivery_Way_SupplierID; }
            set { _Delivery_Way_SupplierID = value; }
        }

        public string Delivery_Way_Name
        {
            get { return _Delivery_Way_Name; }
            set { _Delivery_Way_Name = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int Delivery_Way_Sort
        {
            get { return _Delivery_Way_Sort; }
            set { _Delivery_Way_Sort = value; }
        }

        public int Delivery_Way_InitialWeight
        {
            get { return _Delivery_Way_InitialWeight; }
            set { _Delivery_Way_InitialWeight = value; }
        }

        public int Delivery_Way_UpWeight
        {
            get { return _Delivery_Way_UpWeight; }
            set { _Delivery_Way_UpWeight = value; }
        }

        public int Delivery_Way_FeeType
        {
            get { return _Delivery_Way_FeeType; }
            set { _Delivery_Way_FeeType = value; }
        }

        public double Delivery_Way_Fee
        {
            get { return _Delivery_Way_Fee; }
            set { _Delivery_Way_Fee = value; }
        }

        public double Delivery_Way_InitialFee
        {
            get { return _Delivery_Way_InitialFee; }
            set { _Delivery_Way_InitialFee = value; }
        }

        public double Delivery_Way_UpFee
        {
            get { return _Delivery_Way_UpFee; }
            set { _Delivery_Way_UpFee = value; }
        }

        public int Delivery_Way_Status
        {
            get { return _Delivery_Way_Status; }
            set { _Delivery_Way_Status = value; }
        }

        public int Delivery_Way_Cod
        {
            get { return _Delivery_Way_Cod; }
            set { _Delivery_Way_Cod = value; }
        }

        public string Delivery_Way_Img
        {
            get { return _Delivery_Way_Img; }
            set { _Delivery_Way_Img = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Delivery_Way_Url
        {
            get { return _Delivery_Way_Url; }
            set { _Delivery_Way_Url = value.Length > 300 ? value.Substring(0, 300) : value.ToString(); }
        }

        public string Delivery_Way_Intro
        {
            get { return _Delivery_Way_Intro; }
            set { _Delivery_Way_Intro = value.Length > 1000 ? value.Substring(0, 1000) : value.ToString(); }
        }

        public string Delivery_Way_Site
        {
            get { return _Delivery_Way_Site; }
            set { _Delivery_Way_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

    }

    public class DeliveryWayDistrictInfo
    {
        private int _District_ID;
        private int _District_DeliveryWayID;
        private string _District_Country;
        private string _District_State;
        private string _District_City;
        private string _District_County;

        public int District_ID
        {
            get { return _District_ID; }
            set { _District_ID = value; }
        }

        public int District_DeliveryWayID
        {
            get { return _District_DeliveryWayID; }
            set { _District_DeliveryWayID = value; }
        }

        public string District_Country
        {
            get { return _District_Country; }
            set { _District_Country = value.Length > 8 ? value.Substring(0, 8) : value.ToString(); }
        }

        public string District_State
        {
            get { return _District_State; }
            set { _District_State = value.Length > 8 ? value.Substring(0, 8) : value.ToString(); }
        }

        public string District_City
        {
            get { return _District_City; }
            set { _District_City = value.Length > 8 ? value.Substring(0, 8) : value.ToString(); }
        }

        public string District_County
        {
            get { return _District_County; }
            set { _District_County = value.Length > 8 ? value.Substring(0, 8) : value.ToString(); }
        }

    }
}
