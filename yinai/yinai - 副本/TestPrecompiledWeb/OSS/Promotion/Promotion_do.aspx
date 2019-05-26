<%@ Page Language="C#" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<script runat="server">
    private ITools tools;
    private Promotion myApp;
    int product_id;
    string strPID;
    protected void Page_Load(object sender, EventArgs e)
    {
        tools = ToolsFactory.CreateTools();
        myApp = new Promotion();
        string action = Request["action"];
        switch (action)
        {
            case "new":
                Public.CheckLogin("9569bb39-3d52-49b3-8a70-139778cbecdd");

                Session.Remove("productid_ed");
                myApp.AddPromotion();
                break;
            case "renew":
                Public.CheckLogin("c0330805-14e6-493e-8519-7ca89dddd157");

                Session.Remove("productid_ed");
                myApp.EditPromotion();
                break;
            case "move":
                Public.CheckLogin("8638fc66-772f-4981-af7a-94c128e15ed2");

                myApp.DelPromotion();
                break;
            case "list":
                Public.CheckLogin("0b16441f-dc42-4fd0-b8f1-0f8a80146771");

                Response.Write(myApp.GetPromotions());
                Response.End();
                break;
            case "check_product":                
                strPID = tools.CheckStr(Request.QueryString["product_id"]);
                if (strPID.Length > 0 ) {
                    IList<PromotionProductInfo> entityList = (IList<PromotionProductInfo>)Session["PromotionProductInfo"];
                    PromotionProductInfo entity = null;
                    string[] PIDARR = strPID.Split(',');
                    foreach (string addPID in PIDARR)
                    {
                        if (tools.CheckInt(addPID) < 1) { continue; }

                        entity = new PromotionProductInfo();
                        entity.Promotion_Product_Product_ID = int.Parse(addPID);
                        entity.Promotion_Product_PromotionID = 0;
                        entity.Promotion_Product_ID = 0;
                        entityList.Add(entity);
                    }
                    Session["PromotionProductInfo"] = null;
                    Session["PromotionProductInfo"] = entityList;
                    entityList = null; 
                }
                //Response.Write(myApp.ShowProduct());
                break;
            case "product_del":
                product_id = tools.CheckInt(Request.QueryString["product_id"]);
                if (product_id > 0) {
                    IList<PromotionProductInfo> entityList = (IList<PromotionProductInfo>)Session["PromotionProductInfo"];
                    foreach (PromotionProductInfo entity in entityList)
                    {
                        if (entity.Promotion_Product_Product_ID == product_id) { entityList.Remove(entity); break; }
                    }
                    Session["PromotionProductInfo"] = null;
                    Session["PromotionProductInfo"] = entityList;
                    entityList = null; 
                }
                
                //Response.Write(myApp.ShowProduct());
                break;
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        myApp = null;
    }
</script>
