using System;

namespace Glaer.Trade.B2C.Model
{
    public class MemberAccountOrdersInfo
    {
        private int _Account_Orders_ID;
        private int _Account_Orders_MemberID;
        private int _Account_Orders_SupplierID;
        private int _Account_Orders_AccountType;
        private string _Account_Orders_SN;
        private double _Account_Orders_Amount;
        private int _Account_Orders_Status;
        private DateTime _Account_Orders_Addtime;

        public int Account_Orders_ID
        {
            get { return _Account_Orders_ID; }
            set { _Account_Orders_ID = value; }
        }

        public int Account_Orders_MemberID
        {
            get { return _Account_Orders_MemberID; }
            set { _Account_Orders_MemberID = value; }
        }

        public int Account_Orders_SupplierID
        {
            get { return _Account_Orders_SupplierID; }
            set { _Account_Orders_SupplierID = value; }
        }

        public int Account_Orders_AccountType
        {
            get { return _Account_Orders_AccountType; }
            set { _Account_Orders_AccountType = value; }
        }

        public string Account_Orders_SN
        {
            get { return _Account_Orders_SN; }
            set { _Account_Orders_SN = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public double Account_Orders_Amount
        {
            get { return _Account_Orders_Amount; }
            set { _Account_Orders_Amount = value; }
        }

        public int Account_Orders_Status
        {
            get { return _Account_Orders_Status; }
            set { _Account_Orders_Status = value; }
        }

        public DateTime Account_Orders_Addtime
        {
            get { return _Account_Orders_Addtime; }
            set { _Account_Orders_Addtime = value; }
        }

    }
}
