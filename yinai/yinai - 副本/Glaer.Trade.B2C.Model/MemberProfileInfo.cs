using System;

namespace Glaer.Trade.B2C.Model
{
    public class MemberProfileInfo
    {
        private int _Member_Profile_ID;
        private int _Member_Profile_MemberID;
        private string _Member_Name;
        private int _Member_Sex;
        private DateTime _Member_Birthday;
        private int _Member_Occupational;
        private int _Member_Education;
        private int _Member_Income;
        private string _Member_StreetAddress;
        private string _Member_County;
        private string _Member_City;
        private string _Member_State;
        private string _Member_Country;
        private string _Member_Zip;
        private string _Member_Phone_Countrycode;
        private string _Member_Phone_Areacode;
        private string _Member_Phone_Number;
        private string _Member_Mobile;
        private string _Member_Company;
        private string _Member_Fax;
        private string _Member_QQ;
        private string _Member_OrganizationCode;
        private string _Member_BusinessCode;
        private string _Member_SealImg;
        private string _Member_Corporate;
        private string _Member_CorporateMobile;
        private double _Member_RegisterFunds;
        private string _Member_TaxationCode;
        private string _Member_BankAccountCode;
        private string _Member_HeadImg;
        private string _Member_RealName;
        private string _Member_UniformSocial_Number;

        public int Member_Profile_ID
        {
            get { return _Member_Profile_ID; }
            set { _Member_Profile_ID = value; }
        }

        public int Member_Profile_MemberID
        {
            get { return _Member_Profile_MemberID; }
            set { _Member_Profile_MemberID = value; }
        }

        public string Member_Name
        {
            get { return _Member_Name; }
            set { _Member_Name = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int Member_Sex
        {
            get { return _Member_Sex; }
            set { _Member_Sex = value; }
        }

        public DateTime Member_Birthday
        {
            get { return _Member_Birthday; }
            set { _Member_Birthday = value; }
        }

        public int Member_Occupational
        {
            get { return _Member_Occupational; }
            set { _Member_Occupational = value; }
        }

        public int Member_Education
        {
            get { return _Member_Education; }
            set { _Member_Education = value; }
        }

        public int Member_Income
        {
            get { return _Member_Income; }
            set { _Member_Income = value; }
        }

        public string Member_StreetAddress
        {
            get { return _Member_StreetAddress; }
            set { _Member_StreetAddress = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Member_County
        {
            get { return _Member_County; }
            set { _Member_County = value; }
        }

        public string Member_City
        {
            get { return _Member_City; }
            set { _Member_City = value; }
        }

        public string Member_State
        {
            get { return _Member_State; }
            set { _Member_State = value; }
        }

        public string Member_Country
        {
            get { return _Member_Country; }
            set { _Member_Country = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Member_Zip
        {
            get { return _Member_Zip; }
            set { _Member_Zip = value.Length > 10 ? value.Substring(0, 10) : value.ToString(); }
        }

        public string Member_Phone_Countrycode
        {
            get { return _Member_Phone_Countrycode; }
            set { _Member_Phone_Countrycode = value.Length > 10 ? value.Substring(0, 10) : value.ToString(); }
        }

        public string Member_Phone_Areacode
        {
            get { return _Member_Phone_Areacode; }
            set { _Member_Phone_Areacode = value.Length > 10 ? value.Substring(0, 10) : value.ToString(); }
        }

        public string Member_Phone_Number
        {
            get { return _Member_Phone_Number; }
            set { _Member_Phone_Number = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Member_Mobile
        {
            get { return _Member_Mobile; }
            set { _Member_Mobile = value.Length > 20 ? value.Substring(0, 20) : value.ToString(); }
        }

        public string Member_Company
        {
            get { return _Member_Company; }
            set { _Member_Company = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Member_Fax
        {
            get { return _Member_Fax; }
            set { _Member_Fax = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Member_QQ
        {
            get { return _Member_QQ; }
            set { _Member_QQ = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Member_OrganizationCode
        {
            get { return _Member_OrganizationCode; }
            set { _Member_OrganizationCode = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Member_BusinessCode
        {
            get { return _Member_BusinessCode; }
            set { _Member_BusinessCode = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Member_SealImg
        {
            get { return _Member_SealImg; }
            set { _Member_SealImg = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }

        public string Member_Corporate
        {
            get { return _Member_Corporate; }
            set { _Member_Corporate = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Member_CorporateMobile
        {
            get { return _Member_CorporateMobile; }
            set { _Member_CorporateMobile = value.Length > 20 ? value.Substring(0, 20) : value.ToString(); }
        }

        public double Member_RegisterFunds
        {
            get { return _Member_RegisterFunds; }
            set { _Member_RegisterFunds = value; }
        }

        public string Member_TaxationCode
        {
            get { return _Member_TaxationCode; }
            set { _Member_TaxationCode = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Member_BankAccountCode
        {
            get { return _Member_BankAccountCode; }
            set { _Member_BankAccountCode = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Member_HeadImg
        {
            get { return _Member_HeadImg; }
            set { _Member_HeadImg = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }


        //新加 真实姓名  统一社会代码证号
        public string Member_RealName
        {
            get { return _Member_RealName; }
            set { _Member_RealName = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }
        public string Member_UniformSocial_Number
        {
            get { return _Member_UniformSocial_Number; }
            set { _Member_UniformSocial_Number = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

    }
}
