using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glaer.Trade.B2C.Model
{
    public class SupplierFavoritesInfo
    {
        private int _Supplier_Favorites_ID;
        private int _Supplier_Favorites_SupplierID;
        private int _Supplier_Favorites_Type;
        private int _Supplier_Favorites_TargetID;
        private DateTime _Supplier_Favorites_Addtime;
        private string _Supplier_Favorites_Site;

        public int Supplier_Favorites_ID
        {
            get { return _Supplier_Favorites_ID; }
            set { _Supplier_Favorites_ID = value; }
        }

        public int Supplier_Favorites_SupplierID
        {
            get { return _Supplier_Favorites_SupplierID; }
            set { _Supplier_Favorites_SupplierID = value; }
        }

        public int Supplier_Favorites_Type
        {
            get { return _Supplier_Favorites_Type; }
            set { _Supplier_Favorites_Type = value; }
        }

        public int Supplier_Favorites_TargetID
        {
            get { return _Supplier_Favorites_TargetID; }
            set { _Supplier_Favorites_TargetID = value; }
        }

        public DateTime Supplier_Favorites_Addtime
        {
            get { return _Supplier_Favorites_Addtime; }
            set { _Supplier_Favorites_Addtime = value; }
        }

        public string Supplier_Favorites_Site
        {
            get { return _Supplier_Favorites_Site; }
            set { _Supplier_Favorites_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

    }
}
