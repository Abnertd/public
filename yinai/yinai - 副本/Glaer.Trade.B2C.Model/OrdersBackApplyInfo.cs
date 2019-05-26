using System;
using System.Collections.Generic;
using System.Text;

namespace Glaer.Trade.B2C.Model
{
    public class OrdersBackApplyInfo
    {
        private int _Orders_BackApply_ID;
        private string _Orders_BackApply_OrdersCode;
        private int _Orders_BackApply_MemberID;
        private string _Orders_BackApply_Name;
        private int _Orders_BackApply_Type;
        private int _Orders_BackApply_DeliveryWay = 0;
        private int _Orders_BackApply_AmountBackType = 0;
        private double _Orders_BackApply_Amount;
        private string _Orders_BackApply_Note;
        private string _Orders_BackApply_Account;
        private int _Orders_BackApply_Status;
        private string _Orders_BackApply_AdminNote;
        private string _Orders_BackApply_SupplierNote;
        private DateTime _Orders_BackApply_SupplierTime;
        private DateTime _Orders_BackApply_AdminTime;
        private DateTime _Orders_BackApply_Addtime;
        private string _Orders_BackApply_Site;

        public int Orders_BackApply_ID
        {
            get { return _Orders_BackApply_ID; }
            set { _Orders_BackApply_ID = value; }
        }

        public string Orders_BackApply_OrdersCode
        {
            get { return _Orders_BackApply_OrdersCode; }
            set { _Orders_BackApply_OrdersCode = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int Orders_BackApply_MemberID
        {
            get { return _Orders_BackApply_MemberID; }
            set { _Orders_BackApply_MemberID = value; }
        }

        public string Orders_BackApply_Name
        {
            get { return _Orders_BackApply_Name; }
            set { _Orders_BackApply_Name = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int Orders_BackApply_Type
        {
            get { return _Orders_BackApply_Type; }
            set { _Orders_BackApply_Type = value; }
        }

        public int Orders_BackApply_DeliveryWay
        {
            get { return _Orders_BackApply_DeliveryWay; }
            set { _Orders_BackApply_DeliveryWay = value; }
        }

        public int Orders_BackApply_AmountBackType
        {
            get { return _Orders_BackApply_AmountBackType; }
            set { _Orders_BackApply_AmountBackType = value; }
        }

        public double Orders_BackApply_Amount
        {
            get { return _Orders_BackApply_Amount; }
            set { _Orders_BackApply_Amount = value; }
        }

        public string Orders_BackApply_Note
        {
            get { return _Orders_BackApply_Note; }
            set { _Orders_BackApply_Note = value.Length > 4000 ? value.Substring(0, 4000) : value.ToString(); }
        }

        public string Orders_BackApply_Account
        {
            get { return _Orders_BackApply_Account; }
            set { _Orders_BackApply_Account = value.Length > 300 ? value.Substring(0, 300) : value.ToString(); }
        }

        public string Orders_BackApply_SupplierNote
        {
            get { return _Orders_BackApply_SupplierNote; }
            set { _Orders_BackApply_SupplierNote = value.Length > 300 ? value.Substring(0, 300) : value.ToString(); }
        }

        public string Orders_BackApply_AdminNote
        {
            get { return _Orders_BackApply_AdminNote; }
            set { _Orders_BackApply_AdminNote = value.Length > 300 ? value.Substring(0, 300) : value.ToString(); }
        }

        public DateTime Orders_BackApply_SupplierTime
        {
            get { return _Orders_BackApply_SupplierTime; }
            set { _Orders_BackApply_SupplierTime = value; }
        }

        public DateTime Orders_BackApply_AdminTime
        {
            get { return _Orders_BackApply_AdminTime; }
            set { _Orders_BackApply_AdminTime = value; }
        }

        public int Orders_BackApply_Status
        {
            get { return _Orders_BackApply_Status; }
            set { _Orders_BackApply_Status = value; }
        }

        public DateTime Orders_BackApply_Addtime
        {
            get { return _Orders_BackApply_Addtime; }
            set { _Orders_BackApply_Addtime = value; }
        }

        public string Orders_BackApply_Site
        {
            get { return _Orders_BackApply_Site; }
            set { _Orders_BackApply_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }
    }
}
