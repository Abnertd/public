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
                Public.CheckLogin("33713ac1-24a8-40af-b122-b60c1109f347");
                promotion.AddPromotionLimitGroup();
                break;
            case "renew":
                Public.CheckLogin("34b7b99f-451c-4c0b-8da1-e3ba000891a8");
                promotion.EditPromotionLimitGroup();
                break;
            case "move":
                Public.CheckLogin("470c7741-f942-42df-9973-c36555c8d2e6");
                promotion.DelPromotionGroup();
                break;
            case "list":
                Public.CheckLogin("22d21441-155a-4dc5-aec6-dcf5bdedd5cf");
                Response.Write(promotion.GetPromotionLimitGroups());
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
