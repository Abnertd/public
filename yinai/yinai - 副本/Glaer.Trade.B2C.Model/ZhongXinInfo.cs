using System;

namespace Glaer.Trade.B2C.Model
{
    public class ZhongXinInfo
    {
        private int _ID;
        private int _SupplierID;
        private string _CompanyName;
        private string _ReceiptAccount;
        private string _ReceiptBank;
        private string _BankCode;
        private string _BankName;
        private string _OpenAccountName;
        private string _SubAccount;
        private int _Audit;
        private int _Register;
        private DateTime _Addtime;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public int SupplierID
        {
            get { return _SupplierID; }
            set { _SupplierID = value; }
        }

        public string CompanyName
        {
            get { return _CompanyName; }
            set { _CompanyName = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string ReceiptAccount
        {
            get { return _ReceiptAccount; }
            set { _ReceiptAccount = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string ReceiptBank
        {
            get { return _ReceiptBank; }
            set { _ReceiptBank = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string BankCode
        {
            get { return _BankCode; }
            set { _BankCode = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string BankName
        {
            get { return _BankName; }
            set { _BankName = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string OpenAccountName
        {
            get { return _OpenAccountName; }
            set { _OpenAccountName = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string SubAccount
        {
            get { return _SubAccount; }
            set { _SubAccount = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int Audit
        {
            get { return _Audit; }
            set { _Audit = value; }
        }

        public int Register
        {
            get { return _Register; }
            set { _Register = value; }
        }

        public DateTime Addtime
        {
            get { return _Addtime; }
            set { _Addtime = value; }
        }

    }

    public class ZhongXinAccountLogInfo
    {
        private int _Account_Log_ID;
        private int _Account_Log_MemberID;
        private double _Account_Log_Amount;
        private double _Account_Log_Remain;
        private string _Account_Log_Note;
        private DateTime _Account_Log_Addtime;
        private string _Account_Log_Site;

        public int Account_Log_ID
        {
            get { return _Account_Log_ID; }
            set { _Account_Log_ID = value; }
        }

        public int Account_Log_MemberID
        {
            get { return _Account_Log_MemberID; }
            set { _Account_Log_MemberID = value; }
        }

        public double Account_Log_Amount
        {
            get { return _Account_Log_Amount; }
            set { _Account_Log_Amount = value; }
        }

        public double Account_Log_Remain
        {
            get { return _Account_Log_Remain; }
            set { _Account_Log_Remain = value; }
        }

        public string Account_Log_Note
        {
            get { return _Account_Log_Note; }
            set { _Account_Log_Note = value.Length > 500 ? value.Substring(0, 500) : value.ToString(); }
        }

        public DateTime Account_Log_Addtime
        {
            get { return _Account_Log_Addtime; }
            set { _Account_Log_Addtime = value; }
        }

        public string Account_Log_Site
        {
            get { return _Account_Log_Site; }
            set { _Account_Log_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

    }
}
