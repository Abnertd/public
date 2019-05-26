<%@ Control Language="C#" ClassName="bottom_simple" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%
    ITools tools = ToolsFactory.CreateTools();
    CMS cms = new CMS();
    AD ad = new AD();
%>
<%--<link href="/css/index.css" rel="stylesheet" type="text/css" />
<link href="/css/index2.css" rel="stylesheet" type="text/css" />--%>

<!--尾部 开始-->
<div class="foot">
    <div class="foot_info01">
        <div class="foot_info01_main">
            <%if (ad.AD_Show("Public_Bottom_Big_IMG", "1", "cycle", 0) != null)
              {               
            %>
            <dl>
                <%=ad.AD_Show("Public_Bottom_Big_IMG", "1", "cycle", 0)%>
            </dl>
            <%} %>
            <%if (ad.AD_Show("Public_Bottom_Big_IMG", "2", "cycle", 0) != null)
              {               
            %>
            <dl>
                <%=ad.AD_Show("Public_Bottom_Big_IMG", "2", "cycle", 0)%>
            </dl>
            <%} %>






            <%if (ad.AD_Show("Public_Bottom_Big_IMG", "3", "cycle", 0) != null)
              {               
            %>
          
            <dl>

                <%=ad.AD_Show("Public_Bottom_Big_IMG", "3", "cycle", 0)%>
            </dl>
            <%} %>

            <%if (ad.AD_Show("Public_Bottom_Big_IMG", "4", "cycle", 0) != null)
              {               
            %>
        
            <dl>
                <%=ad.AD_Show("Public_Bottom_Big_IMG", "4", "cycle", 0)%>
            </dl>
            <%} %>
            <div class="clear"></div>
        </div>
    </div>
    <div class="foot_info02">
        <div class="foot_info02_main">
            <dl class="dst01">
                <dt>用户指南</dt>
                <dd>
                    <%=cms.Help_Left_Sub_Nav1(6)%>
                </dd>
            </dl>
            <dl class="dst01" style="margin-left: 64px;">
                <dt>帮助中心</dt>
                <dd>

                    <%=cms.Help_Left_Sub_Nav1(3)%>
                </dd>
            </dl>
            <dl class="dst02" style="margin-left: 64px;">
                <dt><span>
                    <img src="/images/yinaishangcheng.png">易耐商城</span></dt>
                <dd>400-8108-802<span>（周一至周五 9：00--18：00）</span></dd>
            </dl>
            <dl class="dst01" style="margin-left: 64px;">
                <dt>售后服务</dt>
                <dd>

                    <%=cms.Help_Left_Sub_Nav1(8)%>
                </dd>
            </dl>
            <dl class="dst01" style="float: right;">
                <dt>招商合作</dt>
                <dd>

                    <%=cms.Help_Left_Sub_Nav1(9)%>
                </dd>
            </dl>
        </div>
    </div>
    <div class="foot_info03">
        <p>
            <a href="/about/index.aspx?sign=aboutus" target="_blank">关于我们</a>\<a href="/about/index.aspx?sign=job" target="_blank">招聘信息</a>\<a href="/about/index.aspx?sign=service" target="_blank">服务条款</a>\<a href="/about/index.aspx?sign=core" target="_blank">核心优势</a>\<a href="/about/index.aspx?sign=address" target="_blank">联系我们</a><%if (tools.NullStr(Session["Logistics_Logined"]) == "False")
                                                                                                                                                                                                                                                                                                                                               { %>\<a href="/Logistics/Logistics_login.aspx" target="_blank">物流商登录</a><%} %>\<a href="/about/index_link.aspx" target="_blank">友情链接</a>
        </p>
        <p>《中华人民共和国信息产业部》备案：京ICP备17007089号</p>
        <p>CopyRight ©  2012-2015, All Rights Reserved. </p>
      
        <p><span style="display: inline"><%=ad.AD_Show("Public_Bottom_Small_IMG", "1", "cycle", 0)%><%=ad.AD_Show("Public_Bottom_Small_IMG", "2", "cycle", 0)%><%=ad.AD_Show("Public_Bottom_Small_IMG", "3", "cycle", 0)%><%=ad.AD_Show("Public_Bottom_Small_IMG", "4", "cycle", 0)%><%=ad.AD_Show("Public_Bottom_Small_IMG", "5", "cycle", 0)%><%=ad.AD_Show("Public_Bottom_Small_IMG", "6", "cycle", 0)%></span> </p>
    </div>
</div>
<!--尾部 结束-->

