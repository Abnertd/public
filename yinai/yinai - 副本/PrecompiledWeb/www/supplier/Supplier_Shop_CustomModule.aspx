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
    supplier.Supplier_Login_Check("/supplier/Supplier_Shop_CustomModule.aspx");
    if (tools.NullInt(Session["supplier_ishaveshop"]) == 0)
    {
        pub.Msg("error", "错误信息", "您还没有开通店铺", false, "/supplier/index.aspx");
    }
    SupplierShopInfo entity = supplier.GetSupplierShopBySupplierID(tools.NullInt(Session["supplier_id"]));

    if (entity == null)
    {
        Response.Redirect("/supplier/index.aspx");
    }
    else
    {
        int isactive = tools.CheckInt(Request["IsActive"]);
        string sign = tools.CheckStr(Request["sign"]);
        if (sign != "")
        {
            if (sign == "Shop_Banner_IsActive")
            {
                entity.Shop_Banner_IsActive = isactive;
                supplier.ChangeShopActive(entity);
                Response.Redirect("Supplier_Shop_CustomModule.aspx");
            }
            if (sign == "Shop_Top_IsActive")
            {
                entity.Shop_Top_IsActive = isactive;
                supplier.ChangeShopActive(entity);
                Response.Redirect("Supplier_Shop_CustomModule.aspx");
            }
            if (sign == "Shop_TopNav_IsActive")
            {
                entity.Shop_TopNav_IsActive = isactive;
                supplier.ChangeShopActive(entity);
                Response.Redirect("Supplier_Shop_CustomModule.aspx");
            }
            if (sign == "Shop_Info_IsActive")
            {
                entity.Shop_Info_IsActive = isactive;
                supplier.ChangeShopActive(entity);
                Response.Redirect("Supplier_Shop_CustomModule.aspx");
            }
            if (sign == "Shop_LeftSearch_IsActive")
            {
                entity.Shop_LeftSearch_IsActive = isactive;
                supplier.ChangeShopActive(entity);
                Response.Redirect("Supplier_Shop_CustomModule.aspx");
            }
            if (sign == "Shop_LeftCate_IsActive")
            {
                entity.Shop_LeftCate_IsActive = isactive;
                supplier.ChangeShopActive(entity);
                Response.Redirect("Supplier_Shop_CustomModule.aspx");
            }
            if (sign == "Shop_LeftSale_IsActive")
            {
                entity.Shop_LeftSale_IsActive = isactive;
                supplier.ChangeShopActive(entity);
                Response.Redirect("Supplier_Shop_CustomModule.aspx");
            }

            if (sign == "Shop_Left_IsActive")
            {
                entity.Shop_Left_IsActive = isactive;
                supplier.ChangeShopActive(entity);
                Response.Redirect("Supplier_Shop_CustomModule.aspx");
            }
            if (sign == "Shop_Right_IsActive")
            {
                entity.Shop_Right_IsActive = isactive;
                supplier.ChangeShopActive(entity);
                Response.Redirect("Supplier_Shop_CustomModule.aspx");
            }
            if (sign == "Shop_RightProduct_IsActive")
            {
                entity.Shop_RightProduct_IsActive = isactive;
                supplier.ChangeShopActive(entity);
                Response.Redirect("Supplier_Shop_CustomModule.aspx");
            }


        }
    }
    string Shop_CateID = supplier.GetShopCategory(entity.Shop_ID);
    
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="自定义模块 - 我是卖家 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />


    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    
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
    <script type="text/javascript">
        function chagehomeactive(obj, obj1) {

            window.location.href = "Supplier_Shop_CustomModule.aspx?IsActive=" + obj + "&sign=" + obj1 + "";
        }

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

        .yz_blk19_main a {
            color: #336699;
        }

        .b14_1_main table td a {
            color: #ff6600;
        }
    </style>
