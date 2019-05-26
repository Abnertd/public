<%@ Page Language="C#" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Register Src="~/Public/Top.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>
<% 
    Session["Position"] = "";
    CMS cms = new CMS();
    Supplier supplier = new Supplier();
    Member member = new Member();
    Product product = new Product();
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    HelpInfo help = null;

    string help_title = "缺货登记";
    int help_id = 0;

    member.Member_Login_Check("/help/stockout.aspx");

    string supplier_name, supplier_mobile, supplier_email;
    string member_name,member_mobile,member_email;
    member_name = "";
    member_mobile = "";
    member_email = "";
    SupplierInfo supplierinfo = null;
    MemberInfo memberInfo = null;

    if (tools.NullInt(Session["supplier_id"]) > 0)
    {
        Response.Redirect("/supplier/");
    }

    if (tools.NullInt(Session["member_id"]) > 0)
    {
        memberInfo = member.GetMemberByID();
        if (memberInfo != null)
        {
            member_name = memberInfo.Member_NickName;
            member_mobile = memberInfo.Member_LoginMobile;
            member_email = memberInfo.Member_Email;
        }
    }
    
    
    
    if (Request["action"] == "stockout_add")
    {
        product.Stock_Out_Add();
    }
    string keyword = tools.CheckStr(tools.NullStr(Request["keyword"]));
    keyword = Server.UrlDecode(keyword);
    keyword = keyword.Replace("\"", "&quot;").ToString();
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>
        <%=help_title + " - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script type="text/javascript" src="/scripts/common.js"></script>
    <script type="text/javascript" src="/scripts/jquery-extend-AdAdvance4.js"></script>
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
        .a11 {
            background-image: url(/images/a_bg05.jpg);
            background-repeat: no-repeat;
            width: 141px;
            height: 38px;
            font-size: 16px;
            font-weight: bold;
            text-align: center;
            line-height: 38px;
            display: block;
            color: #FFF;
        }

       .input01 {
            height: 32px;
            border: 1px solid #dddddd;
            box-shadow: inset 1px 1px 4px #d9d9d9;
            -webkit-box-shadow: inset 1px 1px 4px #d9d9d9;
            -moz-box-shadow: inset 1px 1px 4px #d9d9d9;
            font-size: 12px;
            font-weight: normal;
            color: #999;
            line-height: 32px;
            font-family: "微软雅黑";
            padding: 0 0 0 5px;
        }
    </style>
</head>
<body>
    <uctop:top ID="top1" runat="server" />

    <!--主体 开始-->
    <div class="content" style="margin-top: 0;">
        <div class="content_main">
            <!--位置说明 开始-->
            <div class="position">
                当前位置 > <a href="/index.aspx">首页</a> >
                <span><%=help_title %></span>
            </div>
            <div class="clear"></div>

            <div class="position"><a href="/index.aspx">首页</a> > <strong><%=help_title %></strong></div>
            <!--位置说明 结束-->
            <div class="partk">
                <div class="pk_left">
                    <div class="blk14">
                        <h2>帮助中心</h2>
                        <div class="blk14_main">
                            <%=cms.Help_Left_Nav(0, 0)%>
                        </div>
                    </div>
                </div>
                <div class="pk_right">
                    <div class="title06"><span><%=help_title %></span></div>
                    <div class="blk25">

                        <div class="b25_main">
                            <form id="form1" name="form1" method="post" action="stockout.aspx">
                                <table width="100%" border="0" cellspacing="0" cellpadding="5" class="table_padding_5">
                                    <tr>
                                        <td width="130" height="30" align="right" class="t12_53">商品名称：
                                        </td>
                                        <td>
                                            <input name="stockout_product_name" type="text" id="stockout_product_name"
                                                value="<%=keyword %>" style="width: 300px;" class="input01" maxlength="50" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="30" align="right" class="t12_53">商品描述：
                                        </td>
                                        <td>
                                            <textarea name="stockout_product_describe" class="input01"  id="stockout_product_describe"
                                                style="height: 80px;" cols="50" rows="5"></textarea>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="30" align="right" class="t12_53">姓名：
                                        </td>
                                        <td>
                                            <input name="stockout_member_name" type="text" id="stockout_member_name"
                                                maxlength="30" value="<%=member_name %>" style="width: 300px;" class="input01" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="30" align="right" class="t12_53">联系电话：
                                        </td>
                                        <td>
                                            <input name="stockout_member_tel" type="text" id="stockout_member_tel"
                                                maxlength="50" value="<%=member_mobile %>" style="width: 300px;" class="input01" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="30" align="right" class="t12_53">Email：
                                        </td>
                                        <td>
                                            <input name="stockout_member_email" type="text" id="stockout_member_email"
                                                maxlength="50" value="<%=member_email %>" style="width: 300px;" class="input01" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="30" align="right" class="t12_53">验证码：
                                        </td>
                                        <td>
                                            <input type="text" name="verifycode" tabindex="7" class="input01" style="height: 32px; width: 100px;"
                                                id="verifycode" onfocus="$('#var_img').attr('src','../Public/verifycode.aspx?timer='+Math.random());" />
                                            <img src="/Public/verifycode.aspx" id="var_img" onclick="$('#var_img').attr('src','../Public/verifycode.aspx?timer='+Math.random());"
                                                alt="看不清？换一张" style="cursor: pointer; padding-top: 0px; margin-top: 0px; padding-left: 0px; border: 0px; display: inline;"
                                                align="absmiddle" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="50" align="right" class="t12_53"></td>
                                        <td>
                                            <input name="action" type="hidden" id="action" value="stockout_add" />
                                            <%--<input name="button" type="image" src="/images/submit.gif" />--%>
                                            <a href="javascript:void(0);" onclick="$('#form1').submit();" class="a11">保 存</a>
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
    <!--主体 结束-->



  <ucbottom:bottom runat="server" ID="Bottom" />
</body>
</html>
