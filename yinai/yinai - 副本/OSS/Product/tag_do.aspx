<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        ProductTag myApp = new ProductTag();
        string action = Request["action"];
        switch (action)
        {
            case "new":
                Public.CheckLogin("2f1d706e-3356-494d-821c-c4173a923328"); 
                
                myApp.AddProductTag();
                break;
            case "renew":
                Public.CheckLogin("2fea26b6-bbb2-44d8-9b46-0b1aed1cc47f");
                
                myApp.EditProductTag();
                break;
            case "move":
                Public.CheckLogin("7b8b58e2-e509-4e6c-a68e-0361225cefa6"); 
                
                myApp.DelProductTag();
                break;
            case "list":
                Public.CheckLogin("ed87eb87-dade-4fbc-804c-c139c1cbe9c8");
                
                Response.Write(myApp.GetProductTags());
                Response.End();
                break;
        }

    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
