using System;
using System.Collections.Generic;
using System.Text;

namespace Glaer.Trade.B2C.Model
{
    public class PromotionFavorGiftInfo
    {
        private int _Promotion_Gift_ID;
        private string _Promotion_Gift_Title;
        private int _Promotion_Gift_Target;
        private int _Promotion_Gift_Group;
        private int _Promotion_Gift_Limit;
        private DateTime _Promotion_Gift_Starttime;
        private DateTime _Promotion_Gift_Endtime;
        private DateTime _Promotion_Gift_Addtime;
        private int _Promotion_Gift_Sort;
        private int _Promotion_Gift_IsRepeat;
        private int _Promotion_Gift_IsActive;
        private int _Promotion_Gift_IsChecked;
        private string _Promotion_Gift_Site;
        private IList<PromotionFavorGiftBrandInfo> _Promotion_Gift_Brands;
        private IList<PromotionFavorGiftCateInfo> _Promotion_Gift_Cates;
        private IList<PromotionFavorGiftProductInfo> _Promotion_Gift_Products;
        private IList<PromotionFavorGiftExceptInfo> _PromotionFavorGiftExcepts;
        private IList<PromotionFavorGiftMemberGradeInfo> _PromotionFavorGiftMemberGrades;
        private IList<PromotionFavorGiftAmountInfo> _Promotion_Gift_Amounts;

        public int Promotion_Gift_ID
        {
            get { return _Promotion_Gift_ID; }
            set { _Promotion_Gift_ID = value; }
        }

