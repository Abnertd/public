using System;

namespace Glaer.Trade.B2C.Model
{
    public class SupplierAccountLogInfo
    {
        private int _Account_Log_ID;
        private int _Account_Log_Type;
        private int _Account_Log_SupplierID;
        private double _Account_Log_Amount;
        private double _Account_Log_AmountRemain;
        private string _Account_Log_Note;
        private DateTime _Account_Log_Addtime;

        public int Account_Log_ID
        {
            get { return _Account_Log_ID; }
            set { _Account_Log_ID = value; }
        }

        public int Account_Log_Type
        {
            get { return _Account_Log_Type; }
            set { _Account_Log_Type = value; }
        }

        public int Account_Log_SupplierID
        {
            get { return _Account_Log_SupplierID; }
            set { _Account_Log_SupplierID = value; }
        }

        public double Account_Log_Amount
        {
            get { return _Account_Log_Amount; }
            set { _Account_Log_Amount = value; }
        }

        public double Account_Log_AmountRemain
        {
            get { return _Account_Log_AmountRemain; }
            set { _Account_Log_AmountRemain = value; }
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

    }
}
