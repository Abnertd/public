<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    ITools tools;
    protected void Page_Load(object sender, EventArgs e)
    {
        tools = ToolsFactory.CreateTools();
        Promotion promotion = new Promotion();
        string working = Request["action"];
        switch (working) { 
            case "new":
                Public.CheckLogin("7cd745a6-dfce-4f33-8453-a64cc07c44c9");
                promotion.AddPromotionFavorFee();
                break;
            //case "move":
            //    Public.CheckLogin("6919abfd-abf3-4537-98ca-0cb49e206c5e");
            //    promotion.DelPromotionFavorFee();
            //    break;
            case "list":
                Public.CheckLogin("db71e6f9-f858-4469-b45e-b6ab55412853");
                Response.Write(promotion.GetPromotionFavorFees());
                Response.End();
                break;
            case "active":
                Public.CheckLogin("e067b0bd-80be-461d-96d0-d3887e5c2c3d");
                promotion.EditPromotionFavorFeeStatus(1);
                break;
            case "cancelactive":
                Public.CheckLogin("e067b0bd-80be-461d-96d0-d3887e5c2c3d");
                promotion.EditPromotionFavorFeeStatus(2);
                break;
            case "audit":
                Public.CheckLogin("8358602a-fa75-4854-9635-d9f5f725123b");
                promotion.EditPromotionFavorFeeStatus(3);
                break;
            case "cancelaudit":
                Public.CheckLogin("8358602a-fa75-4854-9635-d9f5f725123b");
                promotion.EditPromotionFavorFeeStatus(4);
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
