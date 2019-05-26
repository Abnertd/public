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
    supplier.Supplier_Login_Check("/supplier/Supplier_Shop_Config.aspx");
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
    <title><%="店铺基础设置 - 我是卖家 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />


    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
     <link href="../css/index_newadd.css" rel="stylesheet" />
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
        #treeboxbox_tree table {
            width: 415px;
        }
    </style>
</head>
<body>

    <uctop:top ID="top1" runat="server" />



    <!--主体 开始-->
    <div class="webwrap">
        <div class="content02" style="background-color: #FFF;">
            <!--位置说明 开始-->
            <div class="position">当前位置 > <a href="/supplier/">我是卖家 > </a><strong>店铺基础设置</strong></div>
            <!--位置说明 结束-->

            <div class="partd_1">
                <div class="pd_left">
                    <%-- <div class="blk12">--%>
                    <% supplier.Get_Supplier_Left_HTML(2, 3); %>
                    <%-- </div>--%>
                </div>
                <div class="pd_right">
                    <div class="blk14_1" style="margin-top:0px;">
                        <h2>店铺基础设置</h2>
                        <div class="blk17_sz">
                            <%
                                if (tools.CheckStr(Request.QueryString["tip"]) == "success")
                                {
                                    pub.Tip("positive", "您的设置信息已保存！");
                            %>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td height="10"></td>
                                </tr>
                            </table>
                            <%}%>


                            <form name="formadd" id="formadd" method="post" action="/supplier/account_do.aspx" onsubmit="javascript:MM_findObj('Shop_CateID').value = tree.getAllChecked();">
                                <table width="893" border="0" cellspacing="0" cellpadding="0" id="apply_1_content" class="table_padding_5">
                                    <tr>
                                        <td width="92" class="name">店铺名称：</td>
                                        <td width="801"><%=entity.Shop_Name%></td>
                                    </tr>
                                    <tr>
                                        <td width="92" class="name">店铺等级：</td>
                                        <td width="801"><%=supplier.Get_Shop_Type(entity.Shop_Type)%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="92" class="name">店铺域名：</td>
                                        <td width="801"><a href="/supplier/account_do.aspx?action=shopview" target="_blank"><%=supplier.GetShopURL(entity.Shop_Domain)%></a></td>
                                    </tr>
                                    <tr>
                                        <td width="92" class="name">店铺号：</td>
                                        <td width="801"><%=entity.Shop_Code %>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="92" class="name">店铺图片：</td>
                                        <td width="801">
                                            <table border="0" cellpadding="0" cellspacing="0" style="width: 600px;" align="left">
                                                <tr>
                                                    <td align="left" height="120">
                                                        <input type="hidden" value="<%=entity.Shop_Img %>" name="Shop_Img" />
                                                        <img id="img_Shop_Img" src="<%=pub.FormatImgURL(entity.Shop_Img,"fullpath") %>" width="120" height="120" onload="javascript:AutosizeImage(this,120,120);">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <iframe id="iframe1" src="<% =Application["Upload_Server_URL"]%>/public/uploadify.aspx?App=supplier&formname=formadd&frmelement=Shop_Img&rtvalue=1&rturl=<% =Application["Upload_Server_Return_WWW"]%>" width="100%" height="90" frameborder="0" scrolling="no"></iframe>
                                                        建议上传图片尺寸：120*120
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="92" class="name">牌匾标题：</td>
                                        <td width="801">
                                            <input name="Shop_Banner_Title" type="text" id="Shop_Banner_Title" style="width: 300px; background: none" class="input01" maxlength="50" value="<%=entity.Shop_Banner_Title%>" />
                                        </td>
                                    </tr>
                                  <%--  <tr>
                                        <td width="92" class="name">标题字体：</td>
                                        <td width="801">字体：
                                                  <select name="Shop_Banner_Title_Family">
                                                      <option value="宋体" <%=pub.CheckRadio("宋体",entity.Shop_Banner_Title_Family) %>>宋体</option>
                                                      <option value="黑体" <%=pub.CheckRadio("黑体",entity.Shop_Banner_Title_Family) %>>黑体</option>
                                                      <option value="微软雅黑" <%=pub.CheckRadio("微软雅黑",entity.Shop_Banner_Title_Family) %>>微软雅黑</option>
                                                      <option value="楷体_GB2312" <%=pub.CheckRadio("楷体_GB2312",entity.Shop_Banner_Title_Family) %>>楷体_GB2312</option>
                                                  </select>
                                            字号：<input name="Shop_Banner_Title_Size" type="text" id="Shop_Banner_Title_Size" class="input01" style="background: none" size="10" maxlength="50" value="<%=entity.Shop_Banner_Title_Size%>" />
                                            px 
               颜色：<input name="Shop_banner_Title_color" type="text" id="Shop_banner_Title_color" class="input01" style="background: none" size="10" maxlength="50" value="<%=entity.Shop_banner_Title_color%>" />
                                            左边距：<input name="Shop_Banner_Title_LeftPadding" type="text" id="Shop_Banner_Title_LeftPadding" class="input01" style="background: none" size="10" maxlength="50" value="<%=entity.Shop_Banner_Title_LeftPadding%>" />
                                            px 
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td width="92" class="name">主营分类：</td>
                                        <td width="801">
                                            <div class="td_box" style="width: 460px;">
                                                <table style="width: 430px; border: 1px solid #fff;">
                                                    <tr>
                                                        <td valign="top" id="treeboxbox_tree" style="width: 460px;"></td>
                                                    </tr>
                                                </table>
                                                <script type="text/javascript">
                                                    tree = new dhtmlXTreeObject("treeboxbox_tree", "100%", "100%", 0);
                                                    tree.setSkin('dhx_skyblue');
                                                    tree.setImagePath("/scripts/treeview/imgs/csh_dhx_skyblue/");
                                                    tree.enableCheckBoxes(1);
                                                    tree.enableThreeStateCheckboxes(true);
                                                    tree.loadXML("treedata1.aspx?cate_id=<% =Shop_CateID%>");
                                                </script>
                                                <span id="div_Shop_CateID"></span>
                                                <input name="Shop_CateID" type="hidden" id="Shop_CateID" style="background: none" value="<% =Shop_CateID%>" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="92" class="name">主营产品：</td>
                                        <td width="801">
                                            <input name="Shop_MainProduct" type="text" id="Shop_MainProduct" style="width: 300px; background: none" class="input01" maxlength="50" value="<%=entity.Shop_MainProduct%>" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="92" class="name">SEO标题：</td>
                                        <td width="801">
                                            <input name="Shop_SEO_Title" type="text" id="Shop_SEO_Title" style="width: 300px; background: none" class="input01" maxlength="50" value="<%=entity.Shop_SEO_Title%>" />
                                       <span style="display:block"><span style="color:red">*</span> 标题是被搜索引擎当作确定当前网页主题的最主要的参数。  </span>
                                             </td>
                                    </tr>
                                    <tr>
                                        <td width="92" class="name">SEO关键词</td>
                                        <td width="801">
                                            <textarea name="Shop_SEO_Keyword" cols="50" rows="5"><%=entity.Shop_SEO_Keyword%></textarea>
                                             <span style="display:block"><span style="color:red">*</span>关键词优化是让网站目标关键词在某个搜索引擎上得到更好的排名。让更多的用户都能快速的查找到自己的网站关键词。  </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="92" class="name">SEO介绍：</td>
                                        <td width="801">
                                            <textarea name="Shop_SEO_Description" cols="50" rows="5"><%=entity.Shop_SEO_Description%></textarea>
                                       
                                          
                                             <span style="display:block"><span style="color:red">*</span>通过总结搜索引擎的排名规律,对网站进行合理优化,使你的网站在 百度 和Google的排名提高,让搜索引擎给你带来客户。</span>
                                             </td>
                                    </tr>
                                    <tr>
                                        <td width="92" class="name"></td>
                                        <td width="801">
                                            <input name="action" type="hidden" id="action" value="shop_set" />
                                            <a href="javascript:void();" onclick="$('#formadd').submit();" class="a11"></a>
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
