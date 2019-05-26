using System;

namespace Glaer.Trade.B2C.Model
{
    public class ArticleCateInfo
    {
        private int _Article_Cate_ID;
        private int _Article_Cate_ParentID;
        private string _Article_Cate_Name;
        private int _Article_Cate_Sort;
        private string _Article_Cate_Site;

        public int Article_Cate_ID
        {
            get { return _Article_Cate_ID; }
            set { _Article_Cate_ID = value; }
        }

        public int Article_Cate_ParentID
        {
            get { return _Article_Cate_ParentID; }
            set { _Article_Cate_ParentID = value; }
        }

        public string Article_Cate_Name
        {
            get { return _Article_Cate_Name; }
            set { _Article_Cate_Name = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public int Article_Cate_Sort
        {
            get { return _Article_Cate_Sort; }
            set { _Article_Cate_Sort = value; }
        }

        public string Article_Cate_Site
        {
            get { return _Article_Cate_Site; }
            set { _Article_Cate_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

    }

    public class ArticleInfo
    {
        private int _Article_ID;
        private int _Article_CateID;
        private string _Article_Title;
        private string _Article_Source;
        private string _Article_Author;
        private string _Article_Img;
        private string _Article_Keyword;
        private string _Article_Intro;
        private string _Article_Content;
        private DateTime _Article_Addtime;
        private int _Article_Hits;
        private int _Article_IsRecommend;
        private int _Article_IsAudit;
        private int _Article_Sort;
        private string _Article_Site;

        public int Article_ID
        {
            get { return _Article_ID; }
            set { _Article_ID = value; }
        }

        public int Article_CateID
        {
            get { return _Article_CateID; }
            set { _Article_CateID = value; }
        }

        public string Article_Title
        {
            get { return _Article_Title; }
            set { _Article_Title = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Article_Source
        {
            get { return _Article_Source; }
            set { _Article_Source = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Article_Author
        {
            get { return _Article_Author; }
            set { _Article_Author = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Article_Img
        {
            get { return _Article_Img; }
            set { _Article_Img = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Article_Keyword
        {
            get { return _Article_Keyword; }
            set { _Article_Keyword = value.Length > 250 ? value.Substring(0, 250) : value.ToString(); }
        }

        public string Article_Intro
        {
            get { return _Article_Intro; }
            set { _Article_Intro = value.Length > 250 ? value.Substring(0, 250) : value.ToString(); }
        }

        public string Article_Content
        {
            get { return _Article_Content; }
            set { _Article_Content = value; }
        }

        public DateTime Article_Addtime
        {
            get { return _Article_Addtime; }
            set { _Article_Addtime = value; }
        }

        public int Article_Hits
        {
            get { return _Article_Hits; }
            set { _Article_Hits = value; }
        }

        public int Article_IsRecommend
        {
            get { return _Article_IsRecommend; }
            set { _Article_IsRecommend = value; }
        }

        public int Article_IsAudit
        {
            get { return _Article_IsAudit; }
            set { _Article_IsAudit = value; }
        }

        public int Article_Sort
        {
            get { return _Article_Sort; }
            set { _Article_Sort = value; }
        }

        public string Article_Site
        {
            get { return _Article_Site; }
            set { _Article_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

    }

}
