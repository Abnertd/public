<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="System" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>



<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>
<%
    //静态化配置
    PageURL pageurl = new PageURL(int.Parse(Application["Static_IsEnable"].ToString()));
    
    Product product = new Product();
    ITools tools;

    tools=ToolsFactory.CreateTools();
    Public_Class pub=new Public_Class();

    if (tools.CheckInt(Application["Product_Review_Config_Power"].ToString()) > 0)
    {
        //会员登录检查
    }
    
    int cate_id=0;
    int product_id = 0;
    int validcount=0;
    double average_grade=0;
    string product_name = "";
    
    
    product_id = tools.CheckInt(Request["product_id"]);
    if (product_id == 0)
    {
        Response.Redirect("/index.aspx");
    }
    ProductInfo productinfo = product.GetProductByID(product_id);
    if (productinfo != null)
    {
        if (productinfo.Product_IsAudit == 1)
        {
            if (productinfo.Product_SEO_Title == "")
            {
                product_name = productinfo.Product_Name;
            }
            else
            {
                product_name = productinfo.Product_SEO_Title;
            }
            cate_id = productinfo.Product_CateID;
            average_grade = productinfo.Product_Review_Average;
            validcount = productinfo.Product_Review_ValidCount;
            //Session["url_after_login"] = "/product/ask_add.aspx?product_id=" + product_id;            
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

    if (tools.CheckInt(Application["Product_Review_Config_Power"].ToString()) == 2)
    {
        //商品购买检查
    }
    
    int first_cate = product.Get_First_CateID(cate_id);
    product.Set_Cate_Session(first_cate);
    product.Recent_View_Add(product_id);

%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title><%=product_name + " - " + pub.SEO_TITLE()%></title>
<meta name="Keywords" content="<% = productinfo.Product_SEO_Keyword%>" />
<meta name="Description" content="<%=productinfo.Product_SEO_Description%>" />
<link href="/css/index.css" rel="stylesheet" type="text/css" />
<link href="/css/index2.css" rel="stylesheet" type="text/css" />
<script language="javascript" type="text/javascript" src="/scripts/jquery.js"></script>
<script language="javascript" type="text/javascript" src="/scripts/common.js"></script>
      <script type="text/javascript" src="/scripts/cart.js"></script>
    <script src="../scripts/member.js"></script>

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
table tr td{ padding:3px;}
</style>
</head>
<body>
    <uctop:top ID="top" runat="server" />


 <div class="webwrap">
       <div class="content02" style="background-color: #FFF;">
       <div class="content02_main" style="background-color: #FFF;">
        <!--位置说明 开始-->
           <div class="position"><a href="/">首页</a> <%=product.Get_Cate_Nav(cate_id,"&nbsp;>&nbsp;")%> > <strong><%=productinfo.Product_Name%></strong></div>
        <!--位置说明 结束-->
      
            <div class="pc_left1">
   
        <!--左侧部分开始-->
    	<%product.Get_ProducInfo_Review(productinfo);%>
        <!--左侧部分结束-->
        </div>
        <!--右侧主体开始-->
        <div class="pc_right1" style="width:958px;">



         


             <div class="blk18" id="blk03">
                    <h2>
                        <ul>
                            <li class="on">购买咨询</li>
                            <li class="bo"></li>
                        </ul>
                        <div class="clear"></div>
                    </h2>          
                            
                    <div class="b18_main04">

                        <% product.Product_Ask_List(product_id, 20, 1, 0); %>
                    </div>
                </div>

            <div class="blk18" style="margin-top:20px;">
                    <h2>
                        <ul>
                            <li class="on">提交咨询</li>
                            <li class="bo"></li>
                        </ul>
                        <div class="clear"></div>
                    </h2>
                <div class="b18_main04" style="padding-top:10px;">

                       <form name="form1" method="post" action="/product/ask_do.aspx"> 
                        <table width="764" border="0">                            
                            <tr bgcolor="#FFFFFF">
                                <td  align="right" valign="middle">
                                    内容：
                                </td>
                                <td  align="left" valign="middle">
                                    <label>
                                        <textarea name="ask_content" id="textarea" cols="45" rows="5"></textarea>
                                    </label>
                                </td>
                            </tr>
                            <tr bgcolor="#FFFFFF">
                                <td  align="right" valign="middle">
                                    联系方式：
                                </td>
                                <td align="left" valign="middle">
                                    <label>
                                        <input name="ask_contact" type="text" id="ask_contact" width="30"  onblur="check_feedback_phone('ask_contact')"/>
                                          <strong class="regtip" id="ask_contact_tip" style="font-weight: 500"></strong>
                                    </label>
                                </td>
                            </tr>
                            <tr bgcolor="#FFFFFF">
                                <td width="133"  align="right" valign="middle">
                                    验证码：
                                </td>
                                <td width="621" align="left" valign="middle">
                                    <input name="ask_verify" type="text" style="width: 70px;" />
                                    <img src="/Public/verifycode.aspx" width="65" alt="看不清？换一张" title="看不清？换一张" height="26" id="var_img" style=" cursor:pointer; display:inline;" onclick="$('#var_img').attr('src','/Public/verifycode.aspx?timer='+Math.random());" align="absmiddle" />
                                </td>
                            </tr>
                            <tr bgcolor="#FFFFFF">
                                <td colspan="2" align="center" valign="middle">
                                    <input type="hidden" name="action" id="Hidden1" value="ask_add" />
	                                 <input type="hidden" name="ask_type" id="Hidden2" value="1" />             
	                                  <input type="hidden" name="ask_productid" id="Hidden3" value="<%=product_id %>" />
	                                  <input type="image" src="/images/btn_submit.gif" />
                                </td>
                            </tr>
                        </table>
                        </form>
                    </div>
                </div>
  
       
        </div>
        <div class="clear"></div>
        <!--右侧主体结束-->
    </div>
    </div>
</div>
    <ucbottom:bottom ID="bottom" runat="server" />
</body>
</html>