        public string Promotion_Gift_Title
        {
            get { return _Promotion_Gift_Title; }
            set { _Promotion_Gift_Title = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int Promotion_Gift_Target
        {
            get { return _Promotion_Gift_Target; }
            set { _Promotion_Gift_Target = value; }
        }

        public int Promotion_Gift_Group
        {
            get { return _Promotion_Gift_Group; }
            set { _Promotion_Gift_Group = value; }
        }

        public int Promotion_Gift_Limit
        {
            get { return _Promotion_Gift_Limit; }
            set { _Promotion_Gift_Limit = value; }
        }

        public DateTime Promotion_Gift_Starttime
        {
            get { return _Promotion_Gift_Starttime; }
            set { _Promotion_Gift_Starttime = value; }
        }

        public DateTime Promotion_Gift_Endtime
        {
            get { return _Promotion_Gift_Endtime; }
            set { _Promotion_Gift_Endtime = value; }
        }

        public DateTime Promotion_Gift_Addtime
        {
            get { return _Promotion_Gift_Addtime; }
            set { _Promotion_Gift_Addtime = value; }
        }

        public int Promotion_Gift_Sort
        {
            get { return _Promotion_Gift_Sort; }
            set { _Promotion_Gift_Sort = value; }
        }

        public int Promotion_Gift_IsRepeat
        {
            get { return _Promotion_Gift_IsRepeat; }
            set { _Promotion_Gift_IsRepeat = value; }
        }

        public int Promotion_Gift_IsActive
        {
            get { return _Promotion_Gift_IsActive; }
            set { _Promotion_Gift_IsActive = value; }
        }

        public int Promotion_Gift_IsChecked
        {
            get { return _Promotion_Gift_IsChecked; }
            set { _Promotion_Gift_IsChecked = value; }
        }

        public string Promotion_Gift_Site
        {
            get { return _Promotion_Gift_Site; }
            set { _Promotion_Gift_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public IList<PromotionFavorGiftBrandInfo> Promotion_Gift_Brands
        {
            get { return _Promotion_Gift_Brands; }
            set { _Promotion_Gift_Brands = value; }
        }

        public IList<PromotionFavorGiftCateInfo> Promotion_Gift_Cates
        {
            get { return _Promotion_Gift_Cates; }
            set { _Promotion_Gift_Cates = value; }
        }

        public IList<PromotionFavorGiftProductInfo> Promotion_Gift_Products
        {
            get { return _Promotion_Gift_Products; }
            set { _Promotion_Gift_Products = value; }
        }

        public IList<PromotionFavorGiftExceptInfo> PromotionFavorGiftExcepts
        {
            get { return _PromotionFavorGiftExcepts; }
            set { _PromotionFavorGiftExcepts = value; }
        }

        public IList<PromotionFavorGiftMemberGradeInfo> PromotionFavorGiftMemberGrades
        {
            get { return _PromotionFavorGiftMemberGrades; }
            set { _PromotionFavorGiftMemberGrades = value; }
        }

        public IList<PromotionFavorGiftAmountInfo> Promotion_Gift_Amounts
        {
            get { return _Promotion_Gift_Amounts; }
            set { _Promotion_Gift_Amounts = value; }
        }

    }

    public class PromotionFavorGiftBrandInfo
    {
        private int _Favor_Id;
        private int _Favor_BrandID;
        private int _Promotion_Gift_ID;

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

        public int Promotion_Gift_ID
        {
            get { return _Promotion_Gift_ID; }
            set { _Promotion_Gift_ID = value; }
        }

    }

    public class PromotionFavorGiftCateInfo
    {
        private int _Favor_Id;
        private int _Favor_CateId;
        private int _Promotion_Gift_ID;

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

        public int Promotion_Gift_ID
        {
            get { return _Promotion_Gift_ID; }
            set { _Promotion_Gift_ID = value; }
        }

    }

    public class PromotionFavorGiftProductInfo
    {
        private int _Favor_Id;
        private int _Favor_ProductID;
        private int _Promotion_Gift_ID;

        public int Favor_Id
        {
            get { return _Favor_Id; }
            set { _Favor_Id = value; }
        }

        public int Favor_ProductID
        {
            get { return _Favor_ProductID; }
            set { _Favor_ProductID = value; }
        }

        public int Promotion_Gift_ID
        {
            get { return _Promotion_Gift_ID; }
            set { _Promotion_Gift_ID = value; }
        }

    }

    public class PromotionFavorGiftExceptInfo
    {
        private int _Favor_Id;
        private int _Favor_ProductID;
        private int _Promotion_Gift_ID;

        public int Favor_Id
        {
            get { return _Favor_Id; }
            set { _Favor_Id = value; }
        }

        public int Favor_ProductID
        {
            get { return _Favor_ProductID; }
            set { _Favor_ProductID = value; }
        }

        public int Promotion_Gift_ID
        {
            get { return _Promotion_Gift_ID; }
            set { _Promotion_Gift_ID = value; }
        }

    }

    public class PromotionFavorGiftMemberGradeInfo
    {
        private int _Favor_Id;
        private int _Favor_GradeID;
        private int _Promotion_Gift_ID;

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

        public int Promotion_Gift_ID
        {
            get { return _Promotion_Gift_ID; }
            set { _Promotion_Gift_ID = value; }
        }

    }

    public class PromotionFavorGiftAmountInfo
    {
        private int _Gift_Amount_ID;
        private int _Gift_Amount_GiftID;
        private int _Gift_Amount_Amount;
        private double _Gift_Amount_BuyAmount;
        private IList<PromotionFavorGiftGiftInfo> _Promotion_Gift_Gifts;

        public int Gift_Amount_ID
        {
            get { return _Gift_Amount_ID; }
            set { _Gift_Amount_ID = value; }
        }

        public int Gift_Amount_GiftID
        {
            get { return _Gift_Amount_GiftID; }
            set { _Gift_Amount_GiftID = value; }
        }

        public int Gift_Amount_Amount
        {
            get { return _Gift_Amount_Amount; }
            set { _Gift_Amount_Amount = value; }
        }

        public double Gift_Amount_BuyAmount
        {
            get { return _Gift_Amount_BuyAmount; }
            set { _Gift_Amount_BuyAmount = value; }
        }

        public IList<PromotionFavorGiftGiftInfo> Promotion_Gift_Gifts
        {
            get { return _Promotion_Gift_Gifts; }
            set { _Promotion_Gift_Gifts = value; }
        }

    }

    public class PromotionFavorGiftGiftInfo
    {
        private int _Gift_ID;
        private int _Gift_AmountID;
        private int _Gift_ProductID;
        private int _Gift_Amount;

        public int Gift_ID
        {
            get { return _Gift_ID; }
            set { _Gift_ID = value; }
        }

        public int Gift_AmountID
        {
            get { return _Gift_AmountID; }
            set { _Gift_AmountID = value; }
        }

        public int Gift_ProductID
        {
            get { return _Gift_ProductID; }
            set { _Gift_ProductID = value; }
        }

        public int Gift_Amount
        {
            get { return _Gift_Amount; }
            set { _Gift_Amount = value; }
        }

    }
}
