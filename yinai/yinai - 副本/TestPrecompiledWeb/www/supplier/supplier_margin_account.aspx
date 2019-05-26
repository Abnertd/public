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
    double margin_amount=0;
    supplier.Supplier_AuditLogin_Check("/supplier/supplier_margin_account.aspx");

    //SupplierMarginInfo entity = supplier.GetSupplierMarginByType(tools.NullInt(Session["supplier_type"]));
    SupplierMarginInfo entity = supplier.GetSupplierMarginByType(1);
    if (entity == null)
    {
        Response.Redirect("/supplier/");
    }
    else
    {
        margin_amount = entity.Supplier_Margin_Amount;
    }
%>




<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>
        <%="充值保证金 - 我是卖家 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
     <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index_newadd.css" rel="stylesheet" type="text/css" />
       <!--layer弹出框.js .css引用 开始-->
 <link href="../layer.m/layer.m.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery-1.5.1.min.js" charset="gb2312"></script>
       <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js" ></script>
  <!--layer弹出框.js .css引用 结束-->
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
     <script type="text/javascript" src="/scripts/common.js"></script>
    <script src="/scripts/jquery-extend-AdAdvance3.js" type="text/javascript"></script>
    <!--滑动门 开始-->
    <script type="text/javascript" src="/scripts/hdtab.js"></script>
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
        window.onload = function () {
            var SDmodel = new scrollDoor();
            SDmodel.sd(["c01", "c02"], ["cc01", "cc02"], "on", " ");
            //SDmodel.sd(["n01", "n02", "n03", "n04", "n05"], ["nn01", "nn02", "nn03", "nn04", "nn05"], "on", " ");
        }
    </script>
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
</head>
<body>
    <uctop:top ID="top1" runat="server" />
    <!--主体 开始-->
    <div class="webwrap">
    <div class="content02" style="margin-bottom:20px;">
    
        <!--位置说明 开始-->
       <div class="position">当前位置 > <a href="/supplier/">我是卖家</a> > 账户管理 > <strong>充值保证金</strong></div>
        <!--位置说明 结束-->
        <div class="partd_1">
            <div class="pd_left">
               
                <%supplier.Get_Supplier_Left_HTML(4, 3); %>
                 
            </div>
            <div class="pd_right">
                <div class="blk14_1" style="margin-top:0px;">
                 <h2>充值保证金</h2>
                    <div class="blk17_sz">
                        <form name="frm_account_profile" id="frm_account_profile" method="post" action="/supplier/account_do.aspx">
                        <table width="100%" border="0" cellspacing="0" cellpadding="5" class="table_padding_5">
                            <tr>
                                <td width="92" class="name">
                                    保证金金额
                                </td>
                                <td width="801">
                                    <input name="Account_Amount" type="text" class="txt_border" id="Account_Amount"
                                        size="40" maxlength="20" readonly="readonly" value="<%=entity.Supplier_Margin_Amount %>" /> 元
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    &nbsp;
                                </td>
                                <td class="tip">
                                    <div id="tip_Supplier_password">
                                    </div>
                                    保证金金额根据您注册时选择的类型进行计算得出
                                </td>
                            </tr>
                            
                            <tr>
                                <td width="92" class="name">
                                    支付方式：
                                </td>
                                <td width="801">
                                    <input type="radio" name="pay_type" value="YEEPAY" checked="checked" style="margin-top: 10px;float: left; margin-right: 11px;display: inline-block;vertical-align: middle; padding: 0;border: none; -webkit-box-shadow: none;" /><img alt="易宝" title="易宝" src="/images/YeePay.jpg" width="110" height="35" />
                                </td>
                            </tr>

                            <tr>
                                <td width="92" class="name">
                                    验证码
                                </td>
                                <td width="801">
                                    <input name="verifycode" type="text" onfocus="$('#var_img').attr('src','/Public/verifycode.aspx?timer='+Math.random());"
                                        class="txt_border" id="verifycode" size="10" maxlength="10" /><i>*</i>
                                    <img id="var_img" alt="看不清？换一张" title="看不清？换一张" src="/public/verifycode.aspx" onclick="this.src='../Public/verifycode.aspx?timer='+Math.random();"
                                        style="cursor: pointer; display: inline;" align="absmiddle" />
                                </td>
                            </tr>
                            
                            <tr>
                                <td width="92" class="name">
                                    &nbsp;
                                </td>
                                <td>
                                    <span class="table_v_title">
                                        <input name="action" type="hidden" id="action" value="account_add" />
                                        <input name="Account_Type" type="hidden" id="Account_Type" value="1" />
                                        <a href="javascript:void();" onclick="$('#frm_account_profile').submit();" class="a11"></a>
                                    </span>
                                </td>
                            </tr>
                        </table>
                        </form>
                    </div>
                </div>
                </div>
            </div>
            <div class="clear">
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
