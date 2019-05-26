
function getTotalHeight(){
	if($.browser.msie){
		return document.compatMode == "CSS1Compat"? document.documentElement.clientHeight :
				 document.body.clientHeight;
	}else{
		return self.innerHeight;
	}
}

 function getTotalWidth (){
	if($.browser.msie){
		return document.compatMode == "CSS1Compat"? document.documentElement.clientWidth :
				 document.body.clientWidth;
	}else{
		return self.innerWidth;
	}
}

function AutosizeImage(ImgD,maxwidth,maxheight){  
	 var image=new Image();
	 image.src=ImgD.src;
     if(image.width>0  &&  image.height>0){  
       flag=true;
       if(image.width/image.height >= maxwidth/maxheight){  
         if(image.width > maxwidth){      
         ImgD.width=maxwidth;  
         ImgD.height=(image.height*maxwidth)/image.width;  
         }else{  
         ImgD.width=image.width;      
         ImgD.height=image.height;  
         }
         }  
       else{  
         if(image.height>maxheight){      
         ImgD.height=maxheight;  
         ImgD.width=(image.width*maxheight)/image.height;            
         }else{  
         ImgD.width=image.width;      
         ImgD.height=image.height;  
         }  
         }  
       }  
}

function MM_findObj(n, d) { //v4.01
    var p,i,x;  if(!d) d=document; if((p=n.indexOf("?"))>0&&parent.frames.length) {
    d=parent.frames[n.substring(p+1)].document; n=n.substring(0,p);}
    if(!(x=d[n])&&d.all) x=d.all[n]; for (i=0;!x&&i<d.forms.length;i++) x=d.forms[i][n];
    for(i=0;!x&&d.layers&&i<d.layers.length;i++) x=MM_findObj(n,d.layers[i].document);
    if(!x && d.getElementById) x=d.getElementById(n); return x;
}

function confirmdelete(gotourl){
	if($("#dialog-confirmdelete").length == 0){
	    $("body").append("<div id=\"dialog-confirmdelete\" title=\"确定要删除吗?\"><p><span class=\"ui-icon ui-icon-alert\" style=\"float:left; margin:0 7px 20px 0;\"></span>记录删除后将无法恢复，您确定要删除吗？</p>");
    }
	$("#dialog-confirmdelete").dialog({
		modal: true,
		width: 400,
		buttons: {
			"确认": function() {
				$(this).dialog("close");
				location=gotourl;
			},
			"取消": function() {
				$(this).dialog("close");
			}
		}
	});
	$("#dialog-confirmdelete").dialog({
	    close:function(){
	        $(this).dialog("destroy");
	    }
	});
}




function confirmoperate(gotourl) {
    if ($("#dialog-confirmdelete").length == 0) {
        $("body").append("<div id=\"dialog-confirmdelete\" title=\"确定要执行此操作吗?\"><p><span class=\"ui-icon ui-icon-alert\" style=\"float:left; margin:0 7px 20px 0;\"></span>执行该操作后将无法恢复，您确定要执行该操作吗？</p>");
    }
    $("#dialog-confirmdelete").dialog({
        modal: true,
        width: 400,
        buttons: {
            "确认": function () {
                $(this).dialog("close");
                location = gotourl;
            },
            "取消": function () {
                $(this).dialog("close");
            }
        }
    });
    $("#dialog-confirmdelete").dialog({
        close: function () {
            $(this).dialog("destroy");
        }
    });
}

function confirmsumit(){
	if($("#dialog-confirmdelete").length == 0){
	    $("body").append("<div id=\"dialog-confirmdelete\" title=\"确定要执行此操作吗?\"><p><span class=\"ui-icon ui-icon-alert\" style=\"float:left; margin:0 7px 20px 0;\"></span>执行该操作后将无法恢复，您确定要执行该操作吗？</p>");
    }
	$("#dialog-confirmdelete").dialog({
		modal: true,
		width: 400,
		buttons: {
			"确认": function() {
				$(this).dialog("close");
				$("#formadd").submit();
			},
			"取消": function() {
				$(this).dialog("close");
			}
		}
	});
	$("#dialog-confirmdelete").dialog({
	    close:function(){
	        $(this).dialog("destroy");
	    }
	});
}

