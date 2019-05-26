<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        AD Ad = new AD();
        string working = Request["action"];
        switch (working) { 
            case "new":
                Public.CheckLogin("47effe97-dab0-4c96-9f74-648976c381e7");
                
                Ad.AddAD_Position();
                break;
            case "renew":
                Public.CheckLogin("afbc3245-62b5-4eb3-aefb-c6c8f3e2b02d");
                
                Ad.EditAD_Position();
                break;
            case "move":
                Public.CheckLogin("67c30881-650c-4f84-aa81-08e2e379798c");
                
                Ad.DelAD_Position();
                break;
            case "list":
                Public.CheckLogin("d3aa1596-cc86-46c7-80f0-8bf6248ee31e");
                
                Response.Write(Ad.GetAdPositions());
                Response.End();
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
