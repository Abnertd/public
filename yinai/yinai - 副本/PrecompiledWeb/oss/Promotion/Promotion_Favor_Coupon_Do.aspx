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
        int member_id;
        switch (working) { 
            case "new":
                Public.CheckLogin("d3489f81-bf49-46dc-8222-284f1e0aabbd");
                promotion.AddPromotionFavorCoupon();
                break;
            case "move":
                Public.CheckLogin("d394b6b8-560a-49b9-9d20-1a356d3bf984");
                promotion.DelPromotionFavorCoupon();
                break;
            case "list":
                Public.CheckLogin("18cde8c2-8be5-4b15-b057-795726189795");
                Response.Write(promotion.GetPromotionFavorCoupons());
                Response.End();
                break;
            case "coupon_export":
                Public.CheckLogin("18cde8c2-8be5-4b15-b057-795726189795");
                promotion.PromotionFavorCoupons_Export();
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
