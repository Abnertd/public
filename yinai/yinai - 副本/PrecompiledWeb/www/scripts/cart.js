function ResetDelivery()
{
    $.ajaxSetup({async: false});
    $.ajax({
		type: "get", 
		global: false, 
		async: false,
		dataType: "html",
		url: encodeURI("/cart/cart_do.aspx?action=resetdelivery&timer"+Math.random()),		
		success:function(data){
		},
		error: function (){
			alert("Error Script");
		}
    });
}

function LoadDelivery(targetdiv) 
{
    $.ajaxSetup({async: false});
	$.ajax({
		type: "get", 
		global: false, 
		async: false,
		dataType: "html",
		url: encodeURI("/cart/cart_do.aspx?action=selectdelivery&timer"+Math.random()),		
		success:function(data){
			$("#"+ targetdiv).html(data);
		},
		error: function (){
			alert("Error Script");
		}
    });
}

function SelectDelivery(cod) 
{
    $.ajaxSetup({async: false});
    $('#div_payway').load('/cart/pub_do.aspx?action=loadpay&cod='+cod+'&timer='+Math.random());
    
}

function KeepDelivery()
{    
    //var delivery_id=$("#Orders_Delivery_ID").attr('value');
    var pay_id = $('#Orders_Payway_ID').attr('value');
    var paytype_id = $('#Orders_Paytype_ID').attr('value');
    //var time_id= $('#Orders_DeliveryTime_ID').attr('value');
    //var isnotify= $('#U_Orders_IsNotify').attr('value');
    //var paymentmode= $('#U_Orders_PaymentMode').attr('value');
    var SupplyID=$('#SupplyID').attr('value');
    $.ajax({
		type: "get", 
		global: false, 
		async: false,
		dataType: "html",
		//url: encodeURI("/cart/pub_do.aspx?action=KeepDelivery&delivery_id=" + delivery_id + "&pay_id=" + pay_id + "&paytype_id=" + paytype_id + "&time_id=" + time_id + "&isnotify=" + isnotify + "&paymentmode=" + paymentmode + "&timer=" + Math.random()),		
		url: encodeURI("/cart/pub_do.aspx?action=KeepDelivery&pay_id=" + pay_id + "&paytype_id=" + paytype_id + "&timer=" + Math.random()),		
		success:function(data){
		    $('#guanbi_delivery').hide();
			$('#xiugai_delivery').show();
			$('#div_deliverypay').load('/cart/pub_do.aspx?action=deliverypay&SupplyID='+SupplyID+'&timer='+Math.random());
			$('#cart_price').load('/cart/cart_do.aspx?action=update_totalprice&SupplyID='+SupplyID+'&timer='+Math.random());				
		},
		error: function (){
			alert("Error Script");
		}
    });
}

function Coupon_Verify()
{
    var coupon_code=$('#coupon_code').attr('value');
    var verify_code=$('#verify_code').attr('value');
    $.ajax({
		type: "get", 
		global: false, 
		async: false,
		dataType: "html",
		url: encodeURI("/cart/cart_do.aspx?action=coupon_verify&coupon_code="+coupon_code+"&verify_code="+verify_code+"&timer="+Math.random()),		
		success:function(data){
		    if(data=="")
		    {
		        $('#coupon_code').attr('value','');
		        $('#verify_code').attr('value','');
		        $('#youhui_yanzheng').show();
		    }
		    else
		    {
		        $('#coupon_code').attr('value','');
		        $('#verify_code').attr('value','');
		        $('#youhui_xiugai').show();
		        $('#youhui_guanbi').hide();
			    $('#youhui_list').html(data);
			    $('#cart_price').load('/cart/cart_do.aspx?action=update_totalprice&timer='+Math.random());	
			}		
		},
		error: function (){
			alert("Error Script");
		}
    });
}

