using System;
using System.Collections.Generic;
using System.Text;

namespace Glaer.Trade.B2C.Model
{
    public class ProductTagCategoryInfo
    {
        private int _Tag_Cate_ID;
        private string _Tag_Cate_Name;
        private int _Tag_Cate_IsActive;
        private int _Tag_Cate_Sort;
        private string _Tag_Cate_Site;

        public int Tag_Cate_ID
        {
            get { return _Tag_Cate_ID; }
            set { _Tag_Cate_ID = value; }
        }

        public string Tag_Cate_Name
        {
            get { return _Tag_Cate_Name; }
            set { _Tag_Cate_Name = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int Tag_Cate_IsActive
        {
            get { return _Tag_Cate_IsActive; }
            set { _Tag_Cate_IsActive = value; }
        }

        public int Tag_Cate_Sort
        {
            get { return _Tag_Cate_Sort; }
            set { _Tag_Cate_Sort = value; }
        }

        public string Tag_Cate_Site
        {
            get { return _Tag_Cate_Site; }
            set { _Tag_Cate_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

    }
}
