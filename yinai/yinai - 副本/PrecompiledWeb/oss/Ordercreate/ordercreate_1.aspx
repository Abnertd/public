<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">
    
    private ITools tools;
    private OrdersProcess MyApp;
    
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("9d8d62af-29b1-4302-957c-268732fc15b4");

        tools = ToolsFactory.CreateTools();
        MyApp = new OrdersProcess();

        //MyApp.ClearOrdersGoodsTmpByOrdersID(-1);
        MyApp.Cart_Price_Update(10);
    }
    protected void Page_UnLoad(object sender, EventArgs e) {
        tools = null;

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

<style type="text/css">
    
    .tablebg_green{ background:#390;}
    .tablebg_green td{ background:#FFF;}
</style>

</head>
<body>

<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">创建订单</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="frm_product" name="frm_product" method="post" action="ordercreate_2.aspx">
			<table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
			  <tr>
				<td width="100" height="23" align="right" class="cell_title">用户名</td>
				<td align="left" class="cell_content"><span id="member_name"></span> &nbsp;<input type="button" value="选择用户" class="btn_01" onclick="window.open('member_select.aspx?fresh='+Math.random()+'');" /></td>
			  </tr>
			  <tr>
			    <td height="23" align="right" class="cell_title">选择商品</td>
			    <td align="left" class="cell_content">
			     <div style="height:30px">&nbsp;<input type="button" value="添加商品" class="btn_01" onclick="window.open('product_select.aspx?fresh='+Math.random()+'');" />&nbsp;<input type="button" value="添加捆绑产品" class="btn_01" onclick="window.open('package_select.aspx?fresh='+Math.random()+'');" /></div>
			    <div id="goods_tmpinfo">
			    <%
                    Response.Write(MyApp.Orders_Goods_List( 0, false));
			                 %></div>
			    </td>
			  </tr>

			</table>
			<table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <span class="t12_red">请在此页面与顾客核对好商品信息及数量</span>
            
            <input name="button" type="button" class="bt_orange" id="cart_next_btn" value="下一步" onclick="cart_next();" />
            
            <input name="member_id" type="hidden" id="member_id" value="0" />
				<input name="action" type="hidden" id="action" value="product_select" />
             </td>
          </tr>
        </table>
			</form>
      </td>
    </tr>
  </table>
</div>


<script type="text/javascript">

    function displaySubGoods(id){
        $("#subgoods_"+ id).toggle();
        if ($("#subgoods_"+ id).css("display") == "none"){ 
            $("#subicon_"+ id).attr("src", "/images/display_close.gif");
        }
        else{
            $("#subicon_"+ id).attr("src", "/images/display_open.gif");
        }
    }
    
    function document.onkeydown() { if (event.keyCode == 13) { $("#cart_next_btn").click(); return false; } }

    function cart_next() {
        $.get("ordercreate_do.aspx?action=checkselectproduct&t=" + Math.random(), function(data) {
            if (data == "true") {
                $("#frm_product").submit();
            }
            else {
                alert(data);
            }
        });
    }
</script>
</body>
</html>