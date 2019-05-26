<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<% 
    Public.CheckLogin("b90823db-e737-4ae9-b428-1494717b85c7");
   Supplier Supplier = new Supplier();
   Shop shop = new Shop();
   int Supplier_Shop_ApplyID;
   ITools tools;
    Addr addr=new Addr();
   tools = ToolsFactory.CreateTools();
   DateTime Supplier_PayBack_Apply_Addtime = DateTime.Now;
   DateTime Supplier_PayBack_Apply_AdminTime = DateTime.Now;
   string Supplier_PayBack_Apply_Note = "";
   string Supplier_PayBack_Apply_AdminNote = "";
   int Supplier_PayBack_Apply_Type = 0;
   double Supplier_PayBack_Apply_Amount = 0;
   int Supplier_PayBack_Apply_Status = 0;
   double Supplier_PayBack_Apply_AdminAmount = 0;
   int Supplier_ID = 0;
   string Product_Name = "";
   int Apply_ID = tools.CheckInt(Request["Apply_ID"]);
   if (Apply_ID == 0)
   {
       Public.Msg("error", "错误信息", "记录不存在", false, "/supplier/Supplier_PayBack_Apply.aspx");
   }
   SupplierPayBackApplyInfo entity = Supplier.GetSupplierPayBackApplyByID(Apply_ID);
   if (entity != null)
   {
       Apply_ID = entity.Supplier_PayBack_Apply_ID;
       Supplier_PayBack_Apply_Amount = entity.Supplier_PayBack_Apply_Amount;
       Supplier_PayBack_Apply_Note = entity.Supplier_PayBack_Apply_Note;
       Supplier_PayBack_Apply_Addtime = entity.Supplier_PayBack_Apply_Addtime;
       Supplier_PayBack_Apply_Status = entity.Supplier_PayBack_Apply_Status;
       Supplier_PayBack_Apply_Type = entity.Supplier_PayBack_Apply_Type;
       Supplier_PayBack_Apply_AdminAmount = entity.Supplier_PayBack_Apply_AdminAmount;
       Supplier_PayBack_Apply_AdminNote = entity.Supplier_PayBack_Apply_AdminNote;
       Supplier_PayBack_Apply_AdminTime = entity.Supplier_PayBack_Apply_AdminTime;
       Supplier_ID = entity.Supplier_PayBack_Apply_SupplierID;
   }
   else
   {
       Public.Msg("error", "错误信息", "记录不存在", false, "/supplier/Supplier_PayBack_Apply.aspx");
   }
    
    
    %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>供应商账户退款申请</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
<script src="/Scripts/jquery.js" type="text/javascript"></script>
<script src="/Scripts/common.js" type="text/javascript"></script>
<link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
<script src="/Scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
<script src="/Scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>
    <script type="text/javascript">
        change_inputcss();
        btn_scroll_move();
</script>
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">供应商账户退款申请</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="supplier_payback_apply_Do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">

       <tr>
          <td class="cell_title">供应商名称</td>
          <td class="cell_content"><%
                                       SupplierInfo supplierinfo = Supplier.GetSupplierByID(Supplier_ID);
                                       if (supplierinfo != null)
                                       {
                                           Response.Write(supplierinfo.Supplier_CompanyName);
                                       }
                                       
                                        %></td>
        </tr>
        <tr>
          <td class="cell_title">申请退款类型</td>
          <td class="cell_content"><%if (Supplier_PayBack_Apply_Type == 1)
                        {
                            Response.Write("会员费");
                        }
                        else if (Supplier_PayBack_Apply_Type == 2)
                        {
                            Response.Write("保证金");
                        }
                        else
                        {
                            Response.Write("推广费");
                        }%></td>
         </tr>
         <tr>
          <td class="cell_title">申请退款时间</td>
          <td class="cell_content"><%=Supplier_PayBack_Apply_Addtime%></td>
        </tr>
        <tr>
          <td class="cell_title">申请退款金额</td>
          <td class="cell_content"><%=Public.DisplayCurrency(Supplier_PayBack_Apply_Amount)%></td>
        </tr>
        <tr>
          <td class="cell_title">申请退款备注</td>
          <td class="cell_content"><%=Supplier_PayBack_Apply_Note%></td>
        </tr>
        <%if (Supplier_PayBack_Apply_Status == 0)
          { %>
            <tr>
                <td class="cell_title">
                    实际退款金额：
                </td>
                <td class="cell_content">
                    <input name="Supplier_PayBack_Apply_AdminAmount" type="text" id="Supplier_PayBack_Apply_AdminAmount" size="20" maxlength="50" />
                </td>
            </tr>
            <tr>
                <td class="cell_title">
                    平台备注说明：
                </td>
                <td class="cell_content">
                    <textarea name="Supplier_PayBack_Apply_AdminNote" cols="50" rows="5"></textarea>  最多不超过200个字符
                    
                </td>
            </tr>
            
            <%}
          else
          { %>
          <tr>
                <td class="cell_title">
                    实际退款金额：
                </td>
                <td class="cell_content">
                    <%=Public.DisplayCurrency(Supplier_PayBack_Apply_AdminAmount)%>
                </td>
            </tr>
            <tr>
                <td class="cell_title">
                    平台备注说明：
                </td>
                <td class="cell_content">
                    <%=Supplier_PayBack_Apply_AdminNote%>
                    
                </td>
            </tr>
            <tr>
                <td class="cell_title">
                    申请处理时间：
                </td>
                <td class="cell_content">
                    <%=Supplier_PayBack_Apply_AdminTime%>
                    
                </td>
            </tr>
          <%} %>
      
      </table>

        </td>
    </tr>
  </table>

  

        <div class="foot_gapdiv">
      </div>
      <div class="float_option_div" id="float_option_div">
      <%if (Supplier_PayBack_Apply_Status == 0 && Public.CheckPrivilege("479e01e0-d543-47c2-a229-52e9eb847886"))
        { %>
          <input type="submit" name="Audit" class="bt_orange" value="审核通过" onclick="$('#hd_action').val('applypassaudit');"/>  
          <input type="submit" name="NotAudit" class="bt_orange" value="审核不通过" onclick="$('#hd_action').val('applynotpassaudit');"/>
          <input type="hidden" id="hd_action" name="action" value="applypassaudit" />
            <input type="hidden" id="Hidden1" name="Supplier_PayBack_Apply_ID" value="<%=Apply_ID %>" />
            <%} %>
             <input name="button" type="button" class="bt_orange" id="button1" value="返回" onclick="location='supplier_payback_apply.aspx';" />
      </div>
      </form>
</div>
</body>
</html>
