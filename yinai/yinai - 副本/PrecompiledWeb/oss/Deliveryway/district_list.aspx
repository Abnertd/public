<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    ITools tools;
    DeliveryWay myApp;
    Addr Addr;
    int Delivery_Way_ID;
    string act;
    
    int District_ID;
    string District_Country, District_State, District_City, District_County;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("58d92d67-4e0b-4a4c-bd5c-6062c432554d");
        
        tools = ToolsFactory.CreateTools();
        myApp = new DeliveryWay();
        Addr = new Addr();
        
        Delivery_Way_ID = tools.CheckInt(Request.QueryString["Delivery_Way_ID"]);
        act = tools.CheckStr(Request["action"]);

        if (act == "renew") {
            District_ID = tools.CheckInt(Request.QueryString["District_ID"]);
            DeliveryWayDistrictInfo entity = myApp.GetDeliveryWayDistrictByID(District_ID);
            if (entity != null) {
                District_ID = entity.District_ID;
                Delivery_Way_ID = entity.District_DeliveryWayID;
                District_Country = entity.District_Country;
                District_State = entity.District_State;
                District_City = entity.District_City;
                District_County = entity.District_County;
            }
            else {
                Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
                Response.End();
            }
        }
        else { 
            act = "new"; 
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
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">配送方式区域设置</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="district_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">指定配送地区</td>
          <td class="cell_content"><span id="textDiv"><%=Addr.SelectAddress("textDiv", "District_State", "District_City", "District_County", District_State, District_City, District_County)%></span>
          <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
          <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="javascript:location='district_list.aspx?delivery_way_id=<% =Delivery_Way_ID%>';"/>
          </td>
        </tr>
      </table>
        <input type="hidden" id="District_Country" name="District_Country" value="1" />
        <input type="hidden" id="District_State" name="District_State" value="<% =District_State%>" />
        <input type="hidden" id="District_City" name="District_City" value="<% =District_City%>"/>
        <input type="hidden" id="District_County" name="District_County" value="<% =District_County%>"/>
        
        <input type="hidden" id="action" name="action" value="<% =act%>" />
        <input type="hidden" id="District_DeliveryWayID" name="District_DeliveryWayID" value="<% =Delivery_Way_ID%>" />
        <input type="hidden" id="District_ID" name="District_ID" value="<% =District_ID%>" />
        </form>
      </td>
    </tr>
    <tr>
      <td class="content_content">
        <table id="list"></table>
        <div id="pager"></div>
        <script type="text/javascript">
        jQuery("#list").jqGrid({
        url: 'district_do.aspx?action=list&delivery_way_id=<% =Delivery_Way_ID%>',
			datatype: "json",
            colNames: ['ID', '配送方式', '省', '市', '县', "操作"],
            colModel: [
				{ name: 'ID', index: 'ID', align: 'center', width: '60px', sortable:false},
				{ name: 'name', index: 'name', align: 'center', sortable:false},
				{ name: 'state', index: 'state', align: 'center', sortable:false},
				{ name: 'city', index: 'city', align: 'center', sortable:false},
				{ name: 'county', index: 'county', align: 'center', sortable:false},
				{ name: 'Operate', index: 'Operate', align: 'center', width: '80px', sortable:false},
			],
            sortname: 'ID',
			sortorder: "desc",
			rowNum: 0,
			multiselect: false,
			viewsortcols: [false,'horizontal',true],
			width: getTotalWidth() - 35,
			height: "100%"
        });
        </script>
      </td>
    </tr>
  </table>
</div>
</body>
</html>