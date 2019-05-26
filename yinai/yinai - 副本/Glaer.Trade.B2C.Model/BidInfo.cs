using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glaer.Trade.B2C.Model
{
    /// <summary>
    /// 招标拍卖表
    /// </summary>
    public class BidInfo
    {
        private int _Bid_ID;
        private int _Bid_MemberID;
        private string _Bid_MemberCompany;
        private int _Bid_SupplierID;
        private string _Bid_SupplierCompany;
        private string _Bid_Title;
        private DateTime _Bid_EnterStartTime;
        private DateTime _Bid_EnterEndTime;
        private DateTime _Bid_BidStartTime;
        private DateTime _Bid_BidEndTime;
        private DateTime _Bid_AddTime;
        private double _Bid_Bond;
        private int _Bid_Number;
        private int _Bid_Status;
        private string _Bid_Content;
        private int _Bid_ProductType;
        private double _Bid_AllPrice;
        private int _Bid_Type;
        private string _Bid_Contract;
        private int _Bid_IsAudit;
        private DateTime _Bid_AuditTime;
        private string _Bid_AuditRemarks;
        private int _Bid_ExcludeSupplierID;
        private string _Bid_SN;
        private DateTime _Bid_DeliveryTime;
        private int _Bid_IsOrders;
        private DateTime _Bid_OrdersTime;
        private string _Bid_OrdersSN;
        private DateTime _Bid_FinishTime;
        private int _Bid_IsShow;

        private IList<BidProductInfo> _BidProducts;

        private IList<BidAttachmentsInfo> _BidAttachments;

        public IList<BidAttachmentsInfo> BidAttachments
        {
            get { return _BidAttachments; }
            set { _BidAttachments = value; }
        }
        public IList<BidProductInfo> BidProducts
        {
            get { return _BidProducts; }
            set { _BidProducts = value; }
        }
        public int Bid_ID
        {
            get { return _Bid_ID; }
            set { _Bid_ID = value; }
        }

        public int Bid_MemberID
        {
            get { return _Bid_MemberID; }
            set { _Bid_MemberID = value; }
        }

        public string Bid_MemberCompany
        {
            get { return _Bid_MemberCompany; }
            set { _Bid_MemberCompany = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int Bid_SupplierID
        {
            get { return _Bid_SupplierID; }
            set { _Bid_SupplierID = value; }
        }

        public string Bid_SupplierCompany
        {
            get { return _Bid_SupplierCompany; }
            set { _Bid_SupplierCompany = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Bid_Title
        {
            get { return _Bid_Title; }
            set { _Bid_Title = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public DateTime Bid_EnterStartTime
        {
            get { return _Bid_EnterStartTime; }
            set { _Bid_EnterStartTime = value; }
        }

        public DateTime Bid_EnterEndTime
        {
            get { return _Bid_EnterEndTime; }
            set { _Bid_EnterEndTime = value; }
        }

        public DateTime Bid_BidStartTime
        {
            get { return _Bid_BidStartTime; }
            set { _Bid_BidStartTime = value; }
        }

        public DateTime Bid_BidEndTime
        {
            get { return _Bid_BidEndTime; }
            set { _Bid_BidEndTime = value; }
        }

        public DateTime Bid_AddTime
        {
            get { return _Bid_AddTime; }
            set { _Bid_AddTime = value; }
        }

        public double Bid_Bond
        {
            get { return _Bid_Bond; }
            set { _Bid_Bond = value; }
        }

        public int Bid_Number
        {
            get { return _Bid_Number; }
            set { _Bid_Number = value; }
        }

        public int Bid_Status
        {
            get { return _Bid_Status; }
            set { _Bid_Status = value; }
        }

        public string Bid_Content
        {
            get { return _Bid_Content; }
            set { _Bid_Content = value; }
        }

        public int Bid_ProductType
        {
            get { return _Bid_ProductType; }
            set { _Bid_ProductType = value; }
        }

        public double Bid_AllPrice
        {
            get { return _Bid_AllPrice; }
            set { _Bid_AllPrice = value; }
        }

        public int Bid_Type
        {
            get { return _Bid_Type; }
            set { _Bid_Type = value; }
        }

        public string Bid_Contract
        {
            get { return _Bid_Contract; }
            set { _Bid_Contract = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int Bid_IsAudit
        {
            get { return _Bid_IsAudit; }
            set { _Bid_IsAudit = value; }
        }

        public DateTime Bid_AuditTime
        {
            get { return _Bid_AuditTime; }
            set { _Bid_AuditTime = value; }
        }

        public string Bid_AuditRemarks
        {
            get { return _Bid_AuditRemarks; }
            set { _Bid_AuditRemarks = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }

        public int Bid_ExcludeSupplierID
        {
            get { return _Bid_ExcludeSupplierID; }
            set { _Bid_ExcludeSupplierID = value; }
        }

        public string Bid_SN
        {
            get { return _Bid_SN; }
            set { _Bid_SN = value.Length > 20 ? value.Substring(0, 20) : value.ToString(); }
        }

        public DateTime Bid_DeliveryTime
        {
            get { return _Bid_DeliveryTime; }
            set { _Bid_DeliveryTime = value; }
        }

        public int Bid_IsOrders
        {
            get { return _Bid_IsOrders; }
            set { _Bid_IsOrders = value; }
        }

        public DateTime Bid_OrdersTime
        {
            get { return _Bid_OrdersTime; }
            set { _Bid_OrdersTime = value; }
        }

        public string Bid_OrdersSN
        {
            get { return _Bid_OrdersSN; }
            set { _Bid_OrdersSN = value.Length > 20 ? value.Substring(0, 20) : value.ToString(); }
        }

        public DateTime Bid_FinishTime
        {
            get { return _Bid_FinishTime; }
            set { _Bid_FinishTime = value; }
        }
        public int Bid_IsShow
        {
            get { return _Bid_IsShow; }
            set { _Bid_IsShow = value; }
        }
    }


    public class BidProductInfo
    {
        private int _Bid_Product_ID;
        private int _Bid_BidID;
        private int _Bid_Product_Sort;
        private string _Bid_Product_Code;
        private string _Bid_Product_Name;
        private string _Bid_Product_Spec;
        private string _Bid_Product_Brand;
        private string _Bid_Product_Unit;
        private int _Bid_Product_Amount;
        private string _Bid_Product_Delivery;
        private string _Bid_Product_Remark;
        private double _Bid_Product_StartPrice;
        private string _Bid_Product_Img;
        private int _Bid_Product_ProductID;

        public int Bid_Product_ID
        {
            get { return _Bid_Product_ID; }
            set { _Bid_Product_ID = value; }
        }

        public int Bid_BidID
        {
            get { return _Bid_BidID; }
            set { _Bid_BidID = value; }
        }

        public int Bid_Product_Sort
        {
            get { return _Bid_Product_Sort; }
            set { _Bid_Product_Sort = value; }
        }

        public string Bid_Product_Code
        {
            get { return _Bid_Product_Code; }
            set { _Bid_Product_Code = value.Length > 20 ? value.Substring(0, 20) : value.ToString(); }
        }

        public string Bid_Product_Name
        {
            get { return _Bid_Product_Name; }
            set { _Bid_Product_Name = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Bid_Product_Spec
        {
            get { return _Bid_Product_Spec; }
            set { _Bid_Product_Spec = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Bid_Product_Brand
        {
            get { return _Bid_Product_Brand; }
            set { _Bid_Product_Brand = value.Length > 20 ? value.Substring(0, 20) : value.ToString(); }
        }

        public string Bid_Product_Unit
        {
            get { return _Bid_Product_Unit; }
            set { _Bid_Product_Unit = value.Length > 20 ? value.Substring(0, 20) : value.ToString(); }
        }

        public int Bid_Product_Amount
        {
            get { return _Bid_Product_Amount; }
            set { _Bid_Product_Amount = value; }
        }

        public string Bid_Product_Delivery
        {
            get { return _Bid_Product_Delivery; }
            set { _Bid_Product_Delivery = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Bid_Product_Remark
        {
            get { return _Bid_Product_Remark; }
            set { _Bid_Product_Remark = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }
        public double Bid_Product_StartPrice
        {
            get { return _Bid_Product_StartPrice; }
            set { _Bid_Product_StartPrice = value; }
        }

        public string Bid_Product_Img
        {
            get { return _Bid_Product_Img; }
            set { _Bid_Product_Img = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }
        public int Bid_Product_ProductID
        {
            get { return _Bid_Product_ProductID; }
            set { _Bid_Product_ProductID = value; }
        } 

    }

    public class BidAttachmentsInfo
    {
        private int _Bid_Attachments_ID;
        private int _Bid_Attachments_Sort;
        private string _Bid_Attachments_Name;
        private string _Bid_Attachments_format;
        private string _Bid_Attachments_Size;
        private string _Bid_Attachments_Remarks;
        private string _Bid_Attachments_Path;
        private int _Bid_Attachments_BidID;

        public int Bid_Attachments_ID
        {
            get { return _Bid_Attachments_ID; }
            set { _Bid_Attachments_ID = value; }
        }

        public int Bid_Attachments_Sort
        {
            get { return _Bid_Attachments_Sort; }
            set { _Bid_Attachments_Sort = value; }
        }

        public string Bid_Attachments_Name
        {
            get { return _Bid_Attachments_Name; }
            set { _Bid_Attachments_Name = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Bid_Attachments_format
        {
            get { return _Bid_Attachments_format; }
            set { _Bid_Attachments_format = value.Length > 20 ? value.Substring(0, 20) : value.ToString(); }
        }

        public string Bid_Attachments_Size
        {
            get { return _Bid_Attachments_Size; }
            set { _Bid_Attachments_Size = value.Length > 20 ? value.Substring(0, 20) : value.ToString(); }
        }

        public string Bid_Attachments_Remarks
        {
            get { return _Bid_Attachments_Remarks; }
            set { _Bid_Attachments_Remarks = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Bid_Attachments_Path
        {
            get { return _Bid_Attachments_Path; }
            set { _Bid_Attachments_Path = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int Bid_Attachments_BidID
        {
            get { return _Bid_Attachments_BidID; }
            set { _Bid_Attachments_BidID = value; }
        }

    }

    public class BidEnterInfo
    {
        private int _Bid_Enter_ID;
        private int _Bid_Enter_BidID;
        private int _Bid_Enter_SupplierID;
        private double _Bid_Enter_Bond;
        private int _Bid_Enter_Type;
        private int _Bid_Enter_IsShow;

        public int Bid_Enter_Type
        {
            get { return _Bid_Enter_Type; }
            set { _Bid_Enter_Type = value; }
        }
        public int Bid_Enter_ID
        {
            get { return _Bid_Enter_ID; }
            set { _Bid_Enter_ID = value; }
        }

        public int Bid_Enter_BidID
        {
            get { return _Bid_Enter_BidID; }
            set { _Bid_Enter_BidID = value; }
        }

        public int Bid_Enter_SupplierID
        {
            get { return _Bid_Enter_SupplierID; }
            set { _Bid_Enter_SupplierID = value; }
        }

        public double Bid_Enter_Bond
        {
            get { return _Bid_Enter_Bond; }
            set { _Bid_Enter_Bond = value; }
        }


        public int Bid_Enter_IsShow
        {
            get { return _Bid_Enter_IsShow; }
            set { _Bid_Enter_IsShow = value; }
        }

    }

    public class TenderInfo
    {
        private int _Tender_ID;
        private int _Tender_SupplierID;
        private int _Tender_BidID;
        private DateTime _Tender_Addtime;
        private int _Tender_IsWin;
        private int _Tender_Status;
        private double _Tender_AllPrice;
        private int _Tender_IsRefund;
        private string _Tender_SN;
        private int _Tender_IsProduct;
        private int _Tender_IsShow;
        private string _Tender_ANote;
        private string _Tender_BNote;


        private IList<TenderProductInfo> _TenderProducts;

        private string _BidMemberCompany;
        private string _Bid_Title;
        
        public IList<TenderProductInfo> TenderProducts
        {
            get { return _TenderProducts; }
            set { _TenderProducts = value; }
        }
        public int Tender_ID
        {
            get { return _Tender_ID; }
            set { _Tender_ID = value; }
        }

        public int Tender_SupplierID
        {
            get { return _Tender_SupplierID; }
            set { _Tender_SupplierID = value; }
        }

        public int Tender_BidID
        {
            get { return _Tender_BidID; }
            set { _Tender_BidID = value; }
        }

        public DateTime Tender_Addtime
        {
            get { return _Tender_Addtime; }
            set { _Tender_Addtime = value; }
        }

        public int Tender_IsWin
        {
            get { return _Tender_IsWin; }
            set { _Tender_IsWin = value; }
        }

        public int Tender_Status
        {
            get { return _Tender_Status; }
            set { _Tender_Status = value; }
        }

        public double Tender_AllPrice
        {
            get { return _Tender_AllPrice; }
            set { _Tender_AllPrice = value; }
        }

        public int Tender_IsRefund
        {
            get { return _Tender_IsRefund; }
            set { _Tender_IsRefund = value; }
        }

        public string Tender_SN
        {
            get { return _Tender_SN; }
            set { _Tender_SN = value.Length > 20 ? value.Substring(0, 20) : value.ToString(); }
        }

        public int Tender_IsProduct
        {
            get { return _Tender_IsProduct; }
            set { _Tender_IsProduct = value; }
        } 

        public string BidMemberCompany
        {
            get { return _BidMemberCompany; }
            set { _BidMemberCompany = value; }
        }


        public string Bid_Title
        {
            get { return _Bid_Title; }
            set { _Bid_Title = value; }
        }

        public int Tender_IsShow
        {
            get { return _Tender_IsShow; }
            set { _Tender_IsShow = value; }
        }
        public string Tender_ANote
        {
            get { return _Tender_ANote; }
            set { _Tender_ANote = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }
        public string Tender_BNote
        {
            get { return _Tender_BNote; }
            set { _Tender_BNote = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }
    }

    public class TenderProductInfo
    {
        private int _Tender_Product_ID;
        private int _Tender_Product_ProductID;
        private int _Tender_TenderID;
        private int _Tender_Product_BidProductID;
        private string _Tender_Product_Name;
        private double _Tender_Price;

        public int Tender_Product_ID
        {
            get { return _Tender_Product_ID; }
            set { _Tender_Product_ID = value; }
        }

        public int Tender_Product_ProductID
        {
            get { return _Tender_Product_ProductID; }
            set { _Tender_Product_ProductID = value; }
        }

        public int Tender_TenderID
        {
            get { return _Tender_TenderID; }
            set { _Tender_TenderID = value; }
        }

        public int Tender_Product_BidProductID
        {
            get { return _Tender_Product_BidProductID; }
            set { _Tender_Product_BidProductID = value; }
        }

        public string Tender_Product_Name
        {
            get { return _Tender_Product_Name; }
            set { _Tender_Product_Name = value.Length > 500 ? value.Substring(0, 500) : value.ToString(); }
        }

        public double Tender_Price
        {
            get { return _Tender_Price; }
            set { _Tender_Price = value; }
        }

    }
}
