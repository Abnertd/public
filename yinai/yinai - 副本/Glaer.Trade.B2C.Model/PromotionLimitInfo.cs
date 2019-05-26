using System;
using System.Collections.Generic;
using System.Text;

namespace Glaer.Trade.B2C.Model
{
    public class PromotionLimitGroupInfo
    {
        private int _Promotion_Limit_Group_ID;
        private string _Promotion_Limit_Group_Name;
        private string _Promotion_Limit_Group_Site;

        public int Promotion_Limit_Group_ID
        {
            get { return _Promotion_Limit_Group_ID; }
            set { _Promotion_Limit_Group_ID = value; }
        }

        public string Promotion_Limit_Group_Name
        {
            get { return _Promotion_Limit_Group_Name; }
            set { _Promotion_Limit_Group_Name = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Promotion_Limit_Group_Site
        {
            get { return _Promotion_Limit_Group_Site; }
            set { _Promotion_Limit_Group_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

    }

    public class PromotionLimitInfo
    {
        private int _Promotion_Limit_ID;
        private int _Promotion_Limit_GroupID;
        private int _Promotion_Limit_ProductID;
        private double _Promotion_Limit_Price;
        private int _Promotion_Limit_Amount;
        private int _Promotion_Limit_Limit;
        private DateTime _Promotion_Limit_Starttime;
        private DateTime _Promotion_Limit_Endtime;
        private string _Promotion_Limit_Site;
        private IList<PromotionLimitMemberGradeInfo> _PromotionLimitMemberGrades;

        public int Promotion_Limit_ID
        {
            get { return _Promotion_Limit_ID; }
            set { _Promotion_Limit_ID = value; }
        }

        public int Promotion_Limit_GroupID
        {
            get { return _Promotion_Limit_GroupID; }
            set { _Promotion_Limit_GroupID = value; }
        }

        public int Promotion_Limit_ProductID
        {
            get { return _Promotion_Limit_ProductID; }
            set { _Promotion_Limit_ProductID = value; }
        }

        public double Promotion_Limit_Price
        {
            get { return _Promotion_Limit_Price; }
            set { _Promotion_Limit_Price = value; }
        }

        public int Promotion_Limit_Amount
        {
            get { return _Promotion_Limit_Amount; }
            set { _Promotion_Limit_Amount = value; }
        }

        public int Promotion_Limit_Limit
        {
            get { return _Promotion_Limit_Limit; }
            set { _Promotion_Limit_Limit = value; }
        }

        public DateTime Promotion_Limit_Starttime
        {
            get { return _Promotion_Limit_Starttime; }
            set { _Promotion_Limit_Starttime = value; }
        }

        public DateTime Promotion_Limit_Endtime
        {
            get { return _Promotion_Limit_Endtime; }
            set { _Promotion_Limit_Endtime = value; }
        }

        public string Promotion_Limit_Site
        {
            get { return _Promotion_Limit_Site; }
            set { _Promotion_Limit_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public IList<PromotionLimitMemberGradeInfo> PromotionLimitMemberGrades
        {
            get { return _PromotionLimitMemberGrades; }
            set { _PromotionLimitMemberGrades = value; }
        }

    }

    public class PromotionLimitMemberGradeInfo
    {
        private int _Promotion_Limit_MemberGrade_ID;
        private int _Promotion_Limit_MemberGrade_LimitID;
        private int _Promotion_Limit_MemberGrade_Grade;

        public int Promotion_Limit_MemberGrade_ID
        {
            get { return _Promotion_Limit_MemberGrade_ID; }
            set { _Promotion_Limit_MemberGrade_ID = value; }
        }

        public int Promotion_Limit_MemberGrade_LimitID
        {
            get { return _Promotion_Limit_MemberGrade_LimitID; }
            set { _Promotion_Limit_MemberGrade_LimitID = value; }
        }

        public int Promotion_Limit_MemberGrade_Grade
        {
            get { return _Promotion_Limit_MemberGrade_Grade; }
            set { _Promotion_Limit_MemberGrade_Grade = value; }
        }

    }
}
