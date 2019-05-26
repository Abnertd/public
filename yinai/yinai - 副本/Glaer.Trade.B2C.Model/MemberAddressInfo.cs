using System;

public class MemberAddressInfo
{
    private int _Member_Address_ID;
    private int _Member_Address_MemberID;
    private string _Member_Address_Country;
    private string _Member_Address_State;
    private string _Member_Address_City;
    private string _Member_Address_County;
    private string _Member_Address_StreetAddress;
    private string _Member_Address_Zip;
    private string _Member_Address_Name;
    private string _Member_Address_Phone_Countrycode;
    private string _Member_Address_Phone_Areacode;
    private string _Member_Address_Phone_Number;
    private string _Member_Address_Mobile;
    private int _Member_Address_IsDefault;

    public int Member_Address_IsDefault
    {
        get { return _Member_Address_IsDefault; }
        set { _Member_Address_IsDefault = value; }
    }
    private string _Member_Address_Site;

    public int Member_Address_ID
    {
        get { return _Member_Address_ID; }
        set { _Member_Address_ID = value; }
    }

    public int Member_Address_MemberID
    {
        get { return _Member_Address_MemberID; }
        set { _Member_Address_MemberID = value; }
    }

    public string Member_Address_Country
    {
        get { return _Member_Address_Country; }
        set { _Member_Address_Country = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
    }

    public string Member_Address_State
    {
        get { return _Member_Address_State; }
        set { _Member_Address_State = value.Length > 10 ? value.Substring(0, 10) : value.ToString(); }
    }

    public string Member_Address_City
    {
        get { return _Member_Address_City; }
        set { _Member_Address_City = value.Length > 10 ? value.Substring(0, 10) : value.ToString(); }
    }

    public string Member_Address_County
    {
        get { return _Member_Address_County; }
        set { _Member_Address_County = value.Length > 10 ? value.Substring(0, 10) : value.ToString(); }
    }

    public string Member_Address_StreetAddress
    {
        get { return _Member_Address_StreetAddress; }
        set { _Member_Address_StreetAddress = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
    }

    public string Member_Address_Zip
    {
        get { return _Member_Address_Zip; }
        set { _Member_Address_Zip = value.Length > 10 ? value.Substring(0, 10) : value.ToString(); }
    }

    public string Member_Address_Name
    {
        get { return _Member_Address_Name; }
        set { _Member_Address_Name = value.Length > 10 ? value.Substring(0, 10) : value.ToString(); }
    }

    public string Member_Address_Phone_Countrycode
    {
        get { return _Member_Address_Phone_Countrycode; }
        set { _Member_Address_Phone_Countrycode = value.Length > 10 ? value.Substring(0, 10) : value.ToString(); }
    }

    public string Member_Address_Phone_Areacode
    {
        get { return _Member_Address_Phone_Areacode; }
        set { _Member_Address_Phone_Areacode = value.Length > 10 ? value.Substring(0, 10) : value.ToString(); }
    }

    public string Member_Address_Phone_Number
    {
        get { return _Member_Address_Phone_Number; }
        set { _Member_Address_Phone_Number = value.Length > 20 ? value.Substring(0, 20) : value.ToString(); }
    }

    public string Member_Address_Mobile
    {
        get { return _Member_Address_Mobile; }
        set { _Member_Address_Mobile = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
    }

    public string Member_Address_Site
    {
        get { return _Member_Address_Site; }
        set { _Member_Address_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
    }

}