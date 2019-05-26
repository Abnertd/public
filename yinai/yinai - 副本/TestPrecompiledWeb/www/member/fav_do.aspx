<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        Member member = new Member();
        Public_Class pub = new Public_Class();
        ITools tools;
        tools = ToolsFactory.CreateTools();

        string action = Request["action"];
        int id = tools.CheckInt(tools.NullStr(Request.QueryString["id"]));

        //member.Member_Login_Check("/member/fav_do.aspx?action=" + action + "&id=" + id);

        switch (action)
        {
            case "product":
                member.Member_Favorites_Add("product", id);
                break;
            case "goods_move":
                member.Member_Favorites_Del(id);
                break;
            case "shop":
                member.Member_Favorites_Add("shop", id);
                break;
            case "shop_move":
                member.Member_Favorites_Del(id);
                break;
                
            case "PurchaseAdd":
                member.AddMemberPurchase();
                break;
            case "PurchaseEdit":
                member.EditMemberPurchase();
                break;
            case "PurchaseMove":
                member.DelMemberPurchase();
                break;
        }
    }
</script>
