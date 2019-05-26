<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        ShoppingAsk shopask = new ShoppingAsk();    
        string working = Request["action"];
        switch (working) { 
            case "renew":
                Public.CheckLogin("b2a9d36e-9ba5-45b6-8481-9da1cd12ace0");
                
                shopask.EditShoppingAsk();
                break;
            case "move":
                Public.CheckLogin("9cf98bf5-6f7c-4fbc-973e-a92c9a37c732");
                
                shopask.DelShoppingAsk();
                break;
            case "list":
                Public.CheckLogin("fe2e0dd7-2773-4748-915a-103065ed0334");
                
                Response.Write(shopask.GetShoppingAsks());
                Response.End();
                break;
            case "audit":
                Public.CheckLogin("fe2e0dd7-2773-4748-915a-103065ed0334");

                shopask.ShoppingAsk_Audit();
                Response.End();
                break;
        }

    }
</script>


