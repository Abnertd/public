using System;

namespace Glaer.Trade.B2C.Model
{
    public class OrdersPaymentInfo
    {
        private int _Orders_Payment_ID;
        private int _Orders_Payment_OrdersID;
        private int _Orders_Payment_MemberID = 0;
        private int _Orders_Payment_PaymentStatus;
        private int _Orders_Payment_SysUserID;
        private string _Orders_Payment_DocNo;
        private string _Orders_Payment_Name;
        private double _Orders_Payment_ApplyAmount = 0;
        private double _Orders_Payment_Amount;
        private int _Orders_Payment_Status = 0;
        private string _Orders_Payment_Note;
        private DateTime _Orders_Payment_Addtime;
        private string _Orders_Payment_Site;

        public int Orders_Payment_ID
        {
            get { return _Orders_Payment_ID; }
            set { _Orders_Payment_ID = value; }
        }

        public int Orders_Payment_OrdersID
        {
            get { return _Orders_Payment_OrdersID; }
            set { _Orders_Payment_OrdersID = value; }
        }

        public int Orders_Payment_MemberID
        {
            get { return _Orders_Payment_MemberID; }
            set { _Orders_Payment_MemberID = value; }
        }

        public int Orders_Payment_PaymentStatus
        {
            get { return _Orders_Payment_PaymentStatus; }
            set { _Orders_Payment_PaymentStatus = value; }
        }

        public int Orders_Payment_SysUserID
        {
            get { return _Orders_Payment_SysUserID; }
            set { _Orders_Payment_SysUserID = value; }
        }

        public string Orders_Payment_DocNo
        {
            get { return _Orders_Payment_DocNo; }
            set { _Orders_Payment_DocNo = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Orders_Payment_Name
        {
            get { return _Orders_Payment_Name; }
            set { _Orders_Payment_Name = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public double Orders_Payment_ApplyAmount
        {
            get { return _Orders_Payment_ApplyAmount; }
            set { _Orders_Payment_ApplyAmount = value; }
        }

        public double Orders_Payment_Amount
        {
            get { return _Orders_Payment_Amount; }
            set { _Orders_Payment_Amount = value; }
        }

        public int Orders_Payment_Status
        {
            get { return _Orders_Payment_Status; }
            set { _Orders_Payment_Status = value; }
        }

        public string Orders_Payment_Note
        {
            get { return _Orders_Payment_Note; }
            set { _Orders_Payment_Note = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public DateTime Orders_Payment_Addtime
        {
            get { return _Orders_Payment_Addtime; }
            set { _Orders_Payment_Addtime = value; }
        }

        public string Orders_Payment_Site
        {
            get { return _Orders_Payment_Site; }
            set { _Orders_Payment_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

    }

}
