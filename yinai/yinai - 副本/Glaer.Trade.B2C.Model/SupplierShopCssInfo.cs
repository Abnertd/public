using System;

namespace Glaer.Trade.B2C.Model
{
    public class SupplierShopCssInfo
    {
        private int _Shop_Css_ID;
        private string _Shop_Css_Title;
        private string _Shop_Css_Target;
        private string _Shop_Css_GapColor;
        private string _Shop_Css_Img;
        private int _Shop_Css_IsActive;
        private string _Shop_Css_Site;

        public int Shop_Css_ID
        {
            get { return _Shop_Css_ID; }
            set { _Shop_Css_ID = value; }
        }

        public string Shop_Css_Title
        {
            get { return _Shop_Css_Title; }
            set { _Shop_Css_Title = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Shop_Css_Target
        {
            get { return _Shop_Css_Target; }
            set { _Shop_Css_Target = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Shop_Css_GapColor
        {
            get { return _Shop_Css_GapColor; }
            set { _Shop_Css_GapColor = value.Length > 20 ? value.Substring(0, 20) : value.ToString(); }
        }

        public string Shop_Css_Img
        {
            get { return _Shop_Css_Img; }
            set { _Shop_Css_Img = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public int Shop_Css_IsActive
        {
            get { return _Shop_Css_IsActive; }
            set { _Shop_Css_IsActive = value; }
        }

        public string Shop_Css_Site
        {
            get { return _Shop_Css_Site; }
            set { _Shop_Css_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

    }

    public class SupplierShopCssRelateSupplierInfo
    {
        private int _Relate_ID;
        private int _Relate_ShopCssID;
        private int _Relate_SupplierID;

        public int Relate_ID
        {
            get { return _Relate_ID; }
            set { _Relate_ID = value; }
        }

        public int Relate_ShopCssID
        {
            get { return _Relate_ShopCssID; }
            set { _Relate_ShopCssID = value; }
        }

        public int Relate_SupplierID
        {
            get { return _Relate_SupplierID; }
            set { _Relate_SupplierID = value; }
        }

    }
}
