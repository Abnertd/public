using System;
public class LogisticsLineInfo
{
    private int _Logistics_Line_ID;
    private string _Logistics_Line_Contact;
    private string _Logistics_Line_CarType;
    private string _Logistics_Line_Delivery_Address;
    private string _Logistics_Line_Receiving_Address;
    private DateTime _Logistics_Line_DeliverTime;
    private double _Logistics_Line_Deliver_Price;
    private string _Logistics_Line_Note;
    private int _Logistics_ID;

    public int Logistics_Line_ID
    {
        get { return _Logistics_Line_ID; }
        set { _Logistics_Line_ID = value; }
    }

    public string Logistics_Line_Contact
    {
        get { return _Logistics_Line_Contact; }
        set { _Logistics_Line_Contact = value.Length > 30 ? value.Substring(0, 30) : value.ToString(); }
    }

    public string Logistics_Line_CarType
    {
        get { return _Logistics_Line_CarType; }
        set { _Logistics_Line_CarType = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
    }

    public string Logistics_Line_Delivery_Address
    {
        get { return _Logistics_Line_Delivery_Address; }
        set { _Logistics_Line_Delivery_Address = value.Length > 80 ? value.Substring(0, 80) : value.ToString(); }
    }

    public string Logistics_Line_Receiving_Address
    {
        get { return _Logistics_Line_Receiving_Address; }
        set { _Logistics_Line_Receiving_Address = value.Length > 80 ? value.Substring(0, 80) : value.ToString(); }
    }

    public DateTime Logistics_Line_DeliverTime
    {
        get { return _Logistics_Line_DeliverTime; }
        set { _Logistics_Line_DeliverTime = value; }
    }

    public double Logistics_Line_Deliver_Price
    {
        get { return _Logistics_Line_Deliver_Price; }
        set { _Logistics_Line_Deliver_Price = value; }
    }

    public string Logistics_Line_Note
    {
        get { return _Logistics_Line_Note; }
        set { _Logistics_Line_Note = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
    }

    public int Logistics_ID
    {
        get { return _Logistics_ID; }
        set { _Logistics_ID = value; }
    }

}