<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<style type="text/css">  
	#print_confirm {
		border-top: #999999 1px solid; width: 100%; padding-top: 4px; position: absolute; height: 30px; background-color: #5473ae
	}
	#btn_print {
		background-image: url(images/btn_print.gif); margin-left: auto; width: 71px; cursor: pointer; margin-right: auto; height: 24px
	}
	#barcode {
		background: url(images/bar_code.gif) no-repeat; width: 150px; height: 50px
	}
</style> 

<script runat="server">
    
    private ITools tools;
    private Orders myApp;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("c7d71545-1f4e-4f61-8055-1cd24ef4a596");
        tools = ToolsFactory.CreateTools();
        myApp = new Orders();

        myApp.ShoppingList(tools.CheckInt(Request.QueryString["orders_id"]));
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        myApp = null;
    }
</script>
