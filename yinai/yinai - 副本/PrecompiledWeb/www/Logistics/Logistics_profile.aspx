<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>
<% 
    
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    Logistics logistics = new Logistics();
    Session["Position"] = "";
    Supplier supplier = new Supplier();
    logistics.Logistics_Login_Check("/Logistics/Logistics_profile.aspx");
    Orders MyOrders=new Orders();
    Logistics MyLogistics = new Logistics();
    Addr addr = new Addr();
    LogisticsInfo logisticsinfo = logistics.GetLogisticsByID();
    OrdersInfo orders = null;
    if (logisticsinfo != null)
    {

        if (logisticsinfo.Logistics_Status==0)
        {
            pub.Msg("error", "错误信息", "您的账号已冻结", false, "{back}");
        }
    }
    
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="修改资料 - 物流商用户中心 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />

    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
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

    <style type="text/css">
        .zkw_19_fox img {
            vertical-align: middle;
            display: inline;
        }
    </style>
    <style type="text/css">
                .img_style {
            width: 100%;
            clear: both;
            height: auto;
            display: inline;
        }

            .img_style img {
                width: 11px;
                float: left;
                margin-top: 13px;
                margin-right: 11px;
            }
        .blk14_1 h2 span { float:right; display:inline; margin-top:7px;} 
        .blk14_1 h2 span a.a13 { background-image:url(../images/a_bg01.jpg); background-repeat:no-repeat; width:74px; height:26px; font-size:12px; font-weight:normal; text-align:center; line-height:26px; color:#333; display:inline-block; vertical-align:middle; margin-right:7px;}
    </style>
</head>
<body>

    <uctop:top ID="top1" runat="server" />


    <!--主体 开始-->
    <div class="webwrap">
        <div class="content02" style="background-color: #FFF;">

            <!--位置说明 开始-->
            <div class="position"><a href="/">首页</a> > <a href="/Logistics/Logistics.aspx">物流商</a> > <strong> 修改资料 </strong></div>
            <!--位置说明 结束-->
            <div class="partc">
                <div class="pd_left">

                    <% logistics.Get_Logistics_Left_HTML(2, 2); %>
                </div>
                <div class="pc_right">


                    <div class="blk14_1" style="margin-top: 1px;">
                              <h2>修改资料</h2>
                    <div class="b14_1_main">
                            <div class="b07_main">
                                <div class="b07_info04">
                                    <%
                                        if (tools.CheckStr(Request.QueryString["tip"]) == "success")
                                        {
                                            pub.Tip("positive", "您的资料已成功修改！");
                                    %>
                                    <table width="893" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td height="10"></td>
                                        </tr>
                                    </table>
                                    <%}%>
                                    <form name="frm_account_profile" id="frm_account_profile" method="post" action="/Logistics/Logistics_do.aspx">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="5">
                                            <tr>
                                                <td width="138" class="name">物流商公司名
                                                </td>
                                                <td width="755">
                                                    <input name="Logistics_CompanyName" type="text" style="width: 300px;" class="input01" id="Logistics_CompanyName"  value="<%=logisticsinfo.Logistics_CompanyName %>"/><span>* </span>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td class="name">联系人
                                                </td>
                                                <td>
                                                    <input name="Logistics_Name" type="text" style="width: 300px;" class="input01" id="Logistics_Name" value="<%=logisticsinfo.Logistics_Name %>"/><span>*</span>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td class="name">联系电话
                                                </td>
                                                <td>
                                                    <input name="Logistics_Tel" style="width: 300px;" class="input01" type="text" id="Logistics_Tel" value="<%=logisticsinfo.Logistics_Tel %>"/><span>*</span>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td class="name">验证码
                                                </td>
                                                <td>
                                                    <input name="verifycode" type="text" onfocus="$('#var_img').attr('src','/Public/verifycode.aspx?timer='+Math.random());"
                                                        class="txt_border" id="verifycode" /><i>*</i>
                                                    <img id="var_img" alt="看不清？换一张" title="看不清？换一张" src="/public/verifycode.aspx" onclick="this.src='../Public/verifycode.aspx?timer='+Math.random();"
                                                        style="cursor: pointer; display: inline;" align="absmiddle" />
                                                </td>
                                            </tr>

                                            <tr>
                                                <td align="right" class="t12_53">&nbsp;
                                                </td>
                                                <td>
                                                    <input name="action" type="hidden" id="action" value="profile" />
                                                    <a href="javascript:void(0);" onclick="$('#frm_account_profile').submit();" class="a11"></a>
                                                </td>
                                            </tr>
                                        </table>
                                    </form>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>

            </div>
        </div>
    </div>
    <div class="clear"></div>


    <!--主体 结束-->


  <ucbottom:bottom runat="server" ID="Bottom" />

</body>
</html>
