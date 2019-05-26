using System;
using System.Collections.Generic;
using System.Text;

namespace Glaer.Trade.B2C.Model
{

    public class PromotionFavorFeeInfo
    {
        private int _Promotion_Fee_ID;
        private string _Promotion_Fee_Title;
        private int _Promotion_Fee_Target;
        private double _Promotion_Fee_Payline;
        private int _Promotion_Fee_Manner;
        private double _Promotion_Fee_Price;
        private DateTime _Promotion_Fee_Starttime;
        private DateTime _Promotion_Fee_Endtime;
        private int _Promotion_Fee_Sort;
        private int _Promotion_Fee_IsActive;
        private int _Promotion_Fee_IsChecked;
        private string _Promotion_Fee_Note;
        private DateTime _Promotion_Fee_Addtime;
        private string _Promotion_Fee_Site;
        private IList<PromotionFavorFeeDistrictInfo> _PromotionFavorFeeDistricts;
        private IList<PromotionFavorFeeCateInfo> _PromotionFavorFeeCates;
        private IList<PromotionFavorFeeBrandInfo> _PromotionFavorFeeBrands;
        private IList<PromotionFavorFeeProductInfo> _PromotionFavorFeeProducts;
        private IList<PromotionFavorFeeExceptInfo> _PromotionFavorFeeExcepts;
        private IList<PromotionFavorFeeMemberGradeInfo> _PromotionFavorFeeMemberGrades;
        private IList<PromotionFavorFeeDeliveryInfo> _PromotionFavorFeeDeliverys;
        private IList<PromotionFavorFeePaywayInfo> _PromotionFavorFeePayways;

        public int Promotion_Fee_ID
        {
            get { return _Promotion_Fee_ID; }
            set { _Promotion_Fee_ID = value; }
        }

