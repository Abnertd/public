<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    
    ITools tools;
    Orders myApp;
    Member member;
    string back_ordercode, back_name, back_email, back_addtime, back_note, back_status, back_account, back_type, SupplierNote, AdminNote, Orders_Delivery_Name;
    double back_amount, Orders_Total_Freight;
    DateTime SupplierTime, AdminTime;
    int back_id, back_statuss, amount_backtype, back_deliveryway, Orders_ID;
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
            OrdersInfo ordersinfo = myApp.GetOrdersBySn(back_ordercode);
            if (ordersinfo != null)
            {
                Orders_ID = ordersinfo.Orders_ID;
                Orders_Total_Freight = ordersinfo.Orders_Total_Freight;
                Orders_Delivery_Name = ordersinfo.Orders_Delivery_Name;
            }
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
<script type="text/javascript">
	function sys_payway_select(textstr){
		MM_findObj("Orders_Payment_Name").value = textstr;
	}
</script>
</head>
<body>
<div class="content_div">
<form name="frm_apply" id="frm_apply" action="orders_back_do.aspx?orders_id=<%=Orders_ID%>" method="post">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">退货入库</td>
    </tr>
    <tr>
      <td class="content_content">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">订单号</td>
          <td class="cell_content t12_red"><% =back_ordercode%></td>
        </tr>
     
        <tr>
				<td width="100" height="23" align="right" class="cell_title">物流费用</td>
				<td align="left" class="cell_content"><input type="text" name="Orders_Delivery_Amount" id="Orders_Delivery_Amount" value="<%=Orders_Total_Freight%>" readonly /></td>
			  </tr>
			  <tr>
				<td width="100" height="23" align="right" class="cell_title">配送方式</td>
				<td align="left" class="cell_content"><%=Orders_Delivery_Name %> <input type="hidden" name="Orders_Delivery_Name" id="Orders_Delivery_Name" value="<%=Orders_Delivery_Name %>" /></td>
			  </tr>
			  <tr>
				<td width="100" height="23" align="right" class="cell_title">物流公司</td>
				<td align="left" class="cell_content"><input type="text" name="Orders_Delivery_companyName" id="Orders_Delivery_companyName" value="<%=Orders_Delivery_Name %>" /> <%=myApp.Delivery_Company_Select("Orders_Delivery_company", "onchange=\"$('#Orders_Delivery_companyName').val($(this).val());\"")%></td>
			  </tr>
			  <tr>
				<td width="100" height="23" align="right" class="cell_title">物流单号</td>
				<td align="left" class="cell_content"><input type="text" name="Orders_Delivery_code" id="Orders_Delivery_code" value="" /></td>
			  </tr>
			  <tr>
				<td width="100" height="23" class="cell_title" align="right">配送备注</td>
				<td align="left" class="cell_content"><textarea name="Orders_Delivery_Note" id="Orders_Delivery_Note" cols="50" rows="5"></textarea></td>
			  </tr>
         <tr>
          <td class="cell_title">所退商品</td>
          
          <td class="cell_content"><%if (back_statuss == 1)
                                     { %><%=myApp.GetOrderBackGoodsInfo(back_id)%><%}
                                     else
                                     { %><%=myApp.GetOrderBackGoodsInfo1(back_id)%><%} %></td>
        </tr>
        <tr>
          <td class="cell_title">审核状态</td>
          <td class="cell_content"><% =back_status%></td>
        </tr>
        <%if (Public.CheckPrivilege("add11f44-d1eb-48bc-bc58-48673b91591a"))
          { %>
       <%if (back_statuss == 1)
         { %>
        <tr>
          <td class="cell_title">处理操作</td>
          <td class="cell_content"> 
          <input type="hidden" name="back_id" value="<%=back_id %>"/>

           <input name="button" type="submit"  class="bt_orange" id="Submit1" value="商品入库" />
            <input type="hidden" name="action" value="productinsert" />
          </td>
          <%} %>
          <%} %>
        </tr>
      </table>
       
      <div style="text-align:right; margin:10px 0px;"> <input name="button" type="submit" class="bt_orange" id="button" value="返回" onclick="history.go(-1);" /></div>
        </td>
    </tr>
  </table>
 </form>
</div>
</body>
</html>
