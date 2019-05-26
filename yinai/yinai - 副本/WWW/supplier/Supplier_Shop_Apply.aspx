<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>
<% 
    
    Public_Class pub = new Public_Class();
    ITools tools;
    tools = ToolsFactory.CreateTools();
    Supplier supplier = new Supplier();
    Addr addr = new Addr();

    supplier.Supplier_Login_Check("/supplier/Supplier_Shop_Apply.aspx");

    SupplierInfo supplierinfo = supplier.GetSupplierByID();

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
        SupplierShopInfo shopinfo = supplier.GetSupplierShopBySupplierID(supplierinfo.Supplier_ID);
        if (shopinfo != null)
        {
            Session["supplier_shopid"] = shopinfo.Shop_ID;
            if (shopinfo.Shop_Status == 0)
            {

                Session["supplier_ishaveshop"] = 0;
            }
        }
    }

    //if (tools.NullInt(Session["supplier_ishaveshop"]) == 1 || tools.NullInt(Session["Supplier_Isapply"]) == 1 || tools.NullInt(Session["supplier_shopid"]) > 0)
    //{
    //    pub.Msg("error", "错误信息", "您暂时无法使用该功能", false, "/supplier/index.aspx");
    //}

    string Shop_Apply_Name, Shop_Apply_PINCode, Shop_Apply_Mobile, Shop_Apply_ShopName, Shop_Apply_CompanyType, Shop_Apply_Lawman, Shop_Apply_CertCode, Shop_Apply_CertAddress, Shop_Apply_CompanyAddress, Shop_Apply_CompanyPhone, Shop_Apply_Certification1, Shop_Apply_Certification2, Shop_Apply_Certification3, Shop_Apply_Certification4, Shop_Apply_Certification5, Shop_Apply_MainBrand, Shop_Apply_Note;
    int Shop_Apply_ID, Shop_Apply_SupplierID, Shop_Apply_ShopType, Shop_Apply_Status;
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
    Shop_Apply_ShopType = 0;
    Shop_Apply_Note = "";
    Shop_Apply_Status = 0;

    SupplierShopApplyInfo entity = supplier.GetSupplierShopApplyBySupplierID(supplierinfo.Supplier_ID);
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
        Shop_Apply_Note = entity.Shop_Apply_Note;
        Shop_Apply_Status = entity.Shop_Apply_Status;
    }

    
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="账户信息 - 我是卖家 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />

    <link href="../css/index_newadd.css" rel="stylesheet" />
    <link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js"></script>
    <script type="text/javascript" src="/scripts/common.js"></script>
    <script src="/scripts/supplier.js" type="text/javascript"></script>



    <script type="text/javascript" src="/scripts/layer/layer.js"></script>

    <!--弹出菜单 start-->
    <script type="text/javascript">
        $(document).ready(function () {
            var byt = $(".testbox li");
            var box = $(".boxshow")
            byt.hover(
                 function () {
                     $(this).find(".boxshow").show(); $(this).find(".a3").attr("class", "a3 a3h");
                 },
                function () {
                    $(this).find(".boxshow").hide(); $(this).find(".a3h").attr("class", "a3");
                }
            );
        });
    </script>
    <style type="text/css">
        .input01 {
            background-image: none;
            padding-left: 10px;
            width: 252px;
        }
    </style>
    <!--弹出菜单 end-->
