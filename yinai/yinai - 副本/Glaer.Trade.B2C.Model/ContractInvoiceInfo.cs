using System;

namespace Glaer.Trade.B2C.Model
{
    public class ContractInvoiceInfo
    {
        private int _Invoice_ID;
        private int _Invoice_ContractID;
        private int _Invoice_Type;
        private string _Invoice_Title;
        private int _Invoice_Content;
        private string _Invoice_FirmName;
        private string _Invoice_VAT_FirmName;
        private string _Invoice_VAT_Code;
        private string _Invoice_VAT_RegAddr;
        private string _Invoice_VAT_RegTel;
        private string _Invoice_VAT_Bank;
        private string _Invoice_VAT_BankAccount;
        private string _Invoice_VAT_Content;
        private string _Invoice_Address;
        private string _Invoice_Name;
        private string _Invoice_ZipCode;
        private string _Invoice_Tel;
        private int _Invoice_Status;
        private string _Invoice_PersonelName;
        private string _Invoice_PersonelCard;
        private string _Invoice_VAT_Cert;

        public int Invoice_ID
        {
            get { return _Invoice_ID; }
            set { _Invoice_ID = value; }
        }

        public int Invoice_ContractID
        {
            get { return _Invoice_ContractID; }
            set { _Invoice_ContractID = value; }
        }

        public int Invoice_Type
        {
            get { return _Invoice_Type; }
            set { _Invoice_Type = value; }
        }

        public string Invoice_Title
        {
            get { return _Invoice_Title; }
            set { _Invoice_Title = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public int Invoice_Content
        {
            get { return _Invoice_Content; }
            set { _Invoice_Content = value; }
        }

        public string Invoice_FirmName
        {
            get { return _Invoice_FirmName; }
            set { _Invoice_FirmName = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Invoice_VAT_FirmName
        {
            get { return _Invoice_VAT_FirmName; }
            set { _Invoice_VAT_FirmName = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Invoice_VAT_Code
        {
            get { return _Invoice_VAT_Code; }
            set { _Invoice_VAT_Code = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Invoice_VAT_RegAddr
        {
            get { return _Invoice_VAT_RegAddr; }
            set { _Invoice_VAT_RegAddr = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Invoice_VAT_RegTel
        {
            get { return _Invoice_VAT_RegTel; }
            set { _Invoice_VAT_RegTel = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Invoice_VAT_Bank
        {
            get { return _Invoice_VAT_Bank; }
            set { _Invoice_VAT_Bank = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Invoice_VAT_BankAccount
        {
            get { return _Invoice_VAT_BankAccount; }
            set { _Invoice_VAT_BankAccount = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Invoice_VAT_Content
        {
            get { return _Invoice_VAT_Content; }
            set { _Invoice_VAT_Content = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Invoice_Address
        {
            get { return _Invoice_Address; }
            set { _Invoice_Address = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }

        public string Invoice_Name
        {
            get { return _Invoice_Name; }
            set { _Invoice_Name = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Invoice_ZipCode
        {
            get { return _Invoice_ZipCode; }
            set { _Invoice_ZipCode = value.Length > 20 ? value.Substring(0, 20) : value.ToString(); }
        }

        public string Invoice_Tel
        {
            get { return _Invoice_Tel; }
            set { _Invoice_Tel = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int Invoice_Status
        {
            get { return _Invoice_Status; }
            set { _Invoice_Status = value; }
        }

        public string Invoice_PersonelName
        {
            get { return _Invoice_PersonelName; }
            set { _Invoice_PersonelName = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Invoice_PersonelCard
        {
            get { return _Invoice_PersonelCard; }
            set { _Invoice_PersonelCard = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Invoice_VAT_Cert
        {
            get { return _Invoice_VAT_Cert; }
            set { _Invoice_VAT_Cert = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

    }

    public class ContractInvoiceApplyInfo
    {
        private int _Invoice_Apply_ID;
        private int _Invoice_Apply_ContractID;
        private int _Invoice_Apply_InvoiceID;
        private double _Invoice_Apply_ApplyAmount;
        private double _Invoice_Apply_Amount;
        private int _Invoice_Apply_Status;
        private string _Invoice_Apply_Note;
        private DateTime _Invoice_Apply_Addtime;

        public int Invoice_Apply_ID
        {
            get { return _Invoice_Apply_ID; }
            set { _Invoice_Apply_ID = value; }
        }

        public int Invoice_Apply_ContractID
        {
            get { return _Invoice_Apply_ContractID; }
            set { _Invoice_Apply_ContractID = value; }
        }

        public int Invoice_Apply_InvoiceID
        {
            get { return _Invoice_Apply_InvoiceID; }
            set { _Invoice_Apply_InvoiceID = value; }
        }

        public double Invoice_Apply_ApplyAmount
        {
            get { return _Invoice_Apply_ApplyAmount; }
            set { _Invoice_Apply_ApplyAmount = value; }
        }

        public double Invoice_Apply_Amount
        {
            get { return _Invoice_Apply_Amount; }
            set { _Invoice_Apply_Amount = value; }
        }

        public int Invoice_Apply_Status
        {
            get { return _Invoice_Apply_Status; }
            set { _Invoice_Apply_Status = value; }
        }

        public string Invoice_Apply_Note
        {
            get { return _Invoice_Apply_Note; }
            set { _Invoice_Apply_Note = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }

        public DateTime Invoice_Apply_Addtime
        {
            get { return _Invoice_Apply_Addtime; }
            set { _Invoice_Apply_Addtime = value; }
        }

    }
}
