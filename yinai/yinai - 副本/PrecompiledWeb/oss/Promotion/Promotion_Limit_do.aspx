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
                promotion.AddPromotionLimit();
                break;
            case "renew":
                Public.CheckLogin("34b7b99f-451c-4c0b-8da1-e3ba000891a8");
                promotion.EditPromotionLimit();
                break;
            case "move":
                Public.CheckLogin("470c7741-f942-42df-9973-c36555c8d2e6");
                promotion.DelPromotionLimit();
                break;
            case "list":
                Public.CheckLogin("22d21441-155a-4dc5-aec6-dcf5bdedd5cf");
                Response.Write(promotion.GetPromotionLimits());
                Response.End();
                break;
            case "check_product":
                strPID = tools.CheckStr(Request.QueryString["product_id"]);
                if (strPID.Length > 0)
                {
                    IList<PromotionProductInfo> entityList = (IList<PromotionProductInfo>)Session["PromotionProductInfo"];
                    PromotionProductInfo entity = null;
                    string[] PIDARR = strPID.Split(',');
                    foreach (string addPID in PIDARR)
                    {
                        if (tools.CheckInt(addPID) < 1) { continue; }

                        entity = new PromotionProductInfo();
                        entity.Promotion_Product_Product_ID = int.Parse(addPID);
                        entity.Promotion_Product_PromotionID = 0;
                        entity.Promotion_Product_ID = 0;
                        entityList.Add(entity);
                    }
                    Session["PromotionProductInfo"] = null;
                    Session["PromotionProductInfo"] = entityList;
                    entityList = null;
                }
                Response.Write(promotion.Limit_ShowProduct());
                break;
            case "listsortedit":
                Public.CheckLogin("854d2b3f-8bf6-4f17-9e5e-4bc1fe784a5d");

                promotion.LimitProduct_EditSortList();
                Response.End();
                break;
            case "product_del":
                product_id = tools.CheckInt(Request.QueryString["product_id"]);
                if (product_id > 0)
                {
                    IList<PromotionProductInfo> entityList = (IList<PromotionProductInfo>)Session["PromotionProductInfo"];
                    foreach (PromotionProductInfo entity in entityList)
                    {
                        if (entity.Promotion_Product_Product_ID == product_id) { entityList.Remove(entity); break; }
                    }
                    Session["PromotionProductInfo"] = null;
                    Session["PromotionProductInfo"] = entityList;
                    entityList = null;
                }

                Response.Write(promotion.Limit_ShowProduct());
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
