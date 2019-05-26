<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<script runat="server">
    private SysMenu myApp;
    private ITools tools;
    protected void Page_Load(object sender, EventArgs e)
    {
        myApp = new SysMenu();
        tools = ToolsFactory.CreateTools();
        string action = Request["action"];
        switch (action)
        {
            case "new":
                Public.CheckLogin("c9ce4dd0-6391-4fb9-aa99-f37c23c04a8a");
                
                myApp.AddSysMenu();
                break;
            case "renew":
                Public.CheckLogin("7daf4ba3-15af-4c7f-a9f5-ab0f9413ff08");

                myApp.EditSysMenu();
                break;
            case "move":
                Public.CheckLogin("e5e043cc-5085-41f9-b406-808c319b3a70");

                myApp.DelSysMenu();
                break;
            case "list":
                Public.CheckLogin("4d14d977-e839-4322-ae0d-fa257030dd2b");

                Response.Write(myApp.GetSysMenus());
                Response.End();
                break;
            case "changemenu":
                myApp.Select_Menu_Parent("Sys_Menu_ParentID", 0, tools.CheckInt(Request["channel"]));
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
