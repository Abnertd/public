using System;

namespace Glaer.Trade.B2C.Model
{
    public class SupplierCreditLimitLogInfo
    {
        private int _CreditLimit_Log_ID;
        private int _CreditLimit_Log_Type;
        private int _CreditLimit_Log_SupplierID;
        private double _CreditLimit_Log_Amount;
        private double _CreditLimit_Log_AmountRemain;
        private string _CreditLimit_Log_Note;
        private DateTime _CreditLimit_Log_Addtime;
        private int _CreditLimit_Log_PaymentStatus;

        public int CreditLimit_Log_ID
        {
            get { return _CreditLimit_Log_ID; }
            set { _CreditLimit_Log_ID = value; }
        }

        public int CreditLimit_Log_Type
        {
            get { return _CreditLimit_Log_Type; }
            set { _CreditLimit_Log_Type = value; }
        }

        public int CreditLimit_Log_SupplierID
        {
            get { return _CreditLimit_Log_SupplierID; }
            set { _CreditLimit_Log_SupplierID = value; }
        }

        public double CreditLimit_Log_Amount
        {
            get { return _CreditLimit_Log_Amount; }
            set { _CreditLimit_Log_Amount = value; }
        }

        public double CreditLimit_Log_AmountRemain
        {
            get { return _CreditLimit_Log_AmountRemain; }
            set { _CreditLimit_Log_AmountRemain = value; }
        }

        public string CreditLimit_Log_Note
        {
            get { return _CreditLimit_Log_Note; }
            set { _CreditLimit_Log_Note = value.Length > 500 ? value.Substring(0, 500) : value.ToString(); }
        }

        public DateTime CreditLimit_Log_Addtime
        {
            get { return _CreditLimit_Log_Addtime; }
            set { _CreditLimit_Log_Addtime = value; }
        }

        public int CreditLimit_Log_PaymentStatus
        {
            get { return _CreditLimit_Log_PaymentStatus; }
            set { _CreditLimit_Log_PaymentStatus = value; }
        }

    }
}
