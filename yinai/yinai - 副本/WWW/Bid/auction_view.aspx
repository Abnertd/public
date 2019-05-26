<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Page Language="C#" %>
<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<% 

    Session["Position"] = "Bid";
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    Bid MyBid = new Bid();
    int BID = tools.CheckInt(Request["BID"]);

    BidInfo entity = MyBid.GetBidByID(BID);
    if (entity != null)
    {
        if (entity.Bid_Type == 0 || entity.Bid_IsAudit != 1 || entity.Bid_Status != 1)
        {
            Response.Redirect("/bid/auction.aspx");
        }
    }
    else
    {
        Response.Redirect("/bid/auction.aspx");
    }
    //int Number = MyBid.SignUpNumber(entity.Bid_ID);
    int Number = MyBid.SignBidTenderNumber(entity.Bid_ID);
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%="拍卖详情 - " + pub.SEO_TITLE()%></title>
    <meta name="Keywords" content="<% = Application["Site_Keyword"]%>" />
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/1.js"></script>
    <script type="text/javascript">
        function Sign_Up(BID, number) {
            var index = layer.load(2, {
                shade: [0.4, '#FFF'] //0.1透明度的白色背景
            });
            $.ajax({
                type: "get",
                global: false,
                async: false,
                dataType: "html",
                url: encodeURI("/bid/bid_do.aspx?action=sign_up&BID=" + BID + "&timer=" + Math.random()),
                success: function (data) {
                    if (data == "success") {
                        layer.close(index);
                        alert("报名成功");
                        $("#number").html(parseInt(number) + 1);
                        window.location.href = "/member/auction_tender_sign.aspx";
                    }
                    else {
                        layer.close(index);
                        alert(data);
                    }
                },
                error: function () {
                    layer.close(index);
                    alert("Error Script");
                }
            });
        }
    </script>
    

   <!--layer弹出框.js .css引用 开始-->
 <link href="../layer.m/layer.m.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery-1.5.1.min.js" charset="gb2312"></script>
       <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js" ></script>
  <!--layer弹出框.js .css引用 结束-->
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
    <style type="text/css">
        .b11_main_1 p {
            line-height: 30px;
        }

        .blk11_1 .aa1 td {
            border-bottom: 1px solid #eeeeee;
        }

        .blk11_1 h3 ul li {
            padding: 0;
        }
        blk11_1 table tr td .alist {
           background-color: rgb(247, 247, 247); font-weight: 600; height: 14px; line-height: 14px;border-top: 1px solid #eeeeee;border-bottom: 1px solid #eeeeee;margin-top: 30px;
        }
       
    </style>