function RefillAddress(targetdiv, statename, cityname, countyname, statecode, citycode, countycode) 
{
	$.ajax({
		type: "get", 
		global: false, 
		async: false,
		dataType: "html",
		url: encodeURI("/public/ajax_address.aspx?action=fill&targetdiv="+ targetdiv +"&statename="+ statename +"&cityname="+ cityname +"&countyname="+ countyname +"&statecode="+ statecode +"&citycode="+ citycode +"&countycode="+ countycode +"&timer="+Math.random()),
		
		success:function(data){
			$("#"+ targetdiv).html(data);
		    $("#"+ statename).val(statecode);
            $("#"+ cityname).val(citycode);
            $("#"+ countyname).val(countycode);
		},
		error: function (){
			alert("Error Script");
		}
    });
}
function UODRefillAddress(targetdiv, statename, cityname, countyname, statecode, citycode, countycode) 
{
	$.ajax({
		type: "get", 
		global: false, 
		async: false,
		dataType: "html",
		url: encodeURI("/public/ajax_address.aspx?action=filluod&targetdiv="+ targetdiv +"&statename="+ statename +"&cityname="+ cityname +"&countyname="+ countyname +"&statecode="+ statecode +"&citycode="+ citycode +"&countycode="+ countycode +"&timer="+Math.random()),
		
		success:function(data){
			$("#"+ targetdiv).html(data);
		    $("#"+ statename).val(statecode);
            $("#"+ cityname).val(citycode);
            $("#"+ countyname).val(countycode);
		},
		error: function (){
			alert("Error Script");
		}
    });
}
function CheckAll(form) {
    for (var i=0;i<form.elements.length; i++) {
        var e = form.elements[i];
        if (e.name != 'chkall' && e.id != 'orders_idn') {
            e.checked = form.chkall.checked;
        }
    }
}
function choose_opt(curopt,totalopt)
{
    for(var i=1;i<=totalopt;i++)
    {
        if(i==curopt)
        {
            $("#frm_opt_"+i).attr("class","opt_cur");
            $("#frm_optitem_"+i).show();
        }
        else
        {
            $("#frm_opt_"+i).attr("class","opt_uncur");
            $("#frm_optitem_"+i).hide();
        }
    }
}
function btn_scroll_move()
{
//    $(this).scroll(function() { // 页面发生scroll事件时触发  
//        var bodyTop = 0; 
//        if (typeof window.pageYOffset != 'undefined') { 
//        bodyTop = window.pageYOffset; 
//        } 
//        else if (typeof document.compatMode != 'undefined' && document.compatMode != 'BackCompat') 
//        { 
//        bodyTop = document.documentElement.scrollTop; 
//        } 
//        else if (typeof document.body != 'undefined') { 
//        bodyTop = document.body.scrollTop; 
//        } 
//        if(bodyTop+(document.documentElement.clientHeight)<=document.body.clientHeight)
//        {
//            $("#float_option_div").css("top", bodyTop+(document.documentElement.clientHeight-50));
//        }
//    }); 
}
function change_inputcss()
{
    $(document).ready(function(){ 
        $('input').focus(function(){ 
        $(this).addClass('focusField'); 
        });
        
        $('input').blur(function(){ 
        $(this).removeClass('focusField').addClass('idleField'); 
        });
    });
}

function getCheckBoxSelect(obj) {
    if ($("#" + obj).attr("checked") == true) {
        $("input[id*=privilege_id" + obj + "]").each(function () { this.checked = true; });
    }
    else {
        $("input[id*=privilege_id" + obj + "]").each(function () { this.checked = false; });
    }
}

/*可控制左右无缝循环（需引用MSClass.js）*/
function srcoll_left_right_Control(control_enabel,direction,left_div,right_div,scroll_body,scroll_content,total_width,total_height,scroll_width,scroll_speed,wait_time)
{
    /************control_enabel：是否启用按钮控制ID************/
    /************direction：滚动方向：0上 1下 2左 3右************/
    /************left_div：左控制按钮ID************/
    /************right_div：右控制按钮ID************/
    /************scroll_body：循环主体容器ID************/
    /************scroll_content：循环主体内容容器ID************/
    /************total_width：循环体总宽度************/
    /************total_height：循环体总高度************/
    /************scroll_width：每次循环宽度（0为翻屏）************/
    /************scroll_speed：循环速度步长（越大越慢）************/
	/************wait_time：翻屏等待时间************/
    
    var MarqueeDivControl=new Marquee([scroll_body,scroll_content],direction,0,total_width,total_height,scroll_speed,wait_time,29,scroll_width);
    if(control_enabel==true)
    {
        $("#"+left_div).click(function(){MarqueeDivControl.Run(3);});	
        $("#"+right_div).click(function(){MarqueeDivControl.Run(2);});	
    }
}



function GetrowNum() {
    return 20;
}
function GetrowList() {
    return [10, 20, 40];
}


function change_mainpurchasecate(target_div, obj) {
    //$("#"+target_div).html(obj + "/product/product_do.aspx?action=change_maincate&target="+target_div+"&cate_id=" + $("#"+ obj).val() +"");
    $("#" + target_div).load("/Supplier/Supplier_Purchase_do.aspx?action=change_mainpurchasecate&target=" + target_div + "&cate_id=" + $("#" + obj).val() + "&timer=" + Math.random());
}


function select_AllBrand()
{
    if ($('#chk_allbrand').attr("checked")) {
        $("input[name='ProductType_Brand']").attr("checked", true);
    }
    else {
        $("input[name='ProductType_Brand']").attr("checked", false);
    }
}


function RefillAddressNew(targetdiv, statename, cityname, countyname, statecode, citycode, countycode) {
    $.ajax({
        type: "get",
        global: false,
        async: false,
        dataType: "html",
        url: encodeURI("/public/ajax_address.aspx?action=newfill&targetdiv=" + targetdiv + "&statename=" + statename + "&cityname=" + cityname + "&countyname=" + countyname + "&statecode=" + statecode + "&citycode=" + citycode + "&countycode=" + countycode + "&timer=" + Math.random()),

        success: function (data) {
            $("#" + targetdiv).html(data);
            $("#" + statename).val(statecode);
            $("#" + cityname).val(citycode);
            $("#" + countyname).val(countycode);
        },
        error: function () {
            alert("Error Script");
        }
    });
}