<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>

<% 
    
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    Supplier supplier = new Supplier();
    Addr addr = new Addr();
    supplier.Supplier_Login_Check("/supplier/Supplier_Shop_Banner.aspx");
    if (tools.NullInt(Session["supplier_ishaveshop"]) == 0)
    {
        pub.Msg("error", "错误信息", "您还没有开通店铺", false, "/supplier/index.aspx");
    }
    SupplierShopInfo entity = supplier.GetSupplierShopBySupplierID(tools.NullInt(Session["supplier_id"]));

    if (entity == null)
    {
        Response.Redirect("/supplier/index.aspx");
    }
    string Shop_CateID = supplier.GetShopCategory(entity.Shop_ID);
    
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="店铺设置 - 我是卖家 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />


    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
        <link href="/css/index_newadd.css" rel="stylesheet" type="text/css" />
    
   <!--layer弹出框.js .css引用 开始-->
 <link href="../layer.m/layer.m.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery-1.5.1.min.js" charset="gb2312"></script>
       <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js" ></script>
  <!--layer弹出框.js .css引用 结束-->

    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script src="/scripts/common.js" type="text/javascript"></script>
    <script src="/scripts/jquery-extend-AdAdvance3.js" type="text/javascript"></script>

    <link rel="stylesheet" href="/public/colorpicker/css/colorpicker.css" type="text/css" />
    <script type="text/javascript" src="/public/colorpicker/js/colorpicker.js"></script>
    <link rel="stylesheet" type="text/css" href="/scripts/treeview/dhtmlxtree.css" />
    <script src="/scripts/treeview/dhtmlxcommon.js" type="text/javascript"></script>
    <script src="/scripts/treeview/dhtmlxtree.js" type="text/javascript"></script>
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
     
   <!--示范一个公告层 开始-->
  <script type="text/javascript">
      function SignUpNow() {
          layer.open({
              type: 2
 , title: false //不显示标题栏
              //, closeBtn: false
 , area: ['480px;', '340px']

 , shade: 0.8
 , id: 'LAY_layuipro' //设定一个id，防止重复弹出
 , resize: false
 , btnAlign: 'c'
 , moveType: 1 //拖拽模式，0或者1              
              , content: ("/Bid/SignUpPopup.aspx")
          });
      }
    </script>
   <!--示范一个公告层 结束-->
    <script type="text/javascript">
        $(document).ready(function () {
            $('#Shop_banner_Title_color').ColorPicker({
                onSubmit: function (hsb, hex, rgb, el) {
                    $(el).css('backgroundColor', '#' + hex);
                    $(el).val("#" + hex);
                    $(el).ColorPickerHide();
                },
                onBeforeShow: function () {
                    $(this).ColorPickerSetColor(this.value);
                }
            })
            .bind('keyup', function () {
                $(this).ColorPickerSetColor(this.value);
            });
        });
    </script>
    <style type="text/css">
        #treeboxbox_tree td {
            margin: 0;
            padding: 0;
        }

        .yz_title21 {
            margin-bottom: 10px;
            border-bottom: solid 2px #3891cd;
        }

            .yz_title21 li {
                cursor: pointer;
            }
    </style>
