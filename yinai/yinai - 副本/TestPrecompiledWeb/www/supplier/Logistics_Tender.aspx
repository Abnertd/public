<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>
<% 
    
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    Session["Cur_Position"] = Session["Home_Position"] = "";
    int ID = tools.CheckInt(Request["ID"]);
    Supplier supplier = new Supplier();
    supplier.Supplier_Login_Check("/supplier/Logistics_Tender.aspx?ID="+ID);
    Logistics MyLogistics = new Logistics();
    double Supplier_Account;
    Supplier_Account = 0;
    SupplierInfo supplierinfo = supplier.GetSupplierByID();
    SupplierLogisticsInfo entity = MyLogistics.GetSupplierLogisticsByID(ID);
    if (supplierinfo != null)
    {
        Supplier_Account = supplierinfo.Supplier_Security_Account;
        if (tools.NullInt(Session["supplier_ishaveshop"]) == 0)
        {
            pub.Msg("error", "错误信息", "您还没有开通店铺", false, "/supplier/index.aspx");
        }
        if(entity!=null)
        {
            if(entity.Supplier_SupplierID!=supplierinfo.Supplier_ID)
            {
                Response.Redirect("/supplier/Logistics_list.aspx");
            }
        }
        else
        {
            Response.Redirect("/supplier/Logistics_list.aspx");
        }
    }
    
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="报价列表 - 我是卖家 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />

     <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
     <script type="text/javascript" src="/scripts/common.js"></script>
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script src="/scripts/common.js" type="text/javascript"></script>
    <script src="/scripts/jquery-extend-AdAdvance3.js" type="text/javascript"></script>

    <link type="text/css" href="/scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
    <script src="/scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
    <script src="/scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>
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
        .zkw_19_fox img {
            vertical-align: middle;
            display: inline;
        }
    </style>
</head>
<body>

    <uctop:top ID="top1" runat="server" />


    <!--主体 开始-->
    <div class="webwrap">
    <div class="content02" style="background-color: #FFF;">
     
            <!--位置说明 开始-->
            <div class="position">当前位置 > <a href="/supplier/">我是卖家</a> > 物流管理 > <strong>报价列表</strong></div>
            <!--位置说明 结束-->
            <div class="partc">
                <div class="pd_left">
               
                        <% supplier.Get_Supplier_Left_HTML(6, 1); %>
                  
                </div>
                <div class="pc_right">

                        


                            <div class="blk14_1" style="margin-top: 1px;">
                              <h2>报价列表</h2>
                                <% MyLogistics.Supplier_LogisgicsTender_List(ID);%>
                                </div>

                </div>
            </div>
        </div>
        <div class="clear"></div>
    </div>

    <!--主体 结束-->


  <ucbottom:bottom runat="server" ID="Bottom" />

</body>
</html>
