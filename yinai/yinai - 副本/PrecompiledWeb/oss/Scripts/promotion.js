function inimember(status)
{
    $("#favor_memberid").val('');
    if(status==0)
    {
        $("#member_picker").html("<span class=\"pickertip\">已选择会员</span>");
    }
    else
    {
        $("#favor_memberid").val('-1');
        $("#member_picker").html("<span class=\"pickertip\">全部会员</span>");
    }
}
function inicate(status)
{
    $("#favor_cateid").val('');
    if(status==0)
    {
        $("#cate_picker").html("<span class=\"pickertip\">已选择类别</span>");
    }
    else
    {
        $("#favor_cateid").val('-1');
        $("#cate_picker").html("<span class=\"pickertip\">全部类别</span>");
    }
}
function inibrand(status)
{
    $("#favor_brandid").val('');
    if(status==0)
    {
        $("#brand_picker").html("<span class=\"pickertip\">已选择品牌</span>");
    }
    else
    {
        $("#brand_picker").html("<span class=\"pickertip\">全部品牌</span>");
    }
}
function iniproduct(status)
{
    $("#favor_productid").val('');
    if(status==0)
    {
        $("#product_picker").html("<span class=\"pickertip\">已选择产品</span>");
    }
    else
    {
        $("#product_picker").html("<span class=\"pickertip\">全部产品</span>");
    }
}
function inidelivery(status)
{
    $("#favor_deliveryid").val('');
    if(status==0)
    {
        $("#delivery_picker").html("<span class=\"pickertip\">已选择配送方式</span>");
    }
    else
    {
        $("#delivery_picker").html("<span class=\"pickertip\">全部配送方式</span>");
    }
}
function inipayway(status)
{
    $("#favor_paywayid").val('');
    if(status==0)
    {
        $("#payway_picker").html("<span class=\"pickertip\">已选择支付方式</span>");
    }
    else
    {
        $("#payway_picker").html("<span class=\"pickertip\">全部支付方式</span>");
    }
}
function picker_member_del(member_id)
{
    $.ajax({
        url: encodeURI("/promotion/picker_do.aspx?action=showmember&member_id=" + $("#favor_memberid").val() + "&mid=" + member_id + "&timer=" + Math.random()),
        type: "get",
        global: false,
        async: false,
        dataType: "html",
        success: function (data) {
            $("#member_picker").html(data);
        },
        error: function () {
            alert("Error Script");
        }
    });
}
function picker_cate_del(cate_id)
{
    $.ajax({
        url: encodeURI("/promotion/picker_do.aspx?action=showcate&cate_id=" + $("#favor_cateid").val() + "&cid=" + cate_id + "&timer=" + Math.random()),
        type: "get",
        global: false,
        async: false,
        dataType: "html",
        success: function (data) {
            $("#cate_picker").html(data);
        },
        error: function () {
            alert("Error Script");
        }
    });
}
function picker_brand_del(brand_id)
{
    $.ajax({
        url: encodeURI("/promotion/picker_do.aspx?action=showbrand&brand_id=" + $("#favor_brandid").val() + "&bid=" + brand_id + "&timer=" + Math.random()),
        type: "get",
        global: false,
        async: false,
        dataType: "html",
        success: function (data) {
            $("#brand_picker").html(data);
        },
        error: function () {
            alert("Error Script");
        }
    });
}
function picker_product_del(product_id) {
    $.ajax({
        url: encodeURI("/promotion/picker_do.aspx?action=showproduct&product_id=" + $("#favor_productid").val() + "&pid=" + product_id + "&timer=" + Math.random()),
        type: "get",
        global: false,
        async: false,
        dataType: "html",
        success: function (data) {
            $("#product_picker").html(data);
        },
        error: function () {
            alert("Error Script");
        }
    });

}

