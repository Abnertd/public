<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%
    Public.CheckLogin("all");
    Response.Buffer = true;
    Response.ExpiresAbsolute = DateTime.Now.AddYears(-1);
    Response.Expires = 0;
    Response.CacheControl = "no-cache";
    %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>无标题文档</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css"/>

<script src="/Scripts/jquery.js" type="text/javascript"></script>
<link rel="stylesheet" type="text/css" href="/scripts/treeview/dhtmlxtree.css" />
<script  src="/scripts/treeview/dhtmlxcommon.js" type="text/javascript"></script>
<script  src="/scripts/treeview/dhtmlxtree.js" type="text/javascript"></script>
<script type="text/javascript">
<!--

function MM_ReturnID(url){ //v3.0
	$('#favor_cateid').val(tree.getAllChecked());
	$.ajax({
	async: false,
    type: "POST",
    url: "picker_do.aspx?action=savecateid&timer=" + Math.random(),
    data: "cate_id=" +  $("#favor_cateid").val()
    });
	$('#cate_picker').load("picker_do.aspx?action=showcate&timer=" + Math.random());
	if($('#favor_cateid').val()!="")
	{
	    $('#favor_cateall0').attr("checked",true);
	    $('#favor_cateall1').attr("checked",false);
	}
	close_picker();
}


//-->
</script>
</head>
<body>

              <table width="100%" cellpadding="0" cellspacing="0" border="0" class="picker_tittab">
              <tr><td class="picker_tit">类别选择</td><td width="30" align="center"><a href="javascript:void(0);" onclick="close_picker();"><img src="/images/close.gif" border="0"/></a></td></tr>
              </table>
              <table style="width:600px; background:#f5f5f5; border:1px solid Silver; overflow:auto;">
	            <tr>
		            <td valign="top">
			            <div id="treeboxbox_tree" ></div>
		            </td>
	            </tr>
	            <tr>
		            <td valign="top"><input type="button" name="btn_sub" value="确定" class="btn_01" onclick="MM_ReturnID()"/> </td>
	            </tr>
            </table>
            <script type="text/javascript">
                tree=new dhtmlXTreeObject("treeboxbox_tree","100%","100%",0);
                tree.setSkin('dhx_skyblue');
                tree.setImagePath("/scripts/treeview/imgs/csh_dhx_skyblue/");
                tree.enableCheckBoxes(1);
                tree.enableThreeStateCheckboxes(true);
                tree.loadXML("treedata.aspx?cate_id=<%=Session["selected_cateid"] %>");
            </script>
            <span id="div_Product_CateID"></span>

</body>
</html>
