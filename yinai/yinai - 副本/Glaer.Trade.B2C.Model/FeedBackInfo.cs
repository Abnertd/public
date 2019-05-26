using System;

public class FeedBackInfo
{
    private int _Feedback_ID;
    private int _Feedback_Type;
    private int _Feedback_SupplierID;
    private int _Feedback_MemberID;
    private string _Feedback_Name;
    private string _Feedback_Tel;
    private string _Feedback_Email;
    private string _Feedback_CompanyName;
    private string _Feedback_Content;
    private string _Feedback_Attachment;
    private DateTime _Feedback_Addtime;
    private int _Feedback_IsRead;
    private int _Feedback_Reply_IsRead;
    private string _Feedback_Reply_Content;
    private DateTime _Feedback_Reply_Addtime;
    private string _Feedback_Site;
    private string _Feedback_Address;
    private double _Feedback_Amount;
    private string _Feedback_Note;





    public int Feedback_ID
    {
        get { return _Feedback_ID; }
        set { _Feedback_ID = value; }
    }

    public int Feedback_Type
    {
        get { return _Feedback_Type; }
        set { _Feedback_Type = value; }
    }
    public int Feedback_SupplierID
    {
        get { return _Feedback_SupplierID; }
        set { _Feedback_SupplierID = value; }
    } 

    public int Feedback_MemberID
    {
        get { return _Feedback_MemberID; }
        set { _Feedback_MemberID = value; }
    }

    public string Feedback_Name
    {
        get { return _Feedback_Name; }
        set { _Feedback_Name = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
    }

    public string Feedback_Tel
    {
        get { return _Feedback_Tel; }
        set { _Feedback_Tel = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
    }

    public string Feedback_Email
    {
        get { return _Feedback_Email; }
        set { _Feedback_Email = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
    }

    public string Feedback_CompanyName
    {
        get { return _Feedback_CompanyName; }
        set { _Feedback_CompanyName = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
    }

    public string Feedback_Content
    {
        get { return _Feedback_Content; }
        set { _Feedback_Content = value.Length > 2000 ? value.Substring(0, 2000) : value.ToString(); }
    }

    public string Feedback_Attachment
    {
        get { return _Feedback_Attachment; }
        set { _Feedback_Attachment = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
    }

    public DateTime Feedback_Addtime
    {
        get { return _Feedback_Addtime; }
        set { _Feedback_Addtime = value; }
    }

    public int Feedback_IsRead
    {
        get { return _Feedback_IsRead; }
        set { _Feedback_IsRead = value; }
    }

    public int Feedback_Reply_IsRead
    {
        get { return _Feedback_Reply_IsRead; }
        set { _Feedback_Reply_IsRead = value; }
    }

    public string Feedback_Reply_Content
    {
        get { return _Feedback_Reply_Content; }
        set { _Feedback_Reply_Content = value.Length > 2000 ? value.Substring(0, 2000) : value.ToString(); }
    }

    public DateTime Feedback_Reply_Addtime
    {
        get { return _Feedback_Reply_Addtime; }
        set { _Feedback_Reply_Addtime = value; }
    }

    public string Feedback_Site
    {
        get { return _Feedback_Site; }
        set { _Feedback_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
    }


    public string Feedback_Address
    {
        get { return _Feedback_Address; }
        set { _Feedback_Address = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
    }
    public double Feedback_Amount
    {
        get { return _Feedback_Amount; }
        set { _Feedback_Amount = value; }
    }

    public string Feedback_Note
    {
        get { return _Feedback_Note; }
        set { _Feedback_Note = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
    }

}