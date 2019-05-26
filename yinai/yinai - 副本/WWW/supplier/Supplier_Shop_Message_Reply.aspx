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
    supplier.Supplier_Login_Check("/supplier/Supplier_Shop_Message.aspx");
    if (tools.NullInt(Session["supplier_ishaveshop"]) == 0)
    {
        pub.Msg("error", "错误信息", "您还没有开通店铺", false, "/supplier/index.aspx");
    }

    DateTime Ask_Addtime = DateTime.Now;
    string Ask_Content = "";
    string Ask_Contact = "";
    string Ask_Reply = "";
    int Ask_MemberID = 0;
    int Ask_ProductID = 0;
    string Product_Name = "";
    int Ask_ID = tools.CheckInt(Request["Ask_ID"]);
    if (Ask_ID == 0)
    {
        pub.Msg("error", "错误信息", "记录不存在", false, "/supplier/Supplier_Shop_Message.aspx");
    }
    ShoppingAskInfo entity = supplier.GetShoppingAskByID(Ask_ID);
    if (entity != null)
    {
        Ask_ID = entity.Ask_ID;
        Ask_Content = entity.Ask_Content;
        Ask_Contact = entity.Ask_Contact;
        Ask_Addtime = entity.Ask_Addtime;
        Ask_ProductID = entity.Ask_ProductID;
        Ask_Reply = entity.Ask_Reply;
        Ask_MemberID = entity.Ask_MemberID;
        if (Ask_ProductID > 0)
        {
            ProductInfo productinfo = supplier.GetProductByID(Ask_ProductID);
            if (productinfo != null)
            {
                Product_Name = productinfo.Product_Name;
            }
            else
            {
                Product_Name = "--";
            }
        }
    }
    else
    {
        pub.Msg("error", "错误信息", "记录不存在", false, "/supplier/Supplier_Shop_Message.aspx");
    }
    
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="咨询留言 - 会员中心 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script src="/scripts/common.js" type="text/javascript"></script>
    <script src="/scripts/jquery-extend-AdAdvance3.js" type="text/javascript"></script>
</head>
<body>

    <uctop:top ID="top1" runat="server" />
    
    
    <!--主体 开始-->
<div class="content">
      <!--位置说明 开始-->
      <div class="position">
		 您现在的位置 > <a href="/">首页</a> > <a href="index.aspx">会员中心</a> > <a href="Supplier_Shop_Message.aspx">咨询留言</a>
	</div>
      <div class="clear"></div>
      <!--位置说明 结束-->
      <div class="parth">
            <div class="ph_left">
                     <% supplier.Get_Supplier_Left_HTML(2, 10); %>
            </div>
            <div class="ph_right">
                  <div class="blk13">
                        <h2>咨询留言</h2>
                        <div class="main">
                             
                    <form name="formadd" method="post" action="/supplier/Supplier_Shop_Message_do.aspx">
      <table width="100%" border="0" cellspacing="0" cellpadding="5" id="apply_1_content" class="table_padding_5">
      <tr>
                <td align="right" style="line-height: 24px;" width="100" class="t12_53">
                    咨询时间
                </td>
                <td align="left">
                    <%=Ask_Addtime %>
                </td>
            </tr>
            <tr>
                <td align="right" style="line-height: 24px;" width="100" class="t12_53">
                    咨 询 人
                </td>
                <td align="left">
                    <%
                        if (Ask_MemberID == 0)
                        {
                            Response.Write("游客");
                        }
                        else
                        {
                            Response.Write(supplier.GetMemberNickName(Ask_MemberID));
                        }
                         %>
                </td>
            </tr>
            <tr>
                <td align="right" style="line-height: 24px;" width="100" class="t12_53">
                    联系方式
                </td>
                <td align="left">
                    <%=Ask_Contact %>
                </td>
            </tr>
            <tr>
                <td align="right" style="line-height: 24px;" width="100" class="t12_53">
                    咨询产品
                </td>
                <td align="left">
                    <%=Product_Name%>
                </td>
            </tr>
            <tr>
                <td align="right" style="line-height: 24px;" width="100" class="t12_53">
                    咨询内容
                </td>
                <td align="left">
                    <%=Ask_Content%>
                </td>
            </tr>
            <tr>
                <td align="right" style="line-height: 24px;" width="100" class="t12_53">
                    回复内容
                </td>
                <td align="left">
                <textarea cols="50" rows="5" name="Ask_Reply"><%=Ask_Reply%></textarea>
                    
                </td>
            </tr>
 
            <tr>
              <td></td>
              <td>
              <input name="Ask_ID" type="hidden" id="Ask_ID" value="<%=Ask_ID %>">
              <input name="action" type="hidden" id="action" value="reply">
              <input name="btn_submit" type="image" src="/images/save.jpg" /></td>
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
