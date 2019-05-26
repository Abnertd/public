<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<script runat="server">

    string keyword = "";
    string defaultkey = "";
    string status ;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("18cde8c2-8be5-4b15-b057-795726189795");
        keyword = Request["keyword"];
        if (keyword != "输入卡号进行搜索" && keyword != null)
        {
            keyword = keyword;
        }
        else
        {
            keyword = "输入卡号进行搜索";
        }
        if (keyword == "输入卡号进行搜索")
        {
            defaultkey = "";
        }
        else
        {
            defaultkey = keyword;
        }
        status = Request["coupon_status"];
    }
</script>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>无标题文档</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
<script src="/Scripts/jquery.js" type="text/javascript"></script>
<link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
<script src="/Scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
<script src="/Scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>
<script src="/Public/ckeditor/ckeditor.js" type="text/javascript"></script>
<link href="/Scripts/jqGrid/css/ui.jqgrid.css" rel="stylesheet" type="text/css" />
<script src="/Scripts/jqGrid/grid.locale-zh_CN.js" type="text/javascript"></script>
<script src="/Scripts/jqGrid/jquery.jqGrid.min.js" type="text/javascript"></script>
<script src="/Scripts/common.js" type="text/javascript"></script>
<script type="text/javascript">
change_inputcss();
</script>
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">优惠券/代金券</td>
    </tr>
    <tr><td height="5"></td></tr>
    <tr><td>
    <table width="100%" border="0" cellspacing="0" cellpadding="5">
				<form action="promotion_favor_coupon_list.aspx" method="post" name="frm_sch" id="frm_sch" >
				  <tr bgcolor="#F5F9FC" >
					<td align="right"><span class="left_nav">搜索</span> 
					<select name="coupon_status">
					<option value="0">全部</option>
					<option value="1" <%if(status=="1"){Response.Write("selected");} %>>正常</option>
					<option value="2" <%if(status=="2"){Response.Write("selected");} %>>不可用</option>
					</select>
					 <input type="text" name="keyword" size="50" onfocus="if(this.value=='输入卡号进行搜索'){this.value='';}"  id="keyword" value="<% =keyword %>"> <input type="submit" name="btn_sch" class="btn_01" id="btn_sch" value="搜索" /></td>
				  </tr>
				  </form>
				</table>
    </td></tr>
    <tr>
      <td class="content_content">
        <table id="list"></table>
        <div id="pager"></div>
        <script type="text/javascript">
        jQuery("#list").jqGrid({
        url: 'Promotion_Favor_Coupon_do.aspx?action=list&status=<% =Request["coupon_status"]%>&keyword=<%=Server.UrlEncode(defaultkey) %>',
			datatype: "json",
            colNames: ['ID', '优惠标题', "优惠条件","优惠金额","卡号","验证码","有效期","状态","次数", "操作"],
            colModel: [
				{width:50, name: 'PromotionFavorCouponInfo.Promotion_Coupon_ID', index: 'PromotionFavorCouponInfo.Promotion_Coupon_ID', align: 'center'},
				{align:'center', name: 'PromotionFavorCouponInfo.Promotion_Coupon_Title', index: 'PromotionFavorCouponInfo.Promotion_Coupon_Title'},
				{width:50, align:'center',name: 'PromotionFavorCouponInfo.Promotion_Coupon_Payline', index: 'PromotionFavorCouponInfo.Promotion_Coupon_Payline'},
				{width:50, align:'center',name: 'coupon_favor', index: 'coupon_favor', sortable:false},
				{width:50, align:'center',name: 'PromotionFavorCouponInfo.Promotion_Coupon_Code', index: 'PromotionFavorCouponInfo.Promotion_Coupon_Code'},
				{width:50, align:'center',name: 'PromotionFavorCouponInfo.Promotion_Coupon_Verifycode', index: 'PromotionFavorCouponInfo.Promotion_Coupon_Verifycode'},
				{width:80, align:'center',name: 'coupon_validtime', index: 'coupon_validtime', sortable:false},
				{width:50, align:'center',name: 'FavorFee.Favor_Fee_status', index: 'FavorFee.Favor_Fee_status', sortable:false},
				{width:50, align:'center',name: 'PromotionFavorCouponInfo.Promotion_Coupon_UseAmount', index: 'PromotionFavorCouponInfo.Promotion_Coupon_UseAmount'},
				{width:100, name: 'Operate', index: 'Operate', align: 'center', sortable:true},
			],
            sortname: 'PromotionFavorCouponInfo.Promotion_Coupon_ID',
			sortorder: "desc",
			rowNum: GetrowNum(),
			rowList: GetrowList(), 
			pager: 'pager', 
			multiselect: false,
			viewsortcols: [false,'horizontal',true],
			width: getTotalWidth() - 35,
			height: "100%"
        });
        </script>
        <input type="button" id="Button1" class="bt_orange" value="导出全部优惠券" onclick="location.href='Promotion_Favor_Coupon_do.aspx?action=coupon_export&status=<% =Request["coupon_status"]%>&keyword=<%=Server.UrlEncode(defaultkey) %>'" /> 
      </td>
    </tr>
  </table>
</div>
</body>
</html>
