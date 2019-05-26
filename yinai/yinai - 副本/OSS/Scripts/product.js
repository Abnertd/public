
function change_maincate(target_div,obj)
{
    //$("#"+target_div).html(obj + "/product/product_do.aspx?action=change_maincate&target="+target_div+"&cate_id=" + $("#"+ obj).val() +"");
    $("#"+target_div).load("/product/product_do.aspx?action=change_maincate&target="+target_div+"&cate_id=" +$("#"+ obj).val()+"&timer=" + Math.random());
}



function change_maincate1(target_div, obj) {
    //$("#"+target_div).html(obj + "/product/product_do.aspx?action=change_maincate&target="+target_div+"&cate_id=" + $("#"+ obj).val() +"");
    $("#" + target_div).load("/article/article_do.aspx?action=change_maincate1&target=" + target_div + "&cate_id=" + $("#" + obj).val() + "&timer=" + Math.random());
}




function check_product_maincate()
{
    //$("#div_Product_Cate").html("/product/product_do.aspx?action=check_maincate&parent="+$("#Product_cate_parent").val()+"cate_id=" +$("#Product_cate").val()+"");
    $("#div_Product_Cate").load("/product/product_do.aspx?action=check_maincate&parent="+$("#Product_cate_parent").val()+"&cate_id=" +$("#Product_cate").val()+"&timer=" + Math.random());
}

function check_product_cate(objId)
{
    $.ajax({
        url: encodeURI("/product/product_do.aspx?action=check_cate&objValue="+ $("#"+ objId).val() +"&timer="+ Math.random()),
        async: false,
        dataType: "html",
		success: function(data){
            $("#div_"+ objId).html(data);
		},
		error: function (){
			alert("Error Script");
		}
    });
}

function check_product_type(objId)
{
    $.ajax({
        url: encodeURI("/product/product_do.aspx?action=check_type&objValue="+ $("#"+ objId).val() +"&timer="+ Math.random()), 
        async: false,
        dataType: "html",
		success: function(data){
            $("#div_"+ objId).html(data);
		},
		error: function (){
			alert("Error Script");
		}
    });
}

function check_product_name(objId)
{
    $.ajax({
        url: encodeURI("/product/product_do.aspx?action=check_name&objValue="+ $("#"+ objId).val() +"&timer="+ Math.random()), 
        async: false,
        dataType: "html",
        success: function(data){
            $("#div_"+ objId).html(data);
        },
        error: function (){
            alert("Error Script");
        }
    });
}

function check_product_code(objId, pID)
{
    $.ajax({
        url: encodeURI("/product/product_do.aspx?action=check_code&objValue="+ $("#"+ objId).val() +"&product_id="+ pID +"&timer="+ Math.random()), 
        async: false,
        dataType: "html",
        success: function (data) {          
            $("#div_"+ objId).html(data);
		},
		error: function (){
			alert("Error Script");
		}
    });
}

function checkform_step1(){
	var tmpcheck1 = true;
	var tmpcheck2 = true;
	var tmpcheck3 = true;
	var tmpcheck4 = true;
	
	check_product_maincate();
	if ($("#div_Product_Cate").html().indexOf("tip-error.gif") > 0) {
        tmpcheck1 = false;
    }
	
//	check_product_cate("Product_CateID");
//    if ($("#div_Product_CateID").html().indexOf("tip-error.gif") > 0) {
//        tmpcheck1 = false;
//    }
    
	check_product_type("Product_TypeID");
    if ($("#div_Product_TypeID").html().indexOf("tip-error.gif") > 0) {
        tmpcheck2 = false;
    }
    
	check_product_name("Product_Name");
    if ($("#div_Product_Name").html().indexOf("tip-error.gif") > 0) {
        tmpcheck3 = false;
    }  
    
	check_product_code("Product_Code", $("#Product_ID").val());
    if ($("#div_Product_Code").html().indexOf("tip-error.gif") > 0) {
        tmpcheck4 = false;
    }  

	if (tmpcheck1 && tmpcheck2 && tmpcheck3 && tmpcheck4){return true;}else{return false;}
}


function showProductExtendList()
{
    
}