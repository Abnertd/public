<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>
<% 
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    Supplier myApp = new Supplier();
    string Product_CateID, Product_Code, Product_Name, Product_GroupID;
    int Product_TypeID, Product_Cate;

%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="发布产品 - 我是卖家 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />

     <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script type="text/javascript" src="/scripts/common.js"></script>
    <script src="/scripts/common.js" type="text/javascript"></script>
    <script type="text/javascript" src="/scripts/supplier.js"></script>

    <script type="text/javascript" src="/Public/kindeditor/kindeditor-all.js"></script>
    <script type="text/javascript" src="/Public/kindeditor/lang/zh_CN.js"></script>

    <link rel="stylesheet" type="text/css" href="/scripts/treeview/dhtmlxtree.css" />
    <script src="/scripts/treeview/dhtmlxcommon.js" type="text/javascript"></script>
    <script src="/scripts/treeview/dhtmlxtree.js" type="text/javascript"></script>
    <script type="text/javascript">
        function openUpload(openObj) {
            $("#td_upload").show();

            $("#iframe_upload").attr("src", "<% =Application["Upload_Server_URL"]%>/public/uploadify.aspx?App=product&formname=formadd&frmelement=" + openObj + "&rtvalue=1&rturl=<% =Application["Upload_Server_Return_WWW"]%>");
        }

        function delImage(openObj) {
            $("#img_" + openObj).attr("src", "/images/detail_no_pic.gif");
            $("#" + openObj).val("/images/detail_no_pic.gif");
        }
    </script>

    <script type="text/javascript">

        function SelectProduct() {
            window.open("selectproduct2.aspx", "选择商品", "height=560, width=800, toolbar=no, menubar=no, scrollbars=yes, resizable=no, location=no, status=no,top=100,left=300")
        }

        function product_del(product_id) {
            $.ajax({
                url: encodeURI("product_do.aspx?action=product_del&product_id=" + product_id + "&timer=" + Math.random()),
                type: "get",
                global: false,
                async: false,
                dataType: "html",
                success: function (data) {
                    $("#yhnr").html(data);
                },
                error: function () {
                    alert("Error Script");
                }
            });
        }
    </script>

