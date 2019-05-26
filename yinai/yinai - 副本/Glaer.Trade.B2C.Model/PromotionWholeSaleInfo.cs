using System;
using System.Collections.Generic;
using System.Text;

namespace Glaer.Trade.B2C.Model
{
    public class PromotionWholeSaleGroupInfo
    {
        private int _Promotion_WholeSale_Group_ID;
        private string _Promotion_WholeSale_Group_Name;
        private string _Promotion_WholeSale_Group_Site;

        public int Promotion_WholeSale_Group_ID
        {
            get { return _Promotion_WholeSale_Group_ID; }
            set { _Promotion_WholeSale_Group_ID = value; }
        }

        public string Promotion_WholeSale_Group_Name
        {
            get { return _Promotion_WholeSale_Group_Name; }
            set { _Promotion_WholeSale_Group_Name = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Promotion_WholeSale_Group_Site
        {
            get { return _Promotion_WholeSale_Group_Site; }
            set { _Promotion_WholeSale_Group_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

    }

    public class PromotionWholeSaleInfo
    {
        private int _Promotion_WholeSale_ID;
        private int _Promotion_WholeSale_GroupID;
        private int _Promotion_WholeSale_ProductID;
        private double _Promotion_WholeSale_Price;
        private int _Promotion_WholeSale_MinAmount;
        private string _Promotion_WholeSale_Site;

        public int Promotion_WholeSale_ID
        {
            get { return _Promotion_WholeSale_ID; }
            set { _Promotion_WholeSale_ID = value; }
        }

        public int Promotion_WholeSale_GroupID
        {
            get { return _Promotion_WholeSale_GroupID; }
            set { _Promotion_WholeSale_GroupID = value; }
        }

        public int Promotion_WholeSale_ProductID
        {
            get { return _Promotion_WholeSale_ProductID; }
            set { _Promotion_WholeSale_ProductID = value; }
        }

        public double Promotion_WholeSale_Price
        {
            get { return _Promotion_WholeSale_Price; }
            set { _Promotion_WholeSale_Price = value; }
        }

        public int Promotion_WholeSale_MinAmount
        {
            get { return _Promotion_WholeSale_MinAmount; }
            set { _Promotion_WholeSale_MinAmount = value; }
        }

        public string Promotion_WholeSale_Site
        {
            get { return _Promotion_WholeSale_Site; }
            set { _Promotion_WholeSale_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

    }

}
