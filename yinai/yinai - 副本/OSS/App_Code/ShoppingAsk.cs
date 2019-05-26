using System;
using System.Text;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.Util.Mail;
using Glaer.Trade.B2C.BLL.Product;
using Glaer.Trade.B2C.BLL.MEM;

/// <summary>
///ShoppingAsk 的摘要说明
/// </summary>
public class ShoppingAsk
{

    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private IShoppingAsk MyBLL;
    private IMember MyMem;
    private IProduct MyProduct;
    private Product ProApp;
    private Member MEMApp;


    public ShoppingAsk()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        MyBLL = ShoppingAskFactory.CreateShoppingAsk();
        MyMem = MemberFactory.CreateMember();
        MyProduct = ProductFactory.CreateProduct();

        ProApp = new Product();
        MEMApp = new Member();
    }



    public void EditShoppingAsk()
    {
        string target = tools.CheckStr(Request.QueryString["target"]);
        int Ask_ID = tools.CheckInt(Request.Form["Ask_ID"]);
        string Ask_Reply = tools.CheckStr(Request.Form["Ask_Reply"]);
        int Ask_Isreply=0;
        if (Ask_Reply == "")
        {
            Ask_Isreply = 0;
        }
        else
        {
            Ask_Isreply = 1;
        }


        ShoppingAskInfo entity = MyBLL.GetShoppingAskByID(Ask_ID, Public.GetUserPrivilege());
        if (entity != null)
        {
            entity.Ask_Reply = Ask_Reply;

            entity.Ask_Isreply = Ask_Isreply;


            if (MyBLL.EditShoppingAsk(entity, Public.GetUserPrivilege()))
            {
                if (target.Length>0)
                {
                    Public.Msg("positive", "操作成功", "操作成功", true, "Shopping_Ask_list.aspx?target=" + target + "");
                }
                else
                {
                    Public.Msg("positive", "操作成功", "操作成功", true, "Shopping_Ask_list.aspx");
                }
              
            }
            else
            {
                Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void DelShoppingAsk()
    {
        int Ask_ID = tools.CheckInt(Request.QueryString["Ask_ID"]);
        ShoppingAskInfo entity = GetShoppingAskByID(Ask_ID);
        if (entity != null)
        {
            if (MyBLL.DelShoppingAsk(Ask_ID, Public.GetUserPrivilege()) > 0)
            {
                if (entity.Ask_ProductID == 0)
                {
                    Public.Msg("positive", "操作成功", "操作成功", true, "Shopping_Ask_list.aspx?target=supplier");
                }
                else
                {
                    Public.Msg("positive", "操作成功", "操作成功", true, "Shopping_Ask_list.aspx");
                }
            }
            else
            {
                Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public ShoppingAskInfo GetShoppingAskByID(int cate_id)
    {
        return MyBLL.GetShoppingAskByID(cate_id, Public.GetUserPrivilege());
    }

    //获取咨询信息列表
    public string GetShoppingAsks()
    {
        string keyword = tools.CheckStr(Request["keyword"]);
        int isreply = tools.CheckInt(Request["isreply"]);
        int ischeck = tools.CheckInt(Request["ischeck"]);
        string target = tools.CheckStr(Request["target"]);
        string productidstr,memberidstr;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ShoppingAskInfo.Ask_Site", "=", Public.GetCurrentSite()));
        if (isreply == 1)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ShoppingAskInfo.Ask_Isreply", "=", "1"));
        }
        else if (isreply == 2)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ShoppingAskInfo.Ask_Isreply", "=", "0"));
        }
        if (ischeck == 1)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ShoppingAskInfo.Ask_IsCheck", "=", "1"));
        }
        else if (ischeck == 2)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ShoppingAskInfo.Ask_IsCheck", "=", "0"));
        }
        if (target == "supplier")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ShoppingAskInfo.Ask_ProductID", "=", "0"));
        }
        else
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ShoppingAskInfo.Ask_ProductID", ">", "0"));
        }
        if (keyword.Length > 0)
        {
            if (keyword == "游客")
            {
                memberidstr = MEMApp.GetMemberIDByKeyword("");
            }
            else
            {
                memberidstr = MEMApp.GetMemberIDByKeyword(keyword); 
            }
            productidstr = ProApp.GetProductIDByKeyword(keyword);
            
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ShoppingAskInfo.Ask_ID", ">", "0"));
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ShoppingAskInfo.Ask_Content", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR", "int", "ShoppingAskInfo.Ask_MemberID", "in", memberidstr));
            Query.ParamInfos.Add(new ParamInfo("OR)", "int", "ShoppingAskInfo.Ask_ProductID", "in", productidstr));
        }
        
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));
        PageInfo pageinfo = MyBLL.GetPageInfo(Query, Public.GetUserPrivilege());
        IList<ShoppingAskInfo> entitys = MyBLL.GetShoppingAsks(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (ShoppingAskInfo entity in entitys)
            {
                jsonBuilder.Append("{\"id\":" + entity.Ask_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Ask_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Ask_Content));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.Ask_MemberID > 0)
                {
                    MemberInfo member = MyMem.GetMemberByID(entity.Ask_MemberID, Public.GetUserPrivilege());
                    if (member != null)
                    {
                        jsonBuilder.Append(Public.JsonStr(member.Member_NickName));
                    }
                    else
                    {
                        jsonBuilder.Append("未知");
                    }
                }
                else
                {
                    jsonBuilder.Append("游客");
                }
                
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.Ask_ProductID > 0)
                {
                    ProductInfo product = MyProduct.GetProductByID(entity.Ask_ProductID, Public.GetUserPrivilege());
                    if (product != null)
                    {
                        jsonBuilder.Append(Public.JsonStr(product.Product_Name));
                    }
                    else
                    {
                        jsonBuilder.Append("未知");
                    }
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.Ask_IsCheck == 0)
                {
                    jsonBuilder.Append("<span class=\\\"t12_red\\\">未审核</a>");
                }
                else
                {
                    jsonBuilder.Append("已审核");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.Ask_Isreply == 0)
                {
                    jsonBuilder.Append("<span class=\\\"t12_red\\\">未回复</a>");
                }
                else
                {
                    jsonBuilder.Append("已回复");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Ask_Addtime);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");

                if (Public.CheckPrivilege("b2a9d36e-9ba5-45b6-8481-9da1cd12ace0"))
                {
                   //   jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\"> <a href=\\\"Shopping_ask_edit.aspx?ask_id=" + entity.Ask_ID + "\\\" title=\\\"回复\\\"  target=\\\"_black\\\">回复</a>");
                    jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\"> <a href=\\\"Shopping_ask_edit.aspx?ask_id=" + entity.Ask_ID + "&target=" + target + "\\\" title=\\\"回复\\\" >回复</a>");
                }
                jsonBuilder.Append("<img src=\\\"/images/icon_view.gif\\\"> <a href=\\\"Shopping_ask_view.aspx?ask_id=" + entity.Ask_ID + "&target=" + target + "\\\" title=\\\"查看\\\">查看</a>");
                if (Public.CheckPrivilege("9cf98bf5-6f7c-4fbc-973e-a92c9a37c732"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('shopping_ask_do.aspx?action=move&ask_id=" + entity.Ask_ID + "')\\\" title=\\\"删除\\\">删除</a>");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        else
        {
            return null;
        }

    }

    public void ShoppingAsk_Audit()
    {
        string ask_id = tools.CheckStr(Request.QueryString["ask_id"]);
        string target = tools.CheckStr(Request.QueryString["target"]);
        if (ask_id == "")
        {
            Public.Msg("error", "错误信息", "请选择要审核的记录", false, "{back}");
            return;
        }

        if (tools.Left(ask_id, 1) == ",") { ask_id = ask_id.Remove(0, 1); }

        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ShoppingAskInfo.Ask_ID", "in", ask_id));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ShoppingAskInfo.Ask_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("ShoppingAskInfo.Ask_ID", "DESC"));
        IList<ShoppingAskInfo> entitys = MyBLL.GetShoppingAsks(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            foreach (ShoppingAskInfo entity in entitys)
            {
                entity.Ask_IsCheck = 1;
                MyBLL.EditShoppingAsk(entity, Public.GetUserPrivilege());
            }
        }
        Public.Msg("positive", "操作成功", "操作成功", true, "shopping_ask_list.aspx?target=" + target);
    }

    

    
    
}