</head>
<body>
    <uctop:top runat="server" ID="HomeTop" />
    <!--主体 开始-->
    <div class="content">
        <!--位置 开始-->
        <div class="position">当前位置 > <a href="/">首页</a> > 拍卖详情</div>
        <!--位置 结束-->
        <div class="banner02">
            <img src="/images/img12.jpg" width="1200" height="156" />
        </div>
        <div class="parte" style="border: none;">
            <div class="blk10_1">
                <h2><%=entity.Bid_Title %></h2>
                <div class="b10_1_main">
                    <p>拍卖用户：<%=entity.Bid_MemberCompany %></p>
                    <p>&nbsp;</p>
                    <p>&nbsp;</p>
                </div>
                <div class="b10_1_main02" style="width: 188px;">


                    <%--          <%if(DateTime.Compare(DateTime.Now,entity.Bid_EnterEndTime)<=0) {%>
                  <a href="javascript:void(0);" onclick="Sign_Up(<%=entity.Bid_ID %>,<%=Number %>);" >立即报名</a>
                <%}
                  else if (DateTime.Compare(DateTime.Now, entity.Bid_BidEndTime) <= 0)
                  { %>
                <a href="/member/auction_tender_add.aspx?BID=<%=BID %>">立即竞价</a>
                <% }else {%>
                <a >已结束</a>
                <%} %>--%>

                    
                    <a href="javascript:void(0);" onclick="javascript:if(confirm('报价需要先缴纳<%=entity.Bid_Bond %>元保证金，您确定要立即报价吗？')==false){return false;}else{Sign_Up(<%=entity.Bid_ID %>,<%=Number %>);" style="height: 32px; width: 108px; font-size: 19px; line-height: 32px; float: right;">立即报价</a>





                    <p><span>已收到（<ii id="number"><%=Number %></ii>）条报价</span></p>
                </div>



                <div class="blk11_1" style="margin-top: 0px;">
                    <h2 style="margin-bottom: 20px;">拍卖信息</h2>
                    <table width="1050px" border="0" cellspacing="0" cellpadding="0" style="border-bottom: 0px solid #eeeeee; border: 1px solid #dddddd;">
                        <tr class="aa1">
                            <td width="200px" class="name">公告标题</td>
                            <td width="325px"><span><a><%=entity.Bid_Title %></a></span></td>
                            <td width="200px" class="name">拍卖用户</td>
                            <td width="325px"><span><a><%=entity.Bid_MemberCompany %></a></span></td>
                        </tr>
                        <tr class="aa1">
                            <%-- <td class="name">报名时间</td>
                                    <td><%=entity.Bid_EnterStartTime.ToString("yyyy-MM-dd HH:mm:ss") %>至<%=entity.Bid_EnterEndTime.ToString("yyyy-MM-dd HH:mm:ss") %></td>--%>
                            <td class="name">竞价时间</td>
                            <td colspan="3" style="font-size: 14px;"><%=entity.Bid_BidStartTime.ToString("yyyy-MM-dd HH:mm:ss") %><span style="color: #ff6600; margin: auto 15px;">至</span><%=entity.Bid_BidEndTime.ToString("yyyy-MM-dd HH:mm:ss") %></td>
                        </tr>
                        <tr class="aa1">
                            <td class="name">报价轮次</td>
                            <td><%=entity.Bid_Number %>次</td>
                            <td class="name">保证金</td>
                            <td><%=pub.FormatCurrency(entity.Bid_Bond) %></td>
                        </tr>
                        <tr class="aa1">
                            <td class="name">拍卖状态</td>
                            <td><span><a><%=MyBid.Bid_Status(entity) %></a></span></td>
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

                </div>

            </div>
            <%if (entity.Bid_ProductType == 1)
              {%>
            <div class="blk11_1" style="width: 1050px;">
                <h2>拍卖清单</h2>

                <h3 style="padding-left: 10px;">
                    <ul>
                        <li style="width: 175px;">产品名称</li>
                        <li style="width: 175px;">拍卖数量</li>
                        <li style="width: 155px;">单位</li>
                        <li style="width: 175px;">起始价格</li>

                        <li style="width: 175px;">物流信息</li>
                        <li style="width: 175px; border-right: none; text-align: center;">备注</li>
                    </ul>
                    <div class="clear"></div>
                </h3>
                <table width="1050" border="0" cellspacing="0" cellpadding="0">
                    <%MyBid.BidProdcutView(entity.BidProducts, 1); %>
                </table>

            </div>
            <%} %>
            <div class="blk11_1" style="width: 1050px;">
                <h2>公告内容</h2>
                <div class="b11_main_1">
                    <%=entity.Bid_Content %>
                </div>
            </div>
            <div class="clear"></div>
            <%if (entity.BidAttachments != null)
              {%>
            <div class="blk11_1" style="width: 1050px;">
                <h2>附件列表</h2>
                <%-- <h3 style="padding-left: 10px;">--%>
                <h3 style="padding-left: 10px;">
                    <ul>

                        <li style="width: 400px;">附件名称</li>
                        <li style="width: 400px;">说明</li>
                        <li style="width: 237px; border-right: none;">操作</li>
                    </ul>
                    <div class="clear"></div>
                </h3>
                <table width="1050px" border="0" cellspacing="0" cellpadding="0" style="margin-top: 30px;">
                   <%-- <thead>


                        <tr>
                            <td class="alist" style="background-color: rgb(247, 247, 247); font-weight: 600; height: 14px; line-height: 14px;border-top: 1px solid #eeeeee;border-bottom: 1px solid #eeeeee;margin-top: 30px;border-right:1px solid #ffffff">附件名称</td>
                            <td class="alist" style="background-color: rgb(247, 247, 247); font-weight: 600; height: 14px; line-height: 14px;border-top: 1px solid #eeeeee;border-bottom: 1px solid #eeeeee;margin-top: 30px;border-right:1px solid #ffffff">说明</td>
                            <td class="alist" style="background-color: rgb(247, 247, 247); font-weight: 600; height: 14px; line-height: 14px;border-top: 1px solid #eeeeee;border-bottom: 1px solid #eeeeee;margin-top: 30px;">操作</td>

                        </tr>
                    </thead>--%>


                    <%MyBid.BidAttachmentsView(entity.BidAttachments, 1); %>
                </table>

            </div>
            <div class="clear"></div>
            <%} %>
        </div>

    </div>


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




    <!--主体 结束-->
    <!--尾部 开始-->
    <ucbottom:bottom runat="server" ID="Bottom" />
    <!--右侧滚动 结束-->
</body>
</html>

