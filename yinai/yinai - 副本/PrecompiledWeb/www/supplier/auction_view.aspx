<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>
<% 
    
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    Session["Cur_Position"] = Session["Home_Position"] = "";
    int BID = tools.CheckInt(Request["BID"]);
    Supplier supplier = new Supplier();
    supplier.Supplier_Login_Check("/supplier/auction_view.aspx?BID=" + BID);
    Bid Mybid = new Bid();
    double Supplier_Account;
    Supplier_Account = 0;
    SupplierInfo supplierinfo = supplier.GetSupplierByID();
    int list = tools.CheckInt(Request["list"]);
    BidInfo entity = Mybid.GetBidByID(BID);
    int Count = 0;
    if (supplierinfo != null)
    {
        Supplier_Account = supplierinfo.Supplier_Security_Account;
        if (tools.NullInt(Session["supplier_ishaveshop"]) == 0)
        {
            pub.Msg("error", "错误信息", "您还没有开通店铺", false, "/supplier/index.aspx");
        }
    }

    if (entity != null)
    {
        if (entity.Bid_MemberID != tools.NullInt(Session["member_id"]) || entity.Bid_MemberID == 0)
        {
            Response.Redirect("/supplier/auction_list.aspx");
        }
        if (entity.Bid_Type == 0)
        {
            Response.Redirect("/supplier/auction_list.aspx");
        }
        if (entity.BidProducts != null)
        {
            Count = entity.BidProducts.Count;
        }
    }
    else
    {
        Response.Redirect("/supplier/auction_list.aspx");
    }
    
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="拍卖详情 - 我是卖家 - " + pub.SEO_TITLE()%></title>
    <meta name="Keywords" content="<% = Application["Site_Keyword"]%>" />
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

    <link type="text/css" href="/scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
    <script src="/scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
    <script src="/scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>
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
        function confirmdelete(gotourl) {
            if ($("#dialog-confirmdelete").length == 0) {
                $("body").append("<div id=\"dialog-confirmdelete\" title=\"确定要删除吗?\"><p><span class=\"ui-icon ui-icon-alert\" style=\"float:left; margin:0 7px 20px 0;\"></span>记录删除后将无法恢复，您确定要删除吗？</p>");
            }
            $("#dialog-confirmdelete").dialog({
                modal: true,
                width: 400,
                buttons: {
                    "确认": function () {
                        $(this).dialog("close");
                        location = gotourl;
                    },
                    "取消": function () {
                        $(this).dialog("close");
                    }
                }
            });
            $("#dialog-confirmdelete").dialog({
                close: function () {
                    $(this).dialog("destroy");
                }
            });
        }

      

        window.onload = function () {
            ShowList(<%=list%>);
        };

    </script>
    <style type="text/css">
        .zkw_19_fox img {
            vertical-align: middle;
            display: inline;
        }

        .b14_1_main table td {
            border-bottom: none;
        }


        .blk02 h2 ul li.on1 {
            background-color: #ff6600;
            height: 21px;
            line-height: 32px;
            margin-right: 5px;
            margin-top: 10px;
            width: 78px;
            border-radius: 12px;
            color: #ffffff;
        }

        .b02_main ul li a.a05 {
            width: 120px;
            border-radius: 12px;
        }

        .b02_main ul {
            display: inline;
            float: left;
            /*margin-left: 80px;*/
            width: 610px;
        }

        .b02_main {
    padding: 0;
   
}
    
        .b14_1_main {
        border:none ;
        margin-top:0px;
        }
        .blk02 h2 {
    background-color: #f7f7f7;
    border-bottom: 1px solid #dddddd;
    height: 50px;
    position: relative;
}
        
.blk02 h2 ul li.on {
    background-color: #f7f7f7;
    cursor: pointer;
      font-weight:600;
}

.b14_1_main table td.name {
    background-color: #f9f9f9;
    border-left: 1px solid #eeeeee;
    color: #333;
    font-weight: bold;
    height: 40px;
    padding: 0;
}
.b14_1_main table td {
    border-bottom: medium none;
    border-right:none;
}

.b02_main .b14_1_main table td {
    border-bottom: 1px solid #eeeeee;
}
    </style>
