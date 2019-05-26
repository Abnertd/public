<%@ Page Language="C#" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%@ Register Src="~/Public/Top.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>

<% 
    
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    Session["Cur_Position"] = Session["Home_Position"] = "";
    Supplier supplier = new Supplier();

    supplier.Supplier_Login_Check("/supplier/Supplier_Adv_Account_list.aspx");
    
    string action = tools.NullStr(Request.QueryString["action"]);
    string date_start, date_end;
    if (action == "history")
    {
        Session["account_coin_list_guide"] = "history";
        date_start = tools.CheckStr(tools.NullStr(Request["date_start"]));
        date_end = tools.CheckStr(tools.NullStr(Request["date_end"]));
    }
    else
    {
        Session["account_coin_list_guide"] = "today";
        date_start = tools.FormatDate(DateTime.Now, "yyyy-MM-dd");
        date_end = tools.FormatDate(DateTime.Now, "yyyy-MM-dd");
    }

    double Supplier_Account;
    Supplier_Account = 0;
    SupplierInfo supplierinfo = supplier.GetSupplierByID();
    if (supplierinfo != null)
    {
        Supplier_Account = supplierinfo.Supplier_Adv_Account;
    }
    
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="推广费明细 - 会员中心 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
 
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script src="/scripts/common.js" type="text/javascript"></script>
    <script src="/scripts/jquery-extend-AdAdvance3.js" type="text/javascript"></script>
    
    <link type="text/css" href="/scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
    <script src="/scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
    <script src="/scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>
    
     <style type="text/css">
        .zkw_19_fox img
        {
            vertical-align: middle;
            display: inline;
        }
    </style>
    
</head>
<body>

    <uctop:top ID="top1" runat="server" />
    

    
    <!--主体 开始-->
<div class="content">
      <!--位置说明 开始-->
      <div class="position">
		 您现在的位置 > <a href="/">首页</a> > <a href="index.aspx">会员中心</a> > <a href="supplier_adv_account_list.aspx">推广费明细</a>
	</div>
      <div class="clear"></div>
      <!--位置说明 结束-->
      <div class="parth">
            <div class="ph_left">
                            <% supplier.Get_Supplier_Left_HTML(1, 5); %>
            </div>
            <div class="ph_right">
                  <div class="blk13">
                        <h2>推广费明细</h2>
                        <div class="main">
                                 <div class="zkw_order">
                        
                            <p style=" margin-bottom:10px;">推广费余额：<b style="color:#c00; font-family:Verdana;"><%=pub.FormatCurrency(Supplier_Account)%></b></p>
                            
                            <h3 class="zkw_title21">
                                <%
                                    StringBuilder strHTML = new StringBuilder();
                                    strHTML.Append("<ul class=\"zkw_lst31\">");
                                    strHTML.Append("	<li " + (Session["account_coin_list_guide"] == "today" ? "class=\"on\"" : "") + " id=\"n01\"><a href=\"Supplier_Adv_Account_list.aspx\">今日明细</a></li>");
                                    strHTML.Append("	<li " + (Session["account_coin_list_guide"] == "history" ? "class=\"on\"" : "") + " id=\"n02\"><a href=\"Supplier_Adv_Account_list.aspx?action=history\">历史明细</a></li>");
                                    strHTML.Append("</ul><div class=\"clear\"></div>");
                                    Response.Write(strHTML.ToString());
                                %>
                            </h3>
                                <div class="zkw_19_fox" id="nn01">
                            <%
                                if (Session["account_coin_list_guide"] == "history")
                                {%>
                            <form name="datescope" method="post" action="/supplier/Supplier_Adv_Account_list.aspx?action=history">
                              <div class="zkw_date">
                                    起始日期：<input type="text" class="input_calendar" name="date_start" id="date_start"
                                        maxlength="10" readonly="readonly" value="<%=date_start %>" />
                                    终止日期：<input type="text" class="input_calendar" name="date_end" id="date_end" maxlength="10"
                                        readonly="readonly" value="<%=date_end %>" />
                                    <script type="text/javascript">
                                        $(function () {
                                            $("#date_start").datepicker({ numberOfMonths: 1 });
                                            $("#date_end").datepicker({ numberOfMonths: 1 });
                                        })
                                    </script>
                                    <input name="search" type="submit" class="input10" id="search" value=" " />
                                </div>
                            </form>
                            <% }%>
                        
                              <%supplier.Get_Account_Log_List(2, action, date_start, date_end); %>
                            </div>
                        </div>
                        </div>
                  </div>
            </div>
            <div class="clear"></div>
      </div> 
</div>
<!--主体 结束-->
 
    
  <ucbottom:bottom runat="server" ID="Bottom" />
    
</body>
</html>
