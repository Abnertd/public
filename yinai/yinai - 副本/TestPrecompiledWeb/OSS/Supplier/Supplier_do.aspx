<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="System.Collections.Generic" %>
<script runat="server">
    
    private Supplier myApp;
    private ITools tools;
    private Orders myorders;
    private Feedback feedback;
    
    protected void Page_Load(object sender, EventArgs e)
    {

        myApp = new Supplier();
        myorders = new Orders();
        tools = ToolsFactory.CreateTools();
        feedback = new Feedback();
        
        string action = Request["action"];
        switch (action)
        {
            case "renew":
                Public.CheckLogin("40f51178-030c-402a-bee4-57ed6d1ca03f");
                myApp.EditSupplier();
                break;
            case "remove":
                Public.CheckLogin("d736b77e-334a-4ce1-9748-7f1a23330a6c");
                myApp.SupplierRemove(2);
                break;
            case "trash":
                Public.CheckLogin("dec68736-cc0f-4d60-ad6a-1e6fd96f8d9b");
                myApp.SupplierRemove(1);
                break;
            case "list":
                Public.CheckLogin("1392d14a-6746-4167-804a-d04a2f81d226");                
                Response.Write(myApp.GetSuppliers());
                Response.End();
                break;
            case "audit":
                Public.CheckLogin("d8de7f81-7e9a-44ea-9463-dd1afda2b74e");
                myApp.SupplierAudit(1);
                break;
            case "unaudit":
                Public.CheckLogin("d8de7f81-7e9a-44ea-9463-dd1afda2b74e");
                myApp.SupplierAudit(2);
                break;
            case "settinglist":
                Public.CheckLogin("00b42a78-2cef-4a22-865d-dd2ad9003ec5");

                Response.Write(myorders.Supplier_Commission());
                Response.End();
                break;
            case "AllowOrderEmail":
                Public.CheckLogin("508c538f-25c2-4e38-b7c3-14779900c6d7");
                myApp.SupplierAllowOrdersEmail(1);
                break;
            case "unAllowOrderEmail":
                Public.CheckLogin("508c538f-25c2-4e38-b7c3-14779900c6d7");
                myApp.SupplierAllowOrdersEmail(0);
                break;
            case "certlist":
                Public.CheckLogin("1392d14a-6746-4167-804a-d04a2f81d226");

                Response.Write(myApp.GetSupplierCerts());
                Response.End();
                break;
           
            case "exchange":
                Public.CheckLogin("40f51178-030c-402a-bee4-57ed6d1ca03f");
                myApp.Supplier_Product_Exchange();
                break;
                
            case "passaudit":
                myApp.ChangeSupplierAudit(1);
                break;
            case "notpassaudit":
                myApp.ChangeSupplierAudit(2);
                break;

            case "applylist":
                Response.Write(myApp.GetSupplierShopApplys());
                Response.End();
                break;
                
                
            case "applypassaudit":
                myApp.SupplierApplyAudit(1);
              break;
            case "applynotpassaudit":
              myApp.SupplierApplyAudit(2);
              break;
            case "certaudit":
              myApp.ChangeSupplierCertAudit(2);
              break;
            case "certdeny":
              myApp.ChangeSupplierCertAudit(3);
              break;
            case "check_supplier":
              string strPID;
              strPID = tools.CheckStr(Request.QueryString["supplier_id"]);
              if (strPID.Length > 0)
              {
                  IList<SupplierInfo> entityList = (IList<SupplierInfo>)Session["MessageSupplierInfo"];
                  SupplierInfo entity = null;

                  string[] PIDARR = strPID.Split(',');
                  foreach (string addPID in PIDARR)
                  {
                      if (tools.CheckInt(addPID) < 1) { continue; }

                      entity = new SupplierInfo();
                      entity.Supplier_ID = int.Parse(addPID);
                      if (entityList != null)
                      {
                          entityList.Add(entity);
                      }
                      
                  }
                  Session["MessageSupplierInfo"] = null;
                  Session["MessageSupplierInfo"] = entityList;
                  entityList = null;
              }
              Response.Write(myApp.ShowSupplier());
              break;
            case "supplier_del":
              int supplier_id;
              supplier_id = tools.CheckInt(Request.QueryString["supplier_id"]);
              if (supplier_id > 0)
              {
                  IList<SupplierInfo> entityList = (IList<SupplierInfo>)Session["MessageSupplierInfo"];
                  foreach (SupplierInfo entity in entityList)
                  {
                      if (entity.Supplier_ID == supplier_id) { entityList.Remove(entity); break; }
                  }
                  Session["MessageSupplierInfo"] = null;
                  Session["MessageSupplierInfo"] = entityList;
                  entityList = null;
              }

              Response.Write(myApp.ShowSupplier());
              break;



            case "feedback":
              Public.CheckLogin("9877a09e-5dda-4b1e-bf6f-042504449eeb");

              Response.Write(feedback.GetFeedBacks());
              Response.End();
              break;
            case "feedbackmove":
              Public.CheckLogin("cc567804-3e2e-4c6c-aa22-c9a353508074");

              feedback.FeedBack_Del();
              Response.End();
              break;
            case "feedbackreply":
              Public.CheckLogin("02cc2c2c-9ecc-462a-86dc-406f792ac83a");

              feedback.FeedBack_SupplierReply();
              Response.End();
              break;
            case "feedbackexport":
              Public.CheckLogin("4190769c-4fd5-4013-a701-fe7594c1017f");

              feedback.FeedBack_Export();
              Response.End();
              break;
            case "feedbackfinexport":
              Public.CheckLogin("4190769c-4fd5-4013-a701-fe7594c1017f");

              feedback.FeedBackFin_Export();
              Response.End();
              break;
            case "shopadd":
              Public.CheckLogin("d38ea434-9b11-4668-9fb9-2827a9d22602");

              myApp.SupplierShopAdd();
              Response.End();
              break;
                
            case "contract_settings":
              myApp.Supplier_Contract_Settings();
              Response.End();
              break;


            case "zhongxin_list":
                Public.CheckLogin("1392d14a-6746-4167-804a-d04a2f81d226");

                Response.Write(myApp.GetzhongxinSuppliers());
                Response.End();
                break;


            //case "feedbackmove":
            //    Public.CheckLogin("cc567804-3e2e-4c6c-aa22-c9a353508074");

            //    feedback.FeedBack_Del();
            //    Response.End();
            //    break;
            case "signup":
                Public.CheckLogin("eb0180d5-4df5-4988-87ec-d6ac4e98fc8f");
                Response.Write(myApp.GetSuppliersSignUpInfo());
                Response.End();
                break;
                
        }

    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        myApp = null;
        tools = null;
    }
</script>
