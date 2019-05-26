<%@ Page Language="C#" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Register Src="~/Public/Top.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>
<% 
    CMS cms = new CMS();
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    //Product product = new Product();

    HelpInfo help = null;
    Session["Position"] = "";
    string help_title = "帮助中心";
    int help_id = 0, cate_id = 0;
    string hkeyword = "";
    hkeyword = tools.CheckStr(Request["hkeyword"]);
    string default_key = "";
    if (hkeyword.Length == 0)
    {
        default_key = "输入需要查询的关键词";
    }
    else
    {
        default_key = hkeyword;
    }
    if (hkeyword == "输入需要查询的关键词")
    {
        hkeyword = "";
    }
    help_id = tools.CheckInt(Request["help_id"]);
    //cate_id = tools.CheckInt(Request["cate_id"]);
    help = cms.GetHelpByIDorCateID(help_id, cate_id);
    if (help != null)
    {
        if (help.Help_IsActive != 0)
        {
            help_title = help.Help_Title;
            cate_id = help.Help_CateID;
        }
        else
        {
            help_title = "帮助中心";
            help_id = 0;
        }
    }
    else
    {
        help_title = "帮助中心";
        help_id = 0;
    }    
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>
        <%=help_title + " - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index_newadd.css" rel="stylesheet" type="text/css" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/jquery.js"></script>
</head>
<body>
    <uctop:top ID="top1" runat="server" />
    <!--主体 开始-->
    <div class="content" style=" margin-top:0;">
        <div class="content_main">
        <!--位置说明 开始-->
            <div class="position"><a href="/index.aspx">首页</a> > 帮助中心 > <strong><%=help_title %></strong></div>
        <!--位置说明 结束-->
        <div class="partk">
            <div class="pk_left">
                <div class="blk24">
                    <h2><img src="/images/h2_icon01.png">帮助中心</h2>
                    <div class="main">
                        <%=cms.Help_Left_Nav(cate_id, help_id)%>
                    </div>
                </div>
            </div>
            <div class="pk_right">
                <%if (help != null)
                  { %>
                <div class="blk15">
                    <%}
                  else
                  { %>
                    <div class="pi_right_box">
                        <%} %>
                        <h2>
                            <strong>
                                <%=help_title %></strong></h2>
                        <div class="main">
                            <%
                                if (help != null)
                                    Response.Write(help.Help_Content);
                                else
                                    cms.Help_FAQ(28);
                            %>
                        </div>
                    </div>
                </div>
                <div class="clear">
                </div>
            </div>
    </div>
        </div>
        </div>
  <ucbottom:bottom runat="server" ID="Bottom" />
</body>
</html>
