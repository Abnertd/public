<%@ Page Language="C#" %>
<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/HomeBottom.ascx" TagPrefix="ucbottom" TagName="bottom" %>

<%    
      Public_Class pub = new Public_Class();
      Supplier supplier = new Supplier();
      Product product = new Product();
      CMS cms = new CMS();
      Product myProduct = new Product();
      AD ad = new AD();
      //Session["Position"] = "Guide";
    
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title><%="新手引导 - " + pub.SEO_TITLE()%></title>
     <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index_newadd.css" rel="stylesheet" />
    <script src="scripts/jquery-1.8.3.js" type="text/javascript"></script>
    <script src="scripts/layer/layer.js"  type="text/javascript"></script>
    <!--滑动门 结束-->
<script src="/js/1.js" type="text/javascript"></script>
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
</head>
<body>
<!--头部 开始-->
 <uctop:top runat="server" ID="HomeTop" />
<!--头部 结束-->
<div class="content04" style="background-color:#f2f2f2;">
      <div class="content04_main_sz" style="background-color:#f2f2f2;">
            <h2 class="title09">新手引导</h2>
            <div class="blk48_sz">
                  <h2><strong>1</strong>什么是易耐产业金融</h2>
                  <div class="clear"></div>
                  <div class="b48_main_sz">
                        <dl>
                            <dt><img src="/images/new_pic06.jpg"></dt>
                            <dd>
                                <p> 易耐黄金珠宝产业有限公司（以下简称“易耐黄金”）是一家集金矿勘采选冶、自主品牌研发、加工生产、批发零售于一体，致力通过资源整合，以金融作为推助力实现自身品牌、营销、商业模式升级，从而带动行业持续有效发展，最终成长为中国最具品牌价值的文化珠宝领军企业。</p>
                                <p>北京易耐金服科技有限公司由易耐黄金牵头，与首都金融服务商会、长江商学院、黄金珠宝上下游供应链企业等数家单位联合，以消费者的差异化需求和终端体验为出发点，以提高行业整体盈利能力为己任，倾力打造首家垂直于行业的互联网金融服务平台——黄金珠宝行业的“产业银行”。</p>
                            </dd>
                            <div class="clear"></div>
                        </dl>
                  </div>
            </div>
            <div class="blk49_sz">
                  <h2><strong>2</strong>产品介绍</h2>
                  <div class="clear"></div>
                  <div class="b49_main_sz">
                         <p>易耐金服致力打造国内最大黄金珠宝消费金融服务平台，旗下设有B2B珠宝交易电子商城、易耐产业金融和云金服务平台三大核心业务板块，通过互联网（yinaifin.com）和线下千家珠宝门店将全国珠宝精品和品质生活带给消费者，并提供一系列的增值服务。</p>
                         <dl>
                             <dt><b>B2B珠宝交易平台</b><img src="/images/new_pic07.jpg"></dt>
                             <dd> 致力于打造国内最大的珠宝供应链交易平台，以批发和采购业务为核心，以撮合双方交易为基础，为供应商提供营销指导、数据支持，为采购商提供先行赔付、信贷买单等服务，提供从原料采购——设计加工——现货批发等一站式直达的产品链供应服务。</dd>
                             <div class="clear"></div>
                         </dl>
                  </div>
                  <div class="b49_main02_sz">
                        <dl>
                            <dt><img src="/images/new_pic08.jpg"></dt>
                            <dd>
                                <b>支付与信贷平台</b>
                                <p>易耐产业金融服务旨在针对交易平台中缺乏资金支持的中小微企业，提供信息流、资金流、以及个性化的金融服务。通过易耐支付平台（即智联快付）征信数据采集，对合格企业进行体系内授信支持，缓解上游企业存货压力，从而加快整个行业的货品、资金周转率。它的核心优势是：纯信用、无抵押、无担保，最快24小时放款，随借随还，一次性解决资金顽疾。</p>
                            </dd>
                            <div class="clear"></div>
                        </dl>
                  </div>
                  <div class="b49_main03_sz">
                        <dl>
                             <dt><b>云金管理平台</b><img src="/images/new_pic09.jpg"></dt>
                             <dd>云金服务平台旨在打通线上采购交易平台、支付平台、信贷平台，随时随地掌握终端珠宝零售店信息的安全高效的云平台。为黄金零售珠宝企业提供简易操作、部署灵活、极低拥有成本、移动高效协同的专业零售管理作业系统，打通会员微信营销模式、及时连接B2B采购平台、灵活财务管理，全方位提升管理水平。</dd>
                             <div class="clear"></div>
                         </dl>
                  </div>
            </div>
            <div class="blk50_sz">
                  <h2><strong>3</strong>开店如此简单</h2>
                  <div class="clear"></div>
                  <div class="b50_main_sz">
                        <b>简单6步，完成入驻开店</b>
                        <div class="b50_info_sz">
                              <ul>
                                  <li style="margin-left:36px;"><strong>1</strong><p>提交资料<span>(完善店铺资料)</span></p></li>
                                  <li style="margin-left:80px;"><strong>2</strong><p>平台审核<span>(审核通过)</span></p></li>
                                  <li style="margin-left:88px;"><strong>3</strong><p>缴纳保证金</p></li>
                                  <li style="margin-left:88px;"><strong>4</strong><p>激活账户</p></li>
                                  <li style="margin-left:88px;"><strong>5</strong><p>发布商品</p></li>
                                  <li style="margin-left:88px;"><strong>6</strong><p>成功开店</p></li>
                              </ul>
                              <div class="clear"></div>
                        </div>
                        <a href="/login.aspx">我要开店</a>
                  </div>
            </div>
            <div class="blk51_sz">
                  <h2><strong>4</strong>申请贷款如此容易</h2>
                  <div class="clear"></div>
                  <div class="b51_main_sz">
                        <b>简单5步，资金到手</b>
                        <div class="b51_info_sz">
                              <dl>
                                   <dt>01</dt>
                                   <dd>买家下单</dd>
                                   <div class="clear"></div>
                              </dl>
                              <dl>
                                   <dt>02</dt>
                                   <dd>申请信贷资金</dd>
                                   <div class="clear"></div>
                              </dl>
                              <dl>
                                   <dt>03</dt>
                                   <dd>信贷审核</dd>
                                   <div class="clear"></div>
                              </dl>
                              <dl>
                                   <dt>04</dt>
                                   <dd>审核通过</dd>
                                   <div class="clear"></div>
                              </dl>
                              <dl>
                                   <dt>05</dt>
                                   <dd>支付成功</dd>
                                   <div class="clear"></div>
                              </dl>
                              <div class="clear"></div>
                        </div>
                        <a href="/login.aspx" >我要采购</a>
                  </div>
            </div>
          <div class="blk50_sz">
                  <h2><strong>5</strong>珠宝O2O平台</h2>
                  <div class="clear"></div>
                  <div class="b50_main02_sz">
                        <dl>
                             <dt><img src="/images/new3_pic07.jpg"></dt>
                             <dd>为B2B平台生态单元建设良好的生态环境,通过创新业务满足增量市场消费者需求,线上线下实现业务和流量的交互，储备新零售商业模式,以终端消费者的需求作为平台承载核心，链接线上零售商与线下消费者最直接有效的通道平台。线上线下实现业务和流量的交互，实现O2O化。</dd>
                             <div class="clear"></div>
                        </dl>
                  </div>
            </div>
      </div>
</div>
<!--尾部 开始-->
<ucbottom:bottom runat="server" ID="Bottom" />
<!--尾部 结束-->
</body>
</html>

