using System;

namespace Glaer.Trade.B2C.Model
{
    public class NoticeCateInfo
    {
        private int _Notice_Cate_ID;
        private string _Notice_Cate_Name;
        private int _Notice_Cate_Sort;
        private string _Notice_Cate_Site;

        public int Notice_Cate_ID
        {
            get { return _Notice_Cate_ID; }
            set { _Notice_Cate_ID = value; }
        }

        public string Notice_Cate_Name
        {
            get { return _Notice_Cate_Name; }
            set { _Notice_Cate_Name = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int Notice_Cate_Sort
        {
            get { return _Notice_Cate_Sort; }
            set { _Notice_Cate_Sort = value; }
        }

        public string Notice_Cate_Site
        {
            get { return _Notice_Cate_Site; }
            set { _Notice_Cate_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

    }

    public class NoticeInfo
    {
        private int _Notice_ID;
        private int _Notice_Cate;
        private int _Notice_IsHot;
        private int _Notice_IsAudit;
        private int _Notice_SysUserID;
        private int _Notice_SellerID;
        private string _Notice_Title;
        private string _Notice_Content;
        private DateTime _Notice_Addtime;
        private string _Notice_Site;

        public int Notice_ID
        {
            get { return _Notice_ID; }
            set { _Notice_ID = value; }
        }

        public int Notice_Cate
        {
            get { return _Notice_Cate; }
            set { _Notice_Cate = value; }
        }

        public int Notice_IsHot
        {
            get { return _Notice_IsHot; }
            set { _Notice_IsHot = value; }
        }

        public int Notice_IsAudit
        {
            get { return _Notice_IsAudit; }
            set { _Notice_IsAudit = value; }
        }

        public int Notice_SysUserID
        {
            get { return _Notice_SysUserID; }
            set { _Notice_SysUserID = value; }
        }

        public int Notice_SellerID
        {
            get { return _Notice_SellerID; }
            set { _Notice_SellerID = value; }
        }

        public string Notice_Title
        {
            get { return _Notice_Title; }
            set { _Notice_Title = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Notice_Content
        {
            get { return _Notice_Content; }
            set { _Notice_Content = value; }
        }

        public DateTime Notice_Addtime
        {
            get { return _Notice_Addtime; }
            set { _Notice_Addtime = value; }
        }

        public string Notice_Site
        {
            get { return _Notice_Site; }
            set { _Notice_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

    }

}
