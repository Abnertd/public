<%@  Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>

<% 
    
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    Supplier supplier = new Supplier();
    supplier.Supplier_Login_Check("/supplier/Supplier_Merchants_Add.aspx");
    
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="招商加盟添加 - 我是卖家 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />

     <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script src="/scripts/common.js" type="text/javascript"></script>
    <script type="text/javascript" src="/Public/kindeditor/kindeditor-all.js"></script>
    <script type="text/javascript" src="/Public/kindeditor/lang/zh_CN.js"></script>
    <script src="/scripts/supplier.js" type="text/javascript"></script>
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
<!--弹出菜单 end-->
    <style type="text/css">
        .buttonupload {
            background: url("/images/buttonSkinAL.gif") repeat-x scroll center bottom #FEEEB1;
            border: 1px solid #F39D24;
            color: #5E2708;
            cursor: pointer;
            font-size: 12px;
            font-weight: normal;
            height: 20px;
            padding: 0 2px;
            text-decoration: none;
        }
    </style>

</head>
<body>

    <uctop:top ID="top1" runat="server" />
    <!--主体 开始-->
    <div class="webwrap">
    <div class="content02" style="background-color: #FFF;">
      
            <!--位置说明 开始-->        
          <div class="position">当前位置 > <a href="/supplier/">我是卖家</a> > 店铺管理 > <strong>招商加盟添加</strong></div>
            <!--位置说明 结束-->
            <div class="partc">
                <div class="pd_left">                    
                        <% supplier.Get_Supplier_Left_HTML(2, 8); %>                   
                </div>
                <div class="pd_right">
                    <div class="blk14_1" style="margin-top:0px;">                
                      <h2>招商加盟添加</h2>
                    <div class="blk17_sz">
                        <form name="formadd" id="formadd" method="post" action="/supplier/merchants_do.aspx">
                            <table width="100%" border="0" cellspacing="0" cellpadding="5" id="apply_1_content" class="table_padding_5">

                                <tr>
                                    <td width="92" class="name">招商加盟名称
                                    </td>
                                    <td width="801">
                                        <input name="Merchants_Name" type="text" id="Merchants_Name" style="width: 300px;background:none !important" class="input01" value="" /><i>*</i>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="92" class="name">有效期
                                    </td>
                                    <td width="801">
                                        <select name="Merchants_Validity" id="Merchants_Validity" style="width: 160px;">
                                            <option value="0">选择有效期</option>
                                            <% supplier.Supplier_Merchants_Validity(0); %>
                                        </select><i>*</i>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="92" class="name">加盟渠道
                                    </td>
                                    <td width="801">
                                        <select name="Merchants_Channel" id="Merchants_Channel" style="width: 160px;">
                                            <option value="线上" selected="selected">线上</option>
                                            <option value="线下">线下</option>
                                        </select>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="92" class="name">优势
                                    </td>
                                    <td width="801">
                                        <input name="Merchants_Advantage" id="Merchants_Advantage" type="text" maxlength="100" />
                                        <i>*</i>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="92" class="name">加盟条件
                                    </td>
                                    <td width="801">
                                        <textarea cols="50" rows="5" name="Merchants_Trem" ></textarea><i>*</i>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="92" class="name">预览图片
                                    </td>
                                    <td width="801">
                                        <iframe id="iframe_upload" src="<% =Application["upload_server_url"]%>/public/uploadify.aspx?App=merchants&formname=formadd&frmelement=merchants_Img&rtvalue=1&rturl=<% =Application["upload_server_return_WWW"]%>" width="100%" height="90" frameborder="0" scrolling="no"></iframe>
                                        <input name="merchants_Img" type="hidden" id="merchants_Img" />
                                    </td>
                                </tr>
                                <tr id="tr_merchants_Img" style="display: none;">
                                    <td  width="92" class="name"></td>
                                    <td width="801">
                                        <img src="" id="img_merchants_Img" width="320" height="320" /></td>
                                </tr>
                                <tr>
                                    <td width="92" class="name">说明
                                    </td>
                                    <td width="801">
                                        <textarea id="Merchants_Intro" name="Merchants_Intro" rows="5" cols="50"></textarea>
                                        <script type="text/javascript">
                                            var Merchants_IntroEditor;
                                            KindEditor.ready(function (K) {
                                                Merchants_IntroEditor = K.create('#Merchants_Intro', {
                                                    width: '100%',
                                                    height: '500px',
                                                    filterMode: false,
                                                    afterBlur: function () { this.sync(); }
                                                });
                                            });
                                        </script>
                                        <i>*</i>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="92" class="name"></td>
                                    <td width="801">
                                        <input name="action" type="hidden" id="action" value="save">
                                        <a href="javascript:void();" onclick="$('#formadd').submit();" class="a11"></a></td>
                                </tr>
                            </table>


                        </form>

                    </div>
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
