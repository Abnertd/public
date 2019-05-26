using System;

namespace Glaer.Trade.B2C.Model
{
    public class CategoryInfo
    {
        private int _Cate_ID;
        private int _Cate_ParentID;
        private string _Cate_Name;
        private int _Cate_TypeID;
        private string _Cate_Img;
        private int _Cate_ProductTypeID;
        private int _Cate_Sort;
        private int _Cate_IsFrequently;
        private int _Cate_IsActive;
        private int _Cate_Count_Brand;
        private int _Cate_Count_Product;
        private string _Cate_SEO_Path;
        private string _Cate_SEO_Title;
        private string _Cate_SEO_Keyword;
        private string _Cate_SEO_Description;
        private string _Cate_Site;

        public int Cate_ID
        {
            get { return _Cate_ID; }
            set { _Cate_ID = value; }
        }

        public int Cate_ParentID
        {
            get { return _Cate_ParentID; }
            set { _Cate_ParentID = value; }
        }

        public string Cate_Name
        {
            get { return _Cate_Name; }
            set { _Cate_Name = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public int Cate_TypeID
        {
            get { return _Cate_TypeID; }
            set { _Cate_TypeID = value; }
        }

        public string Cate_Img
        {
            get { return _Cate_Img; }
            set { _Cate_Img = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public int Cate_ProductTypeID
        {
            get { return _Cate_ProductTypeID; }
            set { _Cate_ProductTypeID = value; }
        }

        public int Cate_Sort
        {
            get { return _Cate_Sort; }
            set { _Cate_Sort = value; }
        }

        public int Cate_IsFrequently
        {
            get { return _Cate_IsFrequently; }
            set { _Cate_IsFrequently = value; }
        }

        public int Cate_IsActive
        {
            get { return _Cate_IsActive; }
            set { _Cate_IsActive = value; }
        }

        public int Cate_Count_Brand
        {
            get { return _Cate_Count_Brand; }
            set { _Cate_Count_Brand = value; }
        }

        public int Cate_Count_Product
        {
            get { return _Cate_Count_Product; }
            set { _Cate_Count_Product = value; }
        }

        public string Cate_SEO_Path
        {
            get { return _Cate_SEO_Path; }
            set { _Cate_SEO_Path = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Cate_SEO_Title
        {
            get { return _Cate_SEO_Title; }
            set { _Cate_SEO_Title = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }

        public string Cate_SEO_Keyword
        {
            get { return _Cate_SEO_Keyword; }
            set { _Cate_SEO_Keyword = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }

        public string Cate_SEO_Description
        {
            get { return _Cate_SEO_Description; }
            set { _Cate_SEO_Description = value.Length > 500 ? value.Substring(0, 500) : value.ToString(); }
        }

        public string Cate_Site
        {
            get { return _Cate_Site; }
            set { _Cate_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

    } 
}