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
                Public.CheckLogin("a2a96dd9-aca6-4889-9382-1eb1bc28ac2b");
                
                Ad.AddAD_Position_Channel();
                break;
            case "renew":
                Public.CheckLogin("c6dba721-72aa-4ca4-86fe-2306566e17eb");
                
                Ad.EditAD_Position_Channel();
                break;
            case "move":
                Public.CheckLogin("9adc558d-446c-41cc-a092-bd1313d855e8");
                
                Ad.DelAD_Position_Channel();
                break;
            case "list":
                Public.CheckLogin("8377a798-c8cb-436e-bb4a-4c0a41635c7f");
                
                Response.Write(Ad.GetAdPositionChannels());
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
