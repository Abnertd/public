<%@ Page Language="C#" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%@ Register Src="~/Public/Top.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>

<% 
    
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    Supplier supplier = new Supplier();

    supplier.Supplier_Login_Check("/supplier/Sys_Notice.aspx");

    int notice_id=tools.CheckInt(Request["notice_id"]);
    string notice_title, notice_content;
    notice_title = "";
    notice_content = "";
    if (notice_id > 0)
    {
        SupplierMessageInfo entity = supplier.GetSupplierMessageByID(notice_id);
        if (entity != null)
        {
            if (entity.Supplier_Message_SupplierID == tools.NullInt(Session["supplier_id"]))
            {
                notice_title = entity.Supplier_Message_Title;
                notice_content = entity.Supplier_Message_Content;
                if (entity.Supplier_Message_IsRead == 0)
                {
                    entity.Supplier_Message_IsRead = 1;
                    supplier.Supplier_Message_Edit(entity);
                }
            }
            else
            {
                pub.Msg("error", "错误信息", "记录不存在", false, "/supplier/Sys_Notice.aspx");
            }
        }
        else
        {
            pub.Msg("error", "错误信息", "记录不存在", false, "/supplier/Sys_Notice.aspx");
        }
    }
    else
    {
        pub.Msg("error", "错误信息", "记录不存在", false, "/supplier/Sys_Notice.aspx");
    }
    
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="政策通知 - 会员中心 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    
    <link href="/css/page.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script src="/scripts/jquery-extend-AdAdvance3.js" type="text/javascript"></script>
</head>
<body>

    <uctop:top ID="top1" runat="server" />
    
    <!--主体 开始-->
<div class="content">
      <!--位置说明 开始-->
      <div class="position">
		 您现在的位置 > <a href="/">首页</a> > <a href="index.aspx">会员中心</a> > <a href="sys_notice.aspx">政策通知</a>
	</div>
      <div class="clear"></div>
      <!--位置说明 结束-->
      <div class="parth">
            <div class="ph_left">
                     <% supplier.Get_Supplier_Left_HTML(1, 7); %>
            </div>
            <div class="ph_right">
                  <div class="blk13">
                        <h2><%=notice_title %></h2>
                        <div class="main">
                             
                    <%=notice_content %>
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