function picker_product_delgroup(product_id)
{
    $.ajax({
        url: encodeURI("/promotion/picker_do.aspx?action=showproduct&target=group&product_id=" + $("#favor_productid").val() + "&pid=" + product_id + "&timer=" + Math.random()),
        type: "get",
        global: false,
        async: false,
        dataType: "html",
        success: function (data) {
            $("#product_picker").html(data);
        },
        error: function () {
            alert("Error Script");
        }
    });
}
function picker_limitproduct_del(product_id)
{
    $.ajax({
        url: encodeURI("/promotion/picker_do.aspx?action=showproduct&limit=1&product_id=" + $("#favor_productid").val() + "&pid=" + product_id + "&timer=" + Math.random()),
        type: "get",
        global: false,
        async: false,
        dataType: "html",
        success: function (data) {
            $("#product_picker").html(data);
        },
        error: function () {
            alert("Error Script");
        }
    });
}
function picker_groupproduct_del(product_id)
{
    $.ajax({
        url: encodeURI("/promotion/picker_do.aspx?action=showproduct&group=1&product_id=" + $("#favor_productid").val() + "&pid=" + product_id + "&timer=" + Math.random()),
        type: "get",
        global: false,
        async: false,
        dataType: "html",
        success: function (data) {
            $("#product_picker").html(data);
        },
        error: function () {
            alert("Error Script");
        }
    });
}
function picker_delivery_del(delivery_id)
{
    $.ajax({
        url: encodeURI("/promotion/picker_do.aspx?action=showdelivery&delivery_id=" + $("#favor_deliveryid").val() + "&did=" + delivery_id + "&timer=" + Math.random()),
        type: "get",
        global: false,
        async: false,
        dataType: "html",
        success: function (data) {
            $("#delivery_picker").html(data);
        },
        error: function () {
            alert("Error Script");
        }
    });
}
function picker_payway_del(payway_id)
{
    $.ajax({
        url: encodeURI("/promotion/picker_do.aspx?action=showpayway&payway_id=" + $("#favor_paywayid").val() + "&payid=" + payway_id + "&timer=" + Math.random()),
        type: "get",
        global: false,
        async: false,
        dataType: "html",
        success: function (data) {
            $("#payway_picker").html(data);
        },
        error: function () {
            alert("Error Script");
        }
    });
}
function clk_selcate()
{
        $("#btn_cate").attr("href","cate_check.aspx?cateid="+ $("#favor_cateid").val()+"&timer=" + Math.random());
        $("#btn_cate").zxxbox({height:300, width:600,title:'类别选择',btnclose: false});
}
function refresh_memberlist()
{
    $.ajax({
        url: encodeURI("/promotion/picker_do.aspx?action=refresh_member&keyword=" + $("#keyword").val() + "&timer=" + Math.random()),
        type: "get",
        global: false,
        async: false,
        dataType: "html",
        success: function (data) {
            $("#memberlist").html(data);
        },
        error: function () {
            alert("Error Script");
        }
    });
}
function refresh_brandlist()
{
    $.ajax({
        url: encodeURI("/promotion/picker_do.aspx?action=refresh_brand&cateid=" + $("#cateid").val() + "&keyword=" + $("#keyword").val() + "&timer=" + Math.random()),
        type: "get",
        global: false,
        async: false,
        dataType: "html",
        success: function (data) {
            $("#brandlist").html(data);
        },
        error: function () {
            alert("Error Script");
        }
    });
}
function refresh_productlist()
{
    $.ajax({
        url: encodeURI("/promotion/picker_do.aspx?action=refresh_product&product_cate_parent=" + $("#Product_cate_parent").val() + "&tag=" + $("#tag").val() + "&product_cate=" + $("#Product_cate").val() + "&keyword=" + $("#keyword").val() + "&timer=" + Math.random()),
        type: "get",
        global: false,
        async: false,
        dataType: "html",
        success: function (data) {
            $("#productlist").html(data);
        },
        error: function () {
            alert("Error Script");
        }
    });
}
function refresh_deliverylist()
{
    $.ajax({
        url: encodeURI("/promotion/picker_do.aspx?action=refresh_delivery&keyword=" + $("#keyword").val() + "&timer=" + Math.random()),
        type: "get",
        global: false,
        async: false,
        dataType: "html",
        success: function (data) {
            $("#deliverylist").html(data);
        },
        error: function () {
            alert("Error Script");
        }
    });
}
function refresh_paywaylist()
{
    $.ajax({
        url: encodeURI("/promotion/picker_do.aspx?action=refresh_payway&keyword=" + $("#keyword").val() + "&timer=" + Math.random()),
        type: "get",
        global: false,
        async: false,
        dataType: "html",
        success: function (data) {
            $("#paywaylist").html(data);
        },
        error: function () {
            alert("Error Script");
        }
    });
}
//关闭选择器
function close_picker()
{
    $("#zxxBlank").hide();
	$("#wrapOut").hide();
}


