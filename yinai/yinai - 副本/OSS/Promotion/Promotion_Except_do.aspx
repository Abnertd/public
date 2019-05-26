<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="System.Collections.Generic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    ITools tools;
    protected void Page_Load(object sender, EventArgs e)
    {
        tools = ToolsFactory.CreateTools();
        Promotion promotion = new Promotion();
        string working = Request["action"];
        switch (working) { 
            case "list":
                Public.CheckLogin("db71e6f9-f858-4469-b45e-b6ab55412853");
                Response.Write(promotion.GetPromotionFavorExcepts());
                Response.End();
                break;
            case "sublist":
                Response.Write(promotion.GetSubPromotions());
                Response.End();
                break;
            case "active":
                promotion.EditPromotionFavorFeeStatus(1);
                Response.End();
                break;
            case "exceptproductid":
                promotion.AddPromotionExcept();
                Response.End();
                break;
            case "feeexceptproduct":
                promotion.AddPromotionExcept();
                Response.Write(promotion.ShowFee_FavorProduct("valid",tools.CheckInt(Request["promotion_id"])));
                Response.End();
                break;
            case "feevalidproduct":
                promotion.DelPromotionExcept();
                Response.Write(promotion.ShowFee_FavorProduct("valid", tools.CheckInt(Request["promotion_id"])));
                Response.End();
                break;
            case "refreshexceptfee":
                Response.Write(promotion.ShowFee_FavorProduct("except", tools.CheckInt(Request["promotion_id"])));
                Response.End();
                break;
            case "policyexceptproduct":
                promotion.AddPromotionExcept();
                Response.Write(promotion.ShowPolicy_FavorProduct("valid", tools.CheckInt(Request["promotion_id"])));
                Response.End();
                break;
            case "policyvalidproduct":
                promotion.DelPromotionExcept();
                Response.Write(promotion.ShowPolicy_FavorProduct("valid", tools.CheckInt(Request["promotion_id"])));
                Response.End();
                break;
            case "refreshexceptpolicy":
                Response.Write(promotion.ShowPolicy_FavorProduct("except", tools.CheckInt(Request["promotion_id"])));
                Response.End();
                break;
            case "giftexceptproduct":
                promotion.AddPromotionExcept();
                Response.Write(promotion.ShowGift_FavorProduct("valid", tools.CheckInt(Request["promotion_id"])));
                Response.End();
                break;
            case "giftvalidproduct":
                promotion.DelPromotionExcept();
                Response.Write(promotion.ShowGift_FavorProduct("valid", tools.CheckInt(Request["promotion_id"])));
                Response.End();
                break;
            case "refreshexceptgift":
                Response.Write(promotion.ShowGift_FavorProduct("except", tools.CheckInt(Request["promotion_id"])));
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
