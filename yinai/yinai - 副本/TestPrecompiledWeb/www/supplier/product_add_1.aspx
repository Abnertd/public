<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>

<% 
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    Supplier myApp = new Supplier();
    Addr addr = new Addr();
    myApp.Supplier_Login_Check("/supplier/Product_Add.aspx");
    string Product_CateID, Product_Code, Product_Name, Product_GroupID;
    int Product_TypeID, Product_Cate;

    Product_CateID = tools.CheckStr(Request.Form["Product_CateID"]);
    Product_TypeID = tools.CheckInt(Request.Form["Product_TypeID"]);
    Product_Cate = tools.CheckInt(Request.Form["Product_Cate"]);


  
    //Product_TypeID = tools.CheckInt(Request.Form["Product_TypeID"]);
    Product_Cate = tools.CheckInt(Request.Form["Product_Cate"]);

    CategoryInfo CategoryEntity = new Product() .GetCategoryByID(Product_Cate);
    if (CategoryEntity != null)
    {
        Product_TypeID = CategoryEntity.Cate_TypeID;
    }
    
    
    
    Product_GroupID = tools.CheckStr(Request.Form["Product_GroupID"]);
    Product_Code = tools.CheckStr(Request.Form["Product_Code"]);
    Product_Name = tools.CheckStr(Request.Form["Product_Name"]);
    string Product_State_Name = "";
    string Product_City_Name = "";
    string Product_County_Name = "";
    if (Product_Cate == 0)
    {
        Product_Cate = tools.CheckInt(Request.Form["Product_Cate_parent"]);
    }

    if (Product_Cate == 0)
    {
        pub.Msg("error", "错误信息", "请选择商品主分类！", false, "{back}");
    }

    //if (Product_TypeID == 0)
    //{
    //    pub.Msg("error", "错误信息", "请选择商品所属类型！", false, "{back}");
    //}


    if (Product_Name.Length == 0)
    {
        pub.Msg("error", "错误信息", "请填写商品名称！", false, "{back}");
    }

    Session["selected_productid"] = "0";

   
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="发布产品 - 我是卖家 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />


    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index_newadd.css" rel="stylesheet" type="text/css" />
  <%--  <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js"></script>--%>
    
   <!--layer弹出框.js .css引用 开始-->
 <link href="../layer.m/layer.m.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery-1.5.1.min.js" charset="gb2312"></script>
       <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js" ></script>
  <!--layer弹出框.js .css引用 结束-->

    <script type="text/javascript" src="/scripts/common.js"></script>
    <script type="text/javascript" src="/scripts/supplier.js"></script>
    <script type="text/javascript" src="/Public/kindeditor/kindeditor-all.js"></script>
    <script type="text/javascript" src="/Public/kindeditor/lang/zh_CN.js"></script>
    <link rel="stylesheet" type="text/css" href="/scripts/treeview/dhtmlxtree.css" />
    <script src="/scripts/treeview/dhtmlxcommon.js" type="text/javascript"></script>
    <script src="/scripts/treeview/dhtmlxtree.js" type="text/javascript"></script>
    <script type="text/javascript">
        function openUpload(openObj) {

            $("#td_upload").show();

            $("#iframe_upload").attr("src", "<% =Application["Upload_Server_URL"]%>/public/uploadify.aspx?App=product&formname=formadd&frmelement=" + openObj + "&rtvalue=1&rturl=<% =Application["Upload_Server_Return_WWW"]%>");
        }

        function delImage(openObj) {
            $("#img_" + openObj).attr("src", "/images/detail_no_pic.gif");
            $("#" + openObj).val("/images/detail_no_pic.gif");
        }
    </script>

    <!--弹出菜单 start-->
    <script type="text/javascript">
        $(document).ready(function () {
            var byt = $(".testbox li");
            var box = $(".boxshow")
            byt.hover(
                 function () {
                     $(this).find(".boxshow").show(); $(this).find(".a3").attr("class", "a3 a3h");
                 },
                function () {
                    $(this).find(".boxshow").hide(); $(this).find(".a3h").attr("class", "a3");
                }
            );
        });
        function ChangeUnitCase() {
            var a = document.getElementById("Product_Unit").value
          
            if (a == 0) {
                $("#Product_Unit_Name").html("");
                $("#Product_Unit_Name1").html("");
                $("#Product_Unit_Name2").html("");
                $("#Product_Unit_Name3").html("");
                $("#Product_Unit_Name4").html(""); 
                $("#Product_Unit_Name_weight").html("");
            }
            else {
                $("#Product_Unit_Name").html(a);
                $("#Product_Unit_Name1").html(a);
                $("#Product_Unit_Name2").html(a);
                $("#Product_Unit_Name3").html(a);
                $("#Product_Unit_Name4").html(a);
                $("#Product_Unit_Name_weight").html(a);
            }
        }
    </script>
    <!--弹出菜单 end-->
       <!--示范一个公告层 开始-->
  <script type="text/javascript">
      function SignUpNow() {
          layer.open({
              type: 2
 , title: false //不显示标题栏
              //, closeBtn: false
 , area: ['480px;', '340px']

 , shade: 0.8
 , id: 'LAY_layuipro' //设定一个id，防止重复弹出
 , resize: false
 , btnAlign: 'c'
 , moveType: 1 //拖拽模式，0或者1              
              , content: ("/Bid/SignUpPopup.aspx")
          });
      }
    </script>
   <!--示范一个公告层 结束-->

    <style type="text/css">
        .txt_input_border {
            float: none;
            width: 150px;
            height: 20px;
            line-height: 20px;
            padding: 4px 20px 4px 3px;
            border: 1px solid #cccccc;
            font-size: 14px;
            font-family: "微软雅黑";
        }

        .txt_select_border {
            width: 150px;
            height: 25px;
            line-height: 25px;
            padding: 4px 5px 4px 3px;
        }

        .blk07_sz h2 .list02 {
            background-color: #fff;
            border-top: 1px solid #dddddd;
            border-right: 1px solid #dddddd;
        }

        .input01 {
            background-image: none;
        }

        #table_extend {
            margin: 0px 10px;
        }

            #table_extend td {
                padding: 5px;
            }
    </style>