</head>
<body>
    <%    
        int auditstatus = tools.NullInt(Session["supplier_auditstatus"]);
        if (auditstatus == 0 || auditstatus == -1)
        {

        }
    %>
    <uctop:top ID="top1" runat="server" />
    <!--主体 开始-->
    <div class="webwrap">
        <div class="content02" style="background-color: #FFF; margin-bottom: 20px;">
            <!--位置说明 开始-->
            <div class="position">当前位置 > <a href="/supplier/">我是卖家</a> > 店铺管理 > <strong>开通店铺</strong></div>
            <!--位置说明 结束-->

            <div class="partd_1" style="overflow: visible">
                <div class="pd_left">
                    <% supplier.Get_Supplier_Left_HTML(2, 1); %>
                </div>
                <div class="pd_right">
                    <div class="blk14_1" style="margin-top: 0px;">
                      
                            <h2>店铺开通申请</h2>
                            <div class="blk07_sz">
                               <div class="b07_main_sz">
                                   <div class="b07_info04">
                                <form name="formadd" method="post" action="/supplier/account_do.aspx" id="signupForm">

                                    <div class="member_opt_body">

                                        <table width="100%" border="0" cellspacing="0" cellpadding="5" id="apply_1_content">
                                            <tr>
                                                <td width="100" align="right" class="t12_53">公司名称
                                                </td>
                                                <td align="left">
                                                    <%=supplierinfo.Supplier_CompanyName %>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="100" align="right" class="t12_53">店铺类型
                                                </td>
                                                <td align="left">
                                                    <select name="Shop_Apply_ShopType">
                                                        <option value="1" <%if (Shop_Apply_ShopType == 1) { Response.Write("selected"); } %>>旗舰店</option>
                                                        <option value="2" <%if (Shop_Apply_ShopType == 2) { Response.Write("selected"); } %>>专卖店</option>
                                                        <option value="3" <%if (Shop_Apply_ShopType == 3) { Response.Write("selected"); } %>>专营店</option>
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="line-height: 24px;" class="t12_53">申请人真实姓名
                                                </td>
                                                <td align="left">
                                                    <input name="Shop_Apply_Name" type="text" id="Shop_Apply_Name" class="required"
                                                        size="40" maxlength="100" value="<%=Shop_Apply_Name%>" />
                                                    <span class="t14_red">*</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="line-height: 24px;" class="t12_53">申请人身份证号码
                                                </td>
                                                <td align="left">
                                                    <input name="Shop_Apply_PINCode" type="text" id="Shop_Apply_PINCode" class="Identity"
                                                        size="40" maxlength="50" value="<%=Shop_Apply_PINCode%>" />
                                                    <span class="t14_red">*</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" class="t12_53">手机
                                                </td>
                                                <td align="left">
                                                    <input name="Shop_Apply_Mobile" type="text" class="is_mobile" id="Shop_Apply_Mobile" size="40"
                                                        maxlength="50" value="<%=Shop_Apply_Mobile %>" />
                                                    <span class="t14_red">*</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" class="t12_53">店铺名称
                                                </td>
                                                <td align="left">
                                                    <input name="Shop_Apply_ShopName" type="text" id="Shop_Apply_ShopName" class="required"
                                                        size="40" maxlength="50" value="<%=Shop_Apply_ShopName%>" />
                                                    <span class="t14_red">*</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" class="t12_53">公司类型
                                                </td>
                                                <td align="left">
                                                    <input name="Shop_Apply_CompanyType" type="text" class="required" id="Shop_Apply_CompanyType" size="40"
                                                        maxlength="50" value="<%=Shop_Apply_CompanyType %>" />
                                                    <span class="t14_red">*</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" class="t12_53">公司法人代表
                                                </td>
                                                <td align="left">
                                                    <input name="Shop_Apply_Lawman" type="text" class="required" id="Shop_Apply_Lawman" size="40" maxlength="50"
                                                        value="<%=Shop_Apply_Lawman %>" />
                                                    <span class="t14_red">*</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" class="t12_53">营业执照号码
                                                </td>
                                                <td align="left">
                                                    <input name="Shop_Apply_CertCode" type="text" class="required" id="Shop_Apply_CertCode" size="40" maxlength="50"
                                                        value="<%=Shop_Apply_CertCode %>" />
                                                    <span class="t14_red">*</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" class="t12_53">营业执照所在地
                                                </td>
                                                <td align="left">
                                                    <input name="Shop_Apply_CertAddress" type="text" class="required" id="Shop_Apply_CertAddress" size="40" maxlength="50"
                                                        value="<%=Shop_Apply_CertAddress %>" />
                                                    <span class="t14_red">*</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" class="t12_53">公司地址
                                                </td>
                                                <td align="left">
                                                    <input name="Shop_Apply_CompanyAddress" type="text" class="required" id="Shop_Apply_CompanyAddress" size="40" maxlength="50"
                                                        value="<%=Shop_Apply_CompanyAddress %>" />
                                                    <span class="t14_red">*</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" class="t12_53">公司联系电话
                                                </td>
                                                <td align="left">
                                                    <input name="Shop_Apply_CompanyPhone" type="text" class="is_phone" id="Shop_Apply_CompanyPhone" size="40" maxlength="50"
                                                        value="<%=Shop_Apply_CompanyPhone %>" />
                                                    <span class="t14_red">*</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" class="t12_53">主营品牌
                                                </td>
                                                <td align="left">
                                                    <input name="Shop_Apply_MainBrand" type="text" class="required" id="Shop_Apply_MainBrand" size="40" maxlength="50"
                                                        value="<%=Shop_Apply_MainBrand%>" />
                                                    <span class="t14_red">*</span>
                                                </td>
                                            </tr>
                                            <%if (Shop_Apply_Note.Length > 0)
                                              { %>
                                            <tr>
                                                <td align="right" class="t12_53">审核备注
                                                </td>
                                                <td align="left">

                                                    <span class="t12_red"><%=Shop_Apply_Note%></span>
                                                </td>
                                            </tr>
                                            <%} %>
                                        </table>
                                        <table width="100%" border="0" cellspacing="0" cellpadding="5" id="apply_2_content" style="display: none;">
                                            <tr>
                                                <td width="20%">
                                                    <table border="0" width="100%" cellpadding="3" cellspacing="0">
                                                        <tr>
                                                            <td alian="center" height="120">
                                                                <img id="img_supplier_cert1" src="<%=pub.FormatImgURL(Shop_Apply_Certification1,"fullpath") %>" width="120" height="120" onload="javascript:AutosizeImage(this,120,120);">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td alian="center" height="30">
                                                                <input type="button" name="btn_upload" value="上传" class="smb_btn" onclick="javascript: openUpload('supplier_cert1');" />
                                                                <input type="button" name="btn_upload" value="删除" class="smb_btn" onclick="javascript: delImage('supplier_cert1');"><input type="hidden" name="supplier_cert1" id="supplier_cert1" value="<%=Shop_Apply_Certification1 %>" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center">营业执照 </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td width="20%">
                                                    <table border="0" width="100%" cellpadding="3" cellspacing="0">
                                                        <tr>
                                                            <td alian="center" height="120">
                                                                <img id="img_supplier_cert2" src="<%=pub.FormatImgURL(Shop_Apply_Certification2,"fullpath") %>" width="120" height="120" onload="javascript:AutosizeImage(this,120,120);">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td alian="center" height="30">
                                                                <input type="button" name="btn_upload" value="上传" class="smb_btn" onclick="javascript: openUpload('supplier_cert2');" />
                                                                <input type="button" name="btn_upload" value="删除" class="smb_btn" onclick="javascript: delImage('supplier_cert2');"><input type="hidden" name="supplier_cert2" id="supplier_cert2" value="<%=Shop_Apply_Certification2 %>" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center">税务登记证 </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td width="20%">
                                                    <table border="0" width="100%" cellpadding="3" cellspacing="0">
                                                        <tr>
                                                            <td alian="center" height="120">
                                                                <img id="img_supplier_cert3" src="<%=pub.FormatImgURL(Shop_Apply_Certification3,"fullpath") %>" width="120" height="120" onload="javascript:AutosizeImage(this,120,120);">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td alian="center" height="30">
                                                                <input type="button" name="btn_upload" value="上传" class="smb_btn" onclick="javascript: openUpload('supplier_cert3');" />
                                                                <input type="button" name="btn_upload" value="删除" class="smb_btn" onclick="javascript: delImage('supplier_cert3');"><input type="hidden" name="supplier_cert3" id="supplier_cert3" value="<%=Shop_Apply_Certification3 %>" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center">组织机构代码证 </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td width="20%">
                                                    <table border="0" width="100%" cellpadding="3" cellspacing="0" style="display: none;">
                                                        <tr>
                                                            <td alian="center" height="120">
                                                                <img id="img_supplier_cert4" src="<%=pub.FormatImgURL(Shop_Apply_Certification4,"fullpath") %>" width="120" height="120" onload="javascript:AutosizeImage(this,120,120);">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td alian="center" height="30">
                                                                <input type="button" name="btn_upload" value="上传" class="smb_btn" onclick="javascript: openUpload('supplier_cert4');" />
                                                                <input type="button" name="btn_upload" value="删除" class="smb_btn" onclick="javascript: delImage('supplier_cert4');"><input type="hidden" name="supplier_cert4" id="supplier_cert4" value="<%=Shop_Apply_Certification4 %>" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center">商标注册证/受理通知书 </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td width="20%">
                                                    <table border="0" width="100%" cellpadding="3" cellspacing="0" style="display: none;">
                                                        <tr>
                                                            <td alian="center" height="120">
                                                                <img id="img_supplier_cert5" src="<%=pub.FormatImgURL(Shop_Apply_Certification5,"fullpath") %>" width="120" height="120" onload="javascript:AutosizeImage(this,120,120);">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td alian="center" height="30">
                                                                <input type="button" name="btn_upload" value="上传" class="smb_btn" onclick="javascript: openUpload('supplier_cert5');" />
                                                                <input type="button" name="btn_upload" value="删除" class="smb_btn" onclick="javascript: delImage('supplier_cert5');"><input type="hidden" name="supplier_cert5" id="supplier_cert5" value="<%=Shop_Apply_Certification5 %>" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center">授权书 </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr id="td_upload" style="display: none">
                                                <td colspan="5" style="padding-left: 20px;" align="left">图片上传：<iframe id="iframe_upload" src="" width="300" height="22" frameborder="0" scrolling="no" align="absmiddle"></iframe>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <table width="100%" border="0" cellspacing="0" cellpadding="5">
                                        <tr>
                                            <td align="right" class="t12_53"></td>
                                            <td>
                                                <input name="action" type="hidden" id="action" value="shop_apply"><input name="btn_submit"
                                                    type="image" src="/images/save_buttom.jpg" />
                                            </td>
                                        </tr>
                                    </table>
                                </form>
                            </div>
                                </div></div>
                      
                        <div class="clear"></div>
                    </div>
                </div>
                <div class="clear">
                </div>
            </div>

            <%--  </div>--%>
        </div>
    </div>
    <!--主体 结束-->
  <ucbottom:bottom runat="server" ID="Bottom" />
</body>
</html>
