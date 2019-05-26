using System;

namespace Glaer.Trade.B2C.Model
{
    public class Product_LabelInfo
    {
        private int _Product_Label_ID;
        private int _Product_Label_ProductID;
        private int _Product_Label_LabelID;

        public int Product_Label_ID
        {
            get { return _Product_Label_ID; }
            set { _Product_Label_ID = value; }
        }

        public int Product_Label_ProductID
        {
            get { return _Product_Label_ProductID; }
            set { _Product_Label_ProductID = value; }
        }

        public int Product_Label_LabelID
        {
            get { return _Product_Label_LabelID; }
            set { _Product_Label_LabelID = value; }
        }

    }
}
