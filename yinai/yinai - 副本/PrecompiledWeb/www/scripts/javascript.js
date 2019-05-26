// initialize menus
function initMenu() {

	// init key visual carousel
	jQuery('#index_kv_slider').jcarousel({
		// Configuration goes here
		scroll: 1,
		rtl: false,
		animation: 1000,
		auto: 4,
		wrap: "circular",
		initCallback: index_kv_slider_initCallback
	});

    // init submenus
	$("#navi").find(".navi_submenu").hide();
	$("#navi_menu1").bind({
		mouseenter: function(){
			$("#navi_submenu1").stop(true,true);
			$("#navi_submenu2").stop(true,true);
			$("#navi_submenu3").stop(true,true);
			$("#navi_submenu4").stop(true,true);
			$("#navi").find(".navi_submenu").hide();		
			$("#navi_submenu1").height(514);
			$("#navi_submenu1").slideDown();			
		},
		mouseleave: function(){
			$("#navi_submenu1").delay(300).slideUp();
		}
	})

	$("#navi_submenu1").bind({
		mouseenter: function(){
			$("#navi_submenu1").stop(true,true);			
		},
		mouseleave: function(){
			$("#navi_submenu1").slideUp();
		}
	})

	$("#navi_menu2").bind({
		mouseenter: function(){
			$("#navi_submenu1").stop(true,true);
			$("#navi_submenu2").stop(true,true);
			$("#navi_submenu3").stop(true,true);
			$("#navi_submenu4").stop(true,true);
			$("#navi").find(".navi_submenu").hide();		
			$("#navi_submenu2").height(514);			
			$("#navi_submenu2").slideDown();			
		},
		mouseleave: function(){
			$("#navi_submenu2").delay(300).slideUp();
		}
	})

	$("#navi_submenu2").bind({
		mouseenter: function(){
			$("#navi_submenu2").stop(true,true);			
		},
		mouseleave: function(){
			$("#navi_submenu2").slideUp();
		}
	})

	$("#navi_menu3").bind({
		mouseenter: function(){
			$("#navi_submenu1").stop(true,true);
			$("#navi_submenu2").stop(true,true);
			$("#navi_submenu3").stop(true,true);
			$("#navi_submenu4").stop(true,true);
			$("#navi").find(".navi_submenu").hide();	
			$("#navi_submenu3").height(134);			
			$("#navi_submenu3").slideDown();			
		},
		mouseleave: function(){
			$("#navi_submenu3").delay(300).slideUp();
		}
	})

	$("#navi_submenu3").bind({
		mouseenter: function(){
			$("#navi_submenu3").stop(true,true);			
		},
		mouseleave: function(){
			$("#navi_submenu3").slideUp();
		}
	})

	$("#navi_menu4").bind({
		mouseenter: function(){
			$("#navi_submenu1").stop(true,true);
			$("#navi_submenu2").stop(true,true);
			$("#navi_submenu3").stop(true,true);
			$("#navi_submenu4").stop(true,true);
			$("#navi").find(".navi_submenu").hide();
			$("#navi_submenu4").height(320);			
			$("#navi_submenu4").slideDown();			
		},
		mouseleave: function(){
			$("#navi_submenu4").delay(300).slideUp();
		}
	})

	$("#navi_submenu4").bind({
		mouseenter: function(){
			$("#navi_submenu4").stop(true,true);			
		},
		mouseleave: function(){
			$("#navi_submenu4").slideUp();
		}
	})

    // search button opacity toggle
	$("#navi_searchbtn").bind({
		mouseenter: function(){
			$("#navi_searchbtn").fadeTo(200,0.5);		
		},
		mouseleave: function(){
			$("#navi_searchbtn").fadeTo(200,1);		
		}
	})

}




// index: stop carousel on mouse enter
function index_kv_slider_initCallback(carousel)
{
	// Pause autoscrolling if the user moves with the cursor over the clip.
	carousel.clip.hover(function() {
		carousel.stopAuto();
	}, function() {
		carousel.startAuto();
	});
};




