<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    ITools tools;
    protected void Page_Load(object sender, EventArgs e)
    {
        tools = ToolsFactory.CreateTools();
        AD Ad = new AD();
        string working = Request["action"];
        
        switch (working) { 
            case "new":
                Public.CheckLogin("876d73fe-0893-41e7-b44b-062713a6b190");
                
                Ad.AddAD();
                break;
            case "renew":
                Public.CheckLogin("c47f67fa-1142-459d-b466-e3216848ff9c");
                
                Ad.EditAD();
                break;
            case "move":
                Public.CheckLogin("6087aa59-bd66-4eb5-8fb0-f72da294b1ae");
                
                Ad.DelAD();
                break;
            case "list":
                Public.CheckLogin("237da5cb-1fa2-4862-be25-d83077adeb01");
                
                Response.Write(Ad.GetAds());
                Response.End();
                break;
            case "applylist":
                Public.CheckLogin("bbefe763-2057-4d30-8af3-e19cdd484e00");

                Response.Write(Ad.GetAd_Applys());
                Response.End();
                break;
            case "audit":
                Public.CheckLogin("bbefe763-2057-4d30-8af3-e19cdd484e00");

                Ad.ADApply_Audit_Edit(1);
                Response.End();
                break;
            case "unaudit":
                Public.CheckLogin("bbefe763-2057-4d30-8af3-e19cdd484e00");

                Ad.ADApply_Audit_Edit(2);
                Response.End();
                break;
            case "adpositionchannel":
                Public.CheckLogin("237da5cb-1fa2-4862-be25-d83077adeb01");
                int channel_id = tools.CheckInt(Request["channel_id"]);
                Ad.Select_AD_Position1("Ad_Kind", "", channel_id);
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
