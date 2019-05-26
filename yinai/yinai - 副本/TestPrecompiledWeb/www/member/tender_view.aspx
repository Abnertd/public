<%@ Page Language="C#" %>

<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<% 
    Session["Cur_Position"] = Session["Home_Position"] = "";
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    Member member = new Member();
    Bid Mybid = new Bid();
    int TenderID = tools.CheckInt(Request["TenderID"]);
    int BID = tools.CheckInt(Request["BID"]);
    member.Member_Login_Check("/member/tender_view.aspx?BID=" + BID + "&TenderID=" + TenderID);


    
    
    TenderInfo Tender = Mybid.GetTenderByID(TenderID);
    BidInfo entity = null;
    int GetBidTimes = 0;
    if (Tender != null)
    {
        if (BID != Tender.Tender_BidID)
        {
            Response.Redirect("/member/bid_list.aspx");
        }
        entity = Mybid.GetBidByID(Tender.Tender_BidID);
        
        
         GetBidTimes = Mybid.GetBidTimes(Tender.Tender_BidID, Tender.Tender_SupplierID);
    }
    else
    {
        Response.Redirect("/member/bid_list.aspx");
    }

    if (entity != null)
    {
        if (entity.Bid_MemberID != tools.NullInt(Session["member_id"]) || entity.Bid_MemberID == 0)
        {
            Response.Redirect("/member/bid_list.aspx");
        }

        if (entity.Bid_IsAudit != 1 || entity.Bid_Status != 1 || entity.Bid_Type == 1)
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
    <title><%="报价详情 - 我是买家 - " + pub.SEO_TITLE()%></title>
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
    <uctop:top runat="server" ID="HomeTop" />

    <div class="webwrap">
        <div class="content02" style="margin-bottom: 20px;">
            <div class="position">当前位置 > <a href="/member/index.aspx">我是买家</a> > <a href="/member/bid_list.aspx">招标列表</a> ><span>报价详情</span></div>
            <div class="partd_1">
                <div class="pd_left">
                    <%=member.Member_Left_HTML(2, 2) %>
                </div>
                <div class="pd_right">

                    <div class="blk14_1" style="margin-top:1px;">
                       
                        <h2>报价详情</h2>
                        <div class="main">
                            <div class="b14_1_main">
                                <table width="973" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        
                                        <td width="100" class="name">供应商</td>
                                        <td width="300"><span><a><%=Mybid.Supplier_CompanyName(Tender.Tender_SupplierID) %></a></span></td>
                                  
                                        <td width="100" class="name">报价轮次</td>
                                        <td width="400"><span><a><%=GetBidTimes %></a>次</span></td>
                                          </tr>
                                  <%--  <tr>
                                        <td class="name">报价总金额</td>
                                        <td><span><a><%=pub.FormatCurrency(Tender.Tender_AllPrice) %></a></span></td>
                                        <td class="name">报价时间</td>
                                        <td><%=Tender.Tender_Addtime.ToString("yyyy-MM-dd HH:mm") %></td>
                                    </tr>
                                    <tr>
                                        <td class="name">是否中标</td>
                                        <td><span><a><%=Mybid.IsWin(Tender.Tender_Status,Tender.Tender_IsWin) %></a></span></td>
                                        <td></td>
                                        <td></td>
                                    </tr>--%>
                                </table>


                                <h3 style="padding: 10px;">报价清单</h3>
                                <table width="973" border="0" cellspacing="0" cellpadding="0" style="border: 1px solid #eeeeee;">
                                    <tr>
                                        <%--  <td width="120" class="name">物料编号</td>--%>
                                        <td width="120" class="name">产品名称</td>
                                        <td width="200" class="name">型号规格</td>
                                        <%--  <td width="80" class="name">品牌</td>--%>
                                        <td width="80" class="name">计量单位</td>
                                        <td width="80" class="name">采购数量</td>
                                        <td width="80" class="name">单价报价</td>
                                        <%if (Tender.Tender_IsWin == 1)
                                          {%>
                                        <td width="200" class="name">报价商品</td>
                                        <%} %>
                                    </tr>
                                    <%Mybid.TenderProductList(entity.BidProducts, Tender.TenderProducts, Tender.Tender_IsWin); %>
                                </table>
                            </div>

                            <%if (entity.Bid_SupplierID == 0)
                              {%>
                            <div class="b02_main">
                                <form name="frm_bid" id="frm_bid" method="post" action="/member/bid_do.aspx">
                                    <ul style="width: 850px;">
                                        <li><a class="a05" href="javascript:void(0);" onclick="$('#frm_bid').submit();">中标</a></li>
                                    </ul>
                                    <input name="action" type="hidden" id="action" value="winadd" />
                                    <input name="BidID" type="hidden" id="BidID" value="<%=entity.Bid_ID %>" />
                                    <input name="TenderID" type="hidden" id="TenderID" value="<%=Tender.Tender_ID %>" />
                                </form>
                            </div>
                            <div class="clear"></div>
                            <%} %>
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
    <ucbottom:bottom runat="server" ID="Bottom" />
</body>
</html>