</head>
<body>

    <uctop:top ID="top1" runat="server" />

    <!--主体 开始-->
      <div class="webwrap" >
    <div class="content02" style="margin-bottom:20px;">
      
            <!--位置说明 开始-->
            <div class="position">当前页面 > <a href="/supplier/">我是卖家</a> > 商品管理 > <strong>添加商品</strong></div>
            <!--位置说明 结束-->
            <div class="partd_1">
                <div class="pd_left">                   
                        <%myApp.Get_Supplier_Left_HTML(3, 1); %>                   
                </div>
                <div class="pd_right">
                    <div class="blk14_1" style="margin-top:0px;">
                  <%--  <div class="title03"></div>--%>
                        <h2>添加商品</h2>
                    <div class="blk07">
                        <h2 style="background-color:#fff">
                            <ul class="list02">
                                <li id="apply_1" class="on" onclick="Set_Tab('apply',1,1,'on','');">图片信息</li>
                            </ul>
                            <div class="clear"></div>
                        </h2>

                        <form name="formadd" id="formadd" method="post" action="/supplier/product_do.aspx">

                            <div class="b07_main" id="apply_1_content" >
                                <table width="893" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td width="20%">
                                            <table border="0" width="100%" cellpadding="3" cellspacing="0">
                                                <tr>
                                                    <td align="center" height="120">
                                                        <img id="img_product_img" src="<%=pub.FormatImgURL("","fullpath") %>" width="120" height="120" onload="javascript:AutosizeImage(this,120,120);">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" height="30">
                                                        <input type="button" name="btn_upload" value="上传" class="smb_btn" onclick="javascript: openUpload('product_img');" />
                                                        <input type="button" name="btn_upload" value="删除" class="smb_btn" onclick="javascript: delImage('product_img');"><input type="hidden" name="product_img" id="product_img" value="/images/detail_no_pic.gif" />
                                                    </td>
                                                </tr>

                                            </table>
                                        </td>
                                        <td width="20%">
                                            <table border="0" width="100%" cellpadding="3" cellspacing="0">
                                                <tr>
                                                    <td align="center" height="120">
                                                        <img id="img_product_img_ext_1" src="<%=pub.FormatImgURL("","fullpath") %>" width="120" height="120" onload="javascript:AutosizeImage(this,120,120);">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" height="30">
                                                        <input type="button" name="btn_upload" value="上传" class="smb_btn" onclick="javascript: openUpload('product_img_ext_1');" />
                                                        <input type="button" name="btn_upload" value="删除" class="smb_btn" onclick="javascript: delImage('product_img_ext_1');"><input type="hidden" name="product_img_ext_1" id="product_img_ext_1" value="/images/detail_no_pic.gif" />
                                                    </td>
                                                </tr>

                                            </table>
                                        </td>
                                        <td width="20%">
                                            <table border="0" width="100%" cellpadding="3" cellspacing="0">
                                                <tr>
                                                    <td align="center" height="120">
                                                        <img id="img_product_img_ext_2" src="<%=pub.FormatImgURL("","fullpath") %>" width="120" height="120" onload="javascript:AutosizeImage(this,120,120);">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" height="30">
                                                        <input type="button" name="btn_upload" value="上传" class="smb_btn" onclick="javascript: openUpload('product_img_ext_2');" />
                                                        <input type="button" name="btn_upload" value="删除" class="smb_btn" onclick="javascript: delImage('product_img_ext_2');"><input type="hidden" name="product_img_ext_2" id="product_img_ext_2" value="/images/detail_no_pic.gif" />
                                                    </td>
                                                </tr>

                                            </table>
                                        </td>
                                        <td width="20%">
                                            <table border="0" width="100%" cellpadding="3" cellspacing="0">
                                                <tr>
                                                    <td align="center" height="120">
                                                        <img id="img_product_img_ext_3" src="<%=pub.FormatImgURL("","fullpath") %>" width="120" height="120" onload="javascript:AutosizeImage(this,120,120);">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" height="30">
                                                        <input type="button" name="btn_upload" value="上传" class="smb_btn" onclick="javascript: openUpload('product_img_ext_3');" />
                                                        <input type="button" name="btn_upload" value="删除" class="smb_btn" onclick="javascript: delImage('product_img_ext_3');"><input type="hidden" name="product_img_ext_3" id="product_img_ext_3" value="/images/detail_no_pic.gif" />
                                                    </td>
                                                </tr>

                                            </table>
                                        </td>
                                        <td width="20%">
                                            <table border="0" width="100%" cellpadding="3" cellspacing="0">
                                                <tr>
                                                    <td align="center" height="120">
                                                        <img id="img_product_img_ext_4" src="<%=pub.FormatImgURL("","fullpath") %>" width="120" height="120" onload="javascript:AutosizeImage(this,120,120);">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" height="30">
                                                        <input type="button" name="btn_upload" value="上传" class="smb_btn" onclick="javascript: openUpload('product_img_ext_4');" />
                                                        <input type="button" name="btn_upload" value="删除" class="smb_btn" onclick="javascript: delImage('product_img_ext_4');"><input type="hidden" name="product_img_ext_4" id="product_img_ext_4" value="/images/detail_no_pic.gif" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id="td_upload" style="display: none">
                                        <td colspan="5" style="padding-left: 20px;" align="left">图片上传：<iframe id="iframe_upload" src="" width="300" height="90" frameborder="0" scrolling="no" align="absmiddle"></iframe><br />
                                            <span class="t12_grey" style="height:30px;line-height:30px;">建议图片尺寸：800*800；支持格式：jpg、gif、png；大小不要超过3M</span>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                      
                        </form>
                    </div></div>
                </div>
                <div class="clear"></div>
            </div>
        </div>
    </div>
    <!--主体 结束-->



  <ucbottom:bottom runat="server" ID="Bottom" />

</body>
</html>
