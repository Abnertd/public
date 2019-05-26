<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        Supplier supplier = new Supplier();
        Public_Class pub = new Public_Class();
        ITools tools;
        tools = ToolsFactory.CreateTools();

        string action = Request["action"];
        int id = tools.CheckInt(tools.NullStr(Request.QueryString["id"]));

        supplier.Supplier_Login_Check("/supplier/fav_do.aspx?action=" + action + "&id=" + id);

        switch (action)
        {
            //case "buying":
            //    //supplier.Supplier_Favorites_Add("buying",id);
            //    //break;
            //case "product":
            //    //supplier.Supplier_Favorites_Add("product", id);
            //    //break;
            //case "shop":
            //    supplier.Supplier_Favorites_Add(action, id);
            //    break;
            //case "buying_move":
            //case "product_move":
            //case "shop_move":
            //    supplier.Supplier_Favorites_Del(id);
            //    break;
            case "product":
                supplier.Supplier_Favorites_Add("product", id);
                break;
            case "goods_move":
                supplier.Supplier_Favorites_Del(id);
                break;
            case "shop":
                supplier.Supplier_Favorites_Add("shop", id);
                break;

            case "shop_move":
                supplier.Supplier_Favorites_Del(id);
                break;
            //case "ajax_goods":
            //    member.Ajax_Member_Favorites_Add("ajax_goods", id);
            //    break; 
            //case "ajax_wholesale":
            //    member.Ajax_Member_WholeSale_ApplyAdd(id);
            //    break;
            //case "comment_move":
            //    member.DelSupplierShopEvaluate();
            //    break;

        }

    }
</script>
