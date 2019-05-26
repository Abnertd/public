<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<% 
    Public.CheckLogin("81e0af57-348d-4565-9e73-7146b3116b8c");
   Supplier Supplier = new Supplier();
   Shop shop = new Shop();
   int Supplier_Shop_ApplyID;
   ITools tools;
    Addr addr=new Addr();
   tools = ToolsFactory.CreateTools();
   DateTime CloseShop_Apply_Addtime = DateTime.Now;
   DateTime CloseShop_Apply_AdminTime = DateTime.Now;
   string CloseShop_Apply_Note = "";
   string CloseShop_Apply_AdminNote = "";
   int CloseShop_Apply_Status = 0;
   int Supplier_ID = 0;
   int CloseShop_Apply_ID = tools.CheckInt(Request["Apply_ID"]);
   if (CloseShop_Apply_ID == 0)
   {
       Public.Msg("error", "错误信息", "记录不存在", false, "/supplier/supplier_closeshop_apply.aspx");
   }
   SupplierCloseShopApplyInfo entity = Supplier.GetSupplierCloseShopApplyByID(CloseShop_Apply_ID);
   if (entity != null)
   {
       CloseShop_Apply_ID = entity.CloseShop_Apply_ID;
       CloseShop_Apply_Note = entity.CloseShop_Apply_Note;
       CloseShop_Apply_Addtime = entity.CloseShop_Apply_Addtime;
       CloseShop_Apply_Status = entity.CloseShop_Apply_Status;
       CloseShop_Apply_AdminNote = entity.CloseShop_Apply_AdminNote;
       CloseShop_Apply_AdminTime = entity.CloseShop_Apply_AdminTime;
       Supplier_ID = entity.CloseShop_Apply_SupplierID;
   }
   else
   {
       Public.Msg("error", "错误信息", "记录不存在", false, "/supplier/supplier_closeshop_apply.aspx");
   }
    
    
    %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>供应商店铺关闭申请</title>
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
      <td class="content_title">供应商店铺关闭申请</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="supplier_closeshop_apply_Do.aspx">
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
          <td class="cell_title">申请时间</td>
          <td class="cell_content"><%=CloseShop_Apply_Addtime%></td>
        </tr>
        <tr>
          <td class="cell_title">申请说明</td>
          <td class="cell_content"><%=CloseShop_Apply_Note%></td>
        </tr>
        <%if (CloseShop_Apply_Status == 0)
          { %>

            <tr>
                <td class="cell_title">
                    备注说明：
                </td>
                <td class="cell_content">
                    <textarea name="CloseShop_Apply_AdminNote" cols="50" rows="5"></textarea>  最多不超过200个字符
                    
                </td>
            </tr>
            
            <%}
          else
          { %>

            <tr>
                <td class="cell_title">
                    备注说明：
                </td>
                <td class="cell_content">
                    <%=CloseShop_Apply_AdminNote%>
                    
                </td>
            </tr>
            <tr>
                <td class="cell_title">
                    处理时间：
                </td>
                <td class="cell_content">
                    <%=CloseShop_Apply_AdminTime%>
                    
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
      <%if (CloseShop_Apply_Status == 0 && Public.CheckPrivilege("0791905a-1212-4fa5-8708-b835cc03c4a3"))
        { %>
          <input type="submit" name="Audit" class="bt_orange" value="审核通过" onclick="$('#hd_action').val('applypassaudit');"/>  
          <input type="submit" name="NotAudit" class="bt_orange" value="审核不通过" onclick="$('#hd_action').val('applynotpassaudit');"/>
          <input type="hidden" id="hd_action" name="action" value="applypassaudit" />
            <input type="hidden" id="Hidden1" name="CloseShop_Apply_ID" value="<%=CloseShop_Apply_ID %>" />
            <%} %>
             <input name="button" type="button" class="bt_orange" id="button1" value="返回" onclick="location='supplier_closeshop_apply.aspx';" />
      </div>
      </form>
</div>
</body>
</html>
