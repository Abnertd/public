using System;

namespace Glaer.Trade.B2C.Model
{
    public class FriendlyLinkCateInfo
    {
        private int _FriendlyLink_Cate_ID;
        private string _FriendlyLink_Cate_Name;
        private int _FriendlyLink_Cate_Sort;
        private string _FriendlyLink_Cate_Site;

        public int FriendlyLink_Cate_ID
        {
            get { return _FriendlyLink_Cate_ID; }
            set { _FriendlyLink_Cate_ID = value; }
        }

        public string FriendlyLink_Cate_Name
        {
            get { return _FriendlyLink_Cate_Name; }
            set { _FriendlyLink_Cate_Name = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public int FriendlyLink_Cate_Sort
        {
            get { return _FriendlyLink_Cate_Sort; }
            set { _FriendlyLink_Cate_Sort = value; }
        }

        public string FriendlyLink_Cate_Site
        {
            get { return _FriendlyLink_Cate_Site; }
            set { _FriendlyLink_Cate_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

    }

    public class FriendlyLinkInfo
    {
        private int _FriendlyLink_ID;
        private int _FriendlyLink_CateID;
        private string _FriendlyLink_Name;
        private string _FriendlyLink_Img;
        private string _FriendlyLink_URL;
        private int _FriendlyLink_IsActive;
        private int _FriendlyLink_IsImg;
        private string _FriendlyLink_Site;
        private int _FriendlyLink_Sort;

        public int FriendlyLink_ID
        {
            get { return _FriendlyLink_ID; }
            set { _FriendlyLink_ID = value; }
        }

        public int FriendlyLink_CateID
        {
            get { return _FriendlyLink_CateID; }
            set { _FriendlyLink_CateID = value; }
        }

        public string FriendlyLink_Name
        {
            get { return _FriendlyLink_Name; }
            set { _FriendlyLink_Name = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string FriendlyLink_Img
        {
            get { return _FriendlyLink_Img; }
            set { _FriendlyLink_Img = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }

        public string FriendlyLink_URL
        {
            get { return _FriendlyLink_URL; }
            set { _FriendlyLink_URL = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }

        public int FriendlyLink_IsActive
        {
            get { return _FriendlyLink_IsActive; }
            set { _FriendlyLink_IsActive = value; }
        }

        public int FriendlyLink_IsImg
        {
            get { return _FriendlyLink_IsImg; }
            set { _FriendlyLink_IsImg = value; }
        }

        public string FriendlyLink_Site
        {
            get { return _FriendlyLink_Site; }
            set { _FriendlyLink_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int FriendlyLink_Sort
        {
            get { return _FriendlyLink_Sort; }
            set { _FriendlyLink_Sort = value; }
        }

    }
}
