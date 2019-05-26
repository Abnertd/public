using System;
public class DeliveryTimeInfo
{
    private int _Delivery_Time_ID;
    private string _Delivery_Time_Name;
    private int _Delivery_Time_Sort;
    private int _Delivery_Time_IsActive;
    private string _Delivery_Time_Site;

    public int Delivery_Time_ID
    {
        get { return _Delivery_Time_ID; }
        set { _Delivery_Time_ID = value; }
    }

    public string Delivery_Time_Name
    {
        get { return _Delivery_Time_Name; }
        set { _Delivery_Time_Name = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
    }

    public int Delivery_Time_Sort
    {
        get { return _Delivery_Time_Sort; }
        set { _Delivery_Time_Sort = value; }
    }

    public int Delivery_Time_IsActive
    {
        get { return _Delivery_Time_IsActive; }
        set { _Delivery_Time_IsActive = value; }
    }

    public string Delivery_Time_Site
    {
        get { return _Delivery_Time_Site; }
        set { _Delivery_Time_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
    }

}