</head>
<body>

    <uctop:top ID="top1" runat="server" />

    <!--主体 开始-->
    <div class="webwrap">
        <div class="content02" style="background-color: #FFF;">

            <!--位置说明 开始-->
            <div class="position">当前位置 > <a href="/supplier/">我是卖家 > </a><strong>添加商品</strong></div>
            <!--位置说明 结束-->
            <div class="partd_1">
                <div class="pd_left">
                    <%myApp.Get_Supplier_Left_HTML(3, 1); %>
                </div>
                <div class="pd_right">
                    <div class="blk14_1" style="margin-top: 0px;">
                        <h2>添加商品</h2>
                        <div class="blk07_sz">
                            <h2>
                                <ul class="list02">
                                    <li id="apply_1" class="on" onclick="Set_Tab('apply',1,6,'on','');">基本信息</li>
                                  <%--  <li id="apply_2" onclick="Set_Tab('apply',2,6,'on','');">扩展属性</li>
                                    <li id="apply_3" onclick="Set_Tab('apply',3,6,'on','');">图片信息</li>--%>
                                    <li id="apply_4" onclick="Set_Tab('apply',4,6,'on','');">介绍信息</li>
                                
                                </ul>
                                <div class="clear"></div>
                            </h2>

                            <form name="formadd" id="formadd" method="post" action="/supplier/product_do.aspx">

                                <div class="b07_main_sz" id="apply_1_content">
                                     <div class="b07_info04_sz" style="display: normal;margin-left:50px">
                                           <span style="font-size:18px;color:#ff6600">基础信息</span>
                                        <table width="793" border="0" cellspacing="0" cellpadding="0">
                                            <%--  <tr>
                                                <td width="138" class="name">产品品牌：</td>
                                                <td width="755">
                                                    <select name="Product_BrandID" id="Product_BrandID" style="width: 160px;">
                                                        <option value="0">选择商品品牌</option>
                                                        <% =myApp.ProductBrandOption(Product_TypeID, 0)%>
                                                    </select>
                                                    <i>*</i></td>
                                            </tr>--%>
                                            <%--<tr>

                                                <td width="138" class="name">商品产地：</td>
                                                <td class="cell_content">
                                                    <%=addr.SelectProudctAddress("div_area","Product_Region","product_city","product_county","","","") %>
                                                    <input type="hidden" id="Product_Region" name="Product_Region" /></td>
                                            </tr>--%>


                                            <tr>
                                                <td width="138" class="name">商品产地</td>
                                                <td class="cell_content" id="div_area">
                                                    <%=addr.SelectProductState("div_area","Product_State_Name","Product_City_Name","Product_County_Name",Product_State_Name,Product_City_Name,Product_County_Name) %> </td>
                                                <td>
                                                    <input type="hidden" id="Product_State_Name" name="Product_State_Name" value="<%=Product_State_Name %>" />
                                                    <input type="hidden" id="Product_City_Name" name="Product_City_Name" value="<%=Product_City_Name %>" />
                                                    <input type="hidden" id="Product_County_Name" name="Product_County_Name" value="<%=Product_County_Name %>" /></td>
                                            </tr>
                                            <tr>
                                                <td class="name">产品名称：</td>
                                                <td>
                                                    <input name="Product_Name" type="text" id="Product_Name" style="width: 300px;" class="input01" value="<%=Product_Name %>" /><i>*</i></td>
                                            </tr>

                                            <tr>
                                                <%-- <td class="name">产品编号：</td>--%>
                                                <td style="display: none">
                                                    <input name="Product_Code" type="text" id="Product_Code" style="width: 300px;" class="input01" value="<%=Product_Code %>" /><i>*</i></td>
                                            </tr>

                                          <%--  <tr>
                                                <td class="name">规格型号：</td>
                                                <td>
                                                    <input name="Product_Spec" type="text" id="Product_Spec" style="width: 300px;" class="input01" />
                                                    <input type="hidden" name="Product_PriceType" value="1" />
                                                </td>
                                            </tr>--%>
                                            <%--<tr>
                                            <td class="name">计价方式：</td>
                                            <td>
                                                <input type="radio" id="Product_PriceType1" name="Product_PriceType" value="1" checked="checked" onclick="$('#tr_price').show(); $('#tr_price1').hide(); $('#tr_ManualFee').hide();" class="input02" />一口价
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" id="Product_PriceType2" name="Product_PriceType" value="2" onclick="$('#tr_price').hide(); $('#tr_ManualFee').show(); $('#tr_price1').show();" class="input02" />计价式</td>
                                        </tr>--%>

                                           <%--  <tr>
                                                <td class="name">商品单位</td>
                                                 <td id="selected_Unit">
                                                    <% =new Product() .Product_Unit_Select(0, "Product_Unit")%>
                                                       
                                                </td>


                                               
                                            </tr>--%>



                                              <tr>
                                                <td class="name">商品单位</td>
                                                 <td id="selected_Unit">
                                                <%--    <% =new Product() .Product_Unit_Select(0, "Product_Unit")%>--%>
                                                       <input name="Product_Unit" type="text" id="Product_Unit" style="width: 300px;" value="" class="input01"  onmousemove="ChangeUnitCase();" onblur="ChangeUnitCase();" />
                                                </td>


                                                 <%--  <td>
                                                    <input name="Product_Note" type="text" id="Text1" style="width: 300px;" class="input01" value="" /></td>--%>
                                            </tr>


                                            <tr id="tr_price">
                                                <td class="name">商品价格：</td>
                                                <td>
                                                    <input name="Product_Price" type="text" id="Product_Price" style="width: 300px;" class="input01" value="0" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" />
                                                    元/(单位:<span id="Product_Unit_Name"></span>)</td>
                                            </tr>

                                            <%--  <tr id="tr_ManualFee" style="display: none;">
                                            <td class="name">手工费：</td>
                                            <td>
                                                <input name="Product_ManualFee" type="text" id="Product_ManualFee"
                                                    style="width: 300px;" value="0" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" class="input01" onblur="GetProductPrice();" />
                                                元/g <i>*</i>
                                            </td>
                                        </tr>--%>
                                            <tr>
                                                <td class="name">商品重量：</td>
                                                <td>
                                                    <input name="Product_Weight" type="text" id="Product_Weight" style="width: 300px;" value="0" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" class="input01" onblur="GetProductPrice();" />  (单位:<span id="Product_Unit_Name_weight"></span>)</td>
                                            </tr>

                                          


                                            <%if (myApp.ProductTagIsExist(0))
                                              {                                                 
                                            %>
                                            <tr>
                                                <td class="name">产品推荐：</td>
                                                <td align="left">
                                                    <%=myApp.ProductTagChoose(0)%>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td colspan="2" align="left" style="padding-left: 150px; color: red; font-size: 12px;">说明: 通过选中所属标签,来控制商品在该标签产品展示区域是否显示 </td>
                                            </tr>
                                            <%} %>




                                            <tr>
                                                <td class="name">商品交货周期：</td>
                                                <td>
                                                    <input name="U_Product_DeliveryCycle" type="text" id="U_Product_DeliveryCycle" style="width: 300px;" class="input01" value="" />单位:天</td>
                                            </tr>


                                            <%--  <tr id="tr_price1" style="display: none;">
                                                <td class="name">价格预览：</td>
                                                <td>
                                                    <span id="span_price"><%=pub.FormatCurrency(0) %></span>
                                                </td>
                                            </tr>--%>

                                            <%--  <tr>
                                                <td class="name">规格型号：</td>                                          

                                                <td>
                                                    <input name="Product_Spec" type="text" id="Product_Spec" style="width: 300px;" class="input01" value="" /></td>
                                            </tr>--%>


                                            <tr>
                                            <%--    <td class="name">产品备注：</td>--%>
                                                <td class="name">产品说明：</td>

                                                <td>
                                                    <input name="Product_Note" type="text" id="Product_Note" style="width: 300px;" class="input01" value="" /></td>
                                            </tr>

                                            <tr>
                                                <td class="name">是否上架：</td>
                                                <td>
                                                    <input name="Product_IsInsale" type="radio" id="Product_IsInsale1" value="1" checked="checked" class="input02" />是<input type="radio" name="Product_IsInsale" id="Product_IsInsale2" value="0" class="input02" />否
                                            <i>*</i></td>
                                            </tr>


                                            <%-- <tr>
                                            <td class="name">排序：</td>
                                            <td>
                                                <input name="Product_Sort" type="text" id="Text1" style="width: 300px;" value="1" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" class="input01" /></td>
                                        </tr>--%>
                                            <tr>
                                                <td class="name">产品库存：</td>
                                                <td>
                                                    <input name="Product_StockAmount" type="text" id="Product_StockAmount" class="txt_border"
                                                        size="10" maxlength="10" value="0" onkeyup="if(isNaN(value))execCommand('undo')"
                                                        onafterpaste="if(isNaN(value))execCommand('undo')" />
                                                  <%--  <i>*</i><span id="stockamount_tip"></span>--%>
                                                     (单位:<span id="Product_Unit_Name2"></span>)<i>*</i><span id="stockamount_tip"></span>
                                                </td>
                                            </tr>


                                            <tr>
                                                <td class="name">排序：</td>
                                                <td>
                                                    <input name="Product_Sort" type="text" id="Product_Sort" style="width: 300px;" value="1" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" class="input01" /></td>
                                            </tr>
                                            <tr>
                                                <td class="name">起订量 ≥</td>
                                                <td>
                                                    <input name="U_Product_MinBook" type="text" id="U_Product_MinBook" style="width: 300px;" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" value="1" class="input01" />(单位:<span id="Product_Unit_Name3"></span>)<i>*</i></td>
                                            </tr>
                                        </table>
                                    </div>
                              
                                  <div class="b07_main" style="display: normal;margin-left:90px">
                                        <span style="font-size:18px; color:#ff6600">扩展属性</span>
                          
                             <%--      <div class="b07_main_sz" id="apply_2_content" style="display: normal;">--%>
                                    <div class="b07_info04">
                                        <%=myApp.ProductExtendEditor(Product_TypeID,0) %>
                                    </div>

                                    <%-- <div class="blk08" id="yhnr" style="margin: 15px 10px 15px 10px; width: 953px;">
                                        <%=myApp.ProductExtendCheckboxDisplay(Product_TypeID, "0","add")%>
                                        <div id="extend_div" style="display: none;"><%=myApp.ProductExtendEditorDisplay() %></div>
                                    </div>--%>
                                </div>

                        
                              <div class="b07_main"  style="display: normal; margin:50px 0;margin-left:90px;">
                                         <span style="font-size:18px; color:#ff6600">图片信息</span>
                                    <table width="793" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td width="20%">
                                                <table border="0" width="100%" cellpadding="3" cellspacing="0">
                                                    <tr>
                                                        <td align="center" height="120">
                                                            <img id="img_product_img" src="<%=pub.FormatImgURL("","fullpath") %>" width="120" height="120" onload="javascript:AutosizeImage(this,120,120);">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" height="30">
                                                            <input type="button" name="btn_upload" value="上传" class="smb_btn" onclick="javascript: openUpload('product_img');" />
                                                            <input type="button" name="btn_upload" value="删除" class="smb_btn" onclick="javascript: delImage('product_img');"><input type="hidden" name="product_img" id="product_img" value="/images/detail_no_pic.gif" />
                                                        </td>
                                                    </tr>

                                                </table>
                                            </td>
                                            <td width="20%">
                                                <table border="0" width="100%" cellpadding="3" cellspacing="0">
                                                    <tr>
                                                        <td align="center" height="120">
                                                            <img id="img_product_img_ext_1" src="<%=pub.FormatImgURL("","fullpath") %>" width="120" height="120" onload="javascript:AutosizeImage(this,120,120);">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" height="30">
                                                            <input type="button" name="btn_upload" value="上传" class="smb_btn" onclick="javascript: openUpload('product_img_ext_1');" />
                                                            <input type="button" name="btn_upload" value="删除" class="smb_btn" onclick="javascript: delImage('product_img_ext_1');"><input type="hidden" name="product_img_ext_1" id="product_img_ext_1" value="/images/detail_no_pic.gif" />
                                                        </td>
                                                    </tr>

                                                </table>
                                            </td>
                                            <td width="20%">
                                                <table border="0" width="100%" cellpadding="3" cellspacing="0">
                                                    <tr>
                                                        <td align="center" height="120">
                                                            <img id="img_product_img_ext_2" src="<%=pub.FormatImgURL("","fullpath") %>" width="120" height="120" onload="javascript:AutosizeImage(this,120,120);">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" height="30">
                                                            <input type="button" name="btn_upload" value="上传" class="smb_btn" onclick="javascript: openUpload('product_img_ext_2');" />
                                                            <input type="button" name="btn_upload" value="删除" class="smb_btn" onclick="javascript: delImage('product_img_ext_2');"><input type="hidden" name="product_img_ext_2" id="product_img_ext_2" value="/images/detail_no_pic.gif" />
                                                        </td>
                                                    </tr>

                                                </table>
                                            </td>
                                            <td width="20%">
                                                <table border="0" width="100%" cellpadding="3" cellspacing="0">
                                                    <tr>
                                                        <td align="center" height="120">
                                                            <img id="img_product_img_ext_3" src="<%=pub.FormatImgURL("","fullpath") %>" width="120" height="120" onload="javascript:AutosizeImage(this,120,120);">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" height="30">
                                                            <input type="button" name="btn_upload" value="上传" class="smb_btn" onclick="javascript: openUpload('product_img_ext_3');" />
                                                            <input type="button" name="btn_upload" value="删除" class="smb_btn" onclick="javascript: delImage('product_img_ext_3');"><input type="hidden" name="product_img_ext_3" id="product_img_ext_3" value="/images/detail_no_pic.gif" />
                                                        </td>
                                                    </tr>

                                                </table>
                                            </td>
                                            <td width="20%">
                                                <table border="0" width="100%" cellpadding="3" cellspacing="0">
                                                    <tr>
                                                        <td align="center" height="120">
                                                            <img id="img_product_img_ext_4" src="<%=pub.FormatImgURL("","fullpath") %>" width="120" height="120" onload="javascript:AutosizeImage(this,120,120);">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" height="30">
                                                            <input type="button" name="btn_upload" value="上传" class="smb_btn" onclick="javascript: openUpload('product_img_ext_4');" />
                                                            <input type="button" name="btn_upload" value="删除" class="smb_btn" onclick="javascript: delImage('product_img_ext_4');"><input type="hidden" name="product_img_ext_4" id="product_img_ext_4" value="/images/detail_no_pic.gif" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center; color: red; padding: 5px;">商品默认图片</td>
                                        </tr>

                                        <tr id="td_upload">
                                            <td colspan="5" style="padding-left: 20px;" align="left">图片上传：<iframe id="iframe_upload" src="" width="100%" height="90" frameborder="0" scrolling="no" align="absmiddle"></iframe>
                                                <br />
                                                <span class="t12_grey" style="height: 30px; line-height: 30px; color: red">图片上传：建议图片尺寸：800*800；支持格式：jpg、gif、png；大小不要超过3M；商品图片信息 </span>
                                            </td>

                                        </tr>

                                    </table>
                                </div>
  </div>


                                <div class="b07_main_sz" id="apply_4_content" style="display: none;">
                                   <%-- <div class="b07_main" id="apply_4_content" style="display: normal;margin:50px auto; margin-left:90px; ">--%>
                                  <%--   <span style="font-size:18px; color:#ff6600">介绍信息</span>--%>
                                    <div class="b07_info04">
                                        <table width="793" border="0" cellspacing="0" cellpadding="0">
                                            <tr>                                              
                                                <td width="755">
                                                    <iframe id="iframe2" src="<% =Application["Upload_Server_URL"]%>/public/uploadify.aspx?App=shoppages&formname=formadd&frmelement=Product_Intro&rtvalue=1&rturl=<% =Application["Upload_Server_Return_WWW"]%>" width="100%" height="90" frameborder="0" scrolling="no"></iframe>
                                                    <br />
                                                    <span class="t12_grey" style="height: 30px; line-height: 30px;">建议图片尺寸：宽度不要超过900,高度不限；支持格式：jpg、gif、png；大小不要超过3M</span>
                                                </td>
                                            </tr>
                                            <tr>                                              
                                                <td>
                                                    <textarea cols="80" id="Product_Intro" name="Product_Intro" rows="16"></textarea>
                                                    <script type="text/javascript">
                                                        var Product_IntroEditor;
                                                        KindEditor.ready(function (K) {
                                                            Product_IntroEditor = K.create('#Product_Intro', {
                                                                width: '100%',
                                                                height: '500px',
                                                                filterMode: false,
                                                                afterBlur: function () { this.sync(); }
                                                            });
                                                        });
                                                    </script>
                                                </td>
                                            </tr>


