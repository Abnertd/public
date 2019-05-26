using System;

namespace Glaer.Trade.B2C.Model
{
    public class SupplierBankInfo
    {
        private int _Supplier_Bank_ID;
        private int _Supplier_Bank_SupplierID;
        private string _Supplier_Bank_Name;
        private string _Supplier_Bank_NetWork;
        private string _Supplier_Bank_SName;
        private string _Supplier_Bank_Account;
        private string _Supplier_Bank_Attachment;

        public int Supplier_Bank_ID
        {
            get { return _Supplier_Bank_ID; }
            set { _Supplier_Bank_ID = value; }
        }
        public int Supplier_Bank_SupplierID
        {
            get { return _Supplier_Bank_SupplierID; }
            set { _Supplier_Bank_SupplierID = value; }
        }
        
        public string Supplier_Bank_Name
        {
            get { return _Supplier_Bank_Name; }
            set { _Supplier_Bank_Name = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Supplier_Bank_NetWork
        {
            get { return _Supplier_Bank_NetWork; }
            set { _Supplier_Bank_NetWork = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Supplier_Bank_SName
        {
            get { return _Supplier_Bank_SName; }
            set { _Supplier_Bank_SName = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Supplier_Bank_Account
        {
            get { return _Supplier_Bank_Account; }
            set { _Supplier_Bank_Account = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Supplier_Bank_Attachment
        {
            get { return _Supplier_Bank_Attachment; }
            set { _Supplier_Bank_Attachment = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

    }
}
