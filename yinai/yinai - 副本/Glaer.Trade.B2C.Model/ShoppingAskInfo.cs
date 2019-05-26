using System;


public class ShoppingAskInfo
{
    private int _Ask_ID;
    private int _Ask_Type;
    private string _Ask_Contact;
    private string _Ask_Content;
    private string _Ask_Reply;
    private DateTime _Ask_Addtime;
    private int _Ask_SupplierID;
    private int _Ask_MemberID;
    private int _Ask_ProductID;
    private int _Ask_Pleasurenum;
    private int _Ask_Displeasure;
    private int _Ask_IsCheck;
    private int _Ask_Isreply;
    private string _Ask_Site;

    public int Ask_ID
    {
        get { return _Ask_ID; }
        set { _Ask_ID = value; }
    }

    public int Ask_Type
    {
        get { return _Ask_Type; }
        set { _Ask_Type = value; }
    }

    public string Ask_Contact
    {
        get { return _Ask_Contact; }
        set { _Ask_Contact = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
    }

    public string Ask_Content
    {
        get { return _Ask_Content; }
        set { _Ask_Content = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
    }

    public string Ask_Reply
    {
        get { return _Ask_Reply; }
        set { _Ask_Reply = value.Length > 1000 ? value.Substring(0, 1000) : value.ToString(); }
    }

    public DateTime Ask_Addtime
    {
        get { return _Ask_Addtime; }
        set { _Ask_Addtime = value; }
    }

    public int Ask_SupplierID
    {
        get { return _Ask_SupplierID; }
        set { _Ask_SupplierID = value; }
    }

    public int Ask_MemberID
    {
        get { return _Ask_MemberID; }
        set { _Ask_MemberID = value; }
    }

    public int Ask_ProductID
    {
        get { return _Ask_ProductID; }
        set { _Ask_ProductID = value; }
    }

    public int Ask_Pleasurenum
    {
        get { return _Ask_Pleasurenum; }
        set { _Ask_Pleasurenum = value; }
    }

    public int Ask_Displeasure
    {
        get { return _Ask_Displeasure; }
        set { _Ask_Displeasure = value; }
    }

    public int Ask_IsCheck
    {
        get { return _Ask_IsCheck; }
        set { _Ask_IsCheck = value; }
    }

    public int Ask_Isreply
    {
        get { return _Ask_Isreply; }
        set { _Ask_Isreply = value; }
    }

    public string Ask_Site
    {
        get { return _Ask_Site; }
        set { _Ask_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
    }

}