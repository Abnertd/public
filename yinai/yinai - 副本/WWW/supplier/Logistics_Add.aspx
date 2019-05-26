﻿<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>
<% 
    
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    Addr addr = new Addr();
    Session["Cur_Position"] = Session["Home_Position"] = "";
    string orders_sn = tools.CheckStr(Request["orders_sn"]);
    Supplier supplier = new Supplier();
    Logistics MyLogistics = new Logistics();
    string Supplier_Orders_Address_State = "", Supplier_Orders_Address_City = "", Supplier_Orders_Address_County = "", Supplier_Orders_Address_StreetAddress = "";
    supplier.Supplier_Login_Check("/supplier/Logistics_Add.aspx?orders_sn=" + orders_sn);

    OrdersInfo entity = supplier.GetOrdersInfoBySN(orders_sn, tools.NullInt(Session["supplier_id"]));
    double Supplier_Account;
    Supplier_Account = 0;
    SupplierInfo supplierinfo = supplier.GetSupplierByID();
    if (supplierinfo != null)
    {
        Supplier_Account = supplierinfo.Supplier_Security_Account;
        if (tools.NullInt(Session["supplier_ishaveshop"]) == 0)
        {
            pub.Msg("error", "错误信息", "您还没有开通店铺", false, "/supplier/index.aspx");
        }
    }
    if(entity!=null)
    {
        Supplier_Orders_Address_State = entity.Orders_Address_State;
        Supplier_Orders_Address_City = entity.Orders_Address_City;
        Supplier_Orders_Address_County = entity.Orders_Address_County;
        Supplier_Orders_Address_StreetAddress = entity.Orders_Address_StreetAddress;
        
        if(MyLogistics.Chcek_SupplierLogistic(entity.Orders_ID))
        {
            pub.Msg("error", "错误信息", "该订单已生成物流信息", false, "{back}");
        }
    }
    else
    {
        pub.Msg("error", "错误信息", "记录不存在", false, "/supplier/order_list.aspx");
    }
    DateTime Today = DateTime.Today;
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="创建物流信息 - 我是卖家 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />

     <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script src="/scripts/common.js" type="text/javascript"></script>
    <script src="/scripts/jquery-extend-AdAdvance3.js" type="text/javascript"></script>
     <link href="/css/index.css" rel="stylesheet" type="text/css" />

    <!--layer弹出框.js .css引用 开始-->
    <link href="../layer.m/layer.m.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery-1.5.1.min.js" charset="gb2312"></script>
    <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js"></script>
    <!--layer弹出框.js .css引用 结束-->

    <script src="/scripts/member.js" type="text/javascript"></script>
    <script type="text/javascript" src="/scripts/common.js"></script>
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="../css/index_newadd.css" rel="stylesheet" />
 <%--   <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>--%>
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script src="/js/hdtab.js" type="text/javascript"></script>
    <link type="text/css" href="/scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
    <script src="/scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
    <script src="/scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>
        <link href="/scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery-ui/jquery-ui.js"></script>
    <script type="text/javascript" src="/scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js"></script>
    <script type="text/javascript" src="/Public/kindeditor/kindeditor-all.js"></script>
    <script type="text/javascript" src="/Public/kindeditor/lang/zh_CN.js"></script>
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
        .zkw_19_fox img {
            vertical-align: middle;
            display: inline;
        }
        .b02_main ul li {
            margin-left:1px;
        }
    </style>
