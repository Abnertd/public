<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<script runat="server">
    private ITools tools;
    
    private int ProductType_ID;


    
</script>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("847e8136-fd2f-4834-86b7-f2c984705eff");
        
        tools = ToolsFactory.CreateTools();
        ProductType_ID = tools.CheckInt(Request["producttype_id"]); 
        ProductTypeExtend producttypeextend = new ProductTypeExtend();
        string working = Request["action"];
        switch (working) { 
            case "new":
                producttypeextend.AddProductTypeExtend();
                break;
            case "renew":
                producttypeextend.EditProductTypeExtend();
                break;
            case "move":
                producttypeextend.DelProductTypeExtend();
                break;
            case "list":
                Response.Write(producttypeextend.GetProductTypeExtends(ProductType_ID));
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
