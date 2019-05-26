using System;
using System.Collections.Generic;
using System.Text;

public class SupplierMessageInfo
{
    private int _Supplier_Message_ID;
    private int _Supplier_Message_SupplierID;
    private string _Supplier_Message_Title;
    private string _Supplier_Message_Content;
    private DateTime _Supplier_Message_Addtime;
    private int _Supplier_Message_IsRead;
    private string _Supplier_Message_Site;

    public int Supplier_Message_ID
    {
        get { return _Supplier_Message_ID; }
        set { _Supplier_Message_ID = value; }
    }

    public int Supplier_Message_SupplierID
    {
        get { return _Supplier_Message_SupplierID; }
        set { _Supplier_Message_SupplierID = value; }
    }

    public string Supplier_Message_Title
    {
        get { return _Supplier_Message_Title; }
        set { _Supplier_Message_Title = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
    }

    public string Supplier_Message_Content
    {
        get { return _Supplier_Message_Content; }
        set { _Supplier_Message_Content = value; }
    }

    public DateTime Supplier_Message_Addtime
    {
        get { return _Supplier_Message_Addtime; }
        set { _Supplier_Message_Addtime = value; }
    }

    public int Supplier_Message_IsRead
    {
        get { return _Supplier_Message_IsRead; }
        set { _Supplier_Message_IsRead = value; }
    }

    public string Supplier_Message_Site
    {
        get { return _Supplier_Message_Site; }
        set { _Supplier_Message_Site = value; }
    }

}