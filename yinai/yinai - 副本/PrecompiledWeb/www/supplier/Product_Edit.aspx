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
    myApp.Supplier_Login_Check("/supplier/Product_list.aspx");

    string Product_Code, Product_Name, Product_CateID, Product_GroupID;
    int Product_ID, Product_TypeID, Product_Cate;
    Product_Name = "";
    Product_Code = "";
    Product_Cate = 0;
    Product_TypeID = 0;
    Product_GroupID = "";
    Product_CateID = "";
    Product_ID = tools.CheckInt(Request.QueryString["Product_ID"]);
    ProductInfo entity = myApp.GetProductByID(Product_ID);
    if (entity == null)
    {
        pub.Msg("error", "错误信息", "记录不存在", false, "{back}");
        Response.End();
    }
    else
    {
        Product_ID = entity.Product_ID;
        Product_TypeID = entity.Product_TypeID;
        Product_Code = entity.Product_Code;
        Product_Name = entity.Product_Name;
        Product_Cate = entity.Product_CateID;
        Product_CateID = myApp.GetProductCategory(Product_ID);
        Product_GroupID = myApp.GetProductGroup(Product_ID);

    }
    
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="商品管理 - 我是卖家 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />


    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index_newadd.css" rel="stylesheet" type="text/css" />
    
   <!--layer弹出框.js .css引用 开始-->
 <link href="../layer.m/layer.m.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery-1.5.1.min.js" charset="gb2312"></script>
       <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js" ></script>
  <!--layer弹出框.js .css引用 结束-->
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script src="/scripts/jquery-extend-AdAdvance3.js" type="text/javascript"></script>
    <script src="/scripts/common.js" type="text/javascript"></script>
    <script type="text/javascript" src="/scripts/supplier.js"></script>
    <link rel="stylesheet" type="text/css" href="/scripts/treeview/dhtmlxtree.css" />
    <script src="/scripts/treeview/dhtmlxcommon.js" type="text/javascript"></script>
    <script src="/scripts/treeview/dhtmlxtree.js" type="text/javascript"></script>
    <style type="text/css">
     table,tr,td,th{margin:0;padding-top:10px;font-family:"微软雅黑";}

     table td.name { text-align:right; padding:10px 0 10px 0;}
    </style>
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
    <script type="text/javascript">
        function openUpload(openObj) {
            $("#td_upload").show();

            $("#iframe_upload").attr("src", "<% =Application["Upload_Server_URL"]%>/public/uploadify.aspx?App=shopcert&formname=formadd&frmelement=" + openObj + "&rtvalue=1&rturl=<% =Application["Upload_Server_Return_WWW"]%>");
        }

        function delImage(openObj) {
            $("#img_" + openObj)[0].src = "/images/detail_no_pic.gif";
            $("#" + openObj)[0].value = "/images/detail_no_pic.gif";
        }
    </script>

    <style type="text/css">
        #treeboxbox_tree td {
            margin: 0;
            padding: 0;
        }
       .blk17_sz table td .td_box
        {
        border:0px;
        padding:0px;
        }
        .blk17_sz table td
        {
            padding:0px;
        }
    </style>
