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
    int LogisticsID = tools.CheckInt(Request["LogisticsID"]);
    Supplier supplier = new Supplier();
    logistics.Logistics_Login_Check("/Logistics/Logistics_Tender_Add.aspx?LogisticsID=" + LogisticsID);
    Orders MyOrders=new Orders();
    Logistics MyLogistics = new Logistics();
    Addr addr = new Addr();
    LogisticsInfo logisticsinfo = logistics.GetLogisticsByID();
    if (logisticsinfo != null)
    {

        if (logisticsinfo.Logistics_Status==0)
        {
            pub.Msg("error", "错误信息", "您的账号已冻结", false, "{back}");
        }
        if (logistics.Check_Logistics(logisticsinfo.Logistics_ID, LogisticsID))
        {
            pub.Msg("error", "错误信息", "您已报价", false, "/Logistics/Logistics.aspx");
        }
    }
    SupplierLogisticsInfo entity = MyLogistics.GetSupplierLogisticsByID(LogisticsID);
    if (entity != null)
    {
        if(entity.Supplier_LogisticsID>0)
        {
            pub.Msg("error", "错误信息", "已结束", false, "/Logistics/Logistics.aspx");
        }
    }
    else
    {
        Response.Redirect("/Logistics/Logistics.aspx");
    }
    
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="物流报价 - 物流商用户中心 - " + pub.SEO_TITLE()%></title>
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

    <style type="text/css">
        .zkw_19_fox img {
            vertical-align: middle;
            display: inline;
        }
    </style>
    <style type="text/css">
        #mask_div {
            display: none;
            position: absolute;
            z-index: 1001;
            height: 100%;
            width: 100%;
            left: 0px;
            overflow-y: hidden;
            background: #000000;
            filter: Alpha(opacity=30);
            opacity: 0.3;
        }

        #html-div {
            z-index: 9999;
            width: 602px;
            background-color: #ffffff;
            position: absolute;
            left: 50%;
            top: 50%;
            margin: 0px auto;
            padding: 0px;
        }
        .blk14_1 h2 span { float:right; display:inline; margin-top:7px;} 
        .blk14_1 h2 span a.a13 { background-image:url(../images/a_bg01.jpg); background-repeat:no-repeat; width:74px; height:26px; font-size:12px; font-weight:normal; text-align:center; line-height:26px; color:#333; display:inline-block; vertical-align:middle; margin-right:7px;}
    </style>
</head>
<body>
    <div id="dialog-message" title="" style="display: none;">
    </div>
    <div id="html-div" title="" style="display: none;">
    </div>
    <div id="mask_div">
    </div>
    <uctop:top ID="top1" runat="server" />


    <!--主体 开始-->
    <div class="webwrap">
        <div class="content02" style="background-color: #FFF;">

            <!--位置说明 开始-->
            <div class="position"><a href="/">首页</a> > <a href="/Logistics/Logistics.aspx">物流商</a> > 物流列表 > <strong>物流报价</strong></div>
            <!--位置说明 结束-->
            <div class="partc">
                <div class="pd_left">

                    <% logistics.Get_Logistics_Left_HTML(1, 1); %>
                </div>
                <div class="pc_right">


                    <div class="blk14_1" style="margin-top: 1px;">
                              <h2>物流报价</h2>
                    
                    <div class="main">
                        <div class="b14_1_main">
                            <table width="973" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td width="100" class="name">货物名称</td>
                                    <td width="400"><span><a><%=entity.Supplier_Logistics_Name %></a></span></td>
                                    <td width="100" class="name">到货时间</td>
                                    <td width="300"><span><a><%=entity.Supplier_Logistics_DeliveryTime.ToString("yyyy-MM-dd") %></a></span></td>
                                </tr>
                                <tr>
                                    <td class="name">发货地</td>
                                    <td colspan="3"><span><a><%=addr.DisplayAddress(entity.Supplier_Address_State, entity.Supplier_Address_City, entity.Supplier_Address_County) +"&nbsp;&nbsp;&nbsp;&nbsp;"+entity.Supplier_Address_StreetAddress %></a></span></td>

                                </tr>
                                <tr>
                                    <td class="name">收货地</td>
                                    <td colspan="3"><span><a><%=addr.DisplayAddress(entity.Supplier_Orders_Address_State, entity.Supplier_Orders_Address_City, entity.Supplier_Orders_Address_County) +"&nbsp;&nbsp;&nbsp;&nbsp;"+entity.Supplier_Orders_Address_StreetAddress %></a></span></td>

                                </tr>
                            </table>


<%--                            <h3 style="padding: 10px;">商品清单</h3>
                            <%=supplier.Order_Logistics_Goods(entity.Supplier_OrdersID,1) %>--%>

                            <div class="clear"></div>
                            <form name="frm_bid" id="frm_bid" method="post" action="/Logistics/Logistics_do.aspx">
                        <div class="b02_main">
                            <ul style="width:850px;">
                                <li><span><i>*</i>报价金额：</span><label><input name="Logistics_Tender_Price" id="Logistics_Tender_Price" type="text" style="width: 298px;" /></label></li>
                                <li><a href="javascript:void(0);" onclick="$('#frm_bid').submit();"  class="a05">报价</a></li>
                            </ul>
                            
                            <div class="clear"></div>
                        </div>
                            <input name="action" type="hidden" id="action" value="newprice" />
                            <input name="LogisticsID" type="hidden" id="LogisticsID" value="<%=LogisticsID %>" />
                            </form> 
                        </div>
                        


                    </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
    <div class="clear"></div>
    </div>

    <!--主体 结束-->


  <ucbottom:bottom runat="server" ID="Bottom" />

</body>
</html>
