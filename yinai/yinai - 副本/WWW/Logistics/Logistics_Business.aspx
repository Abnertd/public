<%@ Page Language="C#" %>

<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.B2C.BLL.MEM" %>

<% 

    Session["Position"] = "Logistics";
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    Logistics MyLogistics = new Logistics();
    ISupplierLogistics MySupplierLog = SupplierLogisticsFactory.CreateSupplierLogistics();
    Addr addr = new Addr();
    LogisticsInfo logisticsinfo = null;
    //物流商品ID
    string id = tools.CheckStr(Request["id"]);
    int logistic_id = int.Parse(id);
    //int logistic_id =11111;
    int Supplier_LogisticsID;
    string Logistics_CompanyName = "";
    string Logistics_Tel = "";
    string fahuo = "";
    string shouhuo = "";
    string Logistics_Name = "";
    string Supplier_Logistics_DeliveryTime = "";
    SupplierLogisticsInfo supplierLogInfo = MySupplierLog.GetSupplierLogisticsByID(logistic_id, pub.CreateUserPrivilege("64bb04aa-9b78-4c41-ae9c-e94f57581e22"));
    if (supplierLogInfo != null)
    {
        Supplier_LogisticsID = supplierLogInfo.Supplier_LogisticsID;
        Supplier_Logistics_DeliveryTime = supplierLogInfo.Supplier_Logistics_DeliveryTime.ToString("yyyy-MM-dd");
        if (Supplier_LogisticsID > 0)
        {
            fahuo = addr.DisplayAddress(supplierLogInfo.Supplier_Address_State, supplierLogInfo.Supplier_Address_City, supplierLogInfo.Supplier_Address_County);


            shouhuo = addr.DisplayAddress(supplierLogInfo.Supplier_Orders_Address_State, supplierLogInfo.Supplier_Orders_Address_City, supplierLogInfo.Supplier_Orders_Address_County);
            logisticsinfo = MyLogistics.GetLogisticsByID(Supplier_LogisticsID, pub.CreateUserPrivilege("8426b82b-1be6-4d27-84a7-9d45597be557"));
            if (logisticsinfo != null)
            {
                Logistics_CompanyName = logisticsinfo.Logistics_CompanyName;
                Logistics_Tel = logisticsinfo.Logistics_Tel;
                Logistics_Name = logisticsinfo.Logistics_Name;
                //Logistics_CompanyName =logisticsinfo.Logistics_CompanyName;
                //Logistics_CompanyName =logisticsinfo.Logistics_CompanyName;

            }
            else
            {
                pub.Msg("error", "错误信息", "物流商信息不存在", false, "{back}");
            }
        }
        else
        {
            pub.Msg("error", "错误信息", "物流商信息不存在", false, "{back}");
        }
    }
   
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%="仓储物流 - " + pub.SEO_TITLE()%></title>
    <meta name="Keywords" content="<% = Application["Site_Keyword"]%>" />
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    
   <!--layer弹出框.js .css引用 开始-->
 <link href="../layer.m/layer.m.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery-1.5.1.min.js" charset="gb2312"></script>
       <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js" ></script>
  <!--layer弹出框.js .css引用 结束-->

    <script src="/scripts/common.js"></script>
    <script src="/scripts/1.js"></script>
    <script type="text/javascript">
        window.onload = function () {
            var SDmodel = new scrollDoor();
            SDmodel.sd(["a01", "a02", "a03", "a04"], ["aa01", "aa02", "aa03", "aa04"], "on", " ");
            SDmodel.sd(["b01", "b02"], ["bb01", "bb02"], "on", " ");
        }
    </script>
    <!--滑动门 结束-->
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
    <script src="/scripts/1.js"></script>
    <!--弹出菜单 start-->
    <script>
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
    <style>
        .ccwl_part2_1 span select {
            border: solid 1px #e4e4e4;
            height: 39px;
        }

        table td {
            font-weight: normal;
            color: #666;
            text-align: center;
        }

        .class1 {
            width: 50%;
            font-size: 18px;
            text-align: right;
            padding: 8px 0px 8px 0px;
            font-size: 18px;
            font-weight: bold;
        }

        .class2 {
            width: 50%;
            text-align: left;
            padding: 8px 0px 8px 0px;
            font-size: 18px;
        }

        .ccwl_part2_21 {
            display: block;
            width: 179px;
            height: 41px;
            background: #ff6600;
            color: #fff;
            text-align: center;
            font-size: 16px;
            line-height: 41px;
        }


        table tr td a img {
        margin-top:30px;
        margin-left:592px;
        }
    </style>
</head>
<body>
    <div id="head_box">
        <!--顶部 开始-->
        <uctop:top runat="server" ID="HomeTop" />
             <!--顶部 结束-->
    </div>
    <!--主体 开始-->
    <div class="content">
        <!--位置 开始-->
        <div class="position">当前位置 > <a href="/">首页</a> > 我要找车</div>
        <!--位置 结束-->
        <div class="banner02">
            <img src="/images/img12.jpg" width="1200" height="156" />
        </div>
        <div class="parte" style="border: none;">
            <div class="ccwl_part1">
                <ul>
                    <li><a href="javascript:void(0);" class="part1_on">我要找车</a></li>
                </ul>
            </div>

            <table style="width: 1200px; padding-top: 25px;">
                <tr>
                    <td class="class1">联系人：</td>
                    <td class="class2"><%=Logistics_Name%></td>
                </tr>


                <tr>
                    <td class="class1">联系电话：</td>
                    <td class="class2"><%=Logistics_Tel %></td>
                </tr>
                <tr>
                    <td class="class1">物流商：</td>
                    <td class="class2"><%=Logistics_CompanyName %></td>
                </tr>
                <tr>
                    <td class="class1">发货地址：</td>
                    <td class="class2"><%=fahuo %></td>
                </tr>
                <tr>
                    <td class="class1">收货地址：</td>
                    <td class="class2"><%=shouhuo %></td>
                </tr>
                <tr>
                    <td class="class1">发货时间：</td>
                    <td class="class2"><% =Supplier_Logistics_DeliveryTime%></td>
                </tr>

                <tr>
                    <td colspan="2" ><a href="/index.aspx"><img  src="../images/fanhui.jpg"/></a></td>
                </tr>




            </table>

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