// footer collapse - expand
function initFooter() {

	$("#footer h2").eq(0).bind({
		click: function(){
			
			$("#footer_box").slideToggle(500,function() {$.scrollTo('#footer_icp',1000);$("#footer h2").eq(0).delay(1000).toggleClass('collapse');});
			
		}
	})
}




// index banner opacity toggle
function initBanner() {

	$("#index_banner img").bind({
		mouseenter: function(){
			$(this).fadeTo(200,0.65);		
		},
		mouseleave: function(){
			$(this).fadeTo(100,1);		
		}
	})
}



// about kingsoft left tree
function about_tree(menu1,menu2) {

	$("#about_tree dt a").eq(menu1-1).addClass('selected');
	$("#about_tree ul").eq(menu1-1).show();
	$("#about_tree ul").eq(menu1-1).find('a').eq(menu2-1).addClass('selected');

}




// image map hot area
function initMap() {
	jQuery(function($) {
		var $liveTip = $('<div id="livetip"></div>').hide().appendTo('body');
		var tipTitle = '';

		$('#footer_maptrigger area').bind('mouseover mouseout mousemove', function(event) {
			var $link = $(event.target).closest('area');
			if (!$link.length) { return; }
			var link = $link[0];

			if (event.type == 'mouseover' || event.type == 'mousemove') {
				$liveTip.css({
				top: event.pageY + 12,
				left: event.pageX + 12
				});
			};

			if (event.type == 'mouseover') {
				if (link.href=="http://kingsoft.com.au/") tipTitle="Kingsoft AUS";
				if (link.href=="http://www.kingsoft.com.my/") tipTitle="Kingsoft Malaysia";
				if (link.href=="http://www.kingsoft.jp/") tipTitle="Kingsoft Japan";
				$liveTip.html('<div>' + tipTitle + '</div>')  //<div>' + link.href + '</div>
				//.show();
				.fadeTo(200,1);
			};

			if (event.type == 'mouseout') {
				//$liveTip.hide();
				$liveTip.fadeTo(200,0);
				$liveTip.hide();
			};
		});
	});
}




// support: download tabs
function initSupportDownload() {

	$("#support_download a").eq(0).bind({
		mouseenter: function(){
			$(".support_download_item").hide();
			$("#support_download a").removeClass("selected");
			$(this).addClass("selected");
			$("#support_download_item1").show();
		}
	})

	$("#support_download a").eq(1).bind({
		mouseenter: function(){
			$(".support_download_item").hide();
			$("#support_download a").removeClass("selected");
			$(this).addClass("selected");
			$("#support_download_item2").show();
		}
	})

	$("#support_download a").eq(2).bind({
		mouseenter: function(){
			$(".support_download_item").hide();
			$("#support_download a").removeClass("selected");
			$(this).addClass("selected");
			$("#support_download_item3").show();
		}
	})

	$("#support_download a").eq(3).bind({
		mouseenter: function(){
			$(".support_download_item").hide();
			$("#support_download a").removeClass("selected");
			$(this).addClass("selected");
			$("#support_download_item4").show();
		}
	})

}




//support: pay methods
function initSupportPay()  {

	$("#support_pay_method5 a").eq(0).bind({
		click: function(){
			$("#support_pay_method5_sub").stop(true,true);
			$(this).toggleClass("selected");
			$("#support_pay_method5_sub").slideToggle(500);
			return false;
		}
	})

	$("#support_pay_method5_sub button").eq(0).bind({
		click: function(){
			$("#support_pay_method5_sub").stop(true,true);
			$("#support_pay_method5_sub").slideToggle(500,function() {$("#support_pay_method5 a").eq(0).toggleClass("selected");});
			return false;
		}
	})

}




