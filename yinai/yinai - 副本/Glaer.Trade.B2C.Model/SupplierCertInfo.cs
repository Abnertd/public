using System;

namespace Glaer.Trade.B2C.Model
{
    public class SupplierCertTypeInfo
    {
        private int _Cert_Type_ID;
        private string _Cert_Type_Name;
        private int _Cert_Type_Sort;
        private int _Cert_Type_IsActive;
        private string _Cert_Type_Site;

        public int Cert_Type_ID
        {
            get { return _Cert_Type_ID; }
            set { _Cert_Type_ID = value; }
        }

        public string Cert_Type_Name
        {
            get { return _Cert_Type_Name; }
            set { _Cert_Type_Name = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int Cert_Type_Sort
        {
            get { return _Cert_Type_Sort; }
            set { _Cert_Type_Sort = value; }
        }

        public int Cert_Type_IsActive
        {
            get { return _Cert_Type_IsActive; }
            set { _Cert_Type_IsActive = value; }
        }

        public string Cert_Type_Site
        {
            get { return _Cert_Type_Site; }
            set { _Cert_Type_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }
    }


    public class SupplierCertInfo
    {
        private int _Supplier_Cert_ID;
        private int _Supplier_Cert_Type;
        private string _Supplier_Cert_Name;
        private string _Supplier_Cert_Note;
        private DateTime _Supplier_Cert_Addtime;
        private int _Supplier_Cert_Sort;
        private string _Supplier_Cert_Site;

        public int Supplier_Cert_ID
        {
            get { return _Supplier_Cert_ID; }
            set { _Supplier_Cert_ID = value; }
        }

        public int Supplier_Cert_Type
        {
            get { return _Supplier_Cert_Type; }
            set { _Supplier_Cert_Type = value; }
        }

        public string Supplier_Cert_Name
        {
            get { return _Supplier_Cert_Name; }
            set { _Supplier_Cert_Name = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Supplier_Cert_Note
        {
            get { return _Supplier_Cert_Note; }
            set { _Supplier_Cert_Note = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }

        public DateTime Supplier_Cert_Addtime
        {
            get { return _Supplier_Cert_Addtime; }
            set { _Supplier_Cert_Addtime = value; }
        }

        public int Supplier_Cert_Sort
        {
            get { return _Supplier_Cert_Sort; }
            set { _Supplier_Cert_Sort = value; }
        }

        public string Supplier_Cert_Site
        {
            get { return _Supplier_Cert_Site; }
            set { _Supplier_Cert_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

    }
}
