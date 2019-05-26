using System;

public class CartInfo
{
    private double _Total_Product_MktPrice;
    private double _Total_Product_Price;
    private int _Total_Product_Coin;
    private int _Total_Product_UseCoin;
    private double _Total_Nofavor_Product_Price;
    private double _Total_Favor_Price;
    private double _Total_Weight;
    private double _Total_Favor_Fee;
    private string _Favor_Fee_Note;
    private string _Favor_Price_Note;
    private string _Favor_Policy_ID;


    public double Total_Product_MktPrice
    {
        get { return _Total_Product_MktPrice; }
        set { _Total_Product_MktPrice = value; }
    }

    public double Total_Product_Price
    {
        get { return _Total_Product_Price; }
        set { _Total_Product_Price = value; }
    }

    public int Total_Product_Coin
    {
        get { return _Total_Product_Coin; }
        set { _Total_Product_Coin = value; }
    }

    public int Total_Product_UseCoin
    {
        get { return _Total_Product_UseCoin; }
        set { _Total_Product_UseCoin = value; }
    }

    public double Total_Nofavor_Product_Price
    {
        get { return _Total_Nofavor_Product_Price; }
        set { _Total_Nofavor_Product_Price = value; }
    }

    public double Total_Favor_Price
    {
        get { return _Total_Favor_Price; }
        set { _Total_Favor_Price = value; }
    }

    public double Total_Weight
    {
        get { return _Total_Weight; }
        set { _Total_Weight = value; }
    }

    public double Total_Favor_Fee
    {
        get { return _Total_Favor_Fee; }
        set { _Total_Favor_Fee = value; }
    }

    public string Favor_Fee_Note
    {
        get { return _Favor_Fee_Note; }
        set { _Favor_Fee_Note = value.Length > 500 ? value.Substring(0, 500) : value.ToString(); }
    }


    public string Favor_Price_Note
    {
        get { return _Favor_Price_Note; }
        set { _Favor_Price_Note = value.Length > 1000 ? value.Substring(0, 1000) : value.ToString(); }
    }

    public string Favor_Policy_ID
    {
        get { return _Favor_Policy_ID; }
        set { _Favor_Policy_ID = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
    }
}

public class OrdersGoodsTmpInfo
{
    private int _Orders_Goods_ID;
    private int _Orders_Goods_Type;
    private int _Orders_Goods_BuyerID;
    private string _Orders_Goods_SessionID;
    private int _Orders_Goods_ParentID;
    private int _Orders_Goods_Product_ID;
    private int _Orders_Goods_Product_SupplierID;
    private string _Orders_Goods_Product_Code;
    private int _Orders_Goods_Product_CateID;
    private int _Orders_Goods_Product_BrandID;
    private string _Orders_Goods_Product_Name;
    private string _Orders_Goods_Product_Img;
    private double _Orders_Goods_Product_Price;
    private double _Orders_Goods_Product_MKTPrice;
    private string _Orders_Goods_Product_Maker;
    private string _Orders_Goods_Product_Spec;
    private string _Orders_Goods_Product_DeliveryDate;
    private string _Orders_Goods_Product_AuthorizeCode;
    private double _Orders_Goods_Product_brokerage;
    private double _Orders_Goods_Product_SalePrice;
    private double _Orders_Goods_Product_PurchasingPrice;
    private int _Orders_Goods_Product_Coin;
    private int _Orders_Goods_Product_IsFavor;
    private int _Orders_Goods_Product_UseCoin;
    private int _Orders_Goods_Amount;
    private DateTime _Orders_Goods_Addtime;
    private int _Orders_Goods_OrdersID;

    public int Orders_Goods_ID
    {
        get { return _Orders_Goods_ID; }
        set { _Orders_Goods_ID = value; }
    }

    public int Orders_Goods_Type
    {
        get { return _Orders_Goods_Type; }
        set { _Orders_Goods_Type = value; }
    }

    public int Orders_Goods_BuyerID
    {
        get { return _Orders_Goods_BuyerID; }
        set { _Orders_Goods_BuyerID = value; }
    }