// product: online game tabs
function initProductGame() {

	$("#product_onlinegame_panel a").eq(0).bind({
		mouseenter: function(){
			$(".product_onlinegame").hide();
			$("#product_onlinegame_panel a").removeClass("selected");
			$(this).addClass("selected");
			$("#product_onlinegame1").show();
		}
	})

	$("#product_onlinegame_panel a").eq(1).bind({
		mouseenter: function(){
			$(".product_onlinegame").hide();
			$("#product_onlinegame_panel a").removeClass("selected");
			$(this).addClass("selected");
			$("#product_onlinegame2").show();
		}
	})

	$("#product_onlinegame_panel a").eq(2).bind({
		mouseenter: function(){
			$(".product_onlinegame").hide();
			$("#product_onlinegame_panel a").removeClass("selected");
			$(this).addClass("selected");
			$("#product_onlinegame3").show();
		}
	})

	$("#product_onlinegame_panel a").eq(3).bind({
		mouseenter: function(){
			$(".product_onlinegame").hide();
			$("#product_onlinegame_panel a").removeClass("selected");
			$(this).addClass("selected");
			$("#product_onlinegame4").show();
		}
	})

	$("#product_onlinegame_panel a").eq(4).bind({
		mouseenter: function(){
			$(".product_onlinegame").hide();
			$("#product_onlinegame_panel a").removeClass("selected");
			$(this).addClass("selected");
			$("#product_onlinegame5").show();
		}
	})

	$("#product_onlinegame_panel a").eq(5).bind({
		mouseenter: function(){
			$(".product_onlinegame").hide();
			$("#product_onlinegame_panel a").removeClass("selected");
			$(this).addClass("selected");
			$("#product_onlinegame6").show();
		}
	})

}




// contact us page interaction
function initContactus() {
	jQuery(function($) {
		var $liveTip2 = $('<div id="livetip2"></div>').hide().appendTo('body');
		var tipTitle = '';

		$('#contactus_maptrigger area').bind('mouseover mouseout mousemove', function(event) {
			var $link = $(event.target).closest('area');
			if (!$link.length) { return; }
			var link = $link[0];

			if (event.type == 'mouseover' || event.type == 'mousemove') {
				$liveTip2.css({
				top: event.pageY + 12,
				left: event.pageX + 12
				});
			};

			if (event.type == 'mouseover') {
				var tipLink=link.href;
				tipLink=tipLink.substring(tipLink.length-1,tipLink.length);

				if (tipLink=="1") tipTitle="北京金山";
				if (tipLink=="2") tipTitle="珠海金山";
				if (tipLink=="3") tipTitle="成都金山";
				if (tipLink=="4") tipTitle="大连金山";
				if (tipLink=="5") tipTitle="日本金山";
				if (tipLink=="6") tipTitle="马来金山";
				$liveTip2.html('<div>' + tipTitle + '</div>') 
				//.show();
				.fadeTo(50,1);
			};

			if (event.type == 'mouseout') {
				//$liveTip.hide();
				$liveTip2.fadeTo(50,0);
				$liveTip2.hide();
			};
		});
	});

	$("dl.contactus_address dd a").each (function(i) {
		$("dl.contactus_address dd a").eq(i).bind({
			click: function(){
				$("div.contactus_map").eq(i).stop(true,true);
				$(this).toggleClass("selected");
				$("div.contactus_map").eq(i).slideToggle(500);
				return false;

			}
		});
	});

}




// support: card saler selector
function supportCardMap(idx)  {
	$(".support_cardlist").hide();
	$("#support_cardlist"+idx).show();
}




