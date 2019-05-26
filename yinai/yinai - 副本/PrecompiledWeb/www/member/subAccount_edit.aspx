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
    Member member = new Member();
    
    supplier.Supplier_Login_Check("/member/subAccount_edit.aspx");
    int account_id = tools.NullInt(Session["account_id"]);
    if (account_id > 0)
    {
        Response.Redirect("/member/index.aspx");
    }
    int id = tools.CheckInt(Request["id"]);
    SupplierSubAccountInfo entity = supplier.GetSupplierSubAccountByID(id);

    if (entity == null)
    {
        Response.Redirect("/member/subAccount_list.aspx");
    }
    else
    {
        if (entity.Supplier_SubAccount_SupplierID != tools.NullInt(Session["supplier_id"]))
        {
            Response.Redirect("/member/subAccount_list.aspx");
        }
    }
    
    
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>
        <%="修改子账户 - 我是买家 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    
   <!--layer弹出框.js .css引用 开始-->
 <link href="../layer.m/layer.m.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery-1.5.1.min.js" charset="gb2312"></script>
       <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js" ></script>
  <!--layer弹出框.js .css引用 结束-->
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script type="text/javascript" src="/scripts/My97DatePicker/WdatePicker.js"></script>
    <script src="/scripts/jquery-extend-AdAdvance3.js" type="text/javascript"></script>
    <!--滑动门 开始-->
    <script type="text/javascript" src="/scripts/hdtab.js"></script>
      <style type="text/css">
        .blk17_sz table td .input03 {
            width: 13px;
        }



        .b14_1_main table td.sub_right {
            text-align: left;
            padding-left: 15px;
        }


        .b14_1_main table td a.a11 {
            background-image: url("../images/a_bg05.jpg");
            background-repeat: no-repeat;
            color: #fff;
            display: block;
            font-size: 16px;
            font-weight: bold;
            height: 38px;
            line-height: 38px;
            text-align: center;
            width: 141px;
        }

        .b14_1_main table td span a:hover {
            color: #fff;
        }

        .input03 {
        width: 15px;
        }
    </style>
    <script type="text/javascript">
        window.onload = function () {
            var SDmodel = new scrollDoor();
            SDmodel.sd(["c01", "c02"], ["cc01", "cc02"], "on", " ");
            
        }
    </script>
    

 
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
</head>
<body>
    <uctop:top ID="top1" runat="server" />
    <!--主体 开始-->
    <div class="content02" style="background-color: #FFF;">
       <%-- <div class="content02_main" style="background-color: #FFF;">--%>
            <!--位置说明 开始-->
            <div class="position">当前位置 > <a href="/member/">我是买家</a> > 账户管理 > <strong>修改子账户</strong></div>
            <!--位置说明 结束-->
            <div class="partd_1">
            <div class="pd_left">
                       <%-- <%supplier.Get_Supplier_Left_HTML(4, 2); %>--%>
                   <%=member.Member_Left_HTML(5,2) %>
                   
                </div>
                <div class="pd_right">
                   <%-- <div class="title03">修改子账户</div>--%>
                    <div class="blk14_1" style="margin-top:0px;">
                      <h2>修改子账户</h2>
                   <div class="b14_1_main" style="margin-top: 15px;">
                        <%
                            if (tools.CheckStr(Request.QueryString["tip"]) == "success")
                            {
                                pub.Tip("positive", "您的子账户已修改！");
                        %>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td height="10"></td>
                            </tr>
                        </table>
                        <%}%>
                        <form name="frm_account_profile" id="frm_account_profile" method="post" action="/member/account_do.aspx">
                            <table width="100%" border="0" cellspacing="0" cellpadding="5" class="table_padding_5">
                                <tr>
                                    <td width="92" class="name">用户名
                                    </td>
                                    <td width="801" class="sub_right">
                                        <%=entity.Supplier_SubAccount_Name %>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="92" class="name">新密码
                                    </td>
                                     <td width="801" class="sub_right">
                                        <input name="Supplier_SubAccount_Password" type="password" id="Supplier_SubAccount_Password"
                                            style="width: 300px;" /><i>*</i><em id="tip_Supplier_password">请输入6～20位密码（A-Z，a-z，0-9，不要输入空格）</em>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="92" class="name">确认新密码
                                    </td>
                                     <td width="801" class="sub_right">
                                        <input name="Supplier_SubAccount_Password_confirm" type="password"
                                            id="Supplier_SubAccount_Password_confirm" style="width: 300px;" /><i>*</i><em id="tip_Supplier_password_confirm">请再次输入密码</em>

                                    </td>
                                </tr>
                                <tr>
                                    <td width="92" class="name">姓名
                                    </td>
                                    <td width="801" class="sub_right">
                                        <input name="Supplier_SubAccount_TrueName" type="text" id="Supplier_SubAccount_TrueName"
                                            value="<%=entity.Supplier_SubAccount_TrueName %>"
                                            style="width: 300px;" /><i>*</i>
                                    </td>
                                </tr>

                                <tr>
                                    <td width="92" class="name">部门
                                    </td>
                                    <td width="801" class="sub_right">
                                        <input name="Supplier_SubAccount_Department" type="text" id="Supplier_SubAccount_Department"
                                            value="<%=entity.Supplier_SubAccount_Department%>" style="width: 300px;" />
                                    </td>
                                </tr>

                                <tr>
                                    <td width="92" class="name">手机
                                    </td>
                                  <td width="801" class="sub_right">
                                        <input name="Supplier_SubAccount_Mobile" type="text" id="Supplier_SubAccount_Mobile" style="width: 300px;" value="<%=entity.Supplier_SubAccount_Mobile%>" />

                                    </td>
                                </tr>

                                <tr>
                                    <td width="92" class="name">邮箱
                                    </td>
                                   <td width="801" class="sub_right">
                                        <input name="Supplier_SubAccount_Email" type="text" id="Supplier_SubAccount_Email"
                                            value="<%=entity.Supplier_SubAccount_Email%>" style="width: 300px;" />
                                    </td>
                                </tr>

                                <tr>
                                    <td width="92" class="name">到期时间
                                    </td>
                                    <td width="801" class="sub_right">
                                        <input type="text" name="Supplier_SubAccount_ExpireTime" id="Supplier_SubAccount_ExpireTime"
                                            maxlength="10" readonly="readonly" style="width: 195px;" onclick="WdatePicker({ skin: 'twoer', dateFmt: 'yyyy-MM-dd' })" class="Wdate input" value="<%=entity.Supplier_SubAccount_ExpireTime.ToString("yyyy-MM-dd") %>" />
                                    </td>
                                </tr>
                                <tr>
                                    <td width="92" class="name">状态
                                    </td>
                                     <td width="801" class="sub_right">
                                        <select id="Supplier_SubAccount_IsActive" name="Supplier_SubAccount_IsActive">
                                            <option value="1" <%=pub.CheckSelect("1",entity.Supplier_SubAccount_IsActive.ToString()) %>>启用</option>
                                            <option value="0" <%=pub.CheckSelect("0",entity.Supplier_SubAccount_IsActive.ToString()) %>>禁用</option>
                                        </select>
                                    </td>
                                </tr>

                                <tr>
                                    <td width="92" class="name">权限
                                    </td>
                                    <td width="801" class="sub_right">
                                        <%=supplier.DisplaySubAccountPrivilege(entity.Supplier_SubAccount_Privilege) %>
                                    </td>
                                </tr>

                                <tr>
                                    <td width="92" class="name">&nbsp;
                                    </td>
                                    <td width="801" class="sub_right">
                                        <span class="table_v_title">
                                            <input name="action" type="hidden" id="action" value="subaccountedit" />
                                            <input name="subAccountID" type="hidden" id="subAccountID" value="<%=id %>" />
                                            <a href="javascript:void();" onclick="$('#frm_account_profile').submit();" class="a11">保 存</a>
                                        </span>
                                    </td>
                                </tr>
                            </table>
                        </form>
                    </div>
                        </div>
                </div>
                <div class="clear">
                </div>
            </div>
        <%--</div>--%>
    </div>
    <!-- 右侧弹框 开始-->
    <div id="leftsead">
        <ul>
            <li>
             
                <a href="javascript:void(0);" onclick="SignUpNow();">
                    <div class="hides" style="width: 130px; height: 50px; display: none;" id="qq">
                        <div class="hides" id="p1">
                            <img src="/images/nav_1_1.png" />
                        </div>
                    </div>
                    <img src="/images/nav_1.png" width="57px" height="50px" class="shows" />
                </a>
            </li>
            <li id="tel">
                <a href="javascript:void(0)">
                    <div class="hides" style="width: 130px; height: 50px; display: none;" id="tels">
                        <div class="hides" id="p2">
                            <img src="/images/nav_2_1.png">
                        </div>

                    </div>
                    <img src="/images/nav_2.png" width="57px" height="50px" class="shows" />
                </a>
            </li>
            <li id="btn">
                <a id="top_btn" href="http://wpa.qq.com/msgrd?v=3&uin=3558103267&site=qq&menu=yes" target="_blank">
                    <div class="hides" style="width: 130px; height: 50px; display: none">
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
     <!-- 右侧弹框 结束-->
    <!--主体 结束-->
  <ucbottom:bottom runat="server" ID="Bottom" />
</body>
</html>
