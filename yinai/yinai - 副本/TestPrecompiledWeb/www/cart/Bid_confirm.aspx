<%@ Page Language="C#" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>



<%@ Register Src="~/Public/cartTop.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>

<% 
    Session["url_after_login"] = "/cart/Bid_confirm.aspx";
    Glaer.Trade.B2C.BLL.ORD.IContractTemplate MyTemplate = Glaer.Trade.B2C.BLL.ORD.ContractTemplateFactory.CreateContractTemplate();
    ITools tools = ToolsFactory.CreateTools();
    Public_Class pub = new Public_Class();
    Cart cart = new Cart();
    Supplier supplier = new Supplier();
    Member member = new Member();
    Addr addr = new Addr();
    //Orders orders = new Orders();
    Orders orders = new Orders();
    Bid MyBid = new Bid();
    Glaer.Trade.B2C.BLL.MEM.IMemberAddress Myaddr;
    pub.Check_User_Audits();

    Session["favor_coupon_price"] = tools.NullInt(Session["favor_coupon_price"]);
    int BID = tools.CheckInt(Request["BID"]);
    int SupplyID = 0;
    if (!MyBid.BidOrderStatus(BID, ref SupplyID))
    {
        pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
    }

    Session["BIDSupplierID"] = tools.CheckStr(SupplyID.ToString());

    string gift = "";
    Myaddr = Glaer.Trade.B2C.BLL.MEM.MemberAddressFactory.CreateMemberAddress();


    //new Orders().GetEndOrdersInfoBySupplierID();
    orders.GetEndOrdersInfoBySupplierID();

    Session["Orders_Address_ID"] = 0;
    Session["Orders_Delivery_ID"] = 0;
    Session["Orders_Payway_ID"] = 0;

    Session["Loan_Product_Method_ID"] = 0;
    Session["Loan_Product_Term_ID"] = 0;
    Session["agreement_no"] = "0";
    Session["margin_rate"] = 0;
    Session["fee_rate"] = 0;

    Session["Orders_DeliveryTime_ID"] = 0;
    Session["delivery_fee"] = 0;        //运费
    Session["order_favor_coupon"] = "0";//优惠券编号
    Session["all_favor_coupon"] = "0";//优惠券使用信息
    Session["order_favorfee"] = 0;   //运费优惠编号
    Session["favor_fee"] = 0;        //运费优惠金额
    Session["favor_coupon_price"] = 0;        //优惠券优惠金额
    Session["favor_policy_price"] = 0;  //优惠政策优惠金额
    Session["favor_policy_id"] = 0;     //优惠政策优惠编号
    Session["total_price"] = 0;
    Session["total_coin"] = 0;
    Session["BID"] = BID;




    //合同部分
    string Contract_Template_ContentOnlyRead = "";
    string Sell_Contract_Template_EndFuJian = "";
    string Contract_Template_ContentEdit = "";
    //string Member_NickName = "";
    string supplier_Company_Name = "";
    
    SupplierInfo MemberSupplierinfo = null;
    string MemberSupplierCompanyName = "";
    
    
    
    MemberInfo memberinfo = member.GetMemberByID();
    if (memberinfo != null)
    {
        //Member_NickName = memberinfo.Member_NickName;
        MemberSupplierinfo = supplier.GetSupplierByID(memberinfo.Member_SupplierID);
        if (MemberSupplierinfo != null)
        {
            MemberSupplierCompanyName = MemberSupplierinfo.Supplier_CompanyName;
        }
    }
    else
    {
        MemberSupplierCompanyName = "---";
    }

    //int SupplyID = tools.CheckInt(Request["SupplyID"]);
    SupplierInfo supplierinfo = supplier.GetSupplierByID(SupplyID);
    if (supplierinfo != null)
    {
        supplier_Company_Name = supplierinfo.Supplier_CompanyName;
    }


    ContractTemplateInfo contractTemplateinfo = MyTemplate.GetContractTemplateBySign("Sell_Contract_Template_OnlyRead", pub.CreateUserPrivilege("d4d58107-0e58-485f-af9e-3b38c7ff9672"));
    if (contractTemplateinfo != null)
    {
        Contract_Template_ContentOnlyRead = contractTemplateinfo.Contract_Template_Content;


    }


    ContractTemplateInfo Sell_Contract_Template_EndFuJianInfo = MyTemplate.GetContractTemplateBySign("Sell_Contract_Template_EndFuJian", pub.CreateUserPrivilege("d4d58107-0e58-485f-af9e-3b38c7ff9672"));
    if (Sell_Contract_Template_EndFuJianInfo != null)
    {
        Sell_Contract_Template_EndFuJian = Sell_Contract_Template_EndFuJianInfo.Contract_Template_Content;
        Sell_Contract_Template_EndFuJian = Sell_Contract_Template_EndFuJian.Replace("{supplier_name}", supplier_Company_Name);
        Sell_Contract_Template_EndFuJian = Sell_Contract_Template_EndFuJian.Replace("{member_name}", MemberSupplierCompanyName);

    }



    ContractTemplateInfo contractTemplateinfo1 = MyTemplate.GetContractTemplateBySign("Sell_Contract_Template_FuJian", pub.CreateUserPrivilege("d4d58107-0e58-485f-af9e-3b38c7ff9672"));
    if (contractTemplateinfo1 != null)
    {
        Contract_Template_ContentEdit = contractTemplateinfo1.Contract_Template_Content;

    }

