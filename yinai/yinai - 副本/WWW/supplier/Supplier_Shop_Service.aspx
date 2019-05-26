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
    supplier.Supplier_Login_Check("/supplier/Supplier_Shop_Service.aspx");
    if (tools.NullInt(Session["supplier_ishaveshop"]) == 0)
    {
        pub.Msg("error", "错误信息", "您还没有开通店铺", false, "/supplier/index.aspx");
    }

    string Shop_Pages_Intro = "";
    int Shop_Pages_Sort = 0;
    SupplierShopPagesInfo entity = supplier.GetSupplierShopPagesByIDSign("SERVICE", tools.NullInt(Session["supplier_id"]));
    if (entity != null)
    {
        Shop_Pages_Intro = entity.Shop_Pages_Content;
        Shop_Pages_Sort = entity.Shop_Pages_Sort;
    }
    
%>
    
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="服务条例 - 会员中心 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script src="/scripts/jquery-extend-AdAdvance3.js" type="text/javascript"></script>
    <script type="text/javascript" src="/Public/ckeditor/ckeditor.js"></script>
</head>
<body>
    <uctop:top ID="top1" runat="server" />
    

    
    <!--主体 开始-->
<div class="content">
      <!--位置说明 开始-->
      <div class="position">
		  您现在的位置 > <a href="/">首页</a> > <a href="index.aspx">会员中心</a> > <a href="Supplier_Shop_Service.aspx">服务条例</a>
	</div>
      <div class="clear"></div>
      <!--位置说明 结束-->
      <div class="parth">
            <div class="ph_left">
                   <% supplier.Get_Supplier_Left_HTML(2, 6); %>
            </div>
            <div class="ph_right">
                  <div class="blk13">
                        <h2>服务条例</h2>
                        <div class="main">
                                         
      <form name="formadd" method="post" action="/supplier/Supplier_Shop_Pages_do.aspx">
      <table width="100%" border="0" cellspacing="0" cellpadding="5" id="apply_1_content" class="table_padding_5">
            <tr>
            <td align="right" style="line-height:24px;" width="50" class="t12_53">图片</td>
          <td align="left"><iframe id="iframe2" src="<% =Application["Upload_Server_URL"]%>/public/uploadify.aspx?App=shoppages&formname=formadd&frmelement=Shop_Pages_Content&rtvalue=1&rturl=<% =Application["Upload_Server_Return_WWW"]%>" width="100%" height="90" frameborder="0" scrolling="no"></iframe></td>
        </tr>
        <tr>
        <td align="right" style="line-height:24px;" width="60" class="t12_53">内容</td>
          <td align="left">
            <textarea cols="80" id="Shop_Pages_Content" name="Shop_Pages_Content" rows="16"><%=Shop_Pages_Intro%></textarea>
            <script type="text/javascript">
                CKEDITOR.replace('Shop_Pages_Content');
            </script>
          </td>
        </tr>
        <tr>
                <td align="right" style="line-height: 24px;" class="t12_53">
                    排序
                </td>
                <td align="left">
                   <input name="Shop_Pages_Sort" type="text" id="Shop_Pages_Sort" class="txt_border"
                        size="10" maxlength="10" value="<%=Shop_Pages_Sort %>" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')"  />
                    
                </td>
            </tr>
        <tr>
              <td align="right" class="t12_53">
                  </td>
              <td><input name="Shop_Pages_Sign" type="hidden" id="Shop_Pages_Sign" value="SERVICE"><input name="action" type="hidden" id="action" value="new"><input name="btn_submit" type="image" src="/images/save.jpg" /></td>
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
