<%@ Page Language="C#" %>

<!DOCTYPE html>

<%
    Supplier supplier = new Supplier();
     %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>添加保证金标准</title>
    <link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
    <script src="/Scripts/jquery.js" type="text/javascript"></script>
</head>
<body>
    <div class="content_div">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
            <tr>
                <td class="content_title">
                    添加保证金标准
                </td>
            </tr>
            <tr>
                <td class="content_content">
                    <form id="formadd" name="formadd" method="post" action="/supplier/Supplier_Ｍargin_do.aspx">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
                        
                        <tr>
                            <td class="cell_title">
                                类型
                            </td>
                            <td class="cell_content">
                                <%=supplier.DisplaySupplierCertType(0)%>
                            </td>
                        </tr>

                        <tr>
                            <td class="cell_title">
                                保证金金额
                            </td>
                            <td class="cell_content"> 
                                <input type="text" id="Supplier_Margin_Amount" name="Supplier_Margin_Amount" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" value="0" /> 元
                            </td>
                        </tr>
                        
                    </table>
                    <table width="100%" border="0" cellspacing="0" cellpadding="5">
                        <tr>
                            <td align="right">
                                <input type="hidden" id="action" name="action" value="new" />
                                <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
                                <input name="button" type="button" class="bt_grey" id="button" value="返回" onmouseover="this.className='bt_orange';"
                                    onmouseout="this.className='bt_grey';" onclick="location = 'Supplier_margin_list.aspx';" />
                            </td>
                        </tr>
                    </table>
                    </form>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
