<%@ Page Language="C#" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%@ Register Src="~/Public/Top.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>

<% 
    
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    Supplier myApp = new Supplier();
    Addr addr = new Addr();

    myApp.Supplier_Login_Check("/supplier/Supplier_Shop_Cert.aspx");

    SupplierInfo supplierinfo = myApp.GetSupplierByID();

    if (supplierinfo == null)
    {
        Response.Redirect("/supplier/index.aspx");
    }
    else
    {
        if (supplierinfo.Supplier_Cert_Status != 2)
        {
            pub.Msg("error", "错误信息", "您尚未通过商家资质审核", false, "/supplier/index.aspx");
        }
        Session["supplier_ishaveshop"] = supplierinfo.Supplier_IsHaveShop;
        Session["Supplier_Isapply"] = supplierinfo.Supplier_IsApply;
    }

    if (tools.NullInt(Session["supplier_ishaveshop"]) == 0)
    {
        pub.Msg("error", "错误信息", "您还没有开通店铺", false, "/supplier/index.aspx");
    }
    
    
    string Shop_Apply_Name,Shop_Apply_PINCode,Shop_Apply_Mobile,Shop_Apply_ShopName,Shop_Apply_CompanyType,Shop_Apply_Lawman,Shop_Apply_CertCode,Shop_Apply_CertAddress,Shop_Apply_CompanyAddress,Shop_Apply_CompanyPhone,Shop_Apply_Certification1,Shop_Apply_Certification2,Shop_Apply_Certification3,Shop_Apply_Certification4,Shop_Apply_Certification5,Shop_Apply_MainBrand;
    int Shop_Apply_ID,Shop_Apply_SupplierID,Shop_Apply_ShopType;
    Shop_Apply_Certification1 = "";
    Shop_Apply_Certification2 = "";
    Shop_Apply_Certification3 = "";
    Shop_Apply_Certification4 = "";
    Shop_Apply_Certification5 = "";
    Shop_Apply_Name = "";
    Shop_Apply_PINCode = "";
    Shop_Apply_Mobile = "";
    Shop_Apply_ShopName = "";
    Shop_Apply_CompanyType = "";
    Shop_Apply_Lawman = "";
    Shop_Apply_CertCode = "";
    Shop_Apply_CertAddress = "";
    Shop_Apply_CompanyAddress = "";
    Shop_Apply_CompanyPhone = "";
    Shop_Apply_MainBrand = "";
    Shop_Apply_ShopType=0;
    
    SupplierShopApplyInfo entity = myApp.GetSupplierShopApplyBySupplierID(supplierinfo.Supplier_ID);
    if (entity != null) 
    {
        Shop_Apply_ID = entity.Shop_Apply_ID;
        Shop_Apply_SupplierID = entity.Shop_Apply_SupplierID;
        Shop_Apply_ShopType = entity.Shop_Apply_ShopType;
        Shop_Apply_Name = entity.Shop_Apply_Name;
        Shop_Apply_PINCode = entity.Shop_Apply_PINCode;
        Shop_Apply_Mobile = entity.Shop_Apply_Mobile;
        Shop_Apply_ShopName = entity.Shop_Apply_ShopName;
        Shop_Apply_CompanyType = entity.Shop_Apply_CompanyType;
        Shop_Apply_Lawman = entity.Shop_Apply_Lawman;
        Shop_Apply_CertCode = entity.Shop_Apply_CertCode;
        Shop_Apply_CertAddress = entity.Shop_Apply_CertAddress;
        Shop_Apply_CompanyAddress = entity.Shop_Apply_CompanyAddress;
        Shop_Apply_CompanyPhone = entity.Shop_Apply_CompanyPhone;
        Shop_Apply_Certification1 = entity.Shop_Apply_Certification1;
        Shop_Apply_Certification2 = entity.Shop_Apply_Certification2;
        Shop_Apply_Certification3 = entity.Shop_Apply_Certification3;
        Shop_Apply_Certification4 = entity.Shop_Apply_Certification4;
        Shop_Apply_Certification5 = entity.Shop_Apply_Certification5;
        Shop_Apply_MainBrand = entity.Shop_Apply_MainBrand;
    }
    
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="店铺设置 - 会员中心 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    
    <link href="/css/page.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script src="/scripts/common.js" type="text/javascript"></script>
    <script src="/scripts/jquery-extend-AdAdvance3.js" type="text/javascript"></script>
    
    <link rel="stylesheet" href="/public/colorpicker/css/colorpicker.css" type="text/css" />
    <script type="text/javascript" src="/public/colorpicker/js/colorpicker.js"></script>
    <link rel="stylesheet" type="text/css" href="/scripts/treeview/dhtmlxtree.css" />
    <script  src="/scripts/treeview/dhtmlxcommon.js" type="text/javascript"></script>
    <script  src="/scripts/treeview/dhtmlxtree.js" type="text/javascript"></script>

    <script type="text/javascript">
        function openUpload(openObj) {
            $("#td_upload").show();
            $("#iframe_upload").attr("src","<% =Application["Upload_Server_URL"]%>/public/uploadify.aspx?App=shopcert&formname=formadd&frmelement=" + openObj + "&rtvalue=1&rturl=<% =Application["Upload_Server_Return_WWW"]%>");
        }

         function delImage(openObj) {
            $("#img_"+ openObj)[0].src = "/images/detail_no_pic.gif";
            $("#"+ openObj)[0].value = "/images/detail_no_pic.gif";
        }
    </script>
    <style type="text/css">
        .zkw_title21 { margin-bottom:10px; border-bottom:solid 2px #3891cd; }
        .zkw_title21 li { cursor:pointer; }
        .table_padding_6 td { text-align:center;}
        .table_padding_6 td img{ display:inline;}
    </style>
</head>
<body>

    <uctop:top ID="top1" runat="server" />
    
    <!--主体 开始-->
<div class="content">
      <!--位置说明 开始-->
      <div class="position">
		  您现在的位置 > <a href="/">首页</a> > <a href="index.aspx">会员中心</a> > <a href="Supplier_Shop_Cert.aspx">店铺设置</a>
	</div>
      <div class="clear"></div>
      <!--位置说明 结束-->
      <div class="parth">
            <div class="ph_left">
                    <% myApp.Get_Supplier_Left_HTML(2, 12); %>
            </div>
            <div class="ph_right">
                  <div class="blk13">
                        <h2>店铺资质</h2>
                        <div class="main">
                             
                        <div class="zkw_order">
                        

                            <form name="formadd" id="formadd" method="post" action="/supplier/account_do.aspx">
                            <table width="100%" border="0" cellspacing="0" cellpadding="5" id="apply_2_content" class="table_padding_6">
                                <tr>
                                    <td width="20%">
                                        <table border="0" width="100%" cellpadding="3" cellspacing="0">
                                            <tr><td alian="center" height="120">
                                            <img id="img_supplier_cert1" src="<%=pub.FormatImgURL(Shop_Apply_Certification1,"fullpath") %>" width="120" height="120" onload="javascript:AutosizeImage(this,120,120);">
                                            </td></tr>
                                            <tr><td alian="center" height="30">
                                            <input type="button" name="btn_upload" value="上传" class="smb_btn" onclick="javascript:openUpload('supplier_cert1');" /> <input type="button" name="btn_upload" value="删除" class="smb_btn" onclick="javascript:delImage('supplier_cert1');"><input type="hidden" name="supplier_cert1" id="supplier_cert1" value="<%=Shop_Apply_Certification1 %>" />
                                            </td></tr>
                                            <tr>
                                                <td align="center"> 营业执照 </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="20%">
                                        <table border="0" width="100%" cellpadding="3" cellspacing="0">
                                            <tr><td alian="center" height="120">
                                            <img id="img_supplier_cert2" src="<%=pub.FormatImgURL(Shop_Apply_Certification2,"fullpath") %>" width="120" height="120" onload="javascript:AutosizeImage(this,120,120);">
                                            </td></tr>
                                            <tr><td alian="center" height="30">
                                            <input type="button" name="btn_upload" value="上传" class="smb_btn" onclick="javascript:openUpload('supplier_cert2');" /> <input type="button" name="btn_upload" value="删除" class="smb_btn" onclick="javascript:delImage('supplier_cert2');"><input type="hidden" name="supplier_cert2" id="supplier_cert2" value="<%=Shop_Apply_Certification2 %>" />
                                            </td></tr>
                                            <tr>
                                                <td align="center"> 税务登记证 </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="20%">
                                        <table border="0" width="100%" cellpadding="3" cellspacing="0">
                                            <tr><td alian="center" height="120">
                                            <img id="img_supplier_cert3" src="<%=pub.FormatImgURL(Shop_Apply_Certification3,"fullpath") %>" width="120" height="120" onload="javascript:AutosizeImage(this,120,120);">
                                            </td></tr>
                                            <tr><td alian="center" height="30">
                                            <input type="button" name="btn_upload" value="上传" class="smb_btn" onclick="javascript:openUpload('supplier_cert3');" /> <input type="button" name="btn_upload" value="删除" class="smb_btn" onclick="javascript:delImage('supplier_cert3');"><input type="hidden" name="supplier_cert3" id="supplier_cert3" value="<%=Shop_Apply_Certification3 %>" />
                                            </td></tr>
                                            <tr>
                                                <td align="center"> 组织机构代码证 </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="20%">
                                        <table border="0" width="100%" cellpadding="3" cellspacing="0" style="display:none;">
                                            <tr><td alian="center" height="120">
                                            <img id="img_supplier_cert4" src="<%=pub.FormatImgURL(Shop_Apply_Certification4,"fullpath") %>" width="120" height="120" onload="javascript:AutosizeImage(this,120,120);">
                                            </td></tr>
                                            <tr><td alian="center" height="30">
                                            <input type="button" name="btn_upload" value="上传" class="smb_btn" onclick="javascript:openUpload('supplier_cert4');" /> <input type="button" name="btn_upload" value="删除" class="smb_btn" onclick="javascript:delImage('supplier_cert4');"><input type="hidden" name="supplier_cert4" id="supplier_cert4" value="<%=Shop_Apply_Certification4 %>" />
                                            </td></tr>
                                            <tr>
                                                <td align="center"> 商标注册证/受理通知书 </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="20%">
                                        <table border="0" width="100%" cellpadding="3" cellspacing="0" style="display:none;">
                                            <tr><td alian="center" height="120">
                                            <img id="img_supplier_cert5" src="<%=pub.FormatImgURL(Shop_Apply_Certification5,"fullpath") %>" width="120" height="120" onload="javascript:AutosizeImage(this,120,120);">
                                            </td></tr>
                                            <tr><td alian="center" height="30">
                                            <input type="button" name="btn_upload" value="上传" class="smb_btn" onclick="javascript:openUpload('supplier_cert5');" /> <input type="button" name="btn_upload" value="删除" class="smb_btn" onclick="javascript:delImage('supplier_cert5');"><input type="hidden" name="supplier_cert5" id="supplier_cert5" value="<%=Shop_Apply_Certification5 %>" />
                                            </td></tr>
                                            <tr>
                                                <td align="center"> 授权书 </td>
                                            </tr>
                                        </table>
                                    </td>
                                    </tr>
                                    <tr id="td_upload" style="display:none;">
                                    <td colspan="5" style="padding-left:20px; text-align:left;" align="left">图片上传：<iframe id="iframe_upload" src="" width="300" height="90" frameborder="0" scrolling="no" align="absmiddle"></iframe></td>
                                    </tr>
                            </table>
                            
                            <table width="100%" border="0" cellspacing="0" cellpadding="5">
                                <tr>
                                    <td align="center">
                                        <input name="action" type="hidden" id="action" value="shop_cert"><input name="btn_submit"
                                            type="image" src="/images/save.jpg" />
                                    </td>
                                </tr>
                                </table>
                            </form>
                            
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
