using System;

namespace Glaer.Trade.B2C.Model
{
    public class SupplierPayBackApplyInfo
    {
        private int _Supplier_PayBack_Apply_ID;
        private int _Supplier_PayBack_Apply_SupplierID;
        private int _Supplier_PayBack_Apply_Type;
        private double _Supplier_PayBack_Apply_Amount;
        private string _Supplier_PayBack_Apply_Note;
        private DateTime _Supplier_PayBack_Apply_Addtime;
        private int _Supplier_PayBack_Apply_Status;
        private double _Supplier_PayBack_Apply_AdminAmount;
        private string _Supplier_PayBack_Apply_AdminNote;
        private DateTime _Supplier_PayBack_Apply_AdminTime;
        private string _Supplier_PayBack_Apply_Site;

        public int Supplier_PayBack_Apply_ID
        {
            get { return _Supplier_PayBack_Apply_ID; }
            set { _Supplier_PayBack_Apply_ID = value; }
        }

        public int Supplier_PayBack_Apply_SupplierID
        {
            get { return _Supplier_PayBack_Apply_SupplierID; }
            set { _Supplier_PayBack_Apply_SupplierID = value; }
        }

        public int Supplier_PayBack_Apply_Type
        {
            get { return _Supplier_PayBack_Apply_Type; }
            set { _Supplier_PayBack_Apply_Type = value; }
        }

        public double Supplier_PayBack_Apply_Amount
        {
            get { return _Supplier_PayBack_Apply_Amount; }
            set { _Supplier_PayBack_Apply_Amount = value; }
        }

        public string Supplier_PayBack_Apply_Note
        {
            get { return _Supplier_PayBack_Apply_Note; }
            set { _Supplier_PayBack_Apply_Note = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }

        public DateTime Supplier_PayBack_Apply_Addtime
        {
            get { return _Supplier_PayBack_Apply_Addtime; }
            set { _Supplier_PayBack_Apply_Addtime = value; }
        }

        public int Supplier_PayBack_Apply_Status
        {
            get { return _Supplier_PayBack_Apply_Status; }
            set { _Supplier_PayBack_Apply_Status = value; }
        }

        public double Supplier_PayBack_Apply_AdminAmount
        {
            get { return _Supplier_PayBack_Apply_AdminAmount; }
            set { _Supplier_PayBack_Apply_AdminAmount = value; }
        }

        public string Supplier_PayBack_Apply_AdminNote
        {
            get { return _Supplier_PayBack_Apply_AdminNote; }
            set { _Supplier_PayBack_Apply_AdminNote = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }

        public DateTime Supplier_PayBack_Apply_AdminTime
        {
            get { return _Supplier_PayBack_Apply_AdminTime; }
            set { _Supplier_PayBack_Apply_AdminTime = value; }
        }

        public string Supplier_PayBack_Apply_Site
        {
            get { return _Supplier_PayBack_Apply_Site; }
            set { _Supplier_PayBack_Apply_Site = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }

    }
}
