<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        ProductType producttype = new ProductType();
        string working = Request["action"];
        switch (working) { 
            case "new":
                Public.CheckLogin("567b39bd-d8ee-4c79-b067-7ad68e6ca348");
                
                producttype.AddProductType();
                break;
            case "renew":
                Public.CheckLogin("847e8136-fd2f-4834-86b7-f2c984705eff");
                
                producttype.EditProductType();
                break;
            case "move":
                Public.CheckLogin("fcc7d1f7-e2f5-440f-a827-2e53e6e62184");
                
                producttype.DelProductType();
                break;
            case "list":
                Public.CheckLogin("b83adfda-1c87-4cc1-94e8-b5d905cc3da8");
                
                Response.Write(producttype.GetProductTypes());
                Response.End();
                break;
        }

    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