// support: saler map tips
function initSupportMap() {

	jQuery(function($) {
		var $liveTip3 = $('<div id="livetip3"></div>').hide().appendTo('body');
		var tipTitle = '';

		$('#support_card_r area').bind('mouseover mouseout mousemove click', function(event) {
			var $link = $(event.target).closest('area');
			if (!$link.length) { return; }
			var link = $link[0];

			if (event.type == 'mouseover' || event.type == 'mousemove') {
				$liveTip3.css({
				top: event.pageY + 12,
				left: event.pageX + 12
				});
			};

			if (event.type == 'mouseover') {
				var tipLinkText=link.href;
				var tipLinkArray=tipLinkText.split("#");
				var tipLink=tipLinkArray[1];
				if (tipLink=="1") tipTitle="黑龙江";
				if (tipLink=="2") tipTitle="吉林";
				if (tipLink=="3") tipTitle="辽宁";
				if (tipLink=="4") tipTitle="内蒙古";
				if (tipLink=="5") tipTitle="北京";
				if (tipLink=="6") tipTitle="天津";
				if (tipLink=="7") tipTitle="河北";
				if (tipLink=="8") tipTitle="山东";
				if (tipLink=="9") tipTitle="山西";
				if (tipLink=="10") tipTitle="河南";
				if (tipLink=="11") tipTitle="湖北";
				if (tipLink=="12") tipTitle="湖南";
				if (tipLink=="13") tipTitle="江西";
				if (tipLink=="14") tipTitle="安徽";
				if (tipLink=="15") tipTitle="江苏";
				if (tipLink=="16") tipTitle="浙江";
				if (tipLink=="17") tipTitle="上海";
				if (tipLink=="18") tipTitle="福建";
				if (tipLink=="19") tipTitle="广东";
				if (tipLink=="20") tipTitle="广西";
				if (tipLink=="21") tipTitle="海南";
				if (tipLink=="22") tipTitle="重庆";
				if (tipLink=="23") tipTitle="贵州";
				if (tipLink=="24") tipTitle="四川";
				if (tipLink=="25") tipTitle="云南";
				if (tipLink=="26") tipTitle="陕西";
				if (tipLink=="27") tipTitle="宁夏";
				if (tipLink=="28") tipTitle="甘肃";
				if (tipLink=="29") tipTitle="青海";
				if (tipLink=="30") tipTitle="新疆";
				$liveTip3.html('<div>' + tipTitle + '</div>') 
				.show();
			};

			if (event.type == 'mouseout') {
				$liveTip3.hide();
			};

			if (event.type == 'click') {
				var tipLinkText=link.href;
				var tipLinkArray=tipLinkText.split("#");
				var tipLink=tipLinkArray[1];
				supportCardMap(tipLink);
				this.blur();
				return false;
			};

		});
	});

}


// ir: left tree
function ir_tree(menu1,menu2) {

	$("#ir_tree dt a").eq(menu1-1).addClass('selected');
	//$("#ir_tree ul").eq(menu1-1).show();
	$("#ir_tree ul").eq(menu1-1).find('a').eq(menu2-1).addClass('selected');

}




// ir: faq list
function initFaqlist() {
	var $accoWrap = $("#ir_ir_faqlist");
	var $triggerWrap = $accoWrap.find("h3");
	var $trigger = $triggerWrap.find("a");
	var closeText = "关闭";

	$triggerWrap.each(function() {
		var $wrap = $("<div />", { "class" : "ir_faqcontent"}).insertAfter($(this));
		$wrap.nextAll().prependTo($wrap);
		$("<p />", { "class" : "ir_closefaq" })
			.html('<a href="" class="hide">' + closeText + '</a>')
			.appendTo($wrap);
	});
	var $content = $triggerWrap.next(".ir_faqcontent");
	var $closer = $content.find("p.ir_closefaq").find("a");
	$content.hide();
	$trigger.removeClass("close").addClass("show");

	$trigger.each(function(i) {
		$(this).bind({
			click : function() {
				if ( $(this).parent().next(".ir_faqcontent").is(":hidden") ) {
					$(this).removeClass("show").addClass("close");
					$(this).parent().next(".ir_faqcontent").slideDown(250);
				}
				this.blur();
				return false;
			}
		});
	});
	$closer.each(function(i) {
		$(this).bind({
			click : function() {
				$(this).parent().parent().slideUp(250);
				$(this).parent().parent().prev("h3").find("a").removeClass("close").addClass("show");
				return false;
			}
		});
	});

}

