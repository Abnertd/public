<%@ Page Language="C#" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<% 
    
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    Supplier supplier = new Supplier();
    supplier.Supplier_Login_Check("/supplier/package_add.aspx");
    Addr addr = new Addr();
    if (tools.NullInt(Session["supplier_ishaveshop"]) == 0)
    {
        pub.Msg("error", "错误信息", "您还没有开通店铺", false, "/supplier/index.aspx");
    }
    Package myPackage = new Package();
    
    string searchtype = tools.CheckStr(Request.QueryString["searchtype"]);
    string keyword = Request["keyword"];
    int product_cate = tools.CheckInt(Request["product_cate"]);
    if (product_cate == 0) { product_cate = tools.CheckInt(Request["product_cate_parent"]); }
    
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="添加捆绑 - 会员中心 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script src="/scripts/common.js" type="text/javascript"></script>
    <script src="/scripts/jquery-extend-AdAdvance3.js" type="text/javascript"></script>
    
    <script type="text/javascript" src="/scripts/supplier.js"></script>
    
    <script type="text/javascript">
        function product_add(obj) {
            $.ajax({
                url: encodeURI("package_do.aspx?action=check_product&product_id=" + SelectedValue(MM_findObj(obj)) + "&timer=" + Math.random()),
                type: "get",
                global: false,
                async: false,
                dataType: "html",
                success: function(data) {
                    window.opener.$("#yhnr").html(data);
                    window.close();
                },
                error: function() {
                    alert("Error Script");
                }
            });
        }

        function SelectedValue(obj) {
            var _channel = "";
            for (var i = 0; i < obj.length; i++) {
                if (obj[i].checked) {
                    if (_channel.length == 0) {
                        _channel = obj[i].value;
                    }
                    else {
                        _channel = _channel + "," + obj[i].value;
                    }
                }
            }
            if (obj.length == null) {
                _channel = obj.value;
            }
            return _channel
        }
        
        function CheckAll(form) {
            for (var i = 0; i < form.elements.length; i++) {
                var e = form.elements[i];
                if (e.name != 'chkall' && e.id != 'orders_idn') {
                    e.checked = form.chkall.checked;
                }
            }
        }
    </script>
   
</head>
<body style="margin:10px;">

    <form action="selectproduct.aspx" method="post" name="frm_sch" id="frm_sch">
    <table width="100%" border="0" cellspacing="0" cellpadding="5">
        <tr bgcolor="#F5F9FC">
            <td align="right">
                <span class="left_nav">搜索</span> <span id="main_cate">
                    <%=supplier.Product_Category_Select(product_cate, "main_cate")%></span>
                <input type="text" name="keyword" size="70" id="keyword" onfocus="if(this.value=='输入商品编码、商品名称、通用名、生产企业、拼音首字母进行搜索'){this.value='';}"
                    value="<% =keyword %>">
                <input type="submit" name="btn_sch" class="btn_01" id="btn_sch" value="搜索" />
            </td>
        </tr>
    </table>
    </form>
    <% =myPackage.SelectProduct()%>
    
</body>
</html>
