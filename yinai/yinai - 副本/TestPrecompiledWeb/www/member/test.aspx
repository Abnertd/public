<%@ Page Language="C#" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script type="text/javascript" src="/Public/kindeditor/kindeditor-all.js"></script>
    <script type="text/javascript" src="/Public/kindeditor/lang/zh_CN.js"></script>
</head>
<body>
    <ul>
        <li>
            <textarea id="Bid_Content" name="Bid_Content" rows="1" cols="1"></textarea>
            <script type="text/javascript">
                var Product_IntroEditor;
                KindEditor.ready(function (K) {
                    Product_IntroEditor = K.create('#Bid_Content', {
                        width: '100%',
                        height: '500px',
                        filterMode: false

                    });
                });
            </script>
        </li>
    </ul>
</body>
</html>
