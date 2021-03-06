﻿<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    private Product myApp;
    private Public myPub;
    string Product_Code = "0000000000";
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("a8dcfdfb-2227-40b3-a598-9643fd4c7e18");
        myApp = new Product();
        myPub = new Public();




        //if (!myApp.CheckProductCode(Product_Code, 0))
        //{
        //      Product_Code = myApp.GenerateRandomNumber(20);

        //}
        int j = 1;
        for (int i = 0; i < j; i++)
        {
            if (!myApp.CheckProductCode(Product_Code, 0))
            {
                Product_Code = myApp.GenerateRandomNumber(20);
                j++;
            }
        }
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>添加商品</title>
    <link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
    <script src="/Scripts/common.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="/scripts/treeview/dhtmlxtree.css" />
    <script src="/scripts/treeview/dhtmlxcommon.js" type="text/javascript"></script>
    <script src="/scripts/treeview/dhtmlxtree.js" type="text/javascript"></script>
    <script src="/scripts/product.js" type="text/javascript"></script>
</head>
<body>
    <div class="content_div">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
            <tr>
                <td class="content_title">添加商品</td>
            </tr>
            <tr>
                <td class="content_content">
                    <form id="formadd" name="formadd" method="post" action="/product/product_add_2.aspx" onsubmit="javascript:MM_findObj('Product_CateID').value = tree.getAllChecked();return checkform_step1();">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table" style="table-layout: fixed">
                            <tr>
                                <td class="cell_title" valign="top">主分类</td>
                                <td class="cell_content">

                                    <span id="main_cate"><%=myApp.Product_Category_Select(0, "main_cate")%></span>
                                    <span id="div_Product_Cate"></span>
                                </td>
                            </tr>
                            <tr>
                                <td class="cell_title" valign="top">附加分类</td>
                                <td class="cell_content">
                                    <table style="width: 100%; background: #f5f5f5; border: 1px solid Silver;">
                                        <tr>
                                            <td valign="top" id="treeboxbox_tree"></td>
                                        </tr>
                                    </table>
                                    <script type="text/javascript">
                                        tree = new dhtmlXTreeObject("treeboxbox_tree", "100%", "100%", 0);
                                        tree.setSkin('dhx_skyblue');
                                        tree.setImagePath("/scripts/treeview/imgs/csh_dhx_skyblue/");
                                        tree.enableCheckBoxes(1);
                                        tree.enableThreeStateCheckboxes(true);
                                        tree.loadXML("treedata.aspx");
                                    </script>
                                    <span id="div_Product_CateID"></span>
                                </td>
                            </tr>
                            <tr>
                                <td class="cell_title">产品参数</td>
                                <td class="cell_content">
                                    <select name="Product_TypeID" id="Product_TypeID">
                                        <option value="0">选择商品参数</option>
                                        <% =myApp.ProductTypeOption(0)%>
                                    </select>
                                    <span id="div_Product_TypeID"></span></td>
                            </tr>
                            <tr>
                                <%--   style="display:none;"--%>
                                <%--  <td class="cell_title">商品编码</td>--%>
                                <%-- <td class="cell_content" ><input name="Product_Code" type="text" id="Product_Code" size="50" maxlength="50" value="" onblur="check_product_code('Product_Code', 0);"/><span id="div_Product_Code"></span></td>--%>
                                <td class="cell_content" style="display: none">
                                    <input name="Product_Code" type="text" id="Product_Code" size="50" maxlength="50" value="<%=Product_Code%>" /><span id="div_Product_Code"></span></td>
                            </tr>
                            <tr>
                                <td class="cell_title">商品名称</td>
                                <td class="cell_content">
                                    <input name="Product_Name" type="text" id="Product_Name" size="50" maxlength="100" value="" onblur="check_product_name('Product_Name');" /><span id="div_Product_Name"></span></td>
                            </tr>
                        </table>
                        <table width="100%" border="0" cellspacing="0" cellpadding="5">
                            <tr>
                                <td align="right">
                                    <input name="Product_CateID" type="hidden" id="Product_CateID" value="" />
                                    <input name="save" type="submit" class="bt_orange" id="save" value="下一步" />
                                    <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location = 'product.aspx';" /></td>
                            </tr>
                        </table>
                    </form>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
