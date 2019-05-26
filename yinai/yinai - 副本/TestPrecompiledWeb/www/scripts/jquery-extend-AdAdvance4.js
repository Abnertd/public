// Download by http://sc.xueit.com
$.fn.extend({
    AdAdvance1: function() {
        var listobj = this;
        var objs = $('dt', this);
        //var view = objs.length - 5; //parseInt( Math.random()*objs.length);
        
        objs.each(function(i) {
            $(this).click(function() { $('dd', listobj).hide(); $('.dropList3-hover', listobj).attr("class", ""); $(this).children("p").attr("class", "dropList3-hover"); $(this).next().show() })
            
//            if (i != view) {
//                $(this).next().hide();
//            }
//            else {
//                $(this).next().show();
//                $(this).children("p").attr("class", "dropList3-hover");
//            }
            
        });
    }
}); 