<%@ Page Language="C#" CodePage="65001"%>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<% SysMenu Menu = new SysMenu();
   ITools tools;
   Public.CheckLogin("all");
   tools = ToolsFactory.CreateTools();
   int channel_id = tools.CheckInt(Request["channel"]); %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Menu</title>
<link href="/css/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="/scripts/jquery.js"></script>
<script>
    function change_menu(Obj)
{
    for(var i=0;i<$('td').length;i++)
    {
        if($('td:eq('+i+')').attr('class').indexOf('menu_item')>=0)
        {
            $('td:eq('+i+')').attr('class','menu_item');
        }
    }
    Obj.parent().attr('class','menu_itemon');
}
function menu_fold(Obj) {

    if ($("#" + Obj + "_list").attr("class") == "a1") {
        $("#" + Obj + "_list").hide();
        $("#" + Obj + "_list").attr("class", "a2");
    }
    else {
        $("#" + Obj + "_list").show();
        $("#" + Obj + "_list").attr("class", "a1");
    }
}
</script>
<style type="text/css">
    img{vertical-align:middle; margin-right:3px;}
</style>
</head>
<body>
<% Menu.Sys_Menu_Display(channel_id);%>
</body>
</html>