</head>
<body>

    <uctop:top ID="top1" runat="server" />


    <!--主体 开始-->
    <div class="webwrap">
        <div class="content02" style="background-color: #FFF;">

            <!--位置说明 开始-->
            <div class="position">当前位置 > <a href="/supplier/">我是卖家 > </a><strong>拍卖详情</strong></div>
            <!--位置说明 结束-->
            <div class="partc">
                <div class="pd_left">

                    <% supplier.Get_Supplier_Left_HTML(5, 3); %>
                </div>
                <div class="pc_right">
                    <div class="blk02" style="border: none;">
                        <div style="border: 1px solid #ddd">
                            <h2>
                                <ul>
                                    <li id="a01" onclick="ShowList(0);" class="on">拍卖信息</li>

                                    <%if (entity.Bid_Status == 0)
                                      {
                                         
                                    %>
                                    <li id="a02" onclick="ShowList(0);" class="on1" style="float: right;"><a href="/supplier/auction_edit.aspx?BID=<%=BID%>" class="a05" style="margin-left: 0px; color: #ffffff">修改</a></li>
                                    <%} %>
                                </ul>
                            </h2>

                            <div class="b14_1_main" id="aa01" style="border: 0px solid #dddddd;">
                                <table width="975" border="0" cellspacing="0" cellpadding="0" style="border-bottom: 0px solid #eeeeee; ">
                                    <tr>
                                        <td width="100" class="name">公告标题</td>
                                        <td width="400"><span><a><%=entity.Bid_Title %></a></span></td>
                                        <td width="100" class="name">拍卖用户</td>
                                        <td width="375"><span><a><%=entity.Bid_MemberCompany %></a></span></td>
                                    </tr>
                                    <tr>
                                        <%-- <td class="name">报名时间</td>
                                    <td><%=entity.Bid_EnterStartTime.ToString("yyyy-MM-dd HH:mm:ss") %>至<%=entity.Bid_EnterEndTime.ToString("yyyy-MM-dd HH:mm:ss") %></td>--%>
                                        <td class="name">竞价时间</td>
                                        <td colspan="3" style="font-size: 14px;"><%=entity.Bid_BidStartTime.ToString("yyyy-MM-dd HH:mm:ss") %><span style="color: #ff6600; margin: auto 15px;">至</span><%=entity.Bid_BidEndTime.ToString("yyyy-MM-dd HH:mm:ss") %></td>
                                    </tr>
                                    <tr>
                                        <td class="name">报价轮次</td>
                                        <td><%=entity.Bid_Number %>次</td>
                                        <td class="name">保证金</td>
                                        <td><%=pub.FormatCurrency(entity.Bid_Bond) %></td>
                                    </tr>
                                    <tr>
                                        <td class="name">拍卖状态</td>
                                        <td><span><a><%=Mybid.Bid_Status(entity) %></a></span></td>
                                        <%if (entity.Bid_SupplierID > 0)
                                          {%>
                                        <td class="name">中标单位</td>
                                        <td><%=entity.Bid_SupplierCompany %></td>
                                        <%}
                                          else
                                          { %>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <%} %>
                                    </tr>
                                </table>
                                <div class="blk11_1" style="margin-top: 0px;">
                                    <h3>公告内容</h3>
                                    <div class="b11_main_1">
                                        <%=entity.Bid_Content %>
                                    </div>
                                </div>                               
                                <div class="clear"></div>
                            </div>
                        </div>

                        <div style="margin-top: 20px; border: 1px solid #ddd">
                            <h2 style="border-top: 1px solid #dddddd; border-top: none;">
                                <ul>

                                    <li id="Li3" onclick="ShowList(1);" class="on">商品清单</li>


                                    <%if ((entity.Bid_Status == 0) && (Count == 0))
                                      {%>
                                    <%-- <a href="/supplier/auction_product_add.aspx?BID=<%=BID %>" class="more4">新增商品</a>--%>
                                    <li id="a02" onclick="ShowList(0);" class="on1" style="float: right;"><a href="/supplier/auction_product_add.aspx?BID=<%=BID %>" class="more4" style="color: #ffffff">新增商品</a></li>
                                    <%} %>
                                </ul>
                            </h2>
                            <div class="b02_main" id="aa02">
                                <div class="blk14_1" style="margin-top: 0px;">


                                    <div class="b14_1_main" style="border-left: none; border-right: none;">
                                        <table width="974" border="0" cellspacing="0" cellpadding="0">
                                            <tr>


                                                <td width="100" class="name">产品名称</td>
                                                <td width="100" class="name">型号规格</td>

                                                <td width="100" class="name">计量单位</td>
                                                <td width="100" class="name">产品数量</td>
                                                <td width="100" class="name">物流信息</td>
                                                <td width="100" class="name">起标价格</td>
                                                <td width="100" class="name">备注</td>
                                                <td width="127" class="name">操作</td>
                                            </tr>
                                            <%Mybid.AuctionProductList(entity.BidProducts, entity.Bid_Status, 0); %>
                                        </table>
                                    </div>
                                </div>

                                <div class="clear"></div>
                            </div>
                        </div>


                        <div style="margin-top: 20px; border: 1px solid #ddd">
                            <h2 style="border-top: 1px solid #dddddd; border-top: none;">
                                <ul>
                                    <li id="Li2" onclick="ShowList(2);" class="on">附件列表</li>



                                    <%if (entity.Bid_Status == 0)
                                      {%>
                                    <li id="Li4" onclick="ShowList(0);" class="on1" style="float: right;"><a href="/supplier/bid_Attachments_add.aspx?BID=<%=BID %>" class="more4" style="color: #ffffff">添加附件</a></li>

                                    <%} %>
                                </ul>
                            </h2>
                            <div class="b02_main" id="aa03">

                                <div class="blk14_1" style="margin-top: 0px;">

                                    <div class="b14_1_main" style="border-left: none; border-right: none;">
                                        <table width="974" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td width="356" class="name">附件名称</td>
                                                <td class="name">说明</td>
                                                <td width="127" class="name">操作</td>
                                            </tr>
                                            <%Mybid.BidAttachmentsList(entity.BidAttachments, entity.Bid_Status, 1); %>
                                        </table>
                                    </div>
                                </div>
                                <div class="clear"></div>
                            </div>

                        </div>


                        <% if (DateTime.Compare(DateTime.Now, entity.Bid_BidEndTime) > 0 && entity.Bid_Status == 1 && entity.Bid_IsAudit == 1)
                           { %>

                        <div style="margin-top: 20px; border: 1px solid #ddd">
                            <h2 style="border-top: 1px solid #dddddd; border-top: none;">
                                <ul>

                                    <li id="Li5" onclick="ShowList(1);" class="on">报价信息</li>
                                </ul>
                            </h2>
                            <div class="b02_main" id="Div2">
                                <div class="blk14_1" style="margin-top: 0px;">
                                    <div class="b14_1_main" style="border-left: none; border-right: none;">
                                        <table width="974" border="0" cellspacing="0" cellpadding="0">

                                            <%Mybid.Tender_MemberView(BID, 1); %>
                                        </table>
                                    </div>
                                </div>

                                <div class="clear"></div>
                            </div>
                        </div>

                        <%} %>                      
                    </div>








                    <div class="b02_main">
                        <ul style="width: 975px; margin-left: 0px;">
                            <%if (entity.Bid_Status == 0)
                              {%>
                            <li style="width: 975px; margin-left: 0px;"><a href="/supplier/auction_do.aspx?action=Release&Bid_ID=<%=BID%>" class="a05" style="margin-left: 410px;">发布</a></li>
                            <%}
                              else
                              {
                                  if (entity.Bid_Status == 1 && DateTime.Compare(DateTime.Now, entity.Bid_EnterEndTime) < 0)
                                  {%>
                            <li style="width: 975px; margin-left: 0px;"><a href="/supplier/auction_do.aspx?action=Revoke&Bid_ID=<%=BID%>" class="a05" style="margin-left: 0px; float: left;">撤销</a></li>
                            <%}
                              } %>
                        </ul>
                    </div>

                </div>

            </div>
        </div>
    </div>
    <div class="clear"></div>
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
    <!--主体 结束-->


    <ucbottom:bottom runat="server" ID="Bottom" />

</body>
</html>
