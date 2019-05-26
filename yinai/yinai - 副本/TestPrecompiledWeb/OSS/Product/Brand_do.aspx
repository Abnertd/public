<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        Brand brand = new Brand();
        string working = Request["action"];
        switch (working) { 
            case "new":
                Public.CheckLogin("31d0ad2e-dd9b-4a04-a800-407b8ba3c9e9");
                    
                brand.AddBrand();
                break;
            case "renew":
                Public.CheckLogin("9592b436-454a-42cf-83f4-0d9ce83c339a");
                
                brand.EditBrand();
                break;
            case "move":
                Public.CheckLogin("3297a5d3-44e6-4318-aa23-4d31288a291b");
                
                brand.DelBrand();
                break;
            case "list":
                Public.CheckLogin("9b17d437-fb2a-4caa-821e-daf13d9efae4");
                  
                Response.Write(brand.GetBrands());
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
