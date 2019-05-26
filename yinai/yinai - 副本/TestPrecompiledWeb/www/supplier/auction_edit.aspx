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
    Supplier supplier = new Supplier();
    int BID = tools.CheckInt(Request["BID"]);
    supplier.Supplier_Login_Check("/supplier/auction_edit.aspx?BID=" + BID);

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
    Bid Mybid = new Bid();
    BidInfo entity = Mybid.GetBidByID(BID);
    if (entity != null)
    {
        if (entity.Bid_MemberID != tools.NullInt(Session["member_id"]) || entity.Bid_MemberID == 0)
        {
            Response.Redirect("/supplier/auction_list.aspx");
        }
        if (entity.Bid_Status > 0)
        {
            Response.Redirect("/supplier/auction_list.aspx");
        }

        if (entity.Bid_Type == 0)
        {
            Response.Redirect("/supplier/auction_list.aspx");
        }
    }
    else
    {
        Response.Redirect("/supplier/auction_list.aspx");
    }
    DateTime Today = DateTime.Now;
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="修改拍卖 - 我是卖家 - " + pub.SEO_TITLE()%></title>
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

    <link type="text/css" href="/scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
    <script src="/scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
    <script src="/scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>
        <link href="/scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery-ui/jquery-ui.js"></script>
    <script type="text/javascript" src="/scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js"></script>
    <script type="text/javascript" src="/Public/kindeditor/kindeditor-all.js"></script>
    <script type="text/javascript" src="/Public/kindeditor/lang/zh_CN.js"></script>
    <script type="text/javascript" src="/scripts/My97DatePicker/WdatePicker.js"></script>
    <style type="text/css">
        .zkw_19_fox img {
            vertical-align: middle;
            display: inline;
        }
    </style>
    
 
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
    <div class="content02" style="background-color: #FFF;">
     
            <!--位置说明 开始-->
            <div class="position">当前位置 > <a href="/supplier/">我是卖家</a> > 投标拍卖管理 > <strong>修改拍卖</strong></div>
            <!--位置说明 结束-->
            <div class="partc">
                <div class="pd_left">
               
                        <% supplier.Get_Supplier_Left_HTML(5, 4); %>
                  
                </div>
                <div class="pc_right">

                        

                    <div class="blk14_1" style="margin-top: 1px;">
                              <h2>修改拍卖</h2>
                    
                            <div class="b14_1_main" id="aa02" >
                        <form name="frm_bid" id="frm_bid" method="post" action="/supplier/auction_do.aspx">
                        <div class="b02_main">
                            <ul style="width:850px;">
                                <li><span><i>*</i>公告标题：</span><label><input name="Bid_Title" id="Bid_Title" type="text" style="width: 298px;" value="<%=entity.Bid_Title %>" /></label></li>
                                <div class="clear"></div>
                                <li><span><i>*</i>拍卖用户：</span><label><input name="Bid_MemberCompany" id="Bid_MemberCompany" type="text" style="width: 298px;" value="<%=entity.Bid_MemberCompany %>" /></label></li>
                                <div class="clear"></div>

                               <%-- <li><span><i>*</i>报名时间：</span><label><input name="Bid_EnterStartTime" id="Bid_EnterStartTime" type="text" value="<%=entity.Bid_EnterStartTime.ToString("yyyy-MM-dd HH:mm:ss") %>" readonly="readonly" style="width: 158px;" onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm:ss' });" />-<input name="Bid_EnterEndTime" id="Bid_EnterEndTime" type="text"value="<%=entity.Bid_EnterEndTime.ToString("yyyy-MM-dd HH:mm:ss") %>" readonly="readonly" style="width: 158px;" onclick="    WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm:ss' });" /></label></li>

                                <div class="clear"></div>--%>
                                <li><span><i>*</i>报价时间：</span><label><input name="Bid_BidStartTime" id="Bid_BidStartTime" type="text" value="<%=entity.Bid_BidStartTime.ToString("yyyy-MM-dd HH:mm:ss") %>" readonly="readonly" style="width: 158px;" onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm:ss' });" />-<input name="Bid_BidEndTime" id="Bid_BidEndTime" type="text" value="<%=entity.Bid_BidEndTime.ToString("yyyy-MM-dd HH:mm:ss") %>" readonly="readonly" style="width: 158px;" onclick="    WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm:ss' });" /></label></li>

                                <div class="clear"></div>
                                <li><span><i>*</i>竞价轮次：</span><label><input name="Bid_Number" id="Bid_Number" type="text" value="<%=entity.Bid_Number %>" style="width: 138px;" />&nbsp;次</label></li>
                                <div class="clear"></div>
                                <li><span><i>*</i>保证金：</span><label><input name="Bid_Bond" id="Bid_Bond" type="text" value="<%=entity.Bid_Bond %>" style="width: 138px;" />&nbsp;元</label></li>
                                <div class="clear"></div>

                                <li><span><i>*</i>公告内容：</span>
                                </li>
                                <textarea  id="Bid_Content" name="Bid_Content" rows="80" cols="16"><%=entity.Bid_Content %></textarea>
                                    <script type="text/javascript">
                                        var Bid_ContentEditor;
                                        KindEditor.ready(function (K) {
                                            Bid_ContentEditor = K.create('#Bid_Content', {
                                                width: '100%',
                                                height: '500px',
                                                filterMode: false,
                                                afterBlur: function () { this.sync(); }

                                            });
                                        });
                                </script>
                                <li><a href="javascript:void(0);" onclick="$('#frm_bid').submit();"  class="a05">保存</a></li>
                            </ul>
                            
                            <div class="clear"></div>
                        </div>
                            <input name="action" type="hidden" id="action" value="editauction" />
                            <input name="Bid_ID" type="hidden" id="Bid_ID" value="<%=entity.Bid_ID %>" />
                            <input name="Bid_DeliveryTime" id="Bid_DeliveryTime" type="hidden" value="<%=Today.ToString("yyyy-MM-dd") %>" />
                            <input name="Bid_ProductType" id="Bid_ProductType" type="hidden" value="1"/>
                            </form>
                                </div>
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
