<%@ Page Language="C#" %>
<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>

<% 
    Public_Class pub = new Public_Class();
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="找回密码 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script type="text/javascript" src="/scripts/common.js"></script>
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
        .t14 { font-size: 14px; }
        .table_help { border: 1px solid #e7e7e7; padding-top: 0px; padding-right: 10px; padding-left: 10px; }
        .table_help td { padding:5px; line-height:30px; }
        .table_help input.buttonSkinA { border:none; _border:0px; display:inline; padding:0px 10px; cursor:pointer; height:23px; background-color:#e33d3d; font-size:12px; border-radius:2px; color:#FFF; }
        .table_help input.buttonSkinA:hover { background-color:#ce1329; color:#FFF; text-decoration:none;}
    </style>
</head>
<body>

    <uctop:top ID="top1" runat="server" />

    <div class="content02" style="background-color: #FFF;">
        <div class="content02_main" style="background-color: #FFF;">
            <!--位置说明 开始-->
            <div class="position"><a href="/tradeindex.aspx">首页</a> ><strong><a href="getpassword.aspx">找回密码</a></strong> </div>
            <!--位置说明 结束-->
            <div class="partd">

                <table width="100%" border="0" cellspacing="0" cellpadding="5" class="table_help">
                    <tr>
                        <td width="100" align="center" valign="middle">
                            <img src="/images/info_error_48.gif">
                        </td>
                        <td align="left" valign="top">
                            <h2>验证失败</h2>
                            您重新设置密码的请求验证失败。请检查您点击的链接是否正确。你也可以尝试
                            <input name="btnupgrade" type="button" class="buttonSkinA" id="btnupgrade" value="重新发送"
                                onclick="location.href = 'getpassword.aspx';" />
                        </td>
                    </tr>
                </table>

            </div>
        </div>
    </div>

  <ucbottom:bottom runat="server" ID="Bottom" />
</body>
</html>
