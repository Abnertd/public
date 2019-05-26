using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glaer.Trade.B2C.Model
{
    public class PaymentInformationInfo
    {
        private int _Payment_ID;
        private string _Payment_PayingTeller;
        private string _Payment_Account;
        private string _Payment_Receivable;
        private string _Payment_Account_Receivable;
        private int _Payment_Type;
        private double _Payment_Amount;
        private string _Payment_Remarks;
        private DateTime _Payment_Account_Time;
        private int _Payment_Status;
        private string _Payment_Flow;
        private string _Payment_Remarks1;

        public int Payment_ID
        {
            get { return _Payment_ID; }
            set { _Payment_ID = value; }
        }

        public string Payment_PayingTeller
        {
            get { return _Payment_PayingTeller; }
            set { _Payment_PayingTeller = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Payment_Account
        {
            get { return _Payment_Account; }
         
              set { _Payment_Account = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Payment_Receivable
        {
            get { return _Payment_Receivable; }
            set { _Payment_Receivable = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Payment_Account_Receivable
        {
            get { return _Payment_Account_Receivable; }
            
             set { _Payment_Account_Receivable = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int Payment_Type
        {
            get { return _Payment_Type; }
            set { _Payment_Type = value; }
        }

        public double Payment_Amount
        {
            get { return _Payment_Amount; }
            set { _Payment_Amount = value; }
        }

        public string Payment_Remarks
        {
            get { return _Payment_Remarks; }
            set { _Payment_Remarks = value.Length > 1000 ? value.Substring(0, 1000) : value.ToString(); }
        }

        public DateTime Payment_Account_Time
        {
            get { return _Payment_Account_Time; }
            set { _Payment_Account_Time = value; }
        }

        public int Payment_Status
        {
            get { return _Payment_Status; }
            set { _Payment_Status = value; }
        }

        public string Payment_Flow
        {
            get { return _Payment_Flow; }
            set { _Payment_Flow = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Payment_Remarks1
        {
            get { return _Payment_Remarks1; }
            set { _Payment_Remarks1 = value.Length > 1000 ? value.Substring(0, 1000) : value.ToString(); }
        }

    }
}
