using System;


namespace Glaer.Trade.B2C.Model
{
    public class SupplierShopEvaluateInfo
    {
        private int _Shop_Evaluate_ID;
        private int _Shop_Evaluate_SupplierID;
        private int _Shop_Evaluate_ContractID;
        private int _Shop_Evaluate_Productid;
        private int _Shop_Evaluate_MemberID;
        private int _Shop_Evaluate_Product;
        private int _Shop_Evaluate_Service;
        private int _Shop_Evaluate_Delivery;
        private string _Shop_Evaluate_Note;
        private int _Shop_Evaluate_Ischeck;
        private int _Shop_Evaluate_Recommend;
        private int _Shop_Evaluate_IsGift;
        private DateTime _Shop_Evaluate_Addtime;
        private string _Shop_Evaluate_Site;

        public int Shop_Evaluate_ID
        {
            get { return _Shop_Evaluate_ID; }
            set { _Shop_Evaluate_ID = value; }
        }

        public int Shop_Evaluate_SupplierID
        {
            get { return _Shop_Evaluate_SupplierID; }
            set { _Shop_Evaluate_SupplierID = value; }
        }

        public int Shop_Evaluate_ContractID
        {
            get { return _Shop_Evaluate_ContractID; }
            set { _Shop_Evaluate_ContractID = value; }
        }

        public int Shop_Evaluate_Productid
        {
            get { return _Shop_Evaluate_Productid; }
            set { _Shop_Evaluate_Productid = value; }
        }

        public int Shop_Evaluate_MemberID
        {
            get { return _Shop_Evaluate_MemberID; }
            set { _Shop_Evaluate_MemberID = value; }
        }

        public int Shop_Evaluate_Product
        {
            get { return _Shop_Evaluate_Product; }
            set { _Shop_Evaluate_Product = value; }
        }

        public int Shop_Evaluate_Service
        {
            get { return _Shop_Evaluate_Service; }
            set { _Shop_Evaluate_Service = value; }
        }

        public int Shop_Evaluate_Delivery
        {
            get { return _Shop_Evaluate_Delivery; }
            set { _Shop_Evaluate_Delivery = value; }
        }

        public string Shop_Evaluate_Note
        {
            get { return _Shop_Evaluate_Note; }
            set { _Shop_Evaluate_Note = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }

        public int Shop_Evaluate_Ischeck
        {
            get { return _Shop_Evaluate_Ischeck; }
            set { _Shop_Evaluate_Ischeck = value; }
        }

        public int Shop_Evaluate_Recommend
        {
            get { return _Shop_Evaluate_Recommend; }
            set { _Shop_Evaluate_Recommend = value; }
        }

        public int Shop_Evaluate_IsGift
        {
            get { return _Shop_Evaluate_IsGift; }
            set { _Shop_Evaluate_IsGift = value; }
        }

        public DateTime Shop_Evaluate_Addtime
        {
            get { return _Shop_Evaluate_Addtime; }
            set { _Shop_Evaluate_Addtime = value; }
        }

        public string Shop_Evaluate_Site
        {
            get { return _Shop_Evaluate_Site; }
            set { _Shop_Evaluate_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

    }
}
