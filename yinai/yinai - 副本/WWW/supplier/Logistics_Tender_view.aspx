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
    int ID = tools.CheckInt(Request["ID"]);
    Supplier supplier = new Supplier();
    supplier.Supplier_Login_Check("/supplier/Logistics_Tender_view.aspx?ID=" + ID);
    Orders MyOrders=new Orders();
    Logistics MyLogistics = new Logistics();
    Addr addr = new Addr();
    SupplierInfo supplierinfo = supplier.GetSupplierByID();
    if (supplierinfo != null)
    {
        if (tools.NullInt(Session["supplier_ishaveshop"]) == 0)
        {
            pub.Msg("error", "错误信息", "您还没有开通店铺", false, "/supplier/index.aspx");
        }
    }
    LogisticsTenderInfo logisticstenderinfo =logistics.GetLogisticsTenderByID(ID);
    if (logisticstenderinfo==null)
    {
        Response.Redirect("/supplier/Logistics_Tender.aspx?ID=" + ID);
    }

    SupplierLogisticsInfo entity = MyLogistics.GetSupplierLogisticsByID(logisticstenderinfo.Logistics_Tender_SupplierLogisticsID);
    if (entity != null)
    {
        if (entity != null)
        {
            if (entity.Supplier_SupplierID != supplierinfo.Supplier_ID)
            {
                Response.Redirect("/supplier/Logistics_Tender.aspx?ID=" + ID);
            }
        }
        else
        {
            Response.Redirect("/supplier/Logistics_Tender.aspx?ID=" + ID);
        }
    }
    else
    {
        Response.Redirect("/supplier/Logistics_Tender.aspx?ID=" + ID);
    }
    
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="物流详情 - 物流商用户中心 - " + pub.SEO_TITLE()%></title>
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
            <div class="position">当前位置 > <a href="/supplier/">我是卖家</a> > 物流管理 > <strong>报价详情</strong></div>
            <!--位置说明 结束-->
            <div class="partc">
                <div class="pd_left">

                    <% supplier.Get_Supplier_Left_HTML(6, 1); %>
                </div>
                <div class="pc_right">


                    <div class="blk14_1" style="margin-top: 1px;">
                              <h2>物流详情</h2>
                    
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
                                    <td width="100" class="name">数量</td>
                                    <td width="400"><span><a><%=entity.Supplier_Logistics_Number %></a></span></td>
                                    <td width="100" class="name">状态</td>
                                    <td width="300"><span><a><%=MyLogistics.SupplierLogisticsStatus(entity) %></a></span></td>
                                </tr>
                                <tr>
                                    <td width="100" class="name">是否中标</td>
                                    <td width="400"><span><a><%=MyLogistics.GetLogisgicsTenderIsWin(logisticstenderinfo.Logistics_Tender_IsWin)%></a></span></td>
                                    <td width="100" class="name">报价金额</td>
                                    <td width="300"><span><a><%=pub.FormatCurrency(logisticstenderinfo.Logistics_Tender_Price) %></a></span></td>
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


                        <%if(entity.Supplier_Logistics_TenderID==0) {%>
                            <div class="b02_main" >
                                <form name="frm_bid" id="frm_bid" method="post" action="/supplier/Logistics_do.aspx">
                                <ul style="width:850px;">
                                    <li><a class="a05" href="javascript:void(0);" onclick="$('#frm_bid').submit();" >中标</a></li>
                                    </ul>
                                    <input name="action" type="hidden" id="action" value="winadd" />
                                    <input name="Supplier_Logistics_ID" type="hidden" id="Supplier_Logistics_ID" value="<%=entity.Supplier_Logistics_ID %>" />
                                    <input name="TenderID" type="hidden" id="TenderID" value="<%=ID %>" />
                            </form>
                                </div>
                                <div class="clear"></div>
                            <%} %>

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
