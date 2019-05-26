<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    ITools tools;
    protected void Page_Load(object sender, EventArgs e)
    {
        Promotion promotion = new Promotion();
        tools = ToolsFactory.CreateTools();
        string working = Request["action"];
        switch (working) { 
            case "new":
                Public.CheckLogin("b2170460-e90d-4b4f-89b6-c88b75c2989b");
                promotion.AddPromotionFavorGift();
                break;
            //case "move":
            //    Public.CheckLogin("afeba658-4494-424a-a4d8-8db67814aa59");
            //    promotion.DelPromotionFavorGift();
            //    break;
            case "list":
                Public.CheckLogin("15bb07a5-83ee-4157-94c2-693bc4312d74");
                Response.Write(promotion.GetPromotionFavorGifts());
                Response.End();
                break;
            case "addgift":
                Response.Write("<div id=\"gift_" + Request["group"] + "_" + Request["maxnum"] + "\">");
                promotion.Promotion_Gift("promotion_gift" + Request["maxnum"] + "_" + Request["group"],0);
                Response.Write("&nbsp; 赠送数量 <input type=\"text\" name=\"favor_buy_gift" + Request["maxnum"] + "_amount_" + Request["group"] + "\" value=\"0\" size=\"10\"/> <a href=\"javascript:void(0);\" onclick=\"del_gift('gift_" + Request["group"] + "_" + Request["maxnum"] + "')\"><span class=\"t12_blue\">删除</span></a></div>");
                Response.End();
                break;
            case "additem":
                Response.Write("<div id=\"gift_" + Request["group"] + "\">购买金额 ≥ <input type=\"text\" name=\"favor_buy_money" + Request["group"] + "\" value=\"0\" size=\"10\"/>  &nbsp;购买数量 ≥ <input type=\"text\" name=\"favor_buy_amount" + Request["group"] + "\" value=\"0\" size=\"10\"/><input name=\"maxnum_" + Request["group"] + "\" id=\"maxnum_" + Request["group"] + "\" type=\"hidden\" value=\"1\" />  <a href=\"javascript:void(0);\" onclick=\"del_item('gift_" + Request["group"] + "');\"><span class=\"t12_blue\">删除</span></a></div>");

                Response.Write("<div id=\"gift_" + Request["group"] + "_1\">");
                promotion.Promotion_Gift("promotion_gift1_" + Request["group"],0);
                Response.Write("    &nbsp; 赠送数量 <input type=\"text\" name=\"favor_buy_gift1_amount_" + Request["group"] + "\" value=\"0\" size=\"10\"/> <a href=\"javascript:void(0);\" onclick=\"add_gift(" + Request["group"] + ");\"><span class=\"t12_blue\">添加赠品</span></a></div> ");
                Response.Write("<span id=\"gift_more" + Request["group"] + "\"></span>");
                Response.End();
                break;
            case "active":
                Public.CheckLogin("0812bbf1-1dd1-4029-917d-d2ebdd8dcd38");
                promotion.EditPromotionFavorGiftStatus(1);
                break;
            case "cancelactive":
                Public.CheckLogin("0812bbf1-1dd1-4029-917d-d2ebdd8dcd38");
                promotion.EditPromotionFavorGiftStatus(2);
                break;
            case "audit":
                Public.CheckLogin("06d81453-b5b3-4a94-aa07-43c4d3d4f48b");
                promotion.EditPromotionFavorGiftStatus(3);
                break;
            case "cancelaudit":
                Public.CheckLogin("06d81453-b5b3-4a94-aa07-43c4d3d4f48b");
                promotion.EditPromotionFavorGiftStatus(4);
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
