using System;

public class MemberFavoritesInfo
{
    private int _Member_Favorites_ID;
    private int _Member_Favorites_MemberID;
    private int _Member_Favorites_Type;
    private int _Member_Favorites_TargetID;
    private DateTime _Member_Favorites_Addtime;
    private string _Member_Favorites_Site;

    public int Member_Favorites_ID
    {
        get { return _Member_Favorites_ID; }
        set { _Member_Favorites_ID = value; }
    }

    public int Member_Favorites_MemberID
    {
        get { return _Member_Favorites_MemberID; }
        set { _Member_Favorites_MemberID = value; }
    }

    public int Member_Favorites_Type
    {
        get { return _Member_Favorites_Type; }
        set { _Member_Favorites_Type = value; }
    }

    public int Member_Favorites_TargetID
    {
        get { return _Member_Favorites_TargetID; }
        set { _Member_Favorites_TargetID = value; }
    }

    public DateTime Member_Favorites_Addtime
    {
        get { return _Member_Favorites_Addtime; }
        set { _Member_Favorites_Addtime = value; }
    }

    public string Member_Favorites_Site
    {
        get { return _Member_Favorites_Site; }
        set { _Member_Favorites_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
    }

}