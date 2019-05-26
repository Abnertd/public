<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%--<%@ Register Src="~/Public/MEM_Top.ascx" TagPrefix="uctop" TagName="top" %>--%>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>

<!DOCTYPE html>

<% 
    
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    Member member = new Member();
    string sessionid = tools.NullStr(Request["sessionid"]);
    if (sessionid == "")
    {
        Response.Redirect("message_sessionlist.aspx");
    }

%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%="消息内容 - 我是买家 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .sz_info_main {
            border: solid 1px #ccc;
            padding: 27px;
            width: 850px;
        }

        .line_01 {
            padding: 0 30px 0;
            margin: 20px 0;
            line-height: 1px;
            border-left: 350px solid #ddd;
            border-right: 350px solid #ddd;
            text-align: center;
            font-size: 14px;
        }

        .sz_p1 {
            color: #e33848;
        }

        .sz_p2 {
            color: #666;
            text-indent: 2em;
            word-break: break-all;
            word-wrap: break-word;
            display: block;
            width: 800px;
        }
    </style>
</head>
<body>
    <%--<uctop:top ID="top1" runat="server" />--%>

    <!--主体 开始-->
    <div class="content02">
        <div class="content02_main">
            <!--位置说明 开始-->
            <div class="position"> <a href="/member/">采购商用户中心</a> > 辅助功能 > <strong>消息内容</strong></div>
            <!--位置说明 结束-->
            <div class="partc">
                <div class="pc_left">
                    <div class="blk12">
                        <%=member.Member_Left_HTML(3,9) %>
                    </div>
                </div>
                <div class="pc_right" style="width: 972px;">
                    <div class="blk06">
                        <h2>消息内容</h2>
                        <div class="b06_main" style="padding: 15px 32px 15px 32px">
                            <%
                                DatacogradientJsonInfo JsonInfo = member.GetDatacogradien(sessionid, "chatmessage");
                                if (JsonInfo != null && JsonInfo.status.message == "OK" && JsonInfo.chatInfo != null && JsonInfo.chatInfo.messages != null)
                                {
                                    Response.Write("<div class=\"line_01\">" + pub.ConvertIntDateTime(JsonInfo.chatInfo.starttime).ToString("yyyy-MM-dd") + "</div>");

                                    Response.Write("<div>");
                                    Response.Write("<ul>");

                                    IList<DatacogradientMessageInfo> listMessage = JsonInfo.chatInfo.messages;
                                    if (listMessage != null)
                                    {
                                        foreach (DatacogradientMessageInfo entity in listMessage)
                                        {
                                            Response.Write("<li>");
                                            Response.Write("<p style=\"color: #e33848;\">" + entity.srcname + "    " + entity.time + "</p>");
                                            Response.Write("<p style=\"color: #666;text-indent: 2em;word-break: break-all; word-wrap: break-word;display: block; width: 900px;\">" + entity.content + "</p>");
                                            Response.Write("</li>");
                                        }
                                    }
                                    Response.Write("</ul>");
                                    Response.Write("</div>");
                                }
                            %>
                        </div>
                    </div>
                </div>
                <div class="clear"></div>
            </div>
        </div>
    </div>
    <!--主体 结束-->

  <ucbottom:bottom runat="server" ID="Bottom" />
</body>
</html>
