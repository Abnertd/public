using System;
using System.Collections.Generic;

namespace Glaer.Trade.B2C.Model
{
    public class PromotionInfo
    {
        private int _Promotion_ID;
        private string _Promotion_Title;
        private int _Promotion_Type;
        private string _Promotion_TopHtml;
        private DateTime _Promotion_Addtime;
        private IList<PromotionProductInfo> _PromotionProducts;
        private string _Promotion_Site;

        public int Promotion_ID
        {
            get { return _Promotion_ID; }
            set { _Promotion_ID = value; }
        }

        public string Promotion_Title
        {
            get { return _Promotion_Title; }
            set { _Promotion_Title = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int Promotion_Type
        {
            get { return _Promotion_Type; }
            set { _Promotion_Type = value; }
        }

        public string Promotion_TopHtml
        {
            get { return _Promotion_TopHtml; }
            set { _Promotion_TopHtml = value; }
        }

        public DateTime Promotion_Addtime
        {
            get { return _Promotion_Addtime; }
            set { _Promotion_Addtime = value; }
        }

        public IList<PromotionProductInfo> PromotionProducts
        {
            get { return _PromotionProducts; }
            set { _PromotionProducts = value; }
        }

        public string Promotion_Site
        {
            get { return _Promotion_Site; }
            set { _Promotion_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }
    }

    public class PromotionProductInfo
    {
        private int _Promotion_Product_ID;
        private int _Promotion_Product_PromotionID;
        private int _Promotion_Product_Product_ID;

        public int Promotion_Product_ID
        {
            get { return _Promotion_Product_ID; }
            set { _Promotion_Product_ID = value; }
        }

        public int Promotion_Product_PromotionID
        {
            get { return _Promotion_Product_PromotionID; }
            set { _Promotion_Product_PromotionID = value; }
        }

        public int Promotion_Product_Product_ID
        {
            get { return _Promotion_Product_Product_ID; }
            set { _Promotion_Product_Product_ID = value; }
        }

    }

    public class PromotionGroupInfo
    {
        private int _Promotion_Group_ID;
        private string _Promotion_Group_Title;
        private string _Promotion_Group_PromotionID;
        private DateTime _Promotion_Group_Addtime;
        private string _Promotion_Group_Site;
        private IList<PromotionGroupPromotionInfo> _PromotionGroupPromotions;

        public int Promotion_Group_ID
        {
            get { return _Promotion_Group_ID; }
            set { _Promotion_Group_ID = value; }
        }

        public string Promotion_Group_Title
        {
            get { return _Promotion_Group_Title; }
            set { _Promotion_Group_Title = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Promotion_Group_PromotionID
        {
            get { return _Promotion_Group_PromotionID; }
            set { _Promotion_Group_PromotionID = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public DateTime Promotion_Group_Addtime
        {
            get { return _Promotion_Group_Addtime; }
            set { _Promotion_Group_Addtime = value; }
        }

        public string Promotion_Group_Site
        {
            get { return _Promotion_Group_Site; }
            set { _Promotion_Group_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public IList<PromotionGroupPromotionInfo> PromotionGroupPromotions
        {
            get { return _PromotionGroupPromotions; }
            set { _PromotionGroupPromotions = value; }
        }

    }

    public class PromotionGroupPromotionInfo
    {
        private int _Promotion_Group_Promotion_ID;
        private int _Promotion_Group_Promotion_GroupID;
        private int _Promotion_Group_Promotion_PromotionID;
        private int _Promotion_Group_Promotion_Sort;

        public int Promotion_Group_Promotion_ID
        {
            get { return _Promotion_Group_Promotion_ID; }
            set { _Promotion_Group_Promotion_ID = value; }
        }

        public int Promotion_Group_Promotion_GroupID
        {
            get { return _Promotion_Group_Promotion_GroupID; }
            set { _Promotion_Group_Promotion_GroupID = value; }
        }

        public int Promotion_Group_Promotion_PromotionID
        {
            get { return _Promotion_Group_Promotion_PromotionID; }
            set { _Promotion_Group_Promotion_PromotionID = value; }
        }

        public int Promotion_Group_Promotion_Sort
        {
            get { return _Promotion_Group_Promotion_Sort; }
            set { _Promotion_Group_Promotion_Sort = value; }
        }

    }

}
