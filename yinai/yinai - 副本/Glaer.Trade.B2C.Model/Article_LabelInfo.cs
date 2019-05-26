using System;

namespace Glaer.Trade.B2C.Model
{
    public class Article_LabelInfo
    {
        private int _Article_Label_ID;
        private int _Article_Label_ArticleID;
        private int _Article_Label_LabelID;

        public int Article_Label_ID
        {
            get { return _Article_Label_ID; }
            set { _Article_Label_ID = value; }
        }

        public int Article_Label_ArticleID
        {
            get { return _Article_Label_ArticleID; }
            set { _Article_Label_ArticleID = value; }
        }

        public int Article_Label_LabelID
        {
            get { return _Article_Label_LabelID; }
            set { _Article_Label_LabelID = value; }
        }

    }
}
