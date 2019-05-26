using System;
public class ProductHistoryPriceInfo
{
    private int _History_ID;
    private string _History_SysName;
    private int _History_ProductID;
    private string _History_PriceType;
    private double _History_Price_Original;
    private double _History_Price_New;
    private DateTime _History_Addtime;

    public int History_ID
    {
        get { return _History_ID; }
        set { _History_ID = value; }
    }

    public string History_SysName
    {
        get { return _History_SysName; }
        set { _History_SysName = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
    }

    public int History_ProductID
    {
        get { return _History_ProductID; }
        set { _History_ProductID = value; }
    }

    public string History_PriceType
    {
        get { return _History_PriceType; }
        set { _History_PriceType = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
    }

    public double History_Price_Original
    {
        get { return _History_Price_Original; }
        set { _History_Price_Original = value; }
    }

    public double History_Price_New
    {
        get { return _History_Price_New; }
        set { _History_Price_New = value; }
    }

    public DateTime History_Addtime
    {
        get { return _History_Addtime; }
        set { _History_Addtime = value; }
    }

}