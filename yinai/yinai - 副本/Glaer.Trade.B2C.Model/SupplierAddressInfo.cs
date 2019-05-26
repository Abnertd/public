using System;

public class SupplierAddressInfo
{
    private int _Supplier_Address_ID;
    private int _Supplier_Address_SupplierID;
    private string _Supplier_Address_Country;
    private string _Supplier_Address_State;
    private string _Supplier_Address_City;
    private string _Supplier_Address_County;
    private string _Supplier_Address_StreetAddress;
    private string _Supplier_Address_Zip;
    private string _Supplier_Address_Name;
    private string _Supplier_Address_Phone_Countrycode;
    private string _Supplier_Address_Phone_Areacode;
    private string _Supplier_Address_Phone_Number;
    private string _Supplier_Address_Mobile;
    private string _Supplier_Address_Site;

    public int Supplier_Address_ID
    {
        get { return _Supplier_Address_ID; }
        set { _Supplier_Address_ID = value; }
    }

    public int Supplier_Address_SupplierID
    {
        get { return _Supplier_Address_SupplierID; }
        set { _Supplier_Address_SupplierID = value; }
    }

    public string Supplier_Address_Country
    {
        get { return _Supplier_Address_Country; }
        set { _Supplier_Address_Country = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
    }

    public string Supplier_Address_State
    {
        get { return _Supplier_Address_State; }
        set { _Supplier_Address_State = value.Length > 10 ? value.Substring(0, 10) : value.ToString(); }
    }

    public string Supplier_Address_City
    {
        get { return _Supplier_Address_City; }
        set { _Supplier_Address_City = value.Length > 10 ? value.Substring(0, 10) : value.ToString(); }
    }

    public string Supplier_Address_County
    {
        get { return _Supplier_Address_County; }
        set { _Supplier_Address_County = value.Length > 10 ? value.Substring(0, 10) : value.ToString(); }
    }

    public string Supplier_Address_StreetAddress
    {
        get { return _Supplier_Address_StreetAddress; }
        set { _Supplier_Address_StreetAddress = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
    }

    public string Supplier_Address_Zip
    {
        get { return _Supplier_Address_Zip; }
        set { _Supplier_Address_Zip = value.Length > 10 ? value.Substring(0, 10) : value.ToString(); }
    }

    public string Supplier_Address_Name
    {
        get { return _Supplier_Address_Name; }
        set { _Supplier_Address_Name = value.Length > 10 ? value.Substring(0, 10) : value.ToString(); }
    }

    public string Supplier_Address_Phone_Countrycode
    {
        get { return _Supplier_Address_Phone_Countrycode; }
        set { _Supplier_Address_Phone_Countrycode = value.Length > 10 ? value.Substring(0, 10) : value.ToString(); }
    }

    public string Supplier_Address_Phone_Areacode
    {
        get { return _Supplier_Address_Phone_Areacode; }
        set { _Supplier_Address_Phone_Areacode = value.Length > 10 ? value.Substring(0, 10) : value.ToString(); }
    }

    public string Supplier_Address_Phone_Number
    {
        get { return _Supplier_Address_Phone_Number; }
        set { _Supplier_Address_Phone_Number = value.Length > 20 ? value.Substring(0, 20) : value.ToString(); }
    }

    public string Supplier_Address_Mobile
    {
        get { return _Supplier_Address_Mobile; }
        set { _Supplier_Address_Mobile = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
    }

    public string Supplier_Address_Site
    {
        get { return _Supplier_Address_Site; }
        set { _Supplier_Address_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
    }

}