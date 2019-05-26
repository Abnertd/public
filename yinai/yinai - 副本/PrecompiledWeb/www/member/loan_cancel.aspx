<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%--<%@ Register Src="~/Public/MEM_Top.ascx" TagPrefix="uctop" TagName="top" %>--%>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>

<% 
    
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    Member member = new Member();
    member.Member_Login_Check("/member/loan_project.aspx");
    string loan_proj_no = tools.CheckStr(Request["loan_proj_no"]);
    string orders_sn = tools.CheckStr(Request["orders_sn"]);
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="贷款撤销 - 我是买家 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />

    <link href="/css/index.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script src="/scripts/common.js" type="text/javascript"></script>
    <script src="/scripts/jquery-extend-AdAdvance3.js" type="text/javascript"></script>
    <script type="text/javascript" src="/scripts/My97DatePicker/WdatePicker.js"></script>
    <script src="/scripts/supplier.js" type="text/javascript"></script>

</head>
<body>

<%--    <uctop:top ID="top1" runat="server" />--%>




    <!--主体 开始-->
    <div class="content02">
        <div class="content02_main">
            <!--位置说明 开始-->
            <div class="position"><a href="/tradeindex.aspx">首页</a> > <a href="/supplier/">采购商用户中心</a> > 信贷管理 > <strong>贷款撤销</strong></div>
            <!--位置说明 结束-->
            <div class="partc">
                <div class="pc_left">
                    <div class="blk12">
                        <% =member.Member_Left_HTML(3, 7) %>
                    </div>
                </div>
                <div class="pc_right">
                    <div class="title03">贷款撤销</div>
                    <div class="blk17">
                        <form name="formadd" id="formadd" method="post" action="/member/credit_do.aspx">
                            <table width="100%" border="0" cellspacing="0" cellpadding="5" id="apply_1_content" class="table_padding_5">
                                <tr>
                                    <td width="92" class="name">贷款编号
                                    </td>
                                    <td width="801">
                                        <%=loan_proj_no %>
                                        <input type="hidden"  name="loan_proj_no" value="<%=loan_proj_no %>"  />
                                    </td>
                                </tr>
                                <tr>
                                    <td width="92" class="name">撤销原因
                                    </td>
                                    <td width="801">
                                       <textarea cols="50" rows="5" id="cause" name="cause"></textarea>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="92" class="name"></td>
                                    <td width="801">
                                        <input name="channel" type="hidden" id="channel" value="B2B"/>
                                        <input name="orders_sn" type="hidden" id="orders_sn" value="<%=orders_sn %>" />
                                        <input name="action" type="hidden" id="action" value="loan_cancel"/>
                                        <a href="javascript:void(0);" onclick="$('#formadd').submit();" class="a11">保 存</a></td>
                                </tr>
                            </table>
                        </form>
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
