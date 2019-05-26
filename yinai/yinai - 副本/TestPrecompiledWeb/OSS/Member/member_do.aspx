<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="System.Collections.Generic" %>
<script runat="server">
    
    private Member myApp;
    private Feedback feedback;
    private ITools tools;
    
    protected void Page_Load(object sender, EventArgs e)
    {

        myApp = new Member();
        feedback = new Feedback();
        tools = ToolsFactory.CreateTools();
        string action = Request["action"];
        switch (action)
        {
            //case "new":
            //    Public.CheckLogin("5d071ec6-31d8-4960-a77d-f8209bbab496");
            //    myApp.AddRBACRole();
            //    break;
            //case "renew":
            //    Public.CheckLogin("079ec5fc-33fe-4d58-a17f-14b5877b4ffe");
            //    myApp.EditRBACRole();
            //    break;
            //case "move":
            //    Public.CheckLogin("d736b77e-334a-4ce1-9748-7f1a23330a6c");
            //    myApp.DelRBACRole();
            //    break;
            case "updateprofile":
                Public.CheckLogin("079ec5fc-33fe-4d58-a17f-14b5877b4ffe");
                myApp.UpdateMemberProfile();
                break;
            case "list":
                Public.CheckLogin("833b9bdd-a344-407b-b23a-671348d57f76");
                
                Response.Write(myApp.GetMembers());
                Response.End();
                break;
            case "memberexport":
                Public.CheckLogin("29c1d7e3-ef38-4f80-80c8-b376efafe11d");

                myApp.Member_Export();
                Response.End();
                break;
            case "audit":
                Public.CheckLogin("079ec5fc-33fe-4d58-a17f-14b5877b4ffe");
                myApp.Member_Audit(1);
                break;
            case "denyaudit":
                Public.CheckLogin("079ec5fc-33fe-4d58-a17f-14b5877b4ffe");
                myApp.Member_Audit(2);
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
                
                feedback.FeedBack_Reply();
                Response.End();
                break;
            case "feedbackexport":
                Public.CheckLogin("4190769c-4fd5-4013-a701-fe7594c1017f");
                
                feedback.FeedBack_Export();
                Response.End();
                break;
            case "check_member":
                string strPID;
                strPID = tools.CheckStr(Request.QueryString["member_id"]);
                if (strPID.Length > 0)
                {
                    IList<MemberInfo> entityList = (IList<MemberInfo>)Session["EmailMemberInfo"];
                    MemberInfo entity = null;
                    string[] PIDARR = strPID.Split(',');
                    foreach (string addPID in PIDARR)
                    {
                        if (tools.CheckInt(addPID) < 1) { continue; }

                        entity = new MemberInfo();
                        entity.Member_ID = int.Parse(addPID);
                        entityList.Add(entity);
                    }
                    Session["EmailMemberInfo"] = null;
                    Session["EmailMemberInfo"] = entityList;
                    entityList = null;
                }
                Response.Write(myApp.ShowMember());
                break;
            case "member_del":
                int member_id;
                member_id = tools.CheckInt(Request.QueryString["member_id"]);
                if (member_id > 0)
                {
                    IList<MemberInfo> entityList = (IList<MemberInfo>)Session["EmailMemberInfo"];
                    foreach (MemberInfo entity in entityList)
                    {
                        if (entity.Member_ID == member_id) { entityList.Remove(entity); break; }
                    }
                    Session["EmailMemberInfo"] = null;
                    Session["EmailMemberInfo"] = entityList;
                    entityList = null;
                }

                Response.Write(myApp.ShowMember());
                break;
            case "savecert":
                myApp.AddMemberRelateCert();
                break;    
                
                
                
                
                      
        }

    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        myApp = null;
        tools = null;
    }
</script>
