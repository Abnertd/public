using System;

namespace Glaer.Trade.B2C.Model
{
    public class OrdersDeliveryInfo
    {
        private int _Orders_Delivery_ID;
        private int _Orders_Delivery_OrdersID;
        private int _Orders_Delivery_DeliveryStatus;
        private int _Orders_Delivery_SysUserID;
        private string _Orders_Delivery_DocNo;
        private string _Orders_Delivery_Name;
        private string _Orders_Delivery_companyName;
        private string _Orders_Delivery_Code;
        private int _Orders_Delivery_Status = 0;
        private double _Orders_Delivery_Amount;
        private string _Orders_Delivery_Note;
        private int _Orders_Delivery_ReceiveStatus;
        private DateTime _Orders_Delivery_Addtime;
        private string _Orders_Delivery_Site;

        //新加字段三个  司机电话/车牌号/运输方式/
        private string _Orders_Delivery_DriverMobile;
        private string _Orders_Delivery_PlateNum;
        private string _Orders_Delivery_TransportType;

        public int Orders_Delivery_ID
        {
            get { return _Orders_Delivery_ID; }
            set { _Orders_Delivery_ID = value; }
        }

        public int Orders_Delivery_OrdersID
        {
            get { return _Orders_Delivery_OrdersID; }
            set { _Orders_Delivery_OrdersID = value; }
        }

        public int Orders_Delivery_DeliveryStatus
        {
            get { return _Orders_Delivery_DeliveryStatus; }
            set { _Orders_Delivery_DeliveryStatus = value; }
        }

        public int Orders_Delivery_SysUserID
        {
            get { return _Orders_Delivery_SysUserID; }
            set { _Orders_Delivery_SysUserID = value; }
        }

