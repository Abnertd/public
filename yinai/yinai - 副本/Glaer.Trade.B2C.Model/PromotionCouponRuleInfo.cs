using System;
using System.Collections.Generic;

namespace Glaer.Trade.B2C.Model
{
    public class PromotionCouponRuleInfo
    {
        private int _Coupon_Rule_ID;
        private string _Coupon_Rule_Title;
        private int _Coupon_Rule_Target;
        private double _Coupon_Rule_Payline;
        private int _Coupon_Rule_Manner;
        private double _Coupon_Rule_Price;
        private double _Coupon_Rule_Percent;
        private int _Coupon_Rule_Amount;
        private int _Coupon_Rule_Valid;
        private string _Coupon_Rule_Note;
        private string _Coupon_Rule_Site;
        private IList<PromotionCouponRuleBrandInfo> _PromotionCouponRuleBrands;
        private IList<PromotionCouponRuleCateInfo> _PromotionCouponRuleCates;
        private IList<PromotionCouponRuleProductInfo> _PromotionCouponRuleProducts;

        public int Coupon_Rule_ID
        {
            get { return _Coupon_Rule_ID; }
            set { _Coupon_Rule_ID = value; }
        }

        public string Coupon_Rule_Title
        {
            get { return _Coupon_Rule_Title; }
            set { _Coupon_Rule_Title = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int Coupon_Rule_Target
        {
            get { return _Coupon_Rule_Target; }
            set { _Coupon_Rule_Target = value; }
        }

        public double Coupon_Rule_Payline
        {
            get { return _Coupon_Rule_Payline; }
            set { _Coupon_Rule_Payline = value; }
        }

        public int Coupon_Rule_Manner
        {
            get { return _Coupon_Rule_Manner; }
            set { _Coupon_Rule_Manner = value; }
        }

        public double Coupon_Rule_Price
        {
            get { return _Coupon_Rule_Price; }
            set { _Coupon_Rule_Price = value; }
        }

        public double Coupon_Rule_Percent
        {
            get { return _Coupon_Rule_Percent; }
            set { _Coupon_Rule_Percent = value; }
        }

        public int Coupon_Rule_Amount
        {
            get { return _Coupon_Rule_Amount; }
            set { _Coupon_Rule_Amount = value; }
        }

        public int Coupon_Rule_Valid
        {
            get { return _Coupon_Rule_Valid; }
            set { _Coupon_Rule_Valid = value; }
        }

        public string Coupon_Rule_Note
        {
            get { return _Coupon_Rule_Note; }
            set { _Coupon_Rule_Note = value.Length > 300 ? value.Substring(0, 300) : value.ToString(); }
        }

        public string Coupon_Rule_Site
        {
            get { return _Coupon_Rule_Site; }
            set { _Coupon_Rule_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public IList<PromotionCouponRuleBrandInfo> PromotionCouponRuleBrands
        {
            get { return _PromotionCouponRuleBrands; }
            set { _PromotionCouponRuleBrands = value; }
        }

        public IList<PromotionCouponRuleCateInfo> PromotionCouponRuleCates
        {
            get { return _PromotionCouponRuleCates; }
            set { _PromotionCouponRuleCates = value; }
        }

        public IList<PromotionCouponRuleProductInfo> PromotionCouponRuleProducts
        {
            get { return _PromotionCouponRuleProducts; }
            set { _PromotionCouponRuleProducts = value; }
        }

    }

    public class PromotionCouponRuleBrandInfo
    {
        private int _Coupon_Rule_Brand_ID;
        private int _Coupon_Rule_Brand_BrandID;
        private int _Coupon_Rule_Brand_RuleID;

        public int Coupon_Rule_Brand_ID
        {
            get { return _Coupon_Rule_Brand_ID; }
            set { _Coupon_Rule_Brand_ID = value; }
        }

        public int Coupon_Rule_Brand_BrandID
        {
            get { return _Coupon_Rule_Brand_BrandID; }
            set { _Coupon_Rule_Brand_BrandID = value; }
        }

        public int Coupon_Rule_Brand_RuleID
        {
            get { return _Coupon_Rule_Brand_RuleID; }
            set { _Coupon_Rule_Brand_RuleID = value; }
        }

    }

    public class PromotionCouponRuleCateInfo
    {
        private int _Coupon_Rule_Cate_ID;
        private int _Coupon_Rule_Cate_CateID;
        private int _Coupon_Rule_Cate_RuleID;

        public int Coupon_Rule_Cate_ID
        {
            get { return _Coupon_Rule_Cate_ID; }
            set { _Coupon_Rule_Cate_ID = value; }
        }

        public int Coupon_Rule_Cate_CateID
        {
            get { return _Coupon_Rule_Cate_CateID; }
            set { _Coupon_Rule_Cate_CateID = value; }
        }

        public int Coupon_Rule_Cate_RuleID
        {
            get { return _Coupon_Rule_Cate_RuleID; }
            set { _Coupon_Rule_Cate_RuleID = value; }
        }

    }

    public class PromotionCouponRuleProductInfo
    {
        private int _Coupon_Rule_Product_ID;
        private int _Coupon_Rule_Product_ProductID;
        private int _Coupon_Rule_Product_RuleID;

        public int Coupon_Rule_Product_ID
        {
            get { return _Coupon_Rule_Product_ID; }
            set { _Coupon_Rule_Product_ID = value; }
        }

        public int Coupon_Rule_Product_ProductID
        {
            get { return _Coupon_Rule_Product_ProductID; }
            set { _Coupon_Rule_Product_ProductID = value; }
        }

        public int Coupon_Rule_Product_RuleID
        {
            get { return _Coupon_Rule_Product_RuleID; }
            set { _Coupon_Rule_Product_RuleID = value; }
        }

    }
}
