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

        

        switch (action)
        {
            case "product":
                member.Member_Favorites_Add("product", id);
                break;
            case "shop":
                member.Member_Favorites_Add("shop", id);
                break;
        }
    }
</script>
