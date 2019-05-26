using System;

namespace Glaer.Trade.B2C.Model
{
    public class SupplierShopApplyInfo
    {
        private int _Shop_Apply_ID;
        private int _Shop_Apply_SupplierID;
        private int _Shop_Apply_ShopType;
        private string _Shop_Apply_Name;
        private string _Shop_Apply_PINCode;
        private string _Shop_Apply_Mobile;
        private string _Shop_Apply_ShopName;
        private string _Shop_Apply_CompanyType;
        private string _Shop_Apply_Lawman;
        private string _Shop_Apply_CertCode;
        private string _Shop_Apply_CertAddress;
        private string _Shop_Apply_CompanyAddress;
        private string _Shop_Apply_CompanyPhone;
        private string _Shop_Apply_Certification1;
        private string _Shop_Apply_Certification2;
        private string _Shop_Apply_Certification3;
        private string _Shop_Apply_Certification4;
        private string _Shop_Apply_Certification5;
        private string _Shop_Apply_MainBrand;
        private int _Shop_Apply_Status;
        private string _Shop_Apply_Note;
        private DateTime _Shop_Apply_Addtime;

        public int Shop_Apply_ID
        {
            get { return _Shop_Apply_ID; }
            set { _Shop_Apply_ID = value; }
        }

        public int Shop_Apply_SupplierID
        {
            get { return _Shop_Apply_SupplierID; }
            set { _Shop_Apply_SupplierID = value; }
        }

        public int Shop_Apply_ShopType
        {
            get { return _Shop_Apply_ShopType; }
            set { _Shop_Apply_ShopType = value; }
        }

        public string Shop_Apply_Name
        {
            get { return _Shop_Apply_Name; }
            set { _Shop_Apply_Name = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Shop_Apply_PINCode
        {
            get { return _Shop_Apply_PINCode; }
            set { _Shop_Apply_PINCode = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Shop_Apply_Mobile
        {
            get { return _Shop_Apply_Mobile; }
            set { _Shop_Apply_Mobile = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Shop_Apply_ShopName
        {
            get { return _Shop_Apply_ShopName; }
            set { _Shop_Apply_ShopName = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Shop_Apply_CompanyType
        {
            get { return _Shop_Apply_CompanyType; }
            set { _Shop_Apply_CompanyType = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Shop_Apply_Lawman
        {
            get { return _Shop_Apply_Lawman; }
            set { _Shop_Apply_Lawman = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Shop_Apply_CertCode
        {
            get { return _Shop_Apply_CertCode; }
            set { _Shop_Apply_CertCode = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Shop_Apply_CertAddress
        {
            get { return _Shop_Apply_CertAddress; }
            set { _Shop_Apply_CertAddress = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Shop_Apply_CompanyAddress
        {
            get { return _Shop_Apply_CompanyAddress; }
            set { _Shop_Apply_CompanyAddress = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Shop_Apply_CompanyPhone
        {
            get { return _Shop_Apply_CompanyPhone; }
            set { _Shop_Apply_CompanyPhone = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Shop_Apply_Certification1
        {
            get { return _Shop_Apply_Certification1; }
            set { _Shop_Apply_Certification1 = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Shop_Apply_Certification2
        {
            get { return _Shop_Apply_Certification2; }
            set { _Shop_Apply_Certification2 = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Shop_Apply_Certification3
        {
            get { return _Shop_Apply_Certification3; }
            set { _Shop_Apply_Certification3 = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Shop_Apply_Certification4
        {
            get { return _Shop_Apply_Certification4; }
            set { _Shop_Apply_Certification4 = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Shop_Apply_Certification5
        {
            get { return _Shop_Apply_Certification5; }
            set { _Shop_Apply_Certification5 = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Shop_Apply_MainBrand
        {
            get { return _Shop_Apply_MainBrand; }
            set { _Shop_Apply_MainBrand = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Shop_Apply_Note
        {
            get { return _Shop_Apply_Note; }
            set { _Shop_Apply_Note = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }

        public int Shop_Apply_Status
        {
            get { return _Shop_Apply_Status; }
            set { _Shop_Apply_Status = value; }
        }

        public DateTime Shop_Apply_Addtime
        {
            get { return _Shop_Apply_Addtime; }
            set { _Shop_Apply_Addtime = value; }
        }

    }
}
