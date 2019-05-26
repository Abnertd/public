using System;


namespace Glaer.Trade.B2C.Model
{
    public class SupplierProductCategoryInfo
    {
        private int _Supplier_Product_Cate_ID;
        private int _Supplier_Product_Cate_CateID;
        private int _Supplier_Product_Cate_ProductID;

        public int Supplier_Product_Cate_ID
        {
            get { return _Supplier_Product_Cate_ID; }
            set { _Supplier_Product_Cate_ID = value; }
        }

        public int Supplier_Product_Cate_CateID
        {
            get { return _Supplier_Product_Cate_CateID; }
            set { _Supplier_Product_Cate_CateID = value; }
        }

        public int Supplier_Product_Cate_ProductID
        {
            get { return _Supplier_Product_Cate_ProductID; }
            set { _Supplier_Product_Cate_ProductID = value; }
        }

    }
}
