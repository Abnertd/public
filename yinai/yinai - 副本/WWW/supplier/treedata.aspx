<%@ Page Language="C#" %>
<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        Supplier supplier = new Supplier();
        supplier.Supplier_Login_Check("/supplier/index.aspx");
        string cate_id = Request.QueryString["cate_id"];
        
        Response.Buffer = true;
        Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
        Response.Expires = 0;
        Response.CacheControl = "no-cache";
        Response.ContentType = "text/xml";
        Response.Write("<?xml version='1.0' encoding='utf-8'?>");
        Response.Write("<tree id=\"0\">");
        Response.Write(supplier.SupplierCategoryTree(0, cate_id));
        Response.Write("</tree>");
    }
</script>
