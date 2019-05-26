using System;
using System.Collections.Generic;
public class ProductReviewInfo
{
    private int _Product_Review_ID;
    private int _Product_Review_ProductID;
    private int _Product_Review_MemberID;
    private int _Product_Review_Star;
    private string _Product_Review_Subject;
    private string _Product_Review_Content;
    private int _Product_Review_Useful;
    private int _Product_Review_Useless;
    private DateTime _Product_Review_Addtime;
    private int _Product_Review_IsShow;
    private int _Product_Review_IsBuy;
    private int _Product_Review_IsGift;
    private int _Product_Review_IsView;
    private int _Product_Review_IsRecommend;
    private string _Product_Review_Site;

    public int Product_Review_ID
    {
        get { return _Product_Review_ID; }
        set { _Product_Review_ID = value; }
    }

    public int Product_Review_ProductID
    {
        get { return _Product_Review_ProductID; }
        set { _Product_Review_ProductID = value; }
    }

    public int Product_Review_MemberID
    {
        get { return _Product_Review_MemberID; }
        set { _Product_Review_MemberID = value; }
    }

    public int Product_Review_Star
    {
        get { return _Product_Review_Star; }
        set { _Product_Review_Star = value; }
    }

    public string Product_Review_Subject
    {
        get { return _Product_Review_Subject; }
        set { _Product_Review_Subject = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
    }

    public string Product_Review_Content
    {
        get { return _Product_Review_Content; }
        set { _Product_Review_Content = value.Length > 500 ? value.Substring(0, 500) : value.ToString(); }
    }

    public int Product_Review_Useful
    {
        get { return _Product_Review_Useful; }
        set { _Product_Review_Useful = value; }
    }

    public int Product_Review_Useless
    {
        get { return _Product_Review_Useless; }
        set { _Product_Review_Useless = value; }
    }

    public DateTime Product_Review_Addtime
    {
        get { return _Product_Review_Addtime; }
        set { _Product_Review_Addtime = value; }
    }

    public int Product_Review_IsShow
    {
        get { return _Product_Review_IsShow; }
        set { _Product_Review_IsShow = value; }
    }

    public int Product_Review_IsBuy
    {
        get { return _Product_Review_IsBuy; }
        set { _Product_Review_IsBuy = value; }
    }

    public int Product_Review_IsGift
    {
        get { return _Product_Review_IsGift; }
        set { _Product_Review_IsGift = value; }
    }

    public int Product_Review_IsView
    {
        get { return _Product_Review_IsView; }
        set { _Product_Review_IsView = value; }
    }

    public int Product_Review_IsRecommend
    {
        get { return _Product_Review_IsRecommend; }
        set { _Product_Review_IsRecommend = value; }
    }

    public string Product_Review_Site
    {
        get { return _Product_Review_Site; }
        set { _Product_Review_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
    }

}