function Del_Address(addressid)
{
    $.ajax({
		type: "get", 
		global: false, 
		async: false,
		dataType: "html",
		url: encodeURI("/supplier/order_address_do.aspx?action=cart_address_move&supplier_address_id=" + addressid + "&timer="+Math.random()),	
		success:function(data){
		    if(data=="true")
		    {
			    $('#ti311').load('/cart/pub_do.aspx?action=addresslist&timer='+Math.random());
			}		
		},
		error: function (){
			alert("Error Script");
		}
    });
}

function CheckLogin(url)
{
    $.ajaxSetup({async: false});
    $.ajax({
		type: "get", 
		global: false, 
		async: false,
		dataType: "html",
		
		url: encodeURI("/member/login_do.aspx?action=ajax_checklogin&url_login="+url+"&timer="+Math.random()),	
		success:function(data){
		    if(data=="True")
		    {
			  document.location.href=url;
			}		
			else
			{
			    $('#openBox1').attr('href','/member/registerlogin.aspx?timer='+Math.random());
			}
		},
		error: function (){
			alert("Error Script");
		}
    });
}

function AjaxCheckLogin_ShouCang(url,productid,id)
{
    $.ajaxSetup({async: false});
    $.ajax({
		type: "get", 
		global: false, 
		async: false,
		dataType: "html",
		url: encodeURI("/member/login_do.aspx?action=ajax_checklogin&url_login="+url+"&timer="+Math.random()),	
		success:function(data){
		    if(data=="True")
		    {
			    $.ajax({
		            type: "get", 
		            global: false, 
		            async: false,
		            dataType: "html",
		            url: encodeURI("/member/fav_do.aspx?action=ajax_goods&id="+productid+"&timer="+Math.random()),	
		            success:function(data1){
		                if(data1=="Exist")
		                {
			               alert("该商品已在您的收藏夹中！");            			  
			            }		
			            else if(data1=="True")
			            {
			                alert("该商品已添加至您的收藏夹中！");
			            }
			            else
			            {
			                alert("Error Script");
			            }
		            },
		            error: function (){
			            alert("Error Script");
		            }
                });
			}		
			else
			{
			    $('#'+id).attr('href','/member/registerlogin.aspx');
			    //$('#'+id).zxxbox();
			}
		},
		error: function (){
			alert("Error Script");
		}
    });
}

//删除收藏商品
function AjaxDelShouCang(productid)
{
    $.ajax({
        type: "get", 
        global: false, 
        async: false,
        dataType: "html",
        url: encodeURI("/member/fav_do.aspx?action=goods_move&id="+productid+"&timer="+Math.random()),	
        success:function(data1){
            location.reload()
        },
        error: function (){
            alert("Error Script");
        }
    });
}

function check_Cart_All() {
    if ($('#chk_all_goods').attr("checked")) {
        $("input[name='chk_cart_goods']:enabled").attr("checked", true);
        //$("input[name='chk_cart_supplier']:enabled").attr("checked", true);
    }
    else {
        $("input[name='chk_cart_goods']").attr("checked", false);
        //$("input[name='chk_cart_supplier']").attr("checked", false);
    }
}

function check_Cart_SupplierAll(obj) {
    if ($('#chk_cart_supplier_' + obj).attr("checked")) {
        $("input[id*=chk_cart_goods_" + obj + "]").attr("checked", true);
    }
    else {
        $("input[id*=chk_cart_goods_" + obj + "]").attr("checked", false);
    }
}

function Set_CartPrice() {
    var goods_id = "0";
    var r = $("input[name='chk_cart_goods']");
    for (var i = 0; i < r.length; i++) {
        if (r[i].checked) {
            goods_id += "," + r[i].value;
        }
    }
    $.ajax({
        type: "post",
        url: "/cart/cart_do.aspx",
        data: "action=change_totalprice&goods_id=" + goods_id,
        success: function (msg) {
            $("#strong_totalprice").html(msg);
        }
    })
}