    public string Orders_Goods_SessionID
    {
        get { return _Orders_Goods_SessionID; }
        set { _Orders_Goods_SessionID = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
    }

    public int Orders_Goods_ParentID
    {
        get { return _Orders_Goods_ParentID; }
        set { _Orders_Goods_ParentID = value; }
    }

    public int Orders_Goods_Product_ID
    {
        get { return _Orders_Goods_Product_ID; }
        set { _Orders_Goods_Product_ID = value; }
    }

    public int Orders_Goods_Product_SupplierID
    {
        get { return _Orders_Goods_Product_SupplierID; }
        set { _Orders_Goods_Product_SupplierID = value; }
    }

    public string Orders_Goods_Product_Code
    {
        get { return _Orders_Goods_Product_Code; }
        set { _Orders_Goods_Product_Code = value.Length > 20 ? value.Substring(0, 20) : value.ToString(); }
    }

    public int Orders_Goods_Product_CateID
    {
        get { return _Orders_Goods_Product_CateID; }
        set { _Orders_Goods_Product_CateID = value; }
    }

    public int Orders_Goods_Product_BrandID
    {
        get { return _Orders_Goods_Product_BrandID; }
        set { _Orders_Goods_Product_BrandID = value; }
    }

    public string Orders_Goods_Product_Name
    {
        get { return _Orders_Goods_Product_Name; }
        set { _Orders_Goods_Product_Name = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
    }

    public string Orders_Goods_Product_Img
    {
        get { return _Orders_Goods_Product_Img; }
        set { _Orders_Goods_Product_Img = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
    }

    public double Orders_Goods_Product_Price
    {
        get { return _Orders_Goods_Product_Price; }
        set { _Orders_Goods_Product_Price = value; }
    }

    public double Orders_Goods_Product_MKTPrice
    {
        get { return _Orders_Goods_Product_MKTPrice; }
        set { _Orders_Goods_Product_MKTPrice = value; }
    }

    public string Orders_Goods_Product_Maker
    {
        get { return _Orders_Goods_Product_Maker; }
        set { _Orders_Goods_Product_Maker = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
    }

    public string Orders_Goods_Product_Spec
    {
        get { return _Orders_Goods_Product_Spec; }
        set { _Orders_Goods_Product_Spec = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
    }

    public string Orders_Goods_Product_DeliveryDate
    {
        get { return _Orders_Goods_Product_DeliveryDate; }
        set { _Orders_Goods_Product_DeliveryDate = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
    }

    public string Orders_Goods_Product_AuthorizeCode
    {
        get { return _Orders_Goods_Product_AuthorizeCode; }
        set { _Orders_Goods_Product_AuthorizeCode = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
    }

    public double Orders_Goods_Product_brokerage
    {
        get { return _Orders_Goods_Product_brokerage; }
        set { _Orders_Goods_Product_brokerage = value; }
    }

    public double Orders_Goods_Product_SalePrice
    {
        get { return _Orders_Goods_Product_SalePrice; }
        set { _Orders_Goods_Product_SalePrice = value; }
    }

    public double Orders_Goods_Product_PurchasingPrice
    {
        get { return _Orders_Goods_Product_PurchasingPrice; }
        set { _Orders_Goods_Product_PurchasingPrice = value; }
    }

    public int Orders_Goods_Product_Coin
    {
        get { return _Orders_Goods_Product_Coin; }
        set { _Orders_Goods_Product_Coin = value; }
    }

    public int Orders_Goods_Product_IsFavor
    {
        get { return _Orders_Goods_Product_IsFavor; }
        set { _Orders_Goods_Product_IsFavor = value; }
    }

    public int Orders_Goods_Product_UseCoin
    {
        get { return _Orders_Goods_Product_UseCoin; }
        set { _Orders_Goods_Product_UseCoin = value; }
    }

    public int Orders_Goods_Amount
    {
        get { return _Orders_Goods_Amount; }
        set { _Orders_Goods_Amount = value; }
    }

    public DateTime Orders_Goods_Addtime
    {
        get { return _Orders_Goods_Addtime; }
        set { _Orders_Goods_Addtime = value; }
    }

    public int Orders_Goods_OrdersID
    {
        get { return _Orders_Goods_OrdersID; }
        set { _Orders_Goods_OrdersID = value; }
    }
}