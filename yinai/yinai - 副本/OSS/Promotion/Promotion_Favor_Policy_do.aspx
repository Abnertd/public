<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        Promotion promotion = new Promotion();
        string working = Request["action"];
        switch (working) { 
            case "new":
                Public.CheckLogin("6ee2adbb-590f-4c59-b7b3-da7266977106");
                promotion.AddPromotionFavorPolicy();
                break;
            //case "move":
            //    Public.CheckLogin("efa62de5-d713-4d0b-b46f-2a3ea42bb65e");
            //    promotion.DelPromotionFavorPolicy();
            //    break;
            case "list":
                Public.CheckLogin("b71d572b-93b5-462f-ad32-76f97f4fb8f4");
                Response.Write(promotion.GetPromotionFavorPolicys());
                Response.End();
                break;
            case "active":
                Public.CheckLogin("52866b4b-2f8b-4f14-8065-f1d9d8c3151f");
                promotion.EditPromotionFavorPolicyStatus(1);
                break;
            case "cancelactive":
                Public.CheckLogin("52866b4b-2f8b-4f14-8065-f1d9d8c3151f");
                promotion.EditPromotionFavorPolicyStatus(2);
                break;
            case "audit":
                Public.CheckLogin("e55ca6db-c879-4bf8-8a02-f006dccf444d");
                promotion.EditPromotionFavorPolicyStatus(3);
                break;
            case "cancelaudit":
                Public.CheckLogin("e55ca6db-c879-4bf8-8a02-f006dccf444d");
                promotion.EditPromotionFavorPolicyStatus(4);
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
