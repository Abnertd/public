<%@ Page Language="C#" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    private ITools tools;
    protected void Page_Load(object sender, EventArgs e)
    {
        tools = ToolsFactory.CreateTools();
        Promotion promotion = new Promotion();
        string working = Request["action"];
        string strPID;
        int product_id;
        switch (working) { 
            case "new":
                Public.CheckLogin("fa1deb07-54f5-48b8-a8ff-d94c3c8990be");
                promotion.AddPromotionCouponRule();
                break;
            case "renew":
                Public.CheckLogin("5889f6be-7a22-4ccf-a122-683262434cb1");
                //promotion.e();
                break;
            case "move":
                Public.CheckLogin("ac7af23d-9a18-4c6e-b8d5-cdbbc0bc018c");
                promotion.DelPromotionCouponRule();
                break;
            case "list":
                Public.CheckLogin("e5a32e42-426a-4202-818a-ad20a980b4cb");
                Response.Write(promotion.GetPromotionCouponRules());
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
