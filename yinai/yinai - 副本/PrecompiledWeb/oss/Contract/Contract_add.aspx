<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    private Orders myApp;
    ITools tools;
    int Orders_Type = 0;
    string orders_sn;
    int orders_id;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("010afb3b-1cbf-47f9-8455-c35fe5eceea7");
        tools = ToolsFactory.CreateTools();
        myApp = new Orders();
         orders_sn = tools.CheckStr(Request["orders_sn"]);
        
        if (orders_sn.Length > 0)
        {
            OrdersInfo entity = myApp.GetOrdersBySn(orders_sn);
            if (entity == null)
            {
                Public.Msg("error", "错误信息", "订单记录不存在", false, "/orders/orders_list.aspx");
            }
            else
            {
                Orders_Type = entity.Orders_Type;
            }
            if (entity.Orders_SupplierID > 0)
            {
                Public.Msg("error", "错误信息", "平台不能创建针对供应商的意向合同", false, "/orders/orders_list.aspx");
            }
            orders_id = entity.Orders_ID;
        }
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>无标题文档</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
<script src="/Scripts/jquery.js" type="text/javascript"></script>
<script type="text/javascript" src="/Public/kindeditor/kindeditor-all.js"></script>
<script type="text/javascript" src="/Public/kindeditor/lang/zh_CN.js"></script>
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">创建意向合同</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="contract_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">合同名称</td>
          <td class="cell_content"><input name="Contract_Name" type="text" id="Contract_Name" size="50" maxlength="50" /> <span class="t14_red">*</span></td>
        </tr>
        <%
            if (orders_sn.Length > 0)
            { 
            %>
            <tr>
          <td class="cell_title">订单编号</td>
          <td class="cell_content">
          <a class="a_t12_blue" href="/orders/orders_view.aspx?orders_id=<%=orders_id %>" target="_blank"><%=orders_sn%></a></td>
        </tr>
        <tr>
          <td class="cell_title">合同类型</td>
          <td class="cell_content">
          <%switch (Orders_Type)
            { 
                case 1:
                    Response.Write("现货采购合同");
                    break;
                case 2:
                    Response.Write("定制采购合同");
                    break;
                case 3:
                    Response.Write("代理采购合同");
                    break;
            }
            Response.Write("<input type=\"hidden\" name=\"contract_type\" value=\"" + Orders_Type + "\" />");
            Response.Write("<input type=\"hidden\" name=\"orders_sn\" value=\"" + orders_sn + "\" />");
        %>
          </td>
        </tr>
        <%}
            else
            { %>
        <tr>
          <td class="cell_title">选择合同类型</td>
          <td class="cell_content">
          <input type="radio" name="contract_type" value="1" checked /> 现货采购合同
        <input type="radio" name="contract_type" value="2" /> 定制采购合同
        <input type="radio" name="contract_type" value="3" /> 代理采购合同
        <span class="t14_red">*</span></td>
        </tr>
        <%} %>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="action" name="action" value="addtmpcontract" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='contract_template.aspx';"/></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
</body>
</html>