function submitCart() {
    var goods_id = "0";
    var r = $("input[name='chk_cart_goods']");
    for (var i = 0; i < r.length; i++) {
        if (r[i].checked) {
            goods_id += "," + r[i].value;
        }
    }

    if (goods_id == "0") {
        alert("请选择要结算的商品！");
        return false;
    } else {
        $.ajax({
            cache: false,
            type: "POST",
            url: "/cart/cart_do.aspx?action=check_cartproduct&chk_cart_goods=" + goods_id,
            async: false,
            success: function (msg) {
                if (msg == "success") {
                    window.location = '/cart/order_confirm.aspx?chk_cart_goods=' + goods_id;
                } else {
                    layer.alert(msg, 3, function () { window.location.reload() });
                }
            },
            error: function (request) {
                alert("Connection error");
            }
        })
    }
}

function setSelectAll(supplier_id) {
    //当没有选中某个子复选框时，SelectAll取消选中
    if (!$("#chk_cart_supplier_" + supplier_id).checked) {
        $("#chk_all_goods").attr("checked", false);
    }
    var chsub = $("input[type='checkbox'][name='chk_cart_supplier']").length; //获取subcheck的个数
    var checkedsub = $("input[type='checkbox'][name='chk_cart_supplier']:checked").length; //获取选中的subcheck的个数

    if (checkedsub == chsub) {
        $("#chk_all_goods").attr("checked", true);
    }
}

function setSelectSubAll(supplier_id, goods_id) {
    //当没有选中某个子复选框时，SelectAll取消选中
    if (!$("#chk_cart_goods_" + supplier_id + goods_id).checked) {
        $("#chk_cart_supplier_" + supplier_id).attr("checked", false);
    }
    var chsub = $("input[type='checkbox'][class='select-sub" + supplier_id + "']").length; //获取subcheck的个数
    var checkedsub = $("input[type='checkbox'][class='select-sub" + supplier_id + "']:checked").length; //获取选中的subcheck的个数

    if (checkedsub == chsub) {
        $("#chk_cart_supplier_" + supplier_id).attr("checked", true);
    }
}
function getCountPrice() {
    $.ajaxSetup({ async: false });

    var countprice = 0;
    $("input[name=OrdersGoodsArray]:checked").each(function () {
        countprice = countprice + parseFloat($("#price_" + $(this).val()).val().toString());
        $("input[name=price_" + $(this).val() + "_sub]").each(function () {
            countprice = countprice + parseFloat($(this).val().toString());
        });
    });
    //$(".b36_right p.p1").html("总计金额：<strong>￥" + parseFloat(countprice, 2) + "</strong>");
    $(".b36_right p.p1").html("总计金额：<strong>￥" + parseFloat(countprice, 2) + "</strong>");
}

function check_cartform()
{
    var goods_id = "0";
    var r = $("input[name='chk_cart_goods']");
    for (var i = 0; i < r.length; i++) {
        if (r[i].checked) {
            goods_id += "," + r[i].value;
        }
    }

    if (goods_id == "0") {
        alert("请选择要结算的商品！");
        return false;
    } else {
        return true;
    }


}

function setDefaultAddress(address_id)
{
    $.ajax({
        type: "post",
        dataType: "html",
        url: encodeURI("/cart/cart_do.aspx?action=setdefaultaddress&address_id=" + address_id + "&timer=" + Math.random()),
        success: function (msg) {
            location.reload();
        },
        error: function () {
            alert("Error Script");
        }
    });
}

function movecartgood(id)
{
    layer.confirm('确定要删除该商品？', { icon: 3 },
           function () {
               $.ajax({
                   cache: false,
                   type: "POST",
                   url: "/cart/cart_do.aspx?action=move&goods_id=" + id,
                   async: false,
                   success: function (msg) {
                       if (msg == "success") {
                           window.location.reload();
                       } else {
                           layer.msg('操作失败，请稍后重试！', 3);
                       }
                   },
                   error: function (request) {
                       alert("Connection error");
                   }
               })
           }
       );
}

