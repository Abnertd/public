using System;
using System.Collections.Generic;
using System.Text;

namespace Glaer.Trade.B2C.Model
{
    public class PromotionFavorCouponInfo
    {
        private int _Promotion_Coupon_ID;
        private string _Promotion_Coupon_Title;
        private int _Promotion_Coupon_Target;
        private double _Promotion_Coupon_Payline;
        private int _Promotion_Coupon_Manner;
        private double _Promotion_Coupon_Price;
        private double _Promotion_Coupon_Percent;
        private int _Promotion_Coupon_Amount;
        private DateTime _Promotion_Coupon_Starttime;
        private DateTime _Promotion_Coupon_Endtime;
        private int _Promotion_Coupon_Member_ID;
        private string _Promotion_Coupon_Code;
        private string _Promotion_Coupon_Verifycode;
        private int _Promotion_Coupon_Isused;
        private int _Promotion_Coupon_UseAmount;
        private int _Promotion_Coupon_Display;
        private int _Promotion_Coupon_OrdersID;
        private string _Promotion_Coupon_Note;
        private DateTime _Promotion_Coupon_Addtime;
        private string _Promotion_Coupon_Site;
        private IList<PromotionFavorCouponBrandInfo> _PromotionFavorCouponBrands;
        private IList<PromotionFavorCouponCateInfo> _PromotionFavorCouponCates;
        private IList<PromotionFavorCouponProductInfo> _PromotionFavorCouponProducts;

        public int Promotion_Coupon_ID
        {
            get { return _Promotion_Coupon_ID; }
            set { _Promotion_Coupon_ID = value; }
        }

        public string Promotion_Coupon_Title
        {
            get { return _Promotion_Coupon_Title; }
            set { _Promotion_Coupon_Title = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int Promotion_Coupon_Target
        {
            get { return _Promotion_Coupon_Target; }
            set { _Promotion_Coupon_Target = value; }
        }

        public double Promotion_Coupon_Payline
        {
            get { return _Promotion_Coupon_Payline; }
            set { _Promotion_Coupon_Payline = value; }
        }

        public int Promotion_Coupon_Manner
        {
            get { return _Promotion_Coupon_Manner; }
            set { _Promotion_Coupon_Manner = value; }
        }

        public double Promotion_Coupon_Price
        {
            get { return _Promotion_Coupon_Price; }
            set { _Promotion_Coupon_Price = value; }
        }

        public double Promotion_Coupon_Percent
        {
            get { return _Promotion_Coupon_Percent; }
            set { _Promotion_Coupon_Percent = value; }
        }

        public int Promotion_Coupon_Amount
        {
            get { return _Promotion_Coupon_Amount; }
            set { _Promotion_Coupon_Amount = value; }
        }

        public DateTime Promotion_Coupon_Starttime
        {
            get { return _Promotion_Coupon_Starttime; }
            set { _Promotion_Coupon_Starttime = value; }
        }

        public DateTime Promotion_Coupon_Endtime
        {
            get { return _Promotion_Coupon_Endtime; }
            set { _Promotion_Coupon_Endtime = value; }
        }

        public int Promotion_Coupon_Member_ID
        {
            get { return _Promotion_Coupon_Member_ID; }
            set { _Promotion_Coupon_Member_ID = value; }
        }

        public string Promotion_Coupon_Code
        {
            get { return _Promotion_Coupon_Code; }
            set { _Promotion_Coupon_Code = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Promotion_Coupon_Verifycode
        {
            get { return _Promotion_Coupon_Verifycode; }
            set { _Promotion_Coupon_Verifycode = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int Promotion_Coupon_Isused
        {
            get { return _Promotion_Coupon_Isused; }
            set { _Promotion_Coupon_Isused = value; }
        }

        public int Promotion_Coupon_UseAmount
        {
            get { return _Promotion_Coupon_UseAmount; }
            set { _Promotion_Coupon_UseAmount = value; }
        }

        public int Promotion_Coupon_Display
        {
            get { return _Promotion_Coupon_Display; }
            set { _Promotion_Coupon_Display = value; }
        }

        public int Promotion_Coupon_OrdersID
        {
            get { return _Promotion_Coupon_OrdersID; }
            set { _Promotion_Coupon_OrdersID = value; }
        }

        public string Promotion_Coupon_Note
        {
            get { return _Promotion_Coupon_Note; }
            set { _Promotion_Coupon_Note = value.Length > 300 ? value.Substring(0, 300) : value.ToString(); }
        }

        public DateTime Promotion_Coupon_Addtime
        {
            get { return _Promotion_Coupon_Addtime; }
            set { _Promotion_Coupon_Addtime = value; }
        }

        public string Promotion_Coupon_Site
        {
            get { return _Promotion_Coupon_Site; }
            set { _Promotion_Coupon_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public IList<PromotionFavorCouponCateInfo> PromotionFavorCouponCates
        {
            get { return _PromotionFavorCouponCates; }
            set { _PromotionFavorCouponCates = value; }
        }

        public IList<PromotionFavorCouponBrandInfo> PromotionFavorCouponBrands
        {
            get { return _PromotionFavorCouponBrands; }
            set { _PromotionFavorCouponBrands = value; }
        }

        public IList<PromotionFavorCouponProductInfo> PromotionFavorCouponProducts
        {
            get { return _PromotionFavorCouponProducts; }
            set { _PromotionFavorCouponProducts = value; }
        }

    }

    public class PromotionFavorCouponBrandInfo
    {
        private int _Favor_Id;
        private int _Favor_BrandID;
        private int _Promotion_Coupon_ID;

        public int Favor_Id
        {
            get { return _Favor_Id; }
            set { _Favor_Id = value; }
        }

        public int Favor_BrandID
        {
            get { return _Favor_BrandID; }
            set { _Favor_BrandID = value; }
        }

        public int Promotion_Coupon_ID
        {
            get { return _Promotion_Coupon_ID; }
            set { _Promotion_Coupon_ID = value; }
        }

    }

    public class PromotionFavorCouponCateInfo
    {
        private int _Favor_Id;
        private int _Favor_CateID;
        private int _Promotion_Coupon_ID;

        public int Favor_Id
        {
            get { return _Favor_Id; }
            set { _Favor_Id = value; }
        }

        public int Favor_CateID
        {
            get { return _Favor_CateID; }
            set { _Favor_CateID = value; }
        }

        public int Promotion_Coupon_ID
        {
            get { return _Promotion_Coupon_ID; }
            set { _Promotion_Coupon_ID = value; }
        }

    }

    public class PromotionFavorCouponProductInfo
    {
        private int _Favor_ID;
        private int _Favor_ProductID;
        private int _Promotion_Coupon_ID;

        public int Favor_ID
        {
            get { return _Favor_ID; }
            set { _Favor_ID = value; }
        }

        public int Favor_ProductID
        {
            get { return _Favor_ProductID; }
            set { _Favor_ProductID = value; }
        }

        public int Promotion_Coupon_ID
        {
            get { return _Promotion_Coupon_ID; }
            set { _Promotion_Coupon_ID = value; }
        }

    }
}
