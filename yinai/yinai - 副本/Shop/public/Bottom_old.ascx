<%@ Control Language="C#" ClassName="Bottom" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<%
    ITools tools = ToolsFactory.CreateTools();
    CMS cms = new CMS();
    string Shop_id = tools.CheckStr(Session["shop_id"].ToString());
    Shop shop = new Shop();
    shop.Shop_Initial();
    
%>

<!--尾部 开始-->
<div class="foot">
        <div class="foot_main">
            <div class="foot_info01">
                <dl style=" margin-left:100px;">
                    <dt><img src="/images/foot_icon01.jpg" /></dt>
                    <dd>
                        <b>货品保障</b>
                        <p>机构鉴定，厂家直购。</p>
                    </dd>
                    <div class="clear"></div>
                </dl>
                <dl style=" margin-left:108px;">
                    <dt><img src="/images/foot_icon02.jpg" /></dt>
                    <dd>
                        <b>先行赔付</b>
                        <p>平台监管，资金垫付。</p>
                    </dd>
                    <div class="clear"></div>
                </dl>
                <dl style=" float:right; margin-right:100px;">
                    <dt><img src="/images/foot_icon03.jpg" /></dt>
                    <dd>
                        <b>营销支持</b>
                        <p>提供产品营销 策划等增值服务</p>
                    </dd>
                    <div class="clear"></div>
                </dl>
                <div class="clear"></div>
            </div>
            <div class="foot_info02">
                <dl class="dst01">
                    <dt>公司介绍</dt>
                    <dd>
                        <p><a href="<%=tools.NullStr(Application["Site_URL"]).TrimEnd('/') %>/about/index.aspx?sign=aboutus">关于我们</a></p>
                        <p><a href="<%=tools.NullStr(Application["Site_URL"]).TrimEnd('/') %>/about/index.aspx?sign=core">核心优势</a></p>
                        <p><a href="<%=tools.NullStr(Application["Site_URL"]).TrimEnd('/') %>/about/index.aspx?sign=address">公司地址</a></p>
                    </dd>
                </dl>
                <dl class="dst01">
                    <dt>产业服务</dt>
                    <dd>
                        <%=cms.Help_Left_Sub_Nav1(1) %>
                    </dd>
                </dl>
                <dl class="dst01">
                    <dt>融资便捷</dt>
                    <dd>
                        <%=cms.Help_Left_Sub_Nav1(2) %>
                    </dd>
                </dl>
                <dl class="dst01">
                    <dt>平台安全</dt>
                    <dd>
                        <%=cms.Help_Left_Sub_Nav1(3) %>
                    </dd>
                </dl>
                <dl class="dst02">
                    <dt><img src="/images/ew_pic.jpg" /></dt>
                    <dd>
                        <p onclick="NTKF.im_openInPageChat('sz_<%=shop.Shop_SupplierID+1000%>_9999');"><a href="javascript:void(0);"><img src="/images/foot_icon1.jpg" />在线客服</a></p>
                        <p><strong>400-882-6621</strong></p>
                        <p>(服务时间9:00-18:00)</p>
                        <p style=" margin-top:5px;"><a href="javascript:;"><img src="/images/foot_icon3.jpg" /></a><a href="javascript:;"><img src="/images/foot_icon4.jpg" /></a><a href="javascript:;"><img src="/images/foot_icon5.jpg" /></a></p>
                    </dd>
                    <div class="clear"></div>
                </dl>
            </div>
            <div class="foot_info03">
                <p><img src="/images/foot_icon6.jpg" /></p>
                <p>版权所有www.shunzefin.com 京ICP备12030634号</p>
            </div>
        </div>
    </div>
<!--尾部 结束-->

<script language="javascript" type="text/javascript">
    NTKF_PARAM = {
        siteid: 'sz_1000',                         //平台基础id
        sellerid: 'sz_<%=shop.Shop_SupplierID+1000%>',                       //商户id，平台中不需传递此参数，商家需传递此参数
             settingid: 'sz_<%=shop.Shop_SupplierID+1000%>_9999',                //Ntalker分配的缺省客服组id
             uid: '<%=tools.NullInt(Session["member_id"])%>',                              //用户id
             uname: '<%=tools.NullStr(Session["member_nickname"])%>',                   //用户名
             userlevel: '0'
         }
    </script>
    <script type="text/javascript" src="http://dl.ntalker.com/js/b2b/ntkfstat.js?siteid=sz_1000" charset="utf-8">
    </script>

<div class="right_scroll">
      <ul>
          <li style="z-index:1;">
              <div class="img_box02" onclick="NTKF.im_openInPageChat('sz_<%=shop.Shop_SupplierID+1000 %>_9999');">
        <a href="javascript:"></a></div></li>
          <li style="z-index:2;">
              <div class="img_box"><a href="javascript:;" onclick="favorites_shop_ajax(<%=Shop_id %>)"></a></div>
          </li>
        
        <li><div class="img_box03"><a href="#h_top"></a></div></li>
  </ul>
</div>

