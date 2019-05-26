using System;

namespace Glaer.Trade.B2C.Model
{
    public class ContractDeliveryInfo
    {
        private int _Contract_Delivery_ID;
        private int _Contract_Delivery_ContractID;
        private int _Contract_Delivery_DeliveryStatus;
        private int _Contract_Delivery_SysUserID;
        private string _Contract_Delivery_DocNo;
        private string _Contract_Delivery_Name;
        private string _Contract_Delivery_CompanyName;
        private string _Contract_Delivery_Code;
        private double _Contract_Delivery_Amount;
        private string _Contract_Delivery_Note;
        private string _Contract_Delivery_AccpetNote;
        private DateTime _Contract_Delivery_Addtime;
        private string _Contract_Delivery_Site;

        public int Contract_Delivery_ID
        {
            get { return _Contract_Delivery_ID; }
            set { _Contract_Delivery_ID = value; }
        }

        public int Contract_Delivery_ContractID
        {
            get { return _Contract_Delivery_ContractID; }
            set { _Contract_Delivery_ContractID = value; }
        }

        public int Contract_Delivery_DeliveryStatus
        {
            get { return _Contract_Delivery_DeliveryStatus; }
            set { _Contract_Delivery_DeliveryStatus = value; }
        }

        public int Contract_Delivery_SysUserID
        {
            get { return _Contract_Delivery_SysUserID; }
            set { _Contract_Delivery_SysUserID = value; }
        }

        public string Contract_Delivery_DocNo
        {
            get { return _Contract_Delivery_DocNo; }
            set { _Contract_Delivery_DocNo = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Contract_Delivery_Name
        {
            get { return _Contract_Delivery_Name; }
            set { _Contract_Delivery_Name = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Contract_Delivery_CompanyName
        {
            get { return _Contract_Delivery_CompanyName; }
            set { _Contract_Delivery_CompanyName = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Contract_Delivery_Code
        {
            get { return _Contract_Delivery_Code; }
            set { _Contract_Delivery_Code = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public double Contract_Delivery_Amount
        {
            get { return _Contract_Delivery_Amount; }
            set { _Contract_Delivery_Amount = value; }
        }

        public string Contract_Delivery_Note
        {
            get { return _Contract_Delivery_Note; }
            set { _Contract_Delivery_Note = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }

        public string Contract_Delivery_AccpetNote
        {
            get { return _Contract_Delivery_AccpetNote; }
            set { _Contract_Delivery_AccpetNote = value.Length > 500 ? value.Substring(0, 500) : value.ToString(); }
        }

        public DateTime Contract_Delivery_Addtime
        {
            get { return _Contract_Delivery_Addtime; }
            set { _Contract_Delivery_Addtime = value; }
        }

        public string Contract_Delivery_Site
        {
            get { return _Contract_Delivery_Site; }
            set { _Contract_Delivery_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

    }

    public class ContractDeliveryGoodsInfo
    {
        private int _Delivery_Goods_ID;
        private int _Delivery_Goods_GoodsID;
        private int _Delivery_Goods_DeliveryID;
        private int _Delivery_Goods_Amount;
        private int _Delivery_Goods_Status;
        private int _Delivery_Goods_AcceptAmount;
        private string _Delivery_Goods_Unit;

        public int Delivery_Goods_ID
        {
            get { return _Delivery_Goods_ID; }
            set { _Delivery_Goods_ID = value; }
        }

        public int Delivery_Goods_GoodsID
        {
            get { return _Delivery_Goods_GoodsID; }
            set { _Delivery_Goods_GoodsID = value; }
        }

        public int Delivery_Goods_DeliveryID
        {
            get { return _Delivery_Goods_DeliveryID; }
            set { _Delivery_Goods_DeliveryID = value; }
        }

        public int Delivery_Goods_Amount
        {
            get { return _Delivery_Goods_Amount; }
            set { _Delivery_Goods_Amount = value; }
        }

        public int Delivery_Goods_Status
        {
            get { return _Delivery_Goods_Status; }
            set { _Delivery_Goods_Status = value; }
        }

        public int Delivery_Goods_AcceptAmount
        {
            get { return _Delivery_Goods_AcceptAmount; }
            set { _Delivery_Goods_AcceptAmount = value; }
        }

        public string Delivery_Goods_Unit
        {
            get { return _Delivery_Goods_Unit; }
            set { _Delivery_Goods_Unit = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

    }
}
