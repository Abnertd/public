<%@ Page Language="C#" %>

<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<% 
    Session["Cur_Position"] = Session["Home_Position"] = "";
    Session["Position"] = "";
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    Member member = new Member();
    Bid Mybid = new Bid();
    int BID = tools.CheckInt(Request["BID"]);
    member.Member_Login_Check("/member/Bid_view.aspx?BID=" + BID);
    BidInfo entity = Mybid.GetBidByID(BID);
    int list = tools.CheckInt(Request["list"]);
    int supplier = 0;
    if (entity != null)
    {
        if (entity.Bid_MemberID != tools.NullInt(Session["member_id"]) || entity.Bid_MemberID == 0)
        {
            Response.Redirect("/member/bid_list.aspx");
        }
        if (entity.Bid_Type == 1)
        {
            Response.Redirect("/member/bid_list.aspx");
        }
    }
    else
    {
        Response.Redirect("/member/bid_list.aspx");
    }
%>


<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%="招标详情 - 我是买家 - " + pub.SEO_TITLE()%></title>
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
    <script src="/scripts/hdtab.js"></script>
    <script src="/scripts/1.js"></script>
    <script type="text/javascript" src="/scripts/common.js"></script>
    <script type="text/javascript" src="/Public/kindeditor/kindeditor-all.js"></script>
    <script type="text/javascript" src="/Public/kindeditor/lang/zh_CN.js"></script>
    <link href="/scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
    <script src="/scripts/jquery-ui/jquery-ui.js"></script>
    <script src="/scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js"></script>
    <script>
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
    <style>
        .blk11_1 h3 {
            background-color: #f7f7f7;
            border-bottom: 1px solid #dddddd;
            border-top: 1px solid #dddddd;
            /*padding: 10px 0 10px 10px;*/
            display: block;
            margin-top: 30px;
            /*margin-left: 50px;*/
            width: 975px;
        }

        .b11_main_1 {
            padding: 20px 0 0 50px;
        }

         .layui-anim{ top:250px !important}


        .blk02 h2 ul li.on1 {
            background-color: #ff6600;
            height: 21px;
            line-height: 32px;
            margin-right: 5px;
            margin-top: 10px;
            width: 78px;
            border-radius: 12px;
            color: #ffffff;
            text-align: center;
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

        .blk02 h2 ul li {
            color: #333;
            cursor: pointer;
            display: inline;
            float: left;
            font-size: 16px;
            font-weight: normal;
            height: 40px;
            line-height: 50px;
            padding: 0 0 10px;
            position: relative;
            text-align: center;
            width: 195px;
        }

        .b02_main {
            padding: 0;
        }

        .b14_1_main {
            border: none;
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
                font-weight: 600;
            }
    </style>
</head>
<body>
    <uctop:top runat="server" ID="HomeTop" />

    <div class="webwrap">
        <div class="content02" style="margin-bottom: 20px;">
            <div class="position">当前位置 > <a href="/member/index.aspx">我是买家</a> > <a href="/member/bid_list.aspx">招标列表</a> ><span>招标详情</span></div>
            <div class="partd_1">
                <div class="pd_left">
                    <% =member.Member_Left_HTML(1, 3) %>
                </div>
                <div class="pc_right">

                    <div class="blk02" style="border:none;">
                         <div  style="border:1px solid #ddd">
                        <h2>
                            <ul>
                                <li id="a01" onclick="ShowList(0);" class="on">招标信息</li>  
                                 <%if (entity.Bid_Status == 0)
                                   {%>
                                  <li id="a02" onclick="ShowList(0);" class="on1" style="float: right;text-align:center;"><a href="/member/bid_edit.aspx?BID=<%=BID%>" class="a02" style="color:#ffffff;text-align:center;">修改</a></li>
                                    <%}
                                   else
                                   {
                                       if (entity.Bid_Status == 1 && DateTime.Compare(DateTime.Now, entity.Bid_EnterEndTime) < 0)
                                       {%>
                                   <li id="a02" onclick="ShowList(0);" class="on1" style="float: right;text-align:center "><a href="/member/bid_do.aspx?action=Revoke&Bid_ID=<%=BID%>" class="a02" style="float: left;text-align:center;">撤销</a></li>
                                    <%}
                                      } %>                           
                            </ul>
                        </h2>

                     <%--   <div  style="border:1px solid #ddd">--%>
                        <div class="b14_1_main" id="aa01" style="border: 0px solid #dddddd;">


                            <table width="975" border="0" cellspacing="0" cellpadding="0" style="border-bottom: 0px solid #eeeeee; ">
                                <tr>
                                    <td width="100" class="name">公告标题</td>
                                    <td width="400"><span><a><%=entity.Bid_Title %></a></span></td>
                                    <td width="100" class="name">采购商</td>
                                    <td width="300"><span><a><%=entity.Bid_MemberCompany %></a></span></td>
                                </tr>
                                <tr>
                                    <%-- <td class="name">报名时间</td>
                                    <td><%=entity.Bid_EnterStartTime.ToString("yyyy-MM-dd HH:mm:ss") %>至<%=entity.Bid_EnterEndTime.ToString("yyyy-MM-dd HH:mm:ss") %></td>--%>
                                    <td class="name">报价时间</td>
                                    <%-- <td colspan="3" style="font-size:14px;"><%=entity.Bid_BidStartTime.ToString("yyyy-MM-dd HH:mm:ss")%><span style="color:#ff6600 ;margin:auto 15px;">至</span>   <%=entity.Bid_BidEndTime.ToString("yyyy-MM-dd HH:mm:ss") %></td>--%>
                                    <td style="font-size: 14px;"><%=entity.Bid_BidStartTime.ToString("yyyy-MM-dd")%><span style="color: #ff6600; margin: auto 15px;">至</span>   <%=entity.Bid_BidEndTime.ToString("yyyy-MM-dd") %></td>
                                    <td class="name">招标状态</td>
                                    <td><span><a><%=Mybid.Bid_Status(entity) %></a></span></td>
                                </tr>
                                <tr>
                                    <td class="name">报价轮次</td>
                                    <td><%=entity.Bid_Number %>次</td>
                                    <td class="name">保证金</td>
                                    <td><%=pub.FormatCurrency(entity.Bid_Bond) %></td>
                                </tr>
                                <%--  <tr>
                                   <td class="name">产品清单</td>
                                    <td><%if (entity.Bid_ProductType == 0) { Response.Write("不展示"); } else { Response.Write("展示"); } %></td>
                                    <td class="name">招标状态</td>
                                    <td><span><a><%=Mybid.Bid_Status(entity) %></a></span></td>
                                </tr>--%>
                                <tr>
                                    <td class="name">交货时间</td>
                                    <td><span><a><%=entity.Bid_DeliveryTime.ToString("yyyy-MM-dd") %></a></span></td>

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
                  <%--      </div> 
                      --%>  
                        </div>
                        
                          <div  style="border:1px solid #ddd;margin-top:20px;">
                               <h2 style="border-top: 1px solid #dddddd;border-top:none;">
                                <ul>
                                    <%-- <li id="Li2" onclick="ShowList(0);">拍卖信息</li>--%>
                                    <li id="Li3" onclick="ShowList(1);" class="on">商品清单</li>
                                    <%--   <li id="Li4" onclick="ShowList(2);">附件列表</li>--%>


                                    <%if (entity.Bid_Status == 0)
                                      {%>
                                <li id="a02" onclick="ShowList(0);" class="on1" style="float: right;"> <a href="/member/bid_product_add.aspx?BID=<%=BID %>"  class="more4" style="color: #ffffff">新增商品</a></li>
                                <%} %>


                                </ul>
                            </h2>
                        <div class="b02_main" id="aa02" ">
                            <div class="blk14_1" style="margin-top: 0px;">
                             <div class="b14_1_main" style="border-left:none;border-right:none;">
                                    <table width="974" border="0" cellspacing="0" cellpadding="0">
                                        <tr>

                                            <%-- <td width="100" class="name">物料编号</td>--%>
                                            <td width="100" class="name">产品名称</td>
                                            <td width="100" class="name">型号规格</td>
                                            <%--  <td width="100" class="name">品牌</td>--%>
                                            <td width="100" class="name">计量单位</td>
                                            <td width="100" class="name">采购数量</td>
                                            <td width="100" class="name">物流信息</td>
                                            <td width="100" class="name">备注</td>
                                            <td width="127" class="name">操作</td>
                                        </tr>
                                        <%Mybid.BidProductList(entity.BidProducts, entity.Bid_Status, 0); %>
                                    </table>

                                </div>
                                
                            </div>

                            <div class="clear"></div>
                        </div>
                        </div> 
                        
                        
                        
                        
                        
                         <div  style="border:1px solid #ddd;margin-top:20px;">
                                <h2 style="border-top: 1px solid #dddddd;border-top:none;">
                                <ul>
                                    <li id="Li2" onclick="ShowList(2);" class="on">附件列表</li>


                                     <%if (entity.Bid_Status == 0)
                                       {%>
                               <li id="Li4" onclick="ShowList(0);" class="on1" style="float: right;">   <a href="/member/bid_Attachments_add.aspx?BID=<%=BID %>" class="more4" style="color: #ffffff">添加附件</a></li>
                                <%} %>


                                </ul>
                            </h2>
                        <div class="b02_main" id="aa03" >
                            <div class="blk14_1" style="margin-top: 0px;">
                              <div class="b14_1_main" style="border-left:none;border-right:none;">
                                    <table width="974" border="0" cellspacing="0" cellpadding="0">
                                        <tr>

                                            <td width="356" class="name">附件名称</td>
                                            <td class="name">说明</td>
                                            <td width="127" class="name">操作</td>
                                        </tr>
                                        <%Mybid.BidAttachmentsList(entity.BidAttachments, entity.Bid_Status, 0); %>
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
                                               <%Mybid.Tender_MemberView(BID, 0); %>
                                       
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
                                    <li><a href="/member/bid_do.aspx?action=Release&Bid_ID=<%=BID%>" class="a05" style="text-align:center;margin-left:290px;">发布</a></li>
                                    <%}
                                      else
                                      {
                                          if (entity.Bid_Status == 1 && DateTime.Compare(DateTime.Now, entity.Bid_EnterEndTime) < 0)
                                          {%>
                                    <li><a href="/member/bid_do.aspx?action=Revoke&Bid_ID=<%=BID%>" class="a05" style="float: left;">撤销</a></li>
                                    <%}
                                      } %>
                                    <%if (Mybid.BidOrderStatus(BID, ref supplier))
                                      {%>
                                    <li><a href="/cart/Bid_confirm.aspx?BID=<%=BID%>" class="a05" style="float: left;">生成订单</a></li>
                                    <%} %>
                                </ul>
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
    <ucbottom:bottom runat="server" ID="Bottom" />
</body>
</html>
