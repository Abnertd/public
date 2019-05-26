using System;
using System.Collections.Generic;

namespace Glaer.Trade.B2C.Model
{
    public class ProductTypeInfo
    {
        private int _ProductType_ID;
        private string _ProductType_Name;
        private int _ProductType_Sort;
        private int _ProductType_IsActive;
        private string _ProductType_Site;
        IList<BrandInfo> _BrandInfos = new List<BrandInfo>();
        //新加
        IList<SupplierInfo> _SupplierInfos = new List<SupplierInfo>();


        IList<ProductTypeExtendInfo> _ProductTypeExtendInfos=new List<ProductTypeExtendInfo>();

        public int ProductType_ID
        {
            get { return _ProductType_ID; }
            set { _ProductType_ID = value; }
        }

        public string ProductType_Name
        {
            get { return _ProductType_Name; }
            set { _ProductType_Name = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int ProductType_Sort
        {
            get { return _ProductType_Sort; }
            set { _ProductType_Sort = value; }
        }

        public int ProductType_IsActive
        {
            get { return _ProductType_IsActive; }
            set { _ProductType_IsActive = value; }
        }

        public string ProductType_Site
        {
            get { return _ProductType_Site; }
            set { _ProductType_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }


        public IList<SupplierInfo> SupplierInfos
        {
            get { return _SupplierInfos; }
            set { _SupplierInfos = value; }
        }




        public IList<BrandInfo> BrandInfos
        {
            get { return _BrandInfos; }
            set { _BrandInfos = value; }
        }

        public IList<ProductTypeExtendInfo> ProductTypeExtendInfos
        {
            get { return _ProductTypeExtendInfos; }
            set { _ProductTypeExtendInfos = value; }
        }

    }
}
