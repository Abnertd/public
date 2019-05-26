using System;
public class ADInfo
{
    private int _Ad_ID;
    private string _Ad_Title;
    private string _Ad_Kind;
    private int _Ad_MediaKind;
    private string _Ad_Media;
    private string _Ad_Link;
    private int _Ad_Show_Freq;
    private int _Ad_Show_times;
    private int _Ad_Hits;
    private DateTime _Ad_StartDate;
    private DateTime _Ad_EndDate;
    private int _Ad_IsContain;
    private string _Ad_Propertys;
    private int _Ad_Sort;
    private int _Ad_IsActive;
    private string _Ad_Site;
    private int _U_Ad_Audit = 1;
    private int _U_Ad_Advertiser = 0;
    private DateTime _Ad_Addtime;

    public int Ad_ID
    {
        get { return _Ad_ID; }
        set { _Ad_ID = value; }
    }

    public string Ad_Title
    {
        get { return _Ad_Title; }
        set { _Ad_Title = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
    }

    public string Ad_Kind
    {
        get { return _Ad_Kind; }
        set { _Ad_Kind = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
    }

    public int Ad_MediaKind
    {
        get { return _Ad_MediaKind; }
        set { _Ad_MediaKind = value; }
    }

    public string Ad_Media
    {
        get { return _Ad_Media; }
        set { _Ad_Media = value.Length > 1000 ? value.Substring(0, 1000) : value.ToString(); }
    }

    public string Ad_Link
    {
        get { return _Ad_Link; }
        set { _Ad_Link = value.Length > 255 ? value.Substring(0, 255) : value.ToString(); }
    }

    public int Ad_Show_Freq
    {
        get { return _Ad_Show_Freq; }
        set { _Ad_Show_Freq = value; }
    }

    public int Ad_Show_times
    {
        get { return _Ad_Show_times; }
        set { _Ad_Show_times = value; }
    }

    public int Ad_Hits
    {
        get { return _Ad_Hits; }
        set { _Ad_Hits = value; }
    }

    public DateTime Ad_StartDate
    {
        get { return _Ad_StartDate; }
        set { _Ad_StartDate = value; }
    }

    public DateTime Ad_EndDate
    {
        get { return _Ad_EndDate; }
        set { _Ad_EndDate = value; }
    }

    public int Ad_IsContain
    {
        get { return _Ad_IsContain; }
        set { _Ad_IsContain = value; }
    }

    public string Ad_Propertys
    {
        get { return _Ad_Propertys; }
        set { _Ad_Propertys = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
    }

    public int Ad_Sort
    {
        get { return _Ad_Sort; }
        set { _Ad_Sort = value; }
    }

    public int Ad_IsActive
    {
        get { return _Ad_IsActive; }
        set { _Ad_IsActive = value; }
    }

    public string Ad_Site
    {
        get { return _Ad_Site; }
        set { _Ad_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
    }

    public int U_Ad_Audit
    {
        get { return _U_Ad_Audit; }
        set { _U_Ad_Audit = value; }
    }

    public int U_Ad_Advertiser
    {
        get { return _U_Ad_Advertiser; }
        set { _U_Ad_Advertiser = value; }
    }

    public DateTime Ad_Addtime
    {
        get { return _Ad_Addtime; }
        set { _Ad_Addtime = value; }
    }

}

public class ADPositionInfo
{
    private int _Ad_Position_ID;
    private string _Ad_Position_Name;
    private int _Ad_Position_ChannelID;
    private string _Ad_Position_Value;
    private int _Ad_Position_Width;
    private int _Ad_Position_Height;
    private int _Ad_Position_IsActive;
    private string _Ad_Position_Site;
    private int _U_Ad_Position_Marketing = 0;
    private double _U_Ad_Position_Price = 0;

    public int Ad_Position_ID
    {
        get { return _Ad_Position_ID; }
        set { _Ad_Position_ID = value; }
    }

    public string Ad_Position_Name
    {
        get { return _Ad_Position_Name; }
        set { _Ad_Position_Name = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
    }

    public int Ad_Position_ChannelID
    {
        get { return _Ad_Position_ChannelID; }
        set { _Ad_Position_ChannelID = value; }
    }

    public string Ad_Position_Value
    {
        get { return _Ad_Position_Value; }
        set { _Ad_Position_Value = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
    }

    public int Ad_Position_Width
    {
        get { return _Ad_Position_Width; }
        set { _Ad_Position_Width = value; }
    }

    public int Ad_Position_Height
    {
        get { return _Ad_Position_Height; }
        set { _Ad_Position_Height = value; }
    }

    public int Ad_Position_IsActive
    {
        get { return _Ad_Position_IsActive; }
        set { _Ad_Position_IsActive = value; }
    }

    public string Ad_Position_Site
    {
        get { return _Ad_Position_Site; }
        set { _Ad_Position_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
    }

    public int U_Ad_Position_Marketing
    {
        get { return _U_Ad_Position_Marketing; }
        set { _U_Ad_Position_Marketing = value; }
    }

    public double U_Ad_Position_Price
    {
        get { return _U_Ad_Position_Price; }
        set { _U_Ad_Position_Price = value; }
    }

}

public class ADPositionChannelInfo
{
    private int _AD_Position_Channel_ID;
    private string _AD_Position_Channel_Name;
    private string _AD_Position_Channel_Note;
    private string _AD_Position_Channel_Site;

    public int AD_Position_Channel_ID
    {
        get { return _AD_Position_Channel_ID; }
        set { _AD_Position_Channel_ID = value; }
    }

    public string AD_Position_Channel_Name
    {
        get { return _AD_Position_Channel_Name; }
        set { _AD_Position_Channel_Name = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
    }

    public string AD_Position_Channel_Note
    {
        get { return _AD_Position_Channel_Note; }
        set { _AD_Position_Channel_Note = value.Length > 500 ? value.Substring(0, 500) : value.ToString(); }
    }

    public string AD_Position_Channel_Site
    {
        get { return _AD_Position_Channel_Site; }
        set { _AD_Position_Channel_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
    }

}