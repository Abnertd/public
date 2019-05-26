


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>供应商信息</title>
    <link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
    <script src="/Scripts/jquery.js" type="text/javascript"></script>
    <script src="/Scripts/common.js" type="text/javascript"></script>
    <link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ChangeSupplierAuditStatus(obj) {
            $("#formadd").attr("action", "Supplier_Do.aspx?action=" + obj);
            document.formadd.submit();
        }
    </script>
    <script type="text/javascript">
        change_inputcss();
        btn_scroll_move();
    </script>
</head>
<body>
    <div class="content_div">
        <form id="formadd" name="formadd" method="post" action="/Supplier/Supplier_Do.aspx">
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
                <tr>
                    <td class="content_title">供应商信息</td>
                </tr>
                <tr>
                    <td class="content_content">

                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">

                            <tr>
                                <td class="cell_title">供应商名称</td>
                                <td class="cell_content">铁胆火车侠 【铁胆火车侠】</td>
                                <td class="cell_title">注册邮箱</td>
                                <td class="cell_content">123@126.com</td>
                            </tr>
                            <tr>
                                <td class="cell_title">注册时间</td>
                                <td class="cell_content">2015/7/23 17:58:43</td>
                                <td class="cell_title">所属城市</td>
                                <td class="cell_content">北京市 市辖区 东城区</td>
                            </tr>
                            <tr>
                                <td class="cell_title">地址</td>
                                <td class="cell_content">铁胆火车侠</td>
                                <td class="cell_title">邮政编码</td>
                                <td class="cell_content"></td>
                            </tr>
                            <tr>
                                <td class="cell_title">联系人</td>
                                <td class="cell_content">铁胆火车侠</td>
                                <td class="cell_title">手机</td>
                                <td class="cell_content">18810314610</td>
                            </tr>
                            <tr>
                                <td class="cell_title">电话</td>
                                <td class="cell_content">18810314610</td>
                                <td class="cell_title">传真</td>
                                <td class="cell_content">18810314610</td>
                            </tr>
                            <tr>
                                <td class="cell_title">审核状态</td>
                                <td class="cell_content">已审核</td>
                                <td class="cell_title">账户信息</td>
                                <td class="cell_content">保证金：￥0.00  </td>

                            </tr>
                            
                            <tr>
                                <td class="cell_title">代理费用比率</td>
                                <td class="cell_content">
                                    <input type="text" name="Supplier_AgentRate" value="0" />% </td>

                                <td class="cell_title">运费模式</td>
                                <td class="cell_content"><input name="Supplier_DeliveryMode" type="radio" id="Supplier_DeliveryMode1" value="1" checked />
                                    单独计算</td>

                            </tr>
                            
                            <tr>
                                <td class="cell_title">供应商营销标签</td>
                                <td class="cell_content"> <input type="checkbox" name="Tag_ID" value="1"> 6000</td>
                                <td class="cell_title">供应商状态</td>
                                <td class="cell_content">
                                    <input name="Supplier_Status" type="radio" id="Supplier_Status" value="0"  />
                                    不启用
                                    <input name="Supplier_Status" type="radio" id="Supplier_Status1" value="1" checked="checked" />
                                    启用此账号
                                    <input name="Supplier_Status" type="radio" id="Supplier_Status2" value="2"  />
                                    冻结此账号</td>
                            </tr>

                            <tr>
                                <td class="cell_title">订单邮件提醒</td>
                                <td class="cell_content">
                                    <input name="Supplier_AllowOrderEmail" type="radio" id="Supplier_AllowOrderEmail" value="0"  />
                                    不启用
                                    <input name="Supplier_AllowOrderEmail" type="radio" id="Supplier_AllowOrderEmail1" value="1" checked="checked" />
                                    启用</td>

                                <td class="cell_title">开户人</td>
                                <td class="cell_content"></td>
                            </tr>
                            <tr>
                                <td class="cell_title">开户行</td>
                                <td class="cell_content">
                                    </td>
                                <td class="cell_title">开户网点</td>
                                <td class="cell_content"></td>
                            </tr>
                            <tr>
                                <td class="cell_title">银行账号</td>
                                <td class="cell_content"></td>
                                <td class="cell_title">注册IP</td>
                                <td class="cell_content">127.0.0.1</td>
                            </tr>
                            <tr>
                                <td class="cell_title">IP所在地</td>
                                <td class="cell_content"></td>

                                <td class="cell_title">可用积分数</td>
                                <td class="cell_content">0</td>
                            </tr>
                            <tr>
                                <td class="cell_title">最后登录时间</td>
                                <td class="cell_content">2015/7/24 10:04:43</td>

                                <td class="cell_title">登录次数</td>
                                <td class="cell_content">2</td>
                            </tr>

                        </table>
                    </td>
                </tr>
            </table>





            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
                <tr>
                    <td class="content_title">资质信息</td>
                </tr>
                <tr>
                    <td class="content_content">

                        <div id="cert_show" style="position: absolute; border: 1px solid #000; display: none;"></div>
                        <div id="cert_compare"></div>
                        <script>
                            function show_cert(url) {
                                $("#cert_show").html('<img src=' + url + ' width="600">')
                                $("#cert_show").show();
                                var ojbfoot = $("#cert_compare").offset().top - $("#cert_show").height() - 10;
                                var ojbleft = $("#cert_compare").width() / 2 - ($("#cert_show").width() / 2);
                                $("#cert_show").css("top", ojbfoot);
                                $("#cert_show").css("left", ojbleft);
                            }

                        </script>
                        <table width="100%" border="0" cellpadding="5" cellspacing="0">
                            <tr>
                            <td width="20%" align="center">
                                <table border="0" cellpadding="3" cellspacing="0" width="20%">
                                    <tr>
                                        <td align="center">
                                            <img src="http://img.yinaifin.com/shopcert/2015/07/23/201507231831404.jpg" width="200" height="200"
                                                onmouseover="show_cert('http://img.yinaifin.com/shopcert/2015/07/23/201507231831404.jpg');"
                                                onmouseout="$('#cert_show').hide();" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" height="30">
                                            <input type="button" name="btn_upload" value="上传" class="buttonupload" onclick="javascript: openUpload('supplier_cert1    _tmp');" />
                                            <input type="button" name="btn_upload" value="删除" class="buttonupload" onclick="javascript: delImage('supplier_cert1    _tmp');">
                                            <input type="hidden" name="supplier_cert1_tmp" id="supplier_cert1_tmp"
                                                value="/shopcert/2015/07/23/201507231831404.jpg" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">营业证书</td>
                                    </tr>
                                </table>

                            </td>
                            
                            <td width="20%" align="center">
                                <table border="0" cellpadding="3" cellspacing="0" width="20%">
                                    <tr>
                                        <td align="center">
                                            <img src="http://img.yinaifin.com/shopcert/2015/07/23/201507231831471.jpg" width="200" height="200"
                                                onmouseover="show_cert('http://img.yinaifin.com/shopcert/2015/07/23/201507231831471.jpg');"
                                                onmouseout="$('#cert_show').hide();" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" height="30">
                                            <input type="button" name="btn_upload" value="上传" class="buttonupload" onclick="javascript: openUpload('supplier_cert3    _tmp');" />
                                            <input type="button" name="btn_upload" value="删除" class="buttonupload" onclick="javascript: delImage('supplier_cert3    _tmp');">
                                            <input type="hidden" name="supplier_cert3_tmp" id="supplier_cert3_tmp"
                                                value="/shopcert/2015/07/23/201507231831471.jpg" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">组织机构代码</td>
                                    </tr>
                                </table>

                            </td>
                            
                            <td width="20%" align="center">
                                <table border="0" cellpadding="3" cellspacing="0" width="20%">
                                    <tr>
                                        <td align="center">
                                            <img src="http://img.yinaifin.com/shopcert/2015/07/23/201507231831576.jpg" width="200" height="200"
                                                onmouseover="show_cert('http://img.yinaifin.com/shopcert/2015/07/23/201507231831576.jpg');"
                                                onmouseout="$('#cert_show').hide();" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" height="30">
                                            <input type="button" name="btn_upload" value="上传" class="buttonupload" onclick="javascript: openUpload('supplier_cert4    _tmp');" />
                                            <input type="button" name="btn_upload" value="删除" class="buttonupload" onclick="javascript: delImage('supplier_cert4    _tmp');">
                                            <input type="hidden" name="supplier_cert4_tmp" id="supplier_cert4_tmp"
                                                value="/shopcert/2015/07/23/201507231831576.jpg" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">税务登记证</td>
                                    </tr>
                                </table>

                            </td>
                            
                            <td width="20%" align="center">
                                <table border="0" cellpadding="3" cellspacing="0" width="20%">
                                    <tr>
                                        <td align="center">
                                            <img src="http://img.yinaifin.com/shopcert/2015/07/23/201507231832030.jpg" width="200" height="200"
                                                onmouseover="show_cert('http://img.yinaifin.com/shopcert/2015/07/23/201507231832030.jpg');"
                                                onmouseout="$('#cert_show').hide();" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" height="30">
                                            <input type="button" name="btn_upload" value="上传" class="buttonupload" onclick="javascript: openUpload('supplier_cert5    _tmp');" />
                                            <input type="button" name="btn_upload" value="删除" class="buttonupload" onclick="javascript: delImage('supplier_cert5    _tmp');">
                                            <input type="hidden" name="supplier_cert5_tmp" id="supplier_cert5_tmp"
                                                value="/shopcert/2015/07/23/201507231832030.jpg" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">开户行许可证</td>
                                    </tr>
                                </table>

                            </td>
                            
                            <td width="20%" align="center">
                                <table border="0" cellpadding="3" cellspacing="0" width="20%">
                                    <tr>
                                        <td align="center">
                                            <img src="http://img.yinaifin.com/shopcert/2015/07/23/201507231832051.jpg" width="200" height="200"
                                                onmouseover="show_cert('http://img.yinaifin.com/shopcert/2015/07/23/201507231832051.jpg');"
                                                onmouseout="$('#cert_show').hide();" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" height="30">
                                            <input type="button" name="btn_upload" value="上传" class="buttonupload" onclick="javascript: openUpload('supplier_cert6    _tmp');" />
                                            <input type="button" name="btn_upload" value="删除" class="buttonupload" onclick="javascript: delImage('supplier_cert6    _tmp');">
                                            <input type="hidden" name="supplier_cert6_tmp" id="supplier_cert6_tmp"
                                                value="/shopcert/2015/07/23/201507231832051.jpg" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">法人代表身份证（正反面）</td>
                                    </tr>
                                </table>

                            </td>
                            </tr>
                </tr>


            </table>

            </td>
    </tr>
  </table>



        <div class="foot_gapdiv">
        </div>
            <div class="float_option_div" id="float_option_div">
                
                <input type="hidden" id="working" name="action" value="renew" />

                <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
                
                <input type="hidden" id="Supplier_id" name="Supplier_id" value="33" />
                <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location = 'Supplier_list.aspx';" />
            </div>
        </form>

    </div>
</body>
</html>
