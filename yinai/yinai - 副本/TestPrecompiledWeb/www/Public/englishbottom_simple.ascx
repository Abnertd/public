<%@ Control Language="C#" ClassName="englishbottom_simple" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%
    ITools tools = ToolsFactory.CreateTools();
    CMS cms = new CMS();
    AD ad = new AD();
%>
<%--<link href="/css/index.css" rel="stylesheet" type="text/css" />
<link href="/css/index2.css" rel="stylesheet" type="text/css" />--%>

<!--尾部 开始-->
<div class="foot" style=" margin-top:0px!important;">
    <div class="foot_info03" style=" height:64px; background-color:#333333;">
        <div style=" font-size:16px; text-align:center; color:#fff; line-height:64px;">CopyRight © 2012-2015, All Rights Reserved.</div>
       <%-- <p>《中华人民共和国信息产业部》备案：京ICP备17007089号</p>
        <p>CopyRight ©  2012-2015, All Rights Reserved. </p>     
        <p><span style="display: inline"><%=ad.AD_Show("Public_Bottom_Small_IMG", "1", "cycle", 0)%><%=ad.AD_Show("Public_Bottom_Small_IMG", "2", "cycle", 0)%><%=ad.AD_Show("Public_Bottom_Small_IMG", "3", "cycle", 0)%><%=ad.AD_Show("Public_Bottom_Small_IMG", "4", "cycle", 0)%><%=ad.AD_Show("Public_Bottom_Small_IMG", "5", "cycle", 0)%><%=ad.AD_Show("Public_Bottom_Small_IMG", "6", "cycle", 0)%></span> </p>--%>
    </div>
</div>
<!--尾部 结束-->

