<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<script runat="server">
    
    private ArticleCate myApp;
    private ITools tools;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        myApp = new ArticleCate();
        tools = ToolsFactory.CreateTools();
        string action = Request["action"];
        switch (action)
        {
            case "new":
                Public.CheckLogin("e049fd16-8e37-4096-aa7e-e20ecb61c934");
                
                myApp.AddArticleCate();
                break;
            case "renew":
                Public.CheckLogin("8e2eb41c-060b-4a1c-9c7c-403d6f1072fa");
                
                myApp.EditArticleCate();
                break;
            case "move":
                Public.CheckLogin("8ad36b15-547d-4ef0-aa55-e4fce614af3c");
                
                myApp.DelArticleCate();
                break;
            case "list":
                Public.CheckLogin("1a3208d0-70a4-49dd-8010-400f1254535a");
                
                Response.Write(myApp.GetArticleCates());
                Response.End();
                break;
            case "change_maincate":
                string target_div = tools.CheckStr(Request.QueryString["target"]);
                int cate_id = tools.CheckInt(Request.QueryString["cate_id"]);
                Response.Write(myApp.Article_Category_Select(cate_id, target_div));
                Response.End();
                break;

        }

    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        myApp = null;
        tools = null;
    }
</script>