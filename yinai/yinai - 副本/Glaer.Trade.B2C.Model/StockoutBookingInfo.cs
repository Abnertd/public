using System;

public class StockoutBookingInfo
{
    private int _Stockout_ID;
    private string _Stockout_Product_Name;
    private string _Stockout_Product_Describe;
    private string _Stockout_Member_Name;
    private string _Stockout_Member_Tel;
    private string _Stockout_Member_Email;
    private int _Stockout_IsRead;
    private DateTime _Stockout_Addtime;
    private string _Stockout_Site;

    public int Stockout_ID
    {
        get { return _Stockout_ID; }
        set { _Stockout_ID = value; }
    }

    public string Stockout_Product_Name
    {
        get { return _Stockout_Product_Name; }
        set { _Stockout_Product_Name = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
    }

    public string Stockout_Product_Describe
    {
        get { return _Stockout_Product_Describe; }
        set { _Stockout_Product_Describe = value.Length > 1000 ? value.Substring(0, 1000) : value.ToString(); }
    }

    public string Stockout_Member_Name
    {
        get { return _Stockout_Member_Name; }
        set { _Stockout_Member_Name = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
    }

    public string Stockout_Member_Tel
    {
        get { return _Stockout_Member_Tel; }
        set { _Stockout_Member_Tel = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
    }

    public string Stockout_Member_Email
    {
        get { return _Stockout_Member_Email; }
        set { _Stockout_Member_Email = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
    }

    public int Stockout_IsRead
    {
        get { return _Stockout_IsRead; }
        set { _Stockout_IsRead = value; }
    }

    public DateTime Stockout_Addtime
    {
        get { return _Stockout_Addtime; }
        set { _Stockout_Addtime = value; }
    }

    public string Stockout_Site
    {
        get { return _Stockout_Site; }
        set { _Stockout_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
    }

}