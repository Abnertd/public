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
                Public.CheckLogin("0b16441f-dc42-4fd0-b8f1-0f8a80146771");
                promotion.AddPromotionGroup();
                break;
            case "renew":
                Public.CheckLogin("0b16441f-dc42-4fd0-b8f1-0f8a80146771");
                promotion.EditPromotionGroup();
                break;
            case "move":
                Public.CheckLogin("0b16441f-dc42-4fd0-b8f1-0f8a80146771");
                promotion.DelPromotionLimitGroup();
                break;
            case "list":
                Public.CheckLogin("0b16441f-dc42-4fd0-b8f1-0f8a80146771");
                Response.Write(promotion.GetPromotionGroups());
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
