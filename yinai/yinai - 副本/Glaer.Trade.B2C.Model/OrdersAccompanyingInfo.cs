using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glaer.Trade.B2C.Model
{
    public class OrdersAccompanyingInfo
    {
        private int _Accompanying_ID;
        private int _Accompanying_OrdersID;
        private int _Accompanying_DeliveryID;
        private string _Accompanying_SN;
        private string _Accompanying_Name;
        private double _Accompanying_Amount;
        private string _Accompanying_Unit;
        private double _Accompanying_Price;
        private int _Accompanying_Status;
        private DateTime _Accompanying_Addtime;
        private string _Accompanying_Site;

        public int Accompanying_ID
        {
            get { return _Accompanying_ID; }
            set { _Accompanying_ID = value; }
        }

        public int Accompanying_OrdersID
        {
            get { return _Accompanying_OrdersID; }
            set { _Accompanying_OrdersID = value; }
        }

        public int Accompanying_DeliveryID
        {
            get { return _Accompanying_DeliveryID; }
            set { _Accompanying_DeliveryID = value; }
        }

        public string Accompanying_SN
        {
            get { return _Accompanying_SN; }
            set { _Accompanying_SN = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Accompanying_Name
        {
            get { return _Accompanying_Name; }
            set { _Accompanying_Name = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public double Accompanying_Amount
        {
            get { return _Accompanying_Amount; }
            set { _Accompanying_Amount = value; }
        }

        public string Accompanying_Unit
        {
            get { return _Accompanying_Unit; }
            set { _Accompanying_Unit = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public double Accompanying_Price
        {
            get { return _Accompanying_Price; }
            set { _Accompanying_Price = value; }
        }

        public int Accompanying_Status
        {
            get { return _Accompanying_Status; }
            set { _Accompanying_Status = value; }
        }

        public DateTime Accompanying_Addtime
        {
            get { return _Accompanying_Addtime; }
            set { _Accompanying_Addtime = value; }
        }

        public string Accompanying_Site
        {
            get { return _Accompanying_Site; }
            set { _Accompanying_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }
    }

    public class OrdersAccompanyingGoodsInfo
    {
        private int _Goods_ID;
        private int _Goods_GoodsID;
        private int _Goods_DeliveryID;
        private int _Goods_Amount;
        private int _Goods_AcceptAmount;

        public int Goods_ID
        {
            get { return _Goods_ID; }
            set { _Goods_ID = value; }
        }

        public int Goods_GoodsID
        {
            get { return _Goods_GoodsID; }
            set { _Goods_GoodsID = value; }
        }

        public int Goods_DeliveryID
        {
            get { return _Goods_DeliveryID; }
            set { _Goods_DeliveryID = value; }
        }

        public int Goods_Amount
        {
            get { return _Goods_Amount; }
            set { _Goods_Amount = value; }
        }

        public int Goods_AcceptAmount
        {
            get { return _Goods_AcceptAmount; }
            set { _Goods_AcceptAmount = value; }
        }
    }

    public class OrdersAccompanyingImgInfo
    {
        private int _Img_ID;
        private int _Img_AccompanyingID;
        private string _Img_Path;

        public int Img_ID
        {
            get { return _Img_ID; }
            set { _Img_ID = value; }
        }

        public int Img_AccompanyingID
        {
            get { return _Img_AccompanyingID; }
            set { _Img_AccompanyingID = value; }
        }

        public string Img_Path
        {
            get { return _Img_Path; }
            set { _Img_Path = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }
    }
}