<%--                                            <tr>
                                                <td width="138" class="name">订购流程图片：
                                                </td>
                                                <td width="755">
                                                    <iframe id="iframe1" src="<% =Application["Upload_Server_URL"]%>/public/uploadify.aspx?App=shoppages&formname=formadd&frmelement=U_Product_Parameters&rtvalue=1&rturl=<% =Application["Upload_Server_Return_WWW"]%>" width="100%" height="90" frameborder="0" scrolling="no"></iframe>
                                                    <br />
                                                    <span class="t12_grey" style="height: 30px; line-height: 30px;">建议图片尺寸：宽度不要超过900,高度不限；支持格式：jpg、gif、png；大小不要超过3M</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="name">订购流程：
                                                </td>
                                                <td>
                                                    <textarea cols="80" id="U_Product_Parameters" name="U_Product_Parameters" rows="16"></textarea>
                                                    <script type="text/javascript">
                                                        var U_Product_ParametersEditor;
                                                        KindEditor.ready(function (K) {
                                                            U_Product_ParametersEditor = K.create('#U_Product_Parameters', {
                                                                width: '100%',
                                                                height: '500px',
                                                                filterMode: false,
                                                                afterBlur: function () { this.sync(); }
                                                            });
                                                        });
                                                    </script>
                                                </td>
                                            </tr>--%>
                                        </table>
                                    </div>
                                </div>
                            

                                <div class="b07_main_sz" id="apply_5_content" style="display: none;">
                                    <div class="b07_info04">
                                        <table width="793" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td width="138" class="name">精品库图片</td>
                                                <td width="755">
                                                    <iframe id="iframe3" src="<% =Application["Upload_Server_URL"]%>/public/uploadify.aspx?App=productgifts&formname=formadd&frmelement=Product_LibraryImg&rtvalue=1&rturl=<% =Application["Upload_Server_Return_WWW"]%>" width="100%" height="90" frameborder="0" scrolling="no"></iframe>
                                                    <br />
                                                    <span class="t12_grey" style="height: 30px; line-height: 30px;">建议图片尺寸：1900*800；支持格式：jpg、gif、png；大小不要超过3M</span>
                                                </td>
                                            </tr>
                                            <tr id="tr_Product_LibraryImg" style="display: none;">
                                                <td class="cell_title"></td>
                                                <td class="cell_content">
                                                    <img src="" id="img_Product_LibraryImg" alt="" onload="javascript:AutosizeImage(this,475,200);" width="475" height="200" />
                                                    <input type="hidden" id="Product_LibraryImg" name="Product_LibraryImg" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>

                                <div class="b07_main_sz" id="apply_6_content" style="display: none;">
                                    <div class="b07_info04">
                                        <table width="793" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td>批发价格设置
                                                </td>
                                                <td>

                                                    <script type="text/javascript">

                                                        function add_wholeprice() {
                                                            var maxnum = $("#maxnum_1").val();
                                                            maxnum = parseInt(String(maxnum));
                                                            $.ajaxSetup({ async: false });
                                                            var minamount = $("#max_product_wholeprice_amount_" + maxnum).val();
                                                            $("#gift_item").load("product_do.aspx?action=addwholeprice&minamount=" + minamount + "&maxnum=" + (maxnum + 1) + "&timer=" + Math.random());
                                                            $("#gift_more1").html($("#gift_more1").html() + $("#gift_item").html());
                                                            $("#maxnum_1").val(maxnum + 1);

                                                        }

                                                        function del_wholeprice(obj) {
                                                            $("#" + obj).html("");
                                                            $("#" + obj).hide();
                                                        }

                                                        function changeprice(obj) {

                                                            var a = document.getElementById("max_product_wholeprice_amount_" + obj).value;

                                                            a = parseInt(String(a));
                                                            obj = parseInt(String(obj)) + 1;
                                                            document.getElementById("min_product_wholeprice_amount_" + obj).value = a + 1;
                                                        }

                                                        function checkamount1() {
                                                            var min = $("#min_product_wholeprice_amount_1").val();
                                                            var max = $("#max_product_wholeprice_amount_1").val();
                                                            var price = $("#product_wholeprice_amount_1").val();
                                                            if (min > 0) {
                                                                if (max >= min || max == "0" || max == "") {
                                                                    if (price > 0) {
                                                                        return true;
                                                                    }
                                                                    else {
                                                                        return false;
                                                                    }
                                                                }
                                                                else {
                                                                    return false;
                                                                }
                                                            }
                                                            else {
                                                                return true;
                                                            }
                                                        }

                                                        function checkamount2() {
                                                            var min = $("#min_product_wholeprice_amount_2").val();
                                                            var max = $("#max_product_wholeprice_amount_2").val();
                                                            var price = $("#product_wholeprice_amount_2").val();

                                                            if (min > 0) {
                                                                if (max >= min || max == "0" || max == "") {
                                                                    if (price > 0) {
                                                                        return true;
                                                                    }
                                                                    else {
                                                                        return false;
                                                                    }
                                                                }
                                                                else {
                                                                    return false;
                                                                }
                                                            }
                                                            else {
                                                                return true;
                                                            }
                                                        }

                                                        function checkamount3() {
                                                            var min = $("#min_product_wholeprice_amount_3").val();
                                                            var max = $("#max_product_wholeprice_amount_3").val();
                                                            var price = $("#product_wholeprice_amount_3").val();
                                                            if (min > 0) {
                                                                if (max >= min || max == "0" || max == "") {
                                                                    if (price > 0) {
                                                                        return true;
                                                                    }
                                                                    else {
                                                                        return false;
                                                                    }
                                                                }
                                                                else {
                                                                    return false;
                                                                }
                                                            }
                                                            else {
                                                                return true;
                                                            }
                                                        }

                                                        function checkamount4() {
                                                            var min = $("#min_product_wholeprice_amount_4").val();
                                                            var max = $("#max_product_wholeprice_amount_4").val();
                                                            var price = $("#product_wholeprice_amount_4").val();
                                                            if (min > 0) {
                                                                if (max >= min || max == "0" || max == "") {
                                                                    if (price > 0) {
                                                                        return true;
                                                                    }
                                                                    else {
                                                                        return false;
                                                                    }
                                                                }
                                                                else {
                                                                    return false;
                                                                }
                                                            }
                                                            else {
                                                                return true;
                                                            }
                                                        }

                                                        function checkamount5() {
                                                            var min = $("#min_product_wholeprice_amount_5").val();
                                                            var max = $("#max_product_wholeprice_amount_5").val();
                                                            var price = $("#product_wholeprice_amount_5").val();
                                                            if (min > 0) {
                                                                if (max >= min || max == "0" || max == "") {
                                                                    if (price > 0) {
                                                                        return true;
                                                                    }
                                                                    else {
                                                                        return false;
                                                                    }
                                                                }
                                                                else {
                                                                    return false;
                                                                }
                                                            }
                                                            else {
                                                                return true;
                                                            }
                                                        }

                                                        function checkamount6() {
                                                            var min = $("#min_product_wholeprice_amount_6").val();
                                                            var max = $("#max_product_wholeprice_amount_6").val();
                                                            var price = $("#product_wholeprice_amount_6").val();
                                                            if (min > 0) {
                                                                if (max >= min || max == "0" || max == "") {
                                                                    if (price > 0) {
                                                                        return true;
                                                                    }
                                                                    else {
                                                                        return false;
                                                                    }
                                                                }
                                                                else {
                                                                    return false;
                                                                }
                                                            }
                                                            else {
                                                                return true;
                                                            }
                                                        }

                                                        function checkamount7() {
                                                            var min = $("#min_product_wholeprice_amount_7").val();
                                                            var max = $("#max_product_wholeprice_amount_7").val();
                                                            var price = $("#product_wholeprice_amount_7").val();
                                                            if (min > 0) {
                                                                if (max >= min || max == "0" || max == "") {
                                                                    if (price > 0) {
                                                                        return true;
                                                                    }
                                                                    else {
                                                                        return false;
                                                                    }
                                                                }
                                                                else {
                                                                    return false;
                                                                }
                                                            }
                                                            else {
                                                                return true;
                                                            }
                                                        }

                                                        function checkamount8() {
                                                            var min = $("#min_product_wholeprice_amount_8").val();
                                                            var max = $("#max_product_wholeprice_amount_8").val();
                                                            var price = $("#product_wholeprice_amount_8").val();
                                                            if (min > 0) {
                                                                if (max >= min || max == "0" || max == "") {
                                                                    if (price > 0) {
                                                                        return true;
                                                                    }
                                                                    else {
                                                                        return false;
                                                                    }
                                                                }
                                                                else {
                                                                    return false;
                                                                }
                                                            }
                                                            else {
                                                                return true;
                                                            }
                                                        }

                                                        function checkamount9() {
                                                            var min = $("#min_product_wholeprice_amount_9").val();
                                                            var max = $("#max_product_wholeprice_amount_9").val();
                                                            var price = $("#product_wholeprice_amount_9").val();
                                                            if (min > 0) {
                                                                if (max >= min || max == "0" || max == "") {
                                                                    if (price > 0) {
                                                                        return true;
                                                                    }
                                                                    else {
                                                                        return false;
                                                                    }
                                                                }
                                                                else {
                                                                    return false;
                                                                }
                                                            }
                                                            else {
                                                                return true;
                                                            }
                                                        }

                                                        function checkamount10() {
                                                            var min = $("#min_product_wholeprice_amount_10").val();
                                                            var max = $("#max_product_wholeprice_amount_10").val();
                                                            var price = $("#product_wholeprice_amount_10").val();
                                                            if (min > 0) {
                                                                if (max >= min || max == "0" || max == "") {
                                                                    if (price > 0) {
                                                                        return true;
                                                                    }
                                                                    else {
                                                                        return false;
                                                                    }
                                                                }
                                                                else {
                                                                    return false;
                                                                }
                                                            }
                                                            else {
                                                                return true;
                                                            }
                                                        }

                                                        function checkamount() {
                                                            var check1 = checkamount1();
                                                            var check2 = checkamount2();
                                                            var check3 = checkamount3();
                                                            var check4 = checkamount4();
                                                            var check5 = checkamount5();
                                                            var check6 = checkamount6();
                                                            var check7 = checkamount7();
                                                            var check8 = checkamount8();
                                                            var check9 = checkamount9();
                                                            var check10 = checkamount10();

                                                            if (check1 && check2 && check3 && check4 && check5 && check6 && check7 && check8 && check9 && check10) {
                                                                return true;
                                                            }
                                                            else {
                                                                alert("批发价格数量定义错误");
                                                                return false;
                                                            }
                                                        }
                                                    </script>

                                                    <div id="product_wholeprice_1_1">
                                                        <div style="display: inline-block; width: 60px; text-align: right;">数量1：</div>
                                                        <input type="text" name="min_product_wholeprice_amount_1" id="min_product_wholeprice_amount_1"
                                                            onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')"
                                                            size="10" autocomplete="off" />&nbsp;&nbsp; 
                                                    <div style="display: inline-block; width: 60px; text-align: right;">数量1：</div>
                                                        <input type="text" id="max_product_wholeprice_amount_1"
                                                            name="max_product_wholeprice_amount_1" onkeyup="if(isNaN(value))execCommand('undo');changeprice('1');"
                                                            onafterpaste="if(isNaN(value))execCommand('undo')" size="10" autocomplete="off" />&nbsp;&nbsp;
                                                    <div style="display: inline-block; width: 60px; text-align: right;">价格1：</div>
                                                        <input type="text" name="product_wholeprice_amount_1" id="product_wholeprice_amount_1" onkeyup="if(isNaN(value))execCommand('undo')"
                                                            onafterpaste="if(isNaN(value))execCommand('undo')" size="10" autocomplete="off" value="" />
                                                    </div>
                                                    <br />
                                                    <div id="Div1">
                                                        <div style="display: inline-block; width: 60px; text-align: right;">数量2：</div>
                                                        <input type="text" readonly="readonly" id="min_product_wholeprice_amount_2" name="min_product_wholeprice_amount_2"
                                                            onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')"
                                                            size="10" autocomplete="off" />&nbsp;&nbsp; 
                                                    <div style="display: inline-block; width: 60px; text-align: right;">数量2：</div>
                                                        <input type="text" id="max_product_wholeprice_amount_2"
                                                            name="max_product_wholeprice_amount_2" onkeyup="if(isNaN(value))execCommand('undo');changeprice('2');"
                                                            onafterpaste="if(isNaN(value))execCommand('undo')" size="10" autocomplete="off" />&nbsp;&nbsp;
                                                <div style="display: inline-block; width: 60px; text-align: right;">价格2：</div>
                                                        <input type="text" name="product_wholeprice_amount_2" id="product_wholeprice_amount_2" onkeyup="if(isNaN(value))execCommand('undo')"
                                                            onafterpaste="if(isNaN(value))execCommand('undo')" size="10" autocomplete="off" />
                                                    </div>
                                                    <br />
                                                    <div id="Div2">
                                                        <div style="display: inline-block; width: 60px; text-align: right;">数量3：</div>
                                                        <input type="text" readonly="readonly" id="min_product_wholeprice_amount_3" name="min_product_wholeprice_amount_3"
                                                            onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')"
                                                            size="10" autocomplete="off" />&nbsp;&nbsp; 
                                                    <div style="display: inline-block; width: 60px; text-align: right;">数量3：</div>
                                                        <input type="text" id="max_product_wholeprice_amount_3"
                                                            name="max_product_wholeprice_amount_3" onkeyup="if(isNaN(value))execCommand('undo');changeprice('3');"
                                                            onafterpaste="if(isNaN(value))execCommand('undo')" size="10" autocomplete="off" />&nbsp;&nbsp;
                                                <div style="display: inline-block; width: 60px; text-align: right;">价格3：</div>
                                                        <input type="text" name="product_wholeprice_amount_3" id="product_wholeprice_amount_3" onkeyup="if(isNaN(value))execCommand('undo')"
                                                            onafterpaste="if(isNaN(value))execCommand('undo')" size="10" autocomplete="off" />
                                                    </div>
                                                    <br />
                                                    <div id="Div3">
                                                        <div style="display: inline-block; width: 60px; text-align: right;">数量4：</div>
                                                        <input type="text" readonly="readonly" id="min_product_wholeprice_amount_4" name="min_product_wholeprice_amount_4"
                                                            onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')"
                                                            size="10" autocomplete="off" />&nbsp;&nbsp; 
                                                    <div style="display: inline-block; width: 60px; text-align: right;">数量4：</div>
                                                        <input type="text" id="max_product_wholeprice_amount_4"
                                                            name="max_product_wholeprice_amount_4" onkeyup="if(isNaN(value))execCommand('undo');changeprice('4');"
                                                            onafterpaste="if(isNaN(value))execCommand('undo')" size="10" autocomplete="off" />&nbsp;&nbsp;
                                                <div style="display: inline-block; width: 60px; text-align: right;">价格4：</div>
                                                        <input type="text" name="product_wholeprice_amount_4" id="product_wholeprice_amount_4" onkeyup="if(isNaN(value))execCommand('undo')"
                                                            onafterpaste="if(isNaN(value))execCommand('undo')" size="10" autocomplete="off" />
                                                    </div>
                                                    <br />
                                                    <div id="Div4">
                                                        <div style="display: inline-block; width: 60px; text-align: right;">数量5：</div>
                                                        <input type="text" readonly="readonly" id="min_product_wholeprice_amount_5" name="min_product_wholeprice_amount_5"
                                                            onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')"
                                                            size="10" autocomplete="off" />&nbsp;&nbsp; 
                                                    <div style="display: inline-block; width: 60px; text-align: right;">数量5：</div>
                                                        <input type="text" id="max_product_wholeprice_amount_5"
                                                            name="max_product_wholeprice_amount_5" onkeyup="if(isNaN(value))execCommand('undo');changeprice('5');"
                                                            onafterpaste="if(isNaN(value))execCommand('undo')" size="10" autocomplete="off" />&nbsp;&nbsp;
                                                <div style="display: inline-block; width: 60px; text-align: right;">价格5：</div>
                                                        <input type="text" name="product_wholeprice_amount_5" id="product_wholeprice_amount_5" onkeyup="if(isNaN(value))execCommand('undo')"
                                                            onafterpaste="if(isNaN(value))execCommand('undo')" size="10" autocomplete="off" />
                                                    </div>
                                                    <br />
                                                    <div id="Div5">
                                                        <div style="display: inline-block; width: 60px; text-align: right;">数量6：</div>
                                                        <input type="text" readonly="readonly" id="min_product_wholeprice_amount_6" name="min_product_wholeprice_amount_6"
                                                            onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')"
                                                            size="10" autocomplete="off" />&nbsp;&nbsp; 
                                                    <div style="display: inline-block; width: 60px; text-align: right;">数量6：</div>
                                                        <input type="text" id="max_product_wholeprice_amount_6"
                                                            name="max_product_wholeprice_amount_6" onkeyup="if(isNaN(value))execCommand('undo');changeprice('6');"
                                                            onafterpaste="if(isNaN(value))execCommand('undo')" size="10" autocomplete="off" />&nbsp;&nbsp;
                                                <div style="display: inline-block; width: 60px; text-align: right;">价格6：</div>
                                                        <input type="text" name="product_wholeprice_amount_6" id="product_wholeprice_amount_6" onkeyup="if(isNaN(value))execCommand('undo')"
                                                            onafterpaste="if(isNaN(value))execCommand('undo')" size="10" autocomplete="off" />
                                                    </div>
                                                    <br />
                                                    <div id="Div6">
                                                        <div style="display: inline-block; width: 60px; text-align: right;">数量7：</div>
                                                        <input type="text" readonly="readonly" id="min_product_wholeprice_amount_7" name="min_product_wholeprice_amount_7"
                                                            onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')"
                                                            size="10" autocomplete="off" />&nbsp;&nbsp; 
                                                    <div style="display: inline-block; width: 60px; text-align: right;">数量7：</div>
                                                        <input type="text" id="max_product_wholeprice_amount_7"
                                                            name="max_product_wholeprice_amount_7" onkeyup="if(isNaN(value))execCommand('undo');changeprice('7');"
                                                            onafterpaste="if(isNaN(value))execCommand('undo')" size="10" autocomplete="off" />&nbsp;&nbsp;
                                                <div style="display: inline-block; width: 60px; text-align: right;">价格7：</div>
                                                        <input type="text" name="product_wholeprice_amount_7" id="product_wholeprice_amount_7" onkeyup="if(isNaN(value))execCommand('undo')"
                                                            onafterpaste="if(isNaN(value))execCommand('undo')" size="10" autocomplete="off" />
                                                    </div>
                                                    <br />
                                                    <div id="Div7">
                                                        <div style="display: inline-block; width: 60px; text-align: right;">数量8：</div>
                                                        <input type="text" readonly="readonly" id="min_product_wholeprice_amount_8" name="min_product_wholeprice_amount_8"
                                                            onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')"
                                                            size="10" autocomplete="off" />&nbsp;&nbsp;
                                                    <div style="display: inline-block; width: 60px; text-align: right;">数量8：</div>
                                                        <input type="text" id="max_product_wholeprice_amount_8"
                                                            name="max_product_wholeprice_amount_8" onkeyup="if(isNaN(value))execCommand('undo');changeprice('8');"
                                                            onafterpaste="if(isNaN(value))execCommand('undo')" size="10" autocomplete="off" />&nbsp;&nbsp;
                                                <div style="display: inline-block; width: 60px; text-align: right;">价格8：</div>
                                                        <input type="text" name="product_wholeprice_amount_8" id="product_wholeprice_amount_8" onkeyup="if(isNaN(value))execCommand('undo')"
                                                            onafterpaste="if(isNaN(value))execCommand('undo')" size="10" autocomplete="off" />
                                                    </div>
                                                    <br />
                                                    <div id="Div8">
                                                        <div style="display: inline-block; width: 60px; text-align: right;">数量9：</div>
                                                        <input type="text" readonly="readonly" id="min_product_wholeprice_amount_9" name="min_product_wholeprice_amount_9"
                                                            onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')"
                                                            size="10" autocomplete="off" />&nbsp;&nbsp; 
                                                    <div style="display: inline-block; width: 60px; text-align: right;">数量9：</div>
                                                        <input type="text" id="max_product_wholeprice_amount_9"
                                                            name="max_product_wholeprice_amount_9" onkeyup="if(isNaN(value))execCommand('undo');changeprice('9');"
                                                            onafterpaste="if(isNaN(value))execCommand('undo')" size="10" autocomplete="off" />&nbsp;&nbsp;
                                                <div style="display: inline-block; width: 60px; text-align: right;">价格9：</div>
                                                        <input type="text" name="product_wholeprice_amount_9" id="product_wholeprice_amount_9" onkeyup="if(isNaN(value))execCommand('undo')"
                                                            onafterpaste="if(isNaN(value))execCommand('undo')" size="10" autocomplete="off" />
                                                    </div>
                                                    <br />
                                                    <div id="Div9">
                                                        <div style="display: inline-block; width: 60px; text-align: right;">数量10：</div>
                                                        <input type="text" readonly="readonly" id="min_product_wholeprice_amount_10"
                                                            name="min_product_wholeprice_amount_10" onkeyup="if(isNaN(value))execCommand('undo')"
                                                            onafterpaste="if(isNaN(value))execCommand('undo')" size="10" autocomplete="off" />&nbsp;&nbsp;
                                                    <div style="display: inline-block; width: 60px; text-align: right;">数量10：</div>
                                                        <input type="text" id="max_product_wholeprice_amount_10" name="max_product_wholeprice_amount_10"
                                                            onkeyup="if(isNaN(value))execCommand('undo');changeprice('10');" onafterpaste="if(isNaN(value))execCommand('undo')"
                                                            size="10" autocomplete="off" />&nbsp;&nbsp; 
                                                        <div style="display: inline-block; width: 60px; text-align: right;">价格10：</div>
                                                        <input type="text" name="product_wholeprice_amount_10"
                                                            id="product_wholeprice_amount_10" onkeyup="if(isNaN(value))execCommand('undo')"
                                                            onafterpaste="if(isNaN(value))execCommand('undo')" size="10" autocomplete="off" />
                                                    </div>
                                                    <span id="amount_tip"></span>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <div class="b07_main" style="padding: 0;">
                                    <div class="b07_info04">

                                        <table width="793" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td width="138">&nbsp;</td>
                                                <td width="755">
                                                    <a href="javascript:void();" onclick="product_save('new')" class="a11"></a>
                                                    <input name="action" type="hidden" id="action" value="new" />
                                                    <input name="Product_Cate" type="hidden" id="Product_Cate" value="<% =Product_Cate%>" />
                                                    <input name="Product_GroupID" type="hidden" id="Product_GroupID" value="<% =Product_GroupID%>" />
                                                    <input name="Product_CateID" type="hidden" id="Product_CateID" value="<% =Product_CateID%>" />
                                                    <input name="Product_TypeID" type="hidden" id="Product_TypeID" value="<% =Product_TypeID%>" />
                                                    <input name="Product_Extends" type="hidden" id="Product_Extends" value="" />
                                                    <input name="price_view" id="price_view" type="hidden" value="" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
                <div class="clear"></div>
            </div>
        </div>
    </div>
    <%--右侧浮动弹框 开始--%>
 <div id="leftsead">
        <ul>           
            <li>
                 <a href="javascript:void(0);" onclick="SignUpNow();">
                    <div class="hides" style="width: 130px;height:50px; display: none;" id="qq">
                        <div class="hides" id="p1">
                           <img src="/images/nav_1_1.png" />
                        </div>
                    </div>
                    <img src="/images/nav_1.png" width="57px" height="50px" class="shows" />
                </a>
            </li>
            <li id="tel">
                <a href="javascript:void(0)">
                    <div class="hides" style="width: 130px;height:50px; display: none;" id="tels">
                        <div class="hides" id="p2">
                            <img src="/images/nav_2_1.png">
                        </div>

                    </div>
                    <img src="/images/nav_2.png" width="57px" height="50px" class="shows" />
                </a>
            </li>
            <li id="btn">
              <a id="top_btn" href="http://wpa.qq.com/msgrd?v=3&uin=3558103267&site=qq&menu=yes" target="_blank">
                    <div class="hides" style="width: 130px;height:50px; display: none">
                        <div class="hides" id="p3">
                            <img src="/images/nav_3_1.png" width="130px;" height="50px" id="Img1" />
                        </div>
                    </div>
                    <img src="/images/nav_3.png" width="57px" height="50px" class="shows" />
                </a>
            </li>
            <li id="Li1">
                <a href="#top">
                    <div class="hides" style="width: 130px; display: none" id="Div10">
                        <div class="hides" id="p4">
                            <img src="/images/nav_4_1.png" width="130px;" height="50px" />
                        </div>
                    </div>
                    <img src="/images/nav_4.png" width="57px" height="50px" class="shows" />
                </a>
            </li>
        </ul>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#leftsead a").hover(function () {

                $(this).children("div.hides").show();
                $(this).children("img.shows").hide();
                $(this).children("div.hides").animate({ marginRight: '0px' }, '0');

            }, function () {
                $(this).children("div.hides").animate({ marginRight: '-130px' }, 0, function () { $(this).hide(); $(this).next("img.shows").show(); });
            });
            $("#top_btn").click(function () { if (scroll == "off") return; $("html,body").animate({ scrollTop: 0 }, 600); });
        });
    </script>
    <%--右侧浮动弹框 结束--%>
    <!--主体 结束-->



  <ucbottom:bottom runat="server" ID="Bottom" />

</body>
</html>
