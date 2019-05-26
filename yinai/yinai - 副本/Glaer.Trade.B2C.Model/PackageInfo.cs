using System;
using System.Collections.Generic;

namespace Glaer.Trade.B2C.Model
{
    /// <summary>
    /// 捆绑商品
    /// </summary>
    public class PackageInfo
    {
        private int _Package_ID;
        private string _Package_Name;
        private int _Package_IsInsale;
        private int _Package_StockAmount;
        private int _Package_Weight;
        private double _Package_Price;
        private int _Package_Sort;
        private DateTime _Package_Addtime;
        private string _Package_Site;
        private IList<PackageProductInfo> _PackageProductInfos = null;
        private int _Package_SupplierID = 0;

        public int Package_ID
        {
            get { return _Package_ID; }
            set { _Package_ID = value; }
        }

        public string Package_Name
        {
            get { return _Package_Name; }
            set { _Package_Name = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public int Package_IsInsale
        {
            get { return _Package_IsInsale; }
            set { _Package_IsInsale = value; }
        }

        public int Package_StockAmount
        {
            get { return _Package_StockAmount; }
            set { _Package_StockAmount = value; }
        }

        public int Package_Weight
        {
            get { return _Package_Weight; }
            set { _Package_Weight = value; }
        }

        public double Package_Price
        {
            get { return _Package_Price; }
            set { _Package_Price = value; }
        }

        public int Package_Sort
        {
            get { return _Package_Sort; }
            set { _Package_Sort = value; }
        }

        public DateTime Package_Addtime
        {
            get { return _Package_Addtime; }
            set { _Package_Addtime = value; }
        }

        public string Package_Site
        {
            get { return _Package_Site; }
            set { _Package_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public IList<PackageProductInfo> PackageProductInfos
        {
            get { return _PackageProductInfos; }
            set { _PackageProductInfos = value; }
        }

        public int Package_SupplierID
        {
            get { return _Package_SupplierID; }
            set { _Package_SupplierID = value; }
        }

    }

    /// <summary>
    /// 捆绑对应商品
    /// </summary>
    public class PackageProductInfo
    {
        private int _Package_Product_ID;
        private int _Package_Product_PackageID;
        private int _Package_Product_ProductID;
        private int _Package_Product_Amount;

        public int Package_Product_ID
        {
            get { return _Package_Product_ID; }
            set { _Package_Product_ID = value; }
        }

        public int Package_Product_PackageID
        {
            get { return _Package_Product_PackageID; }
            set { _Package_Product_PackageID = value; }
        }

        public int Package_Product_ProductID
        {
            get { return _Package_Product_ProductID; }
            set { _Package_Product_ProductID = value; }
        }

        public int Package_Product_Amount
        {
            get { return _Package_Product_Amount; }
            set { _Package_Product_Amount = value; }
        }

    }



}