</head>
<body>

    <uctop:top ID="top1" runat="server" />




    <!--主体 开始-->
    <!--主体 开始-->
      <div class="webwrap" >
    <div class="content02" style="margin-bottom:20px;">
      
            <!--位置说明 开始-->
            <div class="position">当前位置 > <a href="/supplier/index.aspx">我是卖家</a> > <strong>产品修改</strong></div>
            <!--位置说明 结束-->
            <div class="partd_1">
                <div class="pd_left">                 
                        <%myApp.Get_Supplier_Left_HTML(3, 2); %>                 
                </div>
                <div class="pd_right">
                    <div class="blk14_1" style="margin-top:0px;">
                    <h2>商品管理</h2>
                    <div class="blk17_sz" style="margin-top:15px;">
                        <form name="formadd" id="formadd" method="post" action="/supplier/product_edit_1.aspx" onsubmit="javascript:MM_findObj('Product_GroupID').value = tree.getAllChecked();javascript:MM_findObj('Product_CateID').value = tree1.getAllChecked();">
                            <table width="893" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td width="92" class="name">产品名称：</td>
                                    <td width="801">
                                        <input name="Product_Name" id="Product_Name" type="text" style="width: 300px;" autocomplete="off" value="<%=Product_Name %>" /><i>*</i></td>
                                </tr>
                               <%-- <tr>
                                    <td class="name">产品编码：</td>
                                    <td>
                                        <input name="Product_Code" type="text" id="Product_Code" style="width: 300px;" value="<%=Product_Code %>" /><i>*</i></td>
                                </tr>--%>
                                <tr>
                                    <td class="name">产品分类：</td>
                                    <td>
                                        <span id="main_cate"><%=myApp.Product_Category_Select(Product_Cate, "main_cate")%></span>
                                        <span id="div_Product_Cate"></span>
                                        <i>*</i></td>
                                </tr>

                              <%--  <tr>
                                    <td class="name">附加分类：</td>
                                    <td align="left" style="padding:10px 0px">
                                        <div class="td_box" style="width: 430px;">
                                            <table style="width: 430px; background: #f5f5f5; border: 1px solid Silver;">
                                                <tr>
                                                    <td valign="top" id="treeboxbox_tree1" style="width: 430px;"></td>
                                                </tr>
                                            </table>
                                            <script type="text/javascript">
                                                tree1 = new dhtmlXTreeObject("treeboxbox_tree1", "500", "100%", 0);
                                                tree1.setSkin('dhx_skyblue');
                                                tree1.setImagePath("/scripts/treeview/imgs/csh_dhx_skyblue/");
                                                tree1.enableCheckBoxes(1);
                                                tree1.enableThreeStateCheckboxes(true);
                                                tree1.loadXML("treedata1.aspx?cate_id=<%=Product_CateID%>");
                                            </script>
                                            <span id="div_Product_CateID"></span>
                                        </div>
                                    </td>
                                </tr>--%>

                                <tr>
                                    <td class="name">本店分类：</td>
                                    <td align="left" style="padding:10px 0px">
                                        <div class="td_box" style="width: 430px;">
                                            <table style="width: 430px; background: #f5f5f5; border: 1px solid Silver;">
                                                <tr>
                                                    <td valign="top" id="treeboxbox_tree" style="width: 430px;"></td>
                                                </tr>
                                            </table>
                                            <script type="text/javascript">
                                                tree = new dhtmlXTreeObject("treeboxbox_tree", "500", "100%", 0);
                                                tree.setSkin('dhx_skyblue');
                                                tree.setImagePath("/scripts/treeview/imgs/csh_dhx_skyblue/");
                                                tree.enableCheckBoxes(1);
                                                tree.enableThreeStateCheckboxes(true);
                                                tree.loadXML("treedata.aspx?cate_id=<%=Product_GroupID%>");
                                            </script>
                                            <span id="div_Product_GroupID"></span>
                                        </div>
                                    </td>
                                </tr>
                               <%-- <tr>
                                    <td class="name">产品参数：</td>
                                    <td>
                                        <select name="Product_TypeID" id="Product_TypeID" style="width: 160px;">
                                            <option value="0">选择商品参数</option>
                                            <% =myApp.ProductTypeOption(Product_TypeID)%>
                                        </select>
                                        <i>*</i></td>
                                </tr>--%>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>
                                        <a href="javascript:void();" onclick="product_save_next();" class="a11" style=" background-image:url(../images/next_buttom.jpg);"></a>

                                        <input name="Product_GroupID" type="hidden" id="Product_GroupID" value="<%=Product_GroupID%>" />
                                        <input name="Product_CateID" type="hidden" id="Product_CateID" value="<% =Product_CateID%>" />
                                        <input type="hidden" id="Product_ID" name="Product_ID" value="<% =Product_ID%>" />
                                        <input name="action" type="hidden" id="action" value="shop_apply" />
                                    </td>
                                </tr>
                            </table>
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
                    <div class="hides" style="width: 130px; display: none" id="Div1">
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
