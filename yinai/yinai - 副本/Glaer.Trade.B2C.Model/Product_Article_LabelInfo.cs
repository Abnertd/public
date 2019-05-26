using System;

namespace Glaer.Trade.B2C.Model
{
    public class Product_Article_LabelInfo
    {
        private int _Product_Article_LabelID;
        private string _Product_Article_LabelName;

        public int Product_Article_LabelID
        {
            get { return _Product_Article_LabelID; }
            set { _Product_Article_LabelID = value; }
        }

        public string Product_Article_LabelName
        {
            get { return _Product_Article_LabelName; }
            set { _Product_Article_LabelName = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

    }
}
