<%@ Page Language="C#" %>
<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("a8dcfdfb-2227-40b3-a598-9643fd4c7e18/854d2b3f-8bf6-4f17-9e5e-4bc1fe784a5d");
        Product product = new Product();
        string cate_id = Request.QueryString["cate_id"];
        
        Response.Buffer = true;
        Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
        Response.Expires = 0;
        Response.CacheControl = "no-cache";
        Response.ContentType = "text/xml";
        Response.Write("<?xml version='1.0' encoding='utf-8'?>");
        Response.Write("<tree id=\"0\">");
        Response.Write(product.CategoryTree(0, cate_id));
        Response.Write("</tree>");
    }
</script>
