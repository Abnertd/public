<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>

<% 
    
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    Supplier supplier = new Supplier();
    ProductInfo product;
    PromotionLimitInfo entity;
    supplier.Supplier_Login_Check("/supplier/Product_Limit_List.aspx");

    int limit_id, Promotion_Limit_ProductID, limit_amount, limit_limit, group_id;
    string product_code, product_name, product_spec, product_maker, Promotion_Limit_Starttime, Promotion_Limit_Endtime, Grade_Str;
    double product_price, Promotion_Limit_Price;
    product_code = "";
    product_maker = "";
    product_spec = "";
    product_name = "";
    product_price = 0;
    Promotion_Limit_ProductID = 0;
    Promotion_Limit_Price = 0;
    Promotion_Limit_Starttime = "";
    Promotion_Limit_Endtime = "";
    group_id = 0;
    limit_id = tools.CheckInt(Request["limit_id"]);
    entity = supplier.GetPromotionLimitByID(limit_id);
    if (entity == null)
    {
        pub.Msg("error", "错误信息", "记录不存在", false, "{back}");
        Response.End();
    }
    else
    {
        if (entity.Promotion_Limit_GroupID == tools.NullInt(Session["supplier_id"]))
        {
            product = supplier.GetProductByID(entity.Promotion_Limit_ProductID);
            if (product != null)
            {
                product_code = product.Product_Code;
                product_name = product.Product_Name;
                product_spec = product.Product_Spec;
                product_maker = product.Product_Maker;
                product_price = product.Product_Price;
            }
            group_id = entity.Promotion_Limit_GroupID;
            Promotion_Limit_ProductID = entity.Promotion_Limit_ProductID;
            Promotion_Limit_Price = entity.Promotion_Limit_Price;
            Promotion_Limit_Starttime = entity.Promotion_Limit_Starttime.ToString("yyyy-MM-dd");
            Promotion_Limit_Endtime = entity.Promotion_Limit_Endtime.ToString("yyyy-MM-dd");
            limit_amount = entity.Promotion_Limit_Amount;
            limit_limit = entity.Promotion_Limit_Limit;
        }
    }
    
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="限时促销 - 会员中心 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />


    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />

    <link href="../css/index_newadd.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script type="text/javascript" src="/scripts/common.js"></script>
    <script src="/scripts/jquery-extend-AdAdvance3.js" type="text/javascript"></script>
    <script type="text/javascript" src="/scripts/My97DatePicker/WdatePicker.js"></script>
    <link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>
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
    </script>

    <!--弹出菜单 end-->
    <style type="text/css">
        .yz_blk19_main img {
            display: inline;
        }

        .blk17_sz table td input {
            background-image: none;
        }
    </style>
</head>
<body>

    <uctop:top ID="top1" runat="server" />


    <!--主体 开始-->
    <div class="webwrap">
        <div class="content02" style="margin-bottom: 20px;">

            <!--位置说明 开始-->
            <div class="position">
                您现在的位置 > <a href="/">首页</a> > <a href="/supplier/index.aspx">商家中心</a> > <a href="Product_Limit_List.aspx.aspx">限时促销</a>
            </div>
            <%--   <div class="clear"></div>--%>
            <!--位置说明 结束-->
            <div class="partd_1">
                <div class="pd_left">
                    <div class="menu_1">
                        <% supplier.Get_Supplier_Left_HTML(3, 5); %>
                    </div>
                </div>
                <div class="pd_right">
                    <div class="blk14_1" style="margin-top: 0px;">

                        <%--   <div class="title03">限时促销管理</div>--%>
                        <h2>限时促销管理</h2>
                        <div class="blk17_sz">

                            <form name="formadd" id="formadd" method="post" action="/supplier/Product_Limit_do.aspx">
                                <table width="100%" border="0" cellspacing="0" cellpadding="5" id="apply_1_content" class="table_padding_5">
                                    <tr>
                                        <td width="92" class="name">产品检索
                                        </td>
                                        <td width="801">
                                            <input name="keyword" type="text" id="pkeyword" style="width: 300px;" class="input01" maxlength="100" value="请输入产品名称/编号搜索" style="color: #888888" onfocus="if($(this).val()=='请输入产品名称/编号搜索'){$(this).val('');}" onblur="$('#product_selectarea').load('Product_Limit_do.aspx?action=searchproduct&keyword='+encodeURI($(this).val())+'&timer='+Math.random());" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="92" class="name">选择产品
                                        </td>
                                        <td width="801">
                                            <span id="product_selectarea">
                                                <%=supplier.Select_Supplier_Product(Promotion_Limit_ProductID)%>
                                            </span><i>*</i>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="92" class="name">产品信息
                                        </td>
                                        <td width="801" id="product_area">
                                            <%supplier.Show_ProductInfo(Promotion_Limit_ProductID); %>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="92" class="name">限时价格
                                        </td>
                                        <td width="801">
                                            <input name="Promotion_Limit_Price" type="text" id="Promotion_Limit_Price" style="width: 300px;" class="input01" maxlength="100" value="<%=Promotion_Limit_Price %>" /><i>*</i>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="92" class="name">开始时间
                                        </td>
                                        <td width="801">
                                            <input type="text" name="Promotion_Limit_Starttime" id="date_start1" maxlength="10" readonly="readonly" style="width: 195px;" onclick="WdatePicker({ skin: 'twoer', dateFmt: 'yyyy-MM-dd' })" class="Wdate input" value="<%=Promotion_Limit_Starttime %>" /><i>*</i>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="92" class="name">结束时间
                                        </td>
                                        <td width="801">
                                            <input type="text" name="Promotion_Limit_Endtime" id="date_end1" maxlength="10" readonly="readonly" value="<%=Promotion_Limit_Endtime %>" style="width: 195px;" onclick="WdatePicker({ skin: 'twoer', dateFmt: 'yyyy-MM-dd' })" class="Wdate input" /><i>*</i>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="92" class="name"></td>
                                        <td width="801">
                                            <input name="Promotion_Limit_ID" type="hidden" id="Promotion_Limit_ID" value="<%=limit_id %>" />
                                            <input name="action" type="hidden" id="action" value="renew" /><a href="javascript:void();" onclick="$('#formadd').submit();" class="a11"></a></td>
                                    </tr>
                                </table>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
            <div class="clear"></div>
        </div>
    </div>
    <!--主体 结束-->


  <ucbottom:bottom runat="server" ID="Bottom" />

</body>
</html>
