<%@ Control Language="C#" ClassName="Bottom" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbtm" TagName="bottom_simple" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%
    Public_Class pub = new Public_Class();
    PageURL pageurl = new PageURL();
    CMS cms = new CMS();
    Product product = new Product();
    Orders orders = new Orders();
    ITools tools = ToolsFactory.CreateTools();

    string product_price = "", productUrl = "";
    int product_id = tools.CheckInt(Request["product_id"]), supplierid=1000;
    string path = Request.Path.ToLower();
    string sellerid = "";
    
    ProductInfo productinfo = product.GetProductByID(product_id);
    if (productinfo != null)
    {
        supplierid = supplierid + productinfo.Product_SupplierID;

        sellerid = "sz_" + supplierid;
        
        if (productinfo.Product_PriceType == 1)
        {
            product_price = pub.FormatCurrency(productinfo.Product_Price);
        }
        else
        {
            product_price = pub.FormatCurrency(pub.GetProductPrice(productinfo.Product_ManualFee, productinfo.Product_Weight));
        }

        productUrl = tools.NullStr(Application["Site_URL"]) + pageurl.FormatURL(pageurl.product_detail, productinfo.Product_ID.ToString());
    }
    
    
%>
<!--尾部 开始-->

<div class="foot">
      <div class="foot_info01">
            <div class="foot_info01_main">
                  <dl>
                       <dt><img src="/images/foot_icon01.jpg"></dt>
                       <dd>全场100%正品保障</dd>
                       <div class="clear"></div>
                  </dl>
                  <dl style=" margin-left:114px;">
                       <dt><img src="/images/foot_icon02.jpg"></dt>
                       <dd>明码实价诚信经营</dd>
                       <div class="clear"></div>
                  </dl>
                  <dl style=" margin-left:106px;">
                       <dt><img src="/images/foot_icon03.jpg"></dt>
                       <dd>满100元包邮费</dd>
                       <div class="clear"></div>
                  </dl>
                  <dl style="float:right;">
                       <dt><img src="/images/foot_icon04.jpg"></dt>
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
                            <%=cms.Help_Left_Sub_Nav1(6)%>
                      </dd>
                  </dl>
                  <dl class="dst01" style=" margin-left:64px;">
                      <dt>支付&配送</dt>
                      <dd>
                      
                            <%=cms.Help_Left_Sub_Nav1(7)%>
                      </dd>
                  </dl>
                  <dl class="dst02" style=" margin-left:64px;">
                      <dt><span><img src="/images/手机端.png">手机端</span><span><img src="/images/微信端.png">微信端</span></dt>
                      <dd>400-8108-802<span>（周一至周五 7:00-17:00）</span></dd>
                  </dl>
                  <dl class="dst01" style=" margin-left:64px;">
                      <dt>售后服务</dt>
                      <dd>
                        
                              <%=cms.Help_Left_Sub_Nav1(8)%>
                      </dd>
                  </dl>
                  <dl class="dst01" style="float:right;">
                      <dt>招商合作</dt>
                      <dd>
                       
                           <%=cms.Help_Left_Sub_Nav1(9)%>
                      </dd>
                  </dl>
            </div>
      </div>
      <div class="foot_info03">
            <p><a href="/about/index.aspx?sign=aboutus" target="_blank">关于我们</a>\<a href="/about/index.aspx?sign=job" target="_blank">招聘信息</a>\<a href="/about/index.aspx?sign=service" target="_blank">服务条款</a>\<a href="/about/index.aspx?sign=ad" target="_blank">广告服务</a>\<a href="/about/index.aspx?sign=contract" target="_blank">联系我们</a></p>
            <p>《中华人民共和国信息产业部》备案：京ICP备15051883号 </p>
            <p>CopyRight ©  2012-2015, All Rights Reserved. </p>
            <p>技术支持：北京光蓝网络科技有限公司</p>
            <p><img src="/images/foot_img.jpg"></p>
      </div>
</div>

<!--尾部 结束-->





<script language="javascript" type="text/javascript">

    var orderid=0;

    $.ajax({ type: "post", url: "/member/account_do.aspx?action=get_erpparam_orderid", async: false, success: function (msg) { orderid = msg; } });

    NTKF_PARAM = {
        siteid: 'sz_1000',                         //平台基础id
        sellerid: '<%=sellerid%>',                       //商户id，平台中不需传递此参数，商家需传递此参数
        settingid: 'sz_<%=supplierid%>_9999',                //Ntalker分配的缺省客服组id
        uid: '<%=tools.NullInt(Session["member_id"])%>',                              //用户id
        uname: '<%=tools.NullStr(Session["member_nickname"])%>',                   //用户名
        userlevel: '0'                         //用户级别,1为vip用户,0为普通用户

        <% if(productinfo!=null){%>
            ,ntalkerparam: {
                item:
	            {
	                'id': '<%=productinfo.Product_ID%>',                              //商品id
	                'name': '<%=productinfo.Product_Name%>',      //商品名称
	                'imageurl': '<%=pub.FormatImgURL(productinfo.Product_Img, "thumbnail")%>',   //商品图片
	                'url': '<%=productUrl%>',            //商品url
	                'marketprice': '<%=product_price%>',                                                           //商品市场价格
	                'siteprice': '<%=product_price%>'                                                           //商品当前价格
	            }
            }
        <%}%>

        <%if(path=="/member/order_list.aspx"){%>
           , erpparam: ""
        <%}%>
    }

    //NTKF_PARAM.erpparam = "orderid=" + orderid;


</script>


<script type="text/javascript" src="http://dl.ntalker.com/js/b2b/ntkfstat.js?siteid=sz_1000" charset="utf-8">
</script>
