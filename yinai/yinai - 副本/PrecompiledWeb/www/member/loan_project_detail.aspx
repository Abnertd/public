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
    int all_term=0;
    string start_date="", end_date="";
    string loan_proj_no = tools.CheckStr(Request["loan_proj_no"]);
    member.Member_Login_Check("/member/loan_project_detail.aspx?loan_proj_no=" + loan_proj_no);

    QueryProjectDetailJsonInfo JsonInfo = member.GetLoanProjectDetailByProjectNo(loan_proj_no);
    if (JsonInfo != null && JsonInfo.Is_success == "T")
    {
        start_date = DateTime.ParseExact(JsonInfo.Begin_date, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture).ToString("yyyy-MM-dd");
        end_date = DateTime.ParseExact(JsonInfo.End_date, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture).ToString("yyyy-MM-dd");
        all_term = JsonInfo.All_term;
    }
    else
    {
        Response.Redirect("/member/loan_project.aspx");
    }
    
    
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="信贷分期详情 - 我是买家 - " + pub.SEO_TITLE()%></title>
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
            <div class="position"><a href="/tradeindex.aspx">首页</a> > <a href="/supplier/">采购商用户中心</a> > 信贷管理 > <strong>信贷分期详情</strong></div>
            <!--位置说明 结束-->
            <div class="partc">
                <div class="pc_left">
                    <div class="blk12">
                        <% =member.Member_Left_HTML(2, 1) %>
                    </div>
                </div>
                <div class="pc_right">
                    <div class="title03">信贷分期详情</div>
                    <div class="blk17">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" id="apply_1_content">
                                <tr>
                                    <td width="112" class="name">贷款编号：
                                    </td>
                                    <td width="781">
                                        <%=loan_proj_no %>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="112" class="name">贷款开始时间：
                                    </td>
                                    <td width="781">
                                       <%=start_date %>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="112" class="name">贷款结束时间：
                                    </td>
                                    <td width="781">
                                        <%=end_date %>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="112" class="name">总期数：
                                    </td>
                                    <td width="781">
                                        <%=all_term %>
                                    </td>
                                </tr>
                            </table>
                    </div>

                     <div class="blk08" style=" margin-top:15px;">
                         <%member.Member_LoanProjectDetail_List(JsonInfo); %>
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