//点击全选复选框
function jqgrid_allclick()
{
        var recordids;
        var selarry;
        var selstr="0";
        var selarrow;
        var selid;
        var j;
      $('.cbox').click(function(){ 
      if($('.cbox').attr("checked"))
      {
         jqgrid_selcurpageall();
        
        
      }
      else
      {
         recordids=jQuery('#list').jqGrid('getDataIDs');
         selarrow=$("#selarrow").val();
         if(recordids.length>0)
         {
                for(j=0;j<recordids.length;j++)
                {
                    selarrow=(","+selarrow+",").replace(","+recordids[j]+",",",")
                    selarrow=(selarrow+"").substring(1,selarrow.length-1);
                }
         }
         $("#selarrow").val(selarrow);
      }
      jqgrid_seltip_display();
      
    });
     
}

function jqgrid_selcurpageall()
{
    var recordids;
        var selarry;
        var selstr="0";
        var selarrow;
        var selid;
        recordids=jQuery('#list').jqGrid('getDataIDs');
        selarrow=$("#selarrow").val();
        if(selarrow!="0,")
        {
            if(recordids.length>0)
            {
                for(j=0;j<recordids.length;j++)
                {
                    selarrow=(","+selarrow+",").replace(","+recordids[j]+",",",")
                    selarrow=(selarrow+"").substring(1,selarrow.length-1);
                }
            }
            
            for(i=0;i<recordids.length;i++)
            {
                if(selarrow!="0,")
                {
                    selarrow=selarrow+","+recordids[i];
                }
                else
                {
                    selarrow=selarrow + recordids[i];
                }
            }
         }
         else
         {
            selarry=new Array();
            selarry.push(0);
            for(i=0;i<recordids.length;i++)
            {
                if(selarrow!="0,")
                {
                    selarrow=(","+selarrow+",").replace(","+recordids[i]+",",",")
                    selarrow=(selarrow+"").substring(1,selarrow.length-1);
                    selarrow=selarrow+","+recordids[i];
                }
                else
                {
                    selarrow=selarrow + recordids[i];
                }
            }
         }
         
        $("#selarrow").val(selarrow);
}
//点击全选链接
function jqgrid_selall()
{
        $('.cbox').attr("checked","true");
        var ids=jQuery('#list').jqGrid('getDataIDs');
        var j;
        var selid=jQuery('#list').jqGrid('getGridParam','selarrrow');
        while(selid.length>0)
        {
            jQuery("#list").jqGrid('setSelection',selid[0]);
        }
        for(var i=0;i<ids.length;i++)
        {
            jQuery("#list").jqGrid('setSelection',ids[i]);
        }
        $("#all_flag").val("1");
        $("#selarrow").val("0,"+$("#allids").val());
        jqgrid_seltip_display();
        
    
}
//点击取消选择链接
function jqgrid_cancelall()
{
    var selid=jQuery('#list').jqGrid('getGridParam','selarrrow');
    if($('.cbox').attr("checked"))
    {
        $('.cbox').attr("checked",false);
        while(selid.length>0)
        {
            jQuery("#list").jqGrid('setSelection',selid[0]);
        }
        
    }
    else
    {
        
        while(selid.length>0)
        {
            jQuery("#list").jqGrid('setSelection',selid[0]);
        }
    }
    $("#selarrow").val('0,');
    jqgrid_seltip_display();
    
}

