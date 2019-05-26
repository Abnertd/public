using System;

namespace Glaer.Trade.B2C.Model
{
    public class ContractPaymentInfo
    {
        private int _Contract_Payment_ID;
        private int _Contract_Payment_ContractID;
        private int _Contract_Payment_PaymentStatus;
        private int _Contract_Payment_SysUserID;
        private string _Contract_Payment_DocNo;
        private string _Contract_Payment_Name;
        private double _Contract_Payment_Amount;
        private string _Contract_Payment_Note;
        private DateTime _Contract_Payment_Addtime;
        private string _Contract_Payment_Site;
        private string _Contract_Payment_Attachment;

        public int Contract_Payment_ID
        {
            get { return _Contract_Payment_ID; }
            set { _Contract_Payment_ID = value; }
        }

        public int Contract_Payment_ContractID
        {
            get { return _Contract_Payment_ContractID; }
            set { _Contract_Payment_ContractID = value; }
        }

        public int Contract_Payment_PaymentStatus
        {
            get { return _Contract_Payment_PaymentStatus; }
            set { _Contract_Payment_PaymentStatus = value; }
        }

        public int Contract_Payment_SysUserID
        {
            get { return _Contract_Payment_SysUserID; }
            set { _Contract_Payment_SysUserID = value; }
        }

        public string Contract_Payment_DocNo
        {
            get { return _Contract_Payment_DocNo; }
            set { _Contract_Payment_DocNo = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Contract_Payment_Name
        {
            get { return _Contract_Payment_Name; }
            set { _Contract_Payment_Name = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public double Contract_Payment_Amount
        {
            get { return _Contract_Payment_Amount; }
            set { _Contract_Payment_Amount = value; }
        }

        public string Contract_Payment_Note
        {
            get { return _Contract_Payment_Note; }
            set { _Contract_Payment_Note = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public DateTime Contract_Payment_Addtime
        {
            get { return _Contract_Payment_Addtime; }
            set { _Contract_Payment_Addtime = value; }
        }

        public string Contract_Payment_Site
        {
            get { return _Contract_Payment_Site; }
            set { _Contract_Payment_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Contract_Payment_Attachment
        {
            get { return _Contract_Payment_Attachment; }
            set { _Contract_Payment_Attachment = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

    }
}