%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>订单信息确认<%=" - " + pub.SEO_TITLE()%></title>
    <meta name="Keywords" content="<% = Application["Site_Keyword"]%>" />
    <meta name="Description" content="<%=Application["Site_Description"]%>" />



    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index_newadd.css" rel="stylesheet" type="text/css" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />


    <script type="text/javascript" src="/scripts/common.js"></script>
    <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js"></script>
    <script src="/scripts/cart.js" type="text/javascript"></script>
      <script type="text/javascript" src="/Public/kindeditor/kindeditor-all.js"></script>
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
      

        #div_deliverypay table td {
            text-align: left;
        }

        #div_payway table td {
            text-align: left;
        }

        #div_paytype table td {
            text-align: left;
        }

       
        #ti311 p {
            height: 25px;
            line-height: 25px;
        }

        .cssgift {
            line-height: 2;
            padding-left: 20px;
        }

        .save_orders_address {
            cursor: pointer;
            margin: 5px 0 0 35px;
            font-size: 14px;
            font-weight: bold;
            color: #FFF;
            text-decoration: none;
            display: block;
            background-color: #2a71b3;
            text-align: center;
            width: 125px;
            height: 27px;
            line-height: 27px;
        }

        #div_payway img {
            display: inline;
        }

        #ti312 {
            width: 680px;
            background: #FFF;
            border: 1px dashed #b7d2df;
            margin-top: 10px;
            padding: 10px;
            color: #333;
        }

        .foot_info03 {
            border-top: 5px solid #ececec;
            width: 990px;
            margin: 0 auto;
        }

        .b38_main ul li .li_box p {
            width: 260px;
            margin: 0 auto;
            display: block;
            height: 20px;
            line-height: 20px;
            padding: 10px 0px 10px 26px;
            font-size: 13px;
            color: #666;
            border-bottom: 1px dashed #dddddd;
        }

            .b38_main ul li .li_box p img {
                float: left;
                display: block;
                position: relative;
                margin: 2px 0px 0px -23px;
            }


            
#select_address table td {
    padding: 5px;
    text-align: left;
}




#ti311 {
    background: #fff none repeat scroll 0 0;
    border: 1px dashed #eee1c1;
    margin-left: 30px;
    margin-top: 20px;
    padding-left: 10px;
    padding-top: 10px;
    width: 790px;
}
    </style>
  
    <script type="text/javascript">
        $(document).ready(function () {
            RefillAddress("div_area", "Member_Address_State", "Member_Address_City", "Member_Address_County", $("#Member_Address_State").val(), $("#Member_Address_City").val(), $("#Member_Address_County").val());
        });
    </script>
