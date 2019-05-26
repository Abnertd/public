using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glaer.Trade.B2C.Model
{
    public class SupplierPriceReportInfo
    {
        private int _PriceReport_ID;
        private int _PriceReport_PurchaseID;
        private int _PriceReport_MemberID;
        private string _PriceReport_Title;
        private string _PriceReport_Name;
        private string _PriceReport_Phone;
        private DateTime _PriceReport_DeliveryDate;
        private DateTime _PriceReport_AddTime;
        private string _PriceReport_ReplyContent;
        private DateTime _PriceReport_ReplyTime;
        private int _PriceReport_IsReply;
        private int _PriceReport_AuditStatus;
        private string _PriceReport_Note;

        public int PriceReport_ID
        {
            get { return _PriceReport_ID; }
            set { _PriceReport_ID = value; }
        }

        public int PriceReport_PurchaseID
        {
            get { return _PriceReport_PurchaseID; }
            set { _PriceReport_PurchaseID = value; }
        }

        public int PriceReport_MemberID
        {
            get { return _PriceReport_MemberID; }
            set { _PriceReport_MemberID = value; }
        }

        public string PriceReport_Title
        {
            get { return _PriceReport_Title; }
            set { _PriceReport_Title = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string PriceReport_Name
        {
            get { return _PriceReport_Name; }
            set { _PriceReport_Name = value.Length > 20 ? value.Substring(0, 20) : value.ToString(); }
        }

        public string PriceReport_Phone
        {
            get { return _PriceReport_Phone; }
            set { _PriceReport_Phone = value.Length > 20 ? value.Substring(0, 20) : value.ToString(); }
        }

        public DateTime PriceReport_DeliveryDate
        {
            get { return _PriceReport_DeliveryDate; }
            set { _PriceReport_DeliveryDate = value; }
        }

        public DateTime PriceReport_AddTime
        {
            get { return _PriceReport_AddTime; }
            set { _PriceReport_AddTime = value; }
        }

        public string PriceReport_ReplyContent
        {
            get { return _PriceReport_ReplyContent; }
            set { _PriceReport_ReplyContent = value.Length > 500 ? value.Substring(0, 500) : value.ToString(); }
        }

        public DateTime PriceReport_ReplyTime
        {
            get { return _PriceReport_ReplyTime; }
            set { _PriceReport_ReplyTime = value; }
        }

        public int PriceReport_IsReply
        {
            get { return _PriceReport_IsReply; }
            set { _PriceReport_IsReply = value; }
        }

        public int PriceReport_AuditStatus
        {
            get { return _PriceReport_AuditStatus; }
            set { _PriceReport_AuditStatus = value; }
        }

        public string PriceReport_Note
        {
            get { return _PriceReport_Note; }
            set { _PriceReport_Note = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }
    }

    public class SupplierPriceReportDetailInfo
    {
        private int _Detail_ID;
        private int _Detail_PriceReportID;
        private int _Detail_PurchaseID;
        private int _Detail_PurchaseDetailID;
        private int _Detail_Amount;
        private double _Detail_Price;

        public int Detail_ID
        {
            get { return _Detail_ID; }
            set { _Detail_ID = value; }
        }

        public int Detail_PriceReportID
        {
            get { return _Detail_PriceReportID; }
            set { _Detail_PriceReportID = value; }
        }

        public int Detail_PurchaseID
        {
            get { return _Detail_PurchaseID; }
            set { _Detail_PurchaseID = value; }
        }

        public int Detail_PurchaseDetailID
        {
            get { return _Detail_PurchaseDetailID; }
            set { _Detail_PurchaseDetailID = value; }
        }

        public int Detail_Amount
        {
            get { return _Detail_Amount; }
            set { _Detail_Amount = value; }
        }

        public double Detail_Price
        {
            get { return _Detail_Price; }
            set { _Detail_Price = value; }
        }

    }
}
