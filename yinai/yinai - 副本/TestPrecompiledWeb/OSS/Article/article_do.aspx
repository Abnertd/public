<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<script runat="server">
    
    private Article myApp;
    private ITools tools;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        myApp = new Article();
        tools = ToolsFactory.CreateTools();
        string action = Request["action"];
        switch (action)
        {
            case "new":
                Public.CheckLogin("870e6332-ab75-41cc-98c3-17e8af7827d3");
                
                myApp.AddArticle();
                break;
            case "renew":
                Public.CheckLogin("1daab676-20b6-4073-af76-132ee8874556");
                
                myApp.EditArticle();
                break;
            case "move":
                Public.CheckLogin("cc00c494-d211-438c-baef-ac20d419b066");
                
                myApp.DelArticle();
                break;
            case "list":
                Public.CheckLogin("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276");
                
                Response.Write(myApp.GetArticles());
                Response.End();
                break;

            case "change_maincate":
                string target_div = tools.CheckStr(Request.QueryString["target"]);
                int cate_id = tools.CheckInt(Request.QueryString["cate_id"]);
                Response.Write(new ArticleCate().Article_Category_Select(cate_id, target_div));
                // new Article().AddArticle
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