using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glaer.Trade.B2C.Model
{
    public class MemberInvoiceInfo
    {
        private int _Invoice_ID;
        private int _Invoice_MemberID;
        private int _Invoice_Type;
        private string _Invoice_Title;
        private int _Invoice_Details;
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
        private string _Invoice_PersonelName;
        private string _Invoice_PersonelCard;
        private string _Invoice_VAT_Cert;

        public int Invoice_ID
        {
            get { return _Invoice_ID; }
            set { _Invoice_ID = value; }
        }

        public int Invoice_MemberID
        {
            get { return _Invoice_MemberID; }
            set { _Invoice_MemberID = value; }
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

        public int Invoice_Details
        {
            get { return _Invoice_Details; }
            set { _Invoice_Details = value; }
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
            set { _Invoice_VAT_Cert = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }
    }
}
