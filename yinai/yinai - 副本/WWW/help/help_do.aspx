<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        ITools tools = ToolsFactory.CreateTools();
        Product product = new Product();
        CMS cms = new CMS();
        string action = Request["action"];
        switch (action)
        {
            case "File_a_complaint":
                product.Shopping_Ask_Add();
                break;
        }

    }
</script>