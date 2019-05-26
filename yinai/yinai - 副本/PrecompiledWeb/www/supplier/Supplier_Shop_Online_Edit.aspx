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
    supplier.Supplier_Login_Check("/supplier/Supplier_Shop_Online.aspx");
    if (tools.NullInt(Session["supplier_ishaveshop"]) == 0)
    {
        pub.Msg("error", "错误信息", "您还没有开通店铺", false, "/supplier/index.aspx");
    }
    
    string Supplier_Online_Type = "";
    string Supplier_Online_Name = "";
    string Supplier_Online_QQ = "";
    string Supplier_Online_MSN = "";
    int Supplier_Online_Sort = 0;
    int Supplier_Online_IsActive = 0;
    int Online_ID = tools.CheckInt(Request["Online_ID"]);
    if (Online_ID == 0)
    {
        pub.Msg("error", "错误信息", "记录不存在", false, "/supplier/Supplier_Shop_Online.aspx");
    }
    SupplierOnlineInfo entity = supplier.GetSupplierOnlineByID(Online_ID, tools.NullInt(Session["supplier_id"]));
    if (entity != null)
    {
        Supplier_Online_Type = entity.Supplier_Online_Type;
        Supplier_Online_Name = entity.Supplier_Online_Name;
        Supplier_Online_Sort = entity.Supplier_Online_Sort;
        Supplier_Online_IsActive = entity.Supplier_Online_IsActive;
        if (Supplier_Online_Type == "QQ")
        {
            Supplier_Online_QQ = entity.Supplier_Online_Code;
        }
        else
        {
            Supplier_Online_MSN = entity.Supplier_Online_Code;
        }
    }
    else
    {
        pub.Msg("error", "错误信息", "记录不存在", false, "/supplier/Product_Category_List.aspx");
    }
    
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="在线客服管理 - 会员中心 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script src="/scripts/jquery-extend-AdAdvance3.js" type="text/javascript"></script>
    <style type="text/css">
        .yz_blk19_main img { display:inline; }
    </style>
</head>
<body>

    <uctop:top ID="top1" runat="server" />
    
    
    <!--主体 开始-->
<div class="content">
      <!--位置说明 开始-->
      <div class="position">
		 您现在的位置 > <a href="/">首页</a> > <a href="index.aspx">会员中心</a> > <a href="Supplier_Shop_Online.aspx">在线客服管理</a>
	</div>
      <div class="clear"></div>
      <!--位置说明 结束-->
      <div class="parth">
            <div class="ph_left">
                    <% supplier.Get_Supplier_Left_HTML(2, 11); %>
            </div>
            <div class="ph_right">
                  <div class="blk13">
                        <h2>在线客服管理</h2>
                        <div class="main">
                             
            <form name="formadd" method="post" action="/supplier/Supplier_Shop_Online_do.aspx">
            <table width="100%" border="0" cellspacing="0" cellpadding="5" id="apply_1_content" class="table_padding_5">
                <tr>
                <td align="right" style="line-height: 24px;" class="t12_53">
                    客服名称
                </td>
                <td align="left">
                    <input name="Online_Name" type="text" id="Online_Name" class="txt_border"
                        size="40" maxlength="100" value="<%=Supplier_Online_Name %>" />
                    <span class="t14_red">*</span>
                </td>
            </tr>
            <tr>
                <td align="right" style="line-height: 24px;" class="t12_53">
                    客服代码类型
                </td>
                <td align="left">
                    <input type="radio" name="Online_Type" value="QQ" <%=pub.CheckRadio("QQ",Supplier_Online_Type) %>/> QQ客服代码
                    <input type="radio" name="Online_Type" value="MSN" <%=pub.CheckRadio("MSN",Supplier_Online_Type) %> /> MSN客服账号
                    <span class="t14_red">*</span> 两者任选其一
                </td>
            </tr>
            <tr>
                <td align="right" style="line-height: 24px;" class="t12_53">
                    QQ客服代码
                </td>
                <td align="left">
                    <textarea name="Online_QQ" cols="40" rows="5" /><%=Supplier_Online_QQ%></textarea>
                    <span class="t14_red">*</span>
                </td>
            </tr>
            <tr>
                <td align="right" style="line-height: 24px;" class="t12_53">
                    MSN客服账号
                </td>
                <td align="left">
                    <input name="Online_MSN" type="text" id="Online_MSN" class="txt_border"
                        size="40" maxlength="100" value="<%=Supplier_Online_MSN%>" />
                    <span class="t14_red">*</span>
                </td>
            </tr>
            <tr>
                <td align="right" style="line-height: 24px;" class="t12_53">
                    状态
                </td>
                <td align="left">
                    <input type="radio" name="Online_IsActive" value="0" <%=pub.CheckRadio("0",Supplier_Online_IsActive.ToString()) %>/> 不启用
                    <input type="radio" name="Online_IsActive" value="1" <%=pub.CheckRadio("1",Supplier_Online_IsActive.ToString()) %> /> 启用
                    <span class="t14_red">*</span> 
                </td>
            </tr>
            <tr>
                <td align="right" style="line-height: 24px;" class="t12_53">
                    分组排序
                </td>
                <td align="left">
                    <input name="Online_Sort" type="text" id="Online_Sort" class="txt_border"
                        size="10" maxlength="50" value="<%=Supplier_Online_Sort %>" />
                    
                </td>
            </tr>
        <tr>
              <td align="right" class="t12_53">
                  </td>
              <td>
              <input name="Supplier_Online_ID" type="hidden" id="Supplier_Online_ID" value="<%=Online_ID %>">
              <input name="action" type="hidden" id="action" value="edit"><input name="btn_submit" type="image" src="/images/save.jpg" /></td>
            </tr>
          </table>

          
            </form>
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