</head>
<body>

    <uctop:top ID="top1" runat="server" />



    <!--主体 开始-->
    <div class="webwrap">
        <div class="content02" style="background-color: #FFF;">
            <%-- <div class="content02_main" style="background-color: #FFF;">--%>
            <!--位置说明 开始-->
            <div class="position">当前位置 > <a href="/supplier/">我是卖家 > </a><strong>店铺设置</strong></div>
            <!--位置说明 结束-->
            <div class="partd_1">
                <div class="pd_left">
                    <% supplier.Get_Supplier_Left_HTML(2, 5); %>
                </div>
                <div class="pd_right">
                    <div class="blk14_1" style="margin-top: 0px;">
                        <h2>店铺Banner</h2>
                        <div class="blk17_sz">
                            <form name="formadd" id="formadd" method="post" action="/supplier/account_do.aspx">
                                <table width="100%" border="0" cellspacing="0" cellpadding="5" id="apply_2_content">
                                    <%=supplier.Display_Shop_Banner(entity.Shop_Banner)%>
                                  
                                    <tr>
                                        <td colspan="3">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td height="25" width="100">
                                                        <input name="shop_banner" value="0" type="radio" <%if (entity.Shop_Banner == 0) { Response.Write("checked"); } %> style="display: inline-block; vertical-align: middle; padding: 0; border: none; -webkit-box-shadow: none; -moz-box-shadow: none;" />
                                                        自定义牌匾</td>
                                                    <td style="padding-top: 10px">
                                                        <iframe id="iframe_upload" src="<% =Application["upload_server_url"]%>/public/uploadify.aspx?App=shopbanner&formname=formadd&frmelement=Shop_Banner_Img&rtvalue=1&rturl=<% =Application["upload_server_return_WWW"]%>" width="100%" height="90" frameborder="0" scrolling="no"></iframe>
                                                        <input name="Shop_Banner_Img" type="hidden" id="Shop_Banner_Img" value="<%=entity.Shop_Banner_Img %>" /></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" align="left" style="padding-left: 30px;">

                                                        <%
                                                            if (entity.Shop_Banner_Img.Length > 0)
                                                            {
                                                                Response.Write("<img src=\"" + pub.FormatImgURL(entity.Shop_Banner_Img, "fullpath") + "\" id=\"img_Shop_Banner_Img\" width=\"480\" height=\"50\" onload=\"AutosizeImage(this,480,50);\">");
                                                            }
                                                            else
                                                            {
                                                                Response.Write("<img src=\"/images/banner_nopic.gif\"  id=\"img_Shop_Banner_Img\">");
                                                            }
                                                        %>
                                                        <span class="t12_grey">图片尺寸：1200*117</span>


                                                          <span style="color:#ff6660;position:relative;  right: 138px;
    top: 30px;">说明:店铺匾额 显示在店铺搜索框与导航栏中间位置</span>

                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>                                    
                                </table>

                                <table width="100%" border="0" cellspacing="0" cellpadding="5">
                                    <tr>
                                        <td align="center" height="50">
                                            <input name="action" type="hidden" id="action" value="shop_set_banner" />
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
        <%--右侧浮动弹框 开始--%>
    <div id="leftsead">
        <ul>           
            <li>
               <a href="javascript:void(0);" onclick="SignUpNow();">
                    <div class="hides" style="width: 130px;height:50px; display: none;" id="qq">
                        <div class="hides" id="p1">
                           <img src="/images/nav_1_1.png" />
                        </div>
                    </div>
                    <img src="/images/nav_1.png" width="57px" height="50px" class="shows" />
                </a>
            </li>
            <li id="tel">
                <a href="javascript:void(0)">
                    <div class="hides" style="width: 130px;height:50px; display: none;" id="tels">
                        <div class="hides" id="p2">
                            <img src="/images/nav_2_1.png">
                        </div>

                    </div>
                    <img src="/images/nav_2.png" width="57px" height="50px" class="shows" />
                </a>
            </li>
            <li id="btn">
              <a id="top_btn" href="http://wpa.qq.com/msgrd?v=3&uin=3558103267&site=qq&menu=yes" target="_blank">
                    <div class="hides" style="width: 130px;height:50px; display: none">
                        <div class="hides" id="p3">
                            <img src="/images/nav_3_1.png" width="130px;" height="50px" id="Img1" />
                        </div>
                    </div>
                    <img src="/images/nav_3.png" width="57px" height="50px" class="shows" />
                </a>
            </li>
            <li id="Li1">
                <a href="#top">
                    <div class="hides" style="width: 130px; display: none" id="Div1">
                        <div class="hides" id="p4">
                            <img src="/images/nav_4_1.png" width="130px;" height="50px" />
                        </div>
                    </div>
                    <img src="/images/nav_4.png" width="57px" height="50px" class="shows" />
                </a>
            </li>
        </ul>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#leftsead a").hover(function () {

                $(this).children("div.hides").show();
                $(this).children("img.shows").hide();
                $(this).children("div.hides").animate({ marginRight: '0px' }, '0');

            }, function () {
                $(this).children("div.hides").animate({ marginRight: '-130px' }, 0, function () { $(this).hide(); $(this).next("img.shows").show(); });
            });
            $("#top_btn").click(function () { if (scroll == "off") return; $("html,body").animate({ scrollTop: 0 }, 600); });
        });
    </script>
   <%--右侧浮动弹框 结束--%>
        <!--主体 结束-->

      <ucbottom:bottom runat="server" ID="Bottom" />
</body>
</html>
