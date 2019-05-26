using System;
using System.Collections.Generic;
using System.Text;

namespace Glaer.Trade.B2C.Model
{
    public class PromotionFavorPolicyInfo
    {
        private int _Promotion_Policy_ID;
        private string _Promotion_Policy_Title;
        private int _Promotion_Policy_Target;
        private double _Promotion_Policy_Payline;
        private int _Promotion_Policy_Manner;
        private int _Promotion_Policy_CouponRuleID;
        private double _Promotion_Policy_Price;
        private double _Promotion_Policy_Percent;
        private int _Promotion_Policy_Group;
        private int _Promotion_Policy_Limit;
        private int _Promotion_Policy_IsRepeat;
        private DateTime _Promotion_Policy_Starttime;
        private DateTime _Promotion_Policy_Endtime;
        private int _Promotion_Policy_Sort;
        private int _Promotion_Policy_IsActive;
        private int _Promotion_Policy_IsChecked;
        private string _Promotion_Policy_Note;
        private string _Promotion_Policy_Site;
        private IList<PromotionFavorPolicyBrandInfo> _PromotionFavorPolicyBrands;
        private IList<PromotionFavorPolicyCateInfo> _PromotionFavorPolicyCates;
        private IList<PromotionFavorPolicyProductInfo> _PromotionFavorPolicyProducts;
        private IList<PromotionFavorPolicyExceptInfo> _PromotionFavorPolicyExcepts;
        private IList<PromotionFavorPolicyMemberGradeInfo> _PromotionFavorPolicyMemberGrades;

        public int Promotion_Policy_ID
        {
            get { return _Promotion_Policy_ID; }
            set { _Promotion_Policy_ID = value; }
        }

        public string Promotion_Policy_Title
        {
            get { return _Promotion_Policy_Title; }
            set { _Promotion_Policy_Title = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int Promotion_Policy_Target
        {
            get { return _Promotion_Policy_Target; }
            set { _Promotion_Policy_Target = value; }
        }

        public double Promotion_Policy_Payline
        {
            get { return _Promotion_Policy_Payline; }
            set { _Promotion_Policy_Payline = value; }
        }

        public int Promotion_Policy_Manner
        {
            get { return _Promotion_Policy_Manner; }
            set { _Promotion_Policy_Manner = value; }
        }

        public int Promotion_Policy_CouponRuleID
        {
            get { return _Promotion_Policy_CouponRuleID; }
            set { _Promotion_Policy_CouponRuleID = value; }
        }

        public double Promotion_Policy_Price
        {
            get { return _Promotion_Policy_Price; }
            set { _Promotion_Policy_Price = value; }
        }

        public double Promotion_Policy_Percent
        {
            get { return _Promotion_Policy_Percent; }
            set { _Promotion_Policy_Percent = value; }
        }

        public int Promotion_Policy_Group
        {
            get { return _Promotion_Policy_Group; }
            set { _Promotion_Policy_Group = value; }
        }

        public int Promotion_Policy_Limit
        {
            get { return _Promotion_Policy_Limit; }
            set { _Promotion_Policy_Limit = value; }
        }

        public int Promotion_Policy_IsRepeat
        {
            get { return _Promotion_Policy_IsRepeat; }
            set { _Promotion_Policy_IsRepeat = value; }
        }

        public DateTime Promotion_Policy_Starttime
        {
            get { return _Promotion_Policy_Starttime; }
            set { _Promotion_Policy_Starttime = value; }
        }

        public DateTime Promotion_Policy_Endtime
        {
            get { return _Promotion_Policy_Endtime; }
            set { _Promotion_Policy_Endtime = value; }
        }

        public int Promotion_Policy_Sort
        {
            get { return _Promotion_Policy_Sort; }
            set { _Promotion_Policy_Sort = value; }
        }

        public int Promotion_Policy_IsActive
        {
            get { return _Promotion_Policy_IsActive; }
            set { _Promotion_Policy_IsActive = value; }
        }

        public int Promotion_Policy_IsChecked
        {
            get { return _Promotion_Policy_IsChecked; }
            set { _Promotion_Policy_IsChecked = value; }
        }

        public string Promotion_Policy_Note
        {
            get { return _Promotion_Policy_Note; }
            set { _Promotion_Policy_Note = value.Length > 300 ? value.Substring(0, 300) : value.ToString(); }
        }

        public string Promotion_Policy_Site
        {
            get { return _Promotion_Policy_Site; }
            set { _Promotion_Policy_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public IList<PromotionFavorPolicyBrandInfo> PromotionFavorPolicyBrands
        {
            get { return _PromotionFavorPolicyBrands; }
            set { _PromotionFavorPolicyBrands = value; }
        }

        public IList<PromotionFavorPolicyCateInfo> PromotionFavorPolicyCates
        {
            get { return _PromotionFavorPolicyCates; }
            set { _PromotionFavorPolicyCates = value; }
        }

        public IList<PromotionFavorPolicyProductInfo> PromotionFavorPolicyProducts
        {
            get { return _PromotionFavorPolicyProducts; }
            set { _PromotionFavorPolicyProducts = value; }
        }

        public IList<PromotionFavorPolicyExceptInfo> PromotionFavorPolicyExcepts
        {
            get { return _PromotionFavorPolicyExcepts; }
            set { _PromotionFavorPolicyExcepts = value; }
        }

        public IList<PromotionFavorPolicyMemberGradeInfo> PromotionFavorPolicyMemberGrades
        {
            get { return _PromotionFavorPolicyMemberGrades; }
            set { _PromotionFavorPolicyMemberGrades = value; }
        }

    }

    public class PromotionFavorPolicyBrandInfo
    {
        private int _Favor_Id;
        private int _Favor_BrandID;
        private int _Promotion_Policy_ID;

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

        public int Promotion_Policy_ID
        {
            get { return _Promotion_Policy_ID; }
            set { _Promotion_Policy_ID = value; }
        }

    }

    public class PromotionFavorPolicyCateInfo
    {
        private int _Favor_Id;
        private int _Favor_CateID;
        private int _Promotion_Policy_ID;

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

        public int Promotion_Policy_ID
        {
            get { return _Promotion_Policy_ID; }
            set { _Promotion_Policy_ID = value; }
        }

    }

    public class PromotionFavorPolicyProductInfo
    {
        private int _Favor_ID;
        private int _Favor_ProductID;
        private int _Promotion_Policy_ID;

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

        public int Promotion_Policy_ID
        {
            get { return _Promotion_Policy_ID; }
            set { _Promotion_Policy_ID = value; }
        }

    }

    public class PromotionFavorPolicyExceptInfo
    {
        private int _Favor_ID;
        private int _Favor_ProductID;
        private int _Promotion_Policy_ID;

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

        public int Promotion_Policy_ID
        {
            get { return _Promotion_Policy_ID; }
            set { _Promotion_Policy_ID = value; }
        }

    }

    public class PromotionFavorPolicyMemberGradeInfo
    {
        private int _Favor_ID;
        private int _Favor_GradeID;
        private int _Promotion_Policy_ID;

        public int Favor_ID
        {
            get { return _Favor_ID; }
            set { _Favor_ID = value; }
        }

        public int Favor_GradeID
        {
            get { return _Favor_GradeID; }
            set { _Favor_GradeID = value; }
        }

        public int Promotion_Policy_ID
        {
            get { return _Promotion_Policy_ID; }
            set { _Promotion_Policy_ID = value; }
        }

    }
}
