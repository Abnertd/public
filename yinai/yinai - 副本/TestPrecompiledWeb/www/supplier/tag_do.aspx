<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<% 
    Supplier supplier = new Supplier();
    supplier.Supplier_Login_Check("/supplier/tag.aspx");
    
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    ProductTag tag = new ProductTag();
    
    string working = Request["action"];
    switch (working)
    {
        case "add":
            tag.AddProductTag();
            break;
        case "edit":
            tag.EditProductTag();
            break;
        case "del":
            tag.DelProductTag();
            break;
    }
%>