//批量删除购物车商品
function MoveCartGoodsByID()
{
    var goods_id = "0";
    var r = $("input[name='chk_cart_goods']");
    for (var i = 0; i < r.length; i++) {
        if (r[i].checked) {
            goods_id += "," + r[i].value;
        }
    }

    if (goods_id == "0") {
        layer.msg('请选择要操作的商品', { icon: 2, time: 2000 });
        return false;
    } else {
        layer.confirm('确定要删除所选商品？', { icon: 3 },
            function () {
                $.ajax({
                    cache: false,
                    type: "POST",
                    url: "/cart/cart_do.aspx?action=batchmove&chk_cart_goods=" + goods_id,
                    async: false,
                    success: function (msg) {
                        if (msg == "success") {
                            window.location.reload();
                        } else {
                            layer.alert('操作失败，请稍后重试！', 3);
                            window.location.reload();
                        }
                    },
                    error: function (request) {
                        alert("Connection error");
                    }
                })
            }
        );
    }
}

function CartGoodsFavorites()
{
    var goods_id = "0";
    var r = $("input[name='chk_cart_goods']");
    for (var i = 0; i < r.length; i++) {
        if (r[i].checked) {
            goods_id += "," + r[i].value;
        }
    }

    if (goods_id == "0") {
        layer.alert('请选择要操作的商品', { icon: 6 });
        return false;
    } else {
        $.ajax({
            cache: false,
            type: "POST",
            url: "/cart/cart_do.aspx?action=batchmove&chk_cart_goods=" + goods_id,
            async: false,
            success: function (msg) {
                if (msg == "success") {
                    window.location.reload();
                } else {
                    layer.alert('操作失败，请稍后重试！', { icon: 6 });
                }
            },
            error: function (request) {
                alert("Connection error");
            }
        })
    }
}

function Set_DeliverySession(delivery_id)
{
    $.ajaxSetup({ async: false });
    $.ajax({
        type: "get",
        global: false,
        async: false,
        dataType: "html",
        url: encodeURI("/cart/cart_do.aspx?action=setDeliverySession&Delivery_Way_ID=" + delivery_id + "&timer" + Math.random()),
        success: function () {
            $("#cartPrice").load("/cart/cart_do.aspx?action=setCartPrice&timer" + Math.random());
            $("#total_price").load("/cart/cart_do.aspx?action=update_totalprice&timer" + Math.random());
        },
        error: function () {
            alert("Error Script");
        }
    });
}

function Set_DeliverySession2(delivery_id) {
    $.ajaxSetup({ async: false });
    $.ajax({
        type: "get",
        global: false,
        async: false,
        dataType: "html",
        url: encodeURI("/cart/cart_do.aspx?action=setDeliverySession&Delivery_Way_ID=" + delivery_id + "&timer" + Math.random()),
        success: function () {
            $("#cartPrice").load("/cart/cart_do.aspx?action=setCartPrice2&timer" + Math.random());
            $("#total_price").load("/cart/cart_do.aspx?action=update_totalprice&timer" + Math.random());
        },
        error: function () {
            alert("Error Script");
        }
    });
}

function Ajax_UpdateCartPrice()
{
    $.ajaxSetup({ async: false });
    $.ajax({
        type: "get",
        global: false,
        async: false,
        dataType: "html",
        url: encodeURI("/cart/cart_do.aspx?action=setCartPrice&timer" + Math.random()),
        success: function (data) {
            $("#" + targetdiv).html(data);
        },
        error: function () {
            alert("Error Script");
        }
    });
}

function Ajax_UpdateCartTotalPrice()
{
    $.ajaxSetup({ async: false });
    $.ajax({
        type: "get",
        global: false,
        async: false,
        dataType: "html",
        url: encodeURI("/cart/cart_do.aspx?action=setTotalPrice&timer" + Math.random()),
        success: function (data) {
            $("#" + targetdiv).html(data);
        },
        error: function () {
            alert("Error Script");
        }
    });
}

