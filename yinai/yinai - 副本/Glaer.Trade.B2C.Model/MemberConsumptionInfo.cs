using System;

public class MemberConsumptionInfo
{
    private int _Consump_ID;
    private int _Consump_MemberID;
    private int _Consump_CoinRemain;
    private int _Consump_Coin;
    private string _Consump_Reason;
    private DateTime _Consump_Addtime;

    public int Consump_ID
    {
        get { return _Consump_ID; }
        set { _Consump_ID = value; }
    }

    public int Consump_MemberID
    {
        get { return _Consump_MemberID; }
        set { _Consump_MemberID = value; }
    }

    public int Consump_CoinRemain
    {
        get { return _Consump_CoinRemain; }
        set { _Consump_CoinRemain = value; }
    }

    public int Consump_Coin
    {
        get { return _Consump_Coin; }
        set { _Consump_Coin = value; }
    }

    public string Consump_Reason
    {
        get { return _Consump_Reason; }
        set { _Consump_Reason = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
    }

    public DateTime Consump_Addtime
    {
        get { return _Consump_Addtime; }
        set { _Consump_Addtime = value; }
    }

}