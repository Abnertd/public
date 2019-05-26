using System;

public class ProductPriceInfo
{
    private int _Product_Price_ID;
    private int _Product_Price_ProcutID;
    private int _Product_Price_MemberGradeID;
    private double _Product_Price_Price;

    public int Product_Price_ID
    {
        get { return _Product_Price_ID; }
        set { _Product_Price_ID = value; }
    }

    public int Product_Price_ProcutID
    {
        get { return _Product_Price_ProcutID; }
        set { _Product_Price_ProcutID = value; }
    }

    public int Product_Price_MemberGradeID
    {
        get { return _Product_Price_MemberGradeID; }
        set { _Product_Price_MemberGradeID = value; }
    }

    public double Product_Price_Price
    {
        get { return _Product_Price_Price; }
        set { _Product_Price_Price = value; }
    }

}