</head>
<body>
    <div id="head_box" style="border-bottom: 2px solid #ff6600;">
        <!--头部 开始-->
        <uctop:top runat="server" ID="Top" />

        <div class="head" style="width: 1000px;">
            <div class="logo"><a href="/">
                <img src="/images/logo.jpg"></a></div>
            <div class="tit">完善订单信息</div>
            <div class="head_right">
                <ul>
                    <li>
                        <img src="/images/icon04.jpg">正品保证</li>
                    <li>
                        <img src="/images/icon05.jpg">明码实价</li>
                    <li>
                        <img src="/images/icon06.jpg">售后保障</li>
                </ul>
                <div class="clear"></div>
            </div>
            <div class="clear"></div>
        </div>
    </div>
    <!--头部 结束-->
    <!--主体 开始-->
    <div class="content03">
        <div class="blk34" style="background-image: url(/images/shop_car_bg02.jpg);">
            <ul>
                <li class="on" style="margin-left: 13px;"><span>1</span>招标拍卖表</li>
                <li class="on" style="margin-left: 105px;"><span>2</span>完善订单信息</li>
                <li style="margin-left: 98px;"><span>3</span>订单提交成功</li>
            </ul>
            <div class="clear"></div>
        </div>
        <h2 class="title08" style="border-bottom: none;"><strong>完善订单信息页</strong> </h2>
        <%-- <div class="blk38">
                <h2><a href="/member/order_address.aspx" target="_blank">管理收货地址</a>收货信息</h2>
                <div class="b38_main">
                    <ul id="div_address">
                        <%cart.Cart_Address_List(); %>
                    </ul>
                    <div class="clear"></div>
                </div>
            </div>--%>
        <div class="blk38">
            <h2>收货信息<a href="javascript:void(0);" onclick="$('#guanbi_address').show();$('#xiugai_address').hide();$('#ti311').load('/cart/pub_do.aspx?action=addresslist&amp;timer='+Math.random());">修改收货人信息</a></h2>

            <div class="order_information_zm">
                <div class="main">
                    <%--收货人地址  地址展示--%>
                    <dl class="dst02" id="xiugai_address">
                        <dt>收货人信息<a href="javascript:void(0);" onclick="$('#guanbi_address').show();$('#xiugai_address').hide();$('#ti311').load('/cart/pub_do.aspx?action=addresslist&amp;timer='+Math.random());">[修改]</a></dt>
                        <dd class="table_padding_5" id="select_address"><% cart.Cart_Address_Info(); %></dd>
                    </dl>
                    <dl class="dst02" id="guanbi_address" style="display: none;">
                        <dt>收货人信息<a href="javascript:void(0);" onclick="$('#guanbi_address').hide();$('#xiugai_address').show();">[关闭]</a></dt>
                        <dd>
                            <% 
                                if (Session["member_logined"].ToString() == "True")
                                {
                                    Response.Write("<div id=\"ti311\">");
                                    //收货地址薄
                                    cart.Cart_Address_List();
                                    Response.Write("</div>");
                                }
                            %>
                            <%--修改收货人地址--%>
                            <form action="/member/order_address_do.aspx" method="post" id="form_address">
                                <div id="div_address">
                                    <% cart.Select_Cart_Address(0); %>
                                </div>
                                <input type="hidden" name="Orders_Address_Country" value="CN" />
                                <input type="hidden" id="action_address" name="action" value="cart_address_add" />
                                <p style="text-decoration: none; margin-top: 5px; margin-bottom: 5px; font-size: 15px;">
                                    <a href="javascript:void(0);" onclick="$.ajaxSetup({async: false});AddCartAddress(0);" class="a17" style="margin-left: 100px; color: #ff6600; margin-bottom: 10px;">[添加到常用地址]</a>
                                </p>
                                <a class="a16" href="javascript:void(0);" onclick="$.ajaxSetup({async: false});AddCartAddress(1);$('#guanbi_delivery').show();$('#xiugai_delivery').hide();ResetDelivery();LoadDelivery('div_delivery');LoadAddress('label_address');  SelectDelivery(-1);$('#delivery_info').show();">保存收货人信息</a>
                            </form>

                        </dd>
                    </dl>

                </div>
            </div>
        </div>


        <form name="ordersform" id="ordersform" method="post" action="/cart/bid_confirm_do.aspx">

            <div class="blk38">
                <h2>运输责任</h2>
                <div class="b38_main02">
                    <%-- <%cart.Bid_Cart_Delivery_List(); %>--%>
                    <ul>
                        <li>
                            <input type="radio" value="1" checked="checked" name="responsible" />
                            <span>卖方负责</span>
                        </li>
                        <li>
                            <input type="radio" value="2" name="responsible" />
                            <span>买方负责</span>
                        </li>
                    </ul>
                </div>
            </div>

            <div class="blk38" style="display: none">
                <h2>配送方式</h2>
                <div class="b38_main02" id="div_delivery">
                    <%cart.Cart_Delivery_List(); %>
                </div>
            </div>





            <div class="blk38">
                <h2>支付方式</h2>
                <div class="b38_main02">
                    <%cart.Cart_Payway_List(-1); %>
                </div>
            </div>



           <%-- <div class="blk38">
                <% =cart.Get_Cart_List() %>
            </div>--%>
               <div class="blk38">
                    <% =MyBid.BidOrderProductList(BID) %>
                </div>

            <div class="blk38">
                <h2>第二项、交付约定</h2>


                <textarea cols="80" id="Contract_Template_ContentEdit" name="Contract_Template_ContentEdit" rows="16" style="margin-top: 10px;">   <%=Server.HtmlDecode( Contract_Template_ContentEdit) %></textarea>
                <script type="text/javascript">
                    var About_ContentEditor;
                    KindEditor.ready(function (K) {
                        About_ContentEditor = K.create('#Contract_Template_ContentEdit', {
                            //width: '100%',
                            width: '998px;',
                            height: '350px',
                            filterMode: false,
                            afterBlur: function () { this.sync(); }
                        });
                    });
                </script>
            </div>

            <div class="blk38" style="margin-top: 15px;">
                <h2>其他合同条款</h2>
                <span id="Contract_Content" name="Contract_Content" rows="16" style="margin-top: 10px;"><%=Server.HtmlDecode( Contract_Template_ContentOnlyRead+Sell_Contract_Template_EndFuJian) %></span>
            </div>

           <%-- <div class="blk38">
                <% =MyBid.BidOrderProductList(BID) %>
            </div>--%>

            <div class="blk39">
                <div class="b39_left">
                    <div class="b39_info01">
                        <ul>
                            <li><span>收货信息：</span><label id="label_address"><%cart.Cart_Address_Infos(); %></label></li>
                            <li><span>支付方式：</span><label id="label_payway"><%cart.Load_Payway_Info(); %></label></li>
                        </ul>
                    </div>
                    <div class="b39_info02">
                        <ul>
                            <li><span>订单备注：</span><label><textarea name="order_note" cols="" rows=""></textarea></label></li>
                        </ul>
                    </div>
                </div>
                <div class="b39_right">
                    <div class="b39_info04" id="cartPrice">
                        <%cart.My_BidCartPrice(); %>
                    </div>
                    <div class="b39_info05">
                        <div id="total_price"><%cart.My_Carttotalprice(); %></div>
                        <p><a href="javascript:void(0);" onclick="$('#ordersform').submit();">提交订单</a></p>
                        <input type="hidden" name="action" id="action" value="savebidorder" />
                        <input type="hidden" name="SupplyID" id="SupplyID" value="<%=SupplyID %>" />
                        <input type="hidden" name="BID" id="BID" value="<%=BID %>" />
                    </div>
                </div>
                <div class="clear"></div>
            </div>

        </form>

    </div>
    <!--主体 结束-->
    <!--尾部 开始-->

    <script type="text/javascript">
        function apply_credit_check(obj, total_price) {
            var apply_credit = $(obj).val();
            if (apply_credit > total_price) {
                layer.msg('贷款金额不可大于商品总金额', { icon: 2, shade: 0.3, time: 1000 }, function () { $(obj).val(0); $(obj).focus; });
            }
        }
    </script>


    <ucbottom:bottom ID="Bottom" runat="server" />
    <!--尾部 结束-->

</body>
</html>