</head>
<body>

    <uctop:top ID="top1" runat="server" />


    <!--主体 开始-->
    <div class="webwrap">
    <div class="content02" style="background-color: #FFF;">
     
            <!--位置说明 开始-->
            <div class="position">当前位置 > <a href="/supplier/">我是卖家</a> > 我的订单 > <strong>创建物流信息</strong></div>
            <!--位置说明 结束-->
            <div class="partc">
                <div class="pd_left">
               
                        <% supplier.Get_Supplier_Left_HTML(6, 1); %>
                  
                </div>
                <div class="pc_right">

                        
                        <div class="blk14_1" style="margin-top: 1px;">
                        <h2>创建物流信息</h2>
                        

                        <div class="b14_1_main">
                        <form name="frm_bid" id="frm_bid" method="post" action="/supplier/Logistics_do.aspx">

                        <div class="b02_main">
                            <ul style="width:850px;">
                                <li><span><i>*</i>货物名称：</span><label><input name="Supplier_Logistics_Name" id="Supplier_Logistics_Name" type="text" style="width: 298px;" value="" /></label></li>
                                <div class="clear"></div>
                                <li><span><i>*</i>数量：</span><label><input name="Supplier_Logistics_Number" id="Supplier_Logistics_Number" type="text" style="width: 298px;" value="" /></label></li>
                                <div class="clear"></div>

                                <li><span><i>*</i>到货时间：</span><label><input name="Supplier_Logistics_DeliveryTime" id="Supplier_Logistics_DeliveryTime" type="text" value="<%=Today.ToString("yyyy-MM-dd") %>" readonly="readonly" style="width: 138px;" /></label></li>
                                <script>$(function () { $("#Supplier_Logistics_DeliveryTime").datepicker({ inline: true }); })</script>
                                <div class="clear"></div>
                                
                                <li><span><i>*</i>发货地：</span><label id="div_area"><%=addr.SelectAddressNew("div_area", "Supplier_Address_State", "Supplier_Address_City", "Supplier_Address_County", "", "", "")%></label></li>
                                <div class="clear"></div>

                                <li><span><i></i>&nbsp;</span><label><input name="Supplier_Address_StreetAddress" id="Supplier_Address_StreetAddress" type="text" style="width: 298px;" value="" /></label></li>
                                <div class="clear"></div>

                                <li><span><i>*</i>收货地：</span><label id="div_area2"><%=addr.SelectAddressNew("div_area2", "Supplier_Orders_Address_State", "Supplier_Orders_Address_City", "Supplier_Orders_Address_County", Supplier_Orders_Address_State, Supplier_Orders_Address_City, Supplier_Orders_Address_County)%></label></li>
                                <div class="clear"></div>

                                <li><span><i></i>&nbsp;</span><label><input name="Supplier_Orders_Address_StreetAddress" id="Supplier_Orders_Address_StreetAddress" type="text" style="width: 298px;" value="<%=Supplier_Orders_Address_StreetAddress %>" /></label></li>
                                <div class="clear"></div>
<%--                                <li><span><i></i>商品清单：</span>
                                    <label></label></li>
                                <li style="width:973px;float:left; margin-left:-80px;"><%=supplier.Order_Logistics_Goods(entity.Orders_ID,1) %>
                                    </li>
                                <div class="clear"></div>--%>
                                <li><a href="javascript:void(0);" onclick="$('#frm_bid').submit();"  class="a05">保存</a></li>
                            </ul>
                            
                            <div class="clear"></div>
                        </div>
                            <input name="action" type="hidden" id="action" value="new" />
                            <input type="hidden" id="Supplier_Address_State" name="Supplier_Address_State" value="" />
                                        <input type="hidden" id="Supplier_Address_City" name="Supplier_Address_City" value="" />
                                        <input type="hidden" id="Supplier_Address_County" name="Supplier_Address_County" value="" />
                            <input type="hidden" name="Supplier_Address_Country" id="Supplier_Address_Country" value="CN" />
                            <input type="hidden" id="Supplier_Orders_Address_State" name="Supplier_Orders_Address_State" value="<%=Supplier_Orders_Address_State %>" />
                                        <input type="hidden" id="Supplier_Orders_Address_City" name="Supplier_Orders_Address_City" value="<%=Supplier_Orders_Address_City %>" />
                                        <input type="hidden" id="Supplier_Orders_Address_County" name="Supplier_Orders_Address_County" value="<%=Supplier_Orders_Address_County %>" />
                            <input type="hidden" name="Supplier_Orders_Address_Country" id="Supplier_Orders_Address_Country" value="CN" />
                            <input type="hidden" name="Supplier_OrdersID" id="Supplier_OrdersID" value="<%=entity.Orders_ID %>" />
                            <input type="hidden" name="Supplier_SupplierID" id="Supplier_SupplierID" value="<%=entity.Orders_SupplierID %>" />
                            </form>
                                </div>

                            </div>
                        </div>

                </div>
            </div>
        </div>
        <div class="clear"></div>

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
