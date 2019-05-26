using System;

namespace Glaer.Trade.B2C.Model
{
    public class SupplierShopInfo
    {
        private int _Shop_ID;
        private string _Shop_Code;
        private int _Shop_Type;
        private string _Shop_Name;
        private int _Shop_SupplierID;
        private string _Shop_Img;
        private int _Shop_Css;
        private int _Shop_Banner;
        private string _Shop_Banner_Title;
        private string _Shop_Banner_Title_Family;
        private int _Shop_Banner_Title_Size;
        private int _Shop_Banner_Title_LeftPadding;
        private string _Shop_banner_Title_color;
        private string _Shop_Banner_Img;
        private string _Shop_Domain;
        private int _Shop_Evaluate;
        private int _Shop_Recommend;
        private int _Shop_Status;
        private string _Shop_MainProduct;
        private string _Shop_SEO_Title;
        private string _Shop_SEO_Keyword;
        private string _Shop_SEO_Description;
        private DateTime _Shop_Addtime;
        private int _Shop_Hits;
        private string _Shop_Site;
        private int _Shop_Banner_IsActive;
        private int _Shop_Top_IsActive;
        private int _Shop_TopNav_IsActive;
        private int _Shop_Info_IsActive;
        private int _Shop_LeftSearch_IsActive;
        private int _Shop_LeftCate_IsActive;
        private int _Shop_LeftSale_IsActive;
        private int _Shop_Left_IsActive;
        private int _Shop_Right_IsActive;
        private int _Shop_RightProduct_IsActive;

        public int Shop_ID
        {
            get { return _Shop_ID; }
            set { _Shop_ID = value; }
        }

        public int Shop_Type
        {
            get { return _Shop_Type; }
            set { _Shop_Type = value; }
        }