//选择指定选项
function jqgrid_selarry()
{
    var selarrow=$("#selarrow").val();
    var selid;
    if(selarrow!="0,")
    {
        selid=selarrow.split(',');
    }
    else
    {
        selid=new Array();
    }
    
    //获取当前页的所以id集合
    var ids=jQuery('#list').jqGrid('getDataIDs');
    var j;
    var allflag=true;    //当前页全选标识
    var curflag=false;
    for(var i=0;i<ids.length;i++)
    {
        curflag=false;
        for(j=0;j<selid.length;j++)
        {
            if(ids[i]==selid[j])
            {
                jQuery("#list").jqGrid('setSelection',ids[i]);
                curflag=true;
                break;
            }
        }
        if(curflag==false)
        {
            allflag=false;
        }
    }
    if(allflag)
    {
        $('.cbox').attr("checked",true);
    }
    
}

//记录单击
function jqgrid_rowclick(id,status)
{
    if($("#all_flag").val()=="1"&&status==false)
    {
        $("#all_flag").val("0");
    }
    var selarrow=$("#selarrow").val();
    var selarry;
    if(status)
    {
        if(selarrow!="0,")
        {
            selarrow=(","+selarrow+",").replace(","+id+",",",")
            selarrow=(selarrow+"").substring(1,selarrow.length-1);
            selarrow=selarrow+","+id;
        }
        else
        {
            selarrow=selarrow + id;
        }
    }
    else
    {
        selarrow=(","+selarrow+",").replace(","+id+",",",")
        selarrow=(selarrow+"").substring(1,selarrow.length-1);
        if(selarrow=="0")
        {
            selarrow="0,"
        }
    }
    $("#selarrow").val(selarrow);
    jqgrid_seltip_display();
}

