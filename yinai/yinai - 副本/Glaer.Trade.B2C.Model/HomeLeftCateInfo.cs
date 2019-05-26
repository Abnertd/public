using System;

namespace Glaer.Trade.B2C.Model
{
    public class HomeLeftCateInfo
    {
        private int _Home_Left_Cate_ID;
        private int _Home_Left_Cate_ParentID;
        private int _Home_Left_Cate_CateID;
        private string _Home_Left_Cate_Name;
        private string _Home_Left_Cate_URL;
        private string _Home_Left_Cate_Img;
        private int _Home_Left_Cate_Sort;
        private int _Home_Left_Cate_Active;
        private string _Home_Left_Cate_Site;

        public int Home_Left_Cate_ID
        {
            get { return _Home_Left_Cate_ID; }
            set { _Home_Left_Cate_ID = value; }
        }

        public int Home_Left_Cate_ParentID
        {
            get { return _Home_Left_Cate_ParentID; }
            set { _Home_Left_Cate_ParentID = value; }
        }

        public int Home_Left_Cate_CateID
        {
            get { return _Home_Left_Cate_CateID; }
            set { _Home_Left_Cate_CateID = value; }
        }

        public string Home_Left_Cate_Name
        {
            get { return _Home_Left_Cate_Name; }
            set { _Home_Left_Cate_Name = value.Length > 30 ? value.Substring(0, 30) : value.ToString(); }
        }

        public string Home_Left_Cate_URL
        {
            get { return _Home_Left_Cate_URL; }
            set { _Home_Left_Cate_URL = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }

        public string Home_Left_Cate_Img
        {
            get { return _Home_Left_Cate_Img; }
            set { _Home_Left_Cate_Img = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public int Home_Left_Cate_Sort
        {
            get { return _Home_Left_Cate_Sort; }
            set { _Home_Left_Cate_Sort = value; }
        }

        public int Home_Left_Cate_Active
        {
            get { return _Home_Left_Cate_Active; }
            set { _Home_Left_Cate_Active = value; }
        }

        public string Home_Left_Cate_Site
        {
            get { return _Home_Left_Cate_Site; }
            set { _Home_Left_Cate_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

    }
}
