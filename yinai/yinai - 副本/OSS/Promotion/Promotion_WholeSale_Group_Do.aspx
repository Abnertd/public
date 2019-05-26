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
                Public.CheckLogin("706d21f3-b439-4e53-b5a9-dc7398ad4787");
                promotion.AddPromotionWholeSaleGroup();
                break;
            case "renew":
                Public.CheckLogin("6f2255a8-caae-4e2e-9228-e9b61fd3ce99");
                promotion.EditPromotionWholeSaleGroup();
                break;
            case "move":
                Public.CheckLogin("03163e1d-0324-414c-8ec7-a6e1d285aacc");
                promotion.DelPromotionWholeSaleGroup();
                break;
            case "list":
                Public.CheckLogin("83aba19a-e789-43a9-8ffd-d9ba3efd2852");
                Response.Write(promotion.GetPromotionWholeSaleGroups());
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
