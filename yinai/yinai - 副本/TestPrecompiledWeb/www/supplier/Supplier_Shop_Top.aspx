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
    supplier.Supplier_Login_Check("/supplier/Supplier_Shop_Index.aspx");
    if (tools.NullInt(Session["supplier_ishaveshop"]) == 0)
    {
        pub.Msg("error", "错误信息", "您还没有开通店铺", false, "/supplier/index.aspx");
    }

    string Shop_Pages_Intro = "";
    int Shop_Pages_Sort = 0;
    int Shop_Pages_IsActive = 0;
    SupplierShopPagesInfo entity = supplier.GetSupplierShopPagesByIDSign("INDEXTOP", tools.NullInt(Session["supplier_id"]));
    if (entity != null)
    {
        Shop_Pages_Intro = entity.Shop_Pages_Content;
        Shop_Pages_Sort = entity.Shop_Pages_Sort;
        Shop_Pages_IsActive = entity.Shop_Pages_IsActive;
    }
    
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="店铺首页通栏 - 我是卖家 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />

  
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="/css/index_newadd.css" rel="stylesheet" type="text/css" />
       <link href="/css/index.css" rel="stylesheet" type="text/css" />
    
   <!--layer弹出框.js .css引用 开始-->
 <link href="../layer.m/layer.m.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery-1.5.1.min.js" charset="gb2312"></script>
       <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js" ></script>
  <!--layer弹出框.js .css引用 结束-->
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
        <script type="text/javascript" src="/scripts/common.js"></script>
    <script type="text/javascript" src="/Public/kindeditor/kindeditor-all.js"></script>
    <script type="text/javascript" src="/Public/kindeditor/lang/zh_CN.js"></script>


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

</head>
<body>
    <uctop:top ID="top1" runat="server" />


    <!--主体 开始-->
      <div class="webwrap">
    <div class="content02"  style="margin-bottom: 20px;">
      

            <!--位置说明 开始-->
            <div class="position">当前位置 > <a href="/supplier/">我是卖家</a> > 店铺管理 > <strong>店铺banner</strong></div>
            <!--位置说明 结束-->
            <div class="partd_1">
                <div class="pd_left">
                <%--    <div class="blk12">--%>
                    <% supplier.Get_Supplier_Left_HTML(2, 5); %>
                     <%--   </div>--%>
                </div>
                <div class="pd_right">
                    <div class="blk14_1" style="margin-top:0px;">
                        <h2>店铺首页通栏</h2>
                    <div class="blk17_sz">

                        <form name="formadd" id="formadd" method="post" action="/supplier/Supplier_Shop_Pages_do.aspx">
                            <table width="100%" border="0" cellspacing="0" cellpadding="5" id="apply_1_content" class="table_padding_5">
                                <tr>
                                    <td align="right" style="line-height: 24px;" width="50" class="t12_53">图片</td>
                                    <td align="left">
                                        <iframe id="iframe2" src="<% =Application["Upload_Server_URL"]%>/public/uploadify.aspx?App=shoppages&formname=formadd&frmelement=Shop_Pages_Content&rtvalue=1&rturl=<% =Application["Upload_Server_Return_WWW"]%>" width="100%" height="90" frameborder="0" scrolling="no"></iframe>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="line-height: 24px;" width="60" class="t12_53">内容</td>
                                    <td align="left">
                                        <textarea cols="80" id="Shop_Pages_Content" name="Shop_Pages_Content" rows="16"><%=Shop_Pages_Intro%></textarea>
                                        <script type="text/javascript">
                                            var Shop_Pages_ContentEditor;
                                            KindEditor.ready(function (K) {
                                                Shop_Pages_ContentEditor = K.create('#Shop_Pages_Content', {
                                                    width: '100%',
                                                    height: '500px',
                                                    filterMode: false,
                                                    afterBlur: function () { this.sync(); }
                                                });
                                            });
                                        </script>
                                    </td>
                                </tr>
                                  <tr><td colspan="2"> <span style="color:#ff6660;position:relative; " >说明:店铺banner 显示在店铺导航栏下方位置</span></td></tr>
                                <tr>
                                    <td align="right" style="line-height: 24px;" class="t12_53">排序
                                    </td>
                                    <td align="left">
                                        <input name="Shop_Pages_Sort" type="text" id="Shop_Pages_Sort" class="txt_border"
                                            size="10" maxlength="10" value="<%=Shop_Pages_Sort %>" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" />

                                    </td>
                                </tr>
                               
                                <tr>
                                    <td align="right" class="t12_53"></td>
                                    <td>
                                        <input name="Shop_Pages_Sign" type="hidden" id="Shop_Pages_Sign" value="INDEXTOP"/>
                                        <input name="action" type="hidden" id="action" value="new" />
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
    <!--主体 结束-->
    <%--右侧浮动弹框 开始--%>
        <div id="leftsead">
            <ul>
                <li>
                    <a href="javascript:void(0);" onclick="SignUpNow();">
                        <div class="hides" style="width: 130px; height: 50px; display: none;" id="qq">
                            <div class="hides" id="p1">
                                <img src="/images/nav_1_1.png" />
                            </div>
                        </div>
                        <img src="/images/nav_1.png" width="57px" height="50px" class="shows" />
                    </a>
                </li>
                <li id="tel">
                    <a href="javascript:void(0)">
                        <div class="hides" style="width: 130px; height: 50px; display: none;" id="tels">
                            <div class="hides" id="p2">
                                <img src="/images/nav_2_1.png">
                            </div>

                        </div>
                        <img src="/images/nav_2.png" width="57px" height="50px" class="shows" />
                    </a>
                </li>
                <li id="btn">
                    <a id="top_btn" href="http://wpa.qq.com/msgrd?v=3&uin=3558103267&site=qq&menu=yes" target="_blank">
                        <div class="hides" style="width: 130px; height: 50px; display: none">
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

  <ucbottom:bottom runat="server" ID="Bottom" />

</body>
</html>
