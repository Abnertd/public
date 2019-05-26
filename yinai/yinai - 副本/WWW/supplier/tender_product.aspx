<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<% 
    
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    Supplier supplier = new Supplier();
    int productid = tools.CheckInt(Request["productid"]);
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="选择商品 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />


     <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script src="/scripts/jquery-extend-AdAdvance3.js" type="text/javascript"></script>
    <script type="text/javascript" src="/scripts/My97DatePicker/WdatePicker.js"></script>
    <link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>
    <style>
        #product_selectarea select { width:400px; height:38px; line-height:38px;}
    </style>
     <script>

         function ProductSelect()
         {
             var proid=<%=productid%>;
             var product=$("select option:selected").val();
             var productname=$("select option:checked").text();

             if(parseInt(product)>0)
             {
                 parent.Product(proid,product,productname);
             }
             dialog_loginhide();
         }
     function dialog_loginhide() {
         $("#html-div").css('left', '50%');
         $("#html-div").css('top', '50%');
         $("#html-div").hide();
         $("#mask_div").hide();
     }

 </script>
</head>
<body>

    <div class="b14_1_main" style="width:600px; margin-top:10px;">

                            <table width="600" border="0" cellspacing="0" cellpadding="5" id="apply_1_content" class="table_padding_5">
                                <tr>
                                    <td width="200" class="name">产品检索
                                    </td>
                                    <td width="400">
                                        <input name="keyword" type="text" id="pkeyword" style="width: 400px;background:none; height:38px; border:none;" maxlength="100" value="请输入产品名称/编号搜索" style="color: #888888" onfocus="if (this.value==this.defaultValue) this.value=''" onblur="$('#product_selectarea').load('Product_Limit_do.aspx?action=searchproduct&keyword='+encodeURI($(this).val())+'&timer='+Math.random());if (this.value=='') this.value=this.defaultValue" />
                                    </td>
                                </tr>
                                <tr>
                                    <td width="200" class="name">选择产品
                                    </td>
                                    <td width="400">
                                        <span id="product_selectarea">
                                            <%=supplier.Select_Supplier_Product(0)%>
                                        </span>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="200" class="name">产品信息
                                    </td>
                                    <td width="400" id="product_area" style="line-height: 22px;"></td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <input name="action" type="hidden" id="action" value="save"><a href="javascript:void();" onclick="ProductSelect();" class="a13">确定</a></td>
                                </tr>
                            </table>


        </div>


</body>
</html>