        public string Promotion_Fee_Title
        {
            get { return _Promotion_Fee_Title; }
            set { _Promotion_Fee_Title = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public double Promotion_Fee_Payline
        {
            get { return _Promotion_Fee_Payline; }
            set { _Promotion_Fee_Payline = value; }
        }

        public int Promotion_Fee_Target
        {
            get { return _Promotion_Fee_Target; }
            set { _Promotion_Fee_Target = value; }
        }

        public int Promotion_Fee_Manner
        {
            get { return _Promotion_Fee_Manner; }
            set { _Promotion_Fee_Manner = value; }
        }

        public double Promotion_Fee_Price
        {
            get { return _Promotion_Fee_Price; }
            set { _Promotion_Fee_Price = value; }
        }

        public DateTime Promotion_Fee_Starttime
        {
            get { return _Promotion_Fee_Starttime; }
            set { _Promotion_Fee_Starttime = value; }
        }

        public DateTime Promotion_Fee_Endtime
        {
            get { return _Promotion_Fee_Endtime; }
            set { _Promotion_Fee_Endtime = value; }
        }

        public int Promotion_Fee_Sort
        {
            get { return _Promotion_Fee_Sort; }
            set { _Promotion_Fee_Sort = value; }
        }

        public int Promotion_Fee_IsActive
        {
            get { return _Promotion_Fee_IsActive; }
            set { _Promotion_Fee_IsActive = value; }
        }

        public int Promotion_Fee_IsChecked
        {
            get { return _Promotion_Fee_IsChecked; }
            set { _Promotion_Fee_IsChecked = value; }
        }

        public string Promotion_Fee_Note
        {
            get { return _Promotion_Fee_Note; }
            set { _Promotion_Fee_Note = value.Length > 300 ? value.Substring(0, 300) : value.ToString(); }
        }

        public DateTime Promotion_Fee_Addtime
        {
            get { return _Promotion_Fee_Addtime; }
            set { _Promotion_Fee_Addtime = value; }
        }

        public string Promotion_Fee_Site
        {
            get { return _Promotion_Fee_Site; }
            set { _Promotion_Fee_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public IList<PromotionFavorFeeDistrictInfo> PromotionFavorFeeDistricts
        {
            get { return _PromotionFavorFeeDistricts; }
            set { _PromotionFavorFeeDistricts = value; }
        }

        public IList<PromotionFavorFeeCateInfo> PromotionFavorFeeCates
        {
            get { return _PromotionFavorFeeCates; }
            set { _PromotionFavorFeeCates = value; }
        }

        public IList<PromotionFavorFeeBrandInfo> PromotionFavorFeeBrands
        {
            get { return _PromotionFavorFeeBrands; }
            set { _PromotionFavorFeeBrands = value; }
        }

        public IList<PromotionFavorFeeProductInfo> PromotionFavorFeeProducts
        {
            get { return _PromotionFavorFeeProducts; }
            set { _PromotionFavorFeeProducts = value; }
        }

        public IList<PromotionFavorFeeExceptInfo> PromotionFavorFeeExcepts
        {
            get { return _PromotionFavorFeeExcepts; }
            set { _PromotionFavorFeeExcepts = value; }
        }

        public IList<PromotionFavorFeeMemberGradeInfo> PromotionFavorFeeMemberGrades
        {
            get { return _PromotionFavorFeeMemberGrades; }
            set { _PromotionFavorFeeMemberGrades = value; }
        }

        public IList<PromotionFavorFeeDeliveryInfo> PromotionFavorFeeDeliverys
        {
            get { return _PromotionFavorFeeDeliverys; }
            set { _PromotionFavorFeeDeliverys = value; }
        }

        public IList<PromotionFavorFeePaywayInfo> PromotionFavorFeePayways
        {
            get { return _PromotionFavorFeePayways; }
            set { _PromotionFavorFeePayways = value; }
        }


    }

    public class FavorDiscountInfo
    {
        private int _Discount_Policy;
        private double _Discount_Amount;
        private string _Discount_Note;

        public int Discount_Policy
        {
            get { return _Discount_Policy; }
            set { _Discount_Policy = value; }
        }

        public double Discount_Amount
        {
            get { return _Discount_Amount; }
            set { _Discount_Amount = value; }
        }

        public string Discount_Note
        {
            get { return _Discount_Note; }
            set { _Discount_Note = value.Length > 300 ? value.Substring(0, 300) : value.ToString(); }
        }
    }

    public class PromotionFavorFeeCateInfo
    {
        private int _Favor_Id;
        private int _Favor_CateId;
        private int _Promotion_Fee_ID;

        public int Favor_Id
        {
            get { return _Favor_Id; }
            set { _Favor_Id = value; }
        }

        public int Favor_CateId
        {
            get { return _Favor_CateId; }
            set { _Favor_CateId = value; }
        }

        public int Promotion_Fee_ID
        {
            get { return _Promotion_Fee_ID; }
            set { _Promotion_Fee_ID = value; }
        }

    }

    public class PromotionFavorFeeBrandInfo
    {
        private int _Favor_Id;
        private int _Favor_BrandID;
        private int _Promotion_Fee_ID;

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

        public int Promotion_Fee_ID
        {
            get { return _Promotion_Fee_ID; }
            set { _Promotion_Fee_ID = value; }
        }

    }

    public class PromotionFavorFeeProductInfo
    {
        private int _Favor_Id;
        private int _Favor_ProductId;
        private int _Promotion_Fee_ID;

        public int Favor_Id
        {
            get { return _Favor_Id; }
            set { _Favor_Id = value; }
        }

        public int Favor_ProductId
        {
            get { return _Favor_ProductId; }
            set { _Favor_ProductId = value; }
        }

        public int Promotion_Fee_ID
        {
            get { return _Promotion_Fee_ID; }
            set { _Promotion_Fee_ID = value; }
        }

    }

    public class PromotionFavorFeeExceptInfo
    {
        private int _Favor_Id;
        private int _Favor_ProductId;
        private int _Promotion_Fee_ID;

        public int Favor_Id
        {
            get { return _Favor_Id; }
            set { _Favor_Id = value; }
        }

        public int Favor_ProductId
        {
            get { return _Favor_ProductId; }
            set { _Favor_ProductId = value; }
        }

        public int Promotion_Fee_ID
        {
            get { return _Promotion_Fee_ID; }
            set { _Promotion_Fee_ID = value; }
        }

    }

    public class PromotionFavorFeeDistrictInfo
    {
        private int _Favor_ID;
        private string _Favor_State_ID;
        private int _Promotion_Fee_ID;

        public int Favor_ID
        {
            get { return _Favor_ID; }
            set { _Favor_ID = value; }
        }

        public string Favor_State_ID
        {
            get { return _Favor_State_ID; }
            set { _Favor_State_ID = value.Length > 10 ? value.Substring(0, 10) : value.ToString(); }
        }

        public int Promotion_Fee_ID
        {
            get { return _Promotion_Fee_ID; }
            set { _Promotion_Fee_ID = value; }
        }

    }

    public class PromotionFavorFeeDeliveryInfo
    {
        private int _Favor_Id;
        private int _Favor_DeliveryId;
        private int _Promotion_Fee_ID;

        public int Favor_Id
        {
            get { return _Favor_Id; }
            set { _Favor_Id = value; }
        }

        public int Favor_DeliveryId
        {
            get { return _Favor_DeliveryId; }
            set { _Favor_DeliveryId = value; }
        }

        public int Promotion_Fee_ID
        {
            get { return _Promotion_Fee_ID; }
            set { _Promotion_Fee_ID = value; }
        }

    }

    public class PromotionFavorFeePaywayInfo
    {
        private int _Favor_Id;
        private int _Favor_PaywayId;
        private int _Promotion_Fee_ID;

        public int Favor_Id
        {
            get { return _Favor_Id; }
            set { _Favor_Id = value; }
        }

        public int Favor_PaywayId
        {
            get { return _Favor_PaywayId; }
            set { _Favor_PaywayId = value; }
        }

        public int Promotion_Fee_ID
        {
            get { return _Promotion_Fee_ID; }
            set { _Promotion_Fee_ID = value; }
        }

    }

    public class PromotionFavorFeeMemberGradeInfo
    {
        private int _Favor_Id;
        private int _Favor_GradeID;
        private int _Promotion_Fee_ID;

        public int Favor_Id
        {
            get { return _Favor_Id; }
            set { _Favor_Id = value; }
        }

        public int Favor_GradeID
        {
            get { return _Favor_GradeID; }
            set { _Favor_GradeID = value; }
        }

        public int Promotion_Fee_ID
        {
            get { return _Promotion_Fee_ID; }
            set { _Promotion_Fee_ID = value; }
        }

    }

}