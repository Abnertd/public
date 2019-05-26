<%@ Control Language="C#" ClassName="HomeBottom" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbtm" TagName="bottom_simple" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<%
    CMS cms = new CMS();
    ITools tools = ToolsFactory.CreateTools();
%>

<!--尾部 开始-->
<div class="foot">
    <div class="foot_info01">
        <div class="foot_info01_main">
            <dl>
                <dt>
                    <img src="/images/foot_icon01.jpg"></dt>
                <dd>全场100%正品保障</dd>
                <div class="clear"></div>
            </dl>
            <dl style="margin-left: 114px;">
                <dt>
                    <img src="/images/foot_icon02.jpg"></dt>
                <dd>明码实价诚信经营</dd>
                <div class="clear"></div>
            </dl>
            <dl style="margin-left: 106px;">
                <dt>
                    <img src="/images/foot_icon03.jpg"></dt>
                <dd>满100元包邮费</dd>
                <div class="clear"></div>
            </dl>
            <dl style="float: right;">
                <dt>
                    <img src="/images/foot_icon04.jpg"></dt>
                <dd>售后服务贴心到位</dd>
                <div class="clear"></div>
            </dl>
            <div class="clear"></div>
        </div>
    </div>
    <div class="foot_info02">
        <div class="foot_info02_main">
            <dl class="dst01">
                <dt>新手导购</dt>
                <dd>
                    <p><a href="#" target="_blank">用户注册</a></p>
                    <p><a href="#" target="_blank">购物指南</a></p>
                    <p><a href="#" target="_blank">报价&合同申请</a></p>
                    <p><a href="#" target="_blank">销售证书</a></p>
                    <p><a href="#" target="_blank">常见问题</a></p>
                </dd>
            </dl>
            <dl class="dst01" style="margin-left: 64px;">
                <dt>支付&配送</dt>
                <dd>
                    <p><a href="#" target="_blank">汇款账户</a></p>
                    <p><a href="#" target="_blank">支付方式</a></p>
                    <p><a href="#" target="_blank">配送范围</a></p>
                    <p><a href="#" target="_blank">配送方式</a></p>
                    <p><a href="#" target="_blank">发票</a></p>
                </dd>
            </dl>
            <dl class="dst02" style="margin-left: 64px;">
                <dt><span>
                    <img src="/images/手机端.png">手机端</span><span><img src="/images/微信端.png">微信端</span></dt>
                <dd>400-8108-802<span>（周一至周五 7:00-17:00）</span></dd>
            </dl>
            <dl class="dst01" style="margin-left: 64px;">
                <dt>售后服务</dt>
                <dd>
                    <p><a href="#" target="_blank">退换货</a></p>
                    <p><a href="#" target="_blank">投诉建议</a></p>
                    <p><a href="#" target="_blank">售后服务</a></p>
                </dd>
            </dl>
            <dl class="dst01" style="float: right;">
                <dt>招商合作</dt>
                <dd>
                    <p><a href="#" target="_blank">大宗采购</a></p>
                    <p><a href="#" target="_blank">经销商</a></p>
                    <p><a href="#" target="_blank">供应商合作</a></p>
                    <p><a href="#" target="_blank">友情链接</a></p>
                </dd>
            </dl>
        </div>
    </div>
    <div class="foot_info03">
        <p><a href="#" target="_blank">关于我们</a>\<a href="#" target="_blank">人才招聘</a>\<a href="#" target="_blank">法律声明</a>\<a href="#" target="_blank">商城公告</a>\<a href="#" target="_blank">联系我们</a></p>
        <p>《中华人民共和国信息产业部》备案：京ICP备15051883号 | 营业执照</p>
        <p>CopyRight ©  2012-2015, All Rights Reserved. </p>
        <p>技术支持：北京光蓝网络科技有限公司</p>
        <p>
            <img src="/images/foot_img.jpg"></p>
    </div>
</div>
<!--尾部 结束-->
<script language="javascript" type="text/javascript">
    NTKF_PARAM = {
        siteid: 'sz_1000',                         //平台基础id
        sellerid: '',                       //商户id，平台中不需传递此参数，商家需传递此参数
        settingid: 'sz_1000_9999',                //Ntalker分配的缺省客服组id
        uid: '<%=tools.NullStr(Session["member_id"]) =="0" ? "" : tools.NullStr(Session["member_id"])%>',                              //用户id
        uname: '<%=tools.NullStr(Session["member_nickname"])%>',                   //用户名
        userlevel: ''
    }

</script>
<script type="text/javascript" src="http://dl.ntalker.com/js/b2b/ntkfstat.js?siteid=sz_1000" charset="utf-8">
</script>