function load_loan_product(payway_id)
{
    if (payway_id == 2)
    {
        $('#Orders_Payway_ID').attr('value', payway_id);
        $('#xindai').show();
        $('#xindai').load('/cart/pub_do.aspx?action=load_loanproduct');
        $('#zuhe').hide();
        $('#loan_lable').html('');
        $('#label_payway').load('/cart/pub_do.aspx?action=loadpayinfo&payway_id=' + payway_id);
    }
    else if (payway_id == 3) {
        $('#Orders_Payway_ID').attr('value', payway_id);
        $('#zuhe').show();
        $('#loan_lable').load('/cart/pub_do.aspx?action=load_loanproduct');
        $('#xindai').html('');
        $('#xindai').hide();
        $('#label_payway').load('/cart/pub_do.aspx?action=loadpayinfo&payway_id=' + payway_id);
    }
    else
    {
        $('#Orders_Payway_ID').attr('value', payway_id);
        $('#label_payway').load('/cart/pub_do.aspx?action=loadpayinfo&payway_id=' + payway_id);
        $('#zuhe').hide();
        $('#loan_lable').html('');
        $('#xindai').hide();
        $('#xindai').html('');
    }
}

function UpdateCartProductAmount(product_id,goods_id,object)
{
    $.ajaxSetup({ async: false });
    $.ajax({
        type: "post",
        global: false,
        async: false,
        dataType: "html",
        url: encodeURI("/cart/cart_do.aspx?action=renew&product_id=" + product_id + "&goods_id=" + goods_id + "&buy_amount=" + $('#' + object).val() + "&timer" + Math.random()),
        success: function (msg) {
        },
        error: function () {
            alert("Error Script");
        }
    });
}

function GetCartConfirmUrl()
{
    var url="";
    $.ajax({
        async: false,
        url: "/cart/cart_do.aspx?action=getconfirm",
        success: function (msg) {
           url=msg
        }
    });
    return url;
}

//Ajax计入进货单
function Ajax_AddToCart(productid)
{
    $.ajaxSetup({ async: false });
    $.ajax({
        cache: false,
        type: "POST",
        url: "/cart/cart_do.aspx?action=add&product_id=" + productid + "&buy_amount=" + $("#buy_amount").val() + "&passto=add",
        async: false,
        success: function (msg) {
            alert(msg);
        },
        error: function (request) {
            alert("Connection error");
        }
    })
}

//Ajax立即购买
function Ajax_AddToCart_Confirm(productid)
{
    $.ajaxSetup({ async: false });
    $.ajax({
        cache: false,
        type: "POST",
        url: "/cart/cart_do.aspx?action=add&product_id=" + productid + "&buy_amount=" + $("#buy_amount").val() + "&passto=confirm",
        async: false,
        
      

        success: function (msg) {
          
           

            if (msg == "ToCart") {
                window.location.href = '/cart/my_cart.aspx';
            }
            else if (msg == "ToCartConfirm")
            {
                if (GetCartConfirmUrl() != "")
                {
                    window.location.href = GetCartConfirmUrl();
                }
            }
            else if (msg.length>1000) {
                alert("请登录后购买商品");
            }
            else
            {
                if (msg.indexOf("False") >-1) {
                  
                    alert("该子账号没有购买商品的权限!");
                } else {                  
                    alert(msg);
                }
               
               
            }
        },
        error: function (request) {
            alert("Connection error");
        }
    })
}


//function check_Cart_All() {
//    if ($('#chk_all_goods').attr("checked")) {
//        $("input[name='chk_cart_goods']:enabled").attr("checked", true);
//        //$("input[name='chk_cart_supplier']:enabled").attr("checked", true);
//    }
//    else {
//        $("input[name='chk_cart_goods']").attr("checked", false);
//        //$("input[name='chk_cart_supplier']").attr("checked", false);
//    }
//}