        public string Shop_Code
        {
            get { return _Shop_Code; }
            set { _Shop_Code = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Shop_Name
        {
            get { return _Shop_Name; }
            set { _Shop_Name = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int Shop_SupplierID
        {
            get { return _Shop_SupplierID; }
            set { _Shop_SupplierID = value; }
        }

        public string Shop_Img
        {
            get { return _Shop_Img; }
            set { _Shop_Img = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int Shop_Css
        {
            get { return _Shop_Css; }
            set { _Shop_Css = value; }
        }

        public int Shop_Banner
        {
            get { return _Shop_Banner; }
            set { _Shop_Banner = value; }
        }

        public string Shop_Banner_Title
        {
            get { return _Shop_Banner_Title; }
            set { _Shop_Banner_Title = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Shop_Banner_Title_Family
        {
            get { return _Shop_Banner_Title_Family; }
            set { _Shop_Banner_Title_Family = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int Shop_Banner_Title_Size
        {
            get { return _Shop_Banner_Title_Size; }
            set { _Shop_Banner_Title_Size = value; }
        }

        public int Shop_Banner_Title_LeftPadding
        {
            get { return _Shop_Banner_Title_LeftPadding; }
            set { _Shop_Banner_Title_LeftPadding = value; }
        }

        public string Shop_banner_Title_color
        {
            get { return _Shop_banner_Title_color; }
            set { _Shop_banner_Title_color = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Shop_Banner_Img
        {
            get { return _Shop_Banner_Img; }
            set { _Shop_Banner_Img = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Shop_Domain
        {
            get { return _Shop_Domain; }
            set { _Shop_Domain = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Shop_MainProduct
        {
            get { return _Shop_MainProduct; }
            set { _Shop_MainProduct = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Shop_SEO_Title
        {
            get { return _Shop_SEO_Title; }
            set { _Shop_SEO_Title = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Shop_SEO_Keyword
        {
            get { return _Shop_SEO_Keyword; }
            set { _Shop_SEO_Keyword = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }

        public string Shop_SEO_Description
        {
            get { return _Shop_SEO_Description; }
            set { _Shop_SEO_Description = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }

        public DateTime Shop_Addtime
        {
            get { return _Shop_Addtime; }
            set { _Shop_Addtime = value; }
        }

        public int Shop_Evaluate
        {
            get { return _Shop_Evaluate; }
            set { _Shop_Evaluate = value; }
        }

        public int Shop_Recommend
        {
            get { return _Shop_Recommend; }
            set { _Shop_Recommend = value; }
        }

        public int Shop_Status
        {
            get { return _Shop_Status; }
            set { _Shop_Status = value; }
        }

        public int Shop_Hits
        {
            get { return _Shop_Hits; }
            set { _Shop_Hits = value; }
        }

        public string Shop_Site
        {
            get { return _Shop_Site; }
            set { _Shop_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int Shop_Banner_IsActive
        {
            get { return _Shop_Banner_IsActive; }
            set { _Shop_Banner_IsActive = value; }
        }

        public int Shop_Top_IsActive
        {
            get { return _Shop_Top_IsActive; }
            set { _Shop_Top_IsActive = value; }
        }

        public int Shop_TopNav_IsActive
        {
            get { return _Shop_TopNav_IsActive; }
            set { _Shop_TopNav_IsActive = value; }
        }

        public int Shop_Info_IsActive
        {
            get { return _Shop_Info_IsActive; }
            set { _Shop_Info_IsActive = value; }
        }

        public int Shop_LeftSearch_IsActive
        {
            get { return _Shop_LeftSearch_IsActive; }
            set { _Shop_LeftSearch_IsActive = value; }
        }

        public int Shop_LeftCate_IsActive
        {
            get { return _Shop_LeftCate_IsActive; }
            set { _Shop_LeftCate_IsActive = value; }
        }

        public int Shop_LeftSale_IsActive
        {
            get { return _Shop_LeftSale_IsActive; }
            set { _Shop_LeftSale_IsActive = value; }
        }

        public int Shop_Left_IsActive
        {
            get { return _Shop_Left_IsActive; }
            set { _Shop_Left_IsActive = value; }
        }

        public int Shop_Right_IsActive
        {
            get { return _Shop_Right_IsActive; }
            set { _Shop_Right_IsActive = value; }
        }

        public int Shop_RightProduct_IsActive
        {
            get { return _Shop_RightProduct_IsActive; }
            set { _Shop_RightProduct_IsActive = value; }
        } 

    }

    public class SupplierShopDomainInfo
    {
        private int _Shop_Domain_ID;
        private int _Shop_Domain_SupplierID;
        private int _Shop_Domain_Type;
        private int _Shop_Domain_ShopID;
        private string _Shop_Domain_Name;
        private int _Shop_Domain_Status;
        private DateTime _Shop_Domain_Addtime;
        private string _Shop_Domain_Site;

        public int Shop_Domain_ID
        {
            get { return _Shop_Domain_ID; }
            set { _Shop_Domain_ID = value; }
        }

        public int Shop_Domain_SupplierID
        {
            get { return _Shop_Domain_SupplierID; }
            set { _Shop_Domain_SupplierID = value; }
        }

        public int Shop_Domain_Type
        {
            get { return _Shop_Domain_Type; }
            set { _Shop_Domain_Type = value; }
        }

        public int Shop_Domain_ShopID
        {
            get { return _Shop_Domain_ShopID; }
            set { _Shop_Domain_ShopID = value; }
        }

        public string Shop_Domain_Name
        {
            get { return _Shop_Domain_Name; }
            set { _Shop_Domain_Name = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int Shop_Domain_Status
        {
            get { return _Shop_Domain_Status; }
            set { _Shop_Domain_Status = value; }
        }

        public DateTime Shop_Domain_Addtime
        {
            get { return _Shop_Domain_Addtime; }
            set { _Shop_Domain_Addtime = value; }
        }

        public string Shop_Domain_Site
        {
            get { return _Shop_Domain_Site; }
            set { _Shop_Domain_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

    }
}
