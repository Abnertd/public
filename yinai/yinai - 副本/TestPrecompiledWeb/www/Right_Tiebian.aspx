<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <meta name="keywords" content="JS代码,在线客服,JS广告代码,JS特效代码" />
    <meta name="description" content="此代码内容为小猪cms演示页面简洁右侧栏代码，属于站长常用代码，更多在线客服代码请访问懒人图库JS代码频道。" />
    <title>小猪cms演示页面简洁右侧栏代码_懒人图库</title>

    <script src="scripts/jquery-1.9.1/jquery-1.9.1.min.js"></script>

    <link rel="stylesheet" href="css/index.css" />

</head>
<body style="height: 2000px;">
    <!-- 代码 开始 -->
    <div id="leftsead">
        <ul>           
            <li>
                <a href="http://wpa.qq.com/msgrd?v=3&uin=800022936&site=qq&menu=yes" target="_blank">
                    <div class="hides" style="width: 130px;height:50px; display: none;" id="qq">
                        <div class="hides" id="p1">
                            <img src="images/nav_1_1.png" />
                        </div>
                    </div>
                    <img src="images/nav_1.png" width="57px" height="50px" class="shows" />
                </a>
            </li>
            <li id="tel">
                <a href="javascript:void(0)">
                    <div class="hides" style="width: 130px;height:50px; display: none;" id="tels">
                        <div class="hides" id="p2">
                            <img src="images/nav_2_1.png">
                        </div>

                    </div>
                    <img src="images/nav_2.png" width="57px" height="50px" class="shows" />
                </a>
            </li>
            <li id="btn">
                <a id="top_btn" href="http://wpa.qq.com/msgrd?v=3&uin=3558103267&site=qq&menu=yes">
                    <div class="hides" style="width: 130px;height:50px; display: none">
                        <div class="hides" id="p3">
                            <img src="images/nav_3_1.png" width="130px;" height="50px" id="qq" />
                        </div>
                    </div>
                    <img src="images/nav_3.png" width="57px" height="50px" class="shows" />
                </a>
            </li>
            <li id="tel">
                <a href="#top">
                    <div class="hides" style="width: 130px; display: none" id="qq">
                        <div class="hides" id="p4">
                            <img src="images/nav_4_1.png" width="130px;" height="50px" />
                        </div>
                    </div>
                    <img src="images/nav_4.png" width="57px" height="50px" class="shows" />
                </a>
            </li>
        </ul>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#leftsead a").hover(function () {
                //if ($(this).prop("className") == "youhui") {
                //    $(this).children("img.hides").show();
                //} else {
                    $(this).children("div.hides").show();
                    $(this).children("img.shows").hide();
                    $(this).children("div.hides").animate({ marginRight: '0px' }, '0');
                //}
            }, function () {
                //if ($(this).prop("className") == "youhui") {
                //    $(this).children("img.hides").hide();
                //} else {
                    $(this).children("div.hides").animate({ marginRight: '-130px' }, 0, function () { $(this).hide(); $(this).next("img.shows").show(); });
                //}
            });

            $("#top_btn").click(function () { if (scroll == "off") return; $("html,body").animate({ scrollTop: 0 }, 600); });

            ////右侧导航 - 二维码
            //$(".youhui").mouseover(function () {
            //    $(this).children(".2wm").show();
            //})
            //$(".youhui").mouseout(function () {
            //    $(this).children(".2wm").hide();
            //});
        });


    </script>
    <!-- 代码 结束 -->

</body>
</html>
