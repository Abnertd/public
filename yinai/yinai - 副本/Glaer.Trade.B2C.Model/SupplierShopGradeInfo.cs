using System;
using System.Collections.Generic;
using System.Text;

namespace Glaer.Trade.B2C.Model
{
    public class SupplierShopGradeInfo
    {
        private int _Shop_Grade_ID;
        private string _Shop_Grade_Name;
        private int _Shop_Grade_ProductLimit;
        private double _Shop_Grade_DefaultCommission;
        private int _Shop_Grade_IsActive;
        private string _Shop_Grade_Site;

        public int Shop_Grade_ID
        {
            get { return _Shop_Grade_ID; }
            set { _Shop_Grade_ID = value; }
        }

        public string Shop_Grade_Name
        {
            get { return _Shop_Grade_Name; }
            set { _Shop_Grade_Name = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int Shop_Grade_ProductLimit
        {
            get { return _Shop_Grade_ProductLimit; }
            set { _Shop_Grade_ProductLimit = value; }
        }

        public double Shop_Grade_DefaultCommission
        {
            get { return _Shop_Grade_DefaultCommission; }
            set { _Shop_Grade_DefaultCommission = value; }
        }

        public int Shop_Grade_IsActive
        {
            get { return _Shop_Grade_IsActive; }
            set { _Shop_Grade_IsActive = value; }
        }

        public string Shop_Grade_Site
        {
            get { return _Shop_Grade_Site; }
            set { _Shop_Grade_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

    }
}
