using System;

namespace Glaer.Trade.B2C.Model
{
    public class PayWayInfo
    {
        private int _Pay_Way_ID;
        private int _Pay_Way_Type;
        private string _Pay_Way_Name;
        private int _Pay_Way_Sort;
        private int _Pay_Way_Status;
        private int _Pay_Way_Cod;
        private string _Pay_Way_Img;
        private string _Pay_Way_Intro;
        private string _Pay_Way_Site;

        public int Pay_Way_ID
        {
            get { return _Pay_Way_ID; }
            set { _Pay_Way_ID = value; }
        }

        public int Pay_Way_Type
        {
            get { return _Pay_Way_Type; }
            set { _Pay_Way_Type = value; }
        }

        public string Pay_Way_Name
        {
            get { return _Pay_Way_Name; }
            set { _Pay_Way_Name = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int Pay_Way_Sort
        {
            get { return _Pay_Way_Sort; }
            set { _Pay_Way_Sort = value; }
        }

        public int Pay_Way_Status
        {
            get { return _Pay_Way_Status; }
            set { _Pay_Way_Status = value; }
        }

        public int Pay_Way_Cod
        {
            get { return _Pay_Way_Cod; }
            set { _Pay_Way_Cod = value; }
        }

        public string Pay_Way_Img
        {
            get { return _Pay_Way_Img; }
            set { _Pay_Way_Img = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Pay_Way_Intro
        {
            get { return _Pay_Way_Intro; }
            set { _Pay_Way_Intro = value.Length > 1000 ? value.Substring(0, 1000) : value.ToString(); }
        }

        public string Pay_Way_Site
        {
            get { return _Pay_Way_Site; }
            set { _Pay_Way_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

    }
}
