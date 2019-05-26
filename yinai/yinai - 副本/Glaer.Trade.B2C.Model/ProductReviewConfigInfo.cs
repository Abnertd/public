using System;
using System.Collections.Generic;

public class ProductReviewConfigInfo
{
    private int _Product_Review_Config_ID;
    private int _Product_Review_Config_ProductCount;
    private int _Product_Review_Config_ListCount;
    private int _Product_Review_Config_Power;
    private int _Product_Review_giftcoin;
    private int _Product_Review_Recommendcoin;
    private string _Product_Review_Config_NoRecordTip;
    private int _Product_Review_Config_VerifyCode_IsOpen;
    private int _Product_Review_Config_ManagerReply_Show;
    private string _Product_Review_Config_Show_SuccessTip;
    private int _Product_Review_Config_IsActive;
    private string _Product_Review_Config_Site;

    public int Product_Review_Config_ID
    {
        get { return _Product_Review_Config_ID; }
        set { _Product_Review_Config_ID = value; }
    }

    public int Product_Review_Config_ProductCount
    {
        get { return _Product_Review_Config_ProductCount; }
        set { _Product_Review_Config_ProductCount = value; }
    }

    public int Product_Review_Config_ListCount
    {
        get { return _Product_Review_Config_ListCount; }
        set { _Product_Review_Config_ListCount = value; }
    }

    public int Product_Review_Config_Power
    {
        get { return _Product_Review_Config_Power; }
        set { _Product_Review_Config_Power = value; }
    }

    public int Product_Review_giftcoin
    {
        get { return _Product_Review_giftcoin; }
        set { _Product_Review_giftcoin = value; }
    }

    public int Product_Review_Recommendcoin
    {
        get { return _Product_Review_Recommendcoin; }
        set { _Product_Review_Recommendcoin = value; }
    }

    public string Product_Review_Config_NoRecordTip
    {
        get { return _Product_Review_Config_NoRecordTip; }
        set { _Product_Review_Config_NoRecordTip = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
    }

    public int Product_Review_Config_VerifyCode_IsOpen
    {
        get { return _Product_Review_Config_VerifyCode_IsOpen; }
        set { _Product_Review_Config_VerifyCode_IsOpen = value; }
    }

    public int Product_Review_Config_ManagerReply_Show
    {
        get { return _Product_Review_Config_ManagerReply_Show; }
        set { _Product_Review_Config_ManagerReply_Show = value; }
    }

    public string Product_Review_Config_Show_SuccessTip
    {
        get { return _Product_Review_Config_Show_SuccessTip; }
        set { _Product_Review_Config_Show_SuccessTip = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
    }

    public int Product_Review_Config_IsActive
    {
        get { return _Product_Review_Config_IsActive; }
        set { _Product_Review_Config_IsActive = value; }
    }

    public string Product_Review_Config_Site
    {
        get { return _Product_Review_Config_Site; }
        set { _Product_Review_Config_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
    }

}