//选择统计提示显示
function jqgrid_seltip_display()
{
    var selid=jQuery('#list').jqGrid('getGridParam','selarrrow');
    var recordids=jQuery('#list').jqGrid('getDataIDs');
    
    var selarrow=$("#selarrow").val();
    var selarry;
    var j,i;
    var selstr="0";
    
    if(selarrow!="0,")
    {
        selarry=selarrow.split(',');   
    }
    else
    {
        selarry=new Array();
        selarry.push(0);
    }
    
    var sel_count=selarry.length-1;
//        if($("#all_flag").val()=="1")
//        {
//            
//            if(("0,"+$("#allids").val())==$("#selarrow").val())
//            {
//                sel_count=jQuery('#list').jqGrid('getGridParam','records');
//            }
//        }
    if(sel_count>0)
    {        
        $("#list_seltip").html("您当前选定了 "+sel_count+" 条记录，<a href=\"javascript:void(0);\" onclick=\"$('#all_flag').val('0');jqgrid_cancelall();\">点此取消选定</a>。");
        
        if(sel_count<jQuery('#list').jqGrid('getGridParam','records'))
        {
            $("#list_seltip").html($("#list_seltip").html()+"<a href=\"javascript:void(0);\" onclick=\"$('#all_flag').val('1');jqgrid_selall();\">点此选定全部</a> 的"+jQuery('#list').jqGrid('getGridParam','records')+"条记录");
        }
    }
    else
    {
        $("#list_seltip").html("您当前选定了 "+sel_count+" 条记录。<a href=\"javascript:void(0);\" onclick=\"$('#all_flag').val('1');jqgrid_selall();\">点此选定全部</a> 的"+jQuery('#list').jqGrid('getGridParam','records')+"条记录");
    }
}
function check_blank(obj)
{
    $('#tip_'+obj).load('ajax_promotion.aspx?action=isblank&val='+$('#'+obj).val()+'&timer='+Math.random())
    if($("#tip_" + obj).html().indexOf("error")>0)
    {
        return false;
    }
    else
    {
        return true;
    }
}
function check_memberall(obj)
{
    if($("#favor_memberall0").attr("checked")&&$("#"+obj).val()=="")
    {
        $("#tip_"+obj).html("<span class=\"tip_bg_error\">请选择会员！</span>");
    }
    else
    {
        $("#tip_"+obj).html("");
    }
    if($("#tip_" + obj).html().indexOf("error")>0)
    {
        return false;
    }
    else
    {
        return true;
    }
}
function check_cateall(obj)
{
    if($("#favor_target0").attr("checked"))
    {
        $('#tip_'+obj).load('ajax_promotion.aspx?action=checkcatebrand&cateall='+$('#favor_cateall1').attr("checked")+'&brandall='+$('#favor_brandall1').attr("checked")+'&timer='+Math.random())
    }
    else
    {
        $('#tip_'+obj).load('ajax_promotion.aspx?action=checkproduct&productall='+$('#favor_productall1').attr("checked")+'&timer='+Math.random())
    }
    if($("#tip_" + obj).html().indexOf("error")>0)
    {
        return false;
    }
    else
    {
        return true;
    }
}
function check_deliveryall(obj)
{
    if($("#favor_deliveryall0").attr("checked")&&$("#"+obj).val()=="")
    {
        $("#tip_"+obj).html("<span class=\"tip_bg_error\">请选择配送方式！</span>");
    }
    else
    {
        $("#tip_"+obj).html("");
    }
    if($("#tip_" + obj).html().indexOf("error")>0)
    {
        return false;
    }
    else
    {
        return true;
    }
}
function check_paywayall(obj)
{
    if($("#favor_paywayall0").attr("checked")&&$("#"+obj).val()=="")
    {
        $("#tip_"+obj).html("<span class=\"tip_bg_error\">请选择支付方式！</span>");
    }
    else
    {
        $("#tip_"+obj).html("");
    }
    if($("#tip_" + obj).html().indexOf("error")>0)
    {
        return false;
    }
    else
    {
        return true;
    }
}
function check_payline(obj)
{
    $('#tip_'+obj).load('ajax_promotion.aspx?action=checkpayline&val='+$('#'+obj).val()+'&timer='+Math.random())
    if($("#tip_" + obj).html().indexOf("error")>0)
    {
        return false;
    }
    else
    {
        return true;
    }
}
function check_price(selobj,obj)
{
    if($("#"+selobj).attr("checked"))
    {
        $('#tip_'+obj).load('ajax_promotion.aspx?action=checkprice&val='+$('#'+obj).val()+'&timer='+Math.random())
    }
    if($("#tip_" + obj).html().indexOf("error")>0)
    {
        return false;
    }
    else
    {
        return true;
    }
}
function check_percent(selobj,obj)
{
    if($("#"+selobj).attr("checked"))
    {
        $('#tip_'+obj).load('ajax_promotion.aspx?action=checkpercent&val='+$('#'+obj).val()+'&timer='+Math.random())
    }
    if($("#tip_" + obj).html().indexOf("error")>0)
    {
        return false;
    }
    else
    {
        return true;
    }
}
function check_state()
{
    if($('#favor_provinceall').attr("checked")==false&&($("#favor_province").val()==""||$("#favor_province").val()=="0"||$("#favor_province").val()==null))
    {
        $("#tip_state").html("<span class=\"tip_bg_error\">请选择适用地区！</span>");
    }
    else
    {
        $("#tip_state").html("");
    }
    if($("#tip_state").html().indexOf("error")>0)
    {
        return false;
    }
    else
    {
        return true;
    }
}
function check_validdate()
{
    if($('#StartDate').val()==""||$('#EndDate').val()=="")
    {
        $("#tip_valid").html("<span class=\"tip_bg_error\">请设置有效时间！</span>");
    }
    else
    {
        $("#tip_valid").html("");
    }
    if($("#tip_valid").html().indexOf("error")>0)
    {
        return false;
    }
    else
    {
        return true;
    }
}
function check_membergrade(chk_name)
{
    var chkflag=false;
    for(var i=0;i<$('input').length;i++)
    {
        
        if($('input:eq('+i+')').attr('name')==chk_name&&$('input:eq('+i+')').attr('checked'))
        {
            chkflag=true;
            break;
        }
    }
    if(!chkflag)
    {
        $("#tip_membergrade").html("<span class=\"tip_bg_error\">请选择针对会员！</span>");
    }
    else
    {
        $("#tip_membergrade").html("");
    }
    if($("#tip_membergrade").html().indexOf("error")>0)
    {
        return false;
    }
    else
    {
        return true;
    }
}
function check_favor_fee()
{
    $.ajaxSetup({async: false});
    var ck1=check_blank('Promotion_Fee_Title');
    var ck2=check_payline('Promotion_Fee_Payline');
    var ck3=check_price('Promotion_Fee_Manner2','Promotion_Fee_Price');
    var ck4=check_cateall('target');
    var ck5=check_state();
    var ck6=check_deliveryall('favor_deliveryid');
    var ck7=check_paywayall('favor_paywayid');
    var ck8=check_validdate();
    var ck9=check_membergrade('Member_Grade');
    if(ck1&&ck2&&ck3&&ck4&&ck5&&ck6&&ck7&&ck8&&ck9)
    {
        return true;
    }
    else
    {
        return false;
    }
    
}
function check_favor_policy()
{
    $.ajaxSetup({async: false});
    var ck1=check_blank('Promotion_Policy_Title');
    var ck2=check_payline('Promotion_Policy_Payline');
    var ck3=check_price('Promotion_Policy_Manner1','Promotion_Policy_Price');
    var ck4=check_cateall('target');
    var ck5=check_percent('Promotion_Policy_Manner2','Promotion_Policy_Percent');
    var ck8=check_validdate();
    var ck9=check_membergrade('Member_Grade');
    if(ck1&&ck2&&ck3&&ck4&&ck5&&ck8&&ck9)
    {
        return true;
    }
    else
    {
        return false;
    }
    
}
function check_favor_gift()
{
    $.ajaxSetup({async: false});
    var ck1=check_blank('Promotion_Gift_Title');
    //var ck2=check_payline('Promotion_Policy_Payline');
    //var ck3=check_price('Promotion_Policy_Manner1','Promotion_Policy_Price');
    var ck4=check_cateall('target');
    var ck8=check_validdate();
    var ck9=check_membergrade('Member_Grade');
    if(ck1&&ck4&&ck8&&ck9)
    {
        return true;
    }
    else
    {
        return false;
    }
    
}
function check_favor_coupon()
{
    $.ajaxSetup({async: false});
    var ck1=check_blank('Promotion_Coupon_Title');
    var ck2=check_payline('Promotion_Coupon_Payline');
    var ck3=check_price('Promotion_Coupon_Manner1','Promotion_Coupon_Price');
    var ck4=check_cateall('target');
    var ck5=check_percent('Promotion_Coupon_Manner2','Promotion_Coupon_Percent');
    var ck6=check_memberall('favor_memberid');
    var ck8=check_validdate();
    if(ck1&&ck2&&ck3&&ck4&&ck6&&ck5&&ck8)
    {
        return true;
    }
    else
    {
        return false;
    }
    
}
function check_favor_couponrule()
{
    $.ajaxSetup({async: false});
    var ck1=check_blank('Promotion_Coupon_Title');
    var ck2=check_payline('Promotion_Coupon_Payline');
    var ck3=check_price('Promotion_Coupon_Manner1','Promotion_Coupon_Price');
    var ck4=check_cateall('target');
    var ck5=check_percent('Promotion_Coupon_Manner2','Promotion_Coupon_Percent');
    if(ck1&&ck2&&ck3&&ck4&&ck6&&ck5)
    {
        return true;
    }
    else
    {
        return false;
    }
    
}