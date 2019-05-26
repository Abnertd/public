<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    Product myApp;
    Promotion myPromotion;
    ITools tools;
    string  searchtype, keyword, ReqURL;
    int product_cate;
    string defaultkey = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("all");
        tools = ToolsFactory.CreateTools();
        myApp = new Product();
        myPromotion = new Promotion();
        searchtype = tools.CheckStr(Request.QueryString["searchtype"]);
        keyword = Request["keyword"];
        product_cate = tools.CheckInt(Request["product_cate"]);

        if (product_cate == 0) { product_cate = tools.CheckInt(Request["product_cate_parent"]); }

        //ReqURL = "listtype=" + listtype + "&searchtype=" + searchtype + "&keyword=" + Server.UrlEncode(keyword) + "&product_cate=" + product_cate;

        if (keyword != "输入商品编码、商品名称、通用名、生产企业、拼音首字母进行搜索" && keyword != null)
        {
            keyword = keyword;
        }
        else
        {
            keyword = "输入商品编码、商品名称、通用名、生产企业、拼音首字母进行搜索";
        }
        if (keyword == "输入商品编码、商品名称、通用名、生产企业、拼音首字母进行搜索")
        {
            defaultkey = "";
        }
        else
        {
            defaultkey = keyword;
        }
        ReqURL = "keyword=" + Server.UrlEncode(defaultkey) + "&product_cate=" + product_cate;
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>无标题文档</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
<script src="/Scripts/common.js" type="text/javascript"></script>
<script src="/Scripts/jquery.js" type="text/javascript"></script>
<script src="/scripts/product.js" type="text/javascript"></script>
<script type="text/javascript">
	function product_add(obj){
	    $.ajax({
	        url: encodeURI("promotion_limit_do.aspx?action=check_product&product_id="+ SelectedValue(MM_findObj(obj)) +"&timer="+Math.random()),
		    type: "get", 
		    global: false, 
		    async: false,
		    dataType: "html",
		    success:function(data){
			    window.opener.$("#yhnr").html(data);
			    window.close();
		    },
		    error: function (){
			    alert("Error Script");
		    }
        });
	}
	
	function SelectedValue(obj){
		var _channel="";	
		for(var i=0;i<obj.length;i++){ 
			if(obj[i].checked){
				if (_channel.length==0){
					_channel=obj[i].value; 
				}
				else{
					_channel=_channel+","+obj[i].value; 
				}
			} 
		}
		if(obj.length==null)
		{
		    _channel=obj.value; 
		}
			return _channel
	}
</script>
</head>
<body style="margin:10px;">
<table width="100%" border="0" cellspacing="0" cellpadding="5">
				<form action="limitproduct.aspx" method="post" name="frm_sch" id="frm_sch" >
				  <tr bgcolor="#F5F9FC" >
					<td align="right"><span class="left_nav">搜索</span> 
					<span id="main_cate"><%=myApp.Product_Category_Select(product_cate, "main_cate")%></span>
					 <input type="text" name="keyword" size="70" id="keyword" onfocus="if(this.value=='输入商品编码、商品名称、通用名、生产企业、拼音首字母进行搜索'){this.value='';}" value="<% =keyword %>"> <input type="submit" name="btn_sch" class="btn_01" id="btn_sch" value="搜索" /></td>
				  </tr>
				  </form>
				</table>
    <% =myPromotion.SelectProduct()%>
</body>
</html>