//商家中心全选
function check_SupplierCenter_ProductAll() {
    if ($('#chk_all_products').attr("checked")) {
        $("input[name='chk_products']:enabled").attr("checked", true);
        //$("input[name='chk_cart_supplier']:enabled").attr("checked", true);
    }
    else {
        $("input[name='chk_products']").attr("checked", false);
        //$("input[name='chk_cart_supplier']").attr("checked", false);
    }
}



//批量上下架商家中心商品
function BatchInsaleProduct() {
    var goods_id = "0";
    var r = $("input[name='chk_products']");
    for (var i = 0; i < r.length; i++) {
        if (r[i].checked) {
            goods_id += "," + r[i].value;
        }
    }

    if (goods_id == "0") {
        layer.msg('请选择上架的商品', { icon: 2, time: 2000 });
        return false;
    } else {
        layer.confirm('确定选中上架商品？', { icon: 3 },
            function () {
                $.ajax({
                    cache: false,
                    type: "POST",
                    //url: "/product/product_do.aspx?action=batchmove&chk_cart_goods=" + goods_id,
                    url: "/product/product_do.aspx?action=batchmove",
                    async: false,
                    success: function (msg) {
                        if (msg == "success") {
                            window.location.reload();
                        } else {
                            layer.alert('操作失败，请稍后重试！', 3);
                            window.location.reload();
                        }
                    },
                    error: function (request) {
                        alert("Connection error");
                    }
                })
            }
        );
    }
}


//批量上下架商家中心商品
function BatchCancleInsaleProduct() {
    var goods_id = "0";
    var r = $("input[name='chk_products']");
    for (var i = 0; i < r.length; i++) {
        if (r[i].checked) {
            goods_id += "," + r[i].value;
        }
    }
    alert(goods_id+"1111111111");

    if (goods_id == "0") {
        layer.msg('请选择下架的商品', { icon: 2, time: 2000 });
        return false;
    } else {
        layer.confirm('确定选中下架商品？', { icon: 3 },
            function () {
                $.ajax({
                    cache: false,
                    type: "POST",
                    //url: "/supplier/product_do.aspx?action=batchmove&chk_cart_goods=" + goods_id,
                    url: "/product/product_do.aspx?action=batchmove",
                    async: false,
                    success: function (msg) {
                        if (msg == "success") {
                            window.location.reload();
                        } else {
                            layer.alert('操作失败，请稍后重试！', 3);
                            window.location.reload();
                        }
                    },
                    error: function (request) {
                        alert("Connection error");
                    }
                })
            }
        );
    }
}

//批量上下架商家中心商品
function BatchCancelInsaleProduct() {
    var goods_id = "0";
    var r = $("input[name='chk_products']");
    for (var i = 0; i < r.length; i++) {
        if (r[i].checked) {
            goods_id += "," + r[i].value;
        }
    }   
    if (goods_id == "0") {
        layer.msg('请选择要操作的商品', { icon: 2, time: 2000 });
        return false;
    } else {
        layer.confirm('确定要下架选中商品？', { icon: 3 },
            function () {
                $.ajax({
                    cache: false,
                    type: "POST",
                    //url: "/cart/cart_do.aspx?action=batchmove&chk_cart_goods=" + goods_id,
                    url: "/cart/cart_do.aspx?action=batchmove&chk_cart_goods=" + goods_id,
                    //product
                    async: false,
                    success: function (msg) {
                        if (msg == "success") {
                            window.location.reload();
                        } else {
                            layer.alert('操作失败，请稍后重试！', 3);
                            window.location.reload();
                        }
                    },
                    error: function (request) {
                        alert("Connection error");
                    }
                })
            }
        );
    }
}




function Del_Address(addressid) {
    $.ajax({
        type: "get",
        global: false,
        async: false,
        dataType: "html",
        url: encodeURI("/member/order_address_do.aspx?action=cart_address_move&member_address_id=" + addressid + "&timer=" + Math.random()),
        success: function (data) {
            if (data == "true") {
                $('#ti311').load('/cart/pub_do.aspx?action=addresslist&timer=' + Math.random());
            }
        },
        error: function () {
            alert("Error Script");
        }
    });
}