<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        Supplier supplier = new Supplier();
        supplier.Supplier_Login_Check("/supplier/Product_List.aspx");
        string action = Request["action"];
        ITools tools = ToolsFactory.CreateTools();
        switch (action)
        {
          
            case "catesave":
                supplier.SupplierProductCategorySave();
                break;
            case "catedel":
                supplier.SupplierProductCategory_Del();
                break;
            case "change_maincate":
                string target_div = tools.CheckStr(Request.QueryString["target"]);
                int cate_id = tools.CheckInt(Request.QueryString["cate_id"]);
                Response.Write(supplier.Product_Category_Select(cate_id, target_div));
                Response.End();
                break;
         
            case "new":
                supplier.AddProduct(); 
                break;
            case "renew":
                supplier.EditProduct();
                break;
            case "renewseo":
                supplier.EditProductSEO();
                break;
            case "del":
                supplier.DelProduct();
                break;
            case "check_product":
                
                string strID = tools.CheckStr(Request["product_id"]);
                if (strID.Length > 0)
                    Session["selected_productid"] += "," + strID;
                
                Response.Write(supplier.ShowProduct());
                break;
            case "product_del":
                int product_id = tools.CheckInt(Request.QueryString["product_id"]);
                string strProID = (tools.NullStr(Session["selected_productid"]) + ",").Replace("," + product_id + ",", ",");
                Session["selected_productid"] = strProID.Trim(',');
                
                Response.Write(supplier.ShowProduct());
                break;
        
            case "getprice":
                Response.Write(supplier.GetProductPrice());
                break;

            case "loadextend":
                Response.Write(supplier.ProductExtendEditorDisplay());
                break;

            case "extend_append":
                Response.Write(supplier.ProductExtendAppend());
                break;

            case "check_productcode":
                supplier.Check_Product_Code();
                break;
                
            case "check_step":
                supplier.Check_Product_Step();
                break;
        }
    }
</script>
