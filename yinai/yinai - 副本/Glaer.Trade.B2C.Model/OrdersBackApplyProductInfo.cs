using System;
using System.Collections.Generic;
using System.Text;

namespace Glaer.Trade.B2C.Model
{
    public class OrdersBackApplyProductInfo
    {
        private int _Orders_BackApply_Product_ID;
        private int _Orders_BackApply_Product_ProductID;
        private int _Orders_BackApply_Product_ApplyID;
        private int _Orders_BackApply_Product_ApplyAmount;

        public int Orders_BackApply_Product_ID
        {
            get { return _Orders_BackApply_Product_ID; }
            set { _Orders_BackApply_Product_ID = value; }
        }

        public int Orders_BackApply_Product_ProductID
        {
            get { return _Orders_BackApply_Product_ProductID; }
            set { _Orders_BackApply_Product_ProductID = value; }
        }

        public int Orders_BackApply_Product_ApplyID
        {
            get { return _Orders_BackApply_Product_ApplyID; }
            set { _Orders_BackApply_Product_ApplyID = value; }
        }

        public int Orders_BackApply_Product_ApplyAmount
        {
            get { return _Orders_BackApply_Product_ApplyAmount; }
            set { _Orders_BackApply_Product_ApplyAmount = value; }
        }

    }
}
