<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%@ Register Src="~/Public/Top.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>

<%
    //静态化配置
    PageURL pageurl = new PageURL(int.Parse(Application["Static_IsEnable"].ToString()));

    Product product = new Product();
    ITools tools = ToolsFactory.CreateTools();
    Public_Class pub = new Public_Class();
    
    int cate_id=0;
    int product_id = 0;
    string product_name = "";
    string product_code="";
    string product_img = "";
    
    product_id = tools.CheckInt(Request["product_id"]);
    product_img = Request["img"];
    if (product_id == 0)
    {
        Response.Redirect("/index.aspx");
    }
    ProductInfo productinfo = product.GetProductByID(product_id);
    if (productinfo != null)
    {
        if (productinfo.Product_IsAudit == 1)
        {
            product_name = productinfo.Product_Name;
            cate_id = productinfo.Product_CateID;
            product_code=productinfo.Product_Code;
        }
        else
        {
            Response.Redirect("/index.aspx");
        } 
    }
    else
    {
        Response.Redirect("/index.aspx");
    }
    string button="";
    if (productinfo.Product_UsableAmount > 0 || productinfo.Product_IsNoStock == 1)
    {
        button = "<a href=\"/cart/cart_do.aspx?action=add&product_id=" + productinfo.Product_ID + "\"><img src=\"/images/btn_buy.jpg\" title=\"加入到购物车\" border=\"0\" align=\"absmiddle\" /></a>";
    }
    else
    {
        button = "<a href=\"javascript:void(0);\"><img src=\"/images/btn_nostock.jpg\" title=\"暂无货\" align=\"absmiddle\"/></a>";
    }
    
    int first_cate = product.Get_First_CateID(cate_id);
    product.Set_Cate_Session(first_cate);
    
    string[] Img_Arry = product.GetProductImg(product_id);
    string Product_Img, S_Product_Img, Product_Img_Ext_1, S_Product_Img_Ext_1, Product_Img_Ext_2, S_Product_Img_Ext_2;
    string Product_Img_Ext_3, S_Product_Img_Ext_3, Product_Img_Ext_4, S_Product_Img_Ext_4;
    Product_Img = pub.FormatImgURL(Img_Arry[0], "fullpath");
    S_Product_Img = pub.FormatImgURL(Img_Arry[0], "thumbnail");
    Product_Img_Ext_1 = pub.FormatImgURL(Img_Arry[1], "fullpath");
    S_Product_Img_Ext_1 = pub.FormatImgURL(Img_Arry[1], "thumbnail");
    Product_Img_Ext_2 = pub.FormatImgURL(Img_Arry[2], "fullpath");
    S_Product_Img_Ext_2 = pub.FormatImgURL(Img_Arry[2], "thumbnail");
    Product_Img_Ext_3 = pub.FormatImgURL(Img_Arry[3], "fullpath");
    S_Product_Img_Ext_3 = pub.FormatImgURL(Img_Arry[3], "thumbnail");
    Product_Img_Ext_4 = pub.FormatImgURL(Img_Arry[4], "fullpath");
    S_Product_Img_Ext_4 = pub.FormatImgURL(Img_Arry[4], "thumbnail");
    if (product_img == "" || product_img == null)
    {
        product_img = Product_Img;
    }
    Session["Web_Cursor"] = "Category";
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%=product_name + " - " + pub.SEO_TITLE()%></title>
    <meta name="Keywords" content="<% = productinfo.Product_SEO_Keyword%>" />
    <meta name="Description" content="<%=productinfo.Product_SEO_Description%>" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="/css/jquery.jqzoom.css" rel="stylesheet" type="text/css" />
    <link href="../css/scrollable-horizontal.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/common.js"></script>
    <script type="text/javascript" src="/scripts/jquery.js"></script>
      <script type="text/javascript" src="/scripts/MSClass.js"></script>
    
    <style type="text/css">
        .img_border{ border:1px solid #d5d5d5;}
        .table_filter {margin-top:10px;}
        .table_filter img { display:inline; }
    </style>
</head>
<body>

    <uctop:top ID="top1" runat="server" />




      <div class="content">
        <!--位置说明 开始-->
        <div class="position">
              当前位置 > <a href="/">首页</a><%=product.Get_Cate_Nav(cate_id, " > ")%>
            > <a href="<%=pageurl.FormatURL(pageurl.product_detail, product_id.ToString()) %>">
                <%=productinfo.Product_Name%></a>
           
           </div>
        <div class="clear">
        </div>
         <div style="border-top: 1px solid #DDDDDD;">
            
    <table width="100%" border="0" cellpadding="6" cellspacing="0" class="table_filter table_padding_5">
      <tr>
        <td align="left" height="50"><h1><%=product_name%></h1></td>
		<td align="right" class="t12_grey"><%=button %> <a href="/supplier/fav_do.aspx?action=product&id=<%=product_id%>" target="myfav"><img src="/Images/btn_favor.jpg" title="收藏该商品" border="0" align="absmiddle" /></a></td>
	  </tr>
	  <tr>
        <td align="left" colspan="2">
		<table border="0" cellspacing="0" cellpadding="5">
		  <tr>
			<%if (Product_Img != "/images/detail_no_pic.gif"){%>
			<td width="40" height="40" align="center" class="img_border"><a href="/product/images.aspx?product_id=<%=product_id%>&img=<%=Product_Img%>"><img src="<%=Product_Img%>" border="0" width="100" height="100" onload="javascript:AutosizeImage(this,100,100);"></a></td>
			<td width="5" align="center"></td>
			<%}%>
			<%if (Product_Img_Ext_1 != "/images/detail_no_pic.gif"){%>
			<td width="40" height="40" align="center" class="img_border"><a href="/product/images.aspx?product_id=<%=product_id%>&img=<%=Product_Img_Ext_1%>"><img src="<%=S_Product_Img_Ext_1%>" border="0" width="100" height="100" onload="javascript:AutosizeImage(this,100,100);"></a></td>
			<td width="5" align="center"></td>
			<%}%>
			<%if (Product_Img_Ext_2 != "/images/detail_no_pic.gif"){%>
			<td width="40" height="40" align="center" class="img_border"><a href="/product/images.aspx?product_id=<%=product_id%>&img=<%=Product_Img_Ext_2%>"><img src="<%=S_Product_Img_Ext_2%>" border="0" width="100" height="100" onload="javascript:AutosizeImage(this,100,100);"></a></td>
			<td width="5" align="center"></td>
			<%}%>
			<%if (Product_Img_Ext_3 != "/images/detail_no_pic.gif"){%>
			<td width="40" height="40" align="center" class="img_border"><a href="/product/images.aspx?product_id=<%=product_id%>&img=<%=Product_Img_Ext_3%>"><img src="<%=S_Product_Img_Ext_3%>" border="0" width="100" height="100" onload="javascript:AutosizeImage(this,100,100);"></a></td>
			<td width="5" align="center"></td>
			<%}%>
			<%if (Product_Img_Ext_4 != "/images/detail_no_pic.gif"){%>
			<td width="40" height="40" align="center" class="img_border"><a href="/product/images.aspx?product_id=<%=product_id%>&img=<%=Product_Img_Ext_4%>"><img src="<%=S_Product_Img_Ext_4%>" border="0" width="100" height="100" onload="javascript:AutosizeImage(this,100,100);"></a></td>
			<td width="5" align="center"></td>
			<%}%>
		  </tr>
		</table>
		</td>
	  </tr>
	  <tr>
        <td height="10" colspan="2" class="dotline_h"></td>
	  </tr>
	  <tr>
        <td align="center" colspan="2"><img src="<%=product_img%>" width="900" height="900" onload="javascript:AutosizeImage(this,900,900);"/></td>
	  </tr>
    </table>
         </div>
    </div>





























     
  <ucbottom:bottom runat="server" ID="Bottom" />
    
</body>
</html>
