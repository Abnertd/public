<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    
    ITools tools;

    string QueryURL = "";
    
    string startDate = "";
    string endDate = "";
    string orders_status = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("febce311-c2f6-4da7-ba34-e99d6fbbbd8b");

        tools = ToolsFactory.CreateTools();
        
        startDate = tools.CheckStr(Request.Form["startDate"]);
        endDate = tools.CheckStr(Request.Form["endDate"]);
        orders_status = tools.CheckStr(Request.Form["orders_status"]);
        if (orders_status.Length == 0) { orders_status = "confirm"; }

        try
        {
            QueryURL += "startDate=" + DateTime.Parse(startDate).ToShortDateString() + "&endDate=" + DateTime.Parse(endDate).ToShortDateString() + "&orders_status=" + orders_status;
        }
        catch (Exception ex)
        {
            QueryURL += "startDate=" + DateTime.Today.ToString("yyyy-M-1") + "&endDate=" + DateTime.Today.ToString() + "&orders_status=" + orders_status;
        }
        
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>无标题文档</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
<script src="/Scripts/jquery.js" type="text/javascript"></script>
<script src="/Scripts/common.js" type="text/javascript"></script>
<link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
<script src="/Scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
<script src="/Scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>
<script src="/Public/ckeditor/ckeditor.js" type="text/javascript"></script>
<link href="/Scripts/jqGrid/css/ui.jqgrid.css" rel="stylesheet" type="text/css" />
<script src="/Scripts/jqGrid/grid.locale-zh_CN.js" type="text/javascript"></script>
<script src="/Scripts/jqGrid/jquery.jqGrid.min.js" type="text/javascript"></script>

<link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
<script src="/Scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
<script src="/Scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>


</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">销售量统计</td>
    </tr>
    <tr>
      <td class="content_content">
      
        <form action="" id="frmsearch" method="post">
        <table cellpadding="0" cellspacing="0">
          <tr>
            <td>分析时间段： <input type="text" class="input_calendar" name="startDate" id="startDate" maxlength="10" readonly="readonly" value="<%=startDate%>" />
            至 <input type="text" class="input_calendar" name="endDate" id="endDate" maxlength="10" readonly="readonly" value="<%=endDate%>" />
                <script type="text/javascript">
                    $(document).ready(function() {
                        $("#startDate").datepicker({ numberOfMonths: 2 });
                        $("#endDate").datepicker({ numberOfMonths: 2 });
                    });
                </script>
            </td>
            <td width="5"></td>
            <td>订单状态
                <select id="orders_status" name="orders_status">
                    <option value="confirm" <% =Public.CheckedSelected("confirm", orders_status)%> >已确认及已成功</option>
                    <option value="success" <% =Public.CheckedSelected("success", orders_status)%>>已成功</option>
                </select>
            </td>
            <td width="5"></td>
            <td><input type="submit" id="btnsubmit" value="分析" class="bt_orange" /></td>
          </tr>
        </table>
        </form>
        
        <div style="height:5px;"></div>
        
        <div style="border-bottom:dotted 2px #ccc;"></div>
        
        <div><img src="" id="salechart" /></div>
        <script type="text/javascript"> $("#salechart")[0].src = "/public/chart.aspx?type=seleamountimg&width="+($(document).width() - 50) +"&height=300&<%=QueryURL%>";</script>
      </td>
    </tr>
  </table>
</div>
</body>
</html>