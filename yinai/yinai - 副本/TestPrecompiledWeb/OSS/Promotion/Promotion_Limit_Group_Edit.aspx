<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    private Promotion myApp;
    private int Promotion_Limit_Group_ID;
    private string Promotion_Limit_Group_Name;
    private PromotionLimitGroupInfo groupinfo;
    ITools tools;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("34b7b99f-451c-4c0b-8da1-e3ba000891a8");
        myApp = new Promotion();
        tools=ToolsFactory.CreateTools();
        Promotion_Limit_Group_ID=tools.CheckInt(Request["Promotion_Limit_Group_ID"]);
        groupinfo = myApp.GetPromotionLimitGroupByID(Promotion_Limit_Group_ID);
        if (groupinfo != null)
        {
            Promotion_Limit_Group_Name = groupinfo.Promotion_Limit_Group_Name;
        }
        else
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
        }
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>无标题文档</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
<script src="/Scripts/jquery.js" type="text/javascript"></script>
<script src="/Scripts/jquery.zxxbox.3.0.js" type="text/javascript"></script>
<link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
<script src="/Scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
<script src="/Scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>
<script src="/Scripts/common.js" type="text/javascript"></script>
<script src="/Scripts/promotion.js" type="text/javascript"></script>
<script type="text/javascript">
change_inputcss();
</script>

</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">编辑限时促销分组</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="/promotion/Promotion_limit_Group_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        
			
        <tr>
	      <td class="cell_title">
	      限时分组名称
              </td>
	      <td class="cell_content"> <input name="Promotion_Limit_Group_Name" value="<%=Promotion_Limit_Group_Name %>" type="text" id="Promotion_Limit_Group_Name" size="40" maxlength="50"> &nbsp; </td>
	    </tr>   
	    
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="action" name="action" value="renew" />
            <input type="hidden" id="Promotion_Limit_Group_ID" name="Promotion_Limit_Group_ID" value="<%=Promotion_Limit_Group_ID %>" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='Promotion_limit_Group_list.aspx';"/></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
</body>
</html>
