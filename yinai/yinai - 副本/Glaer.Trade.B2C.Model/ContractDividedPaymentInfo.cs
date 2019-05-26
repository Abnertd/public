using System;

namespace Glaer.Trade.B2C.Model
{
    public class ContractDividedPaymentInfo
    {
        private int _Payment_ID;
        private int _Payment_ContractID;
        private string _Payment_Name;
        private double _Payment_Amount;
        private DateTime _Payment_PaymentLine;
        private int _Payment_PaymentStatus;
        private DateTime _Payment_PaymentTime;
        private string _Payment_Note;

        public int Payment_ID
        {
            get { return _Payment_ID; }
            set { _Payment_ID = value; }
        }

        public int Payment_ContractID
        {
            get { return _Payment_ContractID; }
            set { _Payment_ContractID = value; }
        }

        public string Payment_Name
        {
            get { return _Payment_Name; }
            set { _Payment_Name = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public double Payment_Amount
        {
            get { return _Payment_Amount; }
            set { _Payment_Amount = value; }
        }

        public DateTime Payment_PaymentLine
        {
            get { return _Payment_PaymentLine; }
            set { _Payment_PaymentLine = value; }
        }

        public int Payment_PaymentStatus
        {
            get { return _Payment_PaymentStatus; }
            set { _Payment_PaymentStatus = value; }
        }

        public DateTime Payment_PaymentTime
        {
            get { return _Payment_PaymentTime; }
            set { _Payment_PaymentTime = value; }
        }

        public string Payment_Note
        {
            get { return _Payment_Note; }
            set { _Payment_Note = value.Length > 500 ? value.Substring(0, 500) : value.ToString(); }
        }
    }
}
