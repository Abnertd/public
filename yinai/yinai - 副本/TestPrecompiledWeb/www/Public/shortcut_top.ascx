<%@ Control Language="C#" ClassName="shortcut_top" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<%
    ITools tools = ToolsFactory.CreateTools();
    Member member = new Member();
%>

<div class="sz_head">

    <div class="top">
        <div class="top_main">
            <div class="top_right">
                <ul>
                    <li style="width: 90px; padding: 0;">
                        <%if (tools.NullStr(Session["member_logined"]) == "True")
                          {%>
                        <dl class="zkn_dst1">
                            <dt><a href="/member/">我的易耐</a></dt>
                            <dd>
                                <p><a href="/member/order_list.aspx">我的订单</a></p>
                                <p><a href="/member/member_favorites.aspx">我的收藏</a></p>
                                <p><a href="/member/feedback.aspx">我的咨询</a></p>
                                <p><a href="/member/Message_List.aspx">站内信息</a></p>
                            </dd>
                        </dl>
                        <%}
                          else if (tools.NullStr(Session["supplier_logined"]) == "True")
                          {%>
                        <dl class="zkn_dst1">
                            <dt><a href="/supplier/">我的易耐</a></dt>
                            <dd>
                                <p><a href="/supplier/order_list.aspx">我的订单</a></p>
                                <p><a href="/supplier/supplier_sysmessage.aspx">站内信息</a></p>
                            </dd>
                        </dl>
                        <%}
                          else
                          {%>
                        <dl class="zkn_dst1">
                            <dt><a href="/member/">我的易耐</a></dt>
                            <dd>
                                <p><a href="/member/order_list.aspx">我的订单</a></p>
                                <p><a href="/member/member_favorites.aspx">我的收藏</a></p>
                                <p><a href="/member/feedback.aspx">我的咨询</a></p>
                                <p><a href="/member/Message_List.aspx">站内信息</a></p>
                            </dd>
                        </dl>
                        <%} %>
                    </li>
                    <li class="li01">
                        <a href="/member/member_favorites.aspx" class="a01">收藏夹</a>
                        <%member.Member_Top_Favoriets(); %>
                    </li>
                    <%if (tools.NullStr(Session["member_logined"]) == "True")
                      {%>
                    <li><a href="/login.htm">采购商入口</a></li>
                    <%}
                      else if (tools.NullStr(Session["supplier_logined"]) == "True")
                      {%>
                    <li><a href="/login.htm?u_type=1">供应商入口</a></li>
                    <%}
                      else
                      { %>
                    <li><a href="/login.htm">采购商入口</a></li>
                    <li><a href="/login.htm?u_type=1">供应商入口</a></li>
                    <%} %>
                    <li class="li02">
                        <a href="javascript:;">手机版<img src="/images/icon01.png" /></a>
                        <div class="li02_box">
                            <img src="/images/手机端.png" /><p>
                                扫描二维码<br />
                                下载手机端
                            </p>
                        </div>
                    </li>
                    <li style="background-image: none; padding: 0;">客户电话：<strong>400-882-6621</strong></li>
                </ul>
                <div class="clear"></div>
            </div>
            <b>
                <%
                    if (tools.NullStr(Session["supplier_logined"]) == "True")
                    {
                        Response.Write("您好，" + Session["supplier_nickname"] + "，欢迎光临" + Application["Site_Name"] + "！  <a href=\"/supplier/login_do.aspx?action=logout\">退出</a>");
                    }
                    else if (tools.NullStr(Session["member_logined"]) == "True")
                    {
                        Response.Write("您好，" + Session["member_nickname"] + "，欢迎光临" + Application["Site_Name"] + "！ <a href=\"/member/login_do.aspx?action=logout\">退出</a>");
                    }
                    else
                    {
                        Response.Write("您好，欢迎光临" + Application["Site_Name"] + "！ <a href=\"/register.htm\">[免费注册]</a> <a href=\"/login.htm\">[立即登录]</a>");
                    }
                %>
            </b>
        </div>
    </div>

</div>
