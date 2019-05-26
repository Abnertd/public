using System;

public class MemberPayLogInfo
{
    private int _Member_Pay_Log_ID;
    private string _Member_Pay_Log_OrderSN;
    private string _Member_Pay_Log_PaywaySign;
    private int _Member_Pay_Log_IsSuccess;
    private double _Member_Pay_Log_Amount;
    private string _Member_Pay_Log_Note;
    private string _Member_Pay_Log_Detail;
    private DateTime _Member_Pay_Log_Addtime;

    public int Member_Pay_Log_ID
    {
        get { return _Member_Pay_Log_ID; }
        set { _Member_Pay_Log_ID = value; }
    }

    public string Member_Pay_Log_OrderSN
    {
        get { return _Member_Pay_Log_OrderSN; }
        set { _Member_Pay_Log_OrderSN = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
    }

    public string Member_Pay_Log_PaywaySign
    {
        get { return _Member_Pay_Log_PaywaySign; }
        set { _Member_Pay_Log_PaywaySign = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
    }

    public int Member_Pay_Log_IsSuccess
    {
        get { return _Member_Pay_Log_IsSuccess; }
        set { _Member_Pay_Log_IsSuccess = value; }
    }

    public double Member_Pay_Log_Amount
    {
        get { return _Member_Pay_Log_Amount; }
        set { _Member_Pay_Log_Amount = value; }
    }

    public string Member_Pay_Log_Note
    {
        get { return _Member_Pay_Log_Note; }
        set { _Member_Pay_Log_Note = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
    }

    public string Member_Pay_Log_Detail
    {
        get { return _Member_Pay_Log_Detail; }
        set { _Member_Pay_Log_Detail = value.Length > 3000 ? value.Substring(0, 3000) : value.ToString(); }
    }

    public DateTime Member_Pay_Log_Addtime
    {
        get { return _Member_Pay_Log_Addtime; }
        set { _Member_Pay_Log_Addtime = value; }
    }

}