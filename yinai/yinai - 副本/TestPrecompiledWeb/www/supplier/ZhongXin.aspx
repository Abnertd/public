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

    //supplier.Supplier_AuditLogin_Check("/supplier/ZhongXin.aspx");
    
    //supplier.CheckBelongsType(new BelongsTypeEnum[] { BelongsTypeEnum.Finance });
    
    SupplierInfo supplierinfo = supplier.GetSupplierByID();
    if (supplierinfo == null)
    {
        Response.Redirect("/supplier/index.aspx");
    }
    else
    {
        //if (supplierinfo.Supplier_Type == 1)
        //{
        //    if (supplierinfo.Supplier_SQSISActive == 0)//如果买家没有通过授权书审核
        //    {
        //        Response.Redirect("/supplier/shouquanshu.aspx");
        //    }
        //}
    }

    ZhongXin ZhongXinApp = new ZhongXin();
    
    ZhongXinInfo zhongxininfo = ZhongXinApp.GetZhongXinBySuppleir(tools.NullInt(Session["supplier_id"]));
    
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="中信支付管理 - 我是卖家 - " + pub.SEO_TITLE()%></title>
    <meta name="Keywords" content="<% = Application["Site_Keyword"]%>" />
    <meta name="Description" content="<%=Application["Site_Description"]%>" />


        <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
     <link href="/css/index_newadd.css" rel="stylesheet" type="text/css" />

    <link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script type="text/javascript" src="/scripts/common.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script src="/scripts/jquery-extend-AdAdvance3.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>
    <style type="text/css">
        .txt_border { height: 25px; }
        .a111 { line-height:26px; padding:0px 10px; display:inline-block; background-color:#ff6600; color:#fff; border:1px solid #ff6600;  -moz-border-radius: 3px; border-radius: 3px; font-size:12px; font-weight:bold; cursor:pointer;   }
        .a111:hover { background-color:#ff6600;  color:#ff6600;  border:1px solid #ff6600;  text-decoration:none;  } 
        
.t12_red {
    color: #f00;
    font-size: 12px;
}
        .img_style img{
        display:inline;}
        
.b14_1_main table td .img_style {
    /*border-bottom: 1px solid #eeeeee;*/
    color: #666;
    font-size: 12px;
    font-weight: normal;
    line-height: 36px;
    padding: 10px 0;
    text-align: center;
}


        .blk17_sz {
           border: none; 
    margin-top: 15px;
    padding: 0; 
        }
    </style>
</head>
<body>
    <uctop:top ID="top1" runat="server" />
    <!--主体 开始-->
    <div class="content">
        <!--位置说明 开始-->
        <div class="position">
            您现在的位置 > <a href="/">首页</a> > <a href="/supplier/index.aspx">我是卖家</a> > <span>中信支付管理</span>
        </div>
        <div class="clear">
        </div>
        <!--位置说明 结束-->
        <div class="partd_1">
            <div class="pd_left">
                <% supplier.Get_Supplier_Left_HTML(7, 1); %>
            </div>
            <div class="pd_right" >
                   <div class="blk14_1" style="margin-top: 0px;">
                    <h2>中信支付管理</h2>
                      <%if (zhongxininfo != null)//信息不为null的时候显示已添加信息，否则显示添加信息
                      { %>
                    <div class="b14_1_main">
                        <%
                            if (tools.CheckStr(Request.QueryString["tip"]) == "success")
                            {
                                pub.Tip("positive1", "您的资料已保存！");
                            }%>
                        <div class="blk17_sz"  style="margin-top: 0px;">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td height="10">
                                </td>
                            </tr>
                        </table>
                        <table width="100%" border="0" cellspacing="0" cellpadding="5" class="table_padding_5">
                            <tr>
                                <td width="100" align="right" class="t12_53">
                                    附属账户名
                                </td>
                                <td align="left">
                                    <%=zhongxininfo.CompanyName%>
                                </td>
                            </tr>
                            <tr>
                                <td width="100" align="right" class="t12_53">
                                    附属账户开户行
                                </td>
                                <td align="left">中信银行北京天桥支行
                                </td>
                            </tr>
                            <tr>
                                <td width="100" align="right" class="t12_53">
                                    中信附属账号
                                </td>
                                <td align="left">
                                    <%=zhongxininfo.SubAccount%>
                                </td>
                            </tr>
                            
                            <tr>
                                <td align="right" class="t12_53">
                                   附属账号余额
                                </td>
                                <td align="left">
                                    <%=ZhongXinApp.GetAmount(zhongxininfo.SubAccount)%>
                                </td>
                            </tr>
                            
                            <tr>
                                <td width="100" align="right" class="t12_53">
                                    出金收款银行
                                </td>
                                <td align="left">
                                    <%=zhongxininfo.ReceiptBank%>
                                </td>
                            </tr>
                            <tr>
                                <td width="100" align="right" class="t12_53">
                                    出金银行名称
                                </td>
                                <td align="left">
                                     <%=zhongxininfo.BankName %>
                                </td>
                            </tr>
                            <tr>
                                <td width="100" align="right" class="t12_53">
                                    出金银行行号
                                </td>
                                <td align="left">
                                    <%=zhongxininfo.BankCode %>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="t12_53">
                                    出金开户名
                                </td>
                                <td align="left">
                                    <%=zhongxininfo.OpenAccountName%>
                                </td>
                            </tr>
                            <tr>
                                <td width="100" align="right" class="t12_53">
                                    出金收款账号
                                </td>
                                <td align="left">
                                    <%=zhongxininfo.ReceiptAccount%>
                                </td>
                            </tr>
                            
                            
                            
                            
                            <tr>
                              <%--  <td align="right" class="t12_53"></td>--%>
                                <td align="left" colspan="2">
                                    <a href="zhongxin_withdraw.aspx" class="a111" style="color:#fff">申请出金</a>
                                    <a href="zhongxin_detail.aspx" class="a111" style="color:#fff">查询明细</a>
                                </td>
                            </tr>
                        </table>
                            </div>
                    </div>
                    <%}
                      else
                      { %>
                      <span style="font-size:14px; color:Red; margin-top:10px;padding-top:10px;">注意：在填写本信息前，确认您已经在银行（不限于中信银行）以营业执照单位名称开户的银行账号作为出金账户。</span>
                    <div class="b14_1_main">
                        <form name="frm_account_profile" method="post" action="/supplier/account_do.aspx">
                        <table width="100%" border="0" cellspacing="0" cellpadding="5" class="table_padding_5">
                            <tr>
                                <td width="100" align="right" class="t12_53">
                                    单位名称
                                </td>
                                  <td  style="text-align:left">
                                    <%=supplierinfo.Supplier_CompanyName %>
                                    <input name="CompanyName" id="CompanyName" value="<%=supplierinfo.Supplier_CompanyName %>" type="hidden" />
                                    <%--<span class="t12_red">* </span>请填写单位名称，单位名称应与营业执照保持一致--%>
                                </td>
                            </tr>
                            
                            <tr>
                                <td width="100" align="right" class="t12_53">
                                    出金开户银行
                                </td>
                               <td  style="text-align:left">
                                    <input type="radio" name="ReceiptBank" id="ReceiptBank" style="margin:0;" value="中信银行" checked="checked" />中信银行
                                    <input type="radio" name="ReceiptBank" id="ReceiptBank1" style="margin:0;" value="其他银行" />其他银行
                                    <span class="t12_red">* </span>请选择您的出金开户银行
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="line-height: 24px;" class="t12_53">
                                    出金银行名称
                                </td>
                                <td  style="text-align:left">
                                    <input name="BankName" id="BankName" class="txt_border" size="20" maxlength="100" />
                                    <span class="t12_red">* </span>出金收款银行名称为“银行全称+城市名称+支行名称”，如中国工商银行北京中关村支行。<a href="https://www.hebbank.com/corporbank/otherBankQueryWeb.do" style="color: Blue" target="_blank"><u>查询银行名称</u></a>
                                </td>
                            </tr>
                                 <tr>
                                <td align="right" class="t12_53">
                                    出金银行行号
                                </td>
                                    <td  style="text-align:left">
                                    <input name="BankCode" id="BankCode" class="txt_border" size="20" maxlength="12" />
                                    <span class="t12_red">* </span>出金银行行号为12位数字。<a href="https://www.hebbank.com/corporbank/otherBankQueryWeb.do" style="color: Blue" target="_blank"><u>查询行号</u></a>
                                </td>
                            </tr>
                            
                            <tr>
                                <td align="right" class="t12_53">
                                    银行开户名称
                                </td>
                                  <td  style="text-align:left">
                                    <%=supplierinfo.Supplier_CompanyName%>
                                    <input name="OpenAccountName" id="OpenAccountName" value="<%=supplierinfo.Supplier_CompanyName %>" type="hidden" />
                                    <%--<span class="t12_red">*</span> 请填写出金账号的开户名称，与营业执照单位名称必须一致--%>
                                </td>
                            </tr>
                            <tr>
                                <td width="100" align="right" class="t12_53">
                                    出金银行账号
                                </td>
                                   <td  style="text-align:left">
                                    <input name="ReceiptAccount" id="ReceiptAccount" class="txt_border" size="20" maxlength="50" />
                                    <span class="t12_red">* </span>请填写出金收款的银行账号
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="t12_53">
                                </td>
                                <td>
                                    <input name="action" type="hidden" value="zhongxin" />
                                    <input name="btn_submit" type="image" style="text-align:left;" src="/images/save_buttom.jpg" />
                                </td>
                            </tr>
                        </table>
                        </form>
                    </div>
                    <%} %>
                </div>
                <div style="padding:20px; line-height:28px;">
                <b>相关说明及下载</b>
                <ul>
                <li>1、<a href="https://www.hebbank.com/corporbank/otherBankQueryWeb.do" style="color: Blue" target="_blank"><u>开户银行名称及行号查询</u></a></li>
                <li>2、必读：<a href="http://img.ztwzsc.com/attachment/zhongxin.doc" style="color: Blue" target="_blank"><u>中信支付操作文档！！！</u></a></li>
                <li>3、入金时，请向附属账号进行转账汇款入金</li>
                        
                </ul>
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
    <ucbottom:bottom ID="bottom1" runat="server" />
</body>
</html>
