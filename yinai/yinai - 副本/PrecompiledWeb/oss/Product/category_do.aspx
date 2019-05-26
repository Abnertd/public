<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        Category category = new Category();
        string action = Request["action"];
        switch (action)
        { 
            case "new":
                Public.CheckLogin("402897ef-7473-4abd-9425-6220a61be7bf");
                
                category.AddCategory();
                break;
            case "renew":
                Public.CheckLogin("2dcee4f1-71e1-4cbd-afa3-470f0b554fd0");
                
                category.EditCategory();
                break;
            case "move":
                Public.CheckLogin("14d9fa43-7e21-4eed-8955-39fafce6f185");
                
                category.DelCategory();
                break;
            case "list":
                Public.CheckLogin("2883de94-8873-4c66-8f9a-75d80c004acb");
                
                Response.Write(category.GetCategorys());
                Response.End();
                break;
        }

    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
