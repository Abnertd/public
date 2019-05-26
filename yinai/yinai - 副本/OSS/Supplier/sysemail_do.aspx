<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%
    Server.ScriptTimeout = 1000;
    Supplier supplier = new Supplier();


    supplier.Supplier_Sysemail_Send();
                    
%>
