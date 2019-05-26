<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%
        ITools tools;
        Promotion myApp;
        tools = ToolsFactory.CreateTools();
        myApp = new Promotion();
        string action = Request["action"];
        switch (action)
        {
            case "isblank":
                Response.Write(Public.Check_IsBlank(tools.NullStr(Request["val"])));
                break;
            case "checkpayline":
                if (tools.CheckFloat(Request["val"]) <= 0)
                {
                    Response.Write("<span class=\"tip_bg_error\">购买金额不可小于等于0！</span>");
                }
                else
                {
                    Response.Write("<span class=\"tip_bg_positive\">购买金额输入正确！</span>");
                }
                break;
            case "checkprice":
                if (tools.CheckFloat(Request["val"]) <= 0)
                {
                    Response.Write("<span class=\"tip_bg_error\">优惠金额不可小于等于0！</span>");
                }
                else
                {
                    Response.Write("<span class=\"tip_bg_positive\">优惠金额输入正确！</span>");
                }
                break;
            case "checkpercent":
                if (tools.CheckFloat(Request["val"]) <= 0)
                {
                    Response.Write("<span class=\"tip_bg_error\">优惠百分比不可小于等于0！</span>");
                }
                else
                {
                    Response.Write("<span class=\"tip_bg_positive\">优惠百分比输入正确！</span>");
                }
                break;
            case "checkcatebrand":
                if (Request["cateall"]!="true")
                {
                    if (tools.NullStr(Session["selected_cateid"]) == "")
                    {
                        Response.Write("<span class=\"tip_bg_error\">请选择指定类别！</span>");
                    }
                }
                if (Request["brandall"] != "true")
                {
                    if (tools.NullStr(Session["selected_brandid"]) == "")
                    {
                        Response.Write("<span class=\"tip_bg_error\">请选择指定品牌！</span>");
                    }
                }
                break;
            case "checkproduct":
                if (Request["productall"] != "true")
                {
                    if (tools.NullStr(Session["selected_productid"]) == "")
                    {
                        Response.Write("<span class=\"tip_bg_error\">请选择指定产品！</span>");
                    }
                }
                
                break;
        }
    myApp=null;
 %>
