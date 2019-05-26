using System;

public class OrdersInvoiceInfo { 
private  int _Invoice_ID;
private  int _Invoice_OrdersID;
private  int _Invoice_Type;
private  string _Invoice_Title;
private  int _Invoice_Content;
private  string _Invoice_FirmName;
private  string _Invoice_VAT_FirmName;
private  string _Invoice_VAT_Code;
private  string _Invoice_VAT_RegAddr;
private  string _Invoice_VAT_RegTel;
private  string _Invoice_VAT_Bank;
private  string _Invoice_VAT_BankAcount;
private  string _Invoice_VAT_Content;

public  int Invoice_ID { 
get { return _Invoice_ID; } 
set { _Invoice_ID = value; } 
} 

public  int Invoice_OrdersID { 
get { return _Invoice_OrdersID; } 
set { _Invoice_OrdersID = value; } 
} 

public  int Invoice_Type { 
get { return _Invoice_Type; } 
set { _Invoice_Type = value; } 
} 

public  string Invoice_Title { 
get { return _Invoice_Title; }
    set { _Invoice_Title = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); } 
} 

public  int Invoice_Content { 
get { return _Invoice_Content; } 
set { _Invoice_Content = value; } 
} 

public  string Invoice_FirmName{ 
get { return _Invoice_FirmName; } 
set { _Invoice_FirmName = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); } 
} 

public  string Invoice_VAT_FirmName{ 
get { return _Invoice_VAT_FirmName; } 
set { _Invoice_VAT_FirmName = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); } 
} 

public  string Invoice_VAT_Code{ 
get { return _Invoice_VAT_Code; } 
set { _Invoice_VAT_Code = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); } 
} 

public  string Invoice_VAT_RegAddr{ 
get { return _Invoice_VAT_RegAddr; } 
set { _Invoice_VAT_RegAddr = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); } 
} 

public  string Invoice_VAT_RegTel{ 
get { return _Invoice_VAT_RegTel; } 
set { _Invoice_VAT_RegTel = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); } 
} 

public  string Invoice_VAT_Bank{ 
get { return _Invoice_VAT_Bank; } 
set { _Invoice_VAT_Bank = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); } 
} 

public  string Invoice_VAT_BankAcount{ 
get { return _Invoice_VAT_BankAcount; } 
set { _Invoice_VAT_BankAcount = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); } 
} 

public  string Invoice_VAT_Content{ 
get { return _Invoice_VAT_Content; } 
set { _Invoice_VAT_Content = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); } 
} 

}