</head>
<body>

    <uctop:top ID="top1" runat="server" />

    <!--主体 开始-->
    <div class="webwrap">
        <div class="content02" style="background-color: #FFF;">

            <!--位置说明 开始-->
            <div class="position">当前位置 > <a href="/supplier/">我是卖家 > </a><strong>自定义模块</strong></div>
            <!--位置说明 结束-->
            <div class="partd_1">
                <div class="pd_left">
                    <% supplier.Get_Supplier_Left_HTML(2, 5); %>
                </div>
                <div class="pd_right">
                    <div class="blk14_1" style="margin-top: 0px;">
                        <h2>自定义模块</h2>
                        <div class="b14_1_main" style="margin-top: 15px;">
                            <table border="0" cellspacing="1" cellpadding="0" width="973" class="table02">
                                <tr>
                                    <td width="400" align="center" class="name">模块名称</td>
                                    <td width="294" align="center" class="name">状态</td>
                                    <td width="299" align="center" class="name">操作</td>
                                </tr>
                                <tr>
                                    <td>店铺匾额</td>
                                    <td>
                                        <%if (entity.Shop_Banner_IsActive == 1)
                                          { %>
                                        <a href="javascript:void(0);" onclick="chagehomeactive('0','Shop_Banner_IsActive');">显示</a>
                                        <%}
                                          else
                                          {%>
                                        <a href="javascript:void(0);" onclick="chagehomeactive('1','Shop_Banner_IsActive');">不显示</a>
                                        <%} %>
                                    </td>
                                    <td><a href="Supplier_Shop_Banner.aspx" class="a12">修改</a></td>
                                </tr>
                                <tr class="bg">
                                    <td>店铺banner</td>
                                    <td>
                                        <%if (entity.Shop_Top_IsActive == 1)
                                          { %>
                                        <a href="javascript:void(0);" onclick="chagehomeactive('0','Shop_Top_IsActive');">显示</a>
                                        <%}
                                          else
                                          {%>
                                        <a href="javascript:void(0);" onclick="chagehomeactive('1','Shop_Top_IsActive');">不显示</a>
                                        <%} %>
                                    </td>
                                    <td><a href="Supplier_Shop_Top.aspx" class="a12">修改</a></td>
                                </tr>
                                <tr>
                                    <td>店铺信息</td>
                                    <td>
                                        <%if (entity.Shop_Info_IsActive == 1)
                                          { %>
                                        <a href="javascript:void(0);" onclick="chagehomeactive('0','Shop_Info_IsActive');">显示 </a>
                                        <%}
                                          else
                                          {%>
                                        <a href="javascript:void(0);" onclick="chagehomeactive('1','Shop_Info_IsActive');">不显示 </a>
                                        <%} %>
                                    </td>
                                    <td>--</td>
                                </tr>
                                <tr class="bg">
                                    <td>店铺首页左侧店内搜索</td>
                                    <td>
                                        <%if (entity.Shop_LeftSearch_IsActive == 1)
                                          { %>
                                        <a href="javascript:void(0);" onclick="chagehomeactive('0','Shop_LeftSearch_IsActive');">显示 </a>
                                        <%}
                                          else
                                          {%>
                                        <a href="javascript:void(0);" onclick="chagehomeactive('1','Shop_LeftSearch_IsActive');">不显示 </a>
                                        <%} %>
                                    </td>
                                    <td>--</td>
                                </tr>
                                <tr>
                                    <td>店铺首页左侧商品分类导航</td>
                                    <td>
                                        <%if (entity.Shop_LeftCate_IsActive == 1)
                                          { %>
                                        <a href="javascript:void(0);" onclick="chagehomeactive('0','Shop_LeftCate_IsActive');">显示 </a>
                                        <%}
                                          else
                                          {%>
                                        <a href="javascript:void(0);" onclick="chagehomeactive('1','Shop_LeftCate_IsActive');">不显示 </a>
                                        <%} %>
                                    </td>
                                    <td>--</td>
                                </tr>
                                <tr class="bg">
                                    <td>店铺首页左侧商品销售排行</td>
                                    <td>
                                        <%if (entity.Shop_LeftSale_IsActive == 1)
                                          { %>
                                        <a href="javascript:void(0);" onclick="chagehomeactive('0','Shop_LeftSale_IsActive');">显示 </a>
                                        <%}
                                          else
                                          {%>
                                        <a href="javascript:void(0);" onclick="chagehomeactive('1','Shop_LeftSale_IsActive');">不显示 </a>
                                        <%} %>
                                    </td>
                                    <td>--</td>
                                </tr>
                                <tr>
                                    <td>店铺首页左侧自定义内容</td>
                                    <td>
                                        <%if (entity.Shop_Left_IsActive == 1)
                                          { %>
                                        <a href="javascript:void(0);" onclick="chagehomeactive('0','Shop_Left_IsActive');">显示 </a>
                                        <%}
                                          else
                                          {%>
                                        <a href="javascript:void(0);" onclick="chagehomeactive('1','Shop_Left_IsActive');">不显示 </a>
                                        <%} %>
                                    </td>
                                    <td><a href="Supplier_Shop_Left.aspx" class="a12">修改</a></td>
                                </tr>
                                <tr class="bg">
                                    <td>店铺首页右侧自定义内容</td>
                                    <td>
                                        <%if (entity.Shop_Right_IsActive == 1)
                                          { %>
                                        <a href="javascript:void(0);" onclick="chagehomeactive('0','Shop_Right_IsActive');">显示 </a>
                                        <%}
                                          else
                                          {%>
                                        <a href="javascript:void(0);" onclick="chagehomeactive('1','Shop_Right_IsActive');">不显示 </a>
                                        <%} %>
                                    </td>
                                    <td><a href="Supplier_Shop_Index.aspx" class="a12">修改</a></td>
                                </tr>
                                <tr>
                                    <td>店铺首页右侧全部商品</td>
                                    <td>
                                        <%if (entity.Shop_RightProduct_IsActive == 1)
                                          { %>
                                        <a href="javascript:void(0);" onclick="chagehomeactive('0','Shop_RightProduct_IsActive');">显示 </a>
                                        <%}
                                          else
                                          {%>
                                        <a href="javascript:void(0);" onclick="chagehomeactive('1','Shop_RightProduct_IsActive');">不显示 </a>
                                        <%} %>
                                    </td>
                                    <td>--</td>
                                </tr>
                                <tr class="bg">
                                    <td>店铺首页导航栏</td>
                                    <td>
                                        <%if (entity.Shop_TopNav_IsActive == 1)
                                          { %>
                                        <a href="javascript:void(0);" onclick="chagehomeactive('0','Shop_TopNav_IsActive');">显示 </a>
                                        <%}
                                          else
                                          {%>
                                        <a href="javascript:void(0);" onclick="chagehomeactive('1','Shop_TopNav_IsActive');">不显示 </a>
                                        <%} %>
                                    </td>
                                    <td>--</td>
                                </tr>
                            </table>
                        </div>
                    </div>


                       <div class="blk14_1" style="margin-top: 15px;">
                        <h2>店铺首页预览</h2><img src="/images/ShopHomeShow1.png" style=" margin-left: -5px;
    margin-top: 20px;
    width: 985px;" /></div>
                </div>
                <div class="clear"></div>
            </div>

           
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
