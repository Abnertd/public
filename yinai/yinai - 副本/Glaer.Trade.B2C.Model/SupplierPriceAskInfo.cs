using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glaer.Trade.B2C.Model
{
    public class SupplierPriceAskInfo
    {
        private int _PriceAsk_ID;
        private int _PriceAsk_ProductID;
        private int _PriceAsk_MemberID;
        private string _PriceAsk_Title;
        private string _PriceAsk_Name;
        private string _PriceAsk_Phone;
        private int _PriceAsk_Quantity;
        private double _PriceAsk_Price;
        private DateTime _PriceAsk_DeliveryDate;
        private string _PriceAsk_Content;
        private DateTime _PriceAsk_AddTime;
        private string _PriceAsk_ReplyContent;
        private DateTime _PriceAsk_ReplyTime;
        private int _PriceAsk_IsReply;

        public int PriceAsk_ID
        {
            get { return _PriceAsk_ID; }
            set { _PriceAsk_ID = value; }
        }

        public int PriceAsk_ProductID
        {
            get { return _PriceAsk_ProductID; }
            set { _PriceAsk_ProductID = value; }
        }

        public int PriceAsk_MemberID
        {
            get { return _PriceAsk_MemberID; }
            set { _PriceAsk_MemberID = value; }
        }

        public string PriceAsk_Title
        {
            get { return _PriceAsk_Title; }
            set { _PriceAsk_Title = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string PriceAsk_Name
        {
            get { return _PriceAsk_Name; }
            set { _PriceAsk_Name = value.Length > 20 ? value.Substring(0, 20) : value.ToString(); }
        }

        public string PriceAsk_Phone
        {
            get { return _PriceAsk_Phone; }
            set { _PriceAsk_Phone = value.Length > 20 ? value.Substring(0, 20) : value.ToString(); }
        }

        public int PriceAsk_Quantity
        {
            get { return _PriceAsk_Quantity; }
            set { _PriceAsk_Quantity = value; }
        }

        public double PriceAsk_Price
        {
            get { return _PriceAsk_Price; }
            set { _PriceAsk_Price = value; }
        }

        public DateTime PriceAsk_DeliveryDate
        {
            get { return _PriceAsk_DeliveryDate; }
            set { _PriceAsk_DeliveryDate = value; }
        }

        public string PriceAsk_Content
        {
            get { return _PriceAsk_Content; }
            set { _PriceAsk_Content = value.Length > 500 ? value.Substring(0, 500) : value.ToString(); }
        }

        public DateTime PriceAsk_AddTime
        {
            get { return _PriceAsk_AddTime; }
            set { _PriceAsk_AddTime = value; }
        }

        public string PriceAsk_ReplyContent
        {
            get { return _PriceAsk_ReplyContent; }
            set { _PriceAsk_ReplyContent = value.Length > 500 ? value.Substring(0, 500) : value.ToString(); }
        }

        public DateTime PriceAsk_ReplyTime
        {
            get { return _PriceAsk_ReplyTime; }
            set { _PriceAsk_ReplyTime = value; }
        }

        public int PriceAsk_IsReply
        {
            get { return _PriceAsk_IsReply; }
            set { _PriceAsk_IsReply = value; }
        }

    }
}
