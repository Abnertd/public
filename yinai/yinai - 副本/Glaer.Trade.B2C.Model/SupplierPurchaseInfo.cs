using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glaer.Trade.B2C.Model
{
    public class SupplierPurchaseInfo
    {
        private int _Purchase_ID;
        private int _Purchase_TypeID;
        private int _Purchase_SupplierID;
        private string _Purchase_Title;
        private DateTime _Purchase_DeliveryTime;
        private string _Purchase_State;
        private string _Purchase_City;
        private string _Purchase_County;
        private string _Purchase_Address;
        private string _Purchase_Intro;
        private DateTime _Purchase_Addtime;
        private int _Purchase_Status;
        private int _Purchase_IsActive;
        private string _Purchase_ActiveReason;
        private int _Purchase_Trash;
        private DateTime _Purchase_ValidDate;
        private string _Purchase_Attachment;
        private string _Purchase_Site;
        private int _Purchase_IsRecommend;
        private int _Purchase_IsPublic;
        private int _Purchase_CateID;
        private int _Purchase_SysUserID;

        public int Purchase_ID
        {
            get { return _Purchase_ID; }
            set { _Purchase_ID = value; }
        }

        public int Purchase_TypeID
        {
            get { return _Purchase_TypeID; }
            set { _Purchase_TypeID = value; }
        }

        public int Purchase_SupplierID
        {
            get { return _Purchase_SupplierID; }
            set { _Purchase_SupplierID = value; }
        }

        public string Purchase_Title
        {
            get { return _Purchase_Title; }
            set { _Purchase_Title = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public DateTime Purchase_DeliveryTime
        {
            get { return _Purchase_DeliveryTime; }
            set { _Purchase_DeliveryTime = value; }
        }

        public string Purchase_State
        {
            get { return _Purchase_State; }
            set { _Purchase_State = value.Length > 8 ? value.Substring(0, 8) : value.ToString(); }
        }

        public string Purchase_City
        {
            get { return _Purchase_City; }
            set { _Purchase_City = value.Length > 8 ? value.Substring(0, 8) : value.ToString(); }
        }

        public string Purchase_County
        {
            get { return _Purchase_County; }
            set { _Purchase_County = value.Length > 8 ? value.Substring(0, 8) : value.ToString(); }
        }

        public string Purchase_Address
        {
            get { return _Purchase_Address; }
            set { _Purchase_Address = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Purchase_Intro
        {
            get { return _Purchase_Intro; }
            set { _Purchase_Intro = value; }
        }

        public DateTime Purchase_Addtime
        {
            get { return _Purchase_Addtime; }
            set { _Purchase_Addtime = value; }
        }

        public int Purchase_Status
        {
            get { return _Purchase_Status; }
            set { _Purchase_Status = value; }
        }

        public int Purchase_IsActive
        {
            get { return _Purchase_IsActive; }
            set { _Purchase_IsActive = value; }
        }

        public string Purchase_ActiveReason
        {
            get { return _Purchase_ActiveReason; }
            set { _Purchase_ActiveReason = value.Length > 500 ? value.Substring(0, 500) : value.ToString(); }
        }

        public int Purchase_Trash
        {
            get { return _Purchase_Trash; }
            set { _Purchase_Trash = value; }
        }

        public DateTime Purchase_ValidDate
        {
            get { return _Purchase_ValidDate; }
            set { _Purchase_ValidDate = value; }
        }

        public string Purchase_Attachment
        {
            get { return _Purchase_Attachment; }
            set { _Purchase_Attachment = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Purchase_Site
        {
            get { return _Purchase_Site; }
            set { _Purchase_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }


        public int Purchase_IsRecommend
        {
            get { return _Purchase_IsRecommend; }
            set { _Purchase_IsRecommend = value; }
        }

        public int Purchase_IsPublic
        {
            get { return _Purchase_IsPublic; }
            set { _Purchase_IsPublic = value; }
        }
        public int Purchase_CateID
        {
            get { return _Purchase_CateID; }
            set { _Purchase_CateID = value; }
        }

        public int Purchase_SysUserID
        {
            get { return _Purchase_SysUserID; }
            set { _Purchase_SysUserID = value; }
        } 
    }

    public class SupplierPurchasePrivateInfo
    {
        private int _Purchase_Private_ID;
        private int _Purchase_Private_SupplierID;
        private int _Purchase_Private_PurchaseID;

        public int Purchase_Private_ID
        {
            get { return _Purchase_Private_ID; }
            set { _Purchase_Private_ID = value; }
        }

        public int Purchase_Private_SupplierID
        {
            get { return _Purchase_Private_SupplierID; }
            set { _Purchase_Private_SupplierID = value; }
        }

        public int Purchase_Private_PurchaseID
        {
            get { return _Purchase_Private_PurchaseID; }
            set { _Purchase_Private_PurchaseID = value; }
        }

    }

    public class SupplierPurchaseDetailInfo
    {
        private int _Detail_ID;
        private int _Detail_PurchaseID;
        private string _Detail_Name;
        private string _Detail_Spec;
        private int _Detail_Amount;
        private double _Detail_Price;

        public int Detail_ID
        {
            get { return _Detail_ID; }
            set { _Detail_ID = value; }
        }

        public int Detail_PurchaseID
        {
            get { return _Detail_PurchaseID; }
            set { _Detail_PurchaseID = value; }
        }

        public string Detail_Name
        {
            get { return _Detail_Name; }
            set { _Detail_Name = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Detail_Spec
        {
            get { return _Detail_Spec; }
            set { _Detail_Spec = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
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

    public class SupplierPurchaseCategoryInfo
    {
        private int _Cate_ID;
        private string _Cate_Name;
        private int _Cate_ParentID;
        private int _Cate_Sort;
        private int _Cate_IsActive;
        private string _Cate_Site;

        public int Cate_ID
        {
            get { return _Cate_ID; }
            set { _Cate_ID = value; }
        }

        public string Cate_Name
        {
            get { return _Cate_Name; }
            set { _Cate_Name = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int Cate_ParentID
        {
            get { return _Cate_ParentID; }
            set { _Cate_ParentID = value; }
        }

        public int Cate_Sort
        {
            get { return _Cate_Sort; }
            set { _Cate_Sort = value; }
        }

        public int Cate_IsActive
        {
            get { return _Cate_IsActive; }
            set { _Cate_IsActive = value; }
        }

        public string Cate_Site
        {
            get { return _Cate_Site; }
            set { _Cate_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

    }
}
