<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>

<% 
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    Cart cart = new Cart();
    Orders orders = new Orders();
    Supplier supplier = new Supplier();
    Addr addr = new Addr();
    int PriceReport_ID = tools.CheckInt(Request["PriceReport_ID"]);
    int Purchase_ID = tools.CheckInt(Request["Purchase_ID"]);

    supplier.Supplier_Login_Check("/cart/my_buycart.aspx?PriceReport_ID=" + PriceReport_ID + "&Purchase_ID=" + Purchase_ID);
    if (PriceReport_ID == 0 || Purchase_ID == 0)
    {
        Response.Redirect("/supplier/MyReceivePriceReport_list.aspx");
    }
    SupplierPriceReportInfo entity = supplier.SupplierPriceReportByID(PriceReport_ID);
    SupplierPurchaseInfo spinfo = null;
    string address = "";
    string cateName = " -- ";
    int supplier_id = tools.NullInt(Session["supplier_id"]);
    string purchaseName = "";
    if (entity == null)
    {
        Response.Redirect("/supplier/MyReceivePriceReport_list.aspx");
    }
    else
    {
        if (entity.PriceReport_AuditStatus != 1)
        {
            Response.Redirect("/supplier/MyReceivePriceReport_list.aspx");
        }
        spinfo = supplier.GetSupplierPurchaseByID(entity.PriceReport_PurchaseID);
        if (spinfo != null)
        {
            //
            if (entity.PriceReport_MemberID > 0 && spinfo.Purchase_TypeID == 0)
            {
                Response.Redirect("/supplier/MyReceivePriceReport_list.aspx");
            }
            if (spinfo.Purchase_SupplierID != supplier_id)
            {
                Response.Redirect("/supplier/MyReceivePriceReport_list.aspx");
            }
            //采购申请未审核通过或已删除或已过期
            if (spinfo.Purchase_Trash == 1 || spinfo.Purchase_Status != 2 || spinfo.Purchase_ValidDate < tools.NullDate(DateTime.Now.ToShortDateString()))
            {
                Response.Redirect("/supplier/MyReceivePriceReport_list.aspx");
            }

            purchaseName = spinfo.Purchase_Title;


            SupplierPurchaseCategoryInfo category = supplier.GetSupplierPurchaseCategoryByID(spinfo.Purchase_CateID);
            if (category != null)
            {
                cateName = category.Cate_Name;
            }
        }
        else
        {
            Response.Redirect("/supplier/MyReceivePriceReport_list.aspx");
        }
    }
    
    
    string contract_url;
    Session["Orders_Address_ID"] = 0;
    Session["Orders_Delivery_ID"] = 0;
    Session["Orders_Payway_ID"] = 0;
    Session["Orders_DeliveryTime_ID"] = 0;
    Session["delivery_fee"] = 0;        //运费
    Session["order_favor_coupon"] = "0";//优惠券编号
    Session["all_favor_coupon"] = "0";//优惠券使用信息
    Session["order_favorfee"] = 0;   //运费优惠编号
    Session["favor_fee"] = 0;        //运费优惠金额
    Session["favor_coupon_price"] = 0;        //优惠券优惠金额
    Session["favor_policy_price"] = 0;  //优惠政策优惠金额
    Session["favor_policy_id"] = 0;     //优惠政策优惠编号
    Session["total_price"] = 0;
    Session["total_coin"] = 0;


    Session["url_after_login"] = "/cart/my_cart.aspx";
    Session["Web_Cursor"] = "Category";
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>我的购物车<%=" - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
<link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="/scripts/common.js"></script>
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <style type="text/css">
    .foot_info03{border-top:5px solid #ececec; width:990px; margin:0 auto;}
    </style>
    
</head>
<body>
<!--头部 开始-->
<div class="head" style=" height:130px;">
       <uctop:top ID="top1" runat="server" />
      <div class="head_info" style=" background-image:none; width:990px;">
            <div class="logo">
                <a href="/index.aspx">
                <img src="/images/logo.gif"></a>
                </div>
            <div class="num_box">
                  <ul>
                      <li class="on">1.我的进货单</li>
                      <li>2.填写核对订单信息</li>
                      <li>3.成功提交订单</li>
                  </ul>
            </div>
            <div class="clear"></div>
      </div>
</div>
<!--头部 结束-->
<!--主体 开始-->
<div class="partg">
      <h2>选择采购商品</h2>
      <form action="buycartconfirm.aspx" id="cart_frm" method="post">
      <div class="blk23_main">
          <% =supplier.Cart_Purchase_Goods_List(Purchase_ID,PriceReport_ID,true)%>
          
      </div>
      <div class="pg_main" style="border:0px;">
          <div class="main_info03">
                    <a href="javascript:void(0);" onclick="$('#cart_frm').submit();" class="a_03">去结算 ></a>
                    <input type="hidden" name="PriceReport_ID" id="PriceReport_ID" value="<%=PriceReport_ID %>" />
                  <input type="hidden" name="Purchase_ID" id="Purchase_ID" value="<%=Purchase_ID %>" />
              </div>
             </div>
        </form>
</div>
<!--主体 结束-->
<!--尾部 开始-->
<div class="foot">
<ucbottom:bottom ID="bottom1" runat="server" />
       
</div>
<!--尾部 结束-->
</body>
</html>