        public string Orders_Delivery_DocNo
        {
            get { return _Orders_Delivery_DocNo; }
            set { _Orders_Delivery_DocNo = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Orders_Delivery_Name
        {
            get { return _Orders_Delivery_Name; }
            set { _Orders_Delivery_Name = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Orders_Delivery_companyName
        {
            get { return _Orders_Delivery_companyName; }
            set { _Orders_Delivery_companyName = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Orders_Delivery_Code
        {
            get { return _Orders_Delivery_Code; }
            set { _Orders_Delivery_Code = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public int Orders_Delivery_Status
        {
            get { return _Orders_Delivery_Status; }
            set { _Orders_Delivery_Status = value; }
        }

        public double Orders_Delivery_Amount
        {
            get { return _Orders_Delivery_Amount; }
            set { _Orders_Delivery_Amount = value; }
        }

        public string Orders_Delivery_Note
        {
            get { return _Orders_Delivery_Note; }
            set { _Orders_Delivery_Note = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }

        public int Orders_Delivery_ReceiveStatus
        {
            get { return _Orders_Delivery_ReceiveStatus; }
            set { _Orders_Delivery_ReceiveStatus = value; }
        }

        public DateTime Orders_Delivery_Addtime
        {
            get { return _Orders_Delivery_Addtime; }
            set { _Orders_Delivery_Addtime = value; }
        }

        public string Orders_Delivery_Site
        {
            get { return _Orders_Delivery_Site; }
            set { _Orders_Delivery_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }  
        //private string _Orders_Delivery_DriverMobile;
        //private string _Orders_Delivery_PlateNum;
        //private string _Orders_Delivery_TransportType;

        #region 运输单详情信息新加字段 
        //司机手机号码
        public string Orders_Delivery_DriverMobile
        {
            get { return _Orders_Delivery_DriverMobile; }
            set { _Orders_Delivery_DriverMobile = value.Length > 30 ? value.Substring(0, 30) : value.ToString(); }
        }


        //车牌号
        public string Orders_Delivery_PlateNum
        {
            get { return _Orders_Delivery_PlateNum; }
            set { _Orders_Delivery_PlateNum = value.Length > 30 ? value.Substring(0, 30) : value.ToString(); }
        }

        //运输方式
        public string Orders_Delivery_TransportType
        {
            get { return _Orders_Delivery_TransportType; }
            set { _Orders_Delivery_TransportType = value.Length > 30 ? value.Substring(0, 30) : value.ToString(); }
        }
        #endregion 

    }

    public class OrdersDeliveryGoodsInfo
    {
        private int _Orders_Delivery_Goods_ID;
        private int _Orders_Delivery_Goods_DeliveryID;
        private int _Orders_Delivery_Goods_GoodsID;
        private int _Orders_Delivery_Goods_ProductID;
        private int _Orders_Delivery_Goods_ProductCateID;
        private string _Orders_Delivery_Goods_ProductCode;
        private string _Orders_Delivery_Goods_ProductName;
        private string _Orders_Delivery_Goods_ProductImg;
        private string _Orders_Delivery_Goods_ProductSpec;
        private double _Orders_Delivery_Goods_ProductPrice;
        private int _Orders_Delivery_Goods_ProductAmount;
        private int _Orders_Delivery_Goods_ReceivedAmount;
        private double _Orders_Delivery_Goods_brokerage;

        public int Orders_Delivery_Goods_ID
        {
            get { return _Orders_Delivery_Goods_ID; }
            set { _Orders_Delivery_Goods_ID = value; }
        }

        public int Orders_Delivery_Goods_DeliveryID
        {
            get { return _Orders_Delivery_Goods_DeliveryID; }
            set { _Orders_Delivery_Goods_DeliveryID = value; }
        }

        public int Orders_Delivery_Goods_GoodsID
        {
            get { return _Orders_Delivery_Goods_GoodsID; }
            set { _Orders_Delivery_Goods_GoodsID = value; }
        }

        public int Orders_Delivery_Goods_ProductID
        {
            get { return _Orders_Delivery_Goods_ProductID; }
            set { _Orders_Delivery_Goods_ProductID = value; }
        }

        public int Orders_Delivery_Goods_ProductCateID
        {
            get { return _Orders_Delivery_Goods_ProductCateID; }
            set { _Orders_Delivery_Goods_ProductCateID = value; }
        }

        public string Orders_Delivery_Goods_ProductCode
        {
            get { return _Orders_Delivery_Goods_ProductCode; }
            set { _Orders_Delivery_Goods_ProductCode = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Orders_Delivery_Goods_ProductName
        {
            get { return _Orders_Delivery_Goods_ProductName; }
            set { _Orders_Delivery_Goods_ProductName = value.Length > 500 ? value.Substring(0, 500) : value.ToString(); }
        }

        public string Orders_Delivery_Goods_ProductImg
        {
            get { return _Orders_Delivery_Goods_ProductImg; }
            set { _Orders_Delivery_Goods_ProductImg = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Orders_Delivery_Goods_ProductSpec
        {
            get { return _Orders_Delivery_Goods_ProductSpec; }
            set { _Orders_Delivery_Goods_ProductSpec = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public double Orders_Delivery_Goods_ProductPrice
        {
            get { return _Orders_Delivery_Goods_ProductPrice; }
            set { _Orders_Delivery_Goods_ProductPrice = value; }
        }

        public int Orders_Delivery_Goods_ProductAmount
        {
            get { return _Orders_Delivery_Goods_ProductAmount; }
            set { _Orders_Delivery_Goods_ProductAmount = value; }
        }

        public int Orders_Delivery_Goods_ReceivedAmount
        {
            get { return _Orders_Delivery_Goods_ReceivedAmount; }
            set { _Orders_Delivery_Goods_ReceivedAmount = value; }
        }

        public double Orders_Delivery_Goods_brokerage
        {
            get { return _Orders_Delivery_Goods_brokerage; }
            set { _Orders_Delivery_Goods_brokerage = value; }
        }

    }
}
