<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="System.Collections.Generic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    
    ITools tools;
    Orders myApp;
    Member member;
    string back_ordercode, back_name, back_email, back_addtime, back_note, back_status, back_account,back_type, SupplierNote, AdminNote;
    double back_amount;
    DateTime SupplierTime, AdminTime;
    int back_id, back_statuss, amount_backtype, back_deliveryway;
    OrdersBackApplyInfo entity;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("aaa944b1-6068-42cd-82b5-d7f4841ecf45");
        tools = ToolsFactory.CreateTools();
        myApp = new Orders();
        member = new Member();

        back_id = tools.CheckInt(Request.QueryString["back_id"]);
        entity = myApp.GetOrdersBackApplyByID(back_id);
        if (entity == null) {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        else {
            back_email = "";
            MemberInfo memberinfo = member.GetMemberByID(entity.Orders_BackApply_MemberID);
            if (memberinfo != null)
            {
                back_email = memberinfo.Member_Email;
            }
            back_id = entity.Orders_BackApply_ID;
            back_ordercode = entity.Orders_BackApply_OrdersCode;
            back_amount = entity.Orders_BackApply_Amount;
            back_name = entity.Orders_BackApply_Name;
            back_account = entity.Orders_BackApply_Account;
            SupplierNote = entity.Orders_BackApply_SupplierNote;
            AdminNote = entity.Orders_BackApply_AdminNote;
            SupplierTime = entity.Orders_BackApply_SupplierTime;
            AdminTime = entity.Orders_BackApply_AdminTime;
            amount_backtype = entity.Orders_BackApply_AmountBackType;
            back_deliveryway = entity.Orders_BackApply_DeliveryWay;
            if (entity.Orders_BackApply_Type == 1)
            {
                back_type = "换货";
            }
            if (entity.Orders_BackApply_Type == 2)
            {
                back_type = "退款";
            }
            else
            {
                back_type = "退货";
            }
            back_statuss = entity.Orders_BackApply_Status;
            if (entity.Orders_BackApply_Status == 0)
            {
                back_status = "未处理";
            }
            else if (entity.Orders_BackApply_Status == 1)
            {
                back_status = "审核通过";
            }
            else if (entity.Orders_BackApply_Status == 2)
            {
                back_status = "审核不通过";
            }
            else if (entity.Orders_BackApply_Status == 3)
            {
                back_status = "已处理";
            }
            else
            {
                back_status = "已处理";
            }
            back_note = entity.Orders_BackApply_Note;
            back_addtime = entity.Orders_BackApply_Addtime.ToString();
        }

    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        myApp = null;
        tools = null;
    }
    
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>无标题文档</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
<script src="/Scripts/common.js" type="text/javascript"></script>
<script src="/Scripts/jquery.js" type="text/javascript"></script>
</head>
<body>
<div class="content_div">

  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">退换货申请</td>
    </tr>
    <tr>
      <td class="content_content">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">订单号</td>
          <td class="cell_content t12_red"><% =back_ordercode%></td>
        </tr>
        <tr>
          <td class="cell_title">类型</td>
          <td class="cell_content"><% =back_type%></td>
        </tr>
        <tr>
          <td class="cell_title">商品</td>
          <td class="cell_content">
              <% 
                  Glaer.Trade.B2C.BLL.ORD.IOrders MyBLL = Glaer.Trade.B2C.BLL.ORD.OrdersFactory.CreateOrders();
                  Glaer.Trade.B2C.BLL.ORD.IOrdersBackApplyProduct MyBackProduct = Glaer.Trade.B2C.BLL.ORD.OrdersBackApplyProductFactory.CreateOrdersBackApplyProduct();
                  string return_value = "";
                  OrdersBackApplyInfo ordersbackapplyinfo = myApp.GetOrdersBackApplyByID(back_id);
                  if (ordersbackapplyinfo != null)
                  {
                      return_value += "<table border=\"0\" width=\"600\" cellpadding=\"5\" cellspacing=\"1\" style=\"background:#999999;\"/>";
                      return_value += "<tr><td style=\"background:#ffffff;\" align=\"center\">编码</td><td style=\"background:#ffffff;\" align=\"center\">商品</td><td style=\"background:#ffffff;\" align=\"center\">名称</td><td style=\"background:#ffffff;\" align=\"center\">申请退/换货数量</td></tr>";
                      IList<OrdersBackApplyProductInfo> entitys = MyBackProduct.GetOrdersBackApplyProductByApplyID(ordersbackapplyinfo.Orders_BackApply_ID);
                      if (entitys != null)
                      {
                          foreach (OrdersBackApplyProductInfo entity in entitys)
                          {
                              OrdersGoodsInfo ordersgoodsinfo = MyBLL.GetOrdersGoodsByID(entity.Orders_BackApply_Product_ProductID);
                              if (ordersgoodsinfo != null)
                              {
                                  return_value += "<tr><td style=\"background:#ffffff;\">" + ordersgoodsinfo.Orders_Goods_Product_Code + "</td><td style=\"background:#ffffff;\"><img src=\"" + Public.FormatImgURL(ordersgoodsinfo.Orders_Goods_Product_Img, "fullpath") + "\" width=\"40\" height=\"40\"/></td><td style=\"background:#ffffff;\">" + ordersgoodsinfo.Orders_Goods_Product_Name + "</td><td style=\"background:#ffffff;\" align=\"center\">" + entity.Orders_BackApply_Product_ApplyAmount + "</td></tr>";
                              }
                          }
                      }
                      return_value += "</table>";
                  }

                  Response.Write(return_value);
                                      
              %>
            </td>
        </tr>
        <tr>
        <td class="cell_title">退款方式</td>
          <td class="cell_content"><% if (amount_backtype == 0)
                                      {%>退虚拟余额账户<%}
                                      else
                                      {%>原路返回<%} %></td>
        </tr>
        <tr>
          <td class="cell_title">退货方式</td>
          <td class="cell_content"><% if (back_deliveryway == 0)
                                      {%>回寄<%}
                                      else
                                      { %>上门取货<%} %></td>
        </tr>
        <tr>
          <td class="cell_title">申请人</td>
          <td class="cell_content"><% =back_name%></td>
        </tr>
        <tr>
          <td class="cell_title">Email</td>
          <td class="cell_content"><% =back_email%></td>
        </tr>
        <tr>
          <td class="cell_title">申请处理状态</td>
          <td class="cell_content"><% =back_status%></td>
        </tr>
        <tr>
          <td class="cell_title">申请时间</td>
          <td class="cell_content"><% =back_addtime%></td>
        </tr>
        <tr>
          <td class="cell_title">申请原因</td>
          <td class="cell_content"><% =back_note%></td>
        </tr>
        
        <% if (Public.CheckPrivilege("1f9e3d6c-2229-4894-891b-13e73dd2e593") && back_statuss == 0) { %>
        
        <form method="post" action="orders_back_do.aspx">
          <tr>
              <td class="cell_title">
                  处理操作
              </td>
              <td align="left">
                  <input type="radio" name="back_action" value="1" checked />
                  审核通过
                  <input type="radio" name="back_action" value="0" />
                  审核不通过 <span class="t12_red">*</span>
              </td>
          </tr>
          <tr>
              <td class="cell_title">
                  审核备注
              </td>
              <td class="cell_content">
                  <textarea name="back_note" rows="5" class="txt_border" style="height: 60px;" cols="50"></textarea>
                  300字符以内<span class="t12_red">*</span>
              </td>
          </tr>
          <tr>
              <td class="cell_title">
              </td>
              <td class="cell_content">
                  <input name="back_id" type="hidden" value="<%=back_id %>" />
                  <input name="action" type="hidden" value="apply_edit" />
                  <input name="btn_submit" type="submit" class="bt_orange" value="确定" /></td>
          </tr>
          </form>
          
          <%} %>
      </table>
      
      <div style="text-align:right; margin:10px 0px;"> <input name="button" type="submit" class="bt_orange" id="button" value="返回" onclick="history.go(-1);" /></div>
        </td>
    </tr>
  </table>
 
</div>
</body>
</html>