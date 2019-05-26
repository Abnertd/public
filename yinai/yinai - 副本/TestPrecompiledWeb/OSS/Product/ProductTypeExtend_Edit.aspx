<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="System.Collections.Generic" %>

<script runat="server">

    private ProductTypeExtend extend;
    private ITools tools;

    private int Extend_ID, Extend_Sort, Extend_IsActive, Display, Extend_Options, Extend_IsSearch, ProductType_ID, Extend_Gather, Extend_DisplayForm;
    private string Extend_Name, Extend_Site, Extend_Display, Extend_Default;

    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("847e8136-fd2f-4834-86b7-f2c984705eff");
        extend = new ProductTypeExtend();
        tools = ToolsFactory.CreateTools();

        Extend_ID = tools.CheckInt(Request.QueryString["Extend_ID"]);
        if (Extend_ID == 0)
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        ProductTypeExtendInfo entity = extend.GetProductTypeExtendByID(Extend_ID);
        if (entity == null)
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        else
        {
            Extend_ID = entity.ProductType_Extend_ID;
            ProductType_ID = entity.ProductType_Extend_ProductTypeID;
            Extend_Name = entity.ProductType_Extend_Name;
            Extend_Display = entity.ProductType_Extend_Display;
            Extend_IsSearch = entity.ProductType_Extend_IsSearch;
            Extend_Options = entity.ProductType_Extend_Options;
            Extend_Default = entity.ProductType_Extend_Default;
            Extend_IsActive = entity.ProductType_Extend_IsActive;
            Extend_Gather = entity.ProductType_Extend_Gather;
            Extend_DisplayForm = entity.ProductType_Extend_DisplayForm;
            Extend_Sort = entity.ProductType_Extend_Sort;
            Extend_Site = entity.ProductType_Extend_Site;

            if (Extend_Display == "Text")
            {
                Display = 1;
            }
            else
            {
                Display = 0;
            }

            switch (Extend_Display)
            {
                case "select":
                    Display = 0;
                    break;
                case "Text":
                    Display = 1;
                    break;
                case "html":
                    Display = 2;
                    break;
            }

        }
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
</head>
<body>
    <div class="content_div">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
            <tr>
                <td class="content_title">修改扩展属性</td>
            </tr>
            <tr>
                <td class="content_content">
                    <form id="formadd" name="formadd" method="post" action="/Product/Producttypeextend_Do.aspx">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
                            <tr>
                                <td class="cell_title">属性名称</td>
                                <td class="cell_content">
                                    <input name="ProductTypeExtend_Name" type="text" id="ProductTypeExtend_Name" value="<%=Extend_Name %>" size="50" maxlength="100" /></td>
                            </tr>
                            <tr>
                                <td class="cell_title">后台输入方式</td>
                                <td class="cell_content">
                                    <input name="ProductTypeExtend_Display" type="radio" id="ProductTypeExtend_Display1" value="1" <%=Public.CheckedRadio(Display.ToString(),"1") %> />文本框
                                    <input type="radio" name="ProductTypeExtend_Display" id="ProductTypeExtend_Display2" value="0" <%=Public.CheckedRadio(Display.ToString(),"0") %> />下拉菜单
                                    <%--<input type="radio" name="ProductTypeExtend_Display" id="ProductTypeExtend_Display2" value="2" <%=Public.CheckedRadio(Display.ToString(),"2") %> />HTML--%></td>
                            </tr>
                            <tr>
                                <td class="cell_title">筛选项</td>
                                <td class="cell_content">
                                    <input name="ProductType_Extend_IsSearch" type="radio" id="ProductType_Extend_IsSearch1" value="0" <%=Public.CheckedRadio(Extend_IsSearch.ToString(),"0") %> />否
                                    <input type="radio" name="ProductType_Extend_IsSearch" id="ProductType_Extend_IsSearch" value="1" <%=Public.CheckedRadio(Extend_IsSearch.ToString(),"1") %> />是 </td>
                            </tr>
                            <tr>
                                <td class="cell_title">属性选项</td>
                                <td class="cell_content">
                                    <%--<input name="ProductTypeExtend_Options" type="radio" id="ProductTypeExtend_Options1" value="1" <%=Public.CheckedRadio(Extend_Options.ToString(),"1") %> />客户仅可见 --%>
                                    <input type="radio" name="ProductTypeExtend_Options" id="ProductTypeExtend_Options2" value="2" <%=Public.CheckedRadio(Extend_Options.ToString(),"2") %> />客户选择
                                    <%--<input type="radio" name="ProductTypeExtend_Options" id="ProductTypeExtend_Options3" value="3" <%=Public.CheckedRadio(Extend_Options.ToString(),"3") %> />客户输入--%>
                                </td>
                            </tr>
                            <tr>
                                <td class="cell_title">启用该类型</td>
                                <td class="cell_content">
                                    <input name="ProductTypeExtend_IsActive" type="radio" id="ProductTypeExtend_IsActive1" value="1" <%=Public.CheckedRadio(Extend_IsActive.ToString(),"1") %> />是
                                    <input type="radio" name="ProductTypeExtend_IsActive" id="ProductTypeExtend_IsActive2" value="0" <%=Public.CheckedRadio(Extend_IsActive.ToString(),"0") %> />否 </td>
                            </tr>
                            <tr>
                                <td class="cell_title">前端聚合项</td>
                                <td class="cell_content">
                                    <input name="ProductType_Extend_Gather" type="radio" id="ProductType_Extend_Gather0" value="0" <%=Public.CheckedRadio(Extend_Gather.ToString(),"0") %> />否
                                    <input type="radio" name="ProductType_Extend_Gather" id="ProductType_Extend_Gather1" value="1" <%=Public.CheckedRadio(Extend_Gather.ToString(),"1") %> />主聚合项
                                    <input type="radio" name="ProductType_Extend_Gather" id="ProductType_Extend_Gather2" value="2" <%=Public.CheckedRadio(Extend_Gather.ToString(),"2") %> />从聚合项</td>
                            </tr>
                            <tr>
                                <td class="cell_title">前端聚合项表现形式</td>
                                <td class="cell_content">
                                    <input name="ProductType_Extend_DisplayForm" type="radio" id="ProductType_Extend_DisplayForm0" value="0" <%=Public.CheckedRadio(Extend_DisplayForm.ToString(),"0") %> />下拉菜单
                                    <input type="radio" name="ProductType_Extend_DisplayForm" id="ProductType_Extend_DisplayForm1" value="1" <%=Public.CheckedRadio(Extend_DisplayForm.ToString(),"1") %> />文字平铺
                                    <input type="radio" name="ProductType_Extend_DisplayForm" id="ProductType_Extend_DisplayForm2" value="2" <%=Public.CheckedRadio(Extend_DisplayForm.ToString(),"2") %> />图片平铺
                                    <input type="radio" name="ProductType_Extend_DisplayForm" id="ProductType_Extend_DisplayForm3" value="3" <%=Public.CheckedRadio(Extend_DisplayForm.ToString(),"3") %> />图文平铺</td>
                            </tr>
                            <tr>
                                <td class="cell_title">类型排序</td>
                                <td class="cell_content">
                                    <input name="ProductTypeExtend_Sort" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" type="text" id="ProductTypeExtend_Sort" value="<%=Extend_Sort %>" size="10" maxlength="10" />
                                    <span class="tip">数字越小越靠前</span></td>
                            </tr>
                            <tr>
                                <td class="cell_title">默认值</td>
                                <td class="cell_content">
                                    <textarea name="ProductTypeExtend_Default" rows="5" cols="50"><%=Extend_Default %></textarea>
                                    <span class="tip">各项间以“|”分隔</span></td>
                            </tr>
                        </table>
                        <table width="100%" border="0" cellspacing="0" cellpadding="5">
                            <tr>
                                <td align="right">
                                    <input type="hidden" id="action" name="action" value="renew" />
                                    <input type="hidden" id="Extend_ID" name="Extend_ID" value="<%=Extend_ID %>" />
                                    <input type="hidden" id="ProductType_ID" name="ProductType_ID" value="<%=ProductType_ID %>" />
                                    <input name="save" type="submit" class="bt_orange" id="save" value="保存属性" />
                                    <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="history.go(-1);" /></td>
                            </tr>
                        </table>
